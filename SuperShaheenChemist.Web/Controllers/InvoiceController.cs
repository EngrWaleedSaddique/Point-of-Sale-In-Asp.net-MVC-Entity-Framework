using Newtonsoft.Json;
using PrinterUtility;
using SuperShaheenChemist.Entities;
using SuperShaheenChemist.Services;
using SuperShaheenChemist.Web.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ESC_POS_USB_NET.Printer;
using ESC_POS_USB_NET.Enums;

namespace SuperShaheenChemist.Web.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: Invoice
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateInvoice()
        {
            int billno = PurchaseService.Instance.OrdersMaxID() + 1;
            ViewBag.billno = billno;
            return View();
        }
        public ActionResult ProductsWithBatchNo(string Pname)
        {
            var products = ProductsService.Instance.GetProducts();

            var ob = products.Where(x => x.ProductName.Trim().ToLower() == Pname.Trim().ToLower()).Select(x => new { x.BatchNo, x.Id }).ToList();

            return Json(ob, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetExpiry(string batchNo)
        {
            var products = ProductsService.Instance.GetProducts();
            var ob = products.Where(x => x.BatchNo.Trim().ToLower() == batchNo.Trim().ToLower()).Select(x => new { x.ExpiryDate, x.PackRetailCost, x.Id }).FirstOrDefault();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUnitPrice(string BatchNo)
        {
            var products = ProductsService.Instance.GetProducts();
            var ob = products.Where(x => x.BatchNo.Trim().ToLower() == BatchNo.Trim().ToLower()).Select(x => x.UnitRetail).FirstOrDefault();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveSaleProducts(string saleItems, string CustomerName)
        {

            var result = JsonConvert.DeserializeObject<List<SaleViewModel>>(saleItems);
            OrderItem temp = new OrderItem();
            Order order = new Order();
            decimal total = 0;
            foreach (var item in result)
            {
                total = total + Convert.ToDecimal(item.Amount);
            }
            order.TotalAmount = total;
            order.Date = DateTime.Now;
            order.CustomerName = CustomerName;
            SaleService.Instance.SaveOrder(order);
            foreach (var item in result)
            {
                temp.OrderID = SaleService.Instance.OrderMaxID();
                temp.ProductID = item.ProductID;
                temp.BatchNo = item.BatchNo;
                try
                {
                    if (item.Quantity != "")
                    {
                        temp.Quantity = int.Parse(item.Quantity);
                    }
                }
                catch (Exception)
                {
                    temp.Quantity = 0;
                }
                try
                {
                    if (item.Loose != "")
                    {
                        temp.Loose = int.Parse(item.Loose);
                    }
                }
                catch (Exception)
                {
                    temp.Loose = 0;
                }

                temp.Price = Convert.ToDecimal(item.Price);
                temp.ExpiryDate = DateTime.Parse(item.ExpiryDate);
                try
                {
                    if (item.Discount != "")
                    {
                        temp.Discount = int.Parse(item.Discount);
                    }
                }
                catch (Exception)
                {
                    temp.Discount = 0;
                }
                temp.Amount = Convert.ToDecimal(item.Amount);
                SaleService.Instance.SaveSale(temp);
            }

            StockInventry stock = new StockInventry();
            //Minus the product from stock which are saled.
            foreach (var item in result)
            {
                stock.ProductId = item.ProductID;
                stock.BatchNo = item.BatchNo;
                try
                {
                    if (item.Amount != "")
                    {
                        //stock.TotalAmount = int.Parse(item.Amount);
                        stock.TotalAmount = (int)Convert.ToInt64(Math.Floor(Convert.ToDouble(item.Amount)));
                    }
                }
                catch (Exception)
                {
                    stock.TotalAmount = 0;
                }

                try
                {
                    if (item.Quantity != "")
                    {
                        stock.Stock = int.Parse(item.Quantity);
                        stock.Sale = int.Parse(item.Quantity);
                    }
                }
                catch (Exception)
                {
                    stock.Stock = 0;
                }

                try
                {
                    if (item.Loose != "")
                    {
                        stock.LooseSale = int.Parse(item.Loose);
                    }
                }
                catch (Exception)
                {
                    stock.LooseSale = 0;
                }



                StockService.Instance.SaleProductsDeductedFromStock(stock);
            }
            int orderId = PurchaseService.Instance.OrdersMaxID();
            return Json(orderId);

        }
        [HttpGet]
        public ActionResult PreviewReceipt(int id)
        {
            List<OrderItem> data = new List<OrderItem>();
            data = PurchaseService.Instance.GetOrderDetials(id);
            decimal total = 0;
            int numberOfItems = 0;
            string custName = PurchaseService.Instance.GetCustomerName(id);
            foreach (var item in data)
            {
                total = total + item.Amount;
                numberOfItems++;
            }
            ViewBag.total = total;
            ViewBag.numberofitems = numberOfItems;
            ViewBag.CustomerName = custName;
            ViewBag.BillNo = "";
            ViewBag.data = data;
            ViewBag.orderNo = id;
            return View();
        }
        public ActionResult ViewReceipt(int id)
        {
            List<OrderItem> data = new List<OrderItem>();
            data = PurchaseService.Instance.GetOrderDetials(id);
            decimal total = 0;
            int numberOfItems = 0;

            string custName = PurchaseService.Instance.GetCustomerName(id);

            foreach (var item in data)
            {
                total = total + item.Amount;
                numberOfItems++;
            }
            ViewBag.total = total;
            ViewBag.CustomerName = custName;
            ViewBag.numberofitems = numberOfItems;
            ViewBag.BillNo = "";
            ViewBag.data = data;
            ViewBag.orderNo = id;
            return View();
        }
        public ActionResult ViewInvoices(string FromDate, string ToDate)
        {
            if (!string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(ToDate))
            {
                return View(OrderService.Instance.SearchByDate(FromDate, ToDate));
            }
            else
            {

                return View(OrderService.Instance.GetAllOrders());
            }
        }
        public ActionResult ReturnSales() {
            return View();
        }
        public ActionResult ReturnProducts(int billno)
        {
            var ob = new List<OrderItem>();
            ob = SaleService.Instance.GetBillNoItems(billno);
            if (ob.Count() == 0)
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(ob, JsonRequestBehavior.AllowGet);
            }
        }
        public void sample()
        {
            
        }
        public ActionResult thermalPrinterRecipt(int id)
        {
            List<OrderItem> data = new List<OrderItem>();
            data = PurchaseService.Instance.GetOrderDetials(id);
            decimal total = 0;
            int numberOfItems = 0;
            string custName = PurchaseService.Instance.GetCustomerName(id);
            foreach (var item in data)
            {
                total = total + item.Amount;
                numberOfItems++;
            }
            ViewBag.total = total;
            ViewBag.CustomerName = custName;
            ViewBag.numberofitems = numberOfItems;
            ViewBag.BillNo = "";
            ViewBag.data = data;
            ViewBag.orderNo = id;
            return View();
        }
        public void print(int id)
        {
            //code edited
            List<OrderItem> data = new List<OrderItem>();
            data = PurchaseService.Instance.GetOrderDetials(id);
            decimal total = 0;
            int numberOfItems = 0;
            string custName = PurchaseService.Instance.GetCustomerName(id);
            foreach (var item in data)
            {
                total = total + item.Amount;
                numberOfItems++;
            }
            ViewBag.data = data;
            var totalAmount = total.ToString();
            
            //code edited end
            string DeveloperName= "Waleed Saddique";
            string DMobileNumber="03465888624";
            Printer printer = new Printer("BIXOLON SRP-350plusII");
            printer.AlignCenter();
            printer.Append("Super Shaheen Chemist");
            printer.Append("DHQ KOTLI AZAD KASHMIR");
            printer.Append("Md: Ikram Asghar");
            printer.Append("Mobile # 0331-7707777");
            printer.Append("----------------------------------------");
            printer.AlignLeft();
            printer.Append("Bill: "+id);
            if (custName != "")
            {
                printer.Append("Customer Name: " + custName);
            }
            printer.Append("=========================================");


            
            printer.CondensedMode(PrinterModeState.On);
            printer.Append(string.Format("{0,-8}{1,-8}{2,-14}{3,-12}{4,-8}", "Pack", "Unit", "Item", "B.No", "Amount"));
            printer.Append("=======================================================");
            foreach (var item in data)
            {
                
                printer.Append(string.Format("{0,-8}{1,-8}{2,-14}{3,-12}{4,-8}", item.Quantity, item.Loose, item.Product.ProductName, item.BatchNo, item.Amount));
                

            }
            printer.CondensedMode(PrinterModeState.Off);
            printer.Append("----------------------------------------");
            printer.AlignCenter();
            printer.Font("Total Amount Rs:"+totalAmount, Fonts.FontC);
            printer.Append("----------------------------------------");
            printer.NewLines(2);
            printer.AlignCenter();
            printer.Append("Developed By");
            printer.Append(DeveloperName);
            printer.Append(DMobileNumber);
            printer.NewLines(2);
            printer.FullPaperCut();
            printer.PrintDocument();
        }
    }
}
