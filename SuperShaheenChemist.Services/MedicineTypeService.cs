using SuperShaheenChemist.Database;
using SuperShaheenChemist.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperShaheenChemist.Services
{
    public class MedicineTypeService
    {
        #region Singleton
        public static MedicineTypeService Instance
        {
            get
            {
                if (instance == null) instance = new MedicineTypeService();
                return instance;
            }
        }
        private static MedicineTypeService instance { get; set; }
        private MedicineTypeService()
        {

        }
        #endregion
        public List<MedicineType> GetAllMedicineTypes()
        {
            using (var context = new CBContext())
            {
                return context.Types.ToList();
            }
        }
    }
}
