using Infrastructure.Interface;
using Job_Portal.Models;
using JP_Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private Iuser _user;
        private string loginId;

        public HomeController(ILogger<HomeController> logger, Iuser user)
        {
            _logger = logger;
            _user = user;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult HomePage()
        {
            return View();
        }
        public IActionResult Products()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public bool userAuth(string userName, string userPass)
        {
            loginId = userName;
            bool response = _user.login(userName, userPass);
            return response;
        }
        public bool postJobAd(string jobTitle, string nofvacancies, string starttime,string endtime, string jobType,string jobLevel,string description)
        {
            bool response = _user.postJobAd(jobTitle,nofvacancies,starttime,endtime,jobType,jobLevel,description);
            return response;
        }
        public List<job_entity> getActiveJobAds()
        {
            return  _user.getActiveJobAds();
        }
        public List<job_entity> getJobAdsHistory()
        {
            return  _user.getJobAdsHistory();
        }
        public bool deActivateAd(string id)
        {
            return _user.deActivateAd(id);
        }
    }
}
