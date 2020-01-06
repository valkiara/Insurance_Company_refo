'use strict';

ibmsApp.controller("PilotClaimRecordingController", function ($scope, $http, $rootScope, PilotClaimRecordingService, $location, AuthService, filterFilter, $modal) {

    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;

            if ($scope.currentUser.AccessLevelTypeName === "Admin") {
                //To Do (using a popup to select the desired business unit)
            }
            else {
                $scope.businessUnitID = $scope.currentUser.BusinessUnitID;
                //$scope.loadPolicyInfoRecDetails($scope.businessUnitID);
                //$scope.loadPolicyRecInfoByBUID($scope.businessUnitID);
                //$scope.getAllClaimRecording($scope.businessUnitID);
                //$scope.loadClientByBUID($scope.businessUnitID);
                $scope.loadClientRequestsByBUID($scope.businessUnitID);
                //$scope.getAllClaimRecording($scope.businessUnitID);
            }
        });
    };

    $scope.init = function () {
        $scope.isQuotationAvailable = false;
        $scope.isViewMode = false;
          $scope.isClientReqAddMode = true;
        $scope.cusObj = {};
        $scope.quotationHeaderObj = {};
        $scope.Admission = {};
        $scope.policyInfoRecObj = {};
        $scope.policyCommissionPayment = {};
        //$scope.quotationHeaderObj = {};
        
        $scope.policyInfoRecObj.IsOpened = false;
        $scope.policyInfoRecObj.IsRejected = false;
        $scope.policyInfoRecObj.IsWithdrawn = false;
        $scope.businessUnitID = "";
        ///$scope.policyInfoRecObj.ClaimRecHistoryDetails = "";
        $scope.policyInfoRecObj.ClaimRecPendingDocDetails = [];
        $scope.policyInfoRecObj.ClaimRecHistoryDetails = {};
        //  $scope.Admission.Admission.ClaimRecHistoryDetails = { Description: "", RecordingDate: "", Reason: "" };

        $scope.availablePolicyInfoRecordings = [];
        $scope.availableDocument = [];
        $scope.availableQuotationHeaders = [];
        $scope.availableQuoteInfoInsCompanyDetails = [];
        $scope.availableCurrencies = [];
        $scope.availableCommissionTypes = [];
        $scope.availableComStructLines = [];
        $scope.policyInfoRecObjs = [];

        $scope.getCurrentUser();
        $scope.loadDocumentDetails();

        //$scope.loadCommissionTypes();;
        //  $scope.loadCommissionStructureLines()
        $scope.addItem();
    };

    $scope.loadClientRequestsByBUID = function (businessUnitID) {
        $scope.showLoader = true;
        PilotClaimRecordingService.getAllPilotClaimRecording(businessUnitID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.cusObj = results.data;
                //for (var i = 0; i < results.data.length; i++) {
                //    $scope.availableQuotationHeaders.push({ value: results.data[i].ClientRequestHeaderID, text: results.data[i].ClientName });
                //}

                //$scope.isQuotationAvailable = true;

                //$scope.isViewMode = false;
                //$scope.isClientReqAddMode = false;
                $scope.data = angular.copy($scope.cusObj);
                $scope.viewby = "10";
                $scope.totalItems = $scope.data.length;
                $scope.currentPage = 1;
                $scope.itemsPerPage = $scope.viewby;
                $scope.maxSize = 10; //Number of pager buttons to show

                $scope.setItemsPerPage($scope.viewby);
            }
            else {
                $scope.availableQuotationHeaders = [];
            }




        });
    };





    $scope.refreshContent = function () {
        $scope.loadClientRequestsByBUID($scope.businessUnitID);
        //$scope.loadClientsByBUID($scope.businessUnitID);
       // $scope.search_query = "";
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

        $scope.Admission = {};
    };

    $scope.loadDocumentDetails = function () {
        $scope.showLoader = true;
        PilotClaimRecordingService.getDocumentDetails().then(function (results) {
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


    $scope.getAllClaimRecording = function (businessUnitID) {
        $scope.showLoader = true;
        PilotClaimRecordingService.getAllClaimRecording(businessUnitID).then(function (results) {
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

    //$scope.loadPolicyRecInfoByBUID = function (businessUnitID) {
    //    PilotClaimRecordingService.getAllPolicyInfoRecording(businessUnitID).then(function (results) {
    //        if (results.status === true) {
    //            for (var i = 0; i < results.data.length; i++) {
    //                $scope.availableQuotationHeaders.push({ value: results.data[i].PolicyInfoRecID, text: results.data[i].PolicyNumber });
    //            }
    //        }
    //        else {
    //            $scope.availableQuotationHeaders = [];
    //        }
    //    });
    //};

    $scope.loadClientByBUID = function (businessUnitID) {
        PilotClaimRecordingService.getAllCLients(businessUnitID).then(function (results) {
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableQuotationHeaders.push({ value: results.data[i].ClientID, text: results.data[i].ClientName });
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
        PilotClaimRecordingService.getClientRequestByID($scope.quotationHeaderObj.ClientRequestHeaderID).then(function (results) {
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
        PilotClaimRecordingService.loadQuoteInfoInsCompanyLineDetailsByQuotation(quotationHeaderID).then(function (results) {
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
        PilotClaimRecordingService.getQuatationHeader($scope.quotationHeaderObj.QuotationHeaderID).then(function (results) {
            //$scope.showLoader = false;
            if (results.status === true) {
                $scope.cusReqObj = results.data;

                PilotClaimRecordingService.getClientByID($scope.cusReqObj.ClientID).then(function (results) {
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

        $scope.quotationHeaderObj = quotationObj;

        $scope.showLoader = true;
        PilotClaimRecordingService.loadQuotationHeaderByID($scope.quotationHeaderObj.PolicyInfoRecID).then(function (results) {
            //$scope.showLoader = false;
            if (results.status === true) {
                $scope.cusReqObjs = results.data;
                PilotClaimRecordingService.getQuatationHeader($scope.cusReqObjs.QuotationHeaderID).then(function (results) {
                    //$scope.showLoader = false;
                    if (results.status === true) {
                        $scope.cusReqObj = results.data;

                        PilotClaimRecordingService.getClientByID($scope.cusReqObj.ClientID).then(function (results) {
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
        PilotClaimRecordingService.loadQuotationHeaderByID(quotationHeaderID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.getClientDetailsByID(results.data);
                $scope.loadClientRequestDetailsByID(results.data);
                $scope.loadQuoteInfoInsCompanyLineDetails(quotationHeaderID);
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

    $scope.editClaimRecording = function (ReferenceNo) {
        $scope.activateNewClientRequestTab();

        $scope.showLoader = true;
        PilotClaimRecordingService.editClaimRecording(ReferenceNo).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.isViewMode = false;
                $scope.isClientReqAddMode = false;
                $scope.isQuotationAvailable = true;
                $scope.Admission = results.data;
                // $scope.Admission.PartnerID = $scope.cusReqObj.PartnerID + "";
                //$scope.getClientDetailsByIDs(results.data);
                // $scope.loadQuotationDetailsByID(PolicyInfoRecID);
                //$scope.loadClientByID(clientID);
                //$scope.loadQuotationDetailsByID();
            }
            else {
                $scope.Admission = {};
            }
        });
    };

    $scope.loadClientByID = function (clientID) {
        $scope.showLoader = true;
        PilotClaimRecordingService.getClientByID(clientID).then(function (results) {
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
    $scope.cancelCustomerRequest = function () {
        $scope.ClearFields();
        $scope.refreshContent();
        $scope.activatePolicyInfoRecListTab();
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
                            //var array = $.map($scope.Admission, function (value, index) {
                            //    return [value];
                            //});

                            //if ($scope.Admission.ClaimRecPendingDocDetails[0].DocumentID === "") {
                            //    $scope.Admission.ClaimRecPendingDocDetails[0].DocumentID = 13;
                            //}
                            $scope.Admission.BUID = $scope.businessUnitID;
                            $scope.Admission.CreateBy = $scope.currentUser.UserID;

                            // alert($scope.Admission.DateOfBirth);
                            if ($scope.Admission.DateOfBirth === undefined || $scope.Admission.DateOfBirth === "" || $scope.Admission.DateOfBirth === null) {
                                $scope.Admission.DateOfBirth = "";
                            }
                            else
                                $scope.Admission.DateOfBirth = $scope.getFormattedDate($scope.Admission.DateOfBirth);


                            //if ($scope.cusObj.FamilyDiscount === undefined || $scope.cusObj.FamilyDiscount === "" || $scope.cusObj.FamilyDiscount === null) {
                            //    $scope.cusObj.FamilyDiscount = 0;
                            // alert($scope.Admission.AdmissionDate);
                            if ($scope.Admission.AdmissionDate === undefined || $scope.Admission.AdmissionDate === "" || $scope.Admission.AdmissionDate === null) {
                                $scope.Admission.AdmissionDate = "";
                            }
                            else
                                $scope.Admission.AdmissionDate = $scope.getFormattedDate($scope.Admission.AdmissionDate);


                            //  $scope.Admission.AdmissionDate = $scope.getFormattedDate($scope.Admission.AdmissionDate);
                            // alert($scope.Admission.DischargedDate);
                            if ($scope.Admission.DischargedDate === undefined || $scope.Admission.DischargedDate === "" || $scope.Admission.DischargedDate === null) {
                                $scope.Admission.DischargedDate = "";
                            }
                            else
                                $scope.Admission.DischargedDate = $scope.getFormattedDate($scope.Admission.DischargedDate);
                            //alert($scope.Admission.ClaimDocumentReceivedDate);
                            if ($scope.Admission.ClaimDocumentReceivedDate === undefined || $scope.Admission.ClaimDocumentReceivedDate === "" || $scope.Admission.ClaimDocumentReceivedDate === null) {
                                $scope.Admission.ClaimDocumentReceivedDate = "";
                            }
                            else




                                $scope.Admission.ClaimDocumentReceivedDate = $scope.getFormattedDate($scope.Admission.ClaimDocumentReceivedDate);
                            //  $scope.Admission.DischargedDate = $scope.getFormattedDate($scope.Admission.DischargedDate);
                            //alert($scope.Admission.ClaimDocumentsEmailedDate);
                            if ($scope.Admission.ClaimDocumentsEmailedDate === undefined || $scope.Admission.ClaimDocumentsEmailedDate === "" || $scope.Admission.ClaimDocumentsEmailedDate === null) {
                                $scope.Admission.ClaimDocumentsEmailedDate = "";
                            }
                            else

                                $scope.Admission.ClaimDocumentsEmailedDate = $scope.getFormattedDate($scope.Admission.ClaimDocumentsEmailedDate);

                            //alert($scope.Admission.PaymentAdviceReceviedDate);
                            if ($scope.Admission.PaymentAdviceReceviedDate === undefined || $scope.Admission.PaymentAdviceReceviedDate === "" || $scope.Admission.PaymentAdviceReceviedDate === null) {
                                $scope.Admission.PaymentAdviceReceviedDate = "";
                            }
                            else




                                $scope.Admission.PaymentAdviceReceviedDate = $scope.getFormattedDate($scope.Admission.PaymentAdviceReceviedDate);
                            //alert($scope.Admission.PaymentAdviceEmailedDate);
                            if ($scope.Admission.PaymentAdviceEmailedDate === undefined || $scope.Admission.PaymentAdviceEmailedDate === "" || $scope.Admission.PaymentAdviceEmailedDate === null) {
                                $scope.Admission.PaymentAdviceEmailedDate = "";
                            }
                            else







                                $scope.Admission.PaymentAdviceEmailedDate = $scope.getFormattedDate($scope.Admission.PaymentAdviceEmailedDate);
                            //alert($scope.Admission.OriginalDocumentscourieredDate);
                            if ($scope.Admission.OriginalDocumentscourieredDate === undefined || $scope.Admission.OriginalDocumentscourieredDate === "" || $scope.Admission.OriginalDocumentscourieredDate === null) {
                                $scope.Admission.OriginalDocumentscourieredDate = "";
                            }
                            else









                                $scope.Admission.OriginalDocumentscourieredDate = $scope.getFormattedDate($scope.Admission.OriginalDocumentscourieredDate);

                            $scope.Admission.CurrancyID = 0;

                            PilotClaimRecordingService.saveClaimRecording($scope.Admission, $scope.currentUser.UserID).then(function (results) {
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
                                    $scope.ClearFields();
                                    $scope.refreshContent();
                                    $scope.activatePolicyInfoRecListTab();
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

    $scope.savePolicyInfoRecording_ = function () {
        $scope.showLoader = true;
        noty({
            text: 'Do you want to Save Claim Recording Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();

                         //   alert("ok");
                            $scope.Admission.BUID = $scope.businessUnitID;
                            $scope.Admission.CreateBy = $scope.currentUser.UserID;
                            $scope.Admission.DateOfBirth = $scope.getFormattedDate($scope.Admission.DateOfBirth);
                            $scope.Admission.AdmissionDate = $scope.getFormattedDate($scope.Admission.AdmissionDate);
                            $scope.Admission.ClaimDocumentReceivedDate = $scope.getFormattedDate($scope.Admission.ClaimDocumentReceivedDate);
                            $scope.Admission.DischargedDate = $scope.getFormattedDate($scope.Admission.DischargedDate);
                            $scope.Admission.ClaimDocumentsEmailedDate = $scope.getFormattedDate($scope.Admission.ClaimDocumentsEmailedDate);
                            $scope.Admission.PaymentAdviceReceviedDate = $scope.getFormattedDate($scope.Admission.ClaimDocumentsEmailedDate);
                            $scope.Admission.PaymentAdviceEmailedDate = $scope.getFormattedDate($scope.Admission.PaymentAdviceEmailedDate);
                            $scope.Admission.OriginalDocumentscourieredDate = $scope.getFormattedDate($scope.Admission.OriginalDocumentscourieredDate);
                           
                            PilotClaimRecordingService.saveClaimRecording($scope.Admission, $scope.currentUser.UserID).then(function (results) {
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
                                    $scope.ClearFields();
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
                            //var array = $.map($scope.Admission, function (value, index) {
                            //    return [value];
                            //});

                            //if ($scope.Admission.ClaimRecPendingDocDetails[0].DocumentID === "") {
                            //    $scope.Admission.ClaimRecPendingDocDetails[0].DocumentID = 13;
                            //}
                            $scope.Admission.BUID = $scope.businessUnitID;
                            $scope.Admission.CreateBy = $scope.currentUser.UserID;

                           // alert($scope.Admission.DateOfBirth);
                            if ($scope.Admission.DateOfBirth === undefined || $scope.Admission.DateOfBirth === "" || $scope.Admission.DateOfBirth === null) {
                                $scope.Admission.DateOfBirth = "";
                            }
                            else
                                $scope.Admission.DateOfBirth = $scope.getFormattedDate($scope.Admission.DateOfBirth);


                            //if ($scope.cusObj.FamilyDiscount === undefined || $scope.cusObj.FamilyDiscount === "" || $scope.cusObj.FamilyDiscount === null) {
                            //    $scope.cusObj.FamilyDiscount = 0;
                           // alert($scope.Admission.AdmissionDate);
                            if ($scope.Admission.AdmissionDate === undefined || $scope.Admission.AdmissionDate === "" || $scope.Admission.AdmissionDate === null) {
                                $scope.Admission.AdmissionDate = "";
                            }
                            else
                                $scope.Admission.AdmissionDate = $scope.getFormattedDate($scope.Admission.AdmissionDate);


                            //  $scope.Admission.AdmissionDate = $scope.getFormattedDate($scope.Admission.AdmissionDate);
                           // alert($scope.Admission.DischargedDate);
                            if ($scope.Admission.DischargedDate === undefined || $scope.Admission.DischargedDate === "" || $scope.Admission.DischargedDate === null) {
                                $scope.Admission.DischargedDate = "";
                            }
                            else
                                $scope.Admission.DischargedDate = $scope.getFormattedDate($scope.Admission.DischargedDate);
                            //alert($scope.Admission.ClaimDocumentReceivedDate);
                            if ($scope.Admission.ClaimDocumentReceivedDate === undefined || $scope.Admission.ClaimDocumentReceivedDate === "" || $scope.Admission.ClaimDocumentReceivedDate === null) {
                                $scope.Admission.ClaimDocumentReceivedDate = "";
                            }
                            else
                               



                            $scope.Admission.ClaimDocumentReceivedDate = $scope.getFormattedDate($scope.Admission.ClaimDocumentReceivedDate);
                            //  $scope.Admission.DischargedDate = $scope.getFormattedDate($scope.Admission.DischargedDate);
                            //alert($scope.Admission.ClaimDocumentsEmailedDate);
                            if ($scope.Admission.ClaimDocumentsEmailedDate === undefined || $scope.Admission.ClaimDocumentsEmailedDate === "" || $scope.Admission.ClaimDocumentsEmailedDate === null) {
                                $scope.Admission.ClaimDocumentsEmailedDate = "";
                            }
                            else                             

                                $scope.Admission.ClaimDocumentsEmailedDate = $scope.getFormattedDate($scope.Admission.ClaimDocumentsEmailedDate);

                            //alert($scope.Admission.PaymentAdviceReceviedDate);
                            if ($scope.Admission.PaymentAdviceReceviedDate === undefined || $scope.Admission.PaymentAdviceReceviedDate === "" || $scope.Admission.PaymentAdviceReceviedDate === null) {
                                $scope.Admission.PaymentAdviceReceviedDate = "";
                            }
                            else

                             


                                $scope.Admission.PaymentAdviceReceviedDate = $scope.getFormattedDate($scope.Admission.PaymentAdviceReceviedDate);
                            //alert($scope.Admission.PaymentAdviceEmailedDate);
                            if ($scope.Admission.PaymentAdviceEmailedDate === undefined || $scope.Admission.PaymentAdviceEmailedDate === "" || $scope.Admission.PaymentAdviceEmailedDate === null) {
                                $scope.Admission.PaymentAdviceEmailedDate = "";
                            }
                            else




                            


                                $scope.Admission.PaymentAdviceEmailedDate = $scope.getFormattedDate($scope.Admission.PaymentAdviceEmailedDate);
                            //alert($scope.Admission.OriginalDocumentscourieredDate);
                            if ($scope.Admission.OriginalDocumentscourieredDate === undefined || $scope.Admission.OriginalDocumentscourieredDate === "" || $scope.Admission.OriginalDocumentscourieredDate === null) {
                                $scope.Admission.OriginalDocumentscourieredDate = "";
                            }
                            else









                            $scope.Admission.OriginalDocumentscourieredDate = $scope.getFormattedDate($scope.Admission.OriginalDocumentscourieredDate);

                            $scope.Admission.CurrancyID = 0;



                            //  $scope.Admission.PaymentAdviceReceviedDate = $scope.getFormattedDate($scope.Admission.PaymentAdviceReceviedDate);
                            //$scope.Admission.PolicyInfoRecID = $scope.quotationHeaderObj.PolicyInfoRecID;

                            // $scope.Admissions = [$scope.Admission];

                            PilotClaimRecordingService.saveClaimRecording($scope.Admission, $scope.currentUser.UserID).then(function (results) {
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
                                    $scope.ClearFields();
                                    $scope.refreshContent();
                                    $scope.activatePolicyInfoRecListTab();
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
                            });
                        }
                    }
            ]
        })
    };





});