'use strict';

ibmsApp.factory('PolicyServices', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };
    var getAvailableBusinessUnit = function () {
        return $http.post($rootScope.serviceURL + 'api/BusinessUnit/GetAllBusinessUnits', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };
    var getAvailablePolicyCategory = function () {
        return $http.post($rootScope.serviceURL + 'api/Policy/GetAllPolicyCategories', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAvailablePolicy = function (BUID) {
        var params = { "BusinessUnitID": BUID };
        return $http.post($rootScope.serviceURL + 'api/Policy/GetAllPoliciesByBUID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };


    var savePolicy = function (PolicyObj, cuObj) {
        var params = {
            "PolicyName": PolicyObj.PolicyName, "Rate": PolicyObj.RateValue, "PolicyCategoryID": PolicyObj.PolicyCategoryID, "BUID": PolicyObj.buID, "UserID": cuObj.UserID
        };
        return $http.post($rootScope.serviceURL + 'api/Policy/SavePolicy', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var updatePolicy = function (PolicyID, PolicyName, Rate, PolicyCategoryID, buID, cuObj) {
        var params = { "PolicyID": PolicyID, "PolicyName": PolicyName, "Rate": Rate, "PolicyCategoryID": PolicyCategoryID, "BUID": buID, "UserID": cuObj.UserID };
        return $http.post($rootScope.serviceURL + 'api/Policy/UpdatePolicy', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };
    var deletePolicy = function (PolicyID) {
        var params = { "PolicyID": PolicyID };
        return $http.post($rootScope.serviceURL + 'api/Policy/DeletePolicy', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    return {
        getAvailableBusinessUnit: getAvailableBusinessUnit,
        savePolicy: savePolicy,
        getAvailablePolicyCategory: getAvailablePolicyCategory,
        deletePolicy: deletePolicy,
        updatePolicy: updatePolicy,
        getAvailablePolicy: getAvailablePolicy,

    };
});
