using SuperShaheenChemist.Database;
using SuperShaheenChemist.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperShaheenChemist.Services
{
    public class CompanyService
    {
        #region Singleton
        public static CompanyService Instance
        {
            get
            {
                if (instance == null) instance = new CompanyService();
                return instance;
            }
        }
        private static CompanyService instance { get; set; }
        private CompanyService()
        {

        }
        #endregion
        public List<Company> GetAllCompanies()
        {
            using (var context = new CBContext())
            {
                return context.Companies.ToList();
            }
        }
    }
}
