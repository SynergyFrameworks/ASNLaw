using System.Collections.Generic;

namespace Infrastructure.Common.Domain.Performance
{
    public class PmTemplateAdditionalData
    {
        //The label on the addtitional data option displayed on the client.
        public string Label { get; set; }

        //Key is what the user sees as the label for the option, value is what is passed to the api.
        //Should be more than one entry in the list, otherwise what's the point?
        public IEnumerable<Dictionary<string, object>> Options { get; set; }
    }
}
