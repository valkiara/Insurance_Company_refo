'use strict';

ibmsApp.factory('InsSubClassService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var getAvailableInsuranceDropdown = function () {
        return $http.post($rootScope.serviceURL + 'api/InsClass/GetAllInsClasses', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAvailableInsSubClass = function (BUID) {
        var params = $.param({ "businessUnitID": BUID });

        return $http.post($rootScope.serviceURL + 'api/InsClass/GetAllInsSubClassesByBusinessUnit', params, config).then(function (results) {
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

    var updateINS = function (insSubClassID, insClassID, description, IsActive, UserID) {
        var params = $.param({ "insSubClassID": insSubClassID, "insClassID": insClassID, "description": description, "isActive": IsActive, "userID": UserID });
        return $http.post($rootScope.serviceURL + 'api/InsClass/UpdateInsSubClass', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var deleteINS = function (InsID) {
        var params = $.param({ "insSubClassID": InsID });
        return $http.post($rootScope.serviceURL + 'api/InsClass/DeleteInsSubClass', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    return {
        getAvailableInsuranceDropdown: getAvailableInsuranceDropdown,
        getAvailableInsSubClass: getAvailableInsSubClass,
        saveInsurance: saveInsurance,
        updateINS: updateINS,
        deleteINS: deleteINS
    };
});