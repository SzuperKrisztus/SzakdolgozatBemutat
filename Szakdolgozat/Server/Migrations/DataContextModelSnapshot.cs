﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Szakdolgozat.Server.Data;

#nullable disable

namespace Szakdolgozat.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Szakdolgozat.Shared.CartMeal", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("MealId")
                        .HasColumnType("int");

                    b.Property<int>("MealTypeId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("UserId", "MealId", "MealTypeId");

                    b.ToTable("CartMeals");
                });

            modelBuilder.Entity("Szakdolgozat.Shared.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Main dish",
                            Url = "maindish"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Appetizer",
                            Url = "appetizer"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Drink",
                            Url = "drink"
                        });
                });

            modelBuilder.Entity("Szakdolgozat.Shared.Meal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Allergen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Meals");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Allergen = "Laktóz, glutén",
                            CategoryId = 1,
                            Description = "Durum spaghetti tészta, paradicsomos marha raguval és parmezánnal",
                            ImageUrl = "https://supervalu.ie/thumbnail/800x600/var/files/real-food/recipes/Uploaded-2020/spaghetti-bolognese-recipe.jpg",
                            Name = "Bolgonai Spaghetti"
                        },
                        new
                        {
                            Id = 2,
                            Allergen = "Glutén, Tojás, Laktóz",
                            CategoryId = 1,
                            Description = "Durum spaghetti tészta, tojásos pecorino romano öntettel guanchaleval",
                            ImageUrl = "https://www.sipandfeast.com/wp-content/uploads/2022/09/spaghetti-carbonara-recipe-snippet.jpg",
                            Name = "Carbonara Spaghetti"
                        },
                        new
                        {
                            Id = 3,
                            Allergen = "laktóz, glutén",
                            CategoryId = 1,
                            Description = "Nápolyi pizza paradicsomos alappal, bazsalikommal bölény mozzarellával olivaolajjal",
                            ImageUrl = "https://assets.biggreenegg.eu/app/uploads/2018/06/28115815/topimage-pizza-special17-800x500.jpg",
                            Name = "Margherita Pizza"
                        },
                        new
                        {
                            Id = 4,
                            Allergen = "Alkohol",
                            CategoryId = 3,
                            Description = "0,5 Liter csapolt sör",
                            ImageUrl = "https://proaktivdirekt.com/adaptive/article_md/upload/images/magazine/sor.jpg",
                            Name = "Sör"
                        },
                        new
                        {
                            Id = 5,
                            Allergen = "alkohol",
                            CategoryId = 3,
                            Description = "A frizbiolaj pincészet külömböző típusú borai az ár dl-ben értendő",
                            ImageUrl = "https://joopince.hu/wp-content/uploads/2022/02/bor-fajtak.jpg",
                            Name = "Bor"
                        },
                        new
                        {
                            Id = 6,
                            Allergen = "nincs, kivéve az aktuális gyömölcs",
                            CategoryId = 3,
                            Description = "Házi limonádi valódi gyümölcsből",
                            ImageUrl = "https://receptneked.hu/wp-content/uploads/2020/08/104237976_s.jpg",
                            Name = "Limonádé"
                        });
                });

            modelBuilder.Entity("Szakdolgozat.Shared.MealType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MealTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Vörös bor"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Fehér bor"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Premium Lager"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Ale"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Epres"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Mangós"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Licsis"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Bolognai Spaghetti"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Carbonara Spaghetti"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Margharita Pizza"
                        });
                });

            modelBuilder.Entity("Szakdolgozat.Shared.MealVariant", b =>
                {
                    b.Property<int>("MealId")
                        .HasColumnType("int");

                    b.Property<int>("MealTypeId")
                        .HasColumnType("int");

                    b.Property<decimal>("OriginalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("MealId", "MealTypeId");

                    b.HasIndex("MealTypeId");

                    b.ToTable("MealVariants");

                    b.HasData(
                        new
                        {
                            MealId = 4,
                            MealTypeId = 3,
                            OriginalPrice = 800.00m,
                            Price = 1000.00m
                        },
                        new
                        {
                            MealId = 4,
                            MealTypeId = 4,
                            OriginalPrice = 800.00m,
                            Price = 1200.00m
                        },
                        new
                        {
                            MealId = 5,
                            MealTypeId = 1,
                            OriginalPrice = 0.00m,
                            Price = 400.00m
                        },
                        new
                        {
                            MealId = 5,
                            MealTypeId = 2,
                            OriginalPrice = 0.00m,
                            Price = 300.00m
                        },
                        new
                        {
                            MealId = 6,
                            MealTypeId = 5,
                            OriginalPrice = 1000.00m,
                            Price = 1000.00m
                        },
                        new
                        {
                            MealId = 6,
                            MealTypeId = 6,
                            OriginalPrice = 1000.00m,
                            Price = 1000.00m
                        },
                        new
                        {
                            MealId = 6,
                            MealTypeId = 7,
                            OriginalPrice = 1000.00m,
                            Price = 1000.00m
                        },
                        new
                        {
                            MealId = 1,
                            MealTypeId = 8,
                            OriginalPrice = 2500m,
                            Price = 2500m
                        },
                        new
                        {
                            MealId = 2,
                            MealTypeId = 9,
                            OriginalPrice = 2200m,
                            Price = 2200m
                        },
                        new
                        {
                            MealId = 3,
                            MealTypeId = 10,
                            OriginalPrice = 2300m,
                            Price = 2300m
                        });
                });

            modelBuilder.Entity("Szakdolgozat.Shared.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Szakdolgozat.Shared.OrderItem", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("MealId")
                        .HasColumnType("int");

                    b.Property<int>("MealTypeId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "MealId", "MealTypeId");

                    b.HasIndex("MealId");

                    b.HasIndex("MealTypeId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Szakdolgozat.Shared.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2024, 3, 28, 13, 30, 10, 205, DateTimeKind.Local).AddTicks(9300),
                            Email = "user@user.com",
                            PasswordHash = new byte[] { 1, 2, 3, 4, 5 },
                            PasswordSalt = new byte[] { 1, 2, 3, 4, 5 },
                            Role = "user"
                        });
                });

            modelBuilder.Entity("Szakdolgozat.Shared.Meal", b =>
                {
                    b.HasOne("Szakdolgozat.Shared.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Szakdolgozat.Shared.MealVariant", b =>
                {
                    b.HasOne("Szakdolgozat.Shared.Meal", "Meal")
                        .WithMany("Variants")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Szakdolgozat.Shared.MealType", "MealType")
                        .WithMany()
                        .HasForeignKey("MealTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meal");

                    b.Navigation("MealType");
                });

            modelBuilder.Entity("Szakdolgozat.Shared.OrderItem", b =>
                {
                    b.HasOne("Szakdolgozat.Shared.Meal", "Meal")
                        .WithMany()
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Szakdolgozat.Shared.MealType", "MealType")
                        .WithMany()
                        .HasForeignKey("MealTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Szakdolgozat.Shared.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meal");

                    b.Navigation("MealType");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Szakdolgozat.Shared.Meal", b =>
                {
                    b.Navigation("Variants");
                });

            modelBuilder.Entity("Szakdolgozat.Shared.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
