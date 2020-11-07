using LocksSearch.Models;
using NRediSearch;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
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

        //public static Document ToDocument(this Building building)
        //{
        //    var fields = new Dictionary<string, RedisValue>();

        //    fields

        //    return new Document(building.Guid.ToString(), fields);
        //}

        private static void AddClass(this Schema schema, string className, Dictionary<string, int> properties)
        {
            foreach (var key in properties.Keys)
            {
                schema.AddTextField(className + key, weight: properties[key]);
            }
        }

        private static void AddClassName(this Schema schema)
        {
            schema.AddField(new TextField(CLASS_NAME, noIndex: true));
        }
    }
}
