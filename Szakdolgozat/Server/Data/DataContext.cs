namespace Szakdolgozat.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) { }

        public DbSet<Meal> Meals { get; set; }


    }
}
