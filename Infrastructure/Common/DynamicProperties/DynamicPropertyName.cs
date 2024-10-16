using Infrastructure.Common;

namespace Infrastructure.DynamicProperties
{
    public class DynamicPropertyName : ValueObject
    {
        /// <summary>
        /// Language ID, e.g. en-US.
        /// </summary>
        public string Locale { get; set; }
        public string Name { get; set; }

    }
}
