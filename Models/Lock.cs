using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocksSearch.Models
{
    public class Lock
    {
        [Key]
        public int Id { get; set; }
        public Guid Guid { get; set; }

        [NotMapped]
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
