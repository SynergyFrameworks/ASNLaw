using Elasticsearch.Net;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

internal sealed class ProcessOutboxMessagesJob : IProcessOutboxMessagesJob
{
    private const int BatchSize = 15;

    private static readonly JsonSerializerSettings JsonSeriaIizerSettings = new()
    {

        TypeNameHandling = TypeNameHandling.All

    };

    private readonly IDbConnectionFactory _dbConnectionFactory;
    private readonly IPublisher _publisher;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILogger<ProcessOutboxMessagesJob> _logger;

    public ProcessOutboxMessagesJob(IDbConnectionFactory dbConnectionFactory,
    IPublisher publisher,
    IDateTimeProvider dateTimeProvider,
    ILogger<ProcessOutboxMessagesJob> logger)
    {
        _dbConnectionFactory = dbConnectionFactory;
        _publisher = publisher;
        _dateTimeProvider = dateTimeProvider;
        _logger = logger;
    }

    public async Task ProcessAsync()
    {
        _logger.LogInformation("Beginning to process outbox messages");

        //  using IDbConnection connection = _dbConnectionFactory.GetOpenConnection();
        //using IDbTransaction transaction = connection.BeginTransaction();


        // IReadOnlyList<OutB




    }



}