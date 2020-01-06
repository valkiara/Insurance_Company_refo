'use strict';

ibmsApp.controller("SingaporeAdmissionInvoiceController", function ($scope, $http, $rootScope, SingaporeAdmissionInvoiceService) {

    $scope.referenceNo = "reference no 1";
    $scope.invoiceData = {};

    $scope.init = function () {
        $scope.getInvoiceDetailsByRefNo($scope.referenceNo);
    }

    $scope.getInvoiceDetailsByRefNo = function (referenceNo) {
        try {
            $scope.showLoader = true;
            SingaporeAdmissionInvoiceService.getInvoiceDetailsByRefNo(referenceNo).then(function (results) {
                $scope.showLoader = false;
                if (results.status === true) {
                    $scope.invoiceData = results.data;
                    console.log($scope.invoiceData);
                }
                else {

                }
            });

        } catch (e) {
            console.log(e.message);
        }
    };

    function Success() {
        noty({
            text: 'Do you want to Print this Invoice?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            save();
                        }
                    },
                    {
                        addClass: 'btn btn-danger btn-clean', text: 'Cancel', onClick: function ($noty) {
                            $noty.close();
                            $scope.$apply(function () {
                                $scope.showLoader = false;
                            });
                            //noty({ text: 'You clicked "Cancel" button', layout: 'topRight', type: 'error' });
                        }
                    }
            ]
        })
    };

    function save() {
        $scope.showLoader = true;

        SingaporeAdmissionInvoiceService.save($scope.invoiceData).then(function (result) {
            $scope.showLoader = false;

            if (result.status === true) {
                noty({
                    text: 'Admission Invoice Successfully Saved',
                    layout: 'topCenter',
                    type: 'success',
                    buttons: [
                                 {
                                     addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                                         $noty.close();
                                     }
                                 }
                    ]
                });

                $scope.ClearFields();
                $scope.refreshContent();
                
            }
            else {
                noty({
                    text: 'Error Saving Local Admissions Detail.' + " " + result.message,
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
        })
    }

    $scope.Save = function () {
        $scope.showLoader = true;
        Success();
    };

    $scope.ClearFields = function () {

        $scope.admission.ReferenceNo = "";
        $scope.admission.PatientName = "";
        $scope.admission.DateOfBirth = "";
        $scope.admission.PassportNumber = "";
        $scope.admission.Scheme = "";
        $scope.admission.InceptionDate = "";
        $scope.admission.Deductible = "";
        $scope.admission.DeductibleUsedForTheYear = "";
        $scope.admission.Exclusions = "";
        $scope.admission.Hospital = "";
        $scope.admission.AdmissionDate = "";
        $scope.admission.DischargedDate = "";
        $scope.admission.BHTNumber = "";
        $scope.admission.Illness = "";
        $scope.admission.ConsultantName = "";
        $scope.admission.InformedBy = "";
        $scope.admission.GOPAmount = "";
        $scope.admission.GOPConfirmBy = "";
        $scope.admission.GOPIssueDate = "";
        $scope.admission.ExtendedGOP = "";
        $scope.admission.HandledBy = "";
        $scope.admission.FinalAmount = "";
        $scope.admission.ConsultantFee = "";
        $scope.admission.FinalBillRecievedDate = "";
        $scope.admission.FinalBillGivenToSgs = "";
        $scope.admission.ClaimsDcsSentToAviva = "";
        $scope.admission.ClaimSettledDate = "";
        $scope.admission.ReferalFee = "";
        $scope.admission.ReferalFeeReceivedDate = "";
        $scope.admission.ReferalFeeReceivedBank = "";
        $scope.admission.ReferalFeeReceivedChequeNumber = "";
        $scope.admission.ReferalFeeReceivedTtTransfer = "";
        $scope.admission.PaymentGivenToAccount = "";
        $scope.admission.Remark = "";
        $scope.admission.CaseNumber = "";
        $scope.admission.IntimatedDate = "";
    };

});