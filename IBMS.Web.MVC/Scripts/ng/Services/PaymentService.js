'use strict';

ibmsApp.factory('PaymentService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var getAllPayments = function (businessUnitID) {
        var params = $.param({ "BusinessUnitID": businessUnitID });
        return $http.post($rootScope.serviceURL + 'api/Payment/GetAllPaymentsByBUID', params, config)
            .then(function (results) {
                return results.data;
            }, function (data) {
            })
    };

    var loadBanks = function (BUID) {
        var params = $.param({ "BusinessUnitID": BUID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetAllBanksByBusinessUnitID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var loadPaymentMethods = function () {
        //var params = $.param({ "BusinessUnitID": BUID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetAllPaymentMethods', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var loadCurrencies = function () {
        return $http.post($rootScope.serviceURL + 'api/Utility/GetAllCurrencies', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
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

    var getClientByID = function (clientID) {
        var params = $.param({ "ClientID": clientID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetClientByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var loadPolicyDetails = function (PolicyID) {
        var params = $.param({ "PolicyInfoRecID": PolicyID });
        return $http.post($rootScope.serviceURL + 'api/Policy/GetPolicyInfoRecordingByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var getPolicyInfoRecordingsByClient = function (clientID) {
        var params = $.param({ "ClientID": clientID });
        return $http.post($rootScope.serviceURL + 'api/Policy/GetPolicyInfoRecordingsByClient', params, config).then(function (results) {
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

    var savePayment = function (paymentObj, userID) {
        var params = $.param({ "PaymentObj": paymentObj, "UserID": userID });
        return $http.post($rootScope.serviceURL + 'api/Payment/SavePayment', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var updatePayment = function (paymentObj, userID) {
        var params = $.param({ "PaymentObj": paymentObj, "UserID": userID });
        return $http.post($rootScope.serviceURL + 'api/Payment/UpdatePayment', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var getPaymentByID = function (paymentID) {
        var params = $.param({ "PaymentID": paymentID });
        return $http.post($rootScope.serviceURL + 'api/Payment/GetPaymentByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    return {
        getAllPayments: getAllPayments,
        getAllClientsByBUID: getAllClientsByBUID,
        getClientByID: getClientByID,
        getPolicyInfoRecordingsByClient: getPolicyInfoRecordingsByClient,
        getAllChargeTypes: getAllChargeTypes,
        savePayment: savePayment,
        updatePayment: updatePayment,
        getPaymentByID: getPaymentByID,
        loadBanks: loadBanks,
        loadPaymentMethods: loadPaymentMethods,
        loadCurrencies: loadCurrencies,
        loadPolicyDetails: loadPolicyDetails
    };
});