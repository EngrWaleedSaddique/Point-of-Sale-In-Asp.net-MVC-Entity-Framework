using SuperShaheenChemist.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperShaheenChemist.Database
{
    public class CBContext : DbContext, IDisposable
    {
        public CBContext() : base("SuperShaheenChemist")
        {

        }
        public DbSet<Cashier> Cashiers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Config> Configs { get; set; }
        public DbSet<Distributor> Distributors { get; set; }
        public DbSet<MedicineType> Types { get; set; }
        public DbSet<PurchaseProducts> PurchaseProducts { get; set; }
        public DbSet<ReturnPurchase> ReturnPurchases { get; set; }
        public DbSet<SaleProducts> SaleProducts { get; set; }
        public DbSet<StockInventry> StockInventries { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderMaster> OrderMaster { get; set; }
        public DbSet<PurchaseProductsMaster> PurchaseProductMaster { get; set; }
        public DbSet<ReturnSales> ReturnSales { get; set; }

    }
}
