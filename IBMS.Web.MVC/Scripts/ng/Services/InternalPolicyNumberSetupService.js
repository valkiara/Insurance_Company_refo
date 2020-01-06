'use strict';

ibmsApp.factory('InternalPolicyNumberSetupService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': $rootScope.authKey }

    };

    var getAvailableBUDropdown = function () {
        return $http.post($rootScope.serviceURL + 'api/BusinessUnit/GetAllBusinessUnits', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var GetAllInternalPolicyNumSetups = function () {
        return $http.post($rootScope.serviceURL + 'api/InternalPolicyNum/GetAllInternalPolicyNumSetups', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var SaveInternalPolicyNumSetup = function (internalPolicyNumber, businessUnitID, userID) {
        var params = {
            "InternalPolicyNumber": internalPolicyNumber,
            "BusinessUnitID": businessUnitID,
            "UserID": userID
        };

        return $http.post($rootScope.serviceURL + 'api/InternalPolicyNum/SaveInternalPolicyNumSetup',
           params, config).then(function (results) {
               return results.data;
           }, function (data) {

           });
    };

    var UpdateInternalPolicyNumSetup = function (internalPolicyNumSetupID, internalPolicyNumber, businessUnitID, userID) {

        var params = {
            "InternalPolicyNumSetupID": internalPolicyNumSetupID,
            "InternalPolicyNumber": internalPolicyNumber,
            "BusinessUnitID": businessUnitID,
            "UserID": userID
        };

        return $http.post($rootScope.serviceURL + 'api/InternalPolicyNum/UpdateInternalPolicyNumSetup',
           params, config).then(function (results) {

               return results.data;
           }, function (data) {

           });
    };

    var DeleteInternalPolicyNumSetup = function (InternalPolicyNumSetupID) {

        var params = {
            "InternalPolicyNumSetupID": InternalPolicyNumSetupID,
        };

        return $http.post($rootScope.serviceURL + 'api/InternalPolicyNum/DeleteInternalPolicyNumSetup',
           params, config).then(function (results) {
               return results.data;
           }, function (data) {

           });
    };

    return {

        DeleteInternalPolicyNumSetup: DeleteInternalPolicyNumSetup,
        UpdateInternalPolicyNumSetup: UpdateInternalPolicyNumSetup,
        SaveInternalPolicyNumSetup: SaveInternalPolicyNumSetup,
        GetAllInternalPolicyNumSetups: GetAllInternalPolicyNumSetups,
        getAvailableBUDropdown: getAvailableBUDropdown

    };
});