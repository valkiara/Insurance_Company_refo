'use strict';

ibmsApp.factory('CauseOfLossService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': $rootScope.authKey }

    };

    var GetAllCauseOfLosses = function () {
        return $http.post($rootScope.serviceURL + 'api/ClaimIssue/GetAllCauseOfLosses', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var SaveCauseOfLoss = function (causeOfLoss, insSubClassID, userID) {
        var params = {
            "CauseOfLoss": causeOfLoss,
            "InsSubClassID": insSubClassID,
            "UserID": userID
        };

        return $http.post($rootScope.serviceURL + 'api/ClaimIssue/SaveCauseOfLoss',
           params, config).then(function (results) {
               return results.data;
           }, function (data) {

           });
    };

    var UpdateCauseOfLoss = function (causeOfLossID, causeOfLoss, insSubClassID, userID) {

        var params = {
            "CauseOfLossID": causeOfLossID,
            "CauseOfLoss": causeOfLoss,
            "InsSubClassID": insSubClassID,
            "UserID": userID
        };

        return $http.post($rootScope.serviceURL + 'api/ClaimIssue/UpdateCauseOfLoss',
           params, config).then(function (results) {
               return results.data;
           }, function (data) {

           });
    };

    var DeleteCauseOfLoss = function (CauseOfLossID) {

        var params = {
            "CauseOfLossID": CauseOfLossID,
        };

        return $http.post($rootScope.serviceURL + 'api/ClaimIssue/DeleteCauseOfLoss',
           params, config).then(function (results) {
               return results.data;
           }, function (data) {

           });
    };

    var getAllInsClass = function () {
        return $http.post($rootScope.serviceURL + 'api/InsClass/GetAllInsClasses', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var getAvailableInsSubClass = function (insClassID) {
        var params = { "insClassID": insClassID };
        return $http.post($rootScope.serviceURL + 'api/InsClass/GetAllInsSubClassesByInsClass', params, config).then(function (results) {
            return results.data;
            //alert("login"+angular.toJson(results));
        }, function (data) {
            // Handle error here
        })
    };

    return {
        GetAllCauseOfLosses: GetAllCauseOfLosses,
        SaveCauseOfLoss: SaveCauseOfLoss,
        UpdateCauseOfLoss: UpdateCauseOfLoss,
        DeleteCauseOfLoss: DeleteCauseOfLoss,
        getAllInsClass: getAllInsClass,
        getAvailableInsSubClass: getAvailableInsSubClass

    };

});