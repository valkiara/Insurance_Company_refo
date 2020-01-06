'use strict';

ibmsApp.factory('RequreDocumentService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var getAvailableInsuranceDropdown = function () {
        return $http.post($rootScope.serviceURL + 'api/InsClass/GetAllInsClasses', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAvailableInsSubClassDropdown = function () {
        return $http.post($rootScope.serviceURL + 'api/InsClass/GetAllInsSubClasses', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAvailableInsSubClassByInsClass = function (insClassID) {
        var params = $.param({ "insClassID": insClassID });
        return $http.post($rootScope.serviceURL + 'api/InsClass/GetAllInsSubClassesByInsClass', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAvailableDocCategoryDropdown = function () {
        return $http.post($rootScope.serviceURL + 'api/Document/GetAllDocumentCategories', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAvailableRequreDocument = function (BUID) {
        var params = $.param({ "BusinessUnitID": BUID });
        return $http.post($rootScope.serviceURL + 'api/Document/GetAllRequiredDocumentsByBUID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var saveReqDocument = function (docObj, cuObj) {
        var params = $.param({ "insClassID": docObj.InsuranceClass, "insSubClassID": docObj.InsuranceSubClass, "DocCategoryID": docObj.DocumentCategory, "DocumentName": docObj.DocumentName, "UserID": cuObj.UserID });
        return $http.post($rootScope.serviceURL + 'api/Document/SaveRequiredDocument', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var updateDOC = function (reqDocumentID, insSubClassID, insClassID, docCategoryID, description, userID) {
        var params = $.param({ "ReqDocID": reqDocumentID, "insClassID": insClassID, "insSubClassID": insSubClassID, "DocCategoryID": docCategoryID, "DocumentName": description, "UserID": userID });
        return $http.post($rootScope.serviceURL + 'api/Document/UpdateRequiredDocument', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var deleteREQ = function (DOCID) {
        var params = $.param({ "ReqDocID": DOCID });
        return $http.post($rootScope.serviceURL + 'api/Document/DeleteRequiredDocument', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    return {
        getAvailableInsuranceDropdown: getAvailableInsuranceDropdown,
        getAvailableInsSubClassDropdown: getAvailableInsSubClassDropdown,
        getAvailableDocCategoryDropdown: getAvailableDocCategoryDropdown,
        getAvailableRequreDocument: getAvailableRequreDocument,
        getAvailableInsSubClassByInsClass: getAvailableInsSubClassByInsClass,
        saveReqDocument: saveReqDocument,
        updateDOC: updateDOC,
        deleteREQ: deleteREQ
    };
});