using System;
using System.Web.Mvc;
using DataLayer.DAL.Entities;
using DataLayer.DAL.UnitOfWork;

namespace Trade.Controllers
{
    [Authorize]
    public class SupplierController : Controller
    {

        private UnitOfWork unit;

        public SupplierController()
        {
            unit = new UnitOfWork();
        }

        // GET: Supplier
        public ActionResult Index()
        {
            return View(unit.Suppliers.GetAll());
        }

        public ActionResult CreateSupplier()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateSupplier(Supplier supplier)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Supplier supplierNew = new Supplier();
                    supplierNew = supplier;
                    unit.Suppliers.Create(supplierNew);
                    unit.Save();
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
           
        }

        public ActionResult Edit(int id)
        {
            return View(unit.Suppliers.Get(id));
        }

        [HttpPost]
        public ActionResult Edit(Supplier supplier)
        {
            try
            {                
               if (ModelState.IsValid)
                {
                    var temp = unit.Suppliers.Get(supplier.SupplierID);
                    unit.Suppliers.Update(temp);
                    unit.Save();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message.ToString();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                unit.Suppliers.Delete(id);
                unit.Save();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message.ToString();
                return RedirectToAction("Index");
            }
        }
    }
}