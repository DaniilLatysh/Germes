using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.DAL.Entities;
using DataLayer.DAL.UnitOfWork;
using System.Data;
using Trade.Models;
using Excel = Microsoft.Office.Interop.Excel;
using Trade.Helpers;

namespace Trade.Controllers
{
    [Authorize]
    public class PriceController : Controller
    {
        private UnitOfWork unit;

        public PriceController()
        {
            unit = new UnitOfWork();
        }

        private PriceViewModel Model(IEnumerable<Price> Price, IEnumerable<Supplier> suppliers)
        {
            return new PriceViewModel() {Price = Price, Suppliers = suppliers, SuppliersSLI = GetSuppliers()};
        }

        private Price UpdatePriceItem(Price currentItem, Price newItem)
        {
            currentItem.PriceIn = newItem.PriceIn;
            currentItem.PriceSale = newItem.PriceSale;
            currentItem.ProductName = newItem.ProductName;
            currentItem.Quantity = newItem.Quantity;
            currentItem.Warranty = newItem.Warranty;
            return currentItem;
        }

        // GET: Price
        public ActionResult Index()
        {
            return View(Model(null, unit.Suppliers.GetAll()));
        }    

        /*  SAVE    */

        public ActionResult Save(List<Price> items, PriceViewModel model)
        {
            try
            {
                var price = unit.Prices.GetAll();

                foreach (var item in items)
                {
                    //-- update if item exist ----
                    if (price.Select(x => x.ProductKey).Contains(item.ProductKey))
                    {
                        var update = price.Where(x => x.ProductKey == item.ProductKey).First();

                        unit.Prices.Update(UpdatePriceItem(update, item));
                    }
                    else
                    {
                        unit.Prices.Create(item);
                    }

                    unit.Save();
                }

                return View("Index", Model(null, unit.Suppliers.GetAll()));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message.ToString();
                return View("Index", Model(null, unit.Suppliers.GetAll()));
            }
        }

        /*  DELETE  */

        public ActionResult Delete(int id)
        {
            try
            {
                unit.Prices.Delete(id);
                unit.Save();
                ViewBag.ListItems = unit.Prices.GetAll();
                return View("Index", Model(null, unit.Suppliers.GetAll()));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message.ToString();
                return View("Index", Model(null, unit.Suppliers.GetAll()));
            }
        }

        [HttpPost]
        public ActionResult Upload(PriceViewModel model)
        {
            try
            {
                var items = unit.Prices.GetAll().Where(x => x.Supplier.SupplierID == model.SelectedSupplierID).ToList();
                if (items.Count > 0)
                {
                    ViewBag.ListItems = items;
                    return View("Index", Model(null, unit.Suppliers.GetAll()));
                }
                ViewBag.Error = "Supplier has no items!";
                return View("Index", Model(null, unit.Suppliers.GetAll()));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
                return View("Index", Model(null, unit.Suppliers.GetAll()));
            }
        }

        /*  Download  */

        public ActionResult Download()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Download(HttpPostedFileBase excelFile, PriceViewModel model,
            int Key, 
            int productName, 
            int productWarrianty, 
            int productQuantity, 
            int productPriceIn,
            int productPriceSale
            )
        {

            try
            {
                if (excelFile.FileName.EndsWith("xls") || excelFile.FileName.EndsWith("xlsx"))
                {
                    if (excelFile.ContentLength > 0 && excelFile != null)
                    {

                        string path = Server.MapPath("~/Content/Prices/" + excelFile.FileName);

                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }

                        excelFile.SaveAs(path);

                        Excel.Application app = new Excel.Application();
                        Excel.Workbook workbook = app.Workbooks.Open(path);
                        Excel.Worksheet worksheet = workbook.ActiveSheet;
                        Excel.Range range = worksheet.UsedRange;

                        
                        double temp = 0;
                        int flag = 0;
                        var markup = unit.Settings.Get(1).Markup;
                        var currency = unit.Settings.Get(1).Currency;
                        var supplier = unit.Suppliers.Get(model.SelectedSupplierID);
                        List<Price> items = new List<Price>();

                        for (int row = 1; row < range.Rows.Count; row++)
                        {
                            try
                            {
                                string productKey = ((Excel.Range)range.Cells[row, Key]).Text;


                                if (!(String.IsNullOrEmpty(productKey)) && Int32.TryParse(productKey, out flag))
                                {
                                    Price item = new Price();
                                    item.ProductKey = productKey;
                                    item.Supplier = supplier;
                                    item.ProductName = ((Excel.Range)range.Cells[row, productName]).Text;
                                    item.Warranty = ((Excel.Range)range.Cells[row, productWarrianty]).Text;
                                    item.Quantity = ((Excel.Range)range.Cells[row, productQuantity]).Text;

                                    if (productPriceIn != 0)
                                        item.PriceIn = double.TryParse(((Excel.Range)range.Cells[row, productPriceIn]).Text, out temp) ? temp : 0;
                                    if (productPriceSale != 0)
                                    {
                                        item.PriceSale = double.TryParse(((Excel.Range)range.Cells[row, productPriceSale]).Text, out temp) ? temp :
                                            CurrencyMaker.MakePriceSale(item.PriceIn.Value, markup, currency); // make price for sale
                                    }
                                    else
                                    {
                                        item.PriceSale = CurrencyMaker.MakePriceSale(item.PriceIn.Value, markup, currency);
                                    }
                                    items.Add(item);
                                }
                            }
                            catch
                            {
                                ViewBag.Error = "Cannot parsing row <br/>";
                            }
                        }

                        ViewBag.ListItems = items;
                        workbook.Close();
                        System.IO.File.Create(path).Close();
                        System.IO.File.Delete(path);
                        Save(items, model);

                        return View("Index", Model(null, unit.Suppliers.GetAll()));
                    }
                }
                else
                {
                    ViewBag.Error = "File type is incorrect <br/>";
                    return View("Index", Model(null, unit.Suppliers.GetAll()));
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message.ToString();
                return View("Index", Model(null, unit.Suppliers.GetAll()));
            }
            return RedirectToAction("Index");

        }


        private IEnumerable<SelectListItem> GetSuppliers(object selectedStatus = null)
        {
            var suppliersAll = unit.Suppliers.GetAll();
            var suppliers = suppliersAll.Select(x => new SelectListItem
            {
                Value = x.SupplierID.ToString(),
                Text = x.ShortNameSupplier,
            });

            return new SelectList(suppliers, "Value", "Text", selectedStatus);
        }

    }
}
