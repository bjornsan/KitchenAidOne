﻿// <auto-generated />
using System;
using KitchenAid.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KitchenAid.DataAccess.Migrations
{
    [DbContext(typeof(InventoryContext))]
    partial class InventoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KitchenAid.Model.Helper.StorageProduct", b =>
                {
                    b.Property<int>("StorageId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("StorageId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("StorageProducts");

                    b.HasData(
                        new
                        {
                            StorageId = 1,
                            ProductId = 1
                        },
                        new
                        {
                            StorageId = 1,
                            ProductId = 2
                        },
                        new
                        {
                            StorageId = 1,
                            ProductId = 3
                        },
                        new
                        {
                            StorageId = 1,
                            ProductId = 4
                        },
                        new
                        {
                            StorageId = 1,
                            ProductId = 5
                        },
                        new
                        {
                            StorageId = 1,
                            ProductId = 6
                        },
                        new
                        {
                            StorageId = 1,
                            ProductId = 7
                        },
                        new
                        {
                            StorageId = 1,
                            ProductId = 8
                        },
                        new
                        {
                            StorageId = 1,
                            ProductId = 9
                        });
                });

            modelBuilder.Entity("KitchenAid.Model.Inventory.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            Description = "All kind of fish",
                            Name = "Fish"
                        },
                        new
                        {
                            CategoryId = 2,
                            Description = "Meat from pig",
                            Name = "Pork"
                        },
                        new
                        {
                            CategoryId = 3,
                            Description = "Meat from ox",
                            Name = "Ox"
                        },
                        new
                        {
                            CategoryId = 4,
                            Description = "Meat from deer",
                            Name = "Deer"
                        },
                        new
                        {
                            CategoryId = 5,
                            Description = "Meat from elk",
                            Name = "Elk"
                        },
                        new
                        {
                            CategoryId = 6,
                            Description = "Meat from reindeer",
                            Name = "Reindeer"
                        },
                        new
                        {
                            CategoryId = 7,
                            Description = "Milk, yoghurt etc.",
                            Name = "Dairy"
                        },
                        new
                        {
                            CategoryId = 8,
                            Description = "All kind of pasta",
                            Name = "Pasta"
                        },
                        new
                        {
                            CategoryId = 9,
                            Description = "All sorts of sweets",
                            Name = "Sweets"
                        },
                        new
                        {
                            CategoryId = 10,
                            Description = "Cleaning products",
                            Name = "Cleaning"
                        });
                });

            modelBuilder.Entity("KitchenAid.Model.Inventory.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<double>("CurrentPrice")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<string>("QuantityUnit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StoredIn")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryId = 1,
                            CurrentPrice = 245.0,
                            Name = "Norwegian salmon",
                            Quantity = 2.2999999999999998,
                            QuantityUnit = "kg",
                            StoredIn = 1
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 2,
                            CurrentPrice = 49.0,
                            Name = "Pork Neck",
                            Quantity = 4.5,
                            QuantityUnit = "kg",
                            StoredIn = 1
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryId = 3,
                            CurrentPrice = 349.0,
                            Name = "Ox Filet",
                            Quantity = 1.3,
                            QuantityUnit = "kg",
                            StoredIn = 1
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryId = 4,
                            CurrentPrice = 549.0,
                            Name = "Elk ribs",
                            Quantity = 2.5,
                            QuantityUnit = "kg",
                            StoredIn = 1
                        },
                        new
                        {
                            ProductId = 5,
                            CategoryId = 6,
                            CurrentPrice = 789.0,
                            Name = "Renskav",
                            Quantity = 4.5,
                            QuantityUnit = "kg",
                            StoredIn = 2
                        },
                        new
                        {
                            ProductId = 6,
                            CategoryId = 7,
                            CurrentPrice = 20.0,
                            Name = "Tine melk",
                            Quantity = 3.0,
                            QuantityUnit = "l",
                            StoredIn = 1
                        },
                        new
                        {
                            ProductId = 7,
                            CategoryId = 8,
                            CurrentPrice = 45.0,
                            Name = "Tagliatelle",
                            Quantity = 500.0,
                            QuantityUnit = "g",
                            StoredIn = 3
                        },
                        new
                        {
                            ProductId = 8,
                            CategoryId = 9,
                            CurrentPrice = 29.0,
                            Name = "Algrens Biler",
                            Quantity = 5.0,
                            QuantityUnit = "piece",
                            StoredIn = 3
                        },
                        new
                        {
                            ProductId = 9,
                            CategoryId = 10,
                            CurrentPrice = 199.0,
                            Name = "Via Color",
                            Quantity = 4.2999999999999998,
                            QuantityUnit = "kg",
                            StoredIn = 3
                        });
                });

            modelBuilder.Entity("KitchenAid.Model.Inventory.Storage", b =>
                {
                    b.Property<int>("StorageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("KindOfStorage")
                        .HasColumnType("int");

                    b.HasKey("StorageId");

                    b.ToTable("Storages");

                    b.HasData(
                        new
                        {
                            StorageId = 1,
                            CreatedOn = new DateTime(2021, 5, 24, 16, 25, 2, 310, DateTimeKind.Local).AddTicks(8649),
                            KindOfStorage = 0
                        });
                });

            modelBuilder.Entity("KitchenAid.Model.Recipes.Ingredient", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RecipeId")
                        .HasColumnType("int");

                    b.Property<string>("Unit")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IngredientId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("KitchenAid.Model.Recipes.Instruction", b =>
                {
                    b.Property<int>("InstructionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Instructions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReadyInMinutes")
                        .HasColumnType("int");

                    b.Property<int?>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("Servings")
                        .HasColumnType("int");

                    b.HasKey("InstructionId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Instructions");
                });

            modelBuilder.Entity("KitchenAid.Model.Recipes.Recipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RecipeId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("KitchenAid.Model.Helper.StorageProduct", b =>
                {
                    b.HasOne("KitchenAid.Model.Inventory.Product", "Product")
                        .WithMany("Storages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KitchenAid.Model.Inventory.Storage", "Storage")
                        .WithMany("Products")
                        .HasForeignKey("StorageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KitchenAid.Model.Inventory.Product", b =>
                {
                    b.HasOne("KitchenAid.Model.Inventory.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KitchenAid.Model.Recipes.Ingredient", b =>
                {
                    b.HasOne("KitchenAid.Model.Recipes.Recipe", "Recipe")
                        .WithMany()
                        .HasForeignKey("RecipeId");
                });

            modelBuilder.Entity("KitchenAid.Model.Recipes.Instruction", b =>
                {
                    b.HasOne("KitchenAid.Model.Recipes.Recipe", "Recipe")
                        .WithMany()
                        .HasForeignKey("RecipeId");
                });
#pragma warning restore 612, 618
        }
    }
}
