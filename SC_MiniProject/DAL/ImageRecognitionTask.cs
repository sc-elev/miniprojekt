using SC_MiniProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SC_MiniProject.DAL
{
    public class ImageRecognitionTask
    {
        public List<ImageRecognition> GetAllImageRecognitionQuestions()
        {
            return new List<ImageRecognition>() {
                 new ImageRecognition {  CorrectAnswer="Katt", ImageUrl="/Images/Katt.png", SelectedForTest= false},
                 new ImageRecognition {  CorrectAnswer="Bil", ImageUrl="/Images/Bil.png", SelectedForTest= false},
                 new ImageRecognition {  CorrectAnswer="Hatt", ImageUrl="/Images/Hatt.png", SelectedForTest= false},
                 new ImageRecognition {  CorrectAnswer="Päron", ImageUrl="/Images/Paron.png", SelectedForTest= false},
                 new ImageRecognition {  CorrectAnswer="Stol", ImageUrl="/Images/Stol.jpg", SelectedForTest= false},
                 new ImageRecognition {  CorrectAnswer="Äpple", ImageUrl="/Images/Apple.jpg", SelectedForTest= false}
            };
        }

    }
}