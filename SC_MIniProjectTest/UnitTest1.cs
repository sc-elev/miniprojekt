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
    }
}
