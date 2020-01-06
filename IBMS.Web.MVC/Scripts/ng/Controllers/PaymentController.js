'use strict';

ibmsApp.controller("PaymentController", function ($scope, $http, $rootScope, PaymentService, $location, AuthService, filterFilter, $modal) {

    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;

            if ($scope.currentUser.AccessLevelTypeName === "Admin") {
                //To Do (using a popup to select the desired business unit)
            }
            else {
                $scope.businessUnitID = $scope.currentUser.BusinessUnitID;
                $scope.loadClientsByBUID($scope.businessUnitID);
                $scope.loadPaymentDetails($scope.businessUnitID);
                $scope.loadBanks($scope.businessUnitID);
            }
        });
    };

    $scope.init = function () {
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
        $scope.loadPaymentMethods();
        $scope.loadCurrencyDetails();
        $scope.addItem();
        $scope.loadChargeTypes();
    };

    $scope.LoadReciept = function (PaymentID) {

        
        sessionStorage.setItem("referenceNo", PaymentID);
        window.location.assign("ManagePaymentInvoice");

    };

    $scope.refreshContent = function () {
        $scope.loadPaymentDetails();
        $scope.search_query = "";
    };

    $scope.ClearFields = function () {
        $scope.isPaymentAddMode = true;
        $scope.isCustomerAvailable = false;

        $scope.cusObj = {};
        $scope.paymentObj = {};
        $scope.paymentObj.PaymentAmount = 0;
        $scope.paymentObj.DebitNoteList = [];
    };

        $scope.loadBanks = function (BUID) {
        //$scope.showLoader = true;
        PaymentService.loadBanks(BUID).then(function (results) {
            //$scope.showLoader = false;
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableBanks.push({ value: results.data[i].BankID, text: results.data[i].BankName })
                }
            }
            else {
                $scope.availableBanks = [];
            }
        });
    };
        $scope.loadPaymentMethods = function () {
            PaymentService.loadPaymentMethods().then(function (results) {
                //$scope.showLoader = false;
                if (results.status === true) {
                    for (var i = 0; i < results.data.length; i++) {
                        $scope.availablePaymentMethods.push({ value: results.data[i].PolicyMemberID, text: results.data[i].PolicyMemberName })
                    }
                }
                else {
                    $scope.availablePaymentMethods = [];
                }
            });
        };

        $scope.loadCurrencyDetails = function () {
            PaymentService.loadCurrencies().then(function (results) {
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

    $scope.loadPaymentDetails = function (BUID) {
        $scope.showLoader = true;
        PaymentService.getAllPayments(BUID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.availablePayments = results.data;
                $scope.data = angular.copy($scope.availablePayments);
                $scope.viewby = "10";
                $scope.totalItems = $scope.data.length;
                $scope.currentPage = 1;
                $scope.itemsPerPage = $scope.viewby;
                $scope.maxSize = 10; //Number of pager buttons to show

                $scope.setItemsPerPage($scope.viewby);
            }
            else {
                $scope.availablePayments = [];
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
        $scope.data = filterFilter($scope.availablePayments, searchText);
        $scope.viewby = "10";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 10;
        $scope.setItemsPerPage($scope.viewby);
    };

    $scope.activatePaymentListTab = function () {
        $("#list-tab-1").addClass('active');
        $("#tab-1").addClass('tab-pane active');

        $("#list-tab-2").removeClass('active');
        $("#tab-2").removeClass('tab-pane active');

        $("#tab-1").css("display", "block");
        $("#tab-2").css("display", "none");
    };

    $scope.activatePaymentTab = function () {
        $("#list-tab-1").removeClass('active');
        $("#tab-1").removeClass('tab-pane active');

        $("#list-tab-2").addClass('active');
        $("#tab-2").addClass('tab-pane active');

        $("#tab-1").css("display", "none");
        $("#tab-2").css("display", "block");
    };

    $scope.loadClientsByBUID = function (businessUnitID) {
        PaymentService.getAllClientsByBUID(businessUnitID).then(function (results) {
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableClients.push({ value: results.data[i].ClientID, text: results.data[i].ClientName })
                }
            }
            else {
                $scope.availableClients = [];
            }
        });
    };

    $scope.loadPolicyInfoRecordingsByClient = function (clientID) {
        PaymentService.getPolicyInfoRecordingsByClient(clientID).then(function (results) {
            if (results.status === true) {
                if (results.data.length > 0) {
                    for (var i = 0; i < results.data.length; i++) {
                        $scope.availablePolicyInfoRecordings.push({ value: results.data[i].PolicyInfoRecID, text: results.data[i].PolicyNumber })
                    }
                }
                else {
                    $scope.availablePolicyInfoRecordings = [];
                }
            }
            else {
                $scope.availablePolicyInfoRecordings = [];
            }
        });
    };

    $scope.loadChargeTypes = function () {
        PaymentService.getAllChargeTypes().then(function (results) {
            if (results.status === true) {
                $scope.chargeTypeDetails = results.data;

                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableChargeTypes.push({ value: results.data[i].ChargeTypeID, text: results.data[i].ChargeTypeName })
                }
            }
            else {
                $scope.availableChargeTypes = [];
                $scope.chargeTypeDetails = [];
            }
        });
    };

    $scope.loadPolicyDetails = function (PoilicyID) {
        PaymentService.loadPolicyDetails(PoilicyID).then(function (results) {
            if (results.status === true) {
                // $scope.paymentObj.DebitNoteList = results.data;
                $scope.paymentObj.Cuurency = results.data.SumAssuredCurrencyCode;
                $scope.paymentObj.PaymentAmount = results.data.GrossPremium;
                
                if (results.data.NonCommissionPremium === undefined || results.data.NonCommissionPremium === "" || results.data.NonCommissionPremium === null) {
                    results.data.NonCommissionPremium = 0;
                }
                else {
                    $scope.paymentObj.DebitNoteList[0].TotalNonCommissionPremium = results.data.NonCommissionPremium;
                    $scope.paymentObj.DebitNoteList[0].TotalGrossPremium = results.data.PremiumIncludingTax;
                }

                if (results.data.GrossPremium === undefined || results.data.GrossPremium === "" || results.data.GrossPremium === null) {
                    $scope.paymentObj.DebitNoteList[0].TotalGrossPremium = 0;
                    $scope.paymentObj.DebitNoteList[0].GrossPremium = 0;
                }
                else {
                    $scope.paymentObj.DebitNoteList[0].TotalGrossPremium = results.data.GrossPremium;
                    $scope.paymentObj.DebitNoteList[0].TotalGrossPremium = results.data.PremiumIncludingTax;
                }
               // $scope.bankObj.PolicyInfoRecID = PoilicyID;
            //    $scope.policyInfoChargeListTemp[0].SumAssuredCurrencyTypeID = results.data.SumAssuredCurrencyTypeID;
             //   $scope.policyInfoChargeListTemp[0].Amount = results.data.OtherExcessAmount;
               // $scope.chargeTypeDetails = results.data;

                //for (var i = 0; i < results.data.length; i++) {
                //    $scope.availableChargeTypes.push({ value: results.data[i].ChargeTypeID, text: results.data[i].ChargeTypeName })
                //}
            }
            else {
              //  $scope.availableChargeTypes = [];
              //  $scope.chargeTypeDetails = [];
            }
        });
    };

    $scope.loadClientByID = function (clientID, isEdit) {
        if (!isEdit) {
            $scope.ClearFields();
            $scope.addItem();
        }

        $scope.showLoader = true;
        PaymentService.getClientByID(clientID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.isCustomerAvailable = true;
                $scope.cusObj = results.data;
                $scope.loadPolicyInfoRecordingsByClient(clientID);
            }
            else {
                $scope.isCustomerAvailable = false;
                $scope.cusObj = {};
                $scope.availablePolicyInfoRecordings = [];
            }
        });
    };

    $scope.editPayment = function (paymentID) {
        $scope.activatePaymentTab();

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
                $scope.loadClientByID($scope.paymentObj.ClientID, true);
            }
            else {
                $scope.paymentObj = {};
                $scope.paymentObj.PaymentAmount = 0;
                $scope.paymentObj.DebitNoteList = [];

                $scope.isPaymentAddMode = true;
            }
        });
    };

    $scope.changeCustomer = function () {
        $scope.isCustomerAvailable = false;
        $scope.cusObj = {};
    };

    $scope.addItem = function () {
        $scope.paymentObj.DebitNoteList.push({PolicyInfoRecID: "", TotalNonCommissionPremium: 0, TotalGrossPremium: 0, PolicyInfoPaymentLists: [] });
    };

    $scope.deleteItem = function (deleteIndex) {
        $scope.paymentObj.DebitNoteList.splice(deleteIndex, 1);

        $scope.paymentObj.PaymentAmount = 0;

        for (var i = 0; i < $scope.paymentObj.DebitNoteList.length; i++) {
            $scope.paymentObj.PaymentAmount = $scope.paymentObj.PaymentAmount + $scope.paymentObj.DebitNoteList[i].TotalNonCommissionPremium;
        }
    };

    $scope.managePolicyInfoRecordings = function (idx) {
        $modal.open({
            templateUrl: 'ngTemplatePolicyInfo',
            backdrop: 'static',
            windowClass: 'app-modal-window-pir',
            controller: [
                    '$scope', '$modalInstance', '$http', function ($scopeChild, $modalInstance, $http) {

                        $scopeChild.policyInfoPaymentListTemp = []; 
                        $scopeChild.availableBankss = angular.copy($scope.availableBanks);
                        $scopeChild.availablePaymentMethodss = angular.copy($scope.availablePaymentMethods);

                        $scopeChild.addPolicyInfoDetails = function () {
                            //  $scopeChild.policyInfoPaymentListTemp.push({ PolicyInfoRecID: "", NonCommissionPremium: "", GrossPremium: "", PolicyInfoChargeList: [] });
                            $scopeChild.policyInfoPaymentListTemp.push({ PolicyInfoRecID: "", PolicyInfoChargeList: [] });
                        };

                        $scopeChild.deletePolicyInfoDetails = function (deleteIndex) {
                            $scopeChild.policyInfoPaymentListTemp.splice(deleteIndex, 1);
                        };

                        if ($scope.paymentObj.DebitNoteList[idx].PolicyInfoPaymentLists.length > 0) {
                            $scopeChild.policyInfoPaymentListTemp = angular.copy($scope.paymentObj.DebitNoteList[idx].PolicyInfoPaymentLists);
                            //$scopeChild.policyInfoPaymentListTemp[i].BankAmount = $scope.paymentObj.DebitNoteList[idx].TotalGrossPremium;
                        }
                        else {
                            $scopeChild.addPolicyInfoDetails();
                        }

                        $scopeChild.policyInfoChargeListTemp = [];
                        $scopeChild.availableCurrenciess = angular.copy($scope.availableCurrencies)
                        
                        $scopeChild.savePolicyInfoDetails = function () {
                            //$scope.paymentObj.DebitNoteList[idx].TotalNonCommissionPremium = 0;
                            //$scope.paymentObj.DebitNoteList[idx].TotalGrossPremium = 0;
                            //$scope.paymentObj.PaymentAmount = 0;
                           

                            for (var i = 0; i < $scopeChild.policyInfoPaymentListTemp.length; i++) {
                                $scopeChild.policyInfoPaymentListTemp[i].PaymentDate = $scope.getFormattedDate($scopeChild.policyInfoPaymentListTemp[i].PaymentDate);
                                $scopeChild.policyInfoPaymentListTemp[i].RequestingDate = $scope.getFormattedDate($scopeChild.policyInfoPaymentListTemp[i].RequestingDate);
                                
                            }

                            $scope.paymentObj.DebitNoteList[idx].PolicyInfoPaymentLists = $scopeChild.policyInfoPaymentListTemp;
                            //for (var i = 0; i < $scope.paymentObj.DebitNoteList.length; i++) {
                            //    $scope.paymentObj.PaymentAmount = $scope.paymentObj.PaymentAmount + $scope.paymentObj.DebitNoteList[i].TotalNonCommissionPremium;
                            //}

                            $modalInstance.close();
                        };

                        $scopeChild.cancel = function () {
                            $modalInstance.dismiss('cancel');
                        };

                        $scopeChild.managePolicyInfoChargeDetails = function (idx) {
                            $modal.open({
                                templateUrl: 'ngTemplatePolicyInfoCharge',
                                backdrop: 'static',
                                windowClass: 'app-modal-window-pic',
                                controller: [
                                        '$scope', '$modalInstance', '$http', function ($scopeSubChild, $modalInstance, $http) {

                                            $scopeSubChild.policyInfoChargeListTemp = [];
                                            $scopeSubChild.availableCurrenciess = angular.copy($scope.availableCurrencies);

                                            $scopeSubChild.addPolicyInfoChargeDetails = function () {
                                                $scopeSubChild.policyInfoChargeListTemp.push({ ChargeTypeID: "", Amount: "", IsCR: false });
                                            };

                                            $scopeSubChild.deletePolicyInfoChargeDetails = function (deleteIndex) {
                                                $scopeSubChild.policyInfoChargeListTemp.splice(deleteIndex, 1);
                                            };

                                            $scopeSubChild.calculateAmount = function (rowNum, chargeTypeID) {
                                                var percentage = 0;

                                                for (var i = 0; i < $scope.chargeTypeDetails.length; i++) {
                                                    if ($scope.chargeTypeDetails[i].ChargeTypeID == chargeTypeID) {
                                                        percentage = $scope.chargeTypeDetails[i].Percentage;
                                                        break;
                                                    }
                                                }

                                                $scopeSubChild.policyInfoChargeListTemp[rowNum].Amount = ($scopeChild.policyInfoPaymentListTemp[idx].NonCommissionPremium * percentage) / 100;
                                            };

                                            if ($scopeChild.policyInfoPaymentListTemp[idx].PolicyInfoChargeList.length > 0) {
                                                $scopeSubChild.policyInfoChargeListTemp = angular.copy($scopeChild.policyInfoPaymentListTemp[idx].PolicyInfoChargeList);

                                                for (var i = 0; i < $scopeSubChild.policyInfoChargeListTemp.length; i++) {
                                                    $scopeSubChild.calculateAmount(i, $scopeSubChild.policyInfoChargeListTemp[i].ChargeTypeID);
                                                }
                                            }
                                            else {
                                                $scopeSubChild.addPolicyInfoChargeDetails();
                                            }

                                            $scopeSubChild.savePolicyInfoChargeDetails = function () {
                                                
                                                $scopeChild.policyInfoPaymentListTemp[idx].PolicyInfoChargeList = $scopeSubChild.policyInfoChargeListTemp;
                                                $modalInstance.close();
                                            };

                                            $scopeSubChild.cancel = function () {
                                                $modalInstance.dismiss('cancel');
                                            };
                                        }
                                ],
                            });
                        };
                    }
            ],
        });
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


    $scope.savePayment = function () {
        $scope.showLoader = true;
        noty({
            text: 'Do you want to Save Payment Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();

                            $scope.paymentObj.ClientID = $scope.cusObj.ClientID;
                            //for (var i = 0; i < $scope.paymentObj.DebitNoteList.length; i++) {
                            //    $scope.paymentObj.DebitNoteList[i].RequestedDate = $scope.getFormattedDate($scope.cusReqObj.RequestedDate);
                            //}
                           

                            PaymentService.savePayment($scope.paymentObj, $scope.currentUser.UserID).then(function (results) {
                                $scope.showLoader = false;
                                if (results.status === true) {
                                    noty({
                                        text: 'Successfully Saved Payment Details',
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
                                    //$scope.activatePaymentListTab();
                                    
                                }
                                else {
                                    noty({
                                        text: 'Error Saving Payment Details',
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

    $scope.updatePayment = function () {
        $scope.showLoader = true;
        noty({
            text: 'Do you want to Update Payment Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();

                            $scope.paymentObj.ClientID = $scope.cusObj.ClientID;

                            PaymentService.updatePayment($scope.paymentObj, $scope.currentUser.UserID).then(function (results) {
                                $scope.showLoader = false;
                                if (results.status === true) {
                                    noty({
                                        text: 'Successfully Updated Payment Details',
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
                                    $scope.activatePaymentListTab();
                                }
                                else {
                                    noty({
                                        text: 'Error Updating Payment Details',
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

    $scope.cancelPayment = function () {
        $scope.ClearFields();
        $scope.refreshContent();
        $scope.activatePaymentListTab();
    };
});


