using Microsoft.EntityFrameworkCore;

namespace CSVReader.Models.DataBase
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Record> Records { get; set; } = null!;
        private readonly string _connectionString;

        public ApplicationContext(string connectionString)
        {
            _connectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
