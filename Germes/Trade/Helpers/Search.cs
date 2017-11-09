using DataLayer.DAL.Entities;
using DataLayer.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Trade.Helpers
{
    public static class Search
    {
        public static IEnumerable<Product> SearchProduct(UnitOfWork unit, int key, string query)
        {
            IEnumerable<Product> result = null;
            if (key > 0)
            {
                return result = unit.Products.GetAll().Where(x => x.ProductID == key);
            }
            else if (!(String.IsNullOrEmpty(query)))
            {
                return result = unit.Products.Find(f => f.ToString().Contains(query.ToUpper())).ToList();
            }
            return null;
        }

        public static IEnumerable<Price> SearchPriceItem(UnitOfWork unit, string query)
        {
            IEnumerable<Price> result = null;
            if (!(String.IsNullOrEmpty(query)))
            {
                result = unit.Prices.Find(f => f.ProductName.ToUpper().Contains(query.ToUpper())).ToList();
            }
            return result;
        }

        public static IEnumerable<Order> SerchOrders(UnitOfWork unit, DateTime date, int orderId)
        {
            List<Order> result = new List<Order>();
            List<Order> listSort = new List<Order>();
            var statuses = unit.Statuses.GetAll();

            //-- Filter orders ----
            if (orderId > 0)
            {
                result.AddRange(unit.Orders.GetAll().Where(x => x.OrderID == orderId).ToList());
                return result;
            }            
            else if (date != null)
            {
                result.AddRange(unit.Orders.Find(f => f.CreateOrder.Date == date.Date).ToList());
                var deliveryOrders = unit.Orders.Find(f => f.DeliveryDate == date.Date).ToList();
                if (deliveryOrders != null)
                {
                    foreach (var item in deliveryOrders)
                    {
                        if (!result.Contains(item)) { result.Add(item); }
                    }
                }               
            }

            //-- Sorting orders ----
            for (int i = 1; i <= statuses.Count(); i++)
            {
                foreach (var item in result)
                {
                    if (item.Status.StatusID == i)
                    {
                        listSort.Add(item);
                    }
                }
            }

            return listSort;
        }
    }
}