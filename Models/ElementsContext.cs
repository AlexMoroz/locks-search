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
}
