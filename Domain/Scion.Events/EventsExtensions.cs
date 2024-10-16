using ASNInfrastructure.Events;
using ASNInfrastructure.MessageBrokers;
using Microsoft.AspNetCore.Builder;
using System;
using System.Linq;
using System.Reflection;

namespace ASNEvents
{
    public static class EventsExtensions
    {
        public static IApplicationBuilder UseSubscribeAllEvents(this IApplicationBuilder app)
        {
            var types = typeof(EventsExtensions).GetTypeInfo().Assembly.GetTypes()
                .Where(mytype => mytype.GetInterfaces().Contains(typeof(IEvent)));

            foreach (var type in types)
            {
                app.UseSubscribeEvent(type);
            }

            return app;
        }
    }
}
