using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LocksSearch.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Media> Medias = new List<Media>();
    }
}
