using SuperShaheenChemist.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperShaheenChemist.Web.Controllers
{
    public class StockController : Controller
    {
        // GET: Stock
        public ActionResult Index()
        {
            return View();

        }
        public ActionResult ExpiredStock()
        {
            var stock = StockService.Instance.GetExpiryStock();
            ViewBag.data = stock;
            return View();
        }
    }
}
