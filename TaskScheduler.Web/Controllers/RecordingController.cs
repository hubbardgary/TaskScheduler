using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TaskScheduler.Web.Models.Recording;
using TaskScheduler.Web.Services.Interfaces;

namespace TaskScheduler.Web.Controllers
{
    public class RecordingController : Controller
    {
        private readonly IRecordingServices _recordingServices;
        private readonly IShutdownServices _shutdownServices;

        public RecordingController(IRecordingServices recordingServices, IShutdownServices shutdownServices)
        {
            _recordingServices = recordingServices;
            _shutdownServices = shutdownServices;
        }

        public ActionResult Index(int? page, string sortOrder = "Title")
        {
            var recordings = _recordingServices.GetSortedRecording(sortOrder);
            
            ViewBag.CurrentSort = sortOrder;
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            if (recordings.Count() > pageSize * (pageNumber - 1))
            {
                return View(recordings.ToPagedList(pageNumber, pageSize));
            }

            // Default to Page 1 if the requested page wouldn't have any rows
            return View(recordings.ToPagedList(1, pageSize));
        }
        
        public ActionResult Create()
        {
            var newRecording = new RecordingViewModel();
            return View(newRecording);
        }
        
        [HttpPost]
        public ActionResult Create(RecordingViewModel recording)
        {
            if (ModelState.IsValid)
            {
                _recordingServices.AddRecording(recording);

                if (recording.CreateShutdownTask)
                {
                    _shutdownServices.CreateShutdownFromRecording(recording);
                }

                return RedirectToAction("Index");
            }
            return View(recording);
        }
        
        public ActionResult Edit(string id)
        {
            var recording = _recordingServices.GetRecording(id);
            return View(recording);
        }
        
        [HttpPost]
        public ActionResult Edit(string id, RecordingViewModel recording)
        {
            if (ModelState.IsValid)
            {
                _recordingServices.UpdateRecording(recording);
                _shutdownServices.UpdateLinkedShutdown(recording);
                return RedirectToAction("Index");
            }
            return View(recording);
        }

        [HttpPost]
        public ActionResult Index(int page, List<RecordingViewModel> recordings)
        {
            foreach (var recording in recordings)
            {
                if (recording.Selected)
                {
                    _recordingServices.DeleteRecording(recording);
                    _shutdownServices.DeleteLinkedShutdown(recording);
                }
            }
            return RedirectToAction("Index", new { page = page });
        }

        public ActionResult ValidateUniqueShowTitle(RecordingViewModel recording)
        {
            if (recording.Title == recording.PreviousTitle)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(_recordingServices.GetRecording(recording.Title) == null, JsonRequestBehavior.AllowGet);
        }
    }
}