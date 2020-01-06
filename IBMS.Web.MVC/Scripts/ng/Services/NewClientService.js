'use strict';

ibmsApp.factory('NewClientService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var getNewClient = function (fromDate, toDate, type) {
        var params = $.param({ "filterObj": { "fromDate": fromDate, "toDate": toDate, "type": type } });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetNewClientList', params, config).then(function (results) {
            return results.data;
        }, function (data) {
        })
    };


    return {
        getNewClient: getNewClient,

    }
});