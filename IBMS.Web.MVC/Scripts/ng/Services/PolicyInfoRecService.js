'use strict';

ibmsApp.factory('PolicyInfoRecService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var getAllPolicyInfoRecordingsByBUID = function (businessUnitID) {
        var params = $.param({ "BusinessUnitID": businessUnitID });
        return $http.post($rootScope.serviceURL + 'api/Policy/GetAllPolicyInfoRecordingsByBUID', params, config)
            .then(function (results) {
                return results.data;
            }, function (data) {
            })
    };

    var getAllQuotationHeadersByBUID = function (businessUnitID) {
        var params = $.param({ "BusinessUnitID": businessUnitID });
        return $http.post($rootScope.serviceURL + 'api/Quotation/GetAllQuotationHeadersByBUID', params, config)
            .then(function (results) {
                return results.data;
            }, function (data) {
            })
    };

    var loadQuotationHeaderByID = function (quotationHeaderID) {
        var params = $.param({ "QuotationHeaderID": quotationHeaderID });
        return $http.post($rootScope.serviceURL + 'api/Quotation/GetQuotationHeaderByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };


    var loadQuotationHeaderInsuranceByID = function (quotationHeaderID) {
        var params = $.param({ "QuotationHeaderID": quotationHeaderID });
        return $http.post($rootScope.serviceURL + 'api/Quotation/GetQuotationHeaderInsuranceCompanyByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };


    var SelectInsuranceCompany = function (quotationHeaderID) {
        var params = $.param({ "QuotationHeaderID": quotationHeaderID });
        return $http.post($rootScope.serviceURL + 'api/Quotation/GetQuotationHeaderByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
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

    var getAllChargeTypes = function () {
        return $http.post($rootScope.serviceURL + 'api/ChargeType/GetAllChargeTypes', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };


    var getInsChargeType = function (quotationHeaderID, insuranceCompanyID) {

        var params = $.param({ "quotationHeaderID": quotationHeaderID, "insuranceCompanyID": insuranceCompanyID });
        return $http.post($rootScope.serviceURL + 'api/Quotation/GetAllQuotationLine', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var loadQuoteInfoInsCompanyLineDetailsByQuotation = function (quotationHeaderID) {
        var params = $.param({ "QuotationHeaderID": quotationHeaderID });
        return $http.post($rootScope.serviceURL + 'api/Quotation/GetQuoteInfoInsCompanyLineDetailsByQuotation', params, config).then(function (results) {
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
    var loadTransactionTypeDetails = function () {
        return $http.post($rootScope.serviceURL + 'api/TransactionType/GetAllTransactionTypes', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var loadCommissionTypes = function () {
        return $http.post($rootScope.serviceURL + 'api/ComStructure/GetAllComTypes', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };
    var loadCommissionStructureHeaders = function () {
        return $http.post($rootScope.serviceURL + 'api/ComStructure/GetAllComStructureHeaders', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };
    var loadCommissionStructureLines = function (ComStruLineID) {
        var params = $.param({ "ComStructureHeaderID": ComStruLineID });
        return $http.post($rootScope.serviceURL + 'api/ComStructure/GetAllComStructureLinesByComStructureHeaderID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };
    var LoadComStruLineDetails = function (ComStruLineID) {
        var params = $.param({ "ComStructLineID": ComStruLineID });
        return $http.post($rootScope.serviceURL + 'api/ComStructure/GetComStructureLineByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };
    var loadIntroducer = function () {
        return $http.post($rootScope.serviceURL + 'api/Introducer/GetAllIntroducers', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };
    var editPolicyRecInfo = function (PolicyInfoRecID) {
        var params = $.param({ "PolicyInfoRecID": PolicyInfoRecID });
        return $http.post($rootScope.serviceURL + 'api/Policy/GetPolicyInfoRecordingByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {

        })
    };
    var savePolicyInfoRecording = function (QuatationHeaderID, policyInfoRecObjNew, policyInfoCommissionList, policyInfoChgList, userID) {

        var params = $.param({ "QuotationHeaderID": QuatationHeaderID, "PolicyInfoRecList": policyInfoRecObjNew, "policyInfoCommissionList": policyInfoCommissionList, "policyInfoChgList": policyInfoChgList, "UserID": userID });
        return $http.post($rootScope.serviceURL + 'api/Policy/SavePolicyInformationRecording', params, config).then(function (results) {
            return results.data;
        }, function (data) {

        })
    };
    var updatePolicyInfoRecording = function (QuatationHeaderID, policyInfoRecObjNew, policyInfoCommissionList, policyInfoChgList, userID) {

        var params = $.param({ "QuotationHeaderID": QuatationHeaderID, "PolicyInfoRecList": policyInfoRecObjNew, "policyInfoCommissionList": policyInfoCommissionList, "policyInfoChgList": policyInfoChgList, "UserID": userID });
        return $http.post($rootScope.serviceURL + 'api/Policy/UpdatePolicyInformationRecording', params, config).then(function (results) {
            return results.data;
        }, function (data) {

        })




        //var result = Object.keys(PolicyInfoObj).map(function(key) {
        //    return [Number(key), PolicyInfoObj[key]];
        //});
        //var params = $.param({ "QuotationHeaderID": QuatationHeaderID, "PolicyInfoRecList": policyInfoRecObjNew, "policyInfoCommissionList": policyInfoCommissionList, "policyInfoChgList": policyInfoChgList, "UserID": userID });
        //return $http.post($rootScope.serviceURL + 'api/Policy/UpdatePolicyInformationRecording', params, config).then(function (results) {
        //    return results.data;
        //}, function (data) {

        //})
    };

    var loadInsuranceDetails = function () {
        return $http.post($rootScope.serviceURL + 'api/InsCompany/GetAllInsuranceCompanies', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getInsChargeType = function (quotationHeaderID, insuranceCompanyID) {

        var params = $.param({ "quotationHeaderID": quotationHeaderID, "insuranceCompanyID": insuranceCompanyID });
        return $http.post($rootScope.serviceURL + 'api/Quotation/GetAllQuotationLine', params, config).then(function (results) {
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

    var getAllPolicyInfoRecording = function () {
        return $http.post($rootScope.serviceURL + 'api/Policy/GetAllPolicyInfoRecordings', null, config).then(function (results) {
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

    var getAvailableInsSubClassByInsClass = function (insClassID) {
        var params = $.param({ "insClassID": insClassID });
        return $http.post($rootScope.serviceURL + 'api/InsClass/GetAllInsSubClassesByInsClass', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getInsChargeTypeNew = function (insuranceCompanyIDn, InsuranceClassIDn, InsuranceSubClassIDn) {

        var params = $.param({ "insuranceCompanyIDn": insuranceCompanyIDn, "InsuranceClassIDn": InsuranceClassIDn, "InsuranceSubClassIDn": InsuranceSubClassIDn });
        return $http.post($rootScope.serviceURL + 'api/Quotation/getInsChargeType', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });


    };

    var getAllPolicyRecInfoReport = function (policyId) {
        var params = $.param({ "PolicyInfoRecID": policyId });
        return $http.post($rootScope.serviceURL + 'api/Policy/GetPolicyRecInfor', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getApplicableChargeTypes = function () {
        return $http.post($rootScope.serviceURL + 'api/ChargeType/GetApplicableChargeTypes', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };


    return {
        getInsChargeTypeNew: getInsChargeTypeNew,
        getAvailableInsSubClassByInsClass: getAvailableInsSubClassByInsClass,
        getAvailableInsuranceDropdown: getAvailableInsuranceDropdown,
        getAllPolicyInfoRecording: getAllPolicyInfoRecording,
        loadExecutive: loadExecutive,
        loadQuotationHeaderInsuranceByID: loadQuotationHeaderInsuranceByID,
        getInsChargeType: getInsChargeType,
        SelectInsuranceCompany: SelectInsuranceCompany,
        loadInsuranceDetails: loadInsuranceDetails,
        getAllPolicyInfoRecordingsByBUID: getAllPolicyInfoRecordingsByBUID,
        getAllQuotationHeadersByBUID: getAllQuotationHeadersByBUID,
        loadQuotationHeaderByID: loadQuotationHeaderByID,
        getClientRequestByID: getClientRequestByID,
        getClientByID: getClientByID,
        loadQuoteInfoInsCompanyLineDetailsByQuotation: loadQuoteInfoInsCompanyLineDetailsByQuotation,
        loadCurrencies: loadCurrencies,
        loadCommissionTypes: loadCommissionTypes,
        loadCommissionStructureLines: loadCommissionStructureLines,
        savePolicyInfoRecording: savePolicyInfoRecording,
        updatePolicyInfoRecording: updatePolicyInfoRecording,
        editPolicyRecInfo: editPolicyRecInfo,
        getAllChargeTypes: getAllChargeTypes,
        loadTransactionTypeDetails: loadTransactionTypeDetails,
        loadCommissionStructureHeaders: loadCommissionStructureHeaders,
        loadIntroducer: loadIntroducer,
        LoadComStruLineDetails: LoadComStruLineDetails,
        getInsChargeType: getInsChargeType,
        getInsChargeTypeNew: getInsChargeTypeNew,
        getAllPolicyRecInfoReport: getAllPolicyRecInfoReport,
        getApplicableChargeTypes: getApplicableChargeTypes,
    };
});