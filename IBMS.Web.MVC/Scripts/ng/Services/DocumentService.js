'use strict';

ibmsApp.factory('DocumentService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var getAvailableInsSubClass = function () {
        return $http.post($rootScope.serviceURL + 'api/InsClass/GetAllInsSubClasses', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };
    var getAvailableDocuments = function (BUID) {
        var params = $.param({ "BusinessUnitID": BUID });
        return $http.post($rootScope.serviceURL + 'api/Document/GetAllDocuments', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };
    var deleteDoc = function (docID) {
        var params = $.param({ "documentID": docID });
        return $http.post($rootScope.serviceURL + 'api/Document/DeleteDocument', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    return {
        getAvailableInsSubClass: getAvailableInsSubClass,
        getAvailableDocuments: getAvailableDocuments,
        deleteDoc: deleteDoc

    };
});