using DataLayer.DAL.UnitOfWork;
using DataLayer.DAL.Entities;
using System.Linq;
using System.Web.Mvc;

namespace Trade.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private UnitOfWork unit;
        public AdminController()
        {
            unit = new UnitOfWork();
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View(unit.Settings.GetAll().FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Index(Settings model)
        {
            unit.Settings.Update(model);
            unit.Save();
            return View(unit.Settings.GetAll().FirstOrDefault());
        }
    }
}