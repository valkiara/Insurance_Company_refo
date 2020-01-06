'use strict';

ibmsApp.controller("ProfileController", function ($scope, $http, ProfileService, $location, AuthService) {
    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
            $scope.getUserProfileDetails();
        });
    };
    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': 'Basic VGVzdDoxMjM=' }
        
    };
    $scope.init = function () {
        $scope.userObj = {};
        $scope.passwordObj = {};
        $scope.getCurrentUser();
        
        loadDesignation();
    };
    function loadDesignation() {
        $scope.showLoader = true;
        ProfileService.getAvailableDesignation().then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.gridOptionsDesignation = results.data;
            }
            else {
                $scope.gridOptionsDesignation = [];
            }
        });
    };

    $scope.getUserProfileDetails = function () {
        $scope.showLoader = true;
        var userID = $scope.currentUser.UserID;
        ProfileService.getProfileDetails(userID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.userObj = results.data;
            }
            else {
                $scope.userObj = {};
               // $scope.userObj.ProfilePic = "";
            }
        });
    };
    



    $scope.updateProfile = function () {
        SuccessUpdate();
}

        function upadate(){
        ProfileService.updateUserDetails($scope.userObj, $scope.currentUser).then(function (results) {
            if (results.status === true) {
                noty({ text: 'Successfully Updated Profile Information Details', layout: 'topCenter', type: 'success' });

                setTimeout(function () { window.location.href = "/User/Profile" }, 2500)

                ClearFields();
            }
            else {
                noty({ text: 'Error Update Profile Information Details', layout: 'topCenter', type: 'error' });
            }

        });
    }

        $scope.changePassword = function () {
            SuccessChange();
        }
    function Update(){
        ProfileService.changePasswordDetails($scope.passwordObj, $scope.currentUser).then(function (results) {
            if (results.status === true) {
                noty({ text: 'Successfully Changed Password Details', layout: 'topCenter', type: 'success' });

                setTimeout(function () { window.location.href = "/User/Profile" }, 2500)

                ClearFields();
            }
            else {
                noty({ text: 'Error Change Password Details', layout: 'topCenter', type: 'error' });
            }
        });
    }

    function SuccessUpdate() {
        //alert("cc");
        noty({
            text: 'Do you want to Update Profile Information Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            upadate();

                            //noty({ text: 'You clicked "Ok" button', layout: 'topRight', type: 'success' });
                        }
                    },
                    {
                        addClass: 'btn btn-danger btn-clean', text: 'Cancel', onClick: function ($noty) {
                            $noty.close();
                            //noty({ text: 'You clicked "Cancel" button', layout: 'topRight', type: 'error' });
                        }
                    }
            ]
        })
    }

    function SuccessChange() {
        //alert("cc");
        noty({
            text: 'Do you want to Change Password Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            Update();

                            //noty({ text: 'You clicked "Ok" button', layout: 'topRight', type: 'success' });
                        }
                    },
                    {
                        addClass: 'btn btn-danger btn-clean', text: 'Cancel', onClick: function ($noty) {
                            $noty.close();
                            //noty({ text: 'You clicked "Cancel" button', layout: 'topRight', type: 'error' });
                        }
                    }
            ]
        })
    }
});