using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LeaveBook.Models;
using LeaveBook.Repositories;
using LeaveBook.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace LeaveBook.Controllers
{
    public class LeaveRequestController : Controller
    {
        private readonly ILogger<LeaveRequestController> _logger;
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepo;
        public LeaveRequestController(ILogger<LeaveRequestController> logger, IMapper mapper, ILeaveTypeRepository leaveTypeRepo)
        {
            _logger = logger;
            _mapper = mapper;
            _leaveTypeRepo = leaveTypeRepo;
        }
        // POST: LeaveRequest
        public async Task<ActionResult> ApplyForLeave()
        {
            LeaveRequestViewModel leaveRequestViewModel = new LeaveRequestViewModel();
            List<LeaveType> leaveTypes = await _leaveTypeRepo.GetAllLeaveTypes();
            var leaveTypeViewModel = _mapper.Map<List<LeaveType>, List<LeaveTypesVM>>(leaveTypes);
            leaveRequestViewModel.LeaveTypes = leaveTypeViewModel;
            return View(leaveRequestViewModel);
        }

        [HttpPost]
        public ActionResult ApplyForLeave(LeaveRequestViewModel lrMmodel)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}