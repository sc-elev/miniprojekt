using SC_MiniProject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SC_MiniProject.Controllers
{
    public class TaskController : Controller
    {
        // (1) - ImageRecognitionTask
        // The user is presented with a series of images and is tasked with writing single words matching each of those images.
        public ActionResult ImageRecognitionTask()
        {
            ViewBag.NrOfTasks = 5;
            ViewBag.ListOfImageRecognitionTasks = new ImageRecognitionTask().GetAllImageRecognitionQuestions();


            return View();
        }



    }
}