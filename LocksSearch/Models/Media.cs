using LocksSearch.Attributes;
using LocksSearch.Converters;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocksSearch.Models
{
    public class Media
    {
        [Key]
        [JsonIgnore]
        [RediSearchIgnore]
        public int Id { get; set; }

        [JsonProperty("id")]
        [JsonConverter(typeof(GuidConverter))]
        [RediSearchIgnore]
        public Guid Guid { get; set; }

        [NotMapped]
        [JsonProperty("groupId")]
        [JsonConverter(typeof(GuidConverter))]
        [RediSearchIgnore]
        public Guid GroupGuid { get; set; }

        [JsonIgnore]
        [RediSearchIgnore]
        public int GroupId { get; set; }

        [JsonIgnore]
        [RediSearchTransitional]
        public Group Group { get; set; }

        public string Type { get; set; }

        public string Owner { get; set; }

        public string Description { get; set; }

        public string SerialNumber { get; set; }
    }
}
