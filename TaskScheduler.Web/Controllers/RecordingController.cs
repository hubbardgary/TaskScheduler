using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TaskScheduler.Web.Models.Enums;
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

        public ActionResult Index(int? page, string sortOrder = "Title", ActionOutcome actionOutcome = ActionOutcome.None)
        {
            var recordings = _recordingServices.GetSortedRecording(sortOrder);
            
            ViewBag.CurrentSort = sortOrder;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            
            var recordingsPagedList = recordings.Count() > pageSize * (pageNumber - 1) ? recordings.ToPagedList(pageNumber, pageSize) : recordings.ToPagedList(1, pageSize);

            return View(new RecordingIndexViewModel
            {
                ActionOutcome = actionOutcome,
                Recordings = recordingsPagedList
            });
        }

        [HttpPost]
        public ActionResult Index(int page, List<RecordingViewModel> recordings)
        {
            var linkedShutdownRemoved = false;
            recordings.Where(r => r.Selected).ToList().ForEach(r =>
            {
                _recordingServices.DeleteRecording(r);

                if (_shutdownServices.LinkedShutdownExists(r.Title))
                {
                    _shutdownServices.DeleteLinkedShutdown(r);
                    linkedShutdownRemoved = true;
                }
            });

            var actionOutcome = linkedShutdownRemoved ? ActionOutcome.DeleteWithLinkedShutdownSuccess : ActionOutcome.DeleteSuccess;
            return RedirectToAction("Index", new { page = page, actionOutcome = actionOutcome });
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

                return RedirectToAction("Index", new { actionOutcome = recording.CreateShutdownTask ? ActionOutcome.CreateWithLinkedShutdownSuccess : ActionOutcome.CreateSuccess });
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
                var linkedShutdownExists = _shutdownServices.LinkedShutdownExists(recording.PreviousTitle);
                _recordingServices.UpdateRecording(recording);
                
                if (linkedShutdownExists)
                {
                    _shutdownServices.UpdateLinkedShutdown(recording);
                }
                
                return RedirectToAction("Index", new { actionOutcome = linkedShutdownExists ? ActionOutcome.EditWithLinkedShutdownSuccess : ActionOutcome.EditSuccess });
            }
            return View(recording);
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