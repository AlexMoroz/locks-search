using Microsoft.EntityFrameworkCore;

namespace LocksSearch.Models
{
    public class ElementsContext : DbContext
    {
        public ElementsContext(DbContextOptions<ElementsContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lock>()
                .HasOne(l => l.Building)
                .WithMany(b => b.Locks)
                .HasForeignKey(l => l.BuildingId);

            modelBuilder.Entity<Media>()
                .HasOne(m => m.Group)
                .WithMany(g => g.Medias)
                .HasForeignKey(m => m.GroupId);
        }

        public DbSet<Building> Buildings { get; set; }
        public DbSet<Lock> Locks { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Media> Medias { get; set; }
    }
}
