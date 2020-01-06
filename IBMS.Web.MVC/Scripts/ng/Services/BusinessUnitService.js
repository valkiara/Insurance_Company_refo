'use strict';

ibmsApp.factory('BusinessUnitService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var getAvailableCompanyDropdown = function () {
        return $http.post($rootScope.serviceURL + 'api/Company/GetAllCompanies', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAvailableBusinessUnit = function () {
        return $http.post($rootScope.serviceURL + 'api/BusinessUnit/GetAllBusinessUnits', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };
    var saveBusinessUnit = function (buObj, cuObj) {
        var params = $.param({ "businessUnit": buObj.BusinessUnit, "companyID": cuObj.CompanyID, "isActive": buObj.IsActive, "userID": cuObj.UserID });
        return $http.post($rootScope.serviceURL + 'api/BusinessUnit/SaveBusinessUnit', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };
    var updateBU = function (businessUnitID, businessUnit, ComapnyID, IsActive, userID) {
        var params = $.param({ "businessUnitID": businessUnitID, "businessUnit": businessUnit, "companyID": ComapnyID, "isActive": IsActive, "userID": userID });
        return $http.post($rootScope.serviceURL + 'api/BusinessUnit/UpdateBusinessUnit', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };
    var deleteBU = function (buID) {
        var params = $.param({ "businessUnitID": buID });
        return $http.post($rootScope.serviceURL + 'api/BusinessUnit/DeleteBusinessUnit', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };


    return {
        getAvailableCompanyDropdown: getAvailableCompanyDropdown,
        saveBusinessUnit: saveBusinessUnit,
        getAvailableBusinessUnit: getAvailableBusinessUnit,
        updateBU: updateBU,
        deleteBU: deleteBU
    };
});