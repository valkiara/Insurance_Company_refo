'use strict';

ibmsApp.factory('DocCategoryService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var getAvailableDocCategory = function () {
        return $http.post($rootScope.serviceURL + 'api/Document/GetAllDocumentCategories', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var saveDocCategory = function (docObj, cuObj) {
        var params = $.param({ "CategoryName": docObj.DocCategory, "UserID": cuObj.UserID });
        return $http.post($rootScope.serviceURL + 'api/Document/SaveDocumentCategory', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var updateDOC = function (docCategoryID, docCategoryName, UserID) {
        var params = $.param({ "DocCategoryID": docCategoryID, "CategoryName": docCategoryName, "UserID": UserID });
        return $http.post($rootScope.serviceURL + 'api/Document/UpdateDocumentCategory', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };
    var deleteDOC = function (DocID) {
        var params = $.param({ "DocCategoryID": DocID });
        return $http.post($rootScope.serviceURL + 'api/Document/DeleteDocumentCategory', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    return {

        getAvailableDocCategory: getAvailableDocCategory,
        saveDocCategory: saveDocCategory,
        updateDOC: updateDOC,
        deleteDOC: deleteDOC
    };
});