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


});

