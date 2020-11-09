using LocksSearch.Attributes;
using LocksSearch.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NRediSearch;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static NRediSearch.Schema;

namespace LocksSearch.Helpers
{
    public static class RediSearchHelper
    {
        public static string CLASS_NAME = "ClassName";

        public static Schema CreateSchema()
        {
            var schema = new Schema();
            // add class marker
            schema.AddClassName();
            // add Classes to Schema
            schema.AddClass(nameof(WeightsData.Building), WeightsData.Building);
            schema.AddClass(nameof(WeightsData.Lock), WeightsData.Lock);
            schema.AddClass(nameof(WeightsData.Group), WeightsData.Group);
            schema.AddClass(nameof(WeightsData.Media), WeightsData.Media);

            return schema;
        }

        public static Document ToDocument<T>(T element) where T : class
        {
            return ConvertToDocument(element);
        }

        public static Dictionary<string, string> CastDocumentToDict(Document doc)
        {
            // we lowercase properies to support snakeCase naming policy for JSON
            var jsonDict = doc.GetPropertiesWithoutTag().LowerFirstLetters();
            jsonDict["Guid"] = doc.Id;
            return jsonDict;
        }

        private static Document ConvertToDocument(dynamic element)
        {
            Type type = element.GetType();
            Dictionary<string, RedisValue> fields = PropertiesToDict(type.Name, element);
            fields.Add(CLASS_NAME, type.Name);
            return new Document(element.Guid.ToString(), fields);
        }

        private static Dictionary<string, RedisValue> PropertiesToDict(string tag, dynamic element)
        {
            Type type = element.GetType();
            var properties = new Dictionary<string, RedisValue>();
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                var key = string.Join(':', tag, propertyInfo.Name);
                object value = propertyInfo.GetValue(element);

                if (Attribute.IsDefined(propertyInfo, typeof(RediSearchIgnore)))
                {
                    continue;
                }
                else if (Attribute.IsDefined(propertyInfo, typeof(RediSearchTransitional)))
                {
                    Type innerType = value.GetType();
                    var innerProperties = PropertiesToDict(innerType.Name, value);
                    foreach (var pair in innerProperties)
                    {
                        properties.Add(string.Join(':', tag, pair.Key), pair.Value);
                    }
                }
                else
                {
                    properties.Add(key, (string)value ?? string.Empty);
                }
            }

            return properties;
        }

        private static Dictionary<string, string> GetPropertiesWithoutTag(this Document doc)
        {
            var newProperties = new Dictionary<string, string>();
            // remove classname from properties and transitive properties
            foreach (KeyValuePair<string, RedisValue> pair in doc.GetProperties())
            {
                var parts = pair.Key.Split(':');
                var value = pair.Value;

                if (parts.Length == 1)
                {
                    newProperties[parts[0]] = value;
                    continue;
                }

                if (parts.Length > 2)
                {
                    // ignore transitive class data
                    continue;
                }
                else
                {
                    newProperties[parts[1]] = value;
                }
            }

            return newProperties;
        }

        private static Dictionary<string, string> LowerFirstLetters(this Dictionary<string, string> properties)
        {
            var newProperties = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> pair in properties)
            {
                var key = pair.Key;
                key = char.ToLower(key[0]) + key.Substring(1);
                newProperties[key] = pair.Value;
            }

            return newProperties;
        }

        private static void AddClass(this Schema schema, string className, Dictionary<string, int> properties)
        {
            foreach (var key in properties.Keys)
            {
                schema.AddTextField(string.Join(':', className, key), weight: properties[key]);
            }
        }

        private static void AddClassName(this Schema schema)
        {
            schema.AddField(new TextField(CLASS_NAME, noIndex: true));
        }
    }
}
