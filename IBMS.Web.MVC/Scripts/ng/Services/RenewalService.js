'use strict';

ibmsApp.factory('RenewalService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var getExpiryClientListByDate = function (fromDate, toDate, type) {
        var params = $.param({ "filterObj": { "fromDate": fromDate, "toDate": toDate, "type": type } });
        return $http.post($rootScope.serviceURL + 'api/Renewal/GetClientExpireInfo', params, config).then(function (results) {
            return results.data;
        }, function (data) {
        })
    };


    return {
        getExpiryClientListByDate: getExpiryClientListByDate,

    }
});