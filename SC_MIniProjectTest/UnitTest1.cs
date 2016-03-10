using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using JsonConfig;
using SC_MiniProject;
using SC_MiniProject.DAL;

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
    }
}
