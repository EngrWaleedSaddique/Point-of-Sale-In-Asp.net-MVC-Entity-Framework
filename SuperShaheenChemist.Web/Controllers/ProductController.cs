using SuperShaheenChemist.Entities;
using SuperShaheenChemist.Services;
using SuperShaheenChemist.Web.Models;
using SuperShaheenChemist.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperShaheenChemist.Web.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult add_new_product()
        {
            DropDownViewModels model = new DropDownViewModels();
            model.categories= CategoriesService.Instance.GetAllCategories();
            model.distributors = DistributorService.Instance.GetAllDistributors();
            model.companies = CompanyService.Instance.GetAllCompanies();
            model.medicinesTypes = MedicineTypeService.Instance.GetAllMedicineTypes();
            return View(model);
        }
        [HttpPost]
        public void Create(Product model)
        {
            ProductsService.Instance.SaveProduct(model);
        }
        public ActionResult GetData(JqueryDatatableParam param)
        {
            var products = ProductsService.Instance.GetProducts(); //This method is returning the IEnumerable employee from database 

            //Searching
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                products = products.Where(x => x.ProductName.ToLower().Contains(param.sSearch.ToLower())
                                              || x.GenericName.ToLower().Contains(param.sSearch.ToLower())
                                              || x.BatchNo.ToLower().Contains(param.sSearch.ToLower())
                                              || x.BarCode.ToLower().Contains(param.sSearch.ToLower())
                                              || x.Location.ToLower().Contains(param.sSearch.ToLower())
                                              || x.ExpiryDate.ToString("dd'/'MM'/'yyyy").ToLower().Contains(param.sSearch.ToLower())).ToList();
            }

            //Sorting
            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.QueryString["iSortCol_0"]);
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            if (sortColumnIndex == 3)
            {
                products = sortDirection == "asc" ? products.OrderBy(c => c.ProductName).ToList() : products.OrderByDescending(c => c.ProductName).ToList();
            }
            else if (sortColumnIndex == 4)
            {
                products = sortDirection == "asc" ? products.OrderBy(c => c.GenericName).ToList() : products.OrderByDescending(c => c.GenericName).ToList();
            }
            else if (sortColumnIndex == 5)
            {
                products = sortDirection == "asc" ? products.OrderBy(c => c.PackRetailCost).ToList() : products.OrderByDescending(c => c.PackRetailCost).ToList();
            }
            else
            {
                Func<Product, string> orderingFunction = e => sortColumnIndex == 0 ? e.ProductName : sortColumnIndex == 1 ? e.Location : e.Location;

                products = sortDirection == "asc" ? products.OrderBy(orderingFunction).ToList() : products.OrderByDescending(orderingFunction).ToList();
            }

            //Pagination
            var displayResult = products.Skip(param.iDisplayStart)
               .Take(param.iDisplayLength).ToList();
            var totalRecords = products.Count();


            //Sending data 
            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = displayResult
            }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Edit(int ID)
        {
            EditViewModel model = new EditViewModel();
            model.product = ProductsService.Instance.GetProduct(ID);
            model.categories = CategoriesService.Instance.GetAllCategories();
            model.distributors = DistributorService.Instance.GetAllDistributors();
            model.companies = CompanyService.Instance.GetAllCompanies();
            model.medicinesTypes = MedicineTypeService.Instance.GetAllMedicineTypes();
            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(Product product)
        {
            ProductsService.Instance.UpdateProduct(product);
            //return PartialView(model);
            return new JsonResult { Data = new { status = true } };
        }
        [HttpPost]
        public ActionResult Delete(int ID)
        {
            ProductsService.Instance.DeleteProduct(ID);
            return new JsonResult { Data = new { status = true } };
            
        }
        public ActionResult OutOfStock()
        {
            var products=ProductsService.Instance.OutOfStockProducts();
            ViewBag.data = products;
            return View();
        }

        public ActionResult Stock()
        {
            var products = ProductsService.Instance.getStock();
            ViewBag.data = products;
            return View();
        }
        public ActionResult SaveMedicineType(string type)
        {
            PurchaseService.Instance.AddMedicineType(type);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveCompany(string company)
        {
            PurchaseService.Instance.SaveCompany(company);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveDistributor(string distributor)
        {
            PurchaseService.Instance.SaveDistributor(distributor);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveCategory(string category)
        {
            PurchaseService.Instance.SaveCategory(category);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public ActionResult showCompanyDropDownList()
        {
           var data= CompanyService.Instance.GetAllCompanies();
           return Json(data,JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDistributor()
        {
            var data = DistributorService.Instance.GetAllDistributors();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCategories()
        {
            var data = CategoriesService.Instance.GetAllCategories();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetMedicineTypes()
        {
            var data = MedicineTypeService.Instance.GetAllMedicineTypes();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
