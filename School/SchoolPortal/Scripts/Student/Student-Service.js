/// <reference path="../angular.min.js" />
/// <reference path="Student-App.js" />
var studentApp = angular.module('studentApp', []);
serviceUrlbase = 'http://localhost:52683';

studentApp.factory('studentService',['$http', function ($http) {

    return {

        studentDetentions: function (id, dateStr, callback) {

            var url = serviceUrlbase + '/api/detention/' + id+'/' + dateStr;

            $http.get(url).then(function (data) {

                callback(data);
            }, function (data) { });
        },
        calculate: function (id, datestr, callback) {

            $http({
                url: serviceUrlbase +'/api/detention/' + id + '/29-03-2017',
                method: "POST",
            }).then(function (response) { callback(response.data); }, function (err) { alert(err); });
        }

        

    }

}]);
