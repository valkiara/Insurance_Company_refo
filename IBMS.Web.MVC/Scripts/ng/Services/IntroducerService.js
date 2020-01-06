'use strict';

ibmsApp.factory('IntroducerService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var getAllBusinessUnits = function () {
        return $http.post($rootScope.serviceURL + 'api/BusinessUnit/GetAllBusinessUnits', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var saveIntroducerData = function (intruducerName, insCompanyID, BusinessUnits, description,
           address1, address2,
           address3, UserID) {

        var params = {
            // "businessUnitID": businessUnitID,
            "IntroducerName": intruducerName,
            "Description": description,
            "Address1": address1,
            "Address2": address2,
            "Address3": address3,
            "BusinessUnitIDList": BusinessUnits,
            "UserID": UserID

        };

        return $http.post($rootScope.serviceURL + 'api/Introducer/SaveIntroducer',
           params, config).then(function (results) {

               return results.data;
           }, function (data) {

           });
    };


    var UpdateIntroducerData = function (

            intruducerID, introducername, BusinessUnits, description,
           address1, address2,
           address3, UserID) {

        var params = {
            "IntroducerID": intruducerID,
            "IntroducerName": introducername,
            "Description": description,
            "Address1": address1,
            "Address2": address2,
            "Address3": address3,
            "BusinessUnitIDList": BusinessUnits,
            "UserID": UserID

        };

        return $http.post($rootScope.serviceURL + 'api/Introducer/UpdateIntroducer',
           params, config).then(function (results) {

               return results.data;
           }, function (data) {

           });
    };

    var DeleteIntroducerData = function (introducerID) {

        var params = { "IntroducerID": introducerID };

        return $http.post($rootScope.serviceURL + 'api/Introducer/DeleteIntroducer',
            params, config).then(function (results) {

                return results.data;
            }, function (data) {

            });
    };


    var getAllIntroducer = function (BUID) {
        var params = { "BusinessUnitID": BUID };

        return $http.post($rootScope.serviceURL + 'api/Introducer/GetAllIntroducersByBUID', params, config)
            .then(function (results) {
                return results.data;

            }, function (data) {

            })
    };

    return {
        getAllBusinessUnits: getAllBusinessUnits,
        getAllIntroducer: getAllIntroducer,
        saveIntroducerData: saveIntroducerData,
        UpdateIntroducerData: UpdateIntroducerData,
        DeleteIntroducerData: DeleteIntroducerData
    };
});