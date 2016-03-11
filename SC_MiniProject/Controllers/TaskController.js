var myApp = angular.module('SC_MiniProjectModule');

myApp.controller('TaskController', function ($scope, TaskService, $http, $window) {
    console.log("is running TaskController...");

    $scope.AnswerButtonVisible = true;
    $scope.CurrentQuestion = 0;
    $scope.CorrectAnswer = "";
    $scope.Correct = "";
    $scope.UserAnswer = "";
    $scope.NextButtonTitle = "Nästa";
    $scope.QuestionsAnsweredCorrectly = 0;

    var ques = TaskService.getAllImageRecognitionQuestions();
    ques.then(function (data) {
        $scope.Questions = data.data;
    })

    $scope.onImageRecognitionAnswer_click = function () {
        $scope.AnswerButtonVisible = false;
        var answer = $scope.Questions[$scope.CurrentQuestion].CorrectAnswer;

        if ($scope.UserAnswer.toLowerCase() != answer.toLowerCase()) {
            $scope.CorrectAnswer = "Rätt svar var " + answer + ".";
        } else {
            $scope.Correct = "Rätt svar. Bra gjort.";
            $scope.CorrectAnswer = "";
            $scope.QuestionsAnsweredCorrectly++;
        }

        console.log("clicked answer..");
    }

    $scope.onImageRecognitionNext_click = function () {
        $scope.CurrentQuestion++;
        $scope.AnswerButtonVisible = true;
        $scope.CorrectAnswer = "";
        $scope.Answer = "";
        $scope.Correct = "";

        if ($scope.CurrentQuestion == 4) {
            $scope.NextButtonTitle = "Avsluta";
        }

        if ($scope.CurrentQuestion == 5) {

            var data = $.param({
                TestName: "Alle", 
                Score: $scope.QuestionsAnsweredCorrectly
            });

            var config = {
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8;'
                }
            }

            $http.post('/Task/ImageRecognitionQuestions_Post', data, config)
            .success(
                $window.location.href = "/Home/Index"
            )
            .error(console.log("no1."));

        }
    }



    console.log("Ran TaskController...");
});
