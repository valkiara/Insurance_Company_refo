'use strict';

ibmsApp.factory('TransactionTypeService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var getAvailableBUDropdown = function () {
        return $http.post($rootScope.serviceURL + 'api/BusinessUnit/GetAllBusinessUnits', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAvailableTransaction = function (BUID) {
        var params = $.param({ "BusinessUnitID": BUID });

        return $http.post($rootScope.serviceURL + 'api/TransactionType/GetAllTransactionTypesByBUID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var saveTransactionType = function (traObj, cuObj) {
        var params = $.param({ "Description": traObj.Description, "BUID": traObj.BusinessUnitID, "UserID": cuObj.UserID });
        return $http.post($rootScope.serviceURL + 'api/TransactionType/SaveTransactionType', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var updateTRA = function (transactionTypeID, transactionType, businessunitID, userID) {
        var params = $.param({ "TransactionTypeID": transactionTypeID, "Description": transactionType, "BUID": businessunitID, "UserID": userID });
        return $http.post($rootScope.serviceURL + 'api/TransactionType/UpdateTransactionType', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var deleteTRA = function (TraID) {
        var params = $.param({ "TransactionTypeID": TraID });
        return $http.post($rootScope.serviceURL + 'api/TransactionType/DeleteTransactionType', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    return {
        getAvailableBUDropdown: getAvailableBUDropdown,
        getAvailableTransaction: getAvailableTransaction,
        saveTransactionType: saveTransactionType,
        updateTRA: updateTRA,
        deleteTRA: deleteTRA
    };
});