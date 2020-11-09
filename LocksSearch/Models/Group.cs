using LocksSearch.Attributes;
using LocksSearch.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LocksSearch.Models
{
    public class Group
    {
        [Key]
        [JsonIgnore]
        [RediSearchIgnore]
        public int Id { get; set; }

        [JsonProperty("id")]
        [JsonConverter(typeof(GuidConverter))]
        [RediSearchIgnore]
        public Guid Guid { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [JsonIgnore]
        [RediSearchIgnore]
        public List<Media> Medias { get; set; } = new List<Media>();
    }
}
