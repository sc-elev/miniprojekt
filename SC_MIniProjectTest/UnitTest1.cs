using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using JsonConfig;
using SC_MiniProject;
using SC_MiniProject.DAL;
using SC_MiniProject.Controllers;
using System.Web.Mvc;
using SC_MiniProject.Models;


namespace SC_MIniProjectTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestConfig()
        {
            string[] result = ConfigFile.GetFruits();
        }


        [TestMethod]
        public void SentenceCtrlReturnsModel()
        {
            var controller = new TaskController();
            ViewResult result = controller.Delimiters();
            SentenceModel model = (SentenceModel) result.Model;
            Assert.AreEqual("En konstig mening, eller hur?", model.original);
            Assert.AreEqual("En konstig mening* eller hur*", model.visible);
        }


        [TestMethod]
        public void ScoreboardSortsAndLimitValues()
        {
            var db = new TestScoreDB();
            var scoreboard = new Scoreboard(db);
            scoreboard.AddScore("foo", 10);
            scoreboard.AddScore("bar", 20);
            scoreboard.AddScore("baz", 6);
            scoreboard.AddScore("baz1", 6);
            scoreboard.AddScore("baz2", 6);
            scoreboard.AddScore("baz3", 6);
            scoreboard.AddScore("baz4", 6);
            scoreboard.AddScore("baz5", 6);
            scoreboard.AddScore("baz6", 6);
            scoreboard.AddScore("baz7", 6);
            scoreboard.AddScore("baz8", 6);
            scoreboard.AddScore("baz9", 6);

            var holders = scoreboard.GetScores();
            Assert.AreEqual("bar",holders[0].Nickname );
            Assert.AreEqual(10, holders.Count);
        }

        [TestMethod]
        public void ScoreBoardRemovesDups()
        {
            var db = new TestScoreDB();
            var scoreboard = new Scoreboard(db);
            scoreboard.AddScore("foo", 6);
            scoreboard.AddScore("foo", 10);
            Assert.AreEqual(1, scoreboard.GetScores().Count);
            Assert.AreEqual(10, scoreboard.GetScores()[0].Score);
            Assert.AreEqual("foo", scoreboard.GetScores()[0].Nickname);
        }
    }


}
