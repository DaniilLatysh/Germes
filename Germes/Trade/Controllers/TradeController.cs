using System;
using System.Linq;
using System.Web.Mvc;
using DataLayer.DAL.UnitOfWork;
using Trade.Models;

namespace Trade.Controllers
{
    [Authorize]
    public class TradeController : Controller
    {
        private UnitOfWork unit;

        public TradeController()
        {
            unit = new UnitOfWork();
        }

        public ActionResult Index()
        {
            var ordersToday = Helpers.Search.SerchOrders(unit, DateTime.Now, 0);
            
            return View(ordersToday);
        }

        [HttpPost]
        public ActionResult SearchOrder(DateTime date, int id)
        {
            var ordersFilter = Helpers.Search.SerchOrders(unit, date, id);
            return View("Index", ordersFilter);
        }

        public ActionResult Details(int id)
        {
            return View(unit.Orders.Get(id));
        }

        public ActionResult Delete(int id)
        {
            Cart cart = new Cart(unit);
            var order = unit.Orders.Get(id);

            if (order.Products.Count > 0)
            {
                foreach (var item in order.Products.ToList())
                {
                    cart.RemoveItem(item.Product, order);
                }
            }

            unit.Orders.Delete(id);
            unit.Save();
            return RedirectToAction("Index");
        }
    }
}
