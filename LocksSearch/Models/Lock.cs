using LocksSearch.Converters;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocksSearch.Models
{
    public class Lock
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        [JsonProperty("id")]
        [JsonConverter(typeof(GuidConverter))]
        public Guid Guid { get; set; }

        [NotMapped]
        [JsonProperty("buildingId")]
        [JsonConverter(typeof(GuidConverter))]
        public Guid BuildingGuid { get; set; }

        public Building Building { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string SerialNumber { get; set; }

        public string Floor { get; set; }

        public string RoomNumber { get; set; }
    }
}
