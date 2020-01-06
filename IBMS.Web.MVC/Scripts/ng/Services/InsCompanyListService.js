'use strict';

ibmsApp.factory('InsCompanyListService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': $rootScope.authKey }

    };

    var GetInsuranceCompaniesByBusinessUnitID = function (BusinessUnitID) {

        var params = {
            "businessUnitID": BusinessUnitID
        };

        return $http.post($rootScope.serviceURL + 'api/InsCompany/GetInsuranceCompaniesByBusinessUnitID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    return {
        GetInsuranceCompaniesByBusinessUnitID: GetInsuranceCompaniesByBusinessUnitID
    };
});
