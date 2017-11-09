using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Trade.Models;
using DataLayer.DAL.Interfaces;
using DataLayer.DAL.Repositories;
using DataLayer.DAL.Entities;

namespace Trade.Controllers
{
    public class MenuController : Controller
    {
        IRepository<Order> _repository;
        public MenuController(IRepository<Order> rep)
        {
            _repository = rep;
        }

        List<MenuItem> menuItems = new List<MenuItem>
        {
            new MenuItem { Name = "Trade", Controller = "Trade", Action = "Index", Active = string.Empty, imageUrl = "/Content/Images/trade.png"},
            new MenuItem { Name = "Warehouse", Controller = "Warehouse", Action = "Index", Active = string.Empty, imageUrl = "/Content/Images/warehouse.png"},
            new MenuItem { Name = "Settings", Controller = "Admin", Action = "Index", Active = string.Empty, imageUrl = "/Content/Images/settings.png"}
        };

        // GET: Menu
        public PartialViewResult Index() => PartialView(menuItems);

        public PartialViewResult Main(string a = "Index", string c = "Menu")
        {
            _repository = new EFOrderRepository("Connection");
            var txt = menuItems.Where(m => m.Controller == c)?.FirstOrDefault();
            if (txt != null)
            {
                txt.Active = "active";
            }
            return PartialView(menuItems);
        }
    }
}