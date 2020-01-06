'use strict';

ibmsApp.factory('PolicyCategoryService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };
    var getAllPolicyCategories = function () {
        return $http.post($rootScope.serviceURL + 'api/Policy/GetAllPolicyCategories', null, config).then(function (results) {
            return results.data;
        }, function (data) {
        })
    };

    var savePolicyCategoryData = function (categoryName, userID) {
        var params = {
            "CategoryName": categoryName,
            "UserID": userID
        };
        return $http.post($rootScope.serviceURL + 'api/Policy/SavePolicyCategrory',
           params, config).then(function (results) {
               return results.data;
           }, function (data) {
               alert("Error : " + data);
           });
    };

    var updatePolicyCategoryData = function (
           policyCategoryID, categoryName, userID) {
        var params = {
            "PolicyCategoryID": policyCategoryID,
            "CategoryName": categoryName,
            "UserID": userID
        };
        return $http.post($rootScope.serviceURL + 'api/Policy/UpdatePolicyCategrory',
           params, config).then(function (results) {
               return results.data;
           }, function (data) {
               alert("Error : " + data);
           });
    };

    var DeletePolicyCategoryData = function (policyCategoryID) {

        var params = { "PolicyCategoryID": policyCategoryID };
        return $http.post($rootScope.serviceURL + 'api/Policy/DeletePolicyCategory',
            params, config).then(function (results) {
                return results.data;
            }, function (data) {
                alert("Error : " + data);
            });
    };

    return {
        savePolicyCategoryData: savePolicyCategoryData,
        getAllPolicyCategories: getAllPolicyCategories,
        updatePolicyCategoryData: updatePolicyCategoryData,
        DeletePolicyCategoryData: DeletePolicyCategoryData,
        // getAvailableDesignation: getAvailableDesignation
    };
});