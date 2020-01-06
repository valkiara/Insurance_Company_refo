'use strict';

ibmsApp.factory('AgentService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': $rootScope.authKey }

    };

    var getAllCompany = function () {
        return $http.post($rootScope.serviceURL + 'api/Company/GetAllCompanies', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var getAllAgent = function () {
        return $http.post($rootScope.serviceURL + 'api/Agent/GetAllAgents', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var saveAgent = function (companyID, agentName, address1, address2, address3, rateValue, agentType, agentNIC, agentBR, userID, agentCode) {
        var params = {
            "companyID": companyID,
            "agentName": agentName,
            "address1": address1,
            "address2": address2,
            "address3": address3,
            "rateValue": rateValue,
            "agentType": agentType,
            "agentNIC": agentNIC,
            "agentBR": agentBR,
            "userID": userID,
            "agentCode": agentCode
        };

        return $http.post($rootScope.serviceURL + 'api/Agent/SaveAgent',
            params, config).then(function (results) {
                return results.data;
            }, function (data) {

            });
    };
    var updateAgent = function (agentID, companyID, agentName, address1, address2, address3, rateValue, userID) {

        var params = {
            "companyID": companyID,
            "agentID": agentID,
            "agentName": agentName,
            "address1": address1,
            "address2": address2,
            "address3": address3,
            "rateValue": rateValue,
            "userID": userID
        };

        return $http.post($rootScope.serviceURL + 'api/Agent/UpdateAgent',
            params, config).then(function (results) {

                return results.data;
            }, function (data) {

            });
    }
    var deleteAgent = function (agentID) {

        var params = {
            "agentID": agentID,
        };

        return $http.post($rootScope.serviceURL + 'api/Agent/DeleteAgent',
            params, config).then(function (results) {
                return results.data;
            }, function (data) {

            });
    };

    return {
        saveAgent: saveAgent,
        getAllCompany: getAllCompany,
        getAllAgent: getAllAgent,
        updateAgent: updateAgent,
        deleteAgent: deleteAgent
    };
});