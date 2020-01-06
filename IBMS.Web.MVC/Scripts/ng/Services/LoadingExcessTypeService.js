'use strict';

ibmsApp.factory('LoadingExcessTypeService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': $rootScope.authKey }

    };

    var GetAllLoadingTypes = function () {
        return $http.post($rootScope.serviceURL + 'api/LoadingType/GetAllLoadingTypes', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var SaveLoadingType = function (description, userID) {
        var params = {
            "Description": description,
            "UserID": userID
        };

        return $http.post($rootScope.serviceURL + 'api/LoadingType/SaveLoadingType',
           params, config).then(function (results) {
               return results.data;
           }, function (data) {

           });
    };


    var UpdateLoadingType = function (loadingTypeID, description, userID) {

        var params = {
            "LoadingTypeID": loadingTypeID,
            "Description": description,
            "UserID": userID
        };

        return $http.post($rootScope.serviceURL + 'api/LoadingType/UpdateLoadingType',
           params, config).then(function (results) {

               return results.data;
           }, function (data) {

           });
    };

    var DeleteLoadingType = function (LoadingTypeID) {

        var params = {
            "LoadingTypeID": LoadingTypeID
        };

        return $http.post($rootScope.serviceURL + 'api/LoadingType/DeleteLoadingType',
           params, config).then(function (results) {
               return results.data;
           }, function (data) {

           });
    };

    return {
        GetAllLoadingTypes: GetAllLoadingTypes,
        SaveLoadingType: SaveLoadingType,
        UpdateLoadingType: UpdateLoadingType,
        DeleteLoadingType: DeleteLoadingType
    };


});