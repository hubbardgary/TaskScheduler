using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
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

        public ActionResult Index(int? page, string sortOrder = "Name")
        {
            var shutdowns = _shutdownServices.GetSortedShutdowns(sortOrder);

            ViewBag.CurrentSort = sortOrder;
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            if (shutdowns.Count() > pageSize * (pageNumber - 1))
            {
                return View(shutdowns.ToPagedList(pageNumber, pageSize));
            }

            // Default to Page 1 if the requested page wouldn't have any rows
            return View(shutdowns.ToPagedList(1, pageSize));
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
                return RedirectToAction("Index");
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
                return RedirectToAction("Index");
            }
            return View(shutdown);
        }

        [HttpPost]
        public ActionResult Index(int page, List<ShutdownViewModel> shutdowns)
        {
            foreach (var shutdown in shutdowns)
            {
                if (shutdown.Selected)
                    _shutdownServices.DeleteShutdown(shutdown);
            }
            return RedirectToAction("Index", new { page = page });
        }
    }
}