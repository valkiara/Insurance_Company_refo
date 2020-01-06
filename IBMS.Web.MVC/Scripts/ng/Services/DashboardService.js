'use strict';

ibmsApp.factory('DashboardService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var CountQuotation = function () {

        return $http.post($rootScope.serviceURL + 'api/Dashboard/GetCountQuotation', null, config).then(function (results)
        {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

     var CountClientWithQuotation = function () {

         return $http.post($rootScope.serviceURL + 'api/Dashboard/GetClienVSQuotation', null, config).then(function (results)
        {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };
     

     var CountBusinessUnit = function () {

         return $http.post($rootScope.serviceURL + 'api/Dashboard/GetBusinessUnitCount', null, config).then(function (results) {
             return results.data;
         }, function (data) {
             // Handle error here
         })
     };



     var clientPaymentAmount = function () {

         return $http.post($rootScope.serviceURL + 'api/Dashboard/GetClientPayment', null, config).then(function (results) {
             return results.data;
         }, function (data) {
             // Handle error here
         })
     };
     
     var ClientListCOD = function () {

         return $http.post($rootScope.serviceURL + 'api/Dashboard/GetAllPayment', null, config).then(function (results) {
             return results.data;
         }, function (data) {
             // Handle error here
         })
     };

    return {
        CountQuotation: CountQuotation,
        CountClientWithQuotation: CountClientWithQuotation,
        CountBusinessUnit :CountBusinessUnit,
        clientPaymentAmount: clientPaymentAmount,
        ClientListCOD: ClientListCOD
    };

});