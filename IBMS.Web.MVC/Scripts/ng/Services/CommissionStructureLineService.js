'use strict';

ibmsApp.factory('CommissionStructureLineService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var getAvailableCommisionStructure = function () {
        return $http.post($rootScope.serviceURL + 'api/ComStructure/GetAllComStructureHeaders', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAvailableRateCategory = function () {
        return $http.post($rootScope.serviceURL + 'api/RateCategory/GetAllRateCategories', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAvailableCommissionStructureHeader = function (BUID) {
        var params = $.param({ "BusinessUnitID": BUID });
        //return $http.post(url + 'api/ComStructure/GetAllComStructureLines', null, config).then(function (results) {
        return $http.post($rootScope.serviceURL + 'api/ComStructure/GetAllComStructureLines', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var addComStructLine = function (comObj, CuObj) {
        var params = $.param({ "ComStructID": comObj.CommissionStructureID, "RateCategoryID": comObj.RateCategoryID, "IsAgeConsider": comObj.IsAgeConsider, "AgeFrom": comObj.AgeFrom, "AgeTo": comObj.AgeTo, "isFixed": comObj.IsFixed, "RateValue": comObj.RateValue, "UserID": CuObj.UserID });// "UserID": 1
        return $http.post($rootScope.serviceURL + 'api/ComStructure/SaveComStructureLine', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var updateCSL = function (ComStructLineID, ComStrctID, rateCategoryID, isAgeConsider, AgeFrom, AgeTo, isFixed, RateValue, userID) {
        var params = $.param({ "ComStructLineID": ComStructLineID, "ComStructID": ComStrctID, "RateCategoryID": rateCategoryID, "IsAgeConsider": isAgeConsider, "AgeFrom": AgeFrom, "AgeTo": AgeTo, "isFixed": isFixed, "RateValue": RateValue, "UserID": userID });
        return $http.post($rootScope.serviceURL + 'api/ComStructure/UpdateComStructureLine', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var deleteCSL = function (CSLID) {
        var params = $.param({ "ComStructLineID": CSLID });
        return $http.post($rootScope.serviceURL + 'api/ComStructure/DeleteComStructureLine', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    return {
        getAvailableCommisionStructure: getAvailableCommisionStructure,
        getAvailableRateCategory: getAvailableRateCategory,
        getAvailableCommissionStructureHeader: getAvailableCommissionStructureHeader,
        addComStructLine: addComStructLine,
        updateCSL: updateCSL,
        deleteCSL: deleteCSL
    };
});