var myApp = angular.module('SC_MiniProjectModule');

myApp.controller('TaskController', function ($scope, TaskService, $http, $location) {
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
            //var req = {
            //    method: 'POST',
            //    url: '/Task/ImageRecognitionQuestions_Post',
            //    headers: {
            //        'Content-Type': undefined
            //    },
            //    data: { id: $scope.QuestionsAnsweredCorrectly }
            //}

            ////$http(req).then(function () { console.log("yes.."); }, function () { console.log("No.."); });

            //$http.post("Task/ImageRecognitionQuestions_Post", $scope.QuestionsAnsweredCorrectly).success(function (data) {
            //    Alert(ok)
            //})

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
            console.log("yes1.")
                //function (data) {
                //    $window.location.href = "www.dn.se";
                //}
            
               

            
            )
            .error(console.log("no1."));

            $window.location.href = "www.dn.se";

            //$http.post('/Task/ImageRecognitionQuestions_Post', data, config)
            //.success(function (data, status, headers, config) {
            //    $scope.PostDataResponse = data;
            //})
            //.error(function (data, status, header, config) {
            //    $scope.ResponseDetails = "Data: " + data +
            //        "<hr />status: " + status +
            //        "<hr />headers: " + header +
            //        "<hr />config: " + config;
            //});
        }

        //console.log("clicked next..");
    }



    console.log("Ran TaskController...");
});
