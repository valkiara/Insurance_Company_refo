'use strict';

ibmsApp.factory('INSClassesService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': $rootScope.authKey }

    };

    var GetInsClassesByBusinessUnitID = function (BusinessUnitID) {

        var params = {
            "businessUnitID": BusinessUnitID
        };

        return $http.post($rootScope.serviceURL + 'api/InsClass/GetInsClassesByBusinessUnitID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };
   
    return {
        GetInsClassesByBusinessUnitID: GetInsClassesByBusinessUnitID
    };
});