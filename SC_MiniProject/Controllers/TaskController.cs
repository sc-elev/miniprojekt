using SC_MiniProject.DAL;
using SC_MiniProject.Models;
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

            return View();
        }

        public ActionResult ImageRecognitionQuestions_Read()
        {
            // get list of all predefined ImageRecognitionQuestions.
            var sourceList = new ImageRecognitionTask().GetAllImageRecognitionQuestions();

            // Get random list of 5 ImageRecognitionQuestions, in random order. 
            var resultList = new List<ImageRecognition>();
            var rnd = new Random();

            for (var i = 1; i < 6; i++)
            {
                var img = rnd.Next(0, sourceList.Count);
                var selectedItem = sourceList[img];
                
                var item = new ImageRecognition();
                    item.ImageUrl = selectedItem.ImageUrl;
                    item.CorrectAnswer = selectedItem.CorrectAnswer;

                resultList.Add(item);
                sourceList.Remove(selectedItem);
            }

            return Json(resultList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ImageRecognitionQuestions_Post(TestResult testResult)
        {
            Session["Points"] = testResult.Score;
            return View();
        }

    }
}