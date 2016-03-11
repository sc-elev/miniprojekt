using SC_MiniProject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SC_MiniProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var scoreboard = new Scoreboard();
            scoreboard.AddScore("Orvar", 5); //FIXME: conditionalize test data.
            scoreboard.AddScore("Ivar", 12);
            scoreboard.AddScore("Pelle", 6);
            ViewBag.Scoreboard = scoreboard;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
