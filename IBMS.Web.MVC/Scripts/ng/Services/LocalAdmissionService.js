'use strict';

ibmsApp.factory('LocalAdmissionService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var configEncoded = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };


    var save = function(admission) {

        var params = $.param({ "admission": admission });

        return $http.post($rootScope.serviceURL + 'api/Admission/SaveLocalAdmission',
           params, configEncoded).then(function (results) {
               return results.data;
           }, function (data) {

           });

    }






    var getAllLocalAdmission = function () {
        return $http.post($rootScope.serviceURL + 'api/Admission/GetAllLocalAdmission', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

     var getAdmissionByRefNo = function (referenceNo) {
         //var params = $.param({ "refNo": referenceNo });

         var params = $.param({ "refNo": referenceNo });

         return $http.post($rootScope.serviceURL + 'api/Admission/GetLocalAdmissionByReferenceNo', params, configEncoded).then(function (results) {
            return results.data;
        }, function (data) {

        })
    };

    return {
        save: save,
        getAllLocalAdmission: getAllLocalAdmission,
        getAdmissionByRefNo: getAdmissionByRefNo,
    };

   
    
});