'use strict';

ibmsApp.factory('PartnerMappingService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': $rootScope.authKey }

    };
    var getAllPartnerMappings = function () {
        return $http.post($rootScope.serviceURL + 'api/Partner/GetAllPartnerMappings', null, config).then(function (results) {
            return results.data;
        }, function (data) {
        })
    };

    var savePartnerMappingData = function (partnerName, userID) {
        var params = {
            "PartnerName": partnerName,
            "UserID": userID
        };
        return $http.post($rootScope.serviceURL + 'api/Partner/SavePartnerMapping',
           params, config).then(function (results) {
               return results.data;
           }, function (data) {
               //  alert("Error : " + data);
           });
    };

    var updatePartnerMappingData = function (
           partnerID, partnerName, userID) {
        var params = {
            "PartnerID": partnerID,
            "PartnerName": partnerName,
            "UserID": userID
        };
        return $http.post($rootScope.serviceURL + 'api/Partner/UpdatePartnerMapping',
           params, config).then(function (results) {
               return results.data;
           }, function (data) {
               //  alert("Error : " + data);
           });
    };

    var DeletePartnerMappingData = function (partnerID) {

        var params = { "PartnerID": partnerID };
        return $http.post($rootScope.serviceURL + 'api/Partner/DeletePartnerMapping', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            alert("Error : " + data);
        });
    };

    return {
        savePartnerMappingData: savePartnerMappingData,
        getAllPartnerMappings: getAllPartnerMappings,
        updatePartnerMappingData: updatePartnerMappingData,
        DeletePartnerMappingData: DeletePartnerMappingData,
    };
});