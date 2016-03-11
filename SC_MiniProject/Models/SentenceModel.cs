using SC_MiniProject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SC_MiniProject.Models
{
    public class SentenceModel: Scoreboard
    {
        public string visible { get; set; }
        public string original { get; set; }
        public string userSentence { get; set; }

        public SentenceModel() {}

        public SentenceModel(string sentence)
        {
            visible  = (string)sentence.Clone();
            foreach (var c in new string[] { ",", ".", "?" })
                visible = visible.Replace(c, "*");
            original = sentence;
            userSentence = "";
        }
    }
}