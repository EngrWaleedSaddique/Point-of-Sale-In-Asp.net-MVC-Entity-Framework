using SuperShaheenChemist.Database;
using SuperShaheenChemist.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperShaheenChemist.Services
{
    public class OrderService
    {
        #region Singleton
        public static OrderService Instance
        {
            get
            {
                if (instance == null) instance = new OrderService();
                return instance;
            }
        }
        private static OrderService instance { get; set; }

        private OrderService()
        {

        }
        #endregion
        public List<Order> SearchByDate(string FromDate, string ToDate)
        {
            DateTime fDate = Convert.ToDateTime(FromDate);
            DateTime tDate = Convert.ToDateTime(ToDate).AddDays(1);
            var purchases = new List<Order>();
            using (var context = new CBContext())
            {
                purchases = context.Orders.Where(x => x.Date >= fDate && x.Date <= tDate).ToList();
                return purchases;
            }
        }
        public List<Order> GetAllOrders()
        {
            using (var context = new CBContext())
            {
                return context.Orders.ToList();
            }
        }

    }
}

