using SuperShaheenChemist.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperShaheenChemist.Web.ViewModels
{
    public class ProductAddViewModel
    {

    }
    public class DropDownViewModels
    {
        public List<Category> categories { get; set; }
        public List<Company> companies { get; set; }
        public List<MedicineType> medicinesTypes { get; set; }
        public List<Distributor> distributors { get; set; }

    }
    public class EditViewModel
    {
        public Product product { get; set; }
        public List<Category> categories { get; set; }
        public List<Company> companies { get; set; }
        public List<MedicineType> medicinesTypes { get; set; }
        public List<Distributor> distributors { get; set; }
    }

}
