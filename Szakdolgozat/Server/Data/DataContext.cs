namespace Szakdolgozat.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MealVariant>()
                .HasKey(p => new { p.MealId, p.MealTypeId });

            modelBuilder.Entity<MealType>().HasData(
                new MealType { Id = 1, Name = "Vörös bor" },
                new MealType { Id = 2, Name = "Fehér bor" },
                new MealType { Id = 3, Name = "Premium Lager" },
                new MealType { Id = 4, Name = "Ale" },
                new MealType { Id = 5, Name = "Epres" },
                new MealType { Id = 6, Name = "Mangós" },
                new MealType { Id = 7, Name = "Licsis" },
                new MealType { Id = 8, Name = "Bolognai Spaghetti" },
                new MealType { Id = 9, Name = "Carbonara Spaghetti" },
                new MealType { Id = 10, Name = "Margharita Pizza" });


            

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
                    
                     ImageUrl = "https://supervalu.ie/thumbnail/800x600/var/files/real-food/recipes/Uploaded-2020/spaghetti-bolognese-recipe.jpg",
                     CategoryId = 1

                 },

               new Meal
               {
                   Id = 2,
                   Name = "Carbonara Spaghetti",
                   Description = "Durum spaghetti tészta, tojásos pecorino romano öntettel guanchaleval",
                   Allergen = "Glutén, Tojás, Laktóz",
                  
                   ImageUrl = "https://www.sipandfeast.com/wp-content/uploads/2022/09/spaghetti-carbonara-recipe-snippet.jpg",
                   CategoryId = 1

               },

               new Meal
               {
                   Id = 3,
                   Name = "Margherita Pizza",
                   Description = "Nápolyi pizza paradicsomos alappal, bazsalikommal bölény mozzarellával olivaolajjal",
                   Allergen = "laktóz, glutén",
                   ImageUrl = "https://assets.biggreenegg.eu/app/uploads/2018/06/28115815/topimage-pizza-special17-800x500.jpg",
                   CategoryId = 1,
                 
               },
               new Meal
               {
                   Id = 4,
                   Name = "Sör",
                   Description = "0,5 Liter csapolt sör",
                   Allergen = "Alkohol",
                   ImageUrl = "https://proaktivdirekt.com/adaptive/article_md/upload/images/magazine/sor.jpg",
                   CategoryId = 3,

               },
               new Meal
               {
                   Id = 5,
                   Name = "Bor",
                   Description = "A frizbiolaj pincészet külömböző típusú borai az ár dl-ben értendő",
                   Allergen = "alkohol",
                   ImageUrl = "https://joopince.hu/wp-content/uploads/2022/02/bor-fajtak.jpg",
                   CategoryId = 3,

               },
               new Meal
               {
                   Id = 6,
                   Name = "Limonádé",
                   Description = "Házi limonádi valódi gyümölcsből",
                   Allergen = "nincs, kivéve az aktuális gyömölcs",
                   ImageUrl = "https://receptneked.hu/wp-content/uploads/2020/08/104237976_s.jpg",
                   CategoryId = 3,

               }
               );

            modelBuilder.Entity<MealVariant>().HasData(
                 new MealVariant
                 {
                     MealId = 4, //Sör
                     MealTypeId = 3, //Premium Lager
                     Price = 1000.00m,
                     OriginalPrice = 800.00m 
                 },
                 new MealVariant
                 {
                     MealId = 4, //Sör
                     MealTypeId = 4, //Ale
                     Price = 1200.00m,
                     OriginalPrice = 800.00m
                 },
                 new MealVariant
                 {
                     MealId = 5, //Bor
                     MealTypeId = 1, //Vörös
                     Price = 400.00m,
                     OriginalPrice = 0.00m
                 },
                 new MealVariant
                   {
                       MealId = 5, //Bor
                       MealTypeId = 2, //Fehér bor
                       Price = 300.00m,
                       OriginalPrice = 0.00m
                 },
                 new MealVariant
                 {
                     MealId = 6, //Limo
                     MealTypeId = 5, //Eper
                     Price = 1000.00m,
                     OriginalPrice = 1000.00m
                 },
                 new MealVariant
                 {
                     MealId = 6, //Limo
                     MealTypeId = 6, //Mangó
                     Price = 1000.00m,
                     OriginalPrice = 1000.00m
                 },
                 new MealVariant
                 {
                     MealId = 6, //Limo
                     MealTypeId = 7, //Licsi
                     Price = 1000.00m,
                     OriginalPrice = 1000.00m
                 },
                 new MealVariant
                 {
                     MealId = 1, //Spaghetti
                     MealTypeId = 7, //Bolognai
                     Price = 2500,
                     OriginalPrice = 2500
                 },
                 new MealVariant
                 {
                     MealId = 2, //Spaghetti
                     MealTypeId = 7,//Carbonarra
                     Price = 2200,
                     OriginalPrice = 2200m
                 },
                 new MealVariant
                 {
                     MealId = 3, //Pizza
                     MealTypeId = 7, //Margharita
                     Price = 2300,
                     OriginalPrice = 2300
                 }


                );;





            ;
        }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MealType> MealTypes { get; set; }
        public DbSet<MealVariant> MealVariants { get; set; }




    }





}
