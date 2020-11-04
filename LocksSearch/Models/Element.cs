using System;
namespace LocksSearch.Models
{
    public class Element
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
        public string Floor { get; set; }
        public string RoomNumber { get; set; }
        public string ShortCut { get; set; }
        public string Owner { get; set; }
    }
}
