'use strict';

ibmsApp.factory('clientRequestService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var getAvailableInsSubClass = function (insClassID) {
        var params = { "insClassID": insClassID };

        return $http.post($rootScope.serviceURL + 'api/InsClass/GetAllInsSubClassesByInsClass', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            alert(data);

            // Handle error here
        })
    };

    var getAvailableInsSubClassScope = function (insClassID) {
        var params = { "insSubClassID": insClassID };

        return $http.post($rootScope.serviceURL + 'api/CommonInsScope/GetAllCommonInsScopesByInsSubClass', params, config).then(function (results) {
            return results.data;
        }, function (data) {

        })
    };

    var getAllInsClass = function () {
        return $http.post($rootScope.serviceURL + 'api/InsClass/GetAllInsClasses', null, config).then(function (results) {
            return results.data;
        }, function (data) {

            // Handle error here
        });
    };
    var getAllPartners = function () {
        return $http.post($rootScope.serviceURL + 'api/Partner/GetAllPartners', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };


    var getAllClients = function () {

        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetAllClients', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var getAllBusinessUnits = function () {
        return $http.post($rootScope.serviceURL + 'api/BusinessUnit/GetAllBusinessUnits', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAvailableCompany = function () {
        return $http.post($rootScope.serviceURL + 'api/Company/GetAllCompanies', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAvailableCompany = function () {
        return $http.post($rootScope.serviceURL + 'api/Company/GetAllCompanies', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAllClientRequests = function () {
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetAllClientRequests', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var saveClientRequest = function (isClientExist, clientID, clientObj, clientRequestHeaderObj, userID) {
        var params = { "IsClientExist": isClientExist, "ClientID": clientID, "ClientObj": clientObj, "ClientRequestHeaderObj": clientRequestHeaderObj, "UserID": userID };

        return $http.post($rootScope.serviceURL + 'api/ClientRequest/SaveClientRequest', params, config).then(function (results) {
            return results.data;
        }, function (data) {

        })
    };

    var UpdateClientRequest = function (isClientExist, clientID, clientObj, clientRequestHeaderObj, userID) {
        var params = { "IsClientUpdated": false, "ClientID": clientID, "ClientObj": clientObj, "ClientRequestHeaderObj": clientRequestHeaderObj, "UserID": userID };

        return $http.post($rootScope.serviceURL + 'api/ClientRequest/UpdateClientRequest', params, config).then(function (results) {
            return results.data;

        }, function (data) {

        })
    };

    var saveQuatationRequest = function (QuotationHeaderObj) {
        var params = { "QuotationHeaderObj": QuotationHeaderObj, "UserID": 1 };
        // alert(params);
        return $http.post($rootScope.serviceURL + 'api/Quotation/SaveQuotation', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };


    var getAllPartnerMapping = function () {
        return $http.post($rootScope.serviceURL + 'api/Partner/GetAllPartners', null, config)
            .then(function (results) {
                return results.data;
            }, function (data) {

            })
    };

    return {
        getAllBusinessUnits: getAllBusinessUnits,
        getAvailableCompany: getAvailableCompany,
        getAvailableInsSubClass: getAvailableInsSubClass,
        getAllInsClass: getAllInsClass,
        getAllPartners: getAllPartners,
        getAvailableInsSubClassScope: getAvailableInsSubClassScope,
        saveClientRequest: saveClientRequest,
        getAllPartnerMapping: getAllPartnerMapping,
        getAllClients: getAllClients,
        getAllClientRequests: getAllClientRequests,
        saveQuatationRequest: saveQuatationRequest,
        UpdateClientRequest: UpdateClientRequest

    };
});