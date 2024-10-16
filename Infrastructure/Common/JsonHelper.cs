using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using Newtonsoft.Json;

namespace Infrastructure.Common
{
    public static class JsonHelper 
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(JsonHelper));
        public static Dictionary<string, object> ParseJson(string json)
        {
            try
            {
                var childDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                for (int i = 0; i < childDictionary.Count; i++)
                {
                    var entry = childDictionary.ElementAt(i);
                    if (entry.Value != null)
                    {
                        var value = entry.Value.ToString();
                        if (value[0].Equals('{'))
                        {
                            var child = ParseJson(value);
                            //if any child fails in parsing, chain the nulls back to the top.
                            if (child == null)
                            {
                                Log.ErrorFormat("Error parsing Json in child object: {0}", entry.Key);
                                return null;
                            }
                            childDictionary[entry.Key] = child;
                        }
                    }
                }
                return childDictionary;
            }
            catch (Exception e)
            {
                Log.ErrorFormat("error parsing json for tenant settings: {0}", e.Message);
                return null;
            }
        }
    }
}
