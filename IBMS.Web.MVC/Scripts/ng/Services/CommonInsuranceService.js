'use strict';

ibmsApp.factory('CommonInsuranceService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var getAvailableInsurance = function () {
        return $http.post($rootScope.serviceURL + 'api/InsClass/GetAllInsClasses', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };
    var getAvailableSubInsurance = function () {
        return $http.post($rootScope.serviceURL + 'api/InsClass/GetAllInsSubClasses', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAvailableInsSubClassByInsClass = function (insClassID) {
        var params = $.param({ "insClassID": insClassID });
        return $http.post($rootScope.serviceURL + 'api/InsClass/GetAllInsSubClassesByInsClass', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAvailableCommonInsurance = function (BUID) {
        var params = $.param({ "BusinessUnitID": BUID });
        return $http.post($rootScope.serviceURL + 'api/CommonInsScope/GetAllCommonInsScopes', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var saveCommonInsurance = function (buObj1, cuObj) {
        var params = $.param({ "Description": buObj1.CommonInsurance, "InsClassID": buObj1.InsClassID, "InsSubClassID": buObj1.InsSubClassID, "UserID": cuObj.UserID });
        return $http.post($rootScope.serviceURL + 'api/CommonInsScope/SaveCommonInsScope', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var updateCommonInsurance = function (CommonInsuranceScopeID, Description, InsuranceClassID, InsuranceSubClassID, userID) {

        var params = $.param({ "CommonInsScopeID": CommonInsuranceScopeID, "Description": Description, "InsClassID": InsuranceClassID, "InsSubClassID": InsuranceSubClassID, "UserID": userID });

        return $http.post($rootScope.serviceURL + 'api/CommonInsScope/UpdateCommonInsScope', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var deleteCommonInsurance = function (buObj1) {
        var params = $.param({ "CommonInsScopeID": buObj1 });

        return $http.post($rootScope.serviceURL + 'api/CommonInsScope/DeleteCommonInsScope', params, config).then(function (results) {
            return results.data;
        }, function (data) {

            // Handle error here
        })
    };


    return {
        getAvailableCommonInsurance: getAvailableCommonInsurance,
        getAvailableInsurance: getAvailableInsurance,
        getAvailableSubInsurance: getAvailableSubInsurance,
        getAvailableInsSubClassByInsClass: getAvailableInsSubClassByInsClass,
        saveCommonInsurance: saveCommonInsurance,
        updateCommonInsurance: updateCommonInsurance,
        deleteCommonInsurance: deleteCommonInsurance
    };
});