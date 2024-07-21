using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperShaheenChemist.Entities
{
    public class Product
    {
        public int Id { get; set; } 
        public string ProductName { get; set; } 
        public string GenericName { get; set; } 
        public string BatchNo { get; set; } 
        public string BarCode { get; set; } 
        public string Description { get; set; } 
        public int MedicineTypeId { get; set; }  
        public string ImageURL { get; set; }  
        public decimal CustDiscount { get; set; } 
        public int CompanyId { get; set; }
        public int DistributorId { get; set; }
        public int CategoryId { get; set; } 
        public decimal MinStock { get; set; } 
        public string SideEffects { get; set; } 
        public decimal PackSize { get; set; } 
        public string Location { get; set; } 
        public DateTime ManufactureDate { get; set; } 
        public DateTime ExpiryDate { get; set; } 
        public decimal UnitCost { get; set; } 
        public decimal UnitRetail { get; set; } 
        public decimal PackCost { get; set; } 
        public decimal PackRetailCost { get; set; } 
        public decimal SalesTax { get; set; }
        public List<PurchaseProducts> Products { get; set; }
        public List<StockInventry> StockInventry { get; set; }
        public List<PurchaseOrder> PurchaseOrder { get; set; }
    }
}
