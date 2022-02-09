using Hectre.BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace Hectre.BackEnd.Data
{
    /// <summary>
    /// Database context used for the application
    /// </summary>
    public class HectreDbContext : DbContext
    {
        public HectreDbContext(DbContextOptions<HectreDbContext> options) : base(options)
        {

        }

        public DbSet<Chemical> Chemicals { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Chemical.ModelBuilder(modelBuilder);
        }
    }
}
