using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Infrastructure.Common.Caching;
using Infrastructure.Common;
using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Infrastructure.JsonConverters
{
    public class PolymorphJsonConverter : JsonConverter
    {
        /// <summary>
        /// Factory methods for create instances of proper classes during deserialization
        /// </summary>
        private static readonly ConcurrentDictionary<Type, Func<JObject, object>> _convertFactories = new ConcurrentDictionary<Type, Func<JObject, object>>();

        /// <summary>
        /// Cache for conversion possibility (to reduce AbstractTypeFactory calls thru reflection)
        /// </summary>
        private static readonly ConcurrentDictionary<Type, bool> _canConvertCache = new ConcurrentDictionary<Type, bool>();

        /// <summary>
        /// Cache for instance creation method infos (to reduce AbstractTypeFactory calls thru reflection)
        /// </summary>
        private static readonly ConcurrentDictionary<string, MethodInfo> _createInstanceMethodsCache = new ConcurrentDictionary<string, MethodInfo>();

        public override bool CanWrite => false;

        public override bool CanRead => true;

        public override bool CanConvert(Type objectType)
        {
            if (objectType.IsPrimitive || objectType.Equals(typeof(string)) || objectType.IsArray)
            {
                return false;
            }

            return _canConvertCache.GetOrAdd(objectType, _ =>
            {
                return (bool)typeof(AbstractTypeFactory<>).MakeGenericType(objectType).GetProperty("HasOverrides").GetValue(null, null);
            });
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            object result;
            JObject obj = JObject.Load(reader);

            // Create instances for overrides and discriminator-less cases
            Func<JObject, object> factory = _convertFactories.GetOrAdd(objectType, obj2 =>
            {
                string key = CacheKey.With(nameof(PolymorphJsonConverter), objectType.FullName);
                MethodInfo tryCreateInstance = _createInstanceMethodsCache.GetOrAdd(key, _ => typeof(AbstractTypeFactory<>)
                    .MakeGenericType(objectType)
                    .GetMethod("TryCreateInstance", 0 /* This guarantees template-parameterless method */, new Type[] { }));

                return tryCreateInstance?.Invoke(null, null);
            });

            result = factory(obj);

            serializer.Populate(obj.CreateReader(), result);
            return result;
        }

        public static void RegisterTypeForDiscriminator(Type type, string discriminator)
        {
            RegisterType(type, obj =>
            {
                // Create discriminator-defined instances
                string typeName = type.Name;
                JToken pt = obj.GetValue(discriminator, StringComparison.InvariantCultureIgnoreCase);
                if (pt != null)
                {
                    typeName = pt.Value<string>();
                }

                string key = CacheKey.With(nameof(PolymorphJsonConverter), type.FullName, "+"/* To make a difference in keys for discriminator-specific methods */);
                MethodInfo tryCreateInstance = _createInstanceMethodsCache.GetOrAdd(key, _ => typeof(AbstractTypeFactory<>)
                    .MakeGenericType(type)
                    .GetMethod("TryCreateInstance", new Type[] { typeof(string) }));

                object result = tryCreateInstance?.Invoke(null, new[] { typeName });
                if (result == null)
                {
                    throw new NotSupportedException($"Unknown discriminator type name: {typeName}");
                }

                return result;
            });
        }

        public static void RegisterType(Type type, Func<JObject, object> factory)
        {
            _canConvertCache[type] = true;
            _convertFactories[type] = factory;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
