using Microsoft.CognitiveServices.Speech;
using LawUI.Domain.Model;
using LawUI.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

public class SpeechToTextViewModel
{
    private readonly ILogger<SpeechToTextViewModel> _logger;
    private readonly SpeechRecognizer _recognizer;
    private readonly MongoDbService _mongoDbService;
    private readonly AuthenticationStateProvider _authStateProvider;

    public SpeechModel Model { get; set; } = new SpeechModel();
    private readonly Stack<ICommand> _undoStack = new();
    private readonly Stack<ICommand> _redoStack = new();

    public SpeechToTextViewModel(
        ILogger<SpeechToTextViewModel> logger,
        SpeechRecognizer recognizer,
        MongoDbService mongoDbService,
        AuthenticationStateProvider authStateProvider)
    {
        _logger = logger;
        _recognizer = recognizer;
        _mongoDbService = mongoDbService;
        _authStateProvider = authStateProvider;
    }

    // Command pattern for undo/redo functionality
    public interface ICommand
    {
        void Execute();
        void Unexecute();
    }

    public class EditCommand : ICommand
    {
        private readonly string _previous;
        private readonly string _current;
        private readonly Action<string> _applyChange;

        public EditCommand(string previous, string current, Action<string> applyChange)
        {
            _previous = previous;
            _current = current;
            _applyChange = applyChange;
        }

        public void Execute()
        {
            _applyChange(_current);
        }

        public void Unexecute()
        {
            _applyChange(_previous);
        }
    }

    public void OnTranscriptionChanged(string newTranscription)
    {
        var command = new EditCommand(Model.LastTranscription, newTranscription, (t) => Model.Transcription = t);
        command.Execute();
        _undoStack.Push(command);
        _redoStack.Clear(); // Clear redo stack whenever a new change is made
        Model.LastTranscription = newTranscription;
    }

    public void Undo()
    {
        if (_undoStack.Count > 0)
        {
            var command = _undoStack.Pop();
            command.Unexecute();
            _redoStack.Push(command);
        }
    }

    public void Redo()
    {
        if (_redoStack.Count > 0)
        {
            var command = _redoStack.Pop();
            command.Execute();
            _undoStack.Push(command);
        }
    }

    public async Task StartRecognitionAsync()
    {
        if (Model.IsRecognizingSpeech)
            return;

        Model.IsRecognizingSpeech = true;
        try
        {
            var result = await _recognizer.RecognizeOnceAsync();
            if (result.Reason == ResultReason.RecognizedSpeech)
            {
                OnTranscriptionChanged(result.Text);
                await SaveTranscriptionToMongoDbAsync(result.Text);
            }
            else
            {
                _logger.LogWarning("No speech recognized or recognition canceled");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Error during speech recognition: {ErrorMessage}", ex.Message);
        }
        finally
        {
            Model.IsRecognizingSpeech = false;
        }
    }

    private async Task SaveTranscriptionToMongoDbAsync(string transcription)
    {
        try
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("User ID not found. Transcription not saved.");
                return;
            }

            var entry = new SpeechToTextEntry
            {
                UserId = userId,
                Transcription = transcription,
                Timestamp = DateTime.UtcNow
            };

            await _mongoDbService.InsertSpeechToTextEntryAsync(entry);
            _logger.LogInformation("Transcription saved to MongoDB for user {UserId}", userId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving transcription to MongoDB");
        }
    }
}