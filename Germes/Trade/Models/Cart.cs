using System;
using System.Linq;
using DataLayer.DAL.Entities;
using DataLayer.DAL.UnitOfWork;

namespace Trade.Models
{
    public class Cart
    {
        private UnitOfWork unit;

        public Cart(UnitOfWork unit)
        {
            this.unit = unit;
        }
        public void AddItem(Product product, Order order)
        {
            //-- If cart not contain that product - create new. Else add quantity + 1 ----
            if (!(order.Products.Select(x => x.Product.ProductID).Contains(product.ProductID)))
            {
                CartItem newCart = new CartItem()
                {
                    Order = order,
                    Product = product,
                    Quantity = 1,
                    PriceIn = product.PriceIn,
                    PriceSale = product.PriceSale
                };
                unit.Cart.Create(newCart);
            }
            else
            {
                var temp = order.Products.Where(x => x.Product.ProductID == product.ProductID).First();
                temp.Quantity++;
                unit.Cart.Update(temp);
            }
            unit.Save();
            WarehouseCountManager(product.ProductID, -1, 1);
        }


        public void RemoveItem(Product product, Order order)
        {
            var temp = order.Products.Where(x => x.Product.ProductID == product.ProductID).First();
            unit.Cart.Delete(temp.CartID);
            unit.Save();
            WarehouseCountManager(product.ProductID, temp.Quantity, -temp.Quantity);
        }

        /* Calculating count and raiting product    */
        private void WarehouseCountManager(int product, int count, int raiting)
        {
            var productEdit = unit.Products.Get(product);
            productEdit.Quantity += count;
            productEdit.Rating += raiting;
            unit.Products.Update(productEdit);
            unit.Save();
        }


        /*  Calculating total amount from cart with delivery    */
        public void CalculateTotalAmount(Order order)
        {
            double amount = 0;

            if (order.Products.Count > 0)
            {
                foreach (var item in order.Products)
                {
                    if (order.Products.Select(x => x.Product.PriceSale) != null)
                    {
                        amount += item.Product.PriceSale.Value * item.Quantity;
                    }
                }
                order.TotalAmount = order.CostDelivery != null ? (amount + order.CostDelivery.Value) : amount;
                unit.Save();
            }
        }
    }
}