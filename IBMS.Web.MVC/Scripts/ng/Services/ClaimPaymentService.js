'use strict';

ibmsApp.factory('ClaimPaymentService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var saveClaimPayment = function (claimHeader, UserID) {
        // var params = $.param({ "ClaimAmount": claimHeader.claimAmount, "Notes": claimHeader.notes, "chequeNo": claimHeader.chequeNo, "payAmount": claimHeader.payAmount, "tpay": claimHeader.tpay, "paymentDate": claimHeader.paymentDate, "paymentType": claimHeader.paymentType, "UserID": 1 });
        var claimPaymentMethodList = [{ "ChequeNo": "12345", "PaymentTypeID": 1, "PaidAmount": 2000, "PaidDate": "02/20/2018", "IsFinal": false }, { "ChequeNo": "12346", "PaymentTypeID": 1, "PaidAmount": 2000, "PaidDate": "02/22/2018", "IsFinal": true }];
        var claimPayment = { "ClaimRecordingID": 2, "ClaimAmount": 4000, "Notes": "Test Note", "ClaimPaymentMethodDetails": claimPaymentMethodList };

        var params = $.param({ "ClaimPaymentVM": claimHeader, "UserID": 1 });
        alert(params);
        return $http.post($rootScope.serviceURL + 'api/Claim/SaveClaimPayment', params, config).then(function (results) {
            alert(angular.toJson(results));
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAllClaimPayments = function () {
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

    return {
        saveClaimPayment: saveClaimPayment,
        getAllClaimPayments: getAllClaimPayments,
        updateClaimPayment: updateClaimPayment
    };

});