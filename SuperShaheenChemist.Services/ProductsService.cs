using SuperShaheenChemist.Database;
using SuperShaheenChemist.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperShaheenChemist.Services
{
    public class ProductsService
    {
        #region Singleton
        public static ProductsService Instance
        {
            get
            {
                if (instance == null) instance = new ProductsService();
                return instance;
            }
        }

        

        private static ProductsService instance { get; set; }

        private ProductsService()
        {

        }
        #endregion
        public void SaveProduct(Product product)
        {
            using (var context = new CBContext())
            {
                context.Products.Add(product);
                context.SaveChanges();
            }
        }
        public List<Product> GetProducts()
        {
            
            using(var context=new CBContext())
            {
                return context.Products.ToList();
            }
        }
        public Product GetProductById(int id)
        {

            using (var context = new CBContext())
            {
                return context.Products.Where(product => product.Id == id).FirstOrDefault();
            }
        }

        public List<Product> GetProductsNameBarcode(string productName)
        {
            using (var context = new CBContext())
            {

                return context.Products.ToList();
            }
        }
        public void DeleteProduct(int ID)
        {
            using (var context = new CBContext())
            {
                //context.Entry(category).State = System.Data.Entity.EntityState.Deleted;
                var product = context.Products.Find(ID);
                context.Products.Remove(product);
                context.SaveChanges();
            }
        }
        public decimal ProductPrice(int ID)
        {
            using (var context = new CBContext())
            {
                Product product=context.Products.Find(ID);
                return product.PackCost;
            }
        }

        public Product GetProduct(int ID)
        {
            using (var context = new CBContext())
            {
                return context.Products.Where(product => product.Id == ID).FirstOrDefault();
            }
        }
        public void UpdateProduct(Product product)
        {
            using (var context = new CBContext())
            {
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void PurchaseProduct(PurchaseProducts product)
        {
            using (var context = new CBContext())
            {
                //if (context.PurchaseProducts.Any(x => x.ProductId == product.ProductId))
                //{
                //    var purchaseRecord= context.PurchaseProducts.Where(x => x.ProductId == product.ProductId).FirstOrDefault();
                //    purchaseRecord.Qty = purchaseRecord.Qty+product.Qty;
                //    context.PurchaseProducts.Attach(purchaseRecord);
                //    context.Entry(purchaseRecord).Property(x => x.Qty).IsModified = true;
                //    context.SaveChanges();
                //}
                context.PurchaseProducts.Add(product);
                context.SaveChanges();
                
            }
        }
        public void ReturnPurchase(ReturnPurchase product)
        {
            using (var context = new CBContext())
            {
                if (context.StockInventries.Any(x => x.ProductId == product.ProductId && x.BatchNo==product.BatchNo && x.Stock > product.Qty)) 
                {
                    context.ReturnPurchases.Add(product);
                    context.SaveChanges();
                }
            }
        }
        public bool CheckStockForProductReturn(ReturnPurchase product)
        {
                using (var context = new CBContext())
                if (context.StockInventries.Any(x => x.ProductId == product.ProductId && x.BatchNo==product.BatchNo && x.Stock > product.Qty))
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }
        public List<PurchaseProducts> PurchaseSearchByDate(string fromDate,string toDate)
        {
            DateTime fDate = Convert.ToDateTime(fromDate);
            DateTime tDate = Convert.ToDateTime(toDate).AddDays(1);
            var purchases = new List<PurchaseProducts>();
            using (var context=new CBContext())
            {
                purchases = context.PurchaseProducts.Where(x => x.Date >= fDate && x.Date <= tDate).Include(x=>x.Product).ToList();
            }
            return purchases;
        }
        public Product GetProductInfo(int ID)
        {
            using (var context = new CBContext())
            {

                return context.Products.Find(ID);

            }
        }

        public List<StockInventry> OutOfStockProducts()
        {
            List<StockInventry> stock = new List<StockInventry>();
            List<StockInventry> filterstock = new List<StockInventry>();
            using (var context=new CBContext())
            {
              stock=context.StockInventries.Include(x => x.Product).ToList();
              foreach(var s in stock)
                {
                    if(s.Stock<s.Product.MinStock)
                    {
                        filterstock.Add(s);
                    }
                }
            }
            return filterstock;
        }
        public List<StockInventry> getStock()
        {
            using(var context=new CBContext())
            {
                return context.StockInventries.Include(x=>x.Product).ToList();
            }
        }
        public int CashTotalToday()
        {
            int total = 0;
            using (var context = new CBContext())
            {
                var date = DateTime.Now.ToString("dd/MM/yyyy");
                var temp = context.Orders.ToList();
                foreach (var item in temp)
                {
                    var date2 = item.Date.ToString("dd/MM/yyyy");
                    if (date2 == date)
                    {
                        total = (int)(total + item.TotalAmount);
                    }
                }
            }
            return total;
        }

        public int SaleToday()
        {
            int total = 0;
            using (var context = new CBContext())
            {
                var date = DateTime.Now.ToString("dd/MM/yyyy");
                var temp = context.Orders.ToList();
                foreach (var item in temp)
                {
                    var date2 = item.Date.ToString("dd/MM/yyyy");
                    if (date2 == date)
                    {
                        total = total + 1;
                    }
                }
            }
            return total;
        }
        public int MonthTotalSales()
        {
            int total = 0;
            using (var context = new CBContext())
            {
                var date = DateTime.Now.Month;
                var temp = context.Orders.ToList();
                foreach (var item in temp)
                {
                    var date2 = item.Date.Month;
                    if (date2 == date)
                    {
                        total = total + 1;
                    }
                }
            }
            return total;
        }

    }
}
