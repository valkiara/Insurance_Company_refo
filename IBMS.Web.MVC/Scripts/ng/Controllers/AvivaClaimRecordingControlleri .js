'use strict';

ibmsApp.controller("AvivaClaimRecordingController", function ($scope, $http, $rootScope, AvivaClaimRecordingService, $location, AuthService, filterFilter, $modal) {

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
                
                //$scope.loadClientByBUID($scope.businessUnitID);
                $scope.loadClientRequestsByBUID($scope.businessUnitID);
                //$scope.getAllClaimRecording($scope.businessUnitID);
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
        AvivaClaimRecordingService.getAllClientRequestsByBUID(businessUnitID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.cusObj=results.data;
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableQuotationHeaders.push({ value: results.data[i].ClientRequestHeaderID, text: results.data[i].ClientName });
                }

                $scope.isQuotationAvailable = true;
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
        AvivaClaimRecordingService.getDocumentDetails().then(function (results) {
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
        AvivaClaimRecordingService.getAllClaimRecording(businessUnitID).then(function (results) {
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
    //    AvivaClaimRecordingService.getAllPolicyInfoRecording(businessUnitID).then(function (results) {
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
        AvivaClaimRecordingService.getAllCLients(businessUnitID).then(function (results) {
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
        AvivaClaimRecordingService.getClientRequestByID($scope.quotationHeaderObj.ClientRequestHeaderID).then(function (results) {
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
        AvivaClaimRecordingService.loadQuoteInfoInsCompanyLineDetailsByQuotation(quotationHeaderID).then(function (results) {
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
        AvivaClaimRecordingService.getQuatationHeader($scope.quotationHeaderObj.QuotationHeaderID).then(function (results) {
            //$scope.showLoader = false;
            if (results.status === true) {
                $scope.cusReqObj = results.data;

                AvivaClaimRecordingService.getClientByID($scope.cusReqObj.ClientID).then(function (results) {
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
        AvivaClaimRecordingService.loadQuotationHeaderByID($scope.quotationHeaderObj.PolicyInfoRecID).then(function (results) {
            //$scope.showLoader = false;
            if (results.status === true) {
                $scope.cusReqObjs = results.data;
                AvivaClaimRecordingService.getQuatationHeader($scope.cusReqObjs.QuotationHeaderID).then(function (results) {
                    //$scope.showLoader = false;
                    if (results.status === true) {
                        $scope.cusReqObj = results.data;

                        AvivaClaimRecordingService.getClientByID($scope.cusReqObj.ClientID).then(function (results) {
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
        AvivaClaimRecordingService.loadQuotationHeaderByID(quotationHeaderID).then(function (results) {
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

    $scope.editClaimRecording = function (ClaimRecordingID, PolicyInfoRecID) {
        $scope.activateNewClientRequestTab();

        $scope.showLoader = true;
        AvivaClaimRecordingService.editClaimRecording(ClaimRecordingID).then(function (results) {
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
        AvivaClaimRecordingService.getClientByID(clientID).then(function (results) {
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
                            $scope.cusObj.BusinessUnitID = $scope.businessUnitID;
                            $scope.policyInfoRecObj.DateOfLoss = $scope.getFormattedDate($scope.policyInfoRecObj.DateOfLoss);
                            $scope.policyInfoRecObj.DateOfIntimation = $scope.getFormattedDate($scope.policyInfoRecObj.DateOfIntimation);
                            $scope.policyInfoRecObj.ClaimRecHistoryDetails.RecordingDate = $scope.getFormattedDate($scope.policyInfoRecObj.ClaimRecHistoryDetails.RecordingDate);
                            $scope.policyInfoRecObj.PolicyInfoRecID = $scope.quotationHeaderObj.PolicyInfoRecID;
                            //$scope.cusObj.BusinessUnitID = $scope.businessUnitID;
                            //$scope.cusReqObj.RequestedDate = $scope.getFormattedDate($scope.cusReqObj.RequestedDate);

                            AvivaClaimRecordingService.updateClaimRecording($scope.policyInfoRecObj, $scope.currentUser.UserID).then(function (results) {
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

                            if ($scope.policyInfoRecObj.ClaimRecPendingDocDetails[0].DocumentID === "") {
                                $scope.policyInfoRecObj.ClaimRecPendingDocDetails[0].DocumentID = 13;
                            }
                            $scope.cusObj.BusinessUnitID = $scope.businessUnitID;
                            $scope.policyInfoRecObj.DateOfLoss = $scope.getFormattedDate($scope.policyInfoRecObj.DateOfLoss);
                            $scope.policyInfoRecObj.DateOfIntimation = $scope.getFormattedDate($scope.policyInfoRecObj.DateOfIntimation);
                            $scope.policyInfoRecObj.DischargedDate = $scope.getFormattedDate($scope.policyInfoRecObj.DischargedDate);
                            $scope.policyInfoRecObj.ClaimDocumentsReceivedDate = $scope.getFormattedDate($scope.policyInfoRecObj.ClaimDocumentsReceivedDate);
                            $scope.policyInfoRecObj.ClaimDocumentsEmailedDate = $scope.getFormattedDate($scope.policyInfoRecObj.ClaimDocumentsEmailedDate);
                            $scope.policyInfoRecObj.PaymentAdviceReceviedDate = $scope.getFormattedDate($scope.policyInfoRecObj.PaymentAdviceReceviedDate);
                            $scope.policyInfoRecObj.PaymentAdviceEmailedDate = $scope.getFormattedDate($scope.policyInfoRecObj.PaymentAdviceEmailedDate);
                            $scope.policyInfoRecObj.OriginalDocumentscourieredDate = $scope.getFormattedDate($scope.policyInfoRecObj.OriginalDocumentscourieredDate);
                          //  $scope.policyInfoRecObj.PaymentAdviceReceviedDate = $scope.getFormattedDate($scope.policyInfoRecObj.PaymentAdviceReceviedDate);
                            $scope.policyInfoRecObj.PolicyInfoRecID = $scope.quotationHeaderObj.PolicyInfoRecID;

                            // $scope.policyInfoRecObjs = [$scope.policyInfoRecObj];

                            AvivaClaimRecordingService.saveClaimRecording($scope.policyInfoRecObj, $scope.currentUser.UserID).then(function (results) {
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
});