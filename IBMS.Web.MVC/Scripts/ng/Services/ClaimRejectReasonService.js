'use strict';

ibmsApp.factory('ClaimRejectReasonService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': $rootScope.authKey }

    };

    var GetAllClaimRejectReasons = function () {
        return $http.post($rootScope.serviceURL + 'api/ClaimIssue/GetAllClaimRejectReasons', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var SaveClaimRejectReason = function (claimRejectReason, userID) {
        var params = {
            "ClaimRejectReason": claimRejectReason,
            "UserID": userID
        };

        return $http.post($rootScope.serviceURL + 'api/ClaimIssue/SaveClaimRejectReason',
           params, config).then(function (results) {
               return results.data;
           }, function (data) {

           });
    };


    var UpdateClaimRejectReason = function (claimRejectReasonID, claimRejectReason, userID) {

        var params = {
            "ClaimRejectReasonID": claimRejectReasonID,
            "ClaimRejectReason": claimRejectReason,
            "UserID": userID
        };

        return $http.post($rootScope.serviceURL + 'api/ClaimIssue/UpdateClaimRejectReason',
           params, config).then(function (results) {

               return results.data;
           }, function (data) {

           });
    };

    var DeleteClaimRejectReason = function (ClaimRejectReasonID) {

        var params = {
            "ClaimRejectReasonID": ClaimRejectReasonID
        };

        return $http.post($rootScope.serviceURL + 'api/ClaimIssue/DeleteClaimRejectReason',
           params, config).then(function (results) {
               return results.data;
           }, function (data) {

           });
    };

    return {
        GetAllClaimRejectReasons: GetAllClaimRejectReasons,
        SaveClaimRejectReason: SaveClaimRejectReason,
        UpdateClaimRejectReason: UpdateClaimRejectReason,
        DeleteClaimRejectReason: DeleteClaimRejectReason
    };


});