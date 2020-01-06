'use strict';

ibmsApp.controller("PartnerController", function ($scope, $http, uiGridConstants, PartnerService, $location, AuthService) {
     $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
        });
    };
    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': 'Basic VGVzdDoxMjM=' }

    };

    getAllPartners();
    //loadDesignation();

    $scope.init = function () {
        $scope.getCurrentUser();
        $("#edit" + PartnerID).show();
    };
    $scope.edit = function (Partner) {

        $("#view" + Partner.PartnerID).hide();
        $("#edit" + Partner.PartnerID).show();
    };
    $scope.Delete = function (Partner, Partners, index) {
        alert("fg");
        notyConfirm(Partner, Partners, index);
    };
    $scope.update = function (Partner) {
        $("#view" + Partner.PartnerID).show();
        $("#edit" + Partner.PartnerID).hide();
        Update(Partner);
    };
    $scope.cancel = function (Partner) {
        $("#view" + Partner.PartnerID).show();
        $("#edit" + Partner.PartnerID).hide();

    };

    $scope.Save = function () {

        //var designationID = $scope.DesignationID;

        //var settingCode = $scope.SettingCode;
        var partnerName = $scope.PartnerName;
        var userID = $scope.currentUser.UserID


        PartnerService.savePartnerData(
            partnerName,userID).
           then(function (results) {
               alert(angular.toJson(results.message));
           });
    };
    $scope.refresh = function () {
        getAllPartners();
    };

    function Update(Partner) {
        var partnerID = Partner.PartnerID;
        var partnerName = Partner.PartnerName;
        //var designationID = $scope.DesignationID;

        //var settingDesc = Setting.SettingDesc;

        PartnerService.updatePartnerData(
            partnerID, partnerName).
            then(function (results) {
                alert(angular.toJson(results.message));
            });
    };

    function Delete(Partner) {
        var partnerID = Partner.PartnerID;


        PartnerService.DeletePartnerData(partnerID).
           then(function (results) {
               alert(angular.toJson(results.message));
           });
    };

    function getAllPartners() {
        PartnerService.getAllPartners().then(function (results) {
            $scope.Pats = results.data;
            $scope.Partner = [];

        });
    };

    function getPartnerByID() {
        var id = 1;
        PartnerService.getPartnerByID(id).then(function (results) {
            $scope.Pats = results.data;
        });
    };

    //function loadDesignation() {
    //    $scope.showLoader = true;
    //    SettingService.getAvailableDesignation().then(function (results) {
    //        $scope.showLoader = false;
    //        if (results.status === true) {
    //            $scope.gridOptionsDesignation = results.data;
    //        }
    //        else {
    //            $scope.gridOptionsDesignation = [];
    //        }
    //    });
    //};

    function notyConfirm(Partner, partners, index) {
        var r = confirm("Are You Sure you want Delete this Record of " + Partner.PartnerName);
        if (r == true) {
            Delete(Partner);
            partners.splice(index, 1);
        }
    }
});