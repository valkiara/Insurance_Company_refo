'use strict';

ibmsApp.factory('INSSubClassesService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var getAllInsClass = function () {
        return $http.post($rootScope.serviceURL + 'api/InsClass/GetAllInsClasses', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var GetAllInsSubClassesByInsClass = function (insClassID) {

        var params = {
            "insClassID": insClassID,
        };

        return $http.post($rootScope.serviceURL + 'api/InsClass/GetAllInsSubClassesByInsClass', params, config).then(function (results) {

            return results.data;
        }, function (data) {
            // Handle error here
        });

    };

    return {
        GetAllInsSubClassesByInsClass: GetAllInsSubClassesByInsClass,
        getAllInsClass: getAllInsClass
    };
});