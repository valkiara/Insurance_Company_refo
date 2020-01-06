'use strict';

ibmsApp.factory('EmailService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var saveEmail = function (emlObj, customer) {
        var params = $.param({
            "UserName": emlObj.Name,
            "EmailContent": emlObj.Messege,
            "EmailAddress": emlObj.email,
            "EmailHeader": emlObj.Header
        });

        return $http.post($rootScope.serviceURL + '/api/Email/SendGeneralEmail', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };


    var GetInsuranceCompanyByID = function (insCompanyID) {

        var params = $.param({ "insCompanyID": insCompanyID });

        return $http.post($rootScope.serviceURL + '/api/InsCompany/GetInsuranceCompanyByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    return {
        saveEmail: saveEmail,
        GetInsuranceCompanyByID: GetInsuranceCompanyByID

    };
});