'use strict';

ibmsApp.factory('CommissionHeaderService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var getAvailableBusinessUnit = function () {
        return $http.post($rootScope.serviceURL + 'api/BusinessUnit/GetAllBusinessUnits', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAvailableCommissionHeader = function () {
        return $http.post($rootScope.serviceURL + 'api/ComStructure/GetAllComStructureHeaders', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAvailableCompanyDropdown = function () {
        return $http.post($rootScope.serviceURL + 'api/Company/GetAllCompanies', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAvailableInsCompany = function () {
        return $http.post($rootScope.serviceURL + 'api/InsCompany/GetAllInsuranceCompanies', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAvailablePartner = function () {
        return $http.post($rootScope.serviceURL + 'api/Partner/GetAllPartners', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAvailableInsClass = function () {
        return $http.post($rootScope.serviceURL + 'api/InsClass/GetAllInsClasses', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAvailableInsSubClass = function (insClassID) {
        var params = $.param({ "insClassID": insClassID });
        return $http.post($rootScope.serviceURL + 'api/InsClass/GetAllInsSubClassesByInsClass', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAvailableComStruHeader = function (BUID) {
        var params = $.param({ "BusinessUnitID": BUID });
        return $http.post($rootScope.serviceURL + 'api/ComStructure/GetAllComStructureHeadersByBUID', params, config).then(function (results) {
            //return $http.post(url+'api/ComStructure/GetAllComStructureHeaders', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var saveCommissionHeader = function (CommissionObj, CuObj) {
        var params = $.param({
            "ComStructName": CommissionObj.ComStruName, "BUID": CommissionObj.buID, "PartnerID": CommissionObj.PartnerID, "InsCompanyID": CommissionObj.InsCompanyID,
            "InsClassID": CommissionObj.InsuranceClassID, "InsSubClassID": CommissionObj.InsuranceSubClassID, "UserID": CuObj.UserID
        });
        return $http.post($rootScope.serviceURL + 'api/ComStructure/SaveComStructureHeader', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var updateCommissionHeader = function (ComStructID, CommissionHeaderName, buID, PartnerID, InsCompanyID, InsuranceClassID, InsuranceSubClassID, UserID) {
        var params = $.param({
            "ComStructID": ComStructID, "ComStructName": CommissionHeaderName, "BUID": buID, "PartnerID": PartnerID, "InsCompanyID": InsCompanyID, "InsClassID": InsuranceClassID,
            "InsSubClassID": InsuranceSubClassID, "UserID": UserID
        });
        return $http.post($rootScope.serviceURL + 'api/ComStructure/UpdateComStructureHeader', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var deleteCommissionHeader = function (ComStructID) {
        var params = $.param({ "ComStructID": ComStructID });
        return $http.post($rootScope.serviceURL + 'api/ComStructure/DeleteComStructureHeader', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };


    var getAvailableCommisionStructure = function () {
        return $http.post($rootScope.serviceURL + 'api/ComStructure/GetAllComStructureHeaders', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var addComStructLine = function (comObj, CuObj) {
        var params = $.param({ "ComStructID": comObj.CommissionStructureID, "RateCategoryID": comObj.RateCategoryID, "IsAgeConsider": comObj.IsAgeConsider, "AgeFrom": comObj.AgeFrom, "AgeTo": comObj.AgeTo, "isFixed": comObj.IsFixed, "RateValue": comObj.RateValue, "UserID": CuObj.UserID });// "UserID": 1
        return $http.post($rootScope.serviceURL + 'api/ComStructure/SaveComStructureLine', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };


    var getAvailableRateCategory = function () {
        return $http.post($rootScope.serviceURL + 'api/RateCategory/GetAllRateCategories', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    return {
        getAvailableCompanyDropdown: getAvailableCompanyDropdown,
        saveCommissionHeader: saveCommissionHeader,
        getAvailablePartner: getAvailablePartner,
        getAvailableInsClass: getAvailableInsClass,
        getAvailableInsCompany: getAvailableInsCompany,
        getAvailableInsSubClass: getAvailableInsSubClass,
        getAvailableBusinessUnit: getAvailableBusinessUnit,
        updateCommissionHeader: updateCommissionHeader,
        getAvailableComStruHeader: getAvailableComStruHeader,
        deleteCommissionHeader: deleteCommissionHeader,
        getAvailableCommissionHeader: getAvailableCommissionHeader,
        getAvailableCommisionStructure: getAvailableCommisionStructure,
        addComStructLine: addComStructLine,
        getAvailableRateCategory: getAvailableRateCategory
    };
});