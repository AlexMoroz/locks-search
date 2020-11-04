using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocksSearch.Models
{
    public class Media
    {
        [Key]
        public int Id { get; set; }
        public Guid Guid { get; set; }

        [NotMapped]
        public Guid GroupGuid { get; set; }
        public Group Group { get; set; }
        public string Type { get; set; }
        public string Owner { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
    }
}
