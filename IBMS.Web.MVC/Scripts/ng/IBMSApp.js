'use strict';
var loginApp = angular.module('LoginApp', []);

var ibmsApp = angular.module('IBMSAPP', ['ui.tinymce', 'ui.bootstrap', 'validation.match', 'selectize', 'chart.js']);

ibmsApp.factory('AuthService', function ($http) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
    };

    var getCurrentUser = function () {
        return $http.post('/Login/CheckAuthentication', null, config).then(function (results) {
            return results.data;
        }, function (data) {
        })
    };

    return {
        getCurrentUser: getCurrentUser
    };
});

loginApp.run(function ($rootScope) {
    //$rootScope.serviceURL = 'http://192.168.1.5:9810/';
    //$rootScope.serviceURL = 'http://192.168.1.5:9810/';
    //$rootScope.serviceURL = 'http://localhost:9810/';
   // $rootScope.serviceURL = 'http://localhost:39690/';
    //
    $rootScope.serviceURL = 'http://localhost:39705/';
    //$rootScope.serviceURL = 'http://localhost:9810/';
    $rootScope.authKey = 'Basic QWRtaW5JQk1TOmlibXNJcyMyMDE4';
});

ibmsApp.run(function ($rootScope) {
    //$rootScope.serviceURL = 'http://192.168.1.5:9810/';
    //$rootScope.serviceURL = 'http://localhost:9810/';
    //$rootScope.serviceURL = 'http://localhost:36705/';
    $rootScope.serviceURL = 'http://localhost:39705/';
    $rootScope.authKey = 'Basic QWRtaW5JQk1TOmlibXNJcyMyMDE4';
});
