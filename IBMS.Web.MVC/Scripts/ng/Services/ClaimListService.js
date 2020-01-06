'use strict';

ibmsApp.factory('ClaimListService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var getClaimListByDate = function (fromDate, toDate, type) {
        var params = $.param({ "filterObj": { "fromDate": fromDate, "toDate": toDate, "type": type } });
        return $http.post($rootScope.serviceURL + 'api/Claim/GetClaimListByDate', params, config).then(function (results) {
            return results.data;
        }, function (data) {
        })
    };


    return {
        getClaimListByDate: getClaimListByDate,

    }
});