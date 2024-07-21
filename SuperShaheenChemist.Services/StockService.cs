using SuperShaheenChemist.Database;
using SuperShaheenChemist.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace SuperShaheenChemist.Services
{
    public class StockService
    {
        #region Singleton
        public static StockService Instance
        {
            get
            {
                if (instance == null) instance = new StockService();
                return instance;
            }
        }
        private static StockService instance { get; set; }

        private StockService()
        {

        }
        #endregion
        public void AddStock(StockInventry stock)
        {
            using (var context = new CBContext())
            {
                if (context.StockInventries.Any(x => x.ProductId == stock.ProductId && x.BatchNo==stock.BatchNo))
                {
                    var data = context.StockInventries.Where(x => x.ProductId == stock.ProductId && x.BatchNo == stock.BatchNo).FirstOrDefault();
                    data.Stock = data.Stock + stock.Stock;
                    data.Received = (data.Received + stock.Received);
                    data.TotalAmount = (data.TotalAmount + stock.TotalAmount);
                    context.Entry(data).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    context.StockInventries.Add(stock);
                    context.SaveChanges();
                }
                
            }
        }
        public void ReturnStock(StockInventry stock)
        {
            using (var context = new CBContext())
            {
                if (context.StockInventries.Any(x => x.ProductId == stock.ProductId && x.BatchNo==stock.BatchNo && x.Stock>stock.Stock))
                {
                    var data = context.StockInventries.Where(x => x.ProductId == stock.ProductId && x.BatchNo==stock.BatchNo).FirstOrDefault();
                    data.Stock = (data.Stock - stock.Stock);
                    data.Return = (data.Return + stock.Return);
                    data.TotalAmount = (data.TotalAmount - stock.TotalAmount);
                    context.Entry(data).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }

        public void SaleProductsDeductedFromStock(StockInventry stock)
        {
            using (var context = new CBContext())
            {
                if (context.StockInventries.Any(x => x.ProductId == stock.ProductId && x.BatchNo == stock.BatchNo && x.Stock > stock.Stock))
                {
                    var data = context.StockInventries.Where(x => x.ProductId == stock.ProductId && x.BatchNo == stock.BatchNo).FirstOrDefault();
                    data.Stock = (data.Stock - stock.Stock);
                    data.TotalAmount = (data.TotalAmount - stock.TotalAmount);
                    data.Sale = (data.Sale + stock.Sale);
                    data.LooseSale = (data.LooseSale + stock.LooseSale);
                    //code to minus the pack if loose sale is equal to pack size
                    var temp = context.Products.Where(x => x.Id == stock.ProductId && x.BatchNo == stock.BatchNo).FirstOrDefault();
                    if(data.LooseSale>=temp.PackSize)
                    {
                        data.Stock = data.Stock - 1;
                        data.Sale = data.Sale + 1;
                        data.LooseSale = (int)(data.LooseSale - temp.PackSize);
                    }
                    //ends here
                    context.Entry(data).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }
        public List<StockInventry> GetExpiryStock()
        {
            List<StockInventry> stock = new List<StockInventry>();
            List<StockInventry> filterstock = new List<StockInventry>();
            using (var context=new CBContext())
            {
                stock=context.StockInventries.Include(x=>x.Product).ToList();
                foreach (var s in stock)
                {
                    if (DateTime.Now > s.Product.ExpiryDate)
                    {
                        filterstock.Add(s);
                    }
                }
            }
            return filterstock;
        }

        public StockInventry GetStockById(int productId)
        {
            using (var context=new CBContext())
            {
                return context.StockInventries.Where(x => x.ProductId == productId).FirstOrDefault();
            }
        }

        public void updateStock(StockInventry temp)
        {
            using (var context = new CBContext())
            {
                //code to minus the pack if loose sale is equal to pack size
                var productRecord = context.Products.Where(x => x.Id == temp.ProductId && x.BatchNo == temp.BatchNo).FirstOrDefault();
                if (temp.LooseSale >= productRecord.PackSize)
                {
                    temp.Stock = temp.Stock + 1;
                    temp.Sale = temp.Sale - 1;
                    temp.LooseSale = (int)(temp.LooseSale - productRecord.PackSize);
                }
                //ends here
                context.Entry(temp).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}

