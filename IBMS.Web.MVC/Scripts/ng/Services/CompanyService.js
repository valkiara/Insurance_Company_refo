'use strict';

ibmsApp.factory('CompanyService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var saveCompany = function (companyObj, CuObj) {
        var params = $.param({ "companyName": companyObj.CompanyName, "isActive": companyObj.IsActive, "userID": CuObj.UserID });
        return $http.post($rootScope.serviceURL + 'api/Company/SaveCompany', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAvailableCompany = function () {
        return $http.post($rootScope.serviceURL + 'api/Company/GetAllCompanies', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var updateCompany = function (companyID, companyName, IsActive, UserID) {
        var params = $.param({ "companyID": companyID, "companyName": companyName, "isActive": IsActive, "userID": UserID });
        return $http.post($rootScope.serviceURL + 'api/Company/UpdateCompany', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var deleteCompany = function (companyID) {
        var params = $.param({ "companyID": companyID });
        return $http.post($rootScope.serviceURL + 'api/Company/DeleteCompany', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    return {

        saveCompany: saveCompany,
        getAvailableCompany: getAvailableCompany,
        updateCompany: updateCompany,
        deleteCompany: deleteCompany
    };
});