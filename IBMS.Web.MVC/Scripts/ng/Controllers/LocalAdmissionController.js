/// <reference path="C:\Users\Basith.PERFECTBSS\Source\Workspaces\IBS\IBMS.Web.MVC\Views/LocalAdmission/GetInvoice.cshtml" />
/// <reference path="C:\Users\Basith.PERFECTBSS\Source\Workspaces\IBS\IBMS.Web.MVC\Views/LocalAdmission/GetInvoice.cshtml" />
/// <reference path="C:\Users\Basith.PERFECTBSS\Source\Workspaces\IBS\IBMS.Web.MVC\Views/LocalAdmission/GetInvoice.cshtml" />
'use strict';


ibmsApp.controller("LocalAdmissionController", function ($scope, $http, $rootScope, LocalAdmissionService, $modal) {
    
    $scope.admission = {};
    $scope.paginationTopNumberList = [];
    $scope.isUpdate = false;

    $scope.init = function () {
        //alert("Test");
        getAllLocalAdmission();

    }

    $scope.refreshContent = function () {
        getAllAgent();
        $scope.search_query = "";
    };

    $scope.getFormattedDate = function (date) {
        if (/^\d{2}\/\d{2}\/\d{4}$/.test(date)) {
            return date;
        }
        else {

            //if (date.length > 0) {
            var stringDate = date.getDate() + "";
            var stringMonth = date.getMonth() + 1 + "";
            var stringYear = date.getFullYear() + "";

            if (stringDate.length < 2)
                stringDate = '0' + stringDate;
            if (stringMonth.length < 2)
                stringMonth = '0' + stringMonth;

            return [stringDate, stringMonth, stringYear].join('/');
            // }
        }
    };





    function save() {
        $scope.showLoader = true;
       



        $scope.admission.CurrancyID = 0;
        $scope.admission.CurrancyCode ='NA';
        $scope.admission.CurrencyType = 0;
        $scope.admission.PatientAdmissionId = 0;
        
        $scope.admission.PatientID = 0;
        $scope.admission.TotalAmountPaid=0
        $scope.admission.BUID = 0;
        $scope.admission.DeductionID = 0;
        $scope.admission.CreateBy = 0;
        $scope.admission.ModifiedBy = 0;
        $scope.admission.PremiumHolderType = 0;
        if ($scope.admission.DateOfBirth === undefined || $scope.admission.DateOfBirth === "" || $scope.admission.DateOfBirth === null) {
            $scope.admission.DateOfBirth = "";
        }
        else
            $scope.admission.DateOfBirth = $scope.getFormattedDate($scope.admission.DateOfBirth);


        if ($scope.admission.AdmissionDate === undefined || $scope.admission.AdmissionDate === "" || $scope.admission.AdmissionDate === null) {
            $scope.admission.AdmissionDate = "";
        }
        else
            $scope.admission.AdmissionDate = $scope.getFormattedDate($scope.admission.AdmissionDate);


        if ($scope.admission.ClaimSettledDate === undefined || $scope.admission.ClaimSettledDate === "" || $scope.admission.ClaimSettledDate === null) {
            $scope.admission.ClaimSettledDate = "";
        }
        else
            $scope.admission.ClaimSettledDate = $scope.getFormattedDate($scope.admission.ClaimSettledDate);

        if ($scope.admission.DischargeDate === undefined || $scope.admission.DischargeDate === "" || $scope.admission.DischargeDate === null) {
            $scope.admission.DischargeDate = "";
        }
        else
            $scope.admission.DischargeDate = $scope.getFormattedDate($scope.admission.DischargeDate);

        if ($scope.admission.GOPIssueDate === undefined || $scope.admission.GOPIssueDate === "" || $scope.admission.GOPIssueDate === null) {
            $scope.admission.GOPIssueDate = "";
        }
        else
            $scope.admission.GOPIssueDate = $scope.getFormattedDate($scope.admission.GOPIssueDate);


        if ($scope.admission.DischargedDate === undefined || $scope.admission.DischargedDate === "" || $scope.admission.DischargedDate === null) {
            $scope.admission.DischargedDate = "";
        }
        else
            $scope.admission.DischargedDate = $scope.getFormattedDate($scope.admission.DischargedDate);


        if ($scope.admission.ReferalFeeReceivedDate === undefined || $scope.admission.ReferalFeeReceivedDate === "" || $scope.admission.ReferalFeeReceivedDate === null) {
            $scope.admission.ReferalFeeReceivedDate = "";
        }
        else
            $scope.admission.ReferalFeeReceivedDate = $scope.getFormattedDate($scope.admission.ReferalFeeReceivedDate);



        if ($scope.admission.ClaimDocumentReceivedDate === undefined || $scope.admission.ClaimDocumentReceivedDate === "" || $scope.admission.ClaimDocumentReceivedDate === null) {
            $scope.admission.ClaimDocumentReceivedDate = "";
        }
        else
            $scope.admission.ClaimDocumentReceivedDate = $scope.getFormattedDate($scope.admission.ClaimDocumentReceivedDate);



        if ($scope.admission.ClaimDocumentsEmailedDate === undefined || $scope.admission.ClaimDocumentsEmailedDate === "" || $scope.admission.ClaimDocumentsEmailedDate === null) {
            $scope.admission.ClaimDocumentsEmailedDate = "";
        }
        else
            $scope.admission.ClaimDocumentsEmailedDate = $scope.getFormattedDate($scope.admission.ClaimDocumentsEmailedDate);



        
        if ($scope.admission.PaymentAdviceReceviedDate === undefined || $scope.admission.PaymentAdviceReceviedDate === "" || $scope.admission.PaymentAdviceReceviedDate === null) {
            $scope.admission.PaymentAdviceReceviedDate = "";
        }
        else
            $scope.admission.PaymentAdviceReceviedDate = $scope.getFormattedDate($scope.admission.PaymentAdviceReceviedDate);


        if ($scope.admission.PaymentAdviceEmailedDate === undefined || $scope.admission.PaymentAdviceEmailedDate === "" || $scope.admission.PaymentAdviceEmailedDate === null) {
            $scope.admission.PaymentAdviceEmailedDate = "";
        }
        else
            $scope.admission.PaymentAdviceEmailedDate = $scope.getFormattedDate($scope.admission.PaymentAdviceEmailedDate);


        if ($scope.admission.OriginalDocumentscourieredDate === undefined || $scope.admission.OriginalDocumentscourieredDate === "" || $scope.admission.OriginalDocumentscourieredDate === null) {
            $scope.admission.OriginalDocumentscourieredDate = "";
        }
        else
            $scope.admission.OriginalDocumentscourieredDate = $scope.getFormattedDate($scope.admission.OriginalDocumentscourieredDate);



        if ($scope.admission.FinalBillRecievedDate === undefined || $scope.admission.FinalBillRecievedDate === "" || $scope.admission.FinalBillRecievedDate === null) {
            $scope.admission.FinalBillRecievedDate = "";
        }
        else
            $scope.admission.FinalBillRecievedDate = $scope.getFormattedDate($scope.admission.FinalBillRecievedDate);


        if ($scope.admission.InceptionDate === undefined || $scope.admission.InceptionDate === "" || $scope.admission.InceptionDate === null) {
            $scope.admission.InceptionDate = "";
        }
        else
            $scope.admission.InceptionDate = $scope.getFormattedDate($scope.admission.InceptionDate);


        LocalAdmissionService.save($scope.admission).then(function (result) {








            $scope.showLoader = false;

            if (result.status === true) {
                noty({
                    text: 'Successfully Saved Local Admissions',
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
             //   window.location.assign("GetInvoice");
                // redirect page to invoice
                window.location.assign("LocalAdmission/GetInvoice");
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
    function Success() {
        noty({
            text: 'Do you want to Save Admissions Detail?',
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

    };

    $scope.Save = function () {
        $scope.showLoader = true;
        Success();
    };

     $scope.cancelAdmission = function () {
        $scope.ClearFields();
        $scope.refreshContent();
        $scope.activateAdmissionListTab();
        $scope.isUpdate = false;
    };


    function getAllLocalAdmission() {
        try {
            $scope.paginationTopNumberList = [];
            $scope.showLoader = true;
            LocalAdmissionService.getAllLocalAdmission().then(function (results) {
                $scope.showLoader = false;
                $scope.Admissions = results.data;
                $scope.data = angular.copy($scope.Admissions);
                $scope.viewby = "5";
                $scope.totalItems = $scope.data.length;
                $scope.currentPage = 1;
                $scope.itemsPerPage = $scope.viewby;
                $scope.maxSize = 5; //Number of pager buttons to show
                $scope.setItemsPerPage($scope.viewby);
            });
        } catch (e) {
            console.log(e);
        }
    };

    //$scope.managePaymentDetails = function () {
    //    $scope.showLoader = true;
    //    LocalAdmissionService.getAllLocalAdmission().then(function (results) {

    //    };

    $scope.printDiv = function (divName) {
        var printContents = document.getElementById(divName).innerHTML;
        var popupWin = window.open('', '_blank', 'width=300,height=300');
        popupWin.document.open();
        popupWin.document.write('<html><head><link rel="stylesheet" type="text/css" href="style.css" /></head><body onload="window.print()">' + printContents + '</body></html>');
        popupWin.document.close();
    }

    $scope.manageBankDetails = function (referenceNo) {
            $modal.open({
                templateUrl: 'ngTemplateBank',
                backdrop: 'static',
                windowClass: 'app-modal-window-property',
                controller: [
                        '$scope', '$modalInstance', '$http', function ($scopeChild, $modalInstance, $http) {

                            $scopeChild.availableInvoiceDetails = {};

                        
                            LocalAdmissionService.getAdmissionByRefNo(referenceNo).then(function (results) {
                                //$scope.showLoader = false;
                                if (results.status === true) {
                                    
                                    $scopeChild.availableInvoiceDetails = results.data;

                                }
                                else {
                                    $scopeChild.availableInvoiceDetails = [];
                                }
                            });

                            $modalInstance.close();
                      




                        }
               ],
               });
    };


    $scope.setPage = function (pageNo) {
        $scope.currentPage = pageNo;
    };

    $scope.pageChanged = function () {
        console.log('Page changed to: ' + $scope.currentPage);
    };

    $scope.setItemsPerPage = function (num) {
        $scope.itemsPerPage = num;
        $scope.currentPage = 1; //reset to first page
    };

    $scope.searchTextChange = function (searchText) {
        $scope.data = filterFilter($scope.Agents, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };
    $scope.refreshContent = function () {
        getAllLocalAdmission();
        $scope.search_query = "";
    };

    $scope.getAdmissionByRefNo = function (referenceNo) {
        $scope.activateNewAdmissionTab();

        $scope.showLoader = true;
        LocalAdmissionService.getAdmissionByRefNo(referenceNo).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.isUpdate = true;
                console.log(results.data);
                $scope.admission = results.data;                
            }
            else {
                $scope.isUpdate = false;
            }
        });
    };






    $scope.activateNewAdmissionTab = function () {
        $("#list-tab-1").removeClass('active');
        $("#tab-1").removeClass('tab-pane active');

        $("#list-tab-2").addClass('active');
        $("#tab-2").addClass('tab-pane active');

        $("#tab-1").css("display", "none");
        $("#tab-2").css("display", "block");
    };
    $scope.activateAdmissionListTab = function () {
        $("#list-tab-1").addClass('active');
        $("#tab-1").addClass('tab-pane active');

        $("#list-tab-2").removeClass('active');
        $("#tab-2").removeClass('tab-pane active');

        $("#tab-1").css("display", "block");
        $("#tab-2").css("display", "none");
    };
});

