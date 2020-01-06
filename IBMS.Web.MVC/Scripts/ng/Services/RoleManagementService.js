'use strict';

ibmsApp.factory('RoleManagementService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var getAvailableAccessLevels = function () {
        return $http.post($rootScope.serviceURL + 'api/Role/GetAllAccessLevelTypes', null, config).then(function (results) {
            return results.data;
        }, function (data) {

        })
    };

    var getRoleFunctionByID = function (AccessLevelTypeID) {
        var params = $.param({ "AccessLevelTypeID": AccessLevelTypeID });
        return $http.post($rootScope.serviceURL + 'api/Role/GetRoleFunctionsByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {

        })
    };

    var updateRoleFunctions = function (ObjRole) {
        var params = $.param({ "RoleObj": ObjRole });
        return $http.post($rootScope.serviceURL + 'api/Role/UpdateRoleFunctions', params, config).then(function (results) {
            return results.data;
        }, function (data) {

        })
    };

    return {
        getAvailableAccessLevels: getAvailableAccessLevels,
        getRoleFunctionByID: getRoleFunctionByID,
        updateRoleFunctions: updateRoleFunctions,
    }
});