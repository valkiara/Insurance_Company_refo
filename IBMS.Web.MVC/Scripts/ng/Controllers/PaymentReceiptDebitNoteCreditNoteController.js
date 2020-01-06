'use strict';

var ibmsApp = angular.module("IBMSApp", []);

ibmsApp.controller("PaymentReceiptDebitNoteCreditNoteController", function ($scope,$filter) {

    var DebitHeader = [];
    $scope.DebitHeader = DebitHeader;
    var DebitHeaderSubLine=[];
    $scope.DebitHeaderSubLine = [];
    $scope.Header = [];
    $scope.DebitNote = [];
    $scope.DebitDate = $filter('date')(new Date(), 'MM/dd/yyyy');

    $scope.DebitNote = 'Hello';

    $scope.chargetype = ['SRCC', 'TC', 'TAX'];
    $scope.selectedchargetype = $scope.chargetype[0];
    $scope.crdb = ['CR', 'DB'];
    $scope.selectedcrdb = $scope.crdb[0];
    $scope.policyNumber = '5000';


    $scope.add = function () {

      var line =  $scope.DebitHeaderSubLine.push({
            "Chargetype": $scope.selectedchargetype,
            "Amount": $scope.amount,
            "CRDB": $scope.selectedcrdb
        });
      alert("Line" + angular.toJson($scope.DebitHeaderSubLine));
          };


    $scope.addToHeader = function () {

        $scope.DebitHeader.push({
            "PolicyNumber": $scope.policyNumber,
            "NonCommissionPremium": $scope.nonCommission,
            "GrossPremium": $scope.gross,
            "HeaderLineDetail": $scope.DebitHeaderSubLine
        });

        //$scope.DebitHeaderSubLine = [];
        alert("Head" + angular.toJson($scope.DebitHeader));
        //$scope.refresh();
    };



    $scope.approve = function () {

        $scope.Header.push({

            //"LineDetail": $scope.DebitHeaderSubLine,
            "HeadDetail": $scope.DebitHeader,
            "debitNote": $scope.DebitNote,
            "debitDate": $scope.DebitDate


        });
        alert("HeaderDetails" + angular.toJson($scope.Header));
    };



    $scope.refresh = function()
    {
        window.location.href = "/PaymentReceiptDebitNoteCreditNote/Index" 
    }
 

});