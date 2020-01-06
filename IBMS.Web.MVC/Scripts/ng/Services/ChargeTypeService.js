'use strict';

ibmsApp.factory('ChargeTypeService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': $rootScope.authKey }

    };

    var GetAllChargeTypes = function () {
        return $http.post($rootScope.serviceURL + 'api/ChargeType/GetAllChargeTypes', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var SaveChargeType = function (chargeTypeName, percentage, userID) {
        var params = {
            "ChargeTypeName": chargeTypeName,
            "Percentage": percentage,
            "UserID": userID
        };

        return $http.post($rootScope.serviceURL + 'api/ChargeType/SaveChargeType',
           params, config).then(function (results) {
               return results.data;
           }, function (data) {

           });
    };

    var UpdateChargeType = function (chargeTypeID, chargeTypeName, percentage, userID) {

        var params = {
            "ChargeTypeID": chargeTypeID,
            "ChargeTypeName": chargeTypeName,
            "Percentage": percentage,
            "UserID": userID
        };

        return $http.post($rootScope.serviceURL + 'api/ChargeType/UpdateChargeType',
           params, config).then(function (results) {

               return results.data;
           }, function (data) {

           });
    };

    var DeleteChargeType = function (ChargeTypeID) {

        var params = {
            "ChargeTypeID": ChargeTypeID,
        };

        return $http.post($rootScope.serviceURL + 'api/ChargeType/DeleteChargeType',
           params, config).then(function (results) {
               return results.data;
           }, function (data) {

           });
    };

    return {
        GetAllChargeTypes: GetAllChargeTypes,
        SaveChargeType: SaveChargeType,
        UpdateChargeType: UpdateChargeType,
        DeleteChargeType: DeleteChargeType
    };
});