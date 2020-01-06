'use strict';


ibmsApp.factory('ClaimRecordingsService', function ($http, $rootScope) {

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

    var getAllClaimRecording = function (BusinessUnitID) {
        var params = { "BusinessUnitID": BusinessUnitID, }
        return $http.post($rootScope.serviceURL + 'api/Claim/GetAllClaimRecordingsByBUID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var editClaimRecording = function (ClaimRecordingID) {
        var params = { "ClaimRecordingID": ClaimRecordingID, }
        return $http.post($rootScope.serviceURL + 'api/Claim/GetClaimRecordingByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getDocumentDetails = function () {
        return $http.post($rootScope.serviceURL + 'api/Document/GetAllDocuments', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var saveClaimRecording = function (policyInfoRecList, userID) {
        var params = { "ClaimRecordingVM": policyInfoRecList, "UserID": userID };
        return $http.post($rootScope.serviceURL + 'api/Claim/SaveClaimRecording', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var updateClaimRecording = function (policyInfoRecList, userID) {
        var params = { "PolicyInfoRecList": policyInfoRecList, "UserID": userID };
        return $http.post($rootScope.serviceURL + 'api/Claim/UpdateClaimRecording', params, config).then(function (results) {
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

    var loadQuotationHeaderByID = function (PolicyInfoRecID) {
        var params = { "PolicyInfoRecID": PolicyInfoRecID };
        return $http.post($rootScope.serviceURL + 'api/Policy/GetPolicyInfoRecordingByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
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

   

    var getAvailableYears = function () {
        return $http.post($rootScope.serviceURL + 'api/Claim/GetYear', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var getAllInsClass = function (BUID) {
        var params = {
            "businessUnitID": BUID,
        };
        return $http.post($rootScope.serviceURL + 'api/InsClass/GetAllInsClasses', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var loadClassType = function (InsuranceClassID) {
        var params = {
            "InsuranceClassID":InsuranceClassID
        };
        return $http.post($rootScope.serviceURL + 'api/Claim/GetInsClassType', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var LoadClaimStatus = function () {
     
        return $http.post($rootScope.serviceURL + 'api/Claim/GetClaimStatus', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var loadCurrencies = function () {
        return $http.post($rootScope.serviceURL + 'api/Utility/GetAllCurrencies', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var LoadavailableClaimePaidStatus = function () {

        return $http.post($rootScope.serviceURL + 'api/Claim/GetPaidClaimStatus', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };
   

    return {
        LoadavailableClaimePaidStatus: LoadavailableClaimePaidStatus,
        loadCurrencies:loadCurrencies,
        LoadClaimStatus: LoadClaimStatus,
        loadClassType: loadClassType,
        getAllInsClass: getAllInsClass,
        getAvailableYears: getAvailableYears,
        getAllPolicyInfoRecording: getAllPolicyInfoRecording,
        updateClaimRecording: updateClaimRecording,
        getQuatationHeader: getQuatationHeader,
        getClientByID: getClientByID,
        getClientRequest: getClientRequest,
        getAllPartners: getAllPartners,
        getAllComStructuresHeaders: getAllComStructuresHeaders,
        getAllComStructuresLine: getAllComStructuresLine,
        loadQuotationHeaderByID: loadQuotationHeaderByID,
        getDocumentDetails: getDocumentDetails,
        saveClaimRecording: saveClaimRecording,
        getAllClaimRecording: getAllClaimRecording,
        editClaimRecording: editClaimRecording,
        loadCurrencies: loadCurrencies



    };
});