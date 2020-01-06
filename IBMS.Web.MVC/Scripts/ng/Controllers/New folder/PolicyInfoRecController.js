'use strict';

ibmsApp.controller("PolicyInfoRecController", function ($scope, $http, $rootScope, PolicyInfoRecService, $location, AuthService, filterFilter, $modal) {

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
        $scope.policyInfoRecObj.IsActive = false;
        $scope.businessUnitID = "";
        $scope.policyInfoRecObj.PolicyCommissionPaymentDetails = [];
        $scope.policyInfoRecObj.policyInfoChargeList = [];
        $scope.availablePolicyInfoRecordings = [];
        $scope.availableTransaction = [];
        $scope.availableQuotationHeaders = [];
        $scope.availableQuoteInfoInsCompanyDetails = [];
        $scope.availableCurrencies = [];
        $scope.availableCommissionTypes = [];
        $scope.availableComStructHeaders = [];
        $scope.availableComStructLines = [];
        $scope.policyInfoRecObjs = [];
        $scope.availableChargeTypes = [];
        $scope.availableIntroducers = [];
        $scope.availableQuoteInfoInsCompanys = [];
        $scope.loadInsuranceDetails();
        $scope.loadChargeTypes();
        $scope.getCurrentUser();
        $scope.loadCurrencyDetails();
        $scope.loadCommissionTypes();
        $scope.loadTransactionTypeDetails();
        $scope.loadIntroducer();
        // $scope.loadCommissionStructureLines();
         $scope.loadCommissionStructureHeaders();
        $scope.addItem();
        $scope.addItems();
    };

    $scope.refreshContent = function () {
        //$scope.loadPaymentDetails();
        //$scope.search_query = "";
    };
    $scope.cancelCustomerRequest = function () {
        location.reload();
    }
    $scope.ClearFields = function () {
        //location.reload();
        //$scope.isPaymentAddMode = true;
        //$scope.isCustomerAvailable = false;
       // $scope.isViewMode = false;
       // $scope.isClientReqAddMode = true;
       // $scope.policyInfoRecObj = {};
        //$scope.policyCommissionPayment = {};
        //$scope.addItem();
      //  $scope.policyInfoRecObj.PolicyCommissionPaymentDetails = [];
        //$scope.cusObj = {};
        //$scope.paymentObj = {};
        //$scope.paymentObj.PaymentAmount = 0;
        //$scope.paymentObj.DebitNoteList = [];
    };

    $scope.loadPolicyInfoRecDetails = function (businessUnitID) {
        $scope.showLoader = true;
        PolicyInfoRecService.getAllPolicyInfoRecordingsByBUID(businessUnitID).then(function (results) {
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

    $scope.loadQuotationHeadersByBUID = function (businessUnitID) {
        PolicyInfoRecService.getAllQuotationHeadersByBUID(businessUnitID).then(function (results) {
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableQuotationHeaders.push({ value: results.data[i].QuotationHeaderID, text: results.data[i].ClientName + " (Quotation Header ID: " + results.data[i].QuotationHeaderID + ")" });
                }
            }
            else {
                $scope.availableQuotationHeaders = [];
            }
        });
    };

    $scope.loadCommissionTypes = function () {
        PolicyInfoRecService.loadCommissionTypes().then(function (results) {
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
    $scope.loadCommissionStructureHeaders = function () {
        PolicyInfoRecService.loadCommissionStructureHeaders().then(function (results) {
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableComStructHeaders.push({ value: results.data[i].CommisionStructureID, text: results.data[i].CommisionStructureName});
                }
            }
            else {
                $scope.availableComStructHeaders = [];
            }
        });
    };
    $scope.LoadComStruLine = function (ComStruLineID) {
        PolicyInfoRecService.loadCommissionStructureLines(ComStruLineID).then(function (results) {
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableComStructLines.push({ value: results.data[i].CommisionStructureLineID, text: results.data[i].CommisionStructureName + " - " + results.data[i].CommisionStructureLineID });
                    if ($scope.isClientReqAddMode == false) {
                        $scope.policyInfoRecObj.PolicyCommissionPaymentDetails[i].ComStructLineID = $scope.policyInfoRecObj.PolicyCommissionPaymentDetails[i].ComStructLineID + "";
                    }
                    
                }
            }
            else {
                $scope.availableComStructLines = [];
            }
        });
    };
    $scope.LoadComStruLineDetails = function (ComStruLineID, index, PolicyCommissionPaymentDetails) {
        PolicyInfoRecService.LoadComStruLineDetails(ComStruLineID).then(function (results) {
            if (results.status === true) {

                $scope.policyInfoRecObj.PolicyCommissionPaymentDetails[index].RateValue = results.data.RateValue;
                $scope.policyInfoRecObj.PolicyCommissionPaymentDetails[index].CommisionValue = $scope.policyInfoRecObj.PolicyCommissionPaymentDetails[index].Amount * $scope.policyInfoRecObj.PolicyCommissionPaymentDetails[index].RateValue / 100;
                for (var i = 0; i < PolicyCommissionPaymentDetails.length; i++) {
                    if(i==0){
                        var TotalPremium = $scope.policyInfoRecObj.PolicyCommissionPaymentDetails[i].CommisionValue;
                    }
                    else {
                        TotalPremium = TotalPremium + $scope.policyInfoRecObj.PolicyCommissionPaymentDetails[i].CommisionValue;
                    }
                }
                $scope.policyInfoRecObj.TotalCommission = TotalPremium;
            }
            
            else {
                $scope.availableComStructLines = [];
            }
        });
    };

    $scope.calAmount = function (policyInfoChargeList) {
        
        for (var i = 0; i < policyInfoChargeList.length; i++) {
            if(i==0){
                var GrossPremium = $scope.policyInfoRecObj.PremiumIncludingTax + $scope.policyInfoRecObj.policyInfoChargeList[i].Amount;
            }
            else {
                GrossPremium = GrossPremium + $scope.policyInfoRecObj.policyInfoChargeList[i].Amount;
            }
            
            }
         $scope.policyInfoRecObj.GrossPremium = GrossPremium;
        //$scope.policyInfoRecObj.GrossPremium = $scope.policyInfoRecObj.PremiumIncludingTax + $scope.policyInfoRecObj.policyInfoChargeList[index].Amount;
              //  $scope.policyInfoRecObj.PolicyCommissionPaymentDetails[index].CommisionValue = $scope.policyInfoRecObj.PolicyCommissionPaymentDetails[index].Amount * $scope.policyInfoRecObj.PolicyCommissionPaymentDetails[index].Precentage / 100;
           
    };

    $scope.loadClientRequestDetailsByID = function (quotationObj) {
        $scope.ClearFields();
        $scope.isQuotationAvailable = true;

        $scope.quotationHeaderObj = quotationObj;

        
            //$scope.availableQuoteInfoInsCompanys.push({ value: quotationOb.InsuranceCompanyID, text: quotationOb.InsuranceCompanyID.InsuranceCompanyName });
        
        $scope.showLoader = true;
        PolicyInfoRecService.getClientRequestByID($scope.quotationHeaderObj.ClientRequestHeaderID).then(function (results) {
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
        PolicyInfoRecService.loadQuoteInfoInsCompanyLineDetailsByQuotation(quotationHeaderID).then(function (results) {
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

    $scope.loadQuotationDetailsByID = function (quotationHeaderID) {
        $scope.showLoader = true;
        PolicyInfoRecService.loadQuotationHeaderByID(quotationHeaderID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.loadClientRequestDetailsByID(results.data);
                $scope.loadQuoteInfoInsCompanyLineDetails(quotationHeaderID);
            }
        });
    };

    $scope.SelectedInsuranceCompany = function (QuotationDetailsInsCompanyLineID) {
        $scope.availableQuoteInfoInsCompanys = [];
        $scope.showLoader = true;
        PolicyInfoRecService.loadQuotationHeaderInsuranceByID(QuotationDetailsInsCompanyLineID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                //$scope.loadClientRequestDetailsByID(results.data);
                //$scope.loadQuoteInfoInsCompanyLineDetails(quotationHeaderID);

                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableQuoteInfoInsCompanys.push({ value: results.data[i].InsuranceCompanyID, text: results.data[i].InsuranceCompanyName });
                }


               
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
        PolicyInfoRecService.loadCurrencies().then(function (results) {
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

    $scope.loadTransactionTypeDetails = function () {
        PolicyInfoRecService.loadTransactionTypeDetails().then(function (results) {
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableTransaction.push({ value: results.data[i].TransactionTypeID, text: results.data[i].Description });
                }
            }
            else {
                $scope.availableTransaction = [];
            }
        });
    };

    $scope.loadInsuranceDetails = function () {
        PolicyInfoRecService.loadInsuranceDetails().then(function (results) {
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableQuoteInfoInsCompanys.push({ value: results.data[i].InsuranceCompanyID, text: results.data[i].InsuranceCompanyName });
                }
            }
            else {
                $scope.availableQuoteInfoInsCompanys = [];
            }
        });
    };

    $scope.SelectInsuranceCompany = function (quotationHeaderID) {
        PolicyInfoRecService.SelectInsuranceCompany(quotationHeaderID).then(function (results) {
            if (results.status === true) {

                $scope.availableQuoteInfoInsCompanys.push({ value: results.data.QuotationDetailsInsCompanyLineID.InsuranceCompanyID, text: results.data.QuotationDetailsInsCompanyLineID.InsuranceCompanyName });
                //$scope.availableQuoteInfoInsCompanys = results.data.QuotationDetailsInsCompanyLineID.InsuranceCompanyID;
            }
            else {
            }
        });
    };

    $scope.addItem = function () {
        $scope.policyInfoRecObj.PolicyCommissionPaymentDetails.push({ CommisionTypeID: "",Amount: "", ComStructLineID:"",Precentage:"",CommisionValue: ""});
    };

    $scope.addItems = function () {
        $scope.policyInfoRecObj.policyInfoChargeList.push({ ChargeTypeID: "", Amount: "", IsCR: true });
        //$scope.policyInfoRecObj.GrossPremium = $scope.policyInfoRecObj.policyInfoChargeList[0].Amount;
        //$scope.policyInfoRecObj.GrossPremium = $scope.policyInfoRecObj.GrossPremium + $scope.policyInfoRecObj.policyInfoChargeList[1].Amount;
        //$scope.policyInfoRecObj.GrossPremium = $scope.policyInfoRecObj.GrossPremium + $scope.policyInfoRecObj.policyInfoChargeList[2].Amount;
    }; 

    $scope.deleteItem = function (deleteIndex, PolicyCommissionPaymentDetails) {
        $scope.policyInfoRecObj.PolicyCommissionPaymentDetails.splice(deleteIndex, 1);
        for (var i = 0; i < PolicyCommissionPaymentDetails.length; i++) {
            if (i == 0) {
                var TotalPremium = $scope.policyInfoRecObj.PolicyCommissionPaymentDetails[i].CommisionValue;
            }
            else {
                TotalPremium = TotalPremium + $scope.policyInfoRecObj.PolicyCommissionPaymentDetails[i].CommisionValue;
            }
        }
        $scope.policyInfoRecObj.TotalCommission = TotalPremium;
    };

    $scope.deletePolicyInfoChargeDetails = function (deleteIndex) {
        $scope.policyInfoRecObj.policyInfoChargeList.splice(deleteIndex, 1);
    };

    $scope.activateNewClientRequestTab = function () {
        $("#list-tab-1").removeClass('active');
        $("#tab-1").removeClass('tab-pane active');

        $("#list-tab-2").addClass('active');
        $("#tab-2").addClass('tab-pane active');

        $("#tab-1").css("display", "none");
        $("#tab-2").css("display", "block");
    };

    $scope.loadChargeTypes = function () {
        PolicyInfoRecService.getAllChargeTypes().then(function (results) {
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

    $scope.SwapChargeType = function (availableChargeTypesList) {
        $scope.availableChargeTypes = [];
        $scope.availableChargeTypes = availableChargeTypesList;
    }

    $scope.getInsChargeType = function (quotationHeaderID, insuranceCompanyID) {

        $scope.showLoader = true;
        PolicyInfoRecService.getInsChargeType(quotationHeaderID, insuranceCompanyID).then(function (results) {
            $scope.availableChargeTypes = [];

            if (results.status === true) {
                
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableChargeTypes.push({ value: results.data[i].ChargeTypeID, text: results.data[i].ChargeTypeName })
                    $scope.policyInfoRecObj.policyInfoChargeList[i].policyInfoCharge.ChargeTypeID = $scope.availableChargeTypes;
                }
                
                //$scope.SwapChargeType($scope.availableChargeTypesList);
                //$scope.availableChargeTypes.push({ value: results.data.policyInfoCharge.ChargeTypeID, text: results.data.policyInfoCharge.ChargeTypeName });
            }
            else {
                $scope.availableChargeTypes = [];
               
            }

        });



    };

    //$scope.calCurrency
    $scope.calCurrency = function () {

        
        var currencyConvert = $scope.policyInfoRecObj.SumAssured * $scope.policyInfoRecObj.CurrencyRate;
            
        $scope.policyInfoRecObj.SumFinalAssured = currencyConvert;
        //$scope.policyInfoRecObj.GrossPremium = $scope.policyInfoRecObj.PremiumIncludingTax + $scope.policyInfoRecObj.policyInfoChargeList[index].Amount;
        //  $scope.policyInfoRecObj.PolicyCommissionPaymentDetails[index].CommisionValue = $scope.policyInfoRecObj.PolicyCommissionPaymentDetails[index].Amount * $scope.policyInfoRecObj.PolicyCommissionPaymentDetails[index].Precentage / 100;

    };


    
    $scope.loadIntroducer = function () {
        //$scope.showLoader = true;
        PolicyInfoRecService.loadIntroducer().then(function (results) {
            //$scope.showLoader = false;
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableIntroducers.push({ value: results.data[i].IntroducerID, text: results.data[i].IntroducerName })
                }
            }
            else {
                $scope.availableIntroducers = [];
            }
        });
    };

    $scope.editPolicyRecInfo = function (PolicyInfoRecID,QuatationID) {
        $scope.activateNewClientRequestTab();

        $scope.showLoader = true;
        PolicyInfoRecService.editPolicyRecInfo(PolicyInfoRecID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.isViewMode = false;
                $scope.isClientReqAddMode = false;
                $scope.isQuotationAvailable = true;
                $scope.policyInfoRecObj = results.data;
                $scope.LoadComStruLine($scope.policyInfoRecObj.CommissionStructureHeaderID);
                $scope.policyInfoRecObj.SumAssuredCurrencyTypeID = $scope.policyInfoRecObj.SumAssuredCurrencyTypeID + "";
                $scope.policyInfoRecObj.PremiumIncludingTaxCurrencyTypeID = $scope.policyInfoRecObj.PremiumIncludingTaxCurrencyTypeID + "";
                $scope.policyInfoRecObj.CommissionStructureHeaderID = $scope.policyInfoRecObj.CommissionStructureHeaderID + "";
                $scope.policyInfoRecObj.QuotationDetailsInsCompanyLineID = $scope.policyInfoRecObj.QuotationDetailsInsCompanyLineID + "";
                
                $scope.loadQuotationDetailsByID(QuatationID);
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
        PolicyInfoRecService.getClientByID(clientID).then(function (results) {
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
                            $scope.policyInfoRecObj.PeriodOfCoverFromDate = $scope.getFormattedDate($scope.policyInfoRecObj.PeriodOfCoverFromDate);
                            $scope.policyInfoRecObj.PeriodOfCoverToDate = $scope.getFormattedDate($scope.policyInfoRecObj.PeriodOfCoverToDate);
                          //  $scope.policyInfoRecObj.QuotationDetailsInsCompanyLineID = 12;
                            $scope.policyInfoRecObjs = [$scope.policyInfoRecObj];
                            //$scope.cusObj.BusinessUnitID = $scope.businessUnitID;
                            //$scope.cusReqObj.RequestedDate = $scope.getFormattedDate($scope.cusReqObj.RequestedDate);

                            PolicyInfoRecService.updatePolicyInfoRecording($scope.quotationHeaderObj.QuotationHeaderID, $scope.policyInfoRecObjs, $scope.currentUser.UserID).then(function (results) {
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
                                    $scope.ClearFields();
                                    $scope.refreshContent();
                                    $scope.activateClientRequestListTab();
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

    //$scopeChild.managePolicyInfoChargeDetails = function (idx) {
    //    $modal.open({
    //        templateUrl: 'ngTemplatePolicyInfoCharge',
    //        backdrop: 'static',
    //        windowClass: 'app-modal-window-pic',
    //        controller: [
    //                '$scope', '$modalInstance', '$http', function ($scopeSubChild, $modalInstance, $http) {

    //                    $scopeSubChild.policyInfoChargeListTemp = [];
    //                    $scopeSubChild.availableChargeTypeList = angular.copy($scope.availableChargeTypes);

    //                    $scopeSubChild.addPolicyInfoChargeDetails = function () {
    //                        $scopeSubChild.policyInfoChargeListTemp.push({ ChargeTypeID: "", Amount: "", IsCR: false });
    //                    };

    //                    $scopeSubChild.deletePolicyInfoChargeDetails = function (deleteIndex) {
    //                        $scopeSubChild.policyInfoChargeListTemp.splice(deleteIndex, 1);
    //                    };

    //                    $scopeSubChild.calculateAmount = function (rowNum, chargeTypeID) {
    //                        var percentage = 0;

    //                        for (var i = 0; i < $scope.chargeTypeDetails.length; i++) {
    //                            if ($scope.chargeTypeDetails[i].ChargeTypeID == chargeTypeID) {
    //                                percentage = $scope.chargeTypeDetails[i].Percentage;
    //                                break;
    //                            }
    //                        }

    //                        $scopeSubChild.policyInfoChargeListTemp[rowNum].Amount = ($scopeChild.policyInfoPaymentListTemp[idx].NonCommissionPremium * percentage) / 100;
    //                    };

    //                    if ($scopeChild.policyInfoPaymentListTemp[idx].PolicyInfoChargeList.length > 0) {
    //                        $scopeSubChild.policyInfoChargeListTemp = angular.copy($scopeChild.policyInfoPaymentListTemp[idx].PolicyInfoChargeList);

    //                        for (var i = 0; i < $scopeSubChild.policyInfoChargeListTemp.length; i++) {
    //                            $scopeSubChild.calculateAmount(i, $scopeSubChild.policyInfoChargeListTemp[i].ChargeTypeID);
    //                        }
    //                    }
    //                    else {
    //                        $scopeSubChild.addPolicyInfoChargeDetails();
    //                    }

    //                    $scopeSubChild.savePolicyInfoChargeDetails = function () {
    //                        $scopeChild.policyInfoPaymentListTemp[idx].PolicyInfoChargeList = $scopeSubChild.policyInfoChargeListTemp;
    //                        $modalInstance.close();
    //                    };

    //                    $scopeSubChild.cancel = function () {
    //                        $modalInstance.dismiss('cancel');
    //                    };
    //                }
    //        ],
    //    });
    //};

    $scope.savePolicyInfoRecording = function () {
        $scope.showLoader = true;
        noty({
            text: 'Do you want to Save Customer Request Details?',
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
                            $scope.policyInfoRecObj.PeriodOfCoverFromDate = $scope.getFormattedDate($scope.policyInfoRecObj.PeriodOfCoverFromDate);
                            $scope.policyInfoRecObj.PeriodOfCoverToDate = $scope.getFormattedDate($scope.policyInfoRecObj.PeriodOfCoverToDate);
                          //  $scope.policyInfoRecObj.QuotationDetailsInsCompanyLineID = 12;
                            $scope.policyInfoRecObjs = [$scope.policyInfoRecObj];
                            
                            PolicyInfoRecService.savePolicyInfoRecording($scope.quotationHeaderObj.QuotationHeaderID, $scope.policyInfoRecObjs, $scope.currentUser.UserID).then(function (results) {
                                $scope.showLoader = false;
                                //alert(angular.toJson(results));
                                if (results.status === true) {
                                    noty({
                                        text: 'Successfully Saved Customer Request Details',
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
});


