using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TaskScheduler.Web.Models.Enums;
using TaskScheduler.Web.Models.Shutdown;
using TaskScheduler.Web.Services.Interfaces;

namespace TaskScheduler.Web.Controllers
{
    public class ShutdownController : Controller
    {
        private readonly IShutdownServices _shutdownServices;

        public ShutdownController(IShutdownServices shutdownServices)
        {
            _shutdownServices = shutdownServices;
        }

        public ActionResult Index(int? page, string sortOrder = "Name", ActionOutcome actionOutcome = ActionOutcome.None)
        {
            var shutdowns = _shutdownServices.GetSortedShutdowns(sortOrder);

            ViewBag.CurrentSort = sortOrder;
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var shutdownsPagedList = shutdowns.Count() > pageSize * (pageNumber - 1) ? shutdowns.ToPagedList(pageNumber, pageSize) : shutdowns.ToPagedList(1, pageSize);

            return View(new ShutdownIndexViewModel
            {
                ActionOutcome = actionOutcome,
                Shutdowns = shutdownsPagedList
            });
        }

        [HttpPost]
        public ActionResult Index(int page, List<ShutdownViewModel> shutdowns)
        {
            shutdowns.Where(s => s.Selected).ToList().ForEach(s => _shutdownServices.DeleteShutdown(s));
            return RedirectToAction("Index", new { page = page, actionOutcome = ActionOutcome.DeleteSuccess });
        }

        public ActionResult Create()
        {
            var newShutdown = new ShutdownViewModel();
            return View(newShutdown);
        }

        [HttpPost]
        public ActionResult Create(ShutdownViewModel shutdown)
        {
            if (ModelState.IsValid)
            {
                _shutdownServices.AddShutdown(shutdown);
                return RedirectToAction("Index", new { actionOutcome = ActionOutcome.CreateSuccess });
            }
            return View(shutdown);
        }

        public ActionResult Edit(string id)
        {
            var shutdown = _shutdownServices.GetShutdown(id);
            return View(shutdown);
        }

        [HttpPost]
        public ActionResult Edit(string id, ShutdownViewModel shutdown)
        {
            if (ModelState.IsValid)
            {
                _shutdownServices.UpdateShutdown(shutdown);
                return RedirectToAction("Index", new { actionOutcome = ActionOutcome.EditSuccess });
            }
            return View(shutdown);
        }

        public ActionResult ValidateUniqueShutdownName(ShutdownViewModel shutdown)
        {
            if (shutdown.Name == shutdown.PreviousName)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(_shutdownServices.GetShutdown(shutdown.Name) == null, JsonRequestBehavior.AllowGet);
        }
    }
}