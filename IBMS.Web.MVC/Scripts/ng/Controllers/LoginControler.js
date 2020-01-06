'use strict';

loginApp.controller("LoginControler", function ($scope, $location, LoginService) {
    $scope.init = function () {
        $scope.loginData = {};
        $scope.loginData.BusinessUnitID = "";
        $scope.loadBU();
        $scope.isBusinessUnitAvailable = true;
    };

    $scope.textChange = function () {
        if ($scope.loginData.UserName.toLowerCase() == "admin") {
            $scope.isBusinessUnitAvailable = false;
        }
        else {
            $scope.isBusinessUnitAvailable = true;
        }
    }

    $scope.login = function () {
        $scope.messege = "";
        $scope.showLoader = true;
        LoginService.validateLoginDetails($scope.loginData).then(function (results) {
            if (results.status === true) {
                LoginService.sendLoginDetails(results.data).then(function (results) {
                    if (results.status === true) {
                        location.href = "/Dashboard/Dashboard";
                    }
                });
            }
            else {
                $scope.messege = results.message;
                $scope.showLoader = false;
            }
        });
    };

    $scope.loadBU = function () {
        $scope.showLoader = true;
        LoginService.getAvailableBUDropdown().then(function (results) {
            $scope.showLoader = false;
            $scope.availableBU = results.data;
        });
    };
});