using KitchenAid.Model.Helper;
using KitchenAid.Model.Inventory;
using KitchenAid.Model.Recipes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace KitchenAid.DataAccess
{
    public class InventoryContext : DbContext
    {
        // Inventory contexts
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<StorageProduct> StorageProducts { get; set; }
        public DbSet<PriceHistory> PriceHistories { get; set; }
        public DbSet<Category> Categories { get; set; }

        // Recipe contexts
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Instruction> Instructions { get; set; }


        public InventoryContext() { }
        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "TESTING.Database",
                IntegratedSecurity = true
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

            modelBuilder.Entity<Storage>().HasData(new Storage { StorageId = 1, CreatedOn = DateTime.Now, KindOfStorage = KindOfStorage.MainInventory });

            // salmon    
            modelBuilder.Entity<Product>().HasData(new Product { ProductId = 1, Name = "Norwegian salmon", Quantity = 2.3, QuantityUnit = "kg", CategoryId = 1, CurrentPrice = 245, StoredIn = KindOfStorage.Fridge});
            modelBuilder.Entity<PriceHistory>().HasData(new PriceHistory { PriceHistoryId = 1, ProductId = 1, Date = new DateTime(2021, 04, 24), Price = 170 });
            modelBuilder.Entity<PriceHistory>().HasData(new PriceHistory { PriceHistoryId = 2, ProductId = 1, Date = new DateTime(2021, 04, 18), Price = 172 });
            modelBuilder.Entity<PriceHistory>().HasData(new PriceHistory { PriceHistoryId = 3, ProductId = 1, Date = new DateTime(2021, 04, 10), Price = 179 });
            modelBuilder.Entity<PriceHistory>().HasData(new PriceHistory { PriceHistoryId = 4, ProductId = 1, Date = new DateTime(2021, 04, 02), Price = 123 });
        
            modelBuilder.Entity<StorageProduct>().HasData(new StorageProduct { StorageId = 1, ProductId = 1 });

            //
            //modelBuilder.Entity<Product>().HasData(new Product { ProductId = 1, Name = "Norwegian salmon", Quantity = 2.3, QuantityUnit = "kg", CategoryId = 1, CurrentPrice = 245, StoredIn = KindOfStorage.Fridge });
            //modelBuilder.Entity<PriceHistory>().HasData(new PriceHistory { PriceHistoryId = 1, ProductId = 1, Date = new DateTime(2021, 04, 24), Price = 170 });
            //modelBuilder.Entity<PriceHistory>().HasData(new PriceHistory { PriceHistoryId = 2, ProductId = 1, Date = new DateTime(2021, 04, 18), Price = 172 });
            //modelBuilder.Entity<PriceHistory>().HasData(new PriceHistory { PriceHistoryId = 3, ProductId = 1, Date = new DateTime(2021, 04, 10), Price = 179 });
            //modelBuilder.Entity<PriceHistory>().HasData(new PriceHistory { PriceHistoryId = 4, ProductId = 1, Date = new DateTime(2021, 04, 02), Price = 123 });

            //modelBuilder.Entity<StorageProduct>().HasData(new StorageProduct { StorageId = 1, ProductId = 1 });

        }
    }
}
