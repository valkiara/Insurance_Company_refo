'use strict';

ibmsApp.factory('ClientListService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var SearchClients = function (BusinessUnitID, HomeCountryID, ResidentCountryID) {

        var params = {
            "businessUnitID": BusinessUnitID,
            "homeCountryID": HomeCountryID,
            "residentCountryID": ResidentCountryID
        };

        return $http.post($rootScope.serviceURL + 'api/ClientRequest/SearchClients', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var GetAllCountries = function () {

        return $http.post($rootScope.serviceURL + 'api/Utility/GetAllCountries', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };
    var GetBupaPremiumClients = function () {
        
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetAllBupaClients', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };
    return {
        SearchClients: SearchClients,
        GetAllCountries: GetAllCountries,
        GetBupaPremiumClients: GetBupaPremiumClients,

    };
});