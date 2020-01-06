'use strict';

ibmsApp.factory('InsClassService', function ($http, $rootScope) {
    // alert("fghgf");
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

    var getAllCompany = function () {
        return $http.post($rootScope.serviceURL + 'api/Company/GetAllCompanies', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };
    var getAllInsClass = function (BUID) {
        var params = {
            "businessUnitID": BUID,
        };
        return $http.post($rootScope.serviceURL + 'api/InsClass/GetAllInsClasses', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };
    var saveInsClass = function (businessUnitID,
        insuranceCode,
        description,
        isActive, userID) {

        var params = {
            "businessUnitID": businessUnitID,
            "insuranceCode": insuranceCode,
            "description": description,
            "isActive": isActive,
            "userID": userID
        };

        return $http.post($rootScope.serviceURL + 'api/InsClass/SaveInsClass',
           params, config).then(function (results) {
               return results.data;
           }, function (data) {

           });
    };
    var UpdateInsClass = function (
        insClassID,
        businessUnitID,
        insuranceCode,
        description,
        isActive, userID) {

        var params = {
            "insClassID": insClassID,
            "businessUnitID": businessUnitID,
            "insuranceCode": insuranceCode,
            "description": description,
            "isActive": isActive,
            "userID": userID
        };
        return $http.post($rootScope.serviceURL + 'api/InsClass/UpdateInsClass',
           params, config).then(function (results) {
               return results.data;
           }, function (data) {

           });
    }
    var deleteInsClass = function (insClassID) {

        var params = {
            "insClassID": insClassID,
        };

        return $http.post($rootScope.serviceURL + 'api/InsClass/DeleteInsClass',
           params, config).then(function (results) {

               return results.data;
           }, function (data) {

           });
    };

    var saveInsurance = function (insObj, cuObj) {
        var params = $.param({ "insClassID": insObj.InsuranceClassID, "description": insObj.Description, "isActive": insObj.IsActive, "userID": cuObj.UserID });
        return $http.post($rootScope.serviceURL + 'api/InsClass/SaveInsSubClass', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAvailableInsuranceDropdown = function () {
        return $http.post($rootScope.serviceURL + 'api/InsClass/GetAllInsClasses', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };


    var saveInsurance = function (insObj, cuObj) {
        var params = $.param({ "insClassID": insObj.InsuranceClassID, "description": insObj.Description, "isActive": insObj.IsActive, "userID": cuObj.UserID });
        return $http.post($rootScope.serviceURL + 'api/InsClass/SaveInsSubClass', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    return {
        saveInsClass: saveInsClass,
        getAllCompany: getAllCompany,
        getAllInsClass: getAllInsClass,
        UpdateInsClass: UpdateInsClass,
        deleteInsClass: deleteInsClass,
        getAllBusinessUnits: getAllBusinessUnits, 
        getAvailableInsuranceDropdown: getAvailableInsuranceDropdown,
        saveInsurance: saveInsurance

    };
});