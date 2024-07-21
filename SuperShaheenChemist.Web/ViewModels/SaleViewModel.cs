using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperShaheenChemist.Web.ViewModels
{
    public class SaleViewModel
    {
        public int ProductID { get; set; }
        public string Quantity { get; set; }
        public string Loose { get; set; }
        public string BatchNo { get; set; }
        public string Price { get; set; }
        public string ExpiryDate { get; set; }
        public string Discount { get; set; }
        public string Amount { get; set; }
    }
    public class ReturnItemsViewModel
    {
        public int RecordID { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Loose { get; set; }
        public int Discount { get; set; }
        public decimal ReturnAmount { get; set; }
    }
}
