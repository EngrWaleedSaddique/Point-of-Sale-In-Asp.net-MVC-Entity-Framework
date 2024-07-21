using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperShaheenChemist.Entities
{
    public class PurchaseOrderMaster
    {
        public int Id { get; set; }
        public int OrderNo { get; set; }
        public DateTime Date { get; set; }
        public string CreatedBy { get; set; }
        public decimal totalAmount { get; set; }
    }
}