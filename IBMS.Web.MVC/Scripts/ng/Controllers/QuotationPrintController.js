'use strict';

ibmsApp.controller("QuotationPrintController", function ($scope, $http, $rootScope, QuotationService, $location, AuthService, filterFilter, $modal) {

    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;

            //if ($scope.currentUser.AccessLevelTypeName === "Admin") {
            //    //To Do (using a popup to select the desired business unit)
            //}
            //else {
            $scope.businessUnitID = $scope.currentUser.BusinessUnitID;
            $scope.loadQuotationHeadersByBUID($scope.businessUnitID);
            //$scope.loadInsSubClassesByBUID($scope.businessUnitID);
            //}
        });
    };


    $scope.init = function () {
        try {

            $scope.isQuotationViewMode = false;
            $scope.isQuotationEditMode = false;
            $scope.businessUnitID = "";
            $scope.cusReqObj = {};
            $scope.cusObj = {};
            $scope.quotationHeaderObj = {};
            $scope.quotHeaderObj = {};
            $scope.selectedInsCompanies = [];
            $scope.QuotationDetailsInsCompanyHeaderDetails = [];
            $scope.quotationHeaderObj.QuotationLineDetails = [];
            $scope.quotHeaderObj.QuotLineDetails = [];
            $scope.AddedInscompanies = [];
            $scope.availableTransaction = [];
            //$scope.loadTransactionTypeDetails();
            $scope.HeaderID = "";
            //$scope.selectedInsCompanyHeaderDetails = [];

            $scope.getCurrentUser();
            $scope.HeaderID = sessionStorage.getItem("Quotation")
            $scope.loadQuotationDetailsByID($scope.HeaderID);
        } catch (e) {
            console.log(e);
        }
    };

    $scope.loadQuotationDetailsByID = function (quotationHeaderID) {
        $scope.showLoader = true;
        QuotationService.loadQuotationHeaderByID(quotationHeaderID).then(function (results) {
            $scope.showLoader = false;
            $scope.quotationHeaderObj = results.data;
            if (results.status === true) {
                $scope.loadClientRequestDetailsByID(results.data);
            }
        });
    };

    $scope.printDiv = function (divName) {
        var printContents = document.getElementById(divName).innerHTML;
        var popupWin = window.open('', '_blank', 'width=300,height=300');
        popupWin.document.open();
        popupWin.document.write('<html><head><link rel="stylesheet" type="text/css" href="style.css" /></head><body onload="window.print()">' + printContents + '</body></html>');
        popupWin.document.close();
    }

});