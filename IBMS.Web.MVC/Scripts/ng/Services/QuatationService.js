'use strict';

ibmsApp.factory('QuatationService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': $rootScope.authKey }

    };

    var getAvailableInsSubClass = function (insClassID) {

        var params = { "insClassID": insClassID };

        return $http.post($rootScope.serviceURL + 'api/InsClass/GetAllInsSubClassesByInsClass', params, config).then(function (results) {
            return results.data;
        }, function (data) {

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

    var getAllInsuranceCompanny = function () {
        return $http.post($rootScope.serviceURL + 'api/InsCompany/GetAllInsuranceCompanies', null, config)
            .then(function (results) {
                return results.data;

            }, function (data) {

            })
    };

    var getAllQuotationHeaders = function () {
        return $http.post($rootScope.serviceURL + 'api/Quotation/GetAllQuotationHeaders', null, config)
            .then(function (results) {

                return results.data;

            }, function (data) {

            })
    };

    var saveQuatationRequest = function (QuotationHeaderObj) {
        var params = { "QuotationHeaderObj": QuotationHeaderObj, "UserID": 1 };

        return $http.post($rootScope.serviceURL + 'api/Quotation/SaveQuotation', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var saveTCN = function (CoverNoteObj) {
        var params = { "CoverNoteObj": CoverNoteObj, "UserID": 1 };

        return $http.post($rootScope.serviceURL + 'api/CoverNote/SaveCoverNote', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var updateQuotation = function (QuotationHeader) {

        var params = { "QuotationHeaderObj": QuotationHeader, "UserID": 1 };

        return $http.post($rootScope.serviceURL + 'api/Quotation/UpdateQuotation', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })

    };

    var savePolicyInfoRecording = function (quotationHeaderID, policyInfoRecList, userID) {
        var params = { "QuotationHeaderID": 1, "PolicyInfoRecList": policyInfoRecList, "UserID": userID };
        return $http.post($rootScope.serviceURL + 'api/Policy/SavePolicyInformationRecording', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAllCoverNotes = function () {

        return $http.post($rootScope.serviceURL + 'api/CoverNote/GetAllCoverNotes', null, config)
            .then(function (results) {

                return results.data;

            }, function (data) {

            })
    };

    var getAllPartnerMapping = function () {

        return $http.post($rootScope.serviceURL + 'api/Partner/GetAllPartners', null, config)
            .then(function (results) {
                return results.data;

            }, function (data) {

            })
    };

    var UpdateQuatationStatus = function (QuotationHeaderID, QuotationStatusCode, UserID) {
        var params = { "QuotationHeaderID": QuotationHeaderID, "QuotationStatusCode": QuotationStatusCode, "UserID": UserID };

        return $http.post($rootScope.serviceURL + 'api/Quotation/UpdateQuotationStatus', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var loadTransactionTypeDetails = function () {
        return $http.post($rootScope.serviceURL + 'api/TransactionType/GetAllTransactionTypes', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    return {
        getAllBusinessUnits: getAllBusinessUnits,
        getAvailableCompany: getAvailableCompany,
        getAvailableInsSubClass: getAvailableInsSubClass,
        getAllInsClass: getAllInsClass,
        getAllPartners: getAllPartners,
        getAvailableInsSubClassScope: getAvailableInsSubClassScope,
        saveQuatationRequest: saveQuatationRequest,
        updateQuotation: updateQuotation,
        getAllPartnerMapping: getAllPartnerMapping,
        getAllClients: getAllClients,
        getAllClientRequests: getAllClientRequests,
        getAllInsuranceCompanny: getAllInsuranceCompanny,
        getAllQuotationHeaders: getAllQuotationHeaders,
        saveTCN: saveTCN,
        getAllCoverNotes: getAllCoverNotes,
        savePolicyInfoRecording: savePolicyInfoRecording,
        UpdateQuatationStatus: UpdateQuatationStatus,
        loadTransactionTypeDetails: loadTransactionTypeDetails

    };
});