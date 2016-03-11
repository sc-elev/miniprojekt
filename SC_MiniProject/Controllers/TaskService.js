var myApp = angular.module('SC_MiniProjectModule');

myApp.service('TaskService', function ($http) {

    this.getAllImageRecognitionQuestions = function () {
        console.log("Alle1");
        return $http.get("/Task/ImageRecognitionQuestions_Read")
                .then(function (data) {
                    console.log(data);
                    return data;
                })
    }

    //this.postImageRecognitionQuestionResult = function (QuestionsAnsweredCorrectly) {
    //    console.log("Alle2");
    //    $http.get("/Task/ImageRecognitionQuestions_Post/" + QuestionsAnsweredCorrectly)
    //            .then(function (data) {
    //                console.log("Post");
    //                //return data;
    //            })
    //}

});

