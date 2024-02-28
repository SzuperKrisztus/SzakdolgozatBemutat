namespace Szakdolgozat.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Main dish",
                    Url = "maindish",
                },
                new Category
                {
                    Id = 2,
                    Name = "Appetizer",
                    Url = "appetizer",
                },
                new Category
                {
                    Id = 3,
                    Name = "Drink",
                    Url = "drink",
                });

            modelBuilder.Entity<Meal>().HasData(
                 new Meal
                 {
                     Id = 1,
                     Name = "Bolgonai Spaghetti",
                     Description = "Durum spaghetti tészta, paradicsomos marha raguval és parmezánnal",
                     Allergen = "Laktóz, glutén",
                     Price = 1000,
                     ImageUrl = "https://supervalu.ie/thumbnail/800x600/var/files/real-food/recipes/Uploaded-2020/spaghetti-bolognese-recipe.jpg",
                     CategoryId = 1

                 },

               new Meal
               {
                   Id = 2,
                   Name = "Carbonara Spaghetti",
                   Description = "Durum spaghetti tészta, tojásos pecorino romano öntettel guanchaleval",
                   Allergen = "Glutén, Tojás, Laktóz",
                   Price = 1000,
                   ImageUrl = "https://www.sipandfeast.com/wp-content/uploads/2022/09/spaghetti-carbonara-recipe-snippet.jpg",
                   CategoryId = 1

               },

               new Meal
               {
                   Id = 3,
                   Name = "Margherita Pizza",
                   Description = "Nápolyi pizza paradicsomos alappal, bazsalikommal bölény mozzarellával olivaolajjal",
                   Allergen = "laktóz, glutén",
                   Price = 1000,
                   ImageUrl = "https://assets.biggreenegg.eu/app/uploads/2018/06/28115815/topimage-pizza-special17-800x500.jpg",
                   CategoryId = 1,
                 
               }
                ); ;
        }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Category> Categories { get; set; }



    }





}
