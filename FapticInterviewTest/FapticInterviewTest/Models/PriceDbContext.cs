using Microsoft.EntityFrameworkCore;

namespace FapticInterviewTest.Models
{   
    /// <summary>
    /// EF DbContext definition inside this class, for describing the PricesDb database structure
    /// </summary>
    public class PriceDbContext : DbContext
    {
        
        public PriceDbContext() 
        {
        }
        public PriceDbContext(DbContextOptions<PriceDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Configure the connection to the Database PricesDb by the options above: connection string read from appsettings.json
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            var connectionString = configuration.GetConnectionString("PricesDb");
            optionsBuilder.UseSqlServer(connectionString);
        }

        /// <summary>
        /// EF DbSet - Prices table 
        /// </summary>
        public DbSet<PriceModel> Prices { get; set; }
    }
}
