using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperShaheenChemist.Entities
{
    
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderID { get; set; }
        public string BatchNo { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Discount { get; set; }
        public int Quantity { get; set; }
        public int Loose { get; set; }
        public decimal Amount { get; set; }
        public virtual Order Order { get; set; }
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }

    }
}