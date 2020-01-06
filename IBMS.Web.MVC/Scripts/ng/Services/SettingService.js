'use strict';

ibmsApp.factory('SettingService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };
    var getAllSettings = function () {
        return $http.post($rootScope.serviceURL + 'api/Setting/GetAllSettings', null, config).then(function (results) {
            return results.data;
        }, function (data) {
        })
    };

    var getAvailableDesignation = function () {
        return $http.post($rootScope.serviceURL + 'api/Designation/GetAllDesignations', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            
        })
    };

    var saveSettingData = function (designationID, settingCode, settingDesc, userID) {
        var params = {
            "DesignationID": designationID,
            "SettingCode": settingCode,
            "SettingDesc": settingDesc,
            "UserID": userID
        };
        return $http.post($rootScope.serviceURL + 'api/Setting/SaveSetting',
           params, config).then(function (results) {
               return results.data;
           }, function (data) {
               alert("Error : " + data);
           });
    };

    var UpdateSettingData = function (
           settingID, designationID, settingCode, settingDesc, userID) {
        var params = {
            "SettingID": settingID,
            "DesignationID": designationID,
            "SettingCode": settingCode,
            "SettingDesc": settingDesc,
            "UserID": userID
        };
        return $http.post($rootScope.serviceURL + 'api/Setting/UpdateSetting',
           params, config).then(function (results) {
               return results.data;
           }, function (data) {
               alert("Error : " + data);
           });
    };

    var DeleteSettingData = function (settingID) {

        var params = { "SettingID": settingID };
        return $http.post($rootScope.serviceURL + 'api/Setting/DeleteSetting', params, config).then(function (results) {
            return results.data;
        }, function (data) {
           // alert("Error : " + data);
        });
    };

    var getAllSetting = function (BUID) {
        var params = { "BusinessUnitID": BUID };

        return $http.post($rootScope.serviceURL + 'api/Setting/GetAllSettingsByBUID', params, config)
            .then(function (results) {
                
                return results.data;
            }, function (data) {
            })
    };

    return {
        saveSettingData: saveSettingData,
        getAllSettings: getAllSettings,
        UpdateSettingData: UpdateSettingData,
        DeleteSettingData: DeleteSettingData,
        getAvailableDesignation: getAvailableDesignation
    };
});