'use strict';


ibmsApp.factory('PolicyInfoRecService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': $rootScope.authKey }

    };

    var getAllPolicyInfoRecording = function () {
        return $http.post($rootScope.serviceURL + 'api/Policy/GetAllPolicyInfoRecordings', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var UpdatePolicyInfoRecording = function (quotationHeaderID, policyInfoRecList, userID) {
        var params = { "QuotationHeaderID": quotationHeaderID, "PolicyInfoRecList": policyInfoRecList, "UserID": userID };
        return $http.post($rootScope.serviceURL + 'api/Policy/UpdatePolicyInformationRecording', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getQuatationHeader = function (quotationHeaderID) {
        var params = { "QuotationHeaderID": quotationHeaderID };
        return $http.post($rootScope.serviceURL + 'api/Quotation/GetQuotationHeaderByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };
    var getClientByID = function (ClientID) {
        var params = { "ClientID": ClientID };
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetClientByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getClientRequest = function (ClientRequestHeaderID) {
        var params = { "ClientRequestHeaderID": ClientRequestHeaderID };
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetClientRequestByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };
    var getAllPartners = function () {
        return $http.post($rootScope.serviceURL + 'api/Partner/GetAllPartners', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var getAllComStructuresHeaders = function () {
        return $http.post($rootScope.serviceURL + 'api/ComStructure/GetAllComStructureHeaders', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };


    var getAllComStructuresLine = function () {
        return $http.post($rootScope.serviceURL + 'api/ComStructure/GetAllComStructureLines', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    return {
        getAllPolicyInfoRecording: getAllPolicyInfoRecording,
        UpdatePolicyInfoRecording: UpdatePolicyInfoRecording,
        getQuatationHeader: getQuatationHeader,
        getClientByID: getClientByID,
        getClientRequest: getClientRequest,
        getAllPartners: getAllPartners,
        getAllComStructuresHeaders: getAllComStructuresHeaders,
        getAllComStructuresLine: getAllComStructuresLine

    };
});