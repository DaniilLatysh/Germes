using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer.DAL.Entities;
using DataLayer.DAL.UnitOfWork;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Trade.Helpers
{
    public class ProductPriceManager
    {
        private UnitOfWork unit;
        public ProductPriceManager(UnitOfWork unit)
        {
            this.unit = unit;
        }

        public Product CreateMerge(Product product, Price price)
        {
            try
            {
                if (product.Price.Select(x => x.ProductKey).Contains(price.ProductKey))
                {
                    product.Price.Remove(price);
                }

                product.Price.Add(price);
            //    product.CurrentPrice = price;
                product = Update(product) != null ? Update(product) : product;
                unit.Products.Update(product);
                unit.Save();
                return product;
            }
            catch
            {
                return null;
            }
        }

        public Product DeleteMerge(Product product, Price price)
        {
            try
            {
                product.Price.Remove(price);
                if (product.Price.Count < 1)
                {
                    product.PriceIn = 0;
                    product.PriceSale = 0;
                    product.Warranty = "";
                    product.Quantity = 0;
                    product.CurrentPrice = null;
                }
                else
                {
                    product.CurrentPrice = product.Price.First();
                }

                unit.Products.Update(product);
                unit.Save();
                return Update(product);
            }
            catch
            {
                return null;
            }
        }


        /**
         *  Updating price of product. 
         *  If can't update - return null
         */

        public Product Update(Product product)
        {
            try
            {
                Price minPrice = null;
                minPrice = FindBestProductPrice(product);

                int tempQuantity = 0;
                var markup = unit.Settings.Get(1).Markup;
                var currency = unit.Settings.Get(1).Currency;

                if (minPrice != null)
                {
                    if (Int32.TryParse(minPrice.Quantity, out tempQuantity))
                    {
                        product.Quantity = tempQuantity;
                    }

                    product.PriceIn = minPrice.PriceIn;
                    product.Warranty = minPrice.Warranty;
                    product.CurrentPrice = minPrice;
                }

                product.PriceSale = CurrencyMaker.MakePriceSale(product.PriceIn.Value, markup, currency);

                unit.Products.Update(product);
                unit.Save();
                return product;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message.ToString());
                return null;
            }
        }

        private Price FindBestProductPrice(Product product)
        {
            if (product.Price.Count > 0)
            {
                var minPrice = product.Price.First();
                var allPrices = product.Price;

                foreach (var item in allPrices)
                {
                    if (item.PriceIn < minPrice.PriceIn)
                    {
                        minPrice = item;
                    }
                }

                return minPrice;
            }
            return null;
        }

        public void UpdateAll()
        {
            var products = unit.Products.GetAll();

            foreach (var item in products)
            {
                Update(item);
            }

        }
    }
}