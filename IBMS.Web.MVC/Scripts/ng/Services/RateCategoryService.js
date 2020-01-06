'use strict';

ibmsApp.factory('RateCategoryService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };


    var getAvailableRateCategory = function () {
        return $http.post($rootScope.serviceURL + 'api/RateCategory/GetAllRateCategories', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var saveRateCategory = function (rateObj, cuObj) {
        var params = $.param({ "RateCategory": rateObj.RateCategory, "UserID": cuObj.UserID });
        return $http.post($rootScope.serviceURL + 'api/RateCategory/SaveRateCategory', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var updateRATE = function (rateCategoryID, rateCategoryName, userID) {
        var params = $.param({ "RateCategoryID": rateCategoryID, "RateCategory": rateCategoryName, "UserID": userID });
        return $http.post($rootScope.serviceURL + 'api/RateCategory/UpdateRateCategory', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var deleteRATE = function (rateID) {
        var params = $.param({ "RateCategoryID": rateID });
        return $http.post($rootScope.serviceURL + 'api/RateCategory/DeleteRateCategory', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    return {
        getAvailableRateCategory: getAvailableRateCategory,
        saveRateCategory: saveRateCategory,
        updateRATE: updateRATE,
        deleteRATE: deleteRATE
    };
});