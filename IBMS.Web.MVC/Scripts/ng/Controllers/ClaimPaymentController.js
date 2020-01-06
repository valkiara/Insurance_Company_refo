'use strict';

var ibmsApp = angular.module("IBMSApp", []);

ibmsApp.controller("ClaimPaymentController", function ($scope, $http, ClaimPaymentService, $location,$filter) {

    $scope.init = function () {
        $scope.getAllClaimPayment();
        //$scope.refresh
    };

    var claimHeader = {};
    var claimHeaderLine = [];
    $scope.claimHeader = claimHeader;
    $scope.claimHeaderLine = claimHeaderLine;
    //$scope.paymentDate = new Date();
    $scope.claimAmount = '50000';
    $scope.paymentType = true;
    $scope.ClaimPaymentID = -1;
    $scope.paymentDate = $filter('date')(new Date(), 'MM/dd/yyyy');
    $scope.addPayment = function () {
        //alert('Hi');
        var addpayment;
        $scope.claimHeaderLine.push({
            'ChequeNo': $scope.chequeNo,
            'PaymentTypeID': 1,
            'PaidAmount': $scope.payAmount,
            'PaidDate': $scope.paymentDate,
            'IsFinal': $scope.paymentType
        });
        $scope.chequeNo = '';
        //$scope.PaymentTypeID = '';
        $scope.payAmount = '';
       // $scope.paymentDate = '';
        $scope.paymentType = true;
      
        
        $scope.tpay = $scope.totalPay();
        $scope.addpayment = !addpayment;
       

        alert(angular.toJson($scope.claimHeaderLine));
    };

    $scope.totalPay = function () {
        var total = 0;
        for (var i = 0; i < $scope.claimHeaderLine.length; i++) {
            total += parseInt($scope.claimHeaderLine[i].PaidAmount);
        }
        return total;
    };
    $scope.save = function () {
        $scope.claimHeader = {};
        $scope.claimHeader.ClaimPaymentMethodDetails = [];
        $scope.claimHeader.ClaimRecordingID = 2;
        $scope.claimHeader.ClaimAmount = $scope.claimAmount;
        $scope.claimHeader.Notes = $scope.notes;
        $scope.claimHeader.ClaimPaymentID = $scope.ClaimPaymentID;
      //  $scope.ClaimPaymentID = ClaimPaymentID
        $scope.claimHeader.ClaimPaymentMethodDetails = $scope.claimHeaderLine;
        $scope.claimHeader.tpay = $scope.tpay;
      
      // alert("sdf"+angular.toJson($scope.claimHeader));
   
        if ($scope.claimHeader.ClaimPaymentID !== -1) {

           
        //    $scope.claimPaymentMethodList = [{ "ChequeNo": "12345", "PaymentTypeID": 1, "PaidAmount": 2000, "PaidDate": "02/02/2018", "IsFinal": false }, { "ChequeNo": "12346", "PaymentTypeID": 1, "PaidAmount": 2000, "PaidDate": "02/22/2018", "IsFinal": true }];
           // $scope.claimPayment = { "ClaimPaymentID": 82, "ClaimRecordingID": 2, "ClaimAmount": 4000, "Notes": "Test Note", "ClaimPaymentMethodDetails": $scope.claimPaymentMethodList };






            ClaimPaymentService.updateClaimPayment($scope.claimHeader, 1).then(function (results) {
                if (results.status == true) {

                    alert(angular.toJson(results));
                    setTimeout(function () { window.location.href = "/ClaimPayment/Index" }, 2500)
                }
                else {
                   // alert('failed');
                }
            });
        }
        else {


            ClaimPaymentService.saveClaimPayment($scope.claimHeader, 1).then(function (results) {
                if (results.status == true) {

                    alert(angular.toJson(results));
                    setTimeout(function () { window.location.href = "/ClaimPayment/Index" }, 2500)

                }
               
            });
        }
};

    $scope.edit = function (CH) {
        var addpayment;
      //  alert(angular.toJson(CH));
        $scope.addpayment = !addpayment;
        $scope.claimHeaderLine=[];
        // tab - pane
        //tab-pane active
        $("#tabView").removeClass('active');
        $("#tabEdit").addClass('active');

        $("#tab-first").removeClass('tab-pane active');
        $("#tab-first").addClass('tab-pane ');

        $("#tab-second").removeClass('tab-pane');
        $("#tab-second").addClass('tab-pane active');
        $scope.claimHeader = CH;
        $scope.claimHeader.ClaimRecordingID = 2,
        $scope.claimAmount = CH.ClaimAmount;
        $scope.ClaimPaymentID = CH.ClaimPaymentID;
        $scope.notes = CH.Notes;
        $scope.tpay = CH.PaidAmount;

        // $scope.claimHeaderLine = CH.ClaimPaymentMethodDetails;
        //alert(angular.toJson(CH.ClaimPaymentMethodDetails));
        for (var i = 0; i < CH.ClaimPaymentMethodDetails.length; i++)
        {
            var date = CH.ClaimPaymentMethodDetails[i].PaidDate.split(' ');//$filter('date')(CH.ClaimPaymentMethodDetails[i].PaidDate, 'MM/dd/yyyy');
           // alert(date[0]);
            $scope.claimHeaderLine.push({
               
                'ChequeNo': CH.ClaimPaymentMethodDetails[i].ChequeNo,
                'PaymentTypeID': 1,
                'PaidAmount': CH.ClaimPaymentMethodDetails[i].PaidAmount,
                'PaidDate': $scope.paymentDate,
                'IsFinal': CH.ClaimPaymentMethodDetails[i].IsFinal
            });
        
        
        
        }


        alert(angular.toJson($scope.claimHeaderLine));
       // $scope.save(CH);
    };

    $scope.delete = function (CH) {
        alert(CH.ClaimPaymentID);
    }





 $scope.cancel = function () {
     window.location.href = "/ClaimPayment/Index"
 }
 $scope.getAllClaimPayment = function () {
     $scope.showLoader = true;
     ClaimPaymentService.getAllClaimPayments().then(function (results) {
        // $scope.showLoader = false;
         if (results.status === true) {
             $scope.availableClaimPayment = results.data;
            }
         else {
             $scope.availableClaimPayment = [];
         }
     });
 };
 $scope.refresh = function()
 {
     $scope.getAllClaimPayment();
 }

    
});

