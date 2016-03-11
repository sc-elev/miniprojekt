using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SC_MiniProject.Models
{
    public class ImageRecognition
    {
        public string ImageUrl { get; set;}
        public string CorrectAnswer { get; set; }
        public bool? AnsweredCorrectly { get; set; }
    }
}