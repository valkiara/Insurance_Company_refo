'use strict';

ibmsApp.factory('PartnerService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': $rootScope.authKey }

    };
    var getAllPartners = function () {

        return $http.post($rootScope.serviceURL + '/api/Partner/GetAllPartners', null, config).then(function (results) {
            return results.data;
        }, function (data) {

        })
    };

    var savePartnerData = function (partnerName, userID) {
        var params = {
            "PartnerName": partnerName,
            "UserID": userID
        };
        return $http.post($rootScope.serviceURL + 'api/Partner/SavePartner',
           params, config).then(function (results) {
               return results.data;
           }, function (data) {
           });
    };

    var UpdatePartnerData = function (
           partnerID, partnerName) {
        var params = {
            "PartnerID": partnerID,
            "PartnerName": partnerName,
            "UserID": 1
        };
        return $http.post($rootScope.serviceURL + 'api/Partner/UpdatePartner',
           params, config).then(function (results) {
               return results.data;
           }, function (data) {
           });
    };

    var DeletePartnerData = function (partnerID) {

        var params = { "PartnerID": partnerID };
        return $http.post($rootScope.serviceURL + 'api/Partner/DeletePartner', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            alert("Error : " + data);
        });
    };

    return {
        savePartnerData: savePartnerData,
        getAllPartners: getAllPartners,
        UpdatePartnerData: UpdatePartnerData,
        DeletePartnerData: DeletePartnerData,
    };
});