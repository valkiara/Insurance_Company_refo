'use strict';

ibmsApp.factory('ClaimReopenReasonService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': $rootScope.authKey }

    };

    var GetAllClaimReOpenReasons = function () {
        return $http.post($rootScope.serviceURL + 'api/ClaimIssue/GetAllClaimReOpenReasons', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var SaveClaimReOpenReason = function (claimReOpenReason, userID) {
        var params = {
            "ClaimReOpenReason": claimReOpenReason,
            "UserID": userID
        };

        return $http.post($rootScope.serviceURL + 'api/ClaimIssue/SaveClaimReOpenReason',
           params, config).then(function (results) {
               return results.data;
           }, function (data) {

           });
    };

    var UpdateClaimReOpenReason = function (claimReOpenReasonID, claimReOpenReason, userID) {

        var params = {
            "ClaimReOpenReasonID": claimReOpenReasonID,
            "ClaimReOpenReason": claimReOpenReason,
            "UserID": userID
        };

        return $http.post($rootScope.serviceURL + 'api/ClaimIssue/UpdateClaimReOpenReason',
           params, config).then(function (results) {

               return results.data;
           }, function (data) {

           });
    };

    var DeleteClaimReOpenReason = function (ClaimReOpenReasonID) {

        var params = {
            "ClaimReOpenReasonID": ClaimReOpenReasonID
        };

        return $http.post($rootScope.serviceURL + 'api/ClaimIssue/DeleteClaimReOpenReason',
           params, config).then(function (results) {
               return results.data;
           }, function (data) {

           });
    };


    return {

        GetAllClaimReOpenReasons: GetAllClaimReOpenReasons,
        SaveClaimReOpenReason: SaveClaimReOpenReason,
        UpdateClaimReOpenReason: UpdateClaimReOpenReason,
        DeleteClaimReOpenReason: DeleteClaimReOpenReason
    };


});

