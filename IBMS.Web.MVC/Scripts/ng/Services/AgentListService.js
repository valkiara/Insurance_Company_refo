'use strict';

ibmsApp.factory('AgentListService', function ($http, $rootScope) {


    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': $rootScope.authKey }

    };

    var GetAgentsByCompanyID = function (CompanyID) {

        var params = {
            "companyID": CompanyID
        };

        return $http.post($rootScope.serviceURL + 'api/Agent/GetAgentsByCompanyID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };
    return {
        GetAgentsByCompanyID: GetAgentsByCompanyID
    };

});