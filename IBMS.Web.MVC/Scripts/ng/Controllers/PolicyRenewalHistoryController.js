'use strict';

ibmsApp.controller("PolicyRenewalHistoryController", function ($scope, $http, $rootScope, PolicyRenewalHistoryService, $location, AuthService, filterFilter, $modal) {

    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;

            if ($scope.currentUser.AccessLevelTypeName === "Admin") {
                //To Do (using a popup to select the desired business unit)
            }
            else {
                $scope.businessUnitID = $scope.currentUser.BusinessUnitID;
                $scope.loadPolicyInfoRecDetails($scope.businessUnitID);
                $scope.loadQuotationHeadersByBUID($scope.businessUnitID);
                //$scope.loadPolicyRenewalHistoryByBUID($scope.businessUnitID);
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
        // $scope.policyInfoRecObj.IsActive = false;
        $scope.businessUnitID = "";
        // $scope.policyInfoRecObj.PolicyCommissionPaymentDetails = [];
        $scope.availablePolicyInfoRecordings = [];
        $scope.availableQuotationHeaders = [];
        $scope.availableQuoteInfoInsCompanyDetails = [];
        $scope.availableCurrencies = [];
        $scope.availableCommissionTypes = [];
        $scope.availableComStructLines = [];
        $scope.availableAgents = [];
        $scope.availableloadExecutive = [];
        //  $scope.policyInfoRecObjs = [];

        $scope.getCurrentUser();
        $scope.loadCurrencyDetails();
        $scope.loadCommissionTypes();;
        $scope.loadCommissionStructureLines();



        $scope.loadExecutive();
        $scope.loadAgent();
        // $scope.addItem();


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
        $scope.policyInfoRecObj = {};
        //   $scope.policyCommissionPayment = {};
        //$scope.cusObj = {};
        //$scope.paymentObj = {};
        //$scope.paymentObj.PaymentAmount = 0;
        //$scope.paymentObj.DebitNoteList = [];
    };





    $scope.loadPolicyInfoRecDetails = function (businessUnitID) {
        $scope.showLoader = true;
        PolicyRenewalHistoryService.getAllPolicyInfoRecordingsByBUID(businessUnitID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.availablePolicyInfoRecordings = results.data;
                //$scope.availablePolicyInfoRecordings.RenewalDate = $scope.getFormattedDate(results.data.RenewalDate);
                //$scope.availablePolicyInfoRecordings.NotificationDate = $scope.getFormattedDate(results.data.NotificationDate)
                $scope.data = angular.copy($scope.availablePolicyInfoRecordings);
                $scope.viewby = "10";
                $scope.totalItems = $scope.data.length;
                $scope.currentPage = 1;
                $scope.itemsPerPage = $scope.viewby;
                $scope.maxSize = 10; //Number of pager buttons to show

                $scope.setItemsPerPage($scope.viewby);
                $scope.loadDataFroDashboard();
            }
            else {
                $scope.availablePolicyInfoRecordings = [];
            }
        });
    };


    //$scope.loadPolicyRenewalHistoryByBUID = function (businessUnitID) {
    //    $scope.showLoader = true;
    //    PolicyRenewalHistoryService.getAllPolicyRenewalHistoryByBUID(businessUnitID).then(function (results) {
    //        $scope.showLoader = false;
    //        if (results.status === true) {
    //            $scope.availablePolicyInfoRecordings = results.data;
    //            $scope.data = angular.copy($scope.availablePolicyInfoRecordings);
    //            // $scope.policyInfoRecObj = $scope.data;
    //            $scope.viewby = "10";
    //            $scope.totalItems = $scope.data.length;
    //            $scope.currentPage = 1;
    //            $scope.itemsPerPage = $scope.viewby;
    //            $scope.maxSize = 10; //Number of pager buttons to show

    //            $scope.setItemsPerPage($scope.viewby);
    //            $scope.loadDataFroDashboard();
    //        }
    //        else {
    //            $scope.availablePolicyInfoRecordings = [];
    //        }
    //    });
    //};

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

    $scope.loadAgent = function () {
        //$scope.showLoader = true;
        PolicyRenewalHistoryService.loadAgent().then(function (results) {
            //$scope.showLoader = false;
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableAgents.push({ value: results.data[i].AgentID, text: results.data[i].AgentName })
                }
            }
            else {
                $scope.availableAgents = [];
            }
        });
    };


    $scope.loadExecutive = function () {
        //$scope.showLoader = true;
        PolicyRenewalHistoryService.loadExecutive().then(function (results) {
            //$scope.showLoader = false;
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableloadExecutive.push({ value: results.data[i].EmployeeID, text: results.data[i].EmployeeName })
                }
            }
            else {
                $scope.availableloadExecutive = [];
            }
        });
    };


    $scope.loadQuotationHeadersByBUID = function (businessUnitID) {
        PolicyRenewalHistoryService.getAllPolicyInfoRecordingsByBUID(businessUnitID).then(function (results) {
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableQuotationHeaders.push({ value: results.data[i].PolicyInfoRecID, text: results.data[i].PolicyNumber });
                }
            }
            else {
                $scope.availableQuotationHeaders = [];
            }
        });
    };

    $scope.loadCommissionTypes = function () {
        PolicyRenewalHistoryService.loadCommissionTypes().then(function (results) {
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableCommissionTypes.push({ value: results.data[i].CommisionTypeID, text: results.data[i].CommisionTypeName });
                }
            }
            else {
                $scope.availableCommissionTypes = [];
            }
        });
    };

    $scope.loadCommissionStructureLines = function () {
        PolicyRenewalHistoryService.loadCommissionStructureLines().then(function (results) {
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableComStructLines.push({ value: results.data[i].CommisionStructureLineID, text: results.data[i].CommisionStructureName + " - " + results.data[i].CommisionStructureLineID });
                }
            }
            else {
                $scope.availableComStructLines = [];
            }
        });
    };

    $scope.getClientDetailsByID = function (quotationObj) {
        //$scope.ClearFields();
        $scope.isQuotationAvailable = true;

        $scope.quotationHeaderObj = quotationObj;

        $scope.showLoader = true;
        PolicyRenewalHistoryService.GetQuotationHeaderByID($scope.quotationHeaderObj.QuotationHeaderID).then(function (results) {
            //$scope.showLoader = false;
            if (results.status === true) {
                $scope.cusReqObj = results.data;

                PolicyRenewalHistoryService.getClientByID($scope.cusReqObj.ClientID).then(function (results) {
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
        //$scope.ClearFields();
        $scope.isQuotationAvailable = true;

        $scope.quotationHeaderObj = quotationObj;

        $scope.showLoader = true;
        PolicyRenewalHistoryService.loadQuotationHeaderByID($scope.quotationHeaderObj.PolicyInfoRecID).then(function (results) {
            //$scope.showLoader = false;
            if (results.status === true) {
                $scope.cusReqObjs = results.data;
                PolicyRenewalHistoryService.GetQuotationHeaderByID($scope.cusReqObjs.QuotationHeaderID).then(function (results) {
                    //$scope.showLoader = false;
                    if (results.status === true) {
                        $scope.cusReqObj = results.data;

                        PolicyRenewalHistoryService.getClientByID($scope.cusReqObj.ClientID).then(function (results) {
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

    $scope.loadQuoteInfoInsCompanyLineDetails = function (quotationHeaderID) {
        $scope.availableQuoteInfoInsCompanyDetails = [];

        //$scope.showLoader = true;
        PolicyRenewalHistoryService.loadQuoteInfoInsCompanyLineDetailsByQuotation(quotationHeaderID).then(function (results) {
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

    $scope.loadQuotationDetailsByID = function (PolicyInfoRecID) {
        $scope.showLoader = true;
        PolicyRenewalHistoryService.loadQuotationHeaderByID(PolicyInfoRecID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.getClientDetailsByID(results.data);
                // $scope.loadClientRequestDetailsByID(results.data);
                // $scope.loadQuoteInfoInsCompanyLineDetails(quotationHeaderID);
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

    $scope.loadCurrencyDetails = function () {
        PolicyRenewalHistoryService.loadCurrencies().then(function (results) {
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

    //$scope.addItem = function () {
    //    $scope.policyInfoRecObj.PolicyCommissionPaymentDetails.push({ CommisionTypeID: "", CommisionValue: "", ComStructLineID: "" });
    //};

    //$scope.deleteItem = function (deleteIndex) {
    //    $scope.policyInfoRecObj.PolicyCommissionPaymentDetails.splice(deleteIndex, 1);
    //};

    $scope.activateNewClientRequestTab = function () {
        $("#list-tab-1").removeClass('active');
        $("#tab-1").removeClass('tab-pane active');

        $("#list-tab-2").addClass('active');
        $("#tab-2").addClass('tab-pane active');

        $("#tab-1").css("display", "none");
        $("#tab-2").css("display", "block");
    };

    $scope.editPolicyRenewalHistroy = function (PolicyInfoRecID, QuotationHeaderID) {
        $scope.activateNewClientRequestTab();

        $scope.showLoader = true;
        PolicyRenewalHistoryService.editPolicyRenewalHistroy(PolicyInfoRecID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.isViewMode = false;
                $scope.isClientReqAddMode = false;
                $scope.isQuotationAvailable = true;
                $scope.policyInfoRecObj = results.data;
                // $scope.policyInfoRecObj.PeriodOfCoverFromDate =

                var Fromdate = new Date($scope.policyInfoRecObj.PeriodOfCoverFromDate);
                var Todate = new Date($scope.policyInfoRecObj.PeriodOfCoverToDate);

                // $scope.policyInfoRecObj.PeriodOfCoverFromDate = Fromdate.setDate(Fromdate.getDate() + numberOfYearToAdd);

                var renewalFromDate = new Date(Fromdate.getFullYear() + 1, Fromdate.getMonth(), Fromdate.getDate());
                var renewalToDate = new Date(Todate.getFullYear() + 1, Todate.getMonth(), Todate.getDate());
                $scope.policyInfoRecObj.PeriodOfCoverToDate = $scope.getFormattedDate(renewalToDate);
                $scope.policyInfoRecObj.PeriodOfCoverFromDate = $scope.getFormattedDate(renewalFromDate);

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
        PolicyRenewalHistoryService.getClientByID(clientID).then(function (results) {
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

    $scope.updatePolicyRenewalHistory = function () {
        $scope.showLoader = true;
        noty({
            text: 'Do you want to Update Policy Info Recording Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();

                            //if ($scope.cusObj.FamilyDiscount === undefined || $scope.cusObj.FamilyDiscount === "" || $scope.cusObj.FamilyDiscount === null) {
                            //    $scope.cusObj.FamilyDiscount = 0;
                            //}
                            $scope.cusObj.BusinessUnitID = $scope.businessUnitID;
                            $scope.policyInfoRecObj.PolicyInfoRecID = $scope.quotationHeaderObj.PolicyInfoRecID;
                            $scope.policyInfoRecObj.RenewalDate = $scope.getFormattedDate($scope.policyInfoRecObj.RenewalDate);
                            $scope.policyInfoRecObj.NotificationDate = $scope.getFormattedDate($scope.policyInfoRecObj.NotificationDate);
                            $scope.policyInfoRecObj.RenewalStartDate = $scope.policyInfoRecObj.PeriodOfCoverFromDate;
                            $scope.policyInfoRecObj.RenewalEndDate = $scope.policyInfoRecObj.PeriodOfCoverToDate;
                            //$scope.cusObj.BusinessUnitID = $scope.businessUnitID;
                            //$scope.cusReqObj.RequestedDate = $scope.getFormattedDate($scope.cusReqObj.RequestedDate);

                            if ($scope.policyInfoRecObj.IsRenewal === undefined || $scope.policyInfoRecObj.IsRenewal === "" || $scope.policyInfoRecObj.IsRenewal === null) {
                                $scope.policyInfoRecObj.IsRenewal = false;
                            }
                            else
                                $scope.policyInfoRecObj.IsRenewal = true;

                            if ($scope.policyInfoRecObj.IsCancel === undefined || $scope.policyInfoRecObj.IsCancel === "" || $scope.policyInfoRecObj.IsCancel === null) {
                                $scope.policyInfoRecObj.IsCancel = false;
                            }
                            else
                                $scope.policyInfoRecObj.IsCancel = true;

                            if ($scope.policyInfoRecObj.IsSent === undefined || $scope.policyInfoRecObj.IsSent === "" || $scope.policyInfoRecObj.IsSent === null) {
                                $scope.policyInfoRecObj.IsSent = false;
                            }
                            $scope.policyInfoRecObj.IsSent = true;

                            PolicyRenewalHistoryService.updatePolicyRenewalHistory($scope.policyInfoRecObj, $scope.currentUser.UserID).then(function (results) {
                                $scope.showLoader = false;
                                //alert(angular.toJson(results));
                                if (results.status === true) {
                                    noty({
                                        text: 'Successfully Updated Policy Info Recording Details',
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

                                    sessionStorage.setItem("policyId", $scope.policyInfoRecObj.PolicyInfoRecID);
                                    sessionStorage.setItem("quotationHeaderId", $scope.policyInfoRecObj.QuotationHeaderID);

                                    $scope.ClearFields();
                                    $scope.refreshContent();



                                    window.location.assign("/Transaction/ManagePolicyInfoRecording");
                                }
                                else {
                                    noty({
                                        text: 'Error Updating Customer Request Details',
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

    $scope.savePolicyRenewalHistory = function () {
        $scope.showLoader = true;
        noty({
            text: 'Do you want to Save Policy Renewal History Details?',
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


                            $scope.cusObj.BusinessUnitID = $scope.businessUnitID;
                            $scope.policyInfoRecObj.PolicyInfoRecID = $scope.quotationHeaderObj.PolicyInfoRecID;
                            $scope.policyInfoRecObj.RenewalDate = $scope.getFormattedDate($scope.policyInfoRecObj.RenewalDate);
                            $scope.policyInfoRecObj.NotificationDate = $scope.getFormattedDate($scope.policyInfoRecObj.NotificationDate);
                            $scope.policyInfoRecObj.RenewalStartDate = $scope.getFormattedDate($scope.policyInfoRecObj.RenewalStartDate);
                            $scope.policyInfoRecObj.RenewalEndDate = $scope.getFormattedDate($scope.policyInfoRecObj.RenewalEndDate);
                            // $scope.policyInfoRecObj.QuotationDetailsInsCompanyLineID = 12;
                            // $scope.policyInfoRecObjs = [$scope.policyInfoRecObj];


                            if ($scope.policyInfoRecObj.IsRenewal === undefined || $scope.policyInfoRecObj.IsRenewal === "" || $scope.policyInfoRecObj.IsRenewal === null) {
                                $scope.policyInfoRecObj.IsRenewal = false;
                            }
                            else
                                $scope.policyInfoRecObj.IsRenewal = true;

                            if ($scope.policyInfoRecObj.IsCancel === undefined || $scope.policyInfoRecObj.IsCancel === "" || $scope.policyInfoRecObj.IsCancel === null) {
                                $scope.policyInfoRecObj.IsCancel = false;
                            }
                            else
                                $scope.policyInfoRecObj.IsCancel = true;

                            if ($scope.policyInfoRecObj.IsSent === undefined || $scope.policyInfoRecObj.IsSent === "" || $scope.policyInfoRecObj.IsSent === null) {
                                $scope.policyInfoRecObj.IsSent = false;
                            }
                            $scope.policyInfoRecObj.IsSent = true;

                            PolicyRenewalHistoryService.savePolicyRenewalHistory($scope.policyInfoRecObj, $scope.currentUser.UserID).then(function (results) {
                                $scope.showLoader = false;
                                //alert(angular.toJson(results));
                                if (results.status === true) {
                                    noty({
                                        text: 'Successfully Saved Policy Renewal History Details',
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
                                        text: 'Error Saving Customer Request Details',
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


    $scope.loadDataFroDashboard = function () {

        $scope.loadMode = sessionStorage.getItem("PolicyNumber") == null ? 0 : 1;
        if ($scope.loadMode == 1) {
            var policyIndex = $scope.data.findIndex(record => record.PolicyNumber === sessionStorage.getItem("PolicyNumber"));
            $scope.editPolicyRenewalHistroy($scope.data[policyIndex].PolicyInfoRecID, $scope.data[policyIndex].QuotationHeaderID);
        }
        sessionStorage.removeItem("PolicyNumber");
    };
});