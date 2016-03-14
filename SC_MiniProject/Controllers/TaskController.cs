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
        protected ScoreDB scoreDB;

        private Random rnd = new Random();
        // (1) - ImageRecognitionTask
        // The user is presented with a series of images and is tasked with writing single words matching each of those images.
        public ActionResult ImageRecognitionTask()
        {
            ViewBag.Scoreboard = new Scoreboard(scoreDB);
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

            //throw new NotImplementedException("ImageRecognitionQuestions_Read");
            return Json(resultList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ImageRecognitionQuestions_Post(TestResult testResult)
        {
            var Alle = new Scoreboard(scoreDB);
            Alle.SetCurrentScore(Alle.GetCurrentScore() + testResult.Score);

            return View();
	    }

        public ViewResult Delimiters()
        {
            string[] sentences = ConfigFile.Sentences();
            int ix = rnd.Next(0, sentences.Length);
            string sentence = sentences[ix];
            SentenceModel model = new SentenceModel(sentence);
            ViewBag.Scoreboard = new Scoreboard(scoreDB);
            return View(model);
        }


        [HttpPost]
        public ActionResult Delimiters(SentenceModel model)
        {

            if (model.userSentence == model.original)
            {
                ViewBag.Scoreboard = new Scoreboard(scoreDB);
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
            ViewBag.Scoreboard = new Scoreboard(scoreDB);
            return View("BadDelimiter");
        }


        public ViewResult SaveScore()
        {
            ViewBag.Scoreboard = new Scoreboard(scoreDB);
            ScoreHolder model = new ScoreHolder();
            model.Score = ViewBag.Scoreboard.GetCurrentScore();
            model.Nickname = "";
            return View(model);
        }


        [HttpPost]
        public ActionResult SaveScore(ScoreHolder model)
        {
            Scoreboard board = new Scoreboard(scoreDB);
            board.AddScore(model.Nickname, model.Score);
            return RedirectToAction("Index", "Home");
        }


        public ViewResult Questions()
        {
            QuestionModel model = new QuestionModel();
            dynamic[] questions = ConfigFile.Questions();
            int ix = rnd.Next(0, questions.Length);
            model.Question = questions[ix]["Q"];
            model.Answer = questions[ix]["A"];
            return View("Questions", model);
        }


        [HttpPost]
        public RedirectToRouteResult Questions(QuestionModel model)
        {
            if (model.Reply != model.Answer)
                return RedirectToAction("BadQuestionReply");
            var board = new Scoreboard(scoreDB);
            board.SetCurrentScore(board.GetCurrentScore() + 1);
            ViewBag.Scoreboard = board;
            return RedirectToAction("Index", "Home");
        }


        public TaskController()
        {
            scoreDB = new SessionScoreDB();
        }


        public TaskController(ScoreDB db)
        {
            scoreDB = db;
        }
    }
}
