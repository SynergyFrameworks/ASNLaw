﻿//using OpenAI.Chat;
//using System.ClientModel;
//using System.Text;
//using System.Text.Json;

//namespace OpenAI.Examples;

//public partial class ChatExamples
//{

//    public async Task Example07_StructuredOutputsAsync()
//    {
//        ChatClient client = new("gpt-4o-mini", Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

//        ChatCompletionOptions options = new()
//        {
//            ResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
//                name: "math_reasoning",
//                jsonSchema: BinaryData.FromString("""
//                    {
//                        "type": "object",
//                        "properties": {
//                        "steps": {
//                            "type": "array",
//                            "items": {
//                            "type": "object",
//                            "properties": {
//                                "explanation": { "type": "string" },
//                                "output": { "type": "string" }
//                            },
//                            "required": ["explanation", "output"],
//                            "additionalProperties": false
//                            }
//                        },
//                        "final_answer": { "type": "string" }
//                        },
//                        "required": ["steps", "final_answer"],
//                        "additionalProperties": false
//                    }
//                    """),
//            strictSchemaEnabled: true)
//        };

//        ChatCompletion chatCompletion = await client.CompleteChatAsync(
//            ["How can I solve 8x + 7 = -23?"],
//            options);

//        using JsonDocument structuredJson = JsonDocument.Parse(chatCompletion.ToString());

//        Console.WriteLine($"Final answer: {structuredJson.RootElement.GetProperty("final_answer").GetString()}");
//        Console.WriteLine("Reasoning steps:");

//        foreach (JsonElement stepElement in structuredJson.RootElement.GetProperty("steps").EnumerateArray())
//        {
//            Console.WriteLine($"  - Explanation: {stepElement.GetProperty("explanation").GetString()}");
//            Console.WriteLine($"    Output: {stepElement.GetProperty("output")}");
//        }
//    }


//    public async Task Example06_SimpleChatProtocolAsync()
//    {
//        ChatClient client = new("gpt-4o", Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

//        BinaryData input = BinaryData.FromBytes("""
//            {
//               "model": "gpt-4o",
//               "messages": [
//                   {
//                       "role": "user",
//                       "content": "How does AI work? Explain it in simple terms."
//                   }
//               ]
//            }
//            """u8.ToArray());

//        using BinaryContent content = BinaryContent.Create(input);
//        ClientResult result = await client.CompleteChatAsync(content);
//        BinaryData output = result.GetRawResponse().Content;

//        using JsonDocument outputAsJson = JsonDocument.Parse(output.ToString());
//        string message = outputAsJson.RootElement
//            .GetProperty("choices"u8)[0]
//            .GetProperty("message"u8)
//            .GetProperty("content"u8)
//            .GetString();

//        Console.WriteLine($"[ASSISTANT]:");
//        Console.WriteLine($"{message}");
//    }

//    public async Task Example04_FunctionCallingStreamingAsync()
//    {
//        ChatClient client = new("gpt-4-turbo", Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

//        #region
//        List<ChatMessage> messages = [
//            new UserChatMessage("What's the weather like today?"),
//        ];

//        ChatCompletionOptions options = new()
//        {
//            //Tools = { getCurrentLocationTool, getCurrentWeatherTool },
//        };
//        #endregion

//        #region
//        bool requiresAction;

//        do
//        {
//            requiresAction = false;
//            Dictionary<int, string> indexToToolCallId = [];
//            Dictionary<int, string> indexToFunctionName = [];
//            Dictionary<int, StringBuilder> indexToFunctionArguments = [];
//            StringBuilder contentBuilder = new();
//            AsyncCollectionResult<StreamingChatCompletionUpdate> chatUpdates
//                = client.CompleteChatStreamingAsync(messages, options);

//            await foreach (StreamingChatCompletionUpdate chatUpdate in chatUpdates)
//            {
//                // Accumulate the text content as new updates arrive.
//                foreach (ChatMessageContentPart contentPart in chatUpdate.ContentUpdate)
//                {
//                    contentBuilder.Append(contentPart.Text);
//                }

//                // Build the tool calls as new updates arrive.
//                foreach (StreamingChatToolCallUpdate toolCallUpdate in chatUpdate.ToolCallUpdates)
//                {
//                    // Keep track of which tool call ID belongs to this update index.
//                    if (toolCallUpdate.Id is not null)
//                    {
//                        indexToToolCallId[toolCallUpdate.Index] = toolCallUpdate.Id;
//                    }

//                    // Keep track of which function name belongs to this update index.
//                    if (toolCallUpdate.FunctionName is not null)
//                    {
//                        indexToFunctionName[toolCallUpdate.Index] = toolCallUpdate.FunctionName;
//                    }

//                    // Keep track of which function arguments belong to this update index,
//                    // and accumulate the arguments string as new updates arrive.
//                    if (toolCallUpdate.FunctionArgumentsUpdate is not null)
//                    {
//                        StringBuilder argumentsBuilder
//                            = indexToFunctionArguments.TryGetValue(toolCallUpdate.Index, out StringBuilder existingBuilder)
//                                ? existingBuilder
//                                : new StringBuilder();
//                        argumentsBuilder.Append(toolCallUpdate.FunctionArgumentsUpdate);
//                        indexToFunctionArguments[toolCallUpdate.Index] = argumentsBuilder;
//                    }
//                }

//                switch (chatUpdate.FinishReason)
//                {
//                    case ChatFinishReason.Stop:
//                    {
//                        // Add the assistant message to the conversation history.
//                        messages.Add(new AssistantChatMessage(contentBuilder.ToString()));
//                        break;
//                    }

//                    case ChatFinishReason.ToolCalls:
//                    {
//                        // First, collect the accumulated function arguments into complete tool calls to be processed
//                        List<ChatToolCall> toolCalls = [];
//                        foreach ((int index, string toolCallId) in indexToToolCallId)
//                        {
//                            ChatToolCall toolCall = ChatToolCall.CreateFunctionToolCall(
//                                toolCallId,
//                                indexToFunctionName[index],
//                                indexToFunctionArguments[index].ToString());

//                            toolCalls.Add(toolCall);
//                        }

//                        // Next, add the assistant message with tool calls to the conversation history.
//                        string content = contentBuilder.Length > 0 ? contentBuilder.ToString() : null;
//                        messages.Add(new AssistantChatMessage(toolCalls, content));

//                        // Then, add a new tool message for each tool call to be resolved.
//                        //foreach (ChatToolCall toolCall in toolCalls)
//                        //{
//                        //    switch (toolCall.FunctionName)
//                        //    {
//                        //        case nameof(GetCurrentLocation):
//                        //        {
//                        //            string toolResult = GetCurrentLocation();
//                        //            messages.Add(new ToolChatMessage(toolCall.Id, toolResult));
//                        //            break;
//                        //        }

//                        //        case nameof(GetCurrentWeather):
//                        //        {
//                        //            // The arguments that the model wants to use to call the function are specified as a
//                        //            // stringified JSON object based on the schema defined in the tool definition. Note that
//                        //            // the model may hallucinate arguments too. Consequently, it is important to do the
//                        //            // appropriate parsing and validation before calling the function.
//                        //            using JsonDocument argumentsJson = JsonDocument.Parse(toolCall.FunctionArguments);
//                        //            bool hasLocation = argumentsJson.RootElement.TryGetProperty("location", out JsonElement location);
//                        //            bool hasUnit = argumentsJson.RootElement.TryGetProperty("unit", out JsonElement unit);

//                        //            if (!hasLocation)
//                        //            {
//                        //                throw new ArgumentNullException(nameof(location), "The location argument is required.");
//                        //            }

//                        //            string toolResult = hasUnit
//                        //                ? GetCurrentWeather(location.GetString(), unit.GetString())
//                        //                : GetCurrentWeather(location.GetString());
//                        //            messages.Add(new ToolChatMessage(toolCall.Id, toolResult));
//                        //            break;
//                        //        }

//                        //        default:
//                        //        {
//                        //            // Handle other unexpected calls.
//                        //            throw new NotImplementedException();
//                        //        }
//                        //    }
//                        //}

//                        requiresAction = true;
//                        break;
//                    }

//                    case ChatFinishReason.Length:
//                        throw new NotImplementedException("Incomplete model output due to MaxTokens parameter or token limit exceeded.");

//                    case ChatFinishReason.ContentFilter:
//                        throw new NotImplementedException("Omitted content due to a content filter flag.");

//                    case ChatFinishReason.FunctionCall:
//                        throw new NotImplementedException("Deprecated in favor of tool calls.");

//                    case null:
//                        break;
//                }
//            }
//        } while (requiresAction);
//        #endregion

//        #region
//        foreach (ChatMessage requestMessage in messages)
//        {
//            switch (requestMessage)
//            {
//                case SystemChatMessage systemMessage:
//                    Console.WriteLine($"[SYSTEM]:");
//                    Console.WriteLine($"{systemMessage.Content[0].Text}");
//                    Console.WriteLine();
//                    break;

//                case UserChatMessage userMessage:
//                    Console.WriteLine($"[USER]:");
//                    Console.WriteLine($"{userMessage.Content[0].Text}");
//                    Console.WriteLine();
//                    break;

//                case AssistantChatMessage assistantMessage when assistantMessage.Content.Count > 0:
//                    Console.WriteLine($"[ASSISTANT]:");
//                    Console.WriteLine($"{assistantMessage.Content[0].Text}");
//                    Console.WriteLine();
//                    break;

//                case ToolChatMessage:
//                    // Do not print any tool messages; let the assistant summarize the tool results instead.
//                    break;

//                default:
//                    break;
//            }
//        }
//        #endregion


//    }
//}