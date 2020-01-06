'use strict';

ibmsApp.factory('EmployeesService', function ($http, $rootScope) {


    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var getAvailableDesignation = function () {
        return $http.post($rootScope.serviceURL + 'api/Designation/GetAllDesignations', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var GetEmployeesByDesignationID = function (DesignationID) {

        var params = {
            "DesignationID": DesignationID
        };

        return $http.post($rootScope.serviceURL + 'api/Employee/GetEmployeesByDesignationID', params, config).then(function (results) {
            // alert('Hi');
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    return {

        getAvailableDesignation: getAvailableDesignation,
        GetEmployeesByDesignationID: GetEmployeesByDesignationID
    }
});
