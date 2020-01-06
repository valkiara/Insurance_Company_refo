'use strict';

ibmsApp.factory('CancelledClientRequestService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var getCancelledRequest = function (fromDate, toDate, type) {
        var params = $.param({ "filterObj": { "fromDate": fromDate, "toDate": toDate, "type": type } });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetCancelledRequest', params, config).then(function (results) {
            return results.data;
        }, function (data) {
        })
    };


    return {
        getCancelledRequest: getCancelledRequest,

    }
});