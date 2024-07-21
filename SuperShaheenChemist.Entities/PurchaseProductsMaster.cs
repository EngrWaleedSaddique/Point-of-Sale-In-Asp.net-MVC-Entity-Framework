using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperShaheenChemist.Entities
{
    public class PurchaseProductsMaster
    {
        [Key]
        public int TransactionId { get; set; }
        public DateTime Date { get; set; }
        public string CreatedBy { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
