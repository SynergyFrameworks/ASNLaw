using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Options;
using Scion.Infrastructure.Extensions;
using Scion.Infrastructure.Telemetry;

namespace Scion.Infrastructure.Web.Telemetry
{
    /// <summary>
    /// Application insight telemetry processor which exclude all dependency SQL queries related to Hangfire.
    /// </summary>
    public class IgnoreSqlTelemetryProcessor : ITelemetryProcessor
    {
        private readonly ApplicationInsightsOptions _options;

        /// <summary>
        /// Just for looking at processor options from outside (logging, etc...)
        /// </summary>
        public IgnoreSqlTelemetryOptions Options => _options.IgnoreSqlTelemetryOptions;

        private ITelemetryProcessor Next { get; set; }

        // Link processors to each other in a chain.
        public IgnoreSqlTelemetryProcessor(IOptions<ApplicationInsightsOptions> options, ITelemetryProcessor next)
        {
            _options = options.Value;
            Next = next;
        }

        public void Process(ITelemetry item)
        {
            if (item is DependencyTelemetry dependencyTelemetry &&
                dependencyTelemetry.Type == "SQL")
            {
                foreach (string substring in _options.IgnoreSqlTelemetryOptions.QueryIgnoreSubstrings)
                {
                    if (!(dependencyTelemetry.Data.IsNullOrEmpty()) && dependencyTelemetry.Data.Contains(substring))
                    {
                        // To filter out an item, just terminate the chain:
                        return;
                    }
                }
            }
            // Send everything else:
            Next.Process(item);
        }
    }
}
