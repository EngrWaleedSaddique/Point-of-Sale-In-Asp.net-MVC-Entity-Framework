using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperShaheenChemist.Entities
{
    public class PurchaseOrder
    {
        [Key]
        public int Id { get; set; }
        public int PurchaseOrderNo { get; set; }
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string CreatedBy { get; set; }
        
    }
}
