'use strict';

ibmsApp.factory('SingaporeAdmissionService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var configEncoded = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var save = function (admission) {

        var params = $.param({ "admission": admission });

        return $http.post($rootScope.serviceURL + 'api/Admission/SaveSingaporeAdmission', params, configEncoded).then(function (results) {
               return results.data;
           }, function (data) {

           });

    }

    var getAllLocalAdmission = function () {
        return $http.post($rootScope.serviceURL + 'api/Admission/GetAllSingaporeAdmission', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var getAdmissionByRefNo = function (referenceNo) {
        
        var params = $.param({ "refNo": referenceNo });

        return $http.post($rootScope.serviceURL + 'api/Admission/GetSingaporeAdmissionByReferenceNo', params, configEncoded).then(function (results) {
            return results.data;
        }, function (data) {

        })
    };

    var getInvoiceDetailsByRefNo = function (referenceNo) {
       
        var params = $.param({ "refNo": referenceNo });

        return $http.post($rootScope.serviceURL + 'api/Admission/GetSingaporeInvoiceDetailsByReferenceNo', params, configEncoded).then(function (results) {
            return results.data;
        }, function (data) {

        })
    };

    var loadClientsByBUID = function (ClientID) {
        var params = $.param({ "ClientID": ClientID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetClientByClientID', params, configEncoded).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        })
    };

    var getAllPartners = function () {
        return $http.post($rootScope.serviceURL + 'api/Quotation/GetAllPremiums', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var getClientRequestByID = function (clientReqHeaderID) {
        var params = $.param({ "ClientRequestHeaderID": clientReqHeaderID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetRequestByID', params, configEncoded).then(function (results) {
            return results.data;
        }, function (data) {

        })
    };

    var getClientByID = function (clientID) {
        var params = $.param({ "ClientID": clientID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetClientByID', params, configEncoded).then(function (results) {
            return results.data;
        }, function (data) {

        })
    };

    var getAllHospitals = function () {
        
        return $http.post($rootScope.serviceURL + 'api/Utility/GetAllHospitals', null, config).then(function (results) {
            return results.data;
        }, function (data) {

        })
    };

    var getAllCountries = function () {
        return $http.post($rootScope.serviceURL + 'api/Utility/GetAllCountries', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var getAllDeductions = function (businessUnitID) {
        var params = $.param({ "BusinessUnitID": businessUnitID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetAllDeductionMethods', params, configEncoded).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var getDeductionsID = function (id) {
        var params = $.param({ "id": id });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetDeductionMethodsByID', params, configEncoded).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var GetAllAdmissions = function (businessUnitID) {
        var params = $.param({ "BusinessUnitID": businessUnitID });
        return $http.post($rootScope.serviceURL + 'api/Admission/GetAllAdmissions', params, configEncoded).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        })
    };
    
    var GetAllSingporeAdmissions = function (businessUnitID) {
        var params = $.param({ "BusinessUnitID": businessUnitID });
        return $http.post($rootScope.serviceURL + 'api/Admission/GetAllSingporeAdmissions', params, configEncoded).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        })
    };

    return {

    GetAllAdmissions:GetAllAdmissions,
        getDeductionsID:getDeductionsID,
        getAllDeductions:getAllDeductions,
        getAllCountries:getAllCountries,
        getAllHospitals:getAllHospitals,
        getClientByID:getClientByID,
        getClientRequestByID:getClientRequestByID,
        getAllPartners:getAllPartners,
        loadClientsByBUID: loadClientsByBUID,
        save: save,
        getAllLocalAdmission: getAllLocalAdmission,
        getAdmissionByRefNo: getAdmissionByRefNo,
        getInvoiceDetailsByRefNo: getInvoiceDetailsByRefNo,
        GetAllSingporeAdmissions: GetAllSingporeAdmissions
    };



});