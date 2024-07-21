using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperShaheenChemist.Web.ViewModels
{
    public class PurchaseViewModel
    {



        public int ProductID { get; set; }
        public int Quantity{get;set;}
        public int TotalAmount { get; set; }
        public string BatchNo { get; set; }
    }
    public class ReturnViewModel
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public int TotalAmount { get; set; }
        public string BatchNo { get; set; }

    }
    public class PurchaseTableViewModel
    {
        public int Qty { get; set; }
        public int TotalAmount { get; set; }
        public string ProductName { get; set; }
        public string GenericName { get; set; }
        public string Date { get; set; }
    }

}
