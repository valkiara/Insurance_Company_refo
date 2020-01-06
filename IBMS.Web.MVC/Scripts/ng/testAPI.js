'use strict';
var testAPIApp = angular.module('IBMSAPP', []);

testAPIApp.factory('AuthService', function ($http) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
    };
    var getCurrentUser = function () {
        return $http.post('/Login/CheckAuthentication', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    return {
        getCurrentUser: getCurrentUser
    };
});



/*
testAPIApp.controller('LoginController', function ($scope, LoginService) {
    $scope.message = "";
    alert("jj");
    $scope.init = function () {
        //TO DO
    };
    $scope.getAllTestData = function () {
        TestAPIService.getAllTestData().then(function (results) {
            alert(angular.toJson(results));
        });
    };
});

testAPIApp.factory('TestAPIService', function ($http) {
    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
    };

    var getAllTestData = function () {
        return $http.get('/api/Test/GetAllTestData', config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    return {
        getAllTestData: getAllTestData
    };

    this.SaveEmployee = function (Employee) {
        var request = $http({
            method: "post",
            url: "/api/Test/GetAllTestData",
            data: Employee
        });
        return request;
    }





});*/