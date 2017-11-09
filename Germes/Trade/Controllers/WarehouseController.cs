using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trade.Models;
using Trade.Helpers;
using DataLayer.DAL.Entities;
using DataLayer.DAL.UnitOfWork;
using System.Xml;
using System.IO;
using System.Text;

namespace Trade.Controllers
{
    [Authorize]
    public class WarehouseController : Controller
    {
        UnitOfWork unit;
        ProductPriceManager manager;
        public WarehouseController()
        {
            unit = new UnitOfWork();
            manager = new ProductPriceManager(unit);
        }


        private WarehouseViewModel Model(Product product, IEnumerable<Product> products, IEnumerable<Category> categories, IEnumerable<Price> prices)
        {
            return new WarehouseViewModel() { Product = product, Products = products, CategoriesSLI = GetCategory(), Categories = categories, Price = prices };
        }

        // GET: Warehouse
        public ActionResult Index()
        {
            return View(Model(null, unit.Products.GetAll(), unit.Categories.GetAll(), unit.Prices.GetAll()));
        }

        public ActionResult MergeItems()
        {
            return View(Model(null, unit.Products.GetAll(), unit.Categories.GetAll(), unit.Prices.GetAll()));
        }

        [HttpPost]
        public ActionResult MergeItems(WarehouseViewModel model, string radioProduct = null, string radioPrice = null)
        {
            try
            {
                var product = unit.Products.Get(Int32.Parse(radioProduct));
                var priceItem = unit.Prices.Get(Int32.Parse(radioPrice));

                manager.CreateMerge(product, priceItem);

                return RedirectToAction("MergeItems");
            }
            catch (ArgumentNullException)
            {
                return RedirectToAction("MergeItems");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message.ToString();
                return View("MergeItems", Model(null, unit.Products.GetAll(), unit.Categories.GetAll(), unit.Prices.GetAll()));
            }
        }

        public ActionResult DeleteMerge(int idProduct, int idMerge)
        {
            try
            {
                manager.DeleteMerge(unit.Products.Get(idProduct), unit.Prices.Get(idMerge));

                return View("MergeItems", Model(null, unit.Products.GetAll(), unit.Categories.GetAll(), unit.Prices.GetAll()));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message.ToString();
                return View("MergeItems", Model(null, unit.Products.GetAll(), unit.Categories.GetAll(), unit.Prices.GetAll()));
            }
        }

        public ActionResult Update()
        {
            manager.UpdateAll();
            ViewBag.Message = "Success";
            return View("Index", Model(null, unit.Products.GetAll(), unit.Categories.GetAll(), unit.Prices.GetAll()));
        }


        [HttpPost]
        public ActionResult Search(string query, int key = 0)
        {
            var result = Helpers.Search.SearchProduct(unit, key, query);
            return View("Index", Model(null, result, unit.Categories.GetAll(), unit.Prices.GetAll()));
        }

        [HttpPost]
        public ActionResult SearchMerge(string queryMergePrice, string queryMergeProduct, int key = 0)
        {
            var searchProduct = Helpers.Search.SearchProduct(unit, key, queryMergeProduct);
            var searchPriceItem = Helpers.Search.SearchPriceItem(unit, queryMergePrice);
            var resultProduct = searchProduct != null ? searchProduct : unit.Products.GetAll();
            var resultPrice = searchPriceItem != null ? searchPriceItem : unit.Prices.GetAll();

            return View("MergeItems", Model(null, resultProduct, unit.Categories.GetAll(), resultPrice));
        }

        public ActionResult Delete(int id)
        {
            unit.Products.Delete(id);
            unit.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            return View(Model(
                unit.Products.Get(id),
                null,
                unit.Categories.GetAll(),
                null
                ));
        }

        [HttpPost]
        public ActionResult Edit(WarehouseViewModel model, HttpPostedFileBase imageUpload = null)
        {
            try
            {
                var productUpdate = model.Product;
                productUpdate.Category = unit.Categories.Get(model.SelectedCategoryID);

                if (ModelState.IsValid)
                {
                    unit.Products.Update(productUpdate);
                    unit.Save();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message.ToString();
                return View(Model(model.Product, null, unit.Categories.GetAll(), unit.Prices.GetAll()));
            }
        }

        public ActionResult Create()
        {
            return View(Model(new Product(), null, unit.Categories.GetAll(), unit.Prices.GetAll()));
        }


        [HttpPost]
        public ActionResult Create(WarehouseViewModel model, HttpPostedFileBase imageUpload = null)
        {
            try
            {
                var product = model.Product;
                product.Category = unit.Categories.Get(model.SelectedCategoryID);
                if (ModelState.IsValid)
                {
                    unit.Products.Create(product);
                    unit.Save();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }


        public void ExportProductXML()
        {
            using (MemoryStream stream = new MemoryStream())
            {

                var data = unit.Products.GetAll();
                // Create an XML document. Write our specific values into the document.
                XmlTextWriter xmlWriter = new XmlTextWriter(stream, Encoding.UTF8);
                // Write the XML document header.
                xmlWriter.WriteStartDocument();
                // Write our first XML header.
                xmlWriter.WriteStartElement("price-list");

                foreach (var item in data)
                {
                    // Write an element representing a single web application object.
                    xmlWriter.WriteStartElement("item");
                    // Write child element data for our web application object.
                    xmlWriter.WriteElementString("ID", item.ProductID.ToString());
                    xmlWriter.WriteElementString("Category", item.Category.NameCayegory);
                    xmlWriter.WriteElementString("Manufacturer", item.Manufacturer);
                    xmlWriter.WriteElementString("Model", item.Model);
                    xmlWriter.WriteElementString("ExtendedModel", item.ExtendedModel);
                    xmlWriter.WriteElementString("Description", item.Description);
                    xmlWriter.WriteElementString("Color", item.Color);
                    xmlWriter.WriteElementString("Warranty", item.Warranty);
                    xmlWriter.WriteElementString("Quantity", item.Quantity.ToString());
                    xmlWriter.WriteElementString("PriceIn", item.PriceIn.ToString());
                    xmlWriter.WriteElementString("PriceSale", item.PriceSale.ToString());
                    // End the element WebApplication
                    xmlWriter.WriteEndElement();
                }

                // End the document WebApplications
                xmlWriter.WriteEndElement();
                // Finilize the XML document by writing any required closing tag.
                xmlWriter.WriteEndDocument();
                // To be safe, flush the document to the memory stream.
                xmlWriter.Flush();
                // Convert the memory stream to an array of bytes.
                byte[] byteArray = stream.ToArray();
                // Send the XML file to the web browser for download.
                Response.Clear();
                Response.AppendHeader("Content-Disposition", "filename=products.xml");
                Response.AppendHeader("Content-Length", byteArray.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(byteArray);
                xmlWriter.Close();
            }
        }

        private IEnumerable<SelectListItem> GetCategory()
        {
            var categoryAll = unit.Categories.GetAll();
            var categories = categoryAll.Select(x => new SelectListItem
            {
                Value = x.CategoryID.ToString(),
                Text = x.NameCayegory
            });

            return new SelectList(categories, "Value", "Text");
        }
    }
}