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
        public int Id { get; set; }

        [JsonProperty("id")]
        [JsonConverter(typeof(GuidConverter))]
        public Guid Guid { get; set; }

        [NotMapped]
        [JsonProperty("groupId")]
        [JsonConverter(typeof(GuidConverter))]
        public Guid GroupGuid { get; set; }

        public Group Group { get; set; }

        public string Type { get; set; }

        public string Owner { get; set; }

        public string Description { get; set; }

        public string SerialNumber { get; set; }
    }
}
