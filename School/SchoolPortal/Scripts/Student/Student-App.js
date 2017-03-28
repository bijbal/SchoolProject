/// <reference path="Student-App.js" />
/// <reference path="../angular.min.js" />

var controller = studentApp.controller('studentController', function ($scope, studentService) {

    $scope.detentions = {};
    var sId = document.getElementById("studentId").value;
    var date = new Date().getDate();
    $scope.calculate = function () {
        studentService.calculate(sId, date, function (data) {

        });
    };
    
    $scope.detentions = studentService.studentDetentions(sId, date, function (data) {
        $scope.detentions = data.data;

    });
});