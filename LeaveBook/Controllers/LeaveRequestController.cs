using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveBook.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaveBook.Controllers
{
    public class LeaveRequestController : Controller
    {
        // POST: LeaveRequest
        public ActionResult ApplyForLeave()
        {
            var model = new LeaveRequestViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult ApplyForLeave(LeaveRequestViewModel model)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}