using System.Data.Entity;

namespace RetroCarShop.Models
{
    public class CarContext : DbContext
    {
        public CarContext() : base("RetroCarContext") { }

        public DbSet<Car> Cars { get; set; }
    }
}