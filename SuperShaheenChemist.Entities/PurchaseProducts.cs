using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperShaheenChemist.Entities
{
    public class PurchaseProducts
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Qty { get; set; }
        public int TotalAmount { get; set; }
        public int TransactionId { get; set; }
        public string BatchNo { get; set; }

    }
}
