'use strict';


testAPIApp.factory('ClaimRecordingService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': $rootScope.authKey }

    };

    var getAllPolicyInfoRecording = function () {
        return $http.post($rootScope.serviceURL + '/api/Policy/GetAllPolicyInfoRecordings', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var UpdatePolicyInfoRecording = function (quotationHeaderID, policyInfoRecList, userID) {
        var params = { "QuotationHeaderID": 1, "PolicyInfoRecList": policyInfoRecList, "UserID": userID };
        return $http.post($rootScope.serviceURL + '/api/Policy/UpdatePolicyInformationRecording', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getQuatationHeader = function (quotationHeaderID) {
        var params = { "QuotationHeaderID": quotationHeaderID };
        return $http.post($rootScope.serviceURL + '/api/Quotation/GetQuotationHeaderByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getClientByID = function (ClientID) {
        var params = { "ClientID": ClientID };
        return $http.post($rootScope.serviceURL + '/api/ClientRequest/GetClientByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getClientRequest = function (ClientRequestHeaderID) {
        var params = { "ClientRequestHeaderID": ClientRequestHeaderID };
        return $http.post($rootScope.serviceURL + '/api/ClientRequest/GetClientRequestByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAllPartners = function () {
        return $http.post($rootScope.serviceURL + '/api/Partner/GetAllPartners', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var getAllComStructuresHeaders = function () {
        return $http.post($rootScope.serviceURL + '/api/ComStructure/GetAllComStructureHeaders', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };


    var getAllComStructuresLine = function () {
        return $http.post($rootScope.serviceURL + '/api/ComStructure/GetAllComStructureLines', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var getAllPolicyRenewal = function (PolicyInfoRecID) {
        var params = { "PolicyInfoRecID": PolicyInfoRecID };
        return $http.post($rootScope.serviceURL + '/api/Policy/GetPolicyRenewalHistoriesByPolicyInfoRecID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var savePolicyRenewal = function (PolicyRenewalHistoryVM, userID) {
        var params = { "PolicyRenewalHistoryVM": PolicyRenewalHistoryVM, "UserID": userID };
        return $http.post($rootScope.serviceURL + '/api/Policy/SavePolicyRenewalHistory', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var saveClaimRecording = function (ClaimRecordingVM, userID) {
        var params = { "ClaimRecordingVM": ClaimRecordingVM, "UserID": userID };
        //  alert(angular.toJson(params));
        return $http.post($rootScope.serviceURL + '/api/Claim/SaveClaimRecording', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var UpdateClaimRecording = function (ClaimRecordingVM, userID) {
        var params = { "ClaimRecordingVM": ClaimRecordingVM, "UserID": userID };
        //  alert(angular.toJson(params));
        return $http.post($rootScope.serviceURL + '/api/Claim/UpdateClaimRecording', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var getClaimHistory = function (PolicyInfoRecID) {
        var params = { "PolicyInfoRecID": PolicyInfoRecID };
        return $http.post($rootScope.serviceURL + '/api/Claim/GetAllClaimRecordings', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var getAllDocument = function () {

        return $http.post($rootScope.serviceURL + '/api/Document/GetAllDocuments', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    /************************claim payment******************************/
    var saveClaimPayment = function (claimHeader, UserID) {
        // var params = $.param({ "ClaimAmount": claimHeader.claimAmount, "Notes": claimHeader.notes, "chequeNo": claimHeader.chequeNo, "payAmount": claimHeader.payAmount, "tpay": claimHeader.tpay, "paymentDate": claimHeader.paymentDate, "paymentType": claimHeader.paymentType, "UserID": 1 });
        //var claimPaymentMethodList = [{ "ChequeNo": "12345", "PaymentTypeID": 1, "PaidAmount": 2000, "PaidDate": "02/20/2018", "IsFinal": false }, { "ChequeNo": "12346", "PaymentTypeID": 1, "PaidAmount": 2000, "PaidDate": "02/22/2018", "IsFinal": true }];
        // var claimPayment = { "ClaimRecordingID": 2, "ClaimAmount": 4000, "Notes": "Test Note", "ClaimPaymentMethodDetails":claimPaymentMethodList };

        var params = $.param({ "ClaimPaymentVM": claimHeader, "UserID": 1 });
        // alert(params);
        return $http.post($rootScope.serviceURL + 'api/Claim/SaveClaimPayment', params, config).then(function (results) {
            alert(angular.toJson(results));
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAllClaimPaymentsByClaimID = function (ClaimRecordingID) {
        var params = { "ClaimPaymentID": ClaimRecordingID };
        return $http.post($rootScope.serviceURL + 'api/Claim/GetAllClaimPayments', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var updateClaimPayment = function (claimHeader, UserID) {
        var params = $.param({ "ClaimPaymentVM": claimHeader, "UserID": 1 });
        alert(params);
        return $http.post($rootScope.serviceURL + 'api/Claim/UpdateClaimPayment', params, config).then(function (results) {
            alert(angular.toJson(results));
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var LoadInsClassType = function (InsuranceClassID) {
        var params = { "InsuranceClassID": InsuranceClassID };
        return $http.post($rootScope.serviceURL + '/api/Claim/LoadInsClassType', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };
    /*********************end of claim Payment**************************/
  
    

    return {
        LoadInsClassType: LoadInsClassType,
        getAllPolicyInfoRecording: getAllPolicyInfoRecording,
        UpdatePolicyInfoRecording: UpdatePolicyInfoRecording,
        getQuatationHeader: getQuatationHeader,
        getClientByID: getClientByID,
        getClientRequest: getClientRequest,
        getAllPartners: getAllPartners,
        getAllComStructuresHeaders: getAllComStructuresHeaders,
        getAllComStructuresLine: getAllComStructuresLine,
        getAllPolicyRenewal: getAllPolicyRenewal,
        savePolicyRenewal: savePolicyRenewal,
        getClaimHistory: getClaimHistory,
        saveClaimRecording: saveClaimRecording,
        UpdateClaimRecording: UpdateClaimRecording,
        getAllDocument: getAllDocument,
        saveClaimPayment: saveClaimPayment,
        getAllClaimPaymentsByClaimID: getAllClaimPaymentsByClaimID,
        updateClaimPayment: updateClaimPayment

    };
});