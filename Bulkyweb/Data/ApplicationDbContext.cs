using Bulkyweb.Models;
using Microsoft.EntityFrameworkCore;

namespace Bulkyweb.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) {
        

        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(

                new Category { DisplayOrder = 1, Id = 1, Name = "ridan" },
                new Category { DisplayOrder = 2, Id = 2, Name = "hassan" },
                new Category { DisplayOrder = 3, Id = 3, Name = "shikder" }

                );
        }
    }
}
