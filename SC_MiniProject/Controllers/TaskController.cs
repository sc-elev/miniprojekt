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
        private Random rnd = new Random();
        // (1) - ImageRecognitionTask
        // The user is presented with a series of images and is tasked with writing single words matching each of those images.
        public ActionResult ImageRecognitionTask()
        {
            ViewBag.Scoreboard = new Scoreboard();
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

        public ViewResult Delimiters()
        {
            string[] sentences = ConfigFile.Sentences();
            int ix = rnd.Next(0, sentences.Length);
            string sentence = sentences[ix];
            SentenceModel model = new SentenceModel(sentence);
            ViewBag.Scoreboard = new Scoreboard();
            return View(model);
        }


        [HttpPost]
        public ActionResult Delimiters(SentenceModel model)
        {

            if (model.userSentence == model.original)
            {
                ViewBag.Scoreboard = new Scoreboard();
                model.SetCurrentScore(model.GetCurrentScore() + 1);
                return Delimiters();
            }
            else
            {
                return RedirectToAction("BadDelimiter");
            }
        }


        public ViewResult BadDelimiter()
        {
            ViewBag.Scoreboard = new Scoreboard();
            return View("BadDelimiter");
        }


        public ViewResult SaveScore()
        {
            ViewBag.Scoreboard =  new Scoreboard();
            ScoreHolder model = new ScoreHolder();
            model.Score = ViewBag.Scoreboard.GetCurrentScore();
            model.Nickname = "";
            return View(model);
        }


        [HttpPost]
        public ActionResult SaveScore(ScoreHolder model)
        {
            Scoreboard board = new Scoreboard();
            board.AddScore(model.Nickname, model.Score);
            return RedirectToAction("Index", "Home");
        }
    }
}
