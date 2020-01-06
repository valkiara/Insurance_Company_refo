'use strict';

loginApp.factory('LoginService', function ($http, $rootScope) {

    //var config = {
    //    headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': 'Basic QWRtaW5JQk1TOmlibXNJcyMyMDE4' }
    //};

    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var getAllBusinessUnits = function () {
        return $http.post($rootScope.serviceURL + 'api/BusinessUnit/GetAllBusinessUnits', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAvailableBUDropdown = function () {
        return $http.post($rootScope.serviceURL + 'api/BusinessUnit/GetAllBusinessUnits', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var validateLoginDetails = function (LoginData) {
        var params = { "loginName": LoginData.UserName, "password": LoginData.Password, "businessUnitID": LoginData.BusinessUnitID };

        return $http.post($rootScope.serviceURL + 'api/Login/AuthenticateUser',
           params, config).then(function (results) {
               return results.data;
           }, function (data) {
           });
    };

    var sendLoginDetails = function (LoginDetails) {
        var params = { "loginData": LoginDetails };

        return $http.post('/Login/ValidateLoginDetails',
           params, config).then(function (results) {
               return results.data;
           }, function (data) {
           });
    };

    return {
        validateLoginDetails: validateLoginDetails,
        sendLoginDetails: sendLoginDetails,
        getAllBusinessUnits: getAllBusinessUnits,
        getAvailableBUDropdown: getAvailableBUDropdown
    };
});