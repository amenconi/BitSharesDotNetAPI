using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BitsharesAPI.ViewModels;

namespace BitsharesAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            HomeViewModel model = new HomeViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(HomeViewModel model)
        {
            return View(model);
        }
    }
}
