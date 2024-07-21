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
    public class PurchaseService
    {
        #region Singleton
        public static PurchaseService Instance
        {
            get
            {
                if (instance == null) instance = new PurchaseService();
                return instance;
            }
        }
        private static PurchaseService instance { get; set; }

        private PurchaseService()
        {

        }
        #endregion

        public void AddPurchaseOrder(PurchaseOrder product)
        {   
            using (var context = new CBContext())
            {
                context.PurchaseOrders.Add(product);
                context.SaveChanges();
            }
        }
        public List<PurchaseProductsMaster> GetAllPurchaseRecords()
        {
            using (var context=new CBContext())
            {
                return context.PurchaseProductMaster.ToList();
            }
        }
        public List<PurchaseOrder> PurchaseOrderDetials(int id)
        {
            using (var context=new CBContext())
            {
                return context.PurchaseOrders.Where(x => x.PurchaseOrderNo == id).Include(x=>x.Product).ToList();
            }
        }
        public int PurchaseOrder()
        {
            using (var context = new CBContext())
            {
                return context.PurchaseOrders.Max(x =>(int?) x.PurchaseOrderNo)??0;
                
            }
        }
        public int PurchaseProductMasterMaxID()
        {
            using (var context = new CBContext())
            {
                return context.PurchaseProductMaster.Max(x => (int?)x.TransactionId) ?? 0;
            }
        }
        public int OrdersMaxID()
        {
            using (var context = new CBContext())
            {
                return context.Orders.Max(x => (int?)x.Id) ?? 0;
            }
        }
        public void AddIntoMasterOrder(PurchaseOrderMaster ob)
        {
            using (var context = new CBContext())
            {
                context.OrderMaster.Add(ob);
                context.SaveChanges();
            }
        }
        public List<PurchaseOrderMaster> GetAllPurchaseOrders()
        {
            using (var context =new CBContext())
            {
                return context.OrderMaster.ToList();
            }
        }
        public List<PurchaseOrderMaster> SearchByDate(string FromDate, string ToDate)
        {
            DateTime fDate = Convert.ToDateTime(FromDate);
            DateTime tDate = Convert.ToDateTime(ToDate).AddDays(1);
            var purchases = new List<PurchaseOrderMaster>();
            using (var context = new CBContext())
            {
                purchases = context.OrderMaster.Where(x => x.Date >= fDate && x.Date <= tDate).ToList();
                return purchases;
            }
        }


        public void AddTransaction(PurchaseProductsMaster ob)
        {
            using (var context=new CBContext())
            {
                context.PurchaseProductMaster.Add(ob);
                context.SaveChanges();
            }
        }

        public List<PurchaseProducts> PurchaseProductsDetails(int id)
        {
            using (var context=new CBContext())
            {
                return context.PurchaseProducts.Where(x => x.TransactionId == id).Include(x=>x.Product).ToList();
            }
        }

        public List<PurchaseProductsMaster> GetAllPurchaseRecordsByDate(string datefrom, string dateto)
        {
            DateTime fDate = Convert.ToDateTime(datefrom);
            DateTime tDate = Convert.ToDateTime(dateto).AddDays(1);
            using (var context=new CBContext())
            {
                return context.PurchaseProductMaster.Where(x => x.Date >= fDate && x.Date <= tDate).ToList();
            }
        }
        public List<OrderItem> GetOrderDetials(int id)
        {
            using (var context = new CBContext())
            {
                 return context.OrderItems.Where(x => x.OrderID == id).Include(x=>x.Product).ToList();
            }
        }

        public void AddMedicineType(string type)
        {
            MedicineType ob = new MedicineType();
            ob.Name = type;

            using (var context = new CBContext())
            {
                context.Types.Add(ob);
                context.SaveChanges();
            }
        }

        public void SaveCompany(string company)
        {
            Company ob = new Company();
            ob.Name = company;

            using (var context = new CBContext())
            {
                context.Companies.Add(ob);
                context.SaveChanges();
            }
        }
        public void SaveDistributor(string distributor)
        {
            Distributor ob = new Distributor();
            ob.Name = distributor;

            using (var context = new CBContext())
            {
                context.Distributors.Add(ob);
                context.SaveChanges();
            }
        }

        public void SaveCategory(string category)
        {
            Category ob = new Category();
            ob.Name = category;

            using (var context = new CBContext())
            {
                context.Categories.Add(ob);
                context.SaveChanges();
            }
        }

        public void DeleteReturnItem(int recordID)
        {
            using (var context=new CBContext())
            {
                var product = context.OrderItems.Find(recordID);
                context.OrderItems.Remove(product);
                context.SaveChanges();
            }
        }
        public void UpdateReturnItem(OrderItem item)
        {
            using (var context = new CBContext())
            {
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();

            }
        }

        public string GetCustomerName(int id)
        {
            using (var context =new CBContext())
            {
                return context.Orders.Where(x => x.Id == id).Select(x => x.CustomerName).FirstOrDefault();
            }
        }
    }
}
