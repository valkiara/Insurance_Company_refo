'use strict';

ibmsApp.controller("PaymentInvoiceController", function ($scope, $http, $rootScope, PaymentService, $location, AuthService, filterFilter, $modal) {

    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;

            if ($scope.currentUser.AccessLevelTypeName === "Admin") {
                //To Do (using a popup to select the desired business unit)
            }
            else {
                $scope.businessUnitID = $scope.currentUser.BusinessUnitID;
                //$scope.loadClientsByBUID($scope.businessUnitID);
                //$scope.loadPaymentDetails($scope.businessUnitID);
                //$scope.loadBanks($scope.businessUnitID);
            }
        });
    };

    $scope.init = function () {

        $scope.PaymentID = "";
        $scope.isPaymentAddMode = true;
        $scope.isCustomerAvailable = false;

        $scope.cusObj = {};
        $scope.paymentObj = {};
        $scope.paymentObj.PaymentAmount = 0;
        $scope.paymentObj.DebitNoteList = [];
        $scope.businessUnitID = "";
        $scope.availablePayments = [];
        $scope.availableClients = [];
        $scope.availableBanks = [];
        $scope.availablePolicyInfoRecordings = [];
        $scope.availableChargeTypes = [];
        $scope.chargeTypeDetails = [];
        $scope.availablePaymentMethods = [];
        $scope.availableCurrencies = [];
        $scope.PaymentID = "";
        $scope.getCurrentUser();
        //  $scope.loadPaymentDetails();
        //$scope.loadPaymentMethods();
        //$scope.loadCurrencyDetails();
        //$scope.addItem();
        //$scope.loadChargeTypes();
        $scope.PaymentID = sessionStorage.getItem("referenceNo")
        $scope.LoadPayment($scope.PaymentID);

    };

    $scope.LoadPayment= function (paymentID) {
       
        $scope.showLoader = true;
        PaymentService.getPaymentByID(paymentID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.paymentObj = results.data;
                $scope.isPaymentAddMode = false;
                $scope.paymentObj.DebitNoteList[0].PolicyInfoRecID = $scope.paymentObj.DebitNoteList[0].PolicyInfoPaymentLists[0].PolicyInfoRecID;
                var PaymentAmount = 0;

                for (var i = 0; i < $scope.paymentObj.DebitNoteList[0].PolicyInfoPaymentLists.length; i++) {


                    PaymentAmount = PaymentAmount + parseInt($scope.paymentObj.DebitNoteList[0].PolicyInfoPaymentLists[i].BankAmount);



                    $scope.paymentObj.TotalPayments = PaymentAmount;
                }
                $scope.paymentObj.PendingPayments = $scope.paymentObj.PaymentAmount - $scope.paymentObj.TotalPayments
                //$scope.loadClientByID($scope.paymentObj.ClientID, true);
            }
         })
        };

});