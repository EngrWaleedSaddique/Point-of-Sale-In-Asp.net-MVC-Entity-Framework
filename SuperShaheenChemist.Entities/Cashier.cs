using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperShaheenChemist.Entities
{
    public class Cashier
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [MaxLength(13)]
        public string MobileNumber { get; set; }
    }
}
