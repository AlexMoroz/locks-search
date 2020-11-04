using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LocksSearch.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LocksSearch.Services
{
    public class JsonDataImporter
    {
        public List<Building> Buildings { get; private set; }

        public List<Lock> Locks { get; private set; }

        public List<Group> Groups { get; private set; }

        public List<Media> Medias { get; private set; }

        public JsonDataImporter(string filePath)
        {
            string jsonData = File.ReadAllText(filePath);
            Dictionary<string, JArray> jsonObject = JsonConvert.DeserializeObject<Dictionary<string, JArray>>(jsonData);

            foreach (string token in jsonObject.Keys)
            {
                switch (token)
                {
                    case "buildings":
                        {
                            Buildings = jsonObject[token].Select(b => b.ToObject<Building>()).ToList();
                            break;
                        }
                    case "locks":
                        {
                            Locks = jsonObject[token].Select(b => b.ToObject<Lock>()).ToList();
                            break;
                        }
                    case "groups":
                        {
                            Groups = jsonObject[token].Select(b => b.ToObject<Group>()).ToList();
                            break;
                        }
                    case "media":
                        {
                            Medias = jsonObject[token].Select(b => b.ToObject<Media>()).ToList();
                            break;
                        }
                    default:
                        {
                            throw new JsonReaderException("Wrong data format.");
                        }
                }
            }
        }

    }
}
