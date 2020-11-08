﻿using LocksSearch.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        public static Document ToDocument<T>(T element) where T:class
        {
            //TODO: use reflection with attributes here

            string guid;
            var fields = new Dictionary<string, RedisValue>();
            switch(element)
            {
                case Building building:
                    guid = building.Guid.ToString();
                    fields.Add(CLASS_NAME, nameof(Building));
                    fields.Add("Building:Name", building.Name);
                    fields.Add("Building:Description", building.Description);
                    fields.Add("Building:ShortCut", building.ShortCut);
                    break;
                case Lock lock1:
                    guid = lock1.Guid.ToString();
                    fields.Add(CLASS_NAME, nameof(Lock));
                    fields.Add("Lock:Name", lock1.Name);
                    fields.Add("Lock:Type", lock1.Type);
                    fields.Add("Lock:SerialNumber", lock1.SerialNumber);
                    fields.Add("Lock:Floor", lock1.Floor ?? "");
                    fields.Add("Lock:RoomNumber", lock1.RoomNumber);
                    fields.Add("Lock:Description", lock1.Description ?? "");
                    fields.Add("Lock:Building:Name", lock1.Building.Name);
                    fields.Add("Lock:Building:ShortCut", lock1.Building.ShortCut);
                    fields.Add("Lock:Building:Description", lock1.Building.Description);
                    break;
                case Group group:
                    guid = group.Guid.ToString();
                    fields.Add(CLASS_NAME, nameof(Group));
                    fields.Add("Group:Name", group.Name);
                    fields.Add("Group:Description", group.Description ?? "");
                    break;
                case Media media:
                    guid = media.Guid.ToString();
                    fields.Add(CLASS_NAME, nameof(Media));
                    fields.Add("Lock:Owner", media.Owner);
                    fields.Add("Lock:Type", media.Type);
                    fields.Add("Lock:SerialNumber", media.SerialNumber);
                    fields.Add("Lock:Description", media.Description ?? "");
                    fields.Add("Lock:Group:Name", media.Group.Name);
                    fields.Add("Lock:Group:Description", media.Group.Description ?? "");
                    break;
                default:
                    throw new NotImplementedException("Object type is not supported.");
            }

            return new Document(guid, fields);
        }

        public static T CastDocument<T>(Document doc) where T: class
        {
            var jsonDocument = JsonConvert.SerializeObject(doc.GetPropertiesSimplified());
            JObject jsonObject = JObject.Parse(jsonDocument);
            jsonObject["Guid"] = doc.Id;
            return jsonObject.ToObject<T>();
        }

        private static Dictionary<string, RedisValue> GetPropertiesSimplified(this Document doc)
        {
            var newProperties = new Dictionary<string, RedisValue>();
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