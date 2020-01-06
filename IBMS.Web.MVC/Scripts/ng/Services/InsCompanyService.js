'use strict';

ibmsApp.factory('InsCompanyService', function ($http, $rootScope) {

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

    var saveInsCompData = function (businessUnitID,
        insCompanyName, address1, address2,
        address3, contactPerson, contactNo,
        email,
        fax, userID) {

        var params = {
            "businessUnitID": businessUnitID,
            "insCompanyName": insCompanyName,
            "address1": address1,
            "address2": address2,
            "address3": address3,
            "contactPerson": contactPerson,
            "contactNo": contactNo,
            "email": email,
            "fax": fax,
            "userID": userID
        };

        return $http.post($rootScope.serviceURL + 'api/InsCompany/SaveInsuranceCompany',
           params, config).then(function (results) {

               return results.data;
           }, function (data) {

           });
    };


    var UpdateInsCompData = function (
            businessUnitID,
            insCompanyID,
            InCompanyName,
            address1,
            address2,
            address3,
            contactPerson,
            contactNo,
            email,
            fax,
            userID) {

        var params = {
            "businessUnitID": businessUnitID,
            "insCompanyID": insCompanyID,
            "insCompanyName": InCompanyName,
            "address1": address1,
            "address2": address2,
            "address3": address3,
            "contactPerson": contactPerson,
            "contactNo": contactNo,
            "email": email,
            "fax": fax,
            "userID": userID
        };

        return $http.post($rootScope.serviceURL + 'api/InsCompany/UpdateInsuranceCompany',
           params, config).then(function (results) {

               return results.data;
           }, function (data) {

           });
    };

    var DeleteInsCompData = function (insCompanyID) {

        var params = { "insCompanyID": insCompanyID };

        return $http.post($rootScope.serviceURL + 'api/InsCompany/DeleteInsuranceCompany', params, config).then(function (results) {

            return results.data;
        }, function (data) {

        });
    };

    var getAllInsuranceCompanny = function (BUID) {
        var params = { "businessUnitID": BUID };

        return $http.post($rootScope.serviceURL + 'api/InsCompany/GetInsuranceCompaniesByBusinessUnitID', params, config)
            .then(function (results) {

                return results.data;

            }, function (data) {

            })
    };

    return {
        getAllBusinessUnits: getAllBusinessUnits,
        saveInsCompData: saveInsCompData,
        getAllInsuranceCompanny: getAllInsuranceCompanny,
        UpdateInsCompData: UpdateInsCompData,
        DeleteInsCompData: DeleteInsCompData
    };
});