using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LocksSearch.Models
{
    public class ElementsContext : DbContext
    {
        public ElementsContext(DbContextOptions<ElementsContext> options)
            : base(options)
        { }

        public DbSet<Building> Buildings { get; set; }
        public DbSet<Lock> Locks { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Media> Medias { get; set; }
    }

    public class Building
    {
        [Key]
        public Guid Id { get; set; }
        public string ShortCut { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Lock> Locks = new List<Lock>();
    }

    public class Lock
    {
        [Key]
        public Guid Id { get; set; }
        public Building Building { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
        public string Floor { get; set; }
        public string RoomNumber { get; set; }
    }

    public class Group
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Media> Medias = new List<Media>();
    }

    public class Media
    {
        [Key]
        public Guid Id { get; set; }
        public Group Group { get; set; }
        public string Type { get; set; }
        public string Owner { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
    }
}
