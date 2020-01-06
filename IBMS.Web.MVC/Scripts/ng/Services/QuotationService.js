'use strict';

ibmsApp.factory('QuotationService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var getAllQuotationHeadersByBUID = function (businessUnitID) {
        var params = $.param({ "BusinessUnitID": businessUnitID });
        return $http.post($rootScope.serviceURL + 'api/Quotation/GetAllQuotationHeadersByBUID', params, config)
            .then(function (results) {
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

    var getQuotationDetailByID = function (quotationHeaderId) {
        var params = $.param({ "QuotationHeaderId": quotationHeaderId });
        return $http.post($rootScope.serviceURL + 'api/Quotation/GetQuotationDetailById', params, config).then(function (results) {
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

    var getAllInsuranceCompaniesByBUID = function (businessUnitID) {
        var params = $.param({ "businessUnitID": businessUnitID });
        return $http.post($rootScope.serviceURL + 'api/InsCompany/GetInsuranceCompaniesByBusinessUnitID', params, config)
            .then(function (results) {
                return results.data;
            }, function (data) {
            })
    };

    var updateQuotation = function (quotHeaderObj, userID, selectedInsCompanies) {
        var params = $.param({ "QuotationHeaderObj": quotHeaderObj, "UserID": userID, "RequestedInsuranceCompanyDetails": selectedInsCompanies });

        return $http.post($rootScope.serviceURL + 'api/Quotation/UpdateQuotation', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var updateQuotationStatus = function (quotationHeaderID, quotationStatusCode, userID) {
        var params = $.param({ "QuotationHeaderID": quotationHeaderID, "QuotationStatusCode": quotationStatusCode, "UserID": userID });
        return $http.post($rootScope.serviceURL + 'api/Quotation/UpdateQuotationStatus', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var loadQuotationHeaderByID = function (quotationHeaderID) {
        var params = $.param({ "QuotationHeaderID": quotationHeaderID });
        return $http.post($rootScope.serviceURL + 'api/Quotation/GetQuotationHeaderByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };
    var saveTemCoverNoteDetails = function (CoverNoteObj, UserID) {
        var params = $.param({ "CoverNoteObj": CoverNoteObj, "UserID": UserID });
        return $http.post($rootScope.serviceURL + 'api/CoverNote/SaveCoverNote', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var getAllCoverNotes = function () {

        return $http.post($rootScope.serviceURL + 'api/CoverNote/GetAllCoverNotes', null, config)
            .then(function (results) {

                return results.data;

            }, function (data) {

            })
    };

    var sendEmailRequest = function (emailObj) {
        var params = $.param({ "UserName": emailObj.userName, "EmailAddress": emailObj.emailAddress, "BccMail": emailObj.bccEmail, "EmailHeader": emailObj.emailHeader, "EmailContent": emailObj.emailMessage, "InsSubClassId": emailObj.insSubClassId, "CompanyId": emailObj.compId });
        return $http.post($rootScope.serviceURL + 'api/Email/SendAttachmentEmail', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };
    var sendEmailClientRequest = function (emailObj) {
        var params = $.param({ "UserName": emailObj.userName, "EmailAddress": emailObj.emailAddress, "EmailHeader": emailObj.emailHeader, "EmailContent": emailObj.emailMessage });
        return $http.post($rootScope.serviceURL + 'api/Email/SendGeneralEmail', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var sendAttachedQuotation = function (attachmentObj) {
        //var params = $.param({ "UserName": emailObj.userName, "EmailAddress": emailObj.emailAddress, "EmailHeader": emailObj.emailHeader, "EmailContent": emailObj.emailMessage });
        var params = $.param({ "InsSubClassId": attachmentObj.InsuranceSubClassID, "CompanyId": attachmentObj.companyId });
        return $http.post($rootScope.serviceURL + 'api/Email/SendAttachmentEmail', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };


    var saveTCN = function (CoverNoteObj, userID) {
        var params = $.param({ "CoverNoteObj": CoverNoteObj, "UserID": userID });

        return $http.post($rootScope.serviceURL + 'api/CoverNote/SaveCoverNote', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };
    var loadProposalForm = function (QuatationID) {
        var params = $.param({ "QuotationID": QuatationID });
        return $http.post($rootScope.serviceURL + 'api/CoverNote/GetCoverNoteByQuatationID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var loadTransactionTypeDetails = function () {
        return $http.post($rootScope.serviceURL + 'api/TransactionType/GetAllTransactionTypes', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };
    var getAvailableSubInsurance = function () {
        return $http.post($rootScope.serviceURL + 'api/InsClass/GetAllInsSubClasses', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };
    var getAvailableInsuranceDropdown = function () {
        return $http.post($rootScope.serviceURL + 'api/InsClass/GetAllInsClasses', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };
    var getAvailableInsSubClass = function (id) {
        var params = $.param({ "insClassID": id });
        //var params = { "insClassID": id };
        return $http.post($rootScope.serviceURL + 'api/InsClass/GetAllInsSubClassesByInsClass', params, config).then(function (results) {
            return results.data;
            //alert("login"+angular.toJson(results));
        }, function (data) {
            // Handle error here
        })
    };

    return {
        getAllQuotationHeadersByBUID: getAllQuotationHeadersByBUID,
        getClientRequestByID: getClientRequestByID,
        getClientByID: getClientByID,
        getAllInsuranceCompaniesByBUID: getAllInsuranceCompaniesByBUID,
        updateQuotation: updateQuotation,
        updateQuotationStatus: updateQuotationStatus,
        loadQuotationHeaderByID: loadQuotationHeaderByID,
        sendEmailRequest: sendEmailRequest,
        getAllCoverNotes: getAllCoverNotes,
        saveTCN: saveTCN,
        loadProposalForm: loadProposalForm,
        loadTransactionTypeDetails: loadTransactionTypeDetails,
        getAvailableSubInsurance: getAvailableSubInsurance,
        sendAttachedQuotation: sendAttachedQuotation,
        getQuotationDetailByID: getQuotationDetailByID,
        getAvailableInsuranceDropdown: getAvailableInsuranceDropdown,
        getAvailableInsSubClass: getAvailableInsSubClass,
    };
});