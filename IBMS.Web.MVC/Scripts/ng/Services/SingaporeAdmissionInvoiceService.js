'use strict';

ibmsApp.factory('SingaporeAdmissionInvoiceService', function ($http, $rootScope) {
    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var configEncoded = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var getInvoiceDetailsByRefNo = function (referenceNo) {

        var params = $.param({ "refNo": referenceNo });

        return $http.post($rootScope.serviceURL + 'api/Admission/GetSingaporeInvoiceDetailsByReferenceNo', params, configEncoded).then(function (results) {
            return results.data;
        }, function (data) {

        })
    };
    var save = function (invoiceData) {

        console.log($rootScope.serviceURL);

        return $http.post($rootScope.serviceURL + 'api/Admission/SaveInvoice',
           invoiceData, config).then(function (results) {
               return results.data;
           }, function (data) {

           });

    }
    return {
        getInvoiceDetailsByRefNo: getInvoiceDetailsByRefNo,
        save: save,
    };
});