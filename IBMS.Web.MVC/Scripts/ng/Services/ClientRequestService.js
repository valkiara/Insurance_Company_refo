'use strict';

ibmsApp.factory('ClientRequestService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var getAllClientRequests = function () {
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetAllClientRequests', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAllClientRequestsByBUID = function (businessUnitID) {
        var params = $.param({ "BusinessUnitID": businessUnitID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetAllClientRequestsByBUID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var loadAgent = function () {
        return $http.post($rootScope.serviceURL + 'api/Agent/GetAllAgents', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var loadIntroducer = function () {
        return $http.post($rootScope.serviceURL + 'api/Introducer/GetAllIntroducers', null, config).then(function (results) {
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

    var getAllClientsByBUID = function (businessUnitID) {
        var params = $.param({ "BusinessUnitID": businessUnitID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetAllClientsByBUID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var getAllCountries = function () {
        return $http.post($rootScope.serviceURL + 'api/Utility/GetAllCountries', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var getAllDistrict = function () {
        return $http.post($rootScope.serviceURL + 'api/Utility/getAllDistrict', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var getClientByID = function (clientID) {
        var params = $.param({ "ClientID": clientID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetClientByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var getAllPartners = function () {
       // return $http.post($rootScope.serviceURL + 'api/Partner/GetAllPartners', null, config).then(function (results) {
        return $http.post($rootScope.serviceURL + 'api/Partner/GetAllPartnerMappings', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var getAllInsSubClassesByBUID = function (businessUnitID) {
        var params = $.param({ "businessUnitID": businessUnitID });
        return $http.post($rootScope.serviceURL + 'api/InsClass/GetAllInsSubClassesByBusinessUnit', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var getAllInsSubClassScope = function (insSubClassID) {
        var params = $.param({ "insSubClassID": insSubClassID });
        return $http.post($rootScope.serviceURL + 'api/CommonInsScope/GetAllCommonInsScopesByInsSubClass', params, config).then(function (results) {
            return results.data;
        }, function (data) {

        })
    };

    var saveClientRequest = function (isClientUpdated, isClientAdded, clientObj, clientRequestHeaderObj, userID) {
        var params = $.param({ "IsClientUpdated": isClientUpdated, "IsClientAdded": isClientAdded, "ClientObj": clientObj, "ClientRequestHeaderObj": clientRequestHeaderObj, "UserID": userID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/SaveClientRequest', params, config).then(function (results) {
            return results.data;
        }, function (data) {

        })
    };

    var saveClient = function (isClientUpdated, isClientAdded, clientObj, userID) {
        var params = $.param({ "IsClientUpdated": isClientUpdated, "IsClientAdded": isClientAdded, "ClientObj": clientObj, "UserID": userID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/SaveClient', params, config).then(function (results) {
            return results.data;
        }, function (data) {

        })
    };

    var getClientRequestByID = function (clientReqHeaderID) {
        var params = $.param({ "ClientRequestHeaderID": clientReqHeaderID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetClientRequestByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {

        })
    };

    var updateClientRequest = function (isClientUpdated, isClientAdded, clientObj, clientRequestHeaderObj, userID) {
        var params = $.param({ "IsClientUpdated": isClientUpdated, "IsClientAdded": isClientAdded, "ClientObj": clientObj, "ClientRequestHeaderObj": clientRequestHeaderObj, "UserID": userID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/UpdateClientRequest', params, config).then(function (results) {
            return results.data;
        }, function (data) {

        })
    };

    var initializeQuotation = function (quotationHeaderObj, userID) {
        var params = $.param({ "QuotationHeaderObj": quotationHeaderObj, "UserID": userID });
        return $http.post($rootScope.serviceURL + 'api/Quotation/SaveQuotation', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var loadExecutive = function () {
        return $http.post($rootScope.serviceURL + 'api/Employee/GetAllEmployees', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var loadTitle = function () {
        return $http.post($rootScope.serviceURL + 'api/Utility/GetAllTitles', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };
    var getClientRequestByID = function (clientReqHeaderID) {
        var params = $.param({ "ClientRequestHeaderID": clientReqHeaderID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetClientRequestByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {

        })
    };
    var getClientByID = function (clientID) {
        var params = $.param({ "ClientID": clientID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetClientByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };
    var getQuotationDetailByID = function (quotationHeaderId) {
        var params = $.param({ "QuotationHeaderId": quotationHeaderId });
        return $http.post($rootScope.serviceURL + 'api/Quotation/GetQuotationDetailById', params, config).then(function (results) {
            return results.data;
        }, function (data) {

        })
    };
    var getReceiveQuotationFileName = function (quotationHeaderId) {
        var params = $.param({ "QuotationHeaderId": quotationHeaderId });
        return $http.post($rootScope.serviceURL + 'api/Quotation/DownloadReceivedDocment', params, config).then(function (results) {
            return results.data;
        }, function (data) {

        })
    };

    return {

        loadTitle:loadTitle,
        loadExecutive:loadExecutive,
        getAllClientRequests: getAllClientRequests,
        getAllClientRequestsByBUID: getAllClientRequestsByBUID,
        getAllClients: getAllClients,
        getAllClientsByBUID: getAllClientsByBUID,
        getAllCountries: getAllCountries,
        getClientByID: getClientByID,
        getAllPartners: getAllPartners,
        getAllInsSubClassesByBUID: getAllInsSubClassesByBUID,
        getAllInsSubClassScope: getAllInsSubClassScope,
        saveClientRequest: saveClientRequest,
        getClientRequestByID: getClientRequestByID,
        updateClientRequest: updateClientRequest,
        initializeQuotation: initializeQuotation,
        loadAgent: loadAgent,
        loadIntroducer: loadIntroducer,
        saveClient: saveClient,
        getAllDistrict: getAllDistrict,
        getClientRequestByID: getClientRequestByID,
        getClientByID: getClientByID,
        getQuotationDetailByID: getQuotationDetailByID,
        getReceiveQuotationFileName: getReceiveQuotationFileName,

    };
});