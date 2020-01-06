'use strict';

ibmsApp.controller("ClaimRecordingsController", function ($scope, $http, $rootScope, ClaimRecordingsService, $location, AuthService, filterFilter, $modal) {

    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;

            if ($scope.currentUser.AccessLevelTypeName === "Admin") {
                //To Do (using a popup to select the desired business unit)
            }
            else {
                $scope.businessUnitID = $scope.currentUser.BusinessUnitID;
                //$scope.loadPolicyInfoRecDetails($scope.businessUnitID);
                $scope.loadPolicyRecInfoByBUID($scope.businessUnitID);
                $scope.getAllClaimRecording($scope.businessUnitID);
                $scope.loadInsClass();
            }
        });
    };

    $scope.init = function () {
        $scope.isQuotationAvailable = false;
        $scope.isViewMode = false;
        $scope.cusObj = {};
        $scope.quotationHeaderObj = {};
        $scope.policyInfoRecObj = {};
        //$scope.policyInfoRecObj = {};
        $scope.policyCommissionPayment = {};
        //$scope.quotationHeaderObj = {};
        $scope.isClientReqAddMode = true;
        $scope.policyInfoRecObj.IsOpened = false;
        $scope.policyInfoRecObj.IsRejected = false;
        $scope.policyInfoRecObj.IsWithdrawn = false;
        $scope.businessUnitID = "";
        ///$scope.policyInfoRecObj.ClaimRecHistoryDetails = "";
        $scope.policyInfoRecObj.ClaimRecPendingDocDetails = [];
        $scope.policyInfoRecObj.ClaimRecHistoryDetails = {};
      //  $scope.policyInfoRecObj.policyInfoRecObj.ClaimRecHistoryDetails = { Description: "", RecordingDate: "", Reason: "" };
        
        $scope.availablePolicyInfoRecordings = [];
        $scope.availableDocument= [];
        $scope.availableQuotationHeaders = [];
        $scope.availableQuoteInfoInsCompanyDetails = [];
        $scope.availableCurrencies = [];
        $scope.availableCommissionTypes = [];
        $scope.availableComStructLines = [];
        $scope.policyInfoRecObjs = [];
        $scope.availableInsClass = [];
        

        $scope.getCurrentUser();
        $scope.loadDocumentDetails();
        //$scope.loadCommissionTypes();;
      //  $scope.loadCommissionStructureLines()
        $scope.addItem();
        $scope.availableYears = [];
        $scope.getAvailableYears();
        $scope.availableClaimStatus = [];
        $scope.LoadClaimStatus();
        $scope.availableClaimePaidStatus = [];
        $scope.LoadavailableClaimePaidStatus();
        $scope.loadCurrencyDetails();
        
       
       
        
        
  
        //$scope.availableInsClassType = [];
        //$scope.loadClassType();
    };

    $scope.refreshContent = function () {
        //$scope.loadPaymentDetails();
        //$scope.search_query = "";
    };

    $scope.ClearFields = function () {
        //$scope.isPaymentAddMode = true;
        //$scope.isCustomerAvailable = false;
        // $scope.isViewMode = false;
        // $scope.isClientReqAddMode = true;
        //  $scope.policyInfoRecObj = {};
        //   $scope.policyCommissionPayment = {};
        //$scope.cusObj = {};
        //$scope.paymentObj = {};
        //$scope.paymentObj.PaymentAmount = 0;
        //$scope.paymentObj.DebitNoteList = [];
    };

    $scope.loadDocumentDetails = function () {
        $scope.showLoader = true;
        ClaimRecordingsService.getDocumentDetails().then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableDocument.push({ value: results.data[i].DocumentID, text: results.data[i].DocumentName });
                }
            }
            else {
                $scope.availableDocument = [];
            }
        });
    };

    $scope.getAvailableYears = function () {
        $scope.showLoader = true;
        ClaimRecordingsService.getAvailableYears().then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableYears.push({ value: results.data[i].YearID, text: results.data[i].Description });
                }
            }
            else {
                $scope.availableYears = [];
            }

        });
    };

    $scope.loadInsClass = function () {
        $scope.availableInsClass = [];

        $scope.showLoader = true;
        ClaimRecordingsService.getAllInsClass($scope.currentUser.BusinessUnitID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.availableInsClass = results.data;
            }
            else {
                $scope.availableInsClass = [];
            }

        });
    };

    $scope.loadClassType = function (InsuranceClassID) {

        $scope.getCurrentUser();
        $scope.availableInsClassType = [];

        $scope.showLoader = true;
        ClaimRecordingsService.loadClassType(InsuranceClassID, $scope.currentUser.BusinessUnitID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.availableInsClassType = results.data;
            }
            else {
                $scope.availableInsClassType = [];
            }

        });
    };


    $scope.LoadInsClassType = function (InsuranceClassID) {
        $scope.showLoader = true;
        ClaimRecordingsService.loadClassType(InsuranceClassID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                //for (var i = 0; i < results.data.length; i++) {
                //    $scope.availableInsClassType.push({ value: results.data[i].InsClassTypeID, text: results.data[i].InsClassTypeDes });
                //}
                $scope.availableInsClassType = results.data;
            }
            else {
                $scope.availableInsClassType = [];
            }
        });
    };

    $scope.LoadClaimStatus = function () {
        $scope.showLoader = true;
        ClaimRecordingsService.LoadClaimStatus().then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                //for (var i = 0; i < results.data.length; i++) {
                //    $scope.availableClaimStatus.push({ value: results.data[i].StatusId, text: results.data[i].StatusName });
                //}
                $scope.availableClaimStatus = results.data;
            }
            else {
                $scope.availableClaimStatus = [];
            }
        });
    };



    $scope.LoadavailableClaimePaidStatus = function () {
        $scope.showLoader = true;
        ClaimRecordingsService.LoadavailableClaimePaidStatus().then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                //for (var i = 0; i < results.data.length; i++) {
                //    $scope.availableClaimePaidStatus.push({ value: results.data[i].ClaimPaidStatus, text: results.data[i].ClaimPaidStatusDescription });
                //}
                $scope.availableClaimePaidStatus = results.data;
            }
            else {
                $scope.availableClaimePaidStatus = [];
            }
        });
    };

    $scope.loadCurrencyDetails = function () {
        $scope.showLoader = true;
        ClaimRecordingsService.loadCurrencies().then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableCurrencies.push({ value: results.data[i].CurrencyID, text: results.data[i].CurrencyCode });
                }
            }
            else {
                $scope.availableCurrencies = [];
            }
        });
    };

    $scope.getAllClaimRecording = function (businessUnitID) {
        $scope.showLoader = true;
        ClaimRecordingsService.getAllClaimRecording(businessUnitID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.availablePolicyInfoRecordings = results.data;
                $scope.data = angular.copy($scope.availablePolicyInfoRecordings);
                $scope.viewby = "10";
                $scope.totalItems = $scope.data.length;
                $scope.currentPage = 1;
                $scope.itemsPerPage = $scope.viewby;
                $scope.maxSize = 10; //Number of pager buttons to show

                $scope.setItemsPerPage($scope.viewby);
            }
            else {
                $scope.availablePolicyInfoRecordings = [];
            }
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
        $scope.currentPage = 1;
    };

    $scope.searchTextChange = function (searchText) {
        $scope.data = filterFilter($scope.availablePolicyInfoRecordings, searchText);
        $scope.viewby = "10";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 10;
        $scope.setItemsPerPage($scope.viewby);
    };

    $scope.activatePolicyInfoRecListTab = function () {
        $("#list-tab-1").addClass('active');
        $("#tab-1").addClass('tab-pane active');

        $("#list-tab-2").removeClass('active');
        $("#tab-2").removeClass('tab-pane active');

        $("#tab-1").css("display", "block");
        $("#tab-2").css("display", "none");
    };

    $scope.activatePolicyInfoRecTab = function () {
        $("#list-tab-1").removeClass('active');
        $("#tab-1").removeClass('tab-pane active');

        $("#list-tab-2").addClass('active');
        $("#tab-2").addClass('tab-pane active');

        $("#tab-1").css("display", "none");
        $("#tab-2").css("display", "block");
    };

    $scope.getFormattedDate = function (date) {
        if (/^\d{2}\/\d{2}\/\d{4}$/.test(date)) {
            return date;
        }
        else {
            var stringDate = date.getDate() + "";
            var stringMonth = date.getMonth() + 1 + "";
            var stringYear = date.getFullYear() + "";

            if (stringDate.length < 2)
                stringDate = '0' + stringDate;
            if (stringMonth.length < 2)
                stringMonth = '0' + stringMonth;

            return [stringDate, stringMonth, stringYear].join('/');
        }
    };

    $scope.loadPolicyRecInfoByBUID = function (businessUnitID) {
        ClaimRecordingsService.getAllPolicyInfoRecording(businessUnitID).then(function (results) {
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableQuotationHeaders.push({ value: results.data[i].PolicyInfoRecID, text: results.data[i].PolicyNumber + " (Policy ID: " + results.data[i].PolicyInfoRecID + ")" });
                }
            }
            else {
                $scope.availableQuotationHeaders = [];
            }
        });
    };

    

    

    $scope.loadClientRequestDetailsByID = function (quotationObj) {
        $scope.ClearFields();
        $scope.isQuotationAvailable = true;

        $scope.quotationHeaderObj = quotationObj;

        $scope.showLoader = true;
        ClaimRecordingsService.getClientRequestByID($scope.quotationHeaderObj.ClientRequestHeaderID).then(function (results) {
            //$scope.showLoader = false;
            if (results.status === true) {
                $scope.cusReqObj = results.data;

                PolicyInfoRecService.getClientByID($scope.quotationHeaderObj.ClientID).then(function (results) {
                    $scope.showLoader = false;
                    if (results.status === true) {
                        $scope.cusObj = results.data;
                    }
                    else {
                        $scope.cusObj = {};
                    }
                });
            }
            else {
                $scope.cusReqObj = {};
            }
        });
    };

    $scope.loadQuoteInfoInsCompanyLineDetails = function (quotationHeaderID) {
        $scope.availableQuoteInfoInsCompanyDetails = [];

        //$scope.showLoader = true;
        ClaimRecordingsService.loadQuoteInfoInsCompanyLineDetailsByQuotation(quotationHeaderID).then(function (results) {
            //$scope.showLoader = false;
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableQuoteInfoInsCompanyDetails.push({ value: results.data[i].QuoteInfoInsCompanyLineID, text: results.data[i].InsuranceCompanyName + " ( " + results.data[i].InsSubClassName + " - " + results.data[i].QuoteInfoInsCompanyLineID + " )" });
                }
            }
            else {
                $scope.availableQuoteInfoInsCompanyDetails = [];
            }
        });
    };

    $scope.getClientDetailsByID = function (quotationObj) {
        $scope.ClearFields();
        $scope.isQuotationAvailable = true;

        $scope.quotationHeaderObj = quotationObj;

        $scope.showLoader = true;
        ClaimRecordingsService.getQuatationHeader($scope.quotationHeaderObj.QuotationHeaderID).then(function (results) {
            //$scope.showLoader = false;
            if (results.status === true) {
                $scope.cusReqObj = results.data;

                ClaimRecordingsService.getClientByID($scope.cusReqObj.ClientID).then(function (results) {
                    $scope.showLoader = false;
                    if (results.status === true) {
                        $scope.cusObj = results.data;
                    }
                    else {
                        $scope.cusObj = {};
                    }
                });
            }
            else {
                $scope.cusReqObj = {};
            }
        });
    };

    $scope.getClientDetailsByIDs = function (quotationObj) {
        $scope.ClearFields();
        $scope.isQuotationAvailable = true;
        $scope.quotationHeaderObj.PolicyInfoRecID = quotationObj.PolicyInfoRecID;
        $scope.quotationHeaderObj = quotationObj;

        $scope.showLoader = true;
        ClaimRecordingsService.loadQuotationHeaderByID($scope.quotationHeaderObj.PolicyInfoRecID).then(function (results) {
            //$scope.showLoader = false;
            if (results.status === true) {
                $scope.cusReqObjs = results.data;
                ClaimRecordingsService.getQuatationHeader($scope.cusReqObjs.QuotationHeaderID).then(function (results) {
                    //$scope.showLoader = false;
                    if (results.status === true) {
                        $scope.cusReqObj = results.data;

                        ClaimRecordingsService.getClientByID($scope.cusReqObj.ClientID).then(function (results) {
                            $scope.showLoader = false;
                            if (results.status === true) {
                                $scope.cusObj = results.data;
                            }
                            else {
                                $scope.cusObj = {};
                            }
                        });
                    }
                    else {
                        $scope.cusReqObj = {};
                    }
                });
            }
            else {
                $scope.cusReqObjs = {};
            }
        });
    };

    $scope.loadQuotationDetailsByID = function (quotationHeaderID) {
        $scope.showLoader = true;
        ClaimRecordingsService.loadQuotationHeaderByID(quotationHeaderID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.getClientDetailsByID(results.data);
                //$scope.loadClientRequestDetailsByID(results.data);
                //$scope.loadQuoteInfoInsCompanyLineDetails(quotationHeaderID);
            }
        });
    };

    $scope.changeQuotation = function () {
        $scope.isQuotationAvailable = false;

        $scope.cusObj = {};
        $scope.cusReqObj = {};
        $scope.quotationHeaderObj = {};

        $scope.availableQuoteInfoInsCompanyDetails = [];
    };

    

    $scope.addItem = function () {
        $scope.policyInfoRecObj.ClaimRecPendingDocDetails.push({ DocumentID: "" });
    };

    $scope.deleteItem = function (deleteIndex) {
        $scope.policyInfoRecObj.ClaimRecPendingDocDetails.splice(deleteIndex, 1);
    };

    $scope.activateNewClientRequestTab = function () {
        $("#list-tab-1").removeClass('active');
        $("#tab-1").removeClass('tab-pane active');

        $("#list-tab-2").addClass('active');
        $("#tab-2").addClass('tab-pane active');

        $("#tab-1").css("display", "none");
        $("#tab-2").css("display", "block");
    };

    $scope.editClaimRecording = function (ClaimRecordingID, PolicyInfoRecID) {
        $scope.activateNewClientRequestTab();

       
        $scope.availableQuotationHeaders=[];
        $scope.availableQuotationHeaders=PolicyInfoRecID;
        



       
        $scope.showLoader = true;
        ClaimRecordingsService.editClaimRecording(ClaimRecordingID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.isViewMode = false;
                $scope.isClientReqAddMode = false;
                $scope.isQuotationAvailable = true;
                $scope.policyInfoRecObj = results.data;
                // $scope.policyInfoRecObj.PartnerID = $scope.cusReqObj.PartnerID + "";
                $scope.getClientDetailsByIDs(results.data);
                // $scope.loadQuotationDetailsByID(PolicyInfoRecID);
                //$scope.loadClientByID(clientID);
                //$scope.loadQuotationDetailsByID();
            }
            else {
                $scope.policyInfoRecObj = {};
            }
        });
    };

    $scope.loadClientByID = function (clientID) {
        $scope.showLoader = true;
        ClaimRecordingsService.getClientByID(clientID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.isQuotationAvailable = true;
                $scope.isCustomerAdded = false;
                $scope.isCustomerUpdated = false;

                $scope.cusObj = results.data;
                $scope.cusObj.HomeCountryID = results.data.HomeCountryID + "";
                $scope.cusObj.ResidentCountryID = results.data.ResidentCountryID + "";
            }
            else {
                $scope.isQuotationAvailable = false;
                $scope.isCustomerAdded = false;
                $scope.isCustomerUpdated = false;
                $scope.cusObj = {};
            }
        });
    };

    $scope.updatePolicyInfoRecording = function () {
        $scope.showLoader = true;
        noty({
            text: 'Do you want to Update Claim Recording Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();

                            //if ($scope.cusObj.FamilyDiscount === undefined || $scope.cusObj.FamilyDiscount === "" || $scope.cusObj.FamilyDiscount === null) {
                            //    $scope.cusObj.FamilyDiscount = 0;
                            //}
                            //$scope.cusObj.BusinessUnitID = $scope.businessUnitID;
                            //$scope.policyInfoRecObj.DateOfLoss = $scope.getFormattedDate($scope.policyInfoRecObj.DateOfLoss);
                            //$scope.policyInfoRecObj.DateOfIntimation = $scope.getFormattedDate($scope.policyInfoRecObj.DateOfIntimation);
                            //$scope.policyInfoRecObj.ClaimRecHistoryDetails.RecordingDate = $scope.getFormattedDate($scope.policyInfoRecObj.ClaimRecHistoryDetails.RecordingDate);
                            //$scope.policyInfoRecObj.PolicyInfoRecID = $scope.quotationHeaderObj.PolicyInfoRecID;
                            //$scope.cusObj.BusinessUnitID = $scope.businessUnitID;
                            //$scope.cusReqObj.RequestedDate = $scope.getFormattedDate($scope.cusReqObj.RequestedDate);


                            if ($scope.policyInfoRecObj.ClaimNo === undefined || $scope.policyInfoRecObj.ClaimNo === "" || $scope.policyInfoRecObj.ClaimNo === null) {
                                $scope.policyInfoRecObj.ClaimNo = "";
                            }
                            if ($scope.policyInfoRecObj.Year === undefined || $scope.policyInfoRecObj.Year === "" || $scope.policyInfoRecObj.Year === null) {
                                $scope.policyInfoRecObj.Year = 0;
                            }
                            if ($scope.policyInfoRecObj.FileReferenceNo === undefined || $scope.policyInfoRecObj.FileReferenceNo === "" || $scope.policyInfoRecObj.FileReferenceNo === null) {
                                $scope.policyInfoRecObj.FileReferenceNo = "";
                            }
                            if ($scope.policyInfoRecObj.Insurer === undefined || $scope.policyInfoRecObj.Insurer === "" || $scope.policyInfoRecObj.Insurer === null) {
                                $scope.policyInfoRecObj.Insurer = "";
                            }
                            if ($scope.policyInfoRecObj.VehicleNumber === undefined || $scope.policyInfoRecObj.VehicleNumber === "" || $scope.policyInfoRecObj.VehicleNumber === null) {
                                $scope.policyInfoRecObj.VehicleNumber = "";
                            }
                            if ($scope.policyInfoRecObj.PolicyExcess === undefined || $scope.policyInfoRecObj.PolicyExcess === "" || $scope.policyInfoRecObj.PolicyExcess === null) {
                                $scope.policyInfoRecObj.PolicyExcess = 0;
                            }

                            if ($scope.policyInfoRecObj.InsuranceClassID === undefined || $scope.policyInfoRecObj.InsuranceClassID === "" || $scope.policyInfoRecObj.InsuranceClassID === null) {
                                $scope.policyInfoRecObj.InsuranceClassID = 0;
                            }
                            if ($scope.policyInfoRecObj.InsClassTypeID === undefined || $scope.policyInfoRecObj.InsClassTypeID === "" || $scope.policyInfoRecObj.InsClassTypeID === null) {
                                $scope.policyInfoRecObj.InsClassTypeID = 0;
                            }

                            if ($scope.policyInfoRecObj.DateOfIntimation === undefined || $scope.policyInfoRecObj.DateOfIntimation === "" || $scope.policyInfoRecObj.DateOfIntimation === null) {
                                $scope.policyInfoRecObj.DateOfIntimation = "";
                            }
                            if ($scope.policyInfoRecObj.DateOfLoss === undefined || $scope.policyInfoRecObj.DateOfLoss === "" || $scope.policyInfoRecObj.DateOfLoss === null) {
                                $scope.policyInfoRecObj.DateOfLoss = "";
                            }

                            if ($scope.policyInfoRecObj.CauseOfLoss === undefined || $scope.policyInfoRecObj.CauseOfLoss === "" || $scope.policyInfoRecObj.CauseOfLoss === null) {
                                $scope.policyInfoRecObj.CauseOfLoss = "";
                            }
                            if ($scope.policyInfoRecObj.ClaimStatus === undefined || $scope.policyInfoRecObj.ClaimStatus === "" || $scope.policyInfoRecObj.ClaimStatus === null) {
                                $scope.policyInfoRecObj.ClaimStatus = 0;
                            }

                            if ($scope.policyInfoRecObj.AmountClaimed === undefined || $scope.policyInfoRecObj.AmountClaimed === "" || $scope.policyInfoRecObj.AmountClaimed === null) {
                                $scope.policyInfoRecObj.AmountClaimed = "";
                            }
                            if ($scope.policyInfoRecObj.SumAssuredCurrencyTypeID === undefined || $scope.policyInfoRecObj.SumAssuredCurrencyTypeID === "" || $scope.policyInfoRecObj.SumAssuredCurrencyTypeID === null) {
                                $scope.policyInfoRecObj.SumAssuredCurrencyTypeID = 0;
                            }

                            if ($scope.policyInfoRecObj.RateAmountClaimed === undefined || $scope.policyInfoRecObj.RateAmountClaimed === "" || $scope.policyInfoRecObj.RateAmountClaimed === null) {
                                $scope.policyInfoRecObj.RateAmountClaimed = 0;
                            }
                            if ($scope.policyInfoRecObj.ChequeNo === undefined || $scope.policyInfoRecObj.ChequeNo === "" || $scope.policyInfoRecObj.ChequeNo === null) {
                                $scope.policyInfoRecObj.ChequeNo = "";
                            }

                            if ($scope.policyInfoRecObj.DamageDescription === undefined || $scope.policyInfoRecObj.DamageDescription === "" || $scope.policyInfoRecObj.DamageDescription === null) {
                                $scope.policyInfoRecObj.DamageDescription = "";
                            }
                            if ($scope.policyInfoRecObj.AmountPaid === undefined || $scope.policyInfoRecObj.AmountPaid === "" || $scope.policyInfoRecObj.AmountPaid === null) {
                                $scope.policyInfoRecObj.AmountPaid = 0;
                            }

                            if ($scope.policyInfoRecObj.ClaimPaidStatus === undefined || $scope.policyInfoRecObj.ClaimPaidStatus === "" || $scope.policyInfoRecObj.ClaimPaidStatus === null) {
                                $scope.policyInfoRecObj.ClaimPaidStatus = 0;
                            }
                            if ($scope.policyInfoRecObj.FileId === undefined || $scope.policyInfoRecObj.FileId === "" || $scope.policyInfoRecObj.FileId === null) {
                                $scope.policyInfoRecObj.FileId = 0;
                            }
                            if ($scope.policyInfoRecObj.IsOpened === undefined || $scope.policyInfoRecObj.IsOpened === "" || $scope.policyInfoRecObj.IsOpened === null) {
                                $scope.policyInfoRecObj.IsOpened = false;
                            }
                            if ($scope.policyInfoRecObj.DateOfOpen === undefined || $scope.policyInfoRecObj.DateOfOpen === "" || $scope.policyInfoRecObj.DateOfOpen === null) {
                                $scope.policyInfoRecObj.DateOfOpen = "";
                            }
                            if ($scope.policyInfoRecObj.DateOfReject === undefined || $scope.policyInfoRecObj.DateOfReject === "" || $scope.policyInfoRecObj.DateOfReject === null) {
                                $scope.policyInfoRecObj.DateOfReject = "";
                            }
                            if ($scope.policyInfoRecObj.FileId === undefined || $scope.policyInfoRecObj.FileId === "" || $scope.policyInfoRecObj.FileId === null) {
                                $scope.policyInfoRecObj.FileId = 0;
                            }
                            if ($scope.policyInfoRecObj.IsWithdrawn === undefined || $scope.policyInfoRecObj.IsWithdrawn === "" || $scope.policyInfoRecObj.IsWithdrawn === null) {
                                $scope.policyInfoRecObj.IsWithdrawn = false;
                            }
                            if ($scope.policyInfoRecObj.IsRejected === undefined || $scope.policyInfoRecObj.IsRejected === "" || $scope.policyInfoRecObj.IsRejected === null) {
                                $scope.policyInfoRecObj.IsRejected = false;
                            }
                            if ($scope.policyInfoRecObj.DateOfWithdraw === undefined || $scope.policyInfoRecObj.DateOfWithdraw === "" || $scope.policyInfoRecObj.DateOfWithdraw === null) {
                                $scope.policyInfoRecObj.DateOfWithdraw = "";
                            }
                            if ($scope.policyInfoRecObj.ContactPersonDetails === undefined || $scope.policyInfoRecObj.ContactPersonDetails === "" || $scope.policyInfoRecObj.ContactPersonDetails === null) {
                                $scope.policyInfoRecObj.ContactPersonDetails = "";
                            }
                            if ($scope.policyInfoRecObj.Other === undefined || $scope.policyInfoRecObj.Other === "" || $scope.policyInfoRecObj.Other === null) {
                                $scope.policyInfoRecObj.Other = "";
                            }


                            if ($scope.policyInfoRecObj.ClaimRecHistoryDetails.Description === undefined || $scope.policyInfoRecObj.ClaimRecHistoryDetails.Description === "" || $scope.policyInfoRecObj.ClaimRecHistoryDetails.Description === null) {
                                $scope.policyInfoRecObj.ClaimRecHistoryDetails.Description = "";
                            }
                            if ($scope.policyInfoRecObj.ClaimRecHistoryDetails.RecordingDate === undefined || $scope.policyInfoRecObj.ClaimRecHistoryDetails.RecordingDate === "" || $scope.policyInfoRecObj.ClaimRecHistoryDetails.RecordingDate === null) {
                                $scope.policyInfoRecObj.ClaimRecHistoryDetails.RecordingDate = "";
                            }
                            if ($scope.policyInfoRecObj.ClaimRecHistoryDetails.Reason === undefined || $scope.policyInfoRecObj.ClaimRecHistoryDetails.Reason === "" || $scope.policyInfoRecObj.ClaimRecHistoryDetails.Reason === null) {
                                $scope.policyInfoRecObj.ClaimRecHistoryDetails.Reason = "";
                            }


                            $scope.policyInfoRecObj.ClaimRecPendingDocDetails.ClaimRecPendingDocID = 1;
                            $scope.policyInfoRecObj.ClaimRecPendingDocDetails.DocumentID = 1;
                            $scope.policyInfoRecObj.ClaimRecPendingDocDetails.CreatedBy = 18;
                            $scope.policyInfoRecObj.ClaimRecPendingDocDetails.CreatedDate = "";
                            $scope.policyInfoRecObj.ClaimRecPendingDocDetails.ModifiedBy = 18;
                            $scope.policyInfoRecObj.ClaimRecPendingDocDetails.ModifiedDate = "";




                            //$scope.cusObj.BusinessUnitID = $scope.businessUnitID;
                            $scope.policyInfoRecObj.BusinessUnitID = $scope.businessUnitID;
                            $scope.policyInfoRecObj.DateOfLoss = $scope.getFormattedDate($scope.policyInfoRecObj.DateOfLoss);
                            $scope.policyInfoRecObj.DateOfIntimation = $scope.getFormattedDate($scope.policyInfoRecObj.DateOfIntimation);

                            $scope.policyInfoRecObj.DateOfOpen = $scope.getFormattedDate($scope.policyInfoRecObj.DateOfOpen);
                            $scope.policyInfoRecObj.DateOfWithdraw = $scope.getFormattedDate($scope.policyInfoRecObj.DateOfWithdraw);
                            $scope.policyInfoRecObj.DateOfReject = $scope.getFormattedDate($scope.policyInfoRecObj.DateOfReject);
                            $scope.policyInfoRecObj.PolicyInfoRecID = $scope.quotationHeaderObj.PolicyInfoRecID;
                            $scope.policyInfoRecObj.ClaimRecHistoryDetails.RecordingDate = $scope.getFormattedDate($scope.policyInfoRecObj.ClaimRecHistoryDetails.RecordingDate);



                            ClaimRecordingsService.updateClaimRecording($scope.policyInfoRecObj, $scope.currentUser.UserID).then(function (results) {
                                $scope.showLoader = false;
                                //alert(angular.toJson(results));
                                if (results.status === true) {
                                    noty({
                                        text: 'Successfully Updated Claim Recording Details',
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
                                    $scope.isPaymentAddMode = true;
                                    $scope.isCustomerAvailable = false;
                                    $scope.isViewMode = false;
                                    $scope.isClientReqAddMode = true;
                                    $scope.policyInfoRecObj = {};
                                    $scope.policyCommissionPayment = {};
                                    $scope.cusObj = {};
                                    $scope.paymentObj = {};
                                    $scope.paymentObj.PaymentAmount = 0;
                                    $scope.paymentObj.DebitNoteList = [];
                                    $scope.refreshContent();
                                    $scope.activateClientRequestListTab();
                                }
                                else {
                                    noty({
                                        text: 'Error Updating Claim Recording Details',
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

                        }
                    },
                    {
                        addClass: 'btn btn-danger btn-clean', text: 'Cancel', onClick: function ($noty) {
                            $noty.close();
                            $scope.$apply(function () {
                                $scope.showLoader = false;
                            });
                        }
                    }
            ]
        })
    };

    $scope.savePolicyInfoRecording = function () {
        $scope.showLoader = true;
        noty({
            text: 'Do you want to Save Claim Recording Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();

                            //if ($scope.cusObj.FamilyDiscount === undefined || $scope.cusObj.FamilyDiscount === "" || $scope.cusObj.FamilyDiscount === null) {
                            //    $scope.cusObj.FamilyDiscount = 0;
                            //}
                            //var array = $.map($scope.policyInfoRecObj, function (value, index) {
                            //    return [value];
                            //});
                            if ($scope.policyInfoRecObj.ClaimNo === undefined || $scope.policyInfoRecObj.ClaimNo === "" || $scope.policyInfoRecObj.ClaimNo === null) {
                                $scope.policyInfoRecObj.ClaimNo = "";
                            }
                            if ($scope.policyInfoRecObj.Year === undefined || $scope.policyInfoRecObj.Year === "" || $scope.policyInfoRecObj.Year === null) {
                                $scope.policyInfoRecObj.Year = 0;
                            }
                            if ($scope.policyInfoRecObj.FileReferenceNo === undefined || $scope.policyInfoRecObj.FileReferenceNo === "" || $scope.policyInfoRecObj.FileReferenceNo === null) {
                                $scope.policyInfoRecObj.FileReferenceNo = "";
                            }
                            if ($scope.policyInfoRecObj.Insurer === undefined || $scope.policyInfoRecObj.Insurer === "" || $scope.policyInfoRecObj.Insurer === null) {
                                $scope.policyInfoRecObj.Insurer = "";
                            }
                            if ($scope.policyInfoRecObj.VehicleNumber === undefined || $scope.policyInfoRecObj.VehicleNumber === "" || $scope.policyInfoRecObj.VehicleNumber === null) {
                                $scope.policyInfoRecObj.VehicleNumber = "";
                            }
                            if ($scope.policyInfoRecObj.PolicyExcess === undefined || $scope.policyInfoRecObj.PolicyExcess === "" || $scope.policyInfoRecObj.PolicyExcess === null) {
                                $scope.policyInfoRecObj.PolicyExcess =0;
                            }
                           
                            if ($scope.policyInfoRecObj.InsuranceClassID === undefined || $scope.policyInfoRecObj.InsuranceClassID === "" || $scope.policyInfoRecObj.InsuranceClassID === null) {
                                $scope.policyInfoRecObj.InsuranceClassID = 0;
                            }
                            if ($scope.policyInfoRecObj.InsClassTypeID === undefined || $scope.policyInfoRecObj.InsClassTypeID === "" || $scope.policyInfoRecObj.InsClassTypeID === null) {
                                $scope.policyInfoRecObj.InsClassTypeID = 0;
                            }

                            if ($scope.policyInfoRecObj.DateOfIntimation === undefined || $scope.policyInfoRecObj.DateOfIntimation === "" || $scope.policyInfoRecObj.DateOfIntimation === null) {
                                $scope.policyInfoRecObj.DateOfIntimation = "";
                            }
                            if ($scope.policyInfoRecObj.DateOfLoss === undefined || $scope.policyInfoRecObj.DateOfLoss === "" || $scope.policyInfoRecObj.DateOfLoss === null) {
                                $scope.policyInfoRecObj.DateOfLoss = "";
                            }

                            if ($scope.policyInfoRecObj.CauseOfLoss === undefined || $scope.policyInfoRecObj.CauseOfLoss === "" || $scope.policyInfoRecObj.CauseOfLoss === null) {
                                $scope.policyInfoRecObj.CauseOfLoss = "";
                            }
                            if ($scope.policyInfoRecObj.ClaimStatus === undefined || $scope.policyInfoRecObj.ClaimStatus === "" || $scope.policyInfoRecObj.ClaimStatus === null) {
                                $scope.policyInfoRecObj.ClaimStatus = 0;
                            }

                            if ($scope.policyInfoRecObj.AmountClaimed === undefined || $scope.policyInfoRecObj.AmountClaimed === "" || $scope.policyInfoRecObj.AmountClaimed === null) {
                                $scope.policyInfoRecObj.AmountClaimed = "";
                            }
                            if ($scope.policyInfoRecObj.SumAssuredCurrencyTypeID === undefined || $scope.policyInfoRecObj.SumAssuredCurrencyTypeID === "" || $scope.policyInfoRecObj.SumAssuredCurrencyTypeID === null) {
                                $scope.policyInfoRecObj.SumAssuredCurrencyTypeID = 0;
                            }

                            if ($scope.policyInfoRecObj.RateAmountClaimed === undefined || $scope.policyInfoRecObj.RateAmountClaimed === "" || $scope.policyInfoRecObj.RateAmountClaimed === null) {
                                $scope.policyInfoRecObj.RateAmountClaimed = 0;
                            }
                            if ($scope.policyInfoRecObj.ChequeNo === undefined || $scope.policyInfoRecObj.ChequeNo === "" || $scope.policyInfoRecObj.ChequeNo === null) {
                                $scope.policyInfoRecObj.ChequeNo = "";
                            }

                            if ($scope.policyInfoRecObj.DamageDescription === undefined || $scope.policyInfoRecObj.DamageDescription === "" || $scope.policyInfoRecObj.DamageDescription === null) {
                                $scope.policyInfoRecObj.DamageDescription = "";
                            }
                            if ($scope.policyInfoRecObj.AmountPaid === undefined || $scope.policyInfoRecObj.AmountPaid === "" || $scope.policyInfoRecObj.AmountPaid === null) {
                                $scope.policyInfoRecObj.AmountPaid = 0;
                            }

                            if ($scope.policyInfoRecObj.ClaimPaidStatus === undefined || $scope.policyInfoRecObj.ClaimPaidStatus === "" || $scope.policyInfoRecObj.ClaimPaidStatus === null) {
                                $scope.policyInfoRecObj.ClaimPaidStatus =0;
                            }
                            if ($scope.policyInfoRecObj.FileId === undefined || $scope.policyInfoRecObj.FileId === "" || $scope.policyInfoRecObj.FileId === null) {
                                $scope.policyInfoRecObj.FileId = 0;
                            }
                            if ($scope.policyInfoRecObj.IsOpened === undefined || $scope.policyInfoRecObj.IsOpened === "" || $scope.policyInfoRecObj.IsOpened === null) {
                                $scope.policyInfoRecObj.IsOpened = false;
                            }
                            if ($scope.policyInfoRecObj.DateOfOpen === undefined || $scope.policyInfoRecObj.DateOfOpen === "" || $scope.policyInfoRecObj.DateOfOpen === null) {
                                $scope.policyInfoRecObj.DateOfOpen = "";
                            }
                            else
                                $scope.policyInfoRecObj.DateOfOpen = $scope.getFormattedDate($scope.policyInfoRecObj.DateOfOpen);
                            if ($scope.policyInfoRecObj.DateOfReject === undefined || $scope.policyInfoRecObj.DateOfReject === "" || $scope.policyInfoRecObj.DateOfReject === null) {
                                $scope.policyInfoRecObj.DateOfReject = "";
                            }
                            else
                                $scope.policyInfoRecObj.DateOfReject = $scope.getFormattedDate($scope.policyInfoRecObj.DateOfReject);
                            if ($scope.policyInfoRecObj.FileId === undefined || $scope.policyInfoRecObj.FileId === "" || $scope.policyInfoRecObj.FileId === null) {
                                $scope.policyInfoRecObj.FileId = 0;
                            }
                            if ($scope.policyInfoRecObj.IsWithdrawn === undefined || $scope.policyInfoRecObj.IsWithdrawn === "" || $scope.policyInfoRecObj.IsWithdrawn === null) {
                                $scope.policyInfoRecObj.IsWithdrawn = false;
                            }
                            if ($scope.policyInfoRecObj.IsRejected === undefined || $scope.policyInfoRecObj.IsRejected === "" || $scope.policyInfoRecObj.IsRejected === null) {
                                $scope.policyInfoRecObj.IsRejected = false;
                            }
                            if ($scope.policyInfoRecObj.DateOfWithdraw === undefined || $scope.policyInfoRecObj.DateOfWithdraw === "" || $scope.policyInfoRecObj.DateOfWithdraw === null) {
                                $scope.policyInfoRecObj.DateOfWithdraw = "";
                            }
                            else
                                $scope.policyInfoRecObj.DateOfWithdraw = $scope.getFormattedDate($scope.policyInfoRecObj.DateOfWithdraw);
                            if ($scope.policyInfoRecObj.ContactPersonDetails === undefined || $scope.policyInfoRecObj.ContactPersonDetails === "" || $scope.policyInfoRecObj.ContactPersonDetails === null) {
                                $scope.policyInfoRecObj.ContactPersonDetails = "";
                            }
                            if ($scope.policyInfoRecObj.Other === undefined || $scope.policyInfoRecObj.Other === "" || $scope.policyInfoRecObj.Other === null) {
                                $scope.policyInfoRecObj.Other = "";
                            }


                            if ($scope.policyInfoRecObj.ClaimRecHistoryDetails.Description === undefined || $scope.policyInfoRecObj.ClaimRecHistoryDetails.Description === "" || $scope.policyInfoRecObj.ClaimRecHistoryDetails.Description === null) {
                                $scope.policyInfoRecObj.ClaimRecHistoryDetails.Description = "";
                            }
                            if ($scope.policyInfoRecObj.ClaimRecHistoryDetails.RecordingDate === undefined || $scope.policyInfoRecObj.ClaimRecHistoryDetails.RecordingDate === "" || $scope.policyInfoRecObj.ClaimRecHistoryDetails.RecordingDate === null) {
                                $scope.policyInfoRecObj.ClaimRecHistoryDetails.RecordingDate = "";
                            }
                            if ($scope.policyInfoRecObj.ClaimRecHistoryDetails.Reason === undefined || $scope.policyInfoRecObj.ClaimRecHistoryDetails.Reason === "" || $scope.policyInfoRecObj.ClaimRecHistoryDetails.Reason === null) {
                                $scope.policyInfoRecObj.ClaimRecHistoryDetails.Reason = "";
                            }
                        

                            $scope.policyInfoRecObj.ClaimRecPendingDocDetails.ClaimRecPendingDocID = 1;
                            $scope.policyInfoRecObj.ClaimRecPendingDocDetails.DocumentID = 1;
                            $scope.policyInfoRecObj.ClaimRecPendingDocDetails.CreatedBy = 18;
                            $scope.policyInfoRecObj.ClaimRecPendingDocDetails.CreatedDate = "";
                            $scope.policyInfoRecObj.ClaimRecPendingDocDetails.ModifiedBy = 18;
                            $scope.policyInfoRecObj.ClaimRecPendingDocDetails.ModifiedDate = "";

                           
                          

                            //$scope.cusObj.BusinessUnitID = $scope.businessUnitID;
                            $scope.policyInfoRecObj.BusinessUnitID = $scope.businessUnitID;
                            $scope.policyInfoRecObj.DateOfLoss = $scope.getFormattedDate($scope.policyInfoRecObj.DateOfLoss);
                            $scope.policyInfoRecObj.DateOfIntimation = $scope.getFormattedDate($scope.policyInfoRecObj.DateOfIntimation);
                           
                            
                            
                            
                            $scope.policyInfoRecObj.PolicyInfoRecID = $scope.quotationHeaderObj.PolicyInfoRecID;
                            $scope.policyInfoRecObj.ClaimRecHistoryDetails.RecordingDate = $scope.getFormattedDate($scope.policyInfoRecObj.ClaimRecHistoryDetails.RecordingDate);
                            
                           // $scope.policyInfoRecObjs = [$scope.policyInfoRecObj];

                            ClaimRecordingsService.saveClaimRecording( $scope.policyInfoRecObj, $scope.currentUser.UserID).then(function (results) {
                                $scope.showLoader = false;
                                //alert(angular.toJson(results));
                                if (results.status === true) {
                                    noty({
                                        text: 'Successfully Saved Claim Recording Details',
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
                                    $scope.isPaymentAddMode = true;
                                    $scope.isCustomerAvailable = false;
                                    $scope.isViewMode = false;
                                    $scope.isClientReqAddMode = true;
                                    $scope.policyInfoRecObj = {};
                                    $scope.policyCommissionPayment = {};
                                    $scope.cusObj = {};
                                    $scope.paymentObj = {};
                                    $scope.paymentObj.PaymentAmount = 0;
                                    $scope.paymentObj.DebitNoteList = [];
                                    $scope.refreshContent();
                                    $scope.activateClientRequestListTab();
                                    $scope.refreshContent();
                                    $scope.activateClientRequestListTab();
                                }
                                else {
                                    noty({
                                        text: 'Error Saving Claim Recording Details',
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

                        }
                    },
                    {
                        addClass: 'btn btn-danger btn-clean', text: 'Cancel', onClick: function ($noty) {
                            $noty.close();
                            $scope.$apply(function () {
                                $scope.showLoader = false;
                                $scope.isPaymentAddMode = true;
                                $scope.isCustomerAvailable = false;
                                 $scope.isViewMode = false;
                                 $scope.isClientReqAddMode = true;
                                  $scope.policyInfoRecObj = {};
                                   $scope.policyCommissionPayment = {};
                                $scope.cusObj = {};
                                $scope.paymentObj = {};
                                $scope.paymentObj.PaymentAmount = 0;
                                $scope.paymentObj.DebitNoteList = [];
                            });
                        }
                    }
            ]
        })
    };
});