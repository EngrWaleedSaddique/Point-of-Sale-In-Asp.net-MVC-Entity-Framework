using SuperShaheenChemist.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperShaheenChemist.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.OutofStock=ProductsService.Instance.OutOfStockProducts().Count;
            ViewBag.NoOfProducts = ProductsService.Instance.GetProducts().Count;
            ViewBag.CashTotalToday = ProductsService.Instance.CashTotalToday();
            ViewBag.SalesToday = ProductsService.Instance.SaleToday();
            ViewBag.MonthTotalSales = ProductsService.Instance.MonthTotalSales();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
