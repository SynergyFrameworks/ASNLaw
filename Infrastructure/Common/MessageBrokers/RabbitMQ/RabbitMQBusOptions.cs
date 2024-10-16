//using RawRabbit.Configuration;

namespace Infrastructure.Common.MessageBrokers.RabbitMQ
{
    public class RabbitMQBusOptions //: RawRabbitConfiguration
    {
        private QueueOptions queue;

        public QueueOptions GetQueue()
        {
            return queue;
        }

        public void SetQueue(QueueOptions value)
        {
            queue = value;
        }

        private ExchangeOptions exchange;

        public ExchangeOptions GetExchange()
        {
            return exchange;
        }

        public void SetExchange(ExchangeOptions value)
        {
            exchange = value;
        }
    }

    public class QueueOptions //: GeneralQueueConfiguration
    {
        public string Name { get; set; }
    }

    public class ExchangeOptions //: GeneralExchangeConfiguration
    {
        public string Name { get; set; }
    }
}
