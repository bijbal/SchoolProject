/// <reference path="Student-App.js" />
/// <reference path="../angular.min.js" />

var controller = studentApp.controller('studentController', function ($scope, studentService) {

    $scope.detentions = {};
    $scope.message = "";
    var sId = document.getElementById("studentId").value;
    var date = new Date().getDate();
    $scope.calculate = function () {
        studentService.calculate(sId, date, function (data) {
            var total = 0;
            if ($scope.detentions != null) {
                for (var i = 0; i < $scope.detentions.length; i++) {
                    total += $scope.detentions[i].PunishmentTime;
                }
            }
            if (total >=480) // testing 
                alert('Exceeded Detention Limit 0Inform Parents');
            $scope.message = "Total Detentions -" + (Math.round(total/60)) + " hours"
        });
    };
    
    $scope.detentions = studentService.studentDetentions(sId, date, function (data) {
        $scope.detentions = data.data;

    });
});