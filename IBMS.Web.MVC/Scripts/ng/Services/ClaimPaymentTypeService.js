'use strict';

ibmsApp.factory('ClaimPaymentTypeService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': $rootScope.authKey }

    };

    var GetAllPaymentTypes = function () {
        return $http.post($rootScope.serviceURL + 'api/PaymentType/GetAllPaymentTypes', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var SavePaymentType = function (paymentTypeName, description, userID) {
        var params = {
            "PaymentTypeName": paymentTypeName,
            "Description": description,
            "UserID": userID
        };

        return $http.post($rootScope.serviceURL + 'api/PaymentType/SavePaymentType',
           params, config).then(function (results) {
               return results.data;
           }, function (data) {

           });
    };

    var UpdatePaymentType = function (paymentTypeID, paymentTypeName, description, userID) {

        var params = {
            "PaymentTypeID": paymentTypeID,
            "PaymentTypeName": paymentTypeName,
            "Description": description,
            "UserID": userID
        };

        return $http.post($rootScope.serviceURL + 'api/PaymentType/UpdatePaymentType',
           params, config).then(function (results) {

               return results.data;
           }, function (data) {

           });
    };

    var DeletePaymentType = function (PaymentTypeID) {

        var params = {
            "PaymentTypeID": PaymentTypeID,
        };

        return $http.post($rootScope.serviceURL + 'api/PaymentType/DeletePaymentType',
           params, config).then(function (results) {
               return results.data;
           }, function (data) {

           });
    };

    return {
        GetAllPaymentTypes: GetAllPaymentTypes,
        SavePaymentType: SavePaymentType,
        UpdatePaymentType: UpdatePaymentType,
        DeletePaymentType: DeletePaymentType
    };
});