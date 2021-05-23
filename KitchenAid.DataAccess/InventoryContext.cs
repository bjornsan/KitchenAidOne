using KitchenAid.Model.Helper;
using KitchenAid.Model.Inventory;
using KitchenAid.Model.Recipes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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
        }
    }
}
