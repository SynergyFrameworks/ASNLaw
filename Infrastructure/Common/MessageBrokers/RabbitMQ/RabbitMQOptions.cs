using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.MessageBrokers.RabbitMQ
{
    class RabbitMQOptions
    {
        public string Host { get; set; }
        public string VirtualHost { get; set; }
        public ushort? Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
