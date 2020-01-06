'use strict';

ibmsApp.factory('CommissionPercentagesService', function ($http, $rootScope) {


    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var GetAllComStructureHeadersByBUID = function (businessUnitID) {

        var params = {
            "BusinessUnitID": businessUnitID
        };

        return $http.post($rootScope.serviceURL + 'api/ComStructure/GetAllComStructureHeadersByBUID', params, config).then(function (results) {
            // alert('Hi');
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var GetAllComStructureLinesByComStructureHeaderID = function (ComStructureHeaderID) {

        var params = {
            "ComStructureHeaderID": ComStructureHeaderID
        };

        return $http.post($rootScope.serviceURL + 'api/ComStructure/GetAllComStructureLinesByComStructureHeaderID', params, config).then(function (results) {
            // alert('Hi');
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    return {
        GetAllComStructureHeadersByBUID: GetAllComStructureHeadersByBUID,
        GetAllComStructureLinesByComStructureHeaderID: GetAllComStructureLinesByComStructureHeaderID
    };


});