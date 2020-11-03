using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LocksSearch.Models
{
    public class ElementsContext : DbContext
    {
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Lock> Locks { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Media> Medias { get; set; }
    }

    public class Building
    {
        public Guid id;
        public string shortCut;
        public string name;
        public string description;

        public List<Lock> Locks = new List<Lock>();
    }

    public class Lock
    {
        public Guid id;
        public Building building;
        public string type;
        public string name;
        public string description;
        public string serialNumber; 
        public string floor;
        public string roomNumber;
    }

    public class Group
    {
        public Guid id;
        public string name;
        public string description;

        public List<Media> Medias = new List<Media>();
    }

    public class Media
    {
        public Guid id;
        public Group group;
        public string type;
        public string owner;
        public string description;
        public string serialNumber;
    }
}
