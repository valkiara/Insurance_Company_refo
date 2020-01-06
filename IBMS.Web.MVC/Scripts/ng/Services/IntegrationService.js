'use strict';

ibmsApp.factory('IntegrationService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };
    var getAgentCommission = function () {
        return $http.post($rootScope.serviceURL + 'api/Agent/GetAgentCommission', null, config).then(function (results) {
            return results.data;
        }, function (data) {
        })
    };

    var getBupaAmount = function (fDate,tDate,cType) {
        var params = $.param({ "rootObj": { "fromDate": fDate, "toDate": tDate, "type": cType } });
        return $http.post($rootScope.serviceURL + 'api/Integration/GetBupaAmount', params, config).then(function (results) {
            return results.data;
        }, function (data) {
        })
    };
    var saveTransaction = function (paymentDate, type, transactions, ids) {
        var params = $.param({ "rootObj": { "paymentDate": paymentDate, "type": type, "amountInfo": transactions, "ids" : ids }});
        return $http.post($rootScope.serviceURL + 'api/Integration/SaveTransactionDetail', params, config).then(function (results) {
            return results.data;
        }, function (data) {
        })
    };

    var getCurrency = function () {
        return $http.post($rootScope.serviceURL + 'api/Currency/GetCurrency', null, config).then(function (results) {
            return results.data;
        }, function (data) {
        })
    };
    var getBupaCommissionCommission = function (fDate, tDate, cType) {
        var params = $.param({ "rootObj": { "fromDate": fDate, "toDate": tDate, "type": cType } });
        return $http.post($rootScope.serviceURL + 'api/Integration/GetBUPACommissionDetailByDate', params, config).then(function (results) {
            return results.data;
        }, function (data) {
        })
    };
    return {
        getAgentCommission: getAgentCommission,
        getBupaAmount: getBupaAmount,
        saveTransaction: saveTransaction, 
        getCurrency: getCurrency,
        getBupaCommissionCommission: getBupaCommissionCommission,
    }
});