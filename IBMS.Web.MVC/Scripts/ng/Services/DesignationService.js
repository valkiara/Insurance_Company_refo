'use strict';

ibmsApp.factory('DesignationService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var getAvailableBUDropdown = function () {
        return $http.post($rootScope.serviceURL + 'api/BusinessUnit/GetAllBusinessUnits', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAvailableDesignation = function (BUID) {
        var params = $.param({ "BusinessUnitID": BUID });

        return $http.post($rootScope.serviceURL + 'api/Designation/GetAllDesignationsByBUID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var saveDesignation = function (desObj, cuObj) {
        var params = $.param({ "designationName": desObj.Designation, "businessUnitID": desObj.BusinessUnit, "userID": cuObj.UserID });
        return $http.post($rootScope.serviceURL + 'api/Designation/SaveDesignation', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var updateDES = function (designationID, designation, businessUnitID, UserID) {
        var params = $.param({ "designationID": designationID, "designationName": designation, "businessUnitID": businessUnitID, "userID": UserID });
        return $http.post($rootScope.serviceURL + 'api/Designation/UpdateDesignation', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var deleteDES = function (desID) {
        var params = $.param({ "designationID": desID });
        return $http.post($rootScope.serviceURL + 'api/Designation/DeleteDesignation', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };


    return {
        getAvailableBUDropdown: getAvailableBUDropdown,
        getAvailableDesignation: getAvailableDesignation,
        saveDesignation: saveDesignation,
        updateDES: updateDES,
        deleteDES: deleteDES
    };
});