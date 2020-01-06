'use strict';

ibmsApp.factory('AgentCommissionService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var getAgentCommissionByDate = function (fromDate, toDate, type) {
        var params = $.param({ "filterObj": { "fromDate": fromDate, "toDate": toDate, "type": type }});
        return $http.post($rootScope.serviceURL + 'api/Agent/GetAgentCommissionByDate', params, config).then(function (results) {
            return results.data;
        }, function (data) {
        })
    };

    var getBUPACommissionByDate = function (fromDate, toDate, type) {
        var params = $.param({ "filterObj": { "fromDate": fromDate, "toDate": toDate, "type": type }});
        return $http.post($rootScope.serviceURL + 'api/Integration/GetBUPACommissionByDate', params, config).then(function (results) {
            return results.data;
        }, function (data) {
        })
    };


    // #########################################################################################################
    var getAgentCommission = function () {
        return $http.post($rootScope.serviceURL + 'api/Agent/GetAgentCommission', null, config).then(function (results) {
            return results.data;
        }, function (data) {
        })
    };

    var getBupaAmount = function () {
        return $http.post($rootScope.serviceURL + 'api/Integration/GetBupaAmount', null, config).then(function (results) {
            return results.data;
        }, function (data) {
        })
    };
    var saveTransaction = function (paymentDate, type, transactions, ids) {
        //var params = $.param({ "paymentDate" : paymentDate , "type" : type, "amountInfo": transactions });
        var params = $.param({ "rootObj": { "paymentDate": paymentDate, "type": type, "amountInfo": transactions, "ids": ids } });

        return $http.post($rootScope.serviceURL + 'api/Integration/SaveTransactionDetail', params, config).then(function (results) {
            return results.data;
        }, function (data) {
        })
    };
    return {
        getAgentCommission: getAgentCommission,
        getBupaAmount: getBupaAmount,
        saveTransaction: saveTransaction,

        // #####
        getAgentCommissionByDate: getAgentCommissionByDate,
        getBUPACommissionByDate: getBUPACommissionByDate,
    }
});