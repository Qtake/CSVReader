using Microsoft.EntityFrameworkCore;

namespace CSVReader.DataBase
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Record> Records { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=People;Trusted_Connection=True;");
        }
    }
}
