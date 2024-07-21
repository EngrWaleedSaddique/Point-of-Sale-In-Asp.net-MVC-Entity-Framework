using SuperShaheenChemist.Database;
using SuperShaheenChemist.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperShaheenChemist.Services
{
    public class DistributorService
    {
        #region Singleton
        public static DistributorService Instance
        {
            get
            {
                if (instance == null) instance = new DistributorService();
                return instance;
            }
        }
        private static DistributorService instance { get; set; }
        private DistributorService()
        {

        }
        #endregion
        public List<Distributor> GetAllDistributors()
        {
            using (var context = new CBContext())
            {
                return context.Distributors.ToList();
            }
        }
    }
}