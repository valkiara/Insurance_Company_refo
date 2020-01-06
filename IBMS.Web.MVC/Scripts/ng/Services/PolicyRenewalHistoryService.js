'use strict';

ibmsApp.factory('PolicyRenewalHistoryService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var getAllPolicyRenewalHistoryByBUID = function (businessUnitID) {
        var params = $.param({ "BusinessUnitID": businessUnitID });
        return $http.post($rootScope.serviceURL + 'api/Policy/GetAllPolicyRenewalHistoriesByBUID', params, config)
            .then(function (results) {
                return results.data;
            }, function (data) {
            })
    };

    var GetAllPolicyRenewalHistoriesByBusinessUnitID = function (businessUnitID) {
        var params = $.param({ "BusinessUnitID": businessUnitID });
        return $http.post($rootScope.serviceURL + 'api/Policy/GetAllPolicyRenewalHistoriesByBusinessUnitID', params, config)
            .then(function (results) {
                return results.data;
            }, function (data) {
            })
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

    var loadQuotationHeaderByID = function (PolicyInfoRecID) {
        var params = $.param({ "PolicyInfoRecID": PolicyInfoRecID });
        return $http.post($rootScope.serviceURL + 'api/Policy/GetPolicyInfoRecordingByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var GetQuotationHeaderByID = function (QuotationHeaderID) {
        var params = $.param({ "QuotationHeaderID": QuotationHeaderID });
        return $http.post($rootScope.serviceURL + 'api/Quotation/GetQuotationHeaderByID', params, config).then(function (results) {
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

    var loadCommissionTypes = function () {
        return $http.post($rootScope.serviceURL + 'api/ComStructure/GetAllComTypes', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var loadCommissionStructureLines = function () {
        return $http.post($rootScope.serviceURL + 'api/ComStructure/GetAllComStructureLines', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    //var editPolicyRenewalHistroy = function (policyRenewalID) {
    //    var params = $.param({ "PolicyRenewalHistoryID": policyRenewalID });
    //    return $http.post($rootScope.serviceURL + '/api/Policy/GetPolicyRenewalHistoryByID', params, config).then(function (results) {
    //        return results.data;
    //    }, function (data) {
    //        //Handle error here
    //    });
    //};



    var editPolicyRenewalHistroy = function (PolicyInfoRecID) {
        var params = $.param({ "PolicyInfoRecID": PolicyInfoRecID });
        return $http.post($rootScope.serviceURL + 'api/Policy/GetPolicyInfoRecordingByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {

        })
    };
    //var editPolicyRenewalHistroy = function (policyRenewalHistoryID) {
    //    var params = $.param({ "PolicyRenewalHistoryID": policyRenewalHistoryID });
    //    return $http.post($rootScope.serviceURL + 'api/Policy/GetPolicyRenewalHistoryByID', params, config).then(function (results) {
    //        return results.data;
    //    }, function (data) {

    //    })
    //};


    var savePolicyRenewalHistory = function (PolicyInfoObj, userID) {

        //var result = Object.keys(PolicyInfoObj).map(function(key) {
        //    return [Number(key), PolicyInfoObj[key]];
        //});
        var params = $.param({ "PolicyRenewalHistoryVM": PolicyInfoObj, "UserID": userID });
        return $http.post($rootScope.serviceURL + 'api/Policy/SavePolicyRenewalHistory', params, config).then(function (results) {
            return results.data;
        }, function (data) {

        })
    };
    var updatePolicyRenewalHistory = function (PolicyInfoObj, userID) {

        //var result = Object.keys(PolicyInfoObj).map(function(key) {
        //    return [Number(key), PolicyInfoObj[key]];
        //});
        var params = $.param({ "PolicyRenewalHistoryVM": PolicyInfoObj, "UserID": userID });
        return $http.post($rootScope.serviceURL + 'api/Policy/UpdatePolicyRenewalHistory', params, config).then(function (results) {
            return results.data;
        }, function (data) {

        })
    };



    var loadAgent = function () {
        return $http.post($rootScope.serviceURL + 'api/Agent/GetAllAgents', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var loadExecutive = function () {
        return $http.post($rootScope.serviceURL + 'api/Employee/GetAllEmployees', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    return {

        GetAllPolicyRenewalHistoriesByBusinessUnitID: GetAllPolicyRenewalHistoriesByBusinessUnitID,
        loadExecutive: loadExecutive,
        loadAgent: loadAgent,
        getAllPolicyRenewalHistoryByBUID: getAllPolicyRenewalHistoryByBUID,
        getAllPolicyInfoRecordingsByBUID: getAllPolicyInfoRecordingsByBUID,
        getAllQuotationHeadersByBUID: getAllQuotationHeadersByBUID,
        loadQuotationHeaderByID: loadQuotationHeaderByID,
        // getClientRequestByID: getClientRequestByID,
        GetQuotationHeaderByID: GetQuotationHeaderByID,
        getClientByID: getClientByID,
        loadQuoteInfoInsCompanyLineDetailsByQuotation: loadQuoteInfoInsCompanyLineDetailsByQuotation,
        loadCurrencies: loadCurrencies,
        loadCommissionTypes: loadCommissionTypes,
        loadCommissionStructureLines: loadCommissionStructureLines,
        savePolicyRenewalHistory: savePolicyRenewalHistory,
        updatePolicyRenewalHistory: updatePolicyRenewalHistory,
        editPolicyRenewalHistroy: editPolicyRenewalHistroy
    };
});