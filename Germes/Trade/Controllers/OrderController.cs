using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DataLayer.DAL.Entities;
using DataLayer.DAL.UnitOfWork;
using Trade.Models;


namespace Trade.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private UnitOfWork unit;
        private Cart cart;

        public OrderController()
        {
            unit = new UnitOfWork();
            cart = new Cart(unit);
        }

        private OrderViewModel Model(Order order, IEnumerable<CartItem> cart, IEnumerable<Product> products, IEnumerable<Category> categories)
        {
            return new OrderViewModel() { Order = order, Cart = cart, Statuses = GetStatus(), Products = products, Categories = categories };
        }

        // GET: Order
        public ActionResult Index()
        {
            return View(Model(null, null, unit.Products.GetAll(), unit.Categories.GetAll()));
        }


        public ActionResult Cart()
        {
            return View(Model(null, null, unit.Products.GetAll(), unit.Categories.GetAll()));
        }


        public ActionResult AddToCart(int OrderID, int ProductID)
        {

            Product tempProd = unit.Products.Get(ProductID);
            Order tempOrder = unit.Orders.Get(OrderID);

            if (tempProd != null && tempOrder != null && tempProd.Quantity > 0)
            {
                cart.AddItem(tempProd, tempOrder);
                cart.CalculateTotalAmount(tempOrder);
            }

            return View("Index", Model(tempOrder, GetCartItems(tempOrder), unit.Products.GetAll(), unit.Categories.GetAll()));
        }

        public ActionResult RemoveFromCart(int OrderID, int ProductID)
        {

            Product tempProd = unit.Products.Get(ProductID);
            Order tempOrder = unit.Orders.Get(OrderID);

            if (tempProd != null && tempOrder != null)
            {
                cart.RemoveItem(tempProd, tempOrder);
                cart.CalculateTotalAmount(tempOrder);
            }

            return View("Index", Model(tempOrder, GetCartItems(tempOrder), unit.Products.GetAll(), unit.Categories.GetAll()));
        }




        public ActionResult Filter(int idCategory, int idOrder)
        {
            return View("Index", Model(
                (idOrder != 0 ? unit.Orders.Get(idOrder) : new Order()), 
                GetCartItems(idOrder), 
                (idCategory == 1 ? unit.Products.GetAll() : unit.Products.GetAll().Where(f => f.Category.CategoryID == idCategory)), 
                unit.Categories.GetAll()));
        }


        [HttpPost]
        public ActionResult Search(string request, int idOrder, int key = 0)
        {
            var result = Helpers.Search.SearchProduct(unit, key, request);
            return View("Index", Model(
                (idOrder != 0 ? unit.Orders.Get(idOrder) : new Order()), 
                GetCartItems(idOrder), 
                result != null ? result : unit.Products.GetAll(), 
                unit.Categories.GetAll()));
        }

        /*  EDIT  */

        public ActionResult Edit(int id)
        {
            var order = unit.Orders.Get(id);
            return View("Index", Model(order, GetCartItems(order), unit.Products.GetAll(), unit.Categories.GetAll()));
        }

        [HttpPost]
        public ActionResult Edit(OrderViewModel model)
        {
            var order = unit.Orders.Get(model.Order.OrderID);
            bool isClosed = order.Status.NameStatus.Equals("Closed");
            try
            {
                order.Client = model.Order.Client;
                order.Status = unit.Statuses.Get(model.SelectedSatusID);
                order.DeliveryDate = model.Order.DeliveryDate.Value.Date;
                order.DeliveryTimeFrom = model.Order.DeliveryTimeFrom;
                order.DeliveryTimeTo = model.Order.DeliveryTimeTo;
                order.Description = model.Order.Description;
                order.CostDelivery = model.Order.CostDelivery;

                if (isClosed)
                {
                    order.CloseOrder = DateTime.Now;
                }

                if (ModelState.IsValid && !isClosed)
                {
                    unit.Orders.Update(order);
                    unit.Save();
                }

                return View("Index", Model(order, GetCartItems(order), unit.Products.GetAll(), unit.Categories.GetAll()));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message.ToString();
                return View("Index", Model(order, GetCartItems(order), unit.Products.GetAll(), unit.Categories.GetAll()));
            }

        }

        /*  CREATE  */

        public ActionResult Create()
        {
            var orderNew = new Order()
            {
                Client = new Client()
                {
                    PhoneNumber = "+375000000000"
                },
                CreateOrder = DateTime.Now,
                Status = unit.Statuses.Get(1)
            };
            unit.Orders.Create(orderNew);
            unit.Save();

            return View("Index", Model(orderNew, GetCartItems(orderNew), unit.Products.GetAll(), unit.Categories.GetAll()));
        }


        /*  CART    */

        public IEnumerable<CartItem> GetCartItems(Order order)
        {
            return unit.Cart.GetAll().Where(x => x.Order.OrderID == order.OrderID);
        }
        public IEnumerable<CartItem> GetCartItems(int orderID)
        {
            if (orderID > 0) {
                var order = unit.Orders.Get(orderID);
                return unit.Cart.GetAll().Where(x => x.Order.OrderID == order.OrderID);
            }
            else
            {
                return null;
            }
        }

        /*  STATUSES -- for dropdown list   */

        private IEnumerable<SelectListItem> GetStatus(object selectedStatus = null)
        {
            var statusAll = unit.Statuses.GetAll();
            var statuses = statusAll.Select(x => new SelectListItem
            {
                Value = x.StatusID.ToString(),
                Text = x.NameStatus,
            });

            return new SelectList(statuses, "Value", "Text", selectedStatus);
        }

    }
}