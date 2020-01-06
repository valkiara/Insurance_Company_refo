'use strict';

ibmsApp.controller("RoleManagementController", function ($scope, $http, RoleManagementService, $location, AuthService) {
    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;

        });
    };
    
    $scope.init = function () {
        //$scope.RoleFunction = {};

        $scope.getCurrentUser();

        loadAccessLevel();
    };
    function loadAccessLevel() {
        $scope.showLoader = true;
        RoleManagementService.getAvailableAccessLevels().then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.gridOptionsAccessLevel = results.data;
            }
            else {
                $scope.gridOptionsAccessLevel = [];
            }
        });
    };

    $scope.refreshContent = function () {
        $scope.RoleFunction = {};
    }

    $scope.getRoleFunction = function (AccessLevelTypeID) {
       // var AccessLevelTypeID = $scope.AccessLevelTypeID;
        $scope.showLoader = true;
        RoleManagementService.getRoleFunctionByID(AccessLevelTypeID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.RoleFunction = results.data;
            }
            else {
                $scope.RoleFunction = [];
            }
        });
    };

    $scope.updateRoleFunction = function () {
       
        $scope.showLoader = true;
        RoleManagementService.updateRoleFunctions($scope.RoleFunction).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                noty({
                       text: 'Successfully Updated Role Function Details',
                       layout: 'topCenter',
                       type: 'success',
                       buttons: [
                                 {
                                     addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                                         $noty.close();
                                       //  setTimeout(function () { window.location.href = "/RoleManagement/RoleManagement" }, 2500)
                                     }
                                 }
                       ]
                   });

                   //setTimeout(function () { window.location.href = "/CommissionHeader/Index" }, 2500)
                   //$scope.ClearFields();
                   $scope.refreshContent();
               }
               else {
                   noty({
                       text: 'Error Update Commission Header Details',
                       layout: 'topCenter',
                       type: 'error',
                       buttons: [
                                  {
                                      addClass: 'btn btn-danger btn-clean', text: 'Ok', onClick: function ($noty) {
                                          $noty.close();
                                      }
                                  }
                       ]
                   });
               }
           
        });
    };
});