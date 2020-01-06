'use strict';

ibmsApp.factory('ProfileService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };
    var getAvailableDesignation = function () {
        return $http.post($rootScope.serviceURL + 'api/Designation/GetAllDesignations', null, config).then(function (results) {
            return results.data;
        }, function (data) {

        })
    };

    var getProfileDetails = function (userID) {
        var params = $.param({ "UserID": userID });
        return $http.post($rootScope.serviceURL + 'api/User/GetUserByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })

    }

    var updateUserDetails = function (userObj, cuObj) {
        var params = $.param({ "UserName": userObj.UserName, "LoginName": userObj.LoginName, "DesignationID": userObj.DesignationID, "UserID": cuObj.UserID, "LoggedUserID": cuObj.UserID });
        return $http.post($rootScope.serviceURL + 'api/User/UpdateUser', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })

    }
    var changePasswordDetails = function (passObj, cuObj) {
        var params = $.param({ "OldPassword": passObj.OldPassword, "NewPassword": passObj.NewPassword, "UserID": cuObj.UserID });
        return $http.post($rootScope.serviceURL + 'api/User/ChangePassword', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })

    }

    return {
        getAvailableDesignation: getAvailableDesignation,
        updateUserDetails: updateUserDetails,
        changePasswordDetails: changePasswordDetails,
        getProfileDetails: getProfileDetails

    }
});