using KitchenAid.Model.Helper;
using KitchenAid.Model.Inventory;
using KitchenAid.Model.Recipes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace KitchenAid.DataAccess
{
    /// <summary>Sets the contexts for database access.</summary>
    public class InventoryContext : DbContext
    {
        // Inventory contexts
        /// <summary>Gets or sets the storages.</summary>
        /// <value>The storages.</value>
        public DbSet<Storage> Storages { get; set; }
        /// <summary>Gets or sets the products.</summary>
        /// <value>The products.</value>
        public DbSet<Product> Products { get; set; }

        /// <summary>Gets or sets the storage products.</summary>
        /// <value>The storage products.</value>
        public DbSet<StorageProduct> StorageProducts { get; set; }
        /// <summary>Gets or sets the categories.</summary>
        /// <value>The categories.</value>
        public DbSet<Category> Categories { get; set; }

        // Recipe contexts
        /// <summary>Gets or sets the recipes.</summary>
        /// <value>The recipes.</value>
        public DbSet<Recipe> Recipes { get; set; }
        /// <summary>Gets or sets the ingredients.</summary>
        /// <value>The ingredients.</value>
        public DbSet<Ingredient> Ingredients { get; set; }


        public InventoryContext() { }
        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options) { }

        #region Used only for creating the database
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                //DataSource = @"(localdb)\MSSQLLocalDB",
                //InitialCatalog = "TESTING.Database",
                //IntegratedSecurity = true
                DataSource = @"",
                InitialCatalog = "",
                UserID = "",
                Password = ""
            };

            optionsBuilder.UseSqlServer(builder.ConnectionString.ToString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<StorageProduct>()
                .HasKey(sp => new { sp.StorageId, sp.ProductId });
            modelBuilder.Entity<StorageProduct>()
                .HasOne(sp => sp.Storage)
                .WithMany(s => s.Products)
                .HasForeignKey(sp => sp.StorageId);
            modelBuilder.Entity<StorageProduct>()
                 .HasOne(sp => sp.Product)
                 .WithMany(p => p.Storages)
                 .HasForeignKey(sp => sp.ProductId);


            // Seeding mockup data to the database.

            // Categories
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 1, Name = "Fish", Description = "All kind of fish" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 2, Name = "Pork", Description = "Meat from pig" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 3, Name = "Ox", Description = "Meat from ox" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 4, Name = "Deer", Description = "Meat from deer" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 5, Name = "Elk", Description = "Meat from elk" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 6, Name = "Reindeer", Description = "Meat from reindeer" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 7, Name = "Dairy", Description = "Milk, yoghurt etc." });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 8, Name = "Pasta", Description = "All kind of pasta" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 9, Name = "Sweets", Description = "All sorts of sweets" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 10, Name = "Cleaning", Description = "Cleaning products" });

            // Main inventory
            modelBuilder.Entity<Storage>().HasData(new Storage { StorageId = 1, CreatedOn = DateTime.Now, KindOfStorage = KindOfStorage.MainInventory });

            // Salmon    
            modelBuilder.Entity<Product>().HasData(new Product { ProductId = 1, Name = "Norwegian salmon", Quantity = 2.3, QuantityUnit = "kg", CategoryId = 1, CurrentPrice = 245, StoredIn = KindOfStorage.Fridge });
            modelBuilder.Entity<StorageProduct>().HasData(new StorageProduct { StorageId = 1, ProductId = 1 });

            // Porkneck
            modelBuilder.Entity<Product>().HasData(new Product() { ProductId = 2, Name = "Pork Neck", Quantity = 4.5, QuantityUnit = "kg", CategoryId = 2, CurrentPrice = 49, StoredIn = KindOfStorage.Fridge });
            modelBuilder.Entity<StorageProduct>().HasData(new StorageProduct { StorageId = 1, ProductId = 2 });

            // Oxfillet
            modelBuilder.Entity<Product>().HasData(new Product() { ProductId = 3, Name = "Ox Filet", Quantity = 1.3, QuantityUnit = "kg", CategoryId = 3, CurrentPrice = 349, StoredIn = KindOfStorage.Fridge });
            modelBuilder.Entity<StorageProduct>().HasData(new StorageProduct { StorageId = 1, ProductId = 3 });

            // Elk ribs
            modelBuilder.Entity<Product>().HasData(new Product() { ProductId = 4, Name = "Elk ribs", Quantity = 2.5, QuantityUnit = "kg", CategoryId = 4, CurrentPrice = 549, StoredIn = KindOfStorage.Fridge });
            modelBuilder.Entity<StorageProduct>().HasData(new StorageProduct { StorageId = 1, ProductId = 4 });

            // Renskav
            modelBuilder.Entity<Product>().HasData(new Product() { ProductId = 5, Name = "Renskav", Quantity = 4.5, QuantityUnit = "kg", CategoryId = 6, CurrentPrice = 789, StoredIn = KindOfStorage.Freezer });
            modelBuilder.Entity<StorageProduct>().HasData(new StorageProduct { StorageId = 1, ProductId = 5 });

            // Milk
            modelBuilder.Entity<Product>().HasData(new Product() { ProductId = 6, Name = "Tine melk", Quantity = 3, QuantityUnit = "l", CategoryId = 7, CurrentPrice = 20, StoredIn = KindOfStorage.Fridge });
            modelBuilder.Entity<StorageProduct>().HasData(new StorageProduct { StorageId = 1, ProductId = 6 });

            // Tagliaelle
            modelBuilder.Entity<Product>().HasData(new Product() { ProductId = 7, Name = "Tagliatelle", Quantity = 500, QuantityUnit = "g", CategoryId = 8, CurrentPrice = 45, StoredIn = KindOfStorage.DryStorage });
            modelBuilder.Entity<StorageProduct>().HasData(new StorageProduct { StorageId = 1, ProductId = 7 });

            // Ahlgrens biler
            modelBuilder.Entity<Product>().HasData(new Product() { ProductId = 8, Name = "Algrens Biler", Quantity = 5, QuantityUnit = "piece", CategoryId = 9, CurrentPrice = 29, StoredIn = KindOfStorage.DryStorage });
            modelBuilder.Entity<StorageProduct>().HasData(new StorageProduct { StorageId = 1, ProductId = 8 });

            // Washing powder
            modelBuilder.Entity<Product>().HasData(new Product() { ProductId = 9, Name = "Via Color", Quantity = 4.3, QuantityUnit = "kg", CategoryId = 10, CurrentPrice = 199, StoredIn = KindOfStorage.DryStorage });
            modelBuilder.Entity<StorageProduct>().HasData(new StorageProduct { StorageId = 1, ProductId = 9 });
            #endregion

        }
    }
}
