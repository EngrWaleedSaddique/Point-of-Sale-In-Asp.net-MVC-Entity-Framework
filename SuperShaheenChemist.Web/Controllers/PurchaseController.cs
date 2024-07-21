using Newtonsoft.Json;
using SuperShaheenChemist.Entities;
using SuperShaheenChemist.Services;
using SuperShaheenChemist.Web.Models;
using SuperShaheenChemist.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SuperShaheenChemist.Web.Controllers
{
    public class PurchaseController : Controller
    {
        // GET: Purchase
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SavePurchase(string purchaseItems)
        {

            
            var result = JsonConvert.DeserializeObject<List<PurchaseViewModel>>(purchaseItems);
            PurchaseProducts temp = new PurchaseProducts();
            StockInventry stock = new StockInventry();
            int transactionId = PurchaseService.Instance.PurchaseProductMasterMaxID() + 1;

            int totolAmount = 0;
            foreach (var pItems in result)
            {
                //Adding Product data in Purchase with DateWise
                
                
                Product p=ProductsService.Instance.GetProductInfo(pItems.ProductID);
                temp.ProductId = p.Id;
                temp.Qty = pItems.Quantity;
                temp.Date = DateTime.Now;
                temp.TotalAmount = pItems.TotalAmount;
                temp.TransactionId = transactionId;
                temp.BatchNo = pItems.BatchNo;
                ProductsService.Instance.PurchaseProduct(temp);
                totolAmount = totolAmount + pItems.TotalAmount;
                //Adding Products data in stock
                stock.ProductId = pItems.ProductID;
                stock.Price = ProductsService.Instance.ProductPrice(pItems.ProductID);
                stock.Received = stock.Received + pItems.Quantity;
                stock.Stock = pItems.Quantity;
                stock.BatchNo = pItems.BatchNo;
                stock.TotalAmount = stock.TotalAmount + pItems.TotalAmount;

                StockService.Instance.AddStock(stock);

            }
            PurchaseProductsMaster ob = new PurchaseProductsMaster();
            ob.Date = DateTime.Now;
            ob.CreatedBy = "Admin";
            ob.TotalAmount = totolAmount;
            ob.TransactionId = transactionId;
            PurchaseService.Instance.AddTransaction(ob);
            return Json("Success");

        }
        public JsonResult GetProductList(string productName)
        {
            IEnumerable<Product> products = new List<Product>();
            products = ProductsService.Instance.GetProducts();
            var p = products.Where(x => x.ProductName.ToLower().Contains(productName.ToLower())
             || x.BarCode.ToLower().Contains(productName.ToLower())).Select(x => new { productID = x.Id, ProductName = x.ProductName }).GroupBy(x=>x.ProductName).Select(x=>x.FirstOrDefault());
            return Json(p, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getProduct(int productId)
        {
            Product product = new Product();
            product = ProductsService.Instance.GetProductById(productId);
            return Json(product,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Purchases(string Datefrom,string Dateto)
        {
            if (!string.IsNullOrEmpty(Dateto) && !string.IsNullOrEmpty(Datefrom))
            {
                ViewBag.data = PurchaseService.Instance.GetAllPurchaseRecordsByDate(Datefrom,Dateto);
            }
            else
            {
                ViewBag.data = PurchaseService.Instance.GetAllPurchaseRecords();
            }
            return View();
        }
        public ActionResult GetPurchaseProductDetails(int id)
        {
            var products=PurchaseService.Instance.PurchaseProductsDetails(id);
            int totalAmount = 0;
            DateTime orderDate = new DateTime();
            foreach (var item in products)
            {
                totalAmount = (int)(totalAmount + item.TotalAmount);
                orderDate = item.Date;
            }
            ViewBag.totalAmount = totalAmount;
            ViewBag.data = products;
            ViewBag.OrderDate = orderDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
            return View();
        }
        public JsonResult GetData(JqueryDatatableParam param,string fromDate, string toDate)
        {

            var products = ProductsService.Instance.PurchaseSearchByDate(fromDate, toDate);

            //Pagination
            var displayResult = products.Skip(param.iDisplayStart)
               .Take(param.iDisplayLength).ToList();


            List<PurchaseTableViewModel> p = new List<PurchaseTableViewModel>();
            PurchaseTableViewModel model = new PurchaseTableViewModel();
            foreach(var c in displayResult)
            {
                model.ProductName = c.Product.ProductName;
                model.GenericName = c.Product.GenericName;
                model.Qty = c.Qty;
                model.TotalAmount = c.TotalAmount;
                model.Date = c.Date.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                p.Add(model);
                model = new PurchaseTableViewModel();
            }
            var totalRecords = products.Count();
            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = p
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ReturnPurchase()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ReturnPurchase(string purchaseItems)
        {
            var result = JsonConvert.DeserializeObject<List<ReturnViewModel>>(purchaseItems);
            ReturnPurchase temp = new ReturnPurchase();
            StockInventry stock = new StockInventry();
            int errorCount = 0;

            foreach (var pItems in result)
            {
                //Adding Product data in Purchase with DateWise
                Product p = ProductsService.Instance.GetProductInfo(pItems.ProductID);
                temp.ProductId = p.Id;
                temp.Qty = pItems.Quantity;
                temp.Date = DateTime.Now;
                temp.BatchNo = pItems.BatchNo;
                temp.TotalAmount = pItems.TotalAmount;
                ProductsService.Instance.ReturnPurchase(temp);
                if(ProductsService.Instance.CheckStockForProductReturn(temp))
                {
                    //Retruning Products from Stock
                    stock.ProductId = pItems.ProductID;
                    stock.Price = ProductsService.Instance.ProductPrice(pItems.ProductID);
                    stock.Stock = pItems.Quantity;
                    stock.BatchNo = pItems.BatchNo;
                    stock.TotalAmount = pItems.TotalAmount;
                    stock.Return = pItems.Quantity;
                    StockService.Instance.ReturnStock(stock);
                }
                else
                {
                    errorCount++;
                }
               
            }
            return Json(errorCount);
        }
        public ActionResult PurchaseOrder(string FromDate,string ToDate)
        {
            
            if (!string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(ToDate))
            {
                return View(PurchaseService.Instance.SearchByDate(FromDate, ToDate));
                //ViewBag.data = PurchaseService.Instance.SearchByDate(FromDate, ToDate);
            }
            else
            {
                //ViewBag.data = PurchaseService.Instance.GetAllPurchaseOrders();
                return View(PurchaseService.Instance.GetAllPurchaseOrders());
            }
            
        }
        
        public ActionResult AddPurchaseOrder()
        {
            return View();
        }
        public ActionResult ViewPurchaseOrderDetails(int id)
        {
            List<PurchaseOrder> products = new List<PurchaseOrder>();
            products=PurchaseService.Instance.PurchaseOrderDetials(id);
            int totalAmount = 0;
            DateTime orderDate = new DateTime();
            foreach(var item in products)
            {
                totalAmount = (int)(totalAmount + item.Amount);
                orderDate = item.Date;
            }
            ViewBag.totalAmount = totalAmount;
            ViewBag.data = products;
            ViewBag.OrderDate = orderDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
            return View();
        }
        [HttpPost]
        public ActionResult AddItemsToOrder()
        {
            return Json("Success");
        }

        [HttpPost]
        public ActionResult SavePurchaseOrder(string purchaseItems,string total)
        {
            var result = JsonConvert.DeserializeObject<List<PurchaseViewModel>>(purchaseItems);
            PurchaseOrder temp = new PurchaseOrder();
            //StockInventry stock = new StockInventry();
            int ordernumber = PurchaseService.Instance.PurchaseOrder()+1;
            foreach (var pItems in result)
            {
                //Adding Product data in Purchase with DateWise
                temp.ProductId = pItems.ProductID;
                temp.Quantity = pItems.Quantity;
                temp.Date = DateTime.Now;
                temp.Amount = pItems.TotalAmount;
                temp.CreatedBy = "Admin";
                temp.PurchaseOrderNo = ordernumber;
                PurchaseService.Instance.AddPurchaseOrder(temp);
            }
            PurchaseOrderMaster obj = new PurchaseOrderMaster();
            obj.Date = DateTime.Now;
            obj.CreatedBy = "Admin";
            obj.OrderNo = ordernumber;
            obj.totalAmount = int.Parse(total);
            PurchaseService.Instance.AddIntoMasterOrder(obj);
            return Json("Success");
        }
        public ActionResult GetPackAndUnitCost(int id)
        {
            var products=ProductsService.Instance.GetProducts();
            var ob = products.Where(x => x.Id==id).Select(x => new { x.UnitRetail, x.PackRetailCost }).FirstOrDefault();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ReturnItems(string ReturnItems)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            ReturnItemsViewModel returnObject = js.Deserialize<ReturnItemsViewModel>(ReturnItems);
            var temp = StockService.Instance.GetStockById(returnObject.ProductId);
            if (returnObject.Quantity != 0 && returnObject.Loose==0 && returnObject.Discount==0)
            {
                //done
                var ob = SaleService.Instance.GetOrderItem(returnObject.RecordID);
                if (returnObject.Quantity==ob.Quantity && returnObject.Loose == ob.Loose)
                {
                    PurchaseService.Instance.DeleteReturnItem(returnObject.RecordID);
                    temp.Stock = temp.Stock + returnObject.Quantity;
                    temp.Sale = temp.Sale - returnObject.Quantity;
                    temp.TotalAmount = (int)(temp.TotalAmount + returnObject.ReturnAmount);
                    StockService.Instance.updateStock(temp);
                }
                else
                {
                    ob.Quantity = (ob.Quantity-returnObject.Quantity);
                    ob.Amount = (ob.Amount - returnObject.ReturnAmount);
                    PurchaseService.Instance.UpdateReturnItem(ob);
                    temp.Stock = temp.Stock + returnObject.Quantity;
                    temp.Sale = temp.Sale - returnObject.Quantity;
                    temp.TotalAmount = (int)(temp.TotalAmount + returnObject.ReturnAmount);
                    StockService.Instance.updateStock(temp);
                }
            }
            if (returnObject.Quantity != 0 && returnObject.Loose != 0 && returnObject.Discount != 0)
            {
                //done
                var ob = SaleService.Instance.GetOrderItem(returnObject.RecordID);
                if (returnObject.Quantity == ob.Quantity && returnObject.Loose==ob.Loose)
                {
                    PurchaseService.Instance.DeleteReturnItem(returnObject.RecordID);
                    temp.Stock = temp.Stock + returnObject.Quantity;
                    temp.Sale = temp.Sale - returnObject.Quantity;
                    temp.LooseSale = temp.LooseSale - returnObject.Loose;
                    temp.TotalAmount = (int)(temp.TotalAmount + returnObject.ReturnAmount);
                    StockService.Instance.updateStock(temp);

                }
                else
                {
                    ob.Quantity = (ob.Quantity - returnObject.Quantity);
                    ob.Amount = (ob.Amount - returnObject.ReturnAmount);
                    ob.Loose = (ob.Loose - returnObject.Loose);
                    PurchaseService.Instance.UpdateReturnItem(ob);
                    temp.Stock = temp.Stock + returnObject.Quantity;
                    temp.Sale = temp.Sale - returnObject.Quantity;
                    temp.LooseSale = temp.LooseSale - returnObject.Loose;
                    temp.TotalAmount = (int)(temp.TotalAmount + returnObject.ReturnAmount);
                    StockService.Instance.updateStock(temp);
                }
            }
            if (returnObject.Quantity != 0 && returnObject.Loose != 0 && returnObject.Discount == 0)
            {
                //done
                var ob = SaleService.Instance.GetOrderItem(returnObject.RecordID);
                if (returnObject.Quantity == ob.Quantity && returnObject.Loose == ob.Loose)
                {
                    PurchaseService.Instance.DeleteReturnItem(returnObject.RecordID);
                    temp.Stock = temp.Stock + returnObject.Quantity;
                    temp.Sale = temp.Sale - returnObject.Quantity;
                    temp.LooseSale = temp.LooseSale - returnObject.Loose;
                    temp.TotalAmount = (int)(temp.TotalAmount + returnObject.ReturnAmount);
                    StockService.Instance.updateStock(temp);
                }
                else
                {
                    ob.Quantity = (ob.Quantity - returnObject.Quantity);
                    ob.Amount = (ob.Amount - returnObject.ReturnAmount);
                    ob.Loose = (ob.Loose - returnObject.Loose);
                    PurchaseService.Instance.UpdateReturnItem(ob);
                    temp.Stock = temp.Stock + returnObject.Quantity;
                    temp.Sale = temp.Sale - returnObject.Quantity;
                    temp.LooseSale = temp.LooseSale - returnObject.Loose;
                    temp.TotalAmount = (int)(temp.TotalAmount + returnObject.ReturnAmount);
                    StockService.Instance.updateStock(temp);
                }
            }
            if (returnObject.Quantity == 0 && returnObject.Loose != 0 && returnObject.Discount != 0)
            {
                //done
                var ob = SaleService.Instance.GetOrderItem(returnObject.RecordID);
                if (returnObject.Quantity == ob.Quantity && returnObject.Loose == ob.Loose)
                {
                    PurchaseService.Instance.DeleteReturnItem(returnObject.RecordID);
                    temp.LooseSale = temp.LooseSale - returnObject.Loose;
                    temp.TotalAmount = (int)(temp.TotalAmount + returnObject.ReturnAmount);
                    StockService.Instance.updateStock(temp);
                }
                else
                {
                    ob.Amount = (ob.Amount - returnObject.ReturnAmount);
                    ob.Loose = (ob.Loose - returnObject.Loose);
                    PurchaseService.Instance.UpdateReturnItem(ob);
                    temp.LooseSale = temp.LooseSale - returnObject.Loose;
                    temp.TotalAmount = (int)(temp.TotalAmount + returnObject.ReturnAmount);
                    StockService.Instance.updateStock(temp);

                }
            }
            if (returnObject.Quantity == 0 && returnObject.Loose != 0 && returnObject.Discount == 0)
            {
                //done
                var ob = SaleService.Instance.GetOrderItem(returnObject.RecordID);
                if (returnObject.Quantity == ob.Quantity && returnObject.Loose == ob.Loose)
                {
                    PurchaseService.Instance.DeleteReturnItem(returnObject.RecordID);
                    temp.LooseSale = temp.LooseSale - returnObject.Loose;
                    temp.TotalAmount = (int)(temp.TotalAmount + returnObject.ReturnAmount);
                    StockService.Instance.updateStock(temp);
                }
                else
                {
                    ob.Loose = (ob.Loose - returnObject.Loose);
                    ob.Amount = (ob.Amount - returnObject.ReturnAmount);
                    PurchaseService.Instance.UpdateReturnItem(ob);
                    temp.LooseSale = temp.LooseSale - returnObject.Loose;
                    temp.TotalAmount = (int)(temp.TotalAmount + returnObject.ReturnAmount);
                    StockService.Instance.updateStock(temp);
                }
            }
            if (returnObject.Quantity != 0 && returnObject.Loose == 0 && returnObject.Discount != 0)
            {
                //done
                var ob = SaleService.Instance.GetOrderItem(returnObject.RecordID);
                if (returnObject.Quantity == ob.Quantity && returnObject.Loose == ob.Loose)
                {
                    PurchaseService.Instance.DeleteReturnItem(returnObject.RecordID);
                    temp.Stock = temp.Stock + returnObject.Quantity;
                    temp.Sale = temp.Sale - returnObject.Quantity;
                    temp.TotalAmount = (int)(temp.TotalAmount + returnObject.ReturnAmount);
                    StockService.Instance.updateStock(temp);
                }
                else
                {
                    ob.Quantity = (ob.Quantity - returnObject.Quantity);
                    ob.Amount = (ob.Amount - returnObject.ReturnAmount);
                    PurchaseService.Instance.UpdateReturnItem(ob);
                    temp.Stock = temp.Stock + returnObject.Quantity;
                    temp.Sale = temp.Sale - returnObject.Quantity;
                    temp.TotalAmount = (int)(temp.TotalAmount + returnObject.ReturnAmount);
                    StockService.Instance.updateStock(temp);
                }
            }
            return Json("Success");
        }

    }
}
