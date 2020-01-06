'use strict';

ibmsApp.controller("ReceivedQuotationController", function ($scope, $http, $rootScope,ReceivedQuotationService, PolicyInfoRecService, $location, AuthService, filterFilter, $modal) {

    $scope.getAllInsuranceSubClass = function () {
        $scope.showLoader = true;
         ReceivedQuotationService.getAvailableSubInsurance().then(function (results) {
            //$scope.showLoader = false;
            $scope.isInsuranceClassLoaded = true;

            if (results.status === true) {
                $scope.availableInsuranceClass = results.data;
            }
            else {
                $scope.availableInsuranceClass = [];
            }
        });
    };

    $scope.uploadFile = function () {

        var file = $scope.myFile;
        console.log('file is ');
        console.dir(file);
        var uploadUrl = "//D/";
        fileUpload.uploadFileToUrl(file, uploadUrl);
    };

    $scope.uploadedDocument = "";

    $scope.getDocument = function (e) {
        $scope.uploadedDocument = e.files[0];
    };


    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;

            if ($scope.currentUser.AccessLevelTypeName === "Admin") {
                //To Do (using a popup to select the desired business unit)
            }
            else {
                $scope.businessUnitID = $scope.currentUser.BusinessUnitID;
                $scope.loadQuotationHeadersByBUID($scope.businessUnitID);
                //$scope.loadInsSubClassesByBUID($scope.businessUnitID);
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

    $scope.LoadInsSubClass = function (InsuranceClassID) {

        InsuranceClassChange(InsuranceClassID);
    };
    function InsuranceClassChange(InsuranceClassID) {
        // alert(InsuranceClassID);

         ReceivedQuotationService.getAvailableInsSubClass(InsuranceClassID).then(function (results) {
            $scope.showLoader = false;

            if (results.status === true) {
                $scope.availableInsuranceClass = results.data;
                //availableInsuranceClass
            }
            else {
                $scope.availableInsuranceClass = [];
            }


        });
    };

    $scope.init = function () {
        $scope.isQuotationViewMode = false;
        $scope.isQuotationEditMode = false;
        $scope.businessUnitID = "";
        $scope.cusReqObj = {};
        $scope.cusObj = {};
        $scope.quotationHeaderObj = {};
        $scope.quotHeaderObj = {};
        $scope.selectedInsCompanies = [];
        $scope.QuotationDetailsInsCompanyHeaderDetails = [];
        $scope.quotationHeaderObj.QuotationLineDetails = [];
        $scope.quotHeaderObj.QuotLineDetails = [];
        $scope.AddedInscompanies = [];
        $scope.availableTransaction = [];
        $scope.availableQuoteInfoInsCompanys = [];

        $scope.quotationLine = {};
        $scope.quotationLines = [];
        $scope.uploadedDocumentArray = [];
        $scope.saveStatus = false;
        $scope.quotationLine.quotations = []
        $scope.IsEmailDisable = true;
        $scope.availableInsClass = [];

        //$scope.loadTransactionTypeDetails();


     

        //$scope.getCurrentUser();

        //$scope.addItem();
        //$scope.getAllInsuranceSubClass();
        //$scope.loadInsuranceDetails();
        //$scope.loadInsClass();
    };

    $scope.LoadQuotation = function (quotation) {


        sessionStorage.setItem("Quotation", quotation.QuotationHeaderID);
        window.location.assign("ManageQuotationPrint");

    };

    $scope.loadTransactionTypeDetails = function () {
         ReceivedQuotationService.loadTransactionTypeDetails().then(function (results) {
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

    $scope.refreshContent = function () {
        $scope.loadQuotationHeadersByBUID($scope.businessUnitID);
        $scope.search_query = "";
    };

    $scope.ClearFields = function () {
        $scope.isQuotationViewMode = false;
        $scope.isQuotationEditMode = false;
        $scope.cusReqObj = {};
        $scope.cusObj = {};
        $scope.quotationHeaderObj = {};
        $scope.selectedInsCompanies = [];
        $scope.quotationLine.quotations = [];
        $scope.addItem();
        $scope.IsEmailDisable = true;
    };

    $scope.loadQuotationHeadersByBUID = function (businessUnitID) {
        $scope.showLoader = true;
         ReceivedQuotationService.getAllQuotationHeadersByBUID(businessUnitID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.availableQuotationHeaders = results.data;
                $scope.data = angular.copy($scope.availableQuotationHeaders);
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
        $scope.data = filterFilter($scope.availableQuotationHeaders, searchText);
        $scope.viewby = "10";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 10;
        $scope.setItemsPerPage($scope.viewby);
    };

    $scope.activateQuotationListTab = function () {
        $("#list-tab-1").addClass('active');
        $("#tab-1").addClass('tab-pane active');

        $("#list-tab-2").removeClass('active');
        $("#tab-2").removeClass('tab-pane active');

        $("#tab-1").css("display", "block");
        $("#tab-2").css("display", "none");
    };

    $scope.activateQuotationTab = function () {
        $("#list-tab-1").removeClass('active');
        $("#tab-1").removeClass('tab-pane active');

        $("#list-tab-2").addClass('active');
        $("#tab-2").addClass('tab-pane active');

        $("#tab-1").css("display", "none");
        $("#tab-2").css("display", "block");
    };

    //$scope.isInsSubClassAvailable = function (insSubClassID) {
    //    for (var i = 0; i < $scope.quotationHeaderObj.QuotationLineDetails.length; i++) {
    //        if ($scope.quotationHeaderObj.QuotationLineDetails[i].InsuranceSubClassID === insSubClassID) {
    //            return true;
    //        }
    //    }

    //    return false;
    //};


    $scope.isInsSubClassAvailable = function (insSubClassID, listObj) {
        for (var i = 0; i < listObj.length; i++) {
            if (listObj[i].InsuranceSubClassID === insSubClassID) {
                return true;
            }
        }

        return false;
    };

    $scope.isInsCompanyAvailable = function (insCompanyID) {
        for (var i = 0; i < $scope.quotationHeaderObj.QuotationLineDetails.length; i++) {
            for (var j = 0; j < $scope.quotationHeaderObj.QuotationLineDetails[i].RequestedInsuranceCompanyDetails.length; j++) {
                if ($scope.quotationHeaderObj.QuotationLineDetails[i].RequestedInsuranceCompanyDetails[j].InsuranceCompanyID === insCompanyID) {
                    return true;
                }
            }
        }

        return false;
    };

    //$scope.isInsSubClassAvailableTemp = function (insSubClassID) {
    //    for (var i = 0; i < $scope.quotationLineTemp.length; i++) {
    //        if ($scope.quotationLineTemp[i].InsuranceSubClassID === insSubClassID) {
    //            return true;
    //        }
    //    }

    //    return false;
    //};

    $scope.loadClientRequestDetailsByID = function (quotationObj, isView) {
        $scope.ClearFields();
        $scope.getQuotationDetailByID(quotationObj.QuotationHeaderID);
        $scope.isQuotationEditMode = true;

        if (isView === 'true') {
            $scope.isQuotationViewMode = true;
        }
        else {
            $scope.isQuotationViewMode = false;
        }

        $scope.activateQuotationTab();
        $scope.quotationHeaderObj = quotationObj;

        for (var i = 0; i < $scope.quotationHeaderObj.QuotationLineDetails.length; i++) {
            for (var j = 0; j < $scope.quotationHeaderObj.QuotationLineDetails[i].RequestedInsuranceCompanyDetails.length; j++) {
                $scope.selectedInsCompanies.push({ "InsSubClassID": $scope.quotationHeaderObj.QuotationLineDetails[i].InsuranceSubClassID, "InsSubClassName": $scope.quotationHeaderObj.QuotationLineDetails[i].InsuranceSubClassDescription, "InsCompanyID": $scope.quotationHeaderObj.QuotationLineDetails[i].RequestedInsuranceCompanyDetails[j].InsuranceCompanyID, "InsCompanyName": $scope.quotationHeaderObj.QuotationLineDetails[i].RequestedInsuranceCompanyDetails[j].InsuranceCompanyName, "InsCompanyEmail": $scope.quotationHeaderObj.QuotationLineDetails[i].RequestedInsuranceCompanyDetails[j].InsuranceCompanyEmail, "QuotationDetailsInsCompanyHeaderDetails": $scope.quotationHeaderObj.QuotationLineDetails[i].RequestedInsuranceCompanyDetails[j].QuotationDetailsInsCompanyHeaderDetails });
            }
        }

        $scope.showLoader = true;
         ReceivedQuotationService.getClientRequestByID($scope.quotationHeaderObj.ClientRequestHeaderID).then(function (results) {
            //$scope.showLoader = false;
            if (results.status === true) {
                $scope.cusReqObj = results.data;

                for (var i = 0; i < $scope.cusReqObj.ClientRequestLineDetails.length; i++) {
                    if ($scope.isInsSubClassAvailable($scope.cusReqObj.ClientRequestLineDetails[i].InsSubClassID, $scope.quotationHeaderObj.QuotationLineDetails)) {
                        $scope.cusReqObj.ClientRequestLineDetails[i].IsChecked = true;
                    }
                    else {
                        $scope.cusReqObj.ClientRequestLineDetails[i].IsChecked = false;
                    }
                }

                 ReceivedQuotationService.getClientByID($scope.quotationHeaderObj.ClientID).then(function (results) {
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

    $scope.loadQuotationDetailsByID = function (quotationHeaderID) {
        $scope.showLoader = true;
         ReceivedQuotationService.loadQuotationHeaderByID(quotationHeaderID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.loadClientRequestDetailsByID(results.data);
            }
        });
    };

    $scope.manageProperties = function (idx) {
        $modal.open({
            templateUrl: 'ngTemplateProperty',
            backdrop: 'static',
            //windowClass: 'app-modal-window-property',
            controller: [
                    '$scope', '$modalInstance', '$http', function ($scopeChild, $modalInstance, $http) {

                        $scopeChild.clientPropertyDetailsTemp = [];

                        if ($scope.cusReqObj.ClientRequestLineDetails[idx].ClientPropertyDetails.length > 0) {
                            $scopeChild.clientPropertyDetailsTemp = angular.copy($scope.cusReqObj.ClientRequestLineDetails[idx].ClientPropertyDetails);
                        }

                        $scopeChild.cancel = function () {
                            $modalInstance.dismiss('cancel');
                        };
                    }
            ],
        });
    };

    $scope.manageScopes = function (idx) {
        $modal.open({
            templateUrl: 'ngTemplateScopes',
            backdrop: 'static',
            //windowClass: 'app-modal-window-scopes',
            controller: [
                    '$scope', '$modalInstance', '$http', function ($scopeChild, $modalInstance, $http) {

                        $scopeChild.insSubClassScopeDetailsTemp = [];

                        if ($scope.cusReqObj.ClientRequestLineDetails[idx].ClientRequestInsSubClassScopeDetails.length > 0) {
                            $scopeChild.insSubClassScopeDetailsTemp = angular.copy(($scope.cusReqObj.ClientRequestLineDetails[idx].ClientRequestInsSubClassScopeDetails));
                        }

                        $scopeChild.cancel = function () {
                            $modalInstance.dismiss('cancel');
                        };
                    }
            ],
        });
    };

    $scope.selectInsuranceCompanies = function (idx) {
        $scope.isClientReqLineSelected = false;

        for (var i = 0; i < $scope.cusReqObj.ClientRequestLineDetails.length; i++) {
            if ($scope.cusReqObj.ClientRequestLineDetails[i].IsChecked === true) {
                $scope.isClientReqLineSelected = true;
                break;
            }
        }

        if ($scope.isClientReqLineSelected) {
            $modal.open({
                templateUrl: 'ngTemplateInsCompany',
                backdrop: 'static',
                //windowClass: 'app-modal-window-scopes',
                controller: [
                        '$scope', '$modalInstance', '$http', function ($scopeChild, $modalInstance, $http) {

                            $scopeChild.availableInsuranceCompanies = [];
                            $scopeChild.isInsCompanySelected = false;

                            $scopeChild.loadInsuranceCompanies = function (businessUnitID) {
                                $scopeChild.showLoader = true;
                                 ReceivedQuotationService.getAllInsuranceCompaniesByBUID(businessUnitID).then(function (results) {
                                    $scopeChild.showLoader = false;
                                    if (results.status === true) {
                                        $scopeChild.availableInsuranceCompanies = results.data;

                                        for (var i = 0; i < $scopeChild.availableInsuranceCompanies.length; i++) {
                                            if ($scope.isInsCompanyAvailable($scopeChild.availableInsuranceCompanies[i].InsuranceCompanyID)) {
                                                $scopeChild.availableInsuranceCompanies[i].IsChecked = true;
                                            }
                                            else {
                                                $scopeChild.availableInsuranceCompanies[i].IsChecked = false;
                                            }
                                        }
                                    }
                                    else {
                                        $scopeChild.availableInsuranceCompanies = [];
                                    }
                                });
                            };

                            $scopeChild.loadInsuranceCompanies($scope.businessUnitID);

                            $scopeChild.addInsCompany = function () {
                                for (var i = 0; i < $scopeChild.availableInsuranceCompanies.length; i++) {
                                    if ($scopeChild.availableInsuranceCompanies[i].IsChecked === true) {
                                        $scopeChild.isInsCompanySelected = true;
                                        break;
                                    }
                                }

                                $scopeChild.showLoader = true;
                                if ($scopeChild.isInsCompanySelected) {
                                    noty({
                                        text: 'Do you want to proceed? This will reset the previously added Insurance Sub Class - Insurance Company combination data',
                                        layout: 'topCenter',
                                        buttons: [
                                                {
                                                    addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                                                        $noty.close();

                                                        $scope.selectedInsCompanies = [];

                                                        for (var i = 0; i < $scope.cusReqObj.ClientRequestLineDetails.length; i++) {
                                                            if ($scope.cusReqObj.ClientRequestLineDetails[i].IsChecked === true) {
                                                                for (var j = 0; j < $scopeChild.availableInsuranceCompanies.length; j++) {
                                                                    if ($scopeChild.availableInsuranceCompanies[j].IsChecked === true) {
                                                                        var insCompanyHeaderDetails = [];
                                                                        $scope.selectedInsCompanies.push({ "InsSubClassID": $scope.cusReqObj.ClientRequestLineDetails[i].InsSubClassID, "InsSubClassName": $scope.cusReqObj.ClientRequestLineDetails[i].InsSubClassName, "InsCompanyID": $scopeChild.availableInsuranceCompanies[j].InsuranceCompanyID, "InsCompanyName": $scopeChild.availableInsuranceCompanies[j].InsuranceCompanyName, "QuotationDetailsInsCompanyHeaderDetails": [{ "PremiumIncludingTax": "", "ExcessDescription": "", "ExcessAmount": "", "QuotationDetailsInsCompanyLineDetails": [] }] });
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        //TO DO
                                                        $scopeChild.showLoader = false;
                                                        $modalInstance.close();

                                                    }
                                                },
                                                {
                                                    addClass: 'btn btn-danger btn-clean', text: 'Cancel', onClick: function ($noty) {
                                                        $noty.close();
                                                        $scope.$apply(function () {
                                                            $scopeChild.showLoader = false;
                                                        });
                                                    }
                                                }
                                        ]
                                    })
                                }
                                else {
                                    noty({
                                        text: 'Please select one or more Insurance Companies',
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
                            };

                            $scopeChild.cancel = function () {
                                $modalInstance.dismiss('cancel');
                            };
                        }
                ],
            });
        }
        else {
            noty({
                text: 'Please select one or more customer request line details',
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
    };

    $scope.deleteInsCompanyMain = function (deleteIndex) {
        $scope.selectedInsCompanies.splice(deleteIndex, 1);
    };

    $scope.addInsCompanyHeader = function (idx) {
        $scope.selectedInsCompanies[idx].QuotationDetailsInsCompanyHeaderDetails.push({ "PremiumIncludingTax": "", "ExcessDescription": "", "ExcessAmount": "", "QuotationDetailsInsCompanyLineDetails": [] });
    };

    $scope.deleteInsCompanyHeader = function (parentIndex, deleteIndex) {
        $scope.selectedInsCompanies[parentIndex].QuotationDetailsInsCompanyHeaderDetails.splice(deleteIndex, 1);
    };

    $scope.manageInsCompanyLineDetails = function (insSubClassID, insSubClassDescription, parentIdx, childIdx) {
        $modal.open({
            templateUrl: 'ngTemplateInsCompanyLine',
            backdrop: 'static',
            windowClass: 'app-modal-window-Line',
            controller: [
                    '$scope', '$modalInstance', '$http', function ($scopeChild, $modalInstance, $http) {

                        $scopeChild.quotationDetailsInsCompanyLineDetailsTemp = [];

                        $scopeChild.addInsCompanyLineDetails = function () {
                            $scopeChild.quotationDetailsInsCompanyLineDetailsTemp.push({ InsuranceSubClassID: insSubClassID, InsuranceSubClassDescription: insSubClassDescription, SumInsured: "", QuotationDetailsInsCompanyScopeDetails: [] });
                        };

                        $scopeChild.deleteInsCompanyLineDetails = function (deleteIndex) {
                            $scopeChild.quotationDetailsInsCompanyLineDetailsTemp.splice(deleteIndex, 1);
                        };

                        if ($scope.selectedInsCompanies[parentIdx].QuotationDetailsInsCompanyHeaderDetails[childIdx].QuotationDetailsInsCompanyLineDetails.length > 0) {
                            $scopeChild.quotationDetailsInsCompanyLineDetailsTemp = angular.copy($scope.selectedInsCompanies[parentIdx].QuotationDetailsInsCompanyHeaderDetails[childIdx].QuotationDetailsInsCompanyLineDetails);
                        }
                        else {
                            $scopeChild.addInsCompanyLineDetails();
                        }

                        $scopeChild.saveInsCompanyLineDetails = function () {
                            $scope.selectedInsCompanies[parentIdx].QuotationDetailsInsCompanyHeaderDetails[childIdx].QuotationDetailsInsCompanyLineDetails = $scopeChild.quotationDetailsInsCompanyLineDetailsTemp;
                            $modalInstance.close();
                        };

                        $scopeChild.cancel = function () {
                            $modalInstance.dismiss('cancel');
                        };

                        $scopeChild.manageInsCompanyScopeDetails = function (idx) {
                            $modal.open({
                                templateUrl: 'ngTemplateInsCompanyScope',
                                backdrop: 'static',
                                windowClass: 'app-modal-window-Scope',
                                controller: [
                                        '$scope', '$modalInstance', '$http', function ($scopeSubChild, $modalInstance, $http) {

                                            $scopeSubChild.quotationDetailsInsCompanyScopeTemp = [];

                                            $scopeSubChild.addInsCompanyScopeDetails = function () {
                                                $scopeSubChild.quotationDetailsInsCompanyScopeTemp.push({ ScopeDescription: "", ExcessType: "", ExcessAmount: "" });
                                            };

                                            $scopeSubChild.deleteInsCompanyScopeDetails = function (deleteIndex) {
                                                $scopeSubChild.quotationDetailsInsCompanyScopeTemp.splice(deleteIndex, 1);
                                            };

                                            //if ($scope.selectedInsCompanies[parentIdx].QuotationDetailsInsCompanyHeaderDetails[childIdx].QuotationDetailsInsCompanyLineDetails[idx].QuotationDetailsInsCompanyScopeDetails.length > 0) {
                                            //    $scopeSubChild.quotationDetailsInsCompanyScopeTemp = angular.copy($scope.selectedInsCompanies[parentIdx].QuotationDetailsInsCompanyHeaderDetails[childIdx].QuotationDetailsInsCompanyLineDetails[idx].QuotationDetailsInsCompanyScopeDetails);
                                            //}
                                            //else {
                                            //    $scopeSubChild.addInsCompanyScopeDetails();
                                            //}

                                            if ($scopeChild.quotationDetailsInsCompanyLineDetailsTemp[idx].QuotationDetailsInsCompanyScopeDetails.length > 0) {
                                                $scopeSubChild.quotationDetailsInsCompanyScopeTemp = angular.copy($scopeChild.quotationDetailsInsCompanyLineDetailsTemp[idx].QuotationDetailsInsCompanyScopeDetails);
                                            }
                                            else {
                                                $scopeSubChild.addInsCompanyScopeDetails();
                                            }

                                            //$scopeSubChild.saveInsCompanyScopeDetails = function () {
                                            //    $scope.selectedInsCompanies[parentIdx].QuotationDetailsInsCompanyHeaderDetails[childIdx].QuotationDetailsInsCompanyLineDetails[idx].QuotationDetailsInsCompanyScopeDetails = $scopeSubChild.quotationDetailsInsCompanyScopeTemp;
                                            //    $modalInstance.close();
                                            //};

                                            $scopeSubChild.saveInsCompanyScopeDetails = function () {
                                                $scopeChild.quotationDetailsInsCompanyLineDetailsTemp[idx].QuotationDetailsInsCompanyScopeDetails = $scopeSubChild.quotationDetailsInsCompanyScopeTemp;
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

    $scope.loadProposalForm = function (quotationHeader) {
        $scope.showLoader = true;
         ReceivedQuotationService.loadProposalForm(quotationHeader.QuotationHeaderID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                if (results.data != 0) {



                    $scope.isQuotationViewMode = true;
                    $scope.coverNoteProposal = {};
                    $scope.quotationHeaderObj = {};
                    $scope.coverNoteProposal = results.data;
                    $scope.coverNoteProposal.QuotationHeaderID = results.data[0].QuotationHeaderID;
                    //  $scope.coverNoteProposal.inscompany = $scope.AddedInscompanies[itx];
                    $scope.coverNoteProposal.InsuranceSubClassID = results.data[0].InsuranceSubClassID;
                    $scope.coverNoteProposal.RequestedDate = results.data[0].RequestedDate;
                    //  $scope.coverNote.CoverNoteNo = "TCN-" + $scope.HeaderObj.quotationHeaderID;
                    $scope.coverNoteProposal.InsuranceSubClassName = results.data[0].InsuranceSubClassName;
                    $scope.coverNoteProposal.CoverNoteNo = results.data[0].CoverNoteNo;
                    $scope.coverNoteProposal.ConfirmationMethod = results.data[0].ConfirmationMethod;
                    $scope.coverNoteProposal.PendingDocItem = results.data[0].PendingDocItem;
                    $scope.coverNoteProposal.FromDate = results.data[0].FromDate;
                    $scope.coverNoteProposal.ToDate = results.data[0].ToDate;
                    $scope.coverNoteProposal.IssuedDate = results.data[0].IssuedDate;
                    $scope.quotationHeaderObj.QuotationHeaderID = quotationHeader.QuotationHeaderID;
                    $scope.quotationHeaderObj.ClientRequestHeaderID = quotationHeader.ClientRequestHeaderID;
                    $scope.quotationHeaderObj.QuotationStatusCodeDescription = quotationHeader.QuotationStatusCodeDescription;
                    // $scope.cusReqObj.QuotationHeaderID = quotationHeader.QuotationHeaderID
                    // $scope.loadClientRequestDetailsByID(results.data);
                    $("#proposalform").modal("show");
                }
                noty({
                    text: 'No Temparary Cover Note add for this Quatation',
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
                // alert("no covernote");
            }
        });
    }




    $scope.manageTemCoverNoteDetails = function (quotationHeader, itx) {
        var quatationLine = $scope.AddedInscompanies = [];
        var quatationLine = quotationHeader.QuotationLineDetails;
        for (var i = 0; i < quatationLine.length; i++) {
            var inslassID = quatationLine[i].InsuranceClassID;
            var insSubClassID = quatationLine[i].InsuranceSubClassID;
            var InsClass = quatationLine[i].InsuranceCode;
            var InsSubClass = quatationLine[i].InsuranceSubClassDescription;

            var RequestedInsuranceCompanyDetails = [];
            var RequestedInsuranceCompanyDetails = quatationLine[i].RequestedInsuranceCompanyDetails;
            for (var j = 0; j < RequestedInsuranceCompanyDetails.length; j++) {
                var InsuranceCompanyName = RequestedInsuranceCompanyDetails[j].InsuranceCompanyName;
                var InsuranceCompanyID = RequestedInsuranceCompanyDetails[j].InsuranceCompanyID;
                var QuotationDetailsInsCompanyHeaderDetails = [];
                var QuotationDetailsInsCompanyHeaderDetails = RequestedInsuranceCompanyDetails[j].QuotationDetailsInsCompanyHeaderDetails;

                // alert("f hjhjh" + angular.toJson(QuotationDetailsInsCompanyHeaderDetails));
                //

                $scope.AddedInscompanies.push({
                    "inslassID": inslassID,
                    "insSubClassID": insSubClassID,
                    "InsClass": InsClass,
                    "InsSubClass": InsSubClass,
                    "InsuranceCompanyName": InsuranceCompanyName,
                    "InsuranceCompanyID": InsuranceCompanyID,
                    "isChecked": true,
                    "SumInsured": "0.00"
                });

                for (var k = 0; k < QuotationDetailsInsCompanyHeaderDetails.length; k++) {


                    var QuotationDetailsInsCompanyLineDetails = [];
                    var QuotationDetailsInsCompanyLineDetails = QuotationDetailsInsCompanyHeaderDetails[k].QuotationDetailsInsCompanyLineDetails;
                    // alert(QuotationDetailsInsCompanyHeaderDetails[k].PremiumIncludingTax);
                    //  alert(QuotationDetailsInsCompanyHeaderDetails[k].ExcessDescription);
                    //  alert(QuotationDetailsInsCompanyHeaderDetails[k].ExcessAmount);
                    $scope.QuotationDetailsInsCompanyHeaderDetails.push({
                        "inslassID": inslassID,
                        "insSubClassID": insSubClassID,
                        "InsuranceCompanyID": InsuranceCompanyID,
                        "PremiumIncludingTax": QuotationDetailsInsCompanyHeaderDetails[k].PremiumIncludingTax,
                        "ExcessDescription": QuotationDetailsInsCompanyHeaderDetails[k].ExcessDescription,
                        "ExcessAmount": QuotationDetailsInsCompanyHeaderDetails[k].ExcessAmount,
                        "QuotationDetailsInsCompanyLineDetails": QuotationDetailsInsCompanyLineDetails

                    });









                    for (var l = 0; l < QuotationDetailsInsCompanyLineDetails.length; l++) {
                        $scope.QuotationDetailsInsCompanyLineID = QuotationDetailsInsCompanyLineDetails[l].QuotationDetailsInsCompanyLineID;
                        var QuotationDetailsInsCompanyScopeDetails = [];
                        var QuotationDetailsInsCompanyScopeDetails = QuotationDetailsInsCompanyLineDetails[l].QuotationDetailsInsCompanyScopeDetails;
                        var sumInsured = QuotationDetailsInsCompanyLineDetails[l].SumInsured;
                        var inslassID = QuotationDetailsInsCompanyLineDetails[l].InsuranceClassID;
                        var insSubClassID = QuotationDetailsInsCompanyLineDetails[l].InsuranceSubClassID;
                        var InsClass = QuotationDetailsInsCompanyLineDetails[l].InsuranceCode;
                        var InsSubClass = QuotationDetailsInsCompanyLineDetails[l].InsuranceSubClassDescription;
                        $scope.AddedInscompanies[i].SumInsured = sumInsured;
                        $scope.QuotationDetailsInsCompanyScopeDetails = QuotationDetailsInsCompanyScopeDetails;
                        for (var m = 0; m < QuotationDetailsInsCompanyScopeDetails.length; m++) {

                            // CheckIsExistAddedInscompaniesInsClassScope(insSubClassID, InsClass, QuotationDetailsInsCompanyScopeDetails[m]);

                        }


                    }
                }






            }





        }

        LoadCoverNotesById();
        $("#coverNote").modal("show");
        $scope.coverNote = {};


        // $scope.coverNote.IssuedDate = $filter('date')(new Date(), 'MM/dd/yyyy');
        // $scope.coverNoteNo = "TCN-" + $scope.quotationHeaderID;
        //  $scope.coverNote.QuotationHeaderID = quotationHeader.quotationHeaderID;
        $scope.coverNote.inscompany = $scope.AddedInscompanies[itx];
        $scope.coverNote.InsuranceSubClassID = $scope.AddedInscompanies[itx].insSubClassID;
        $scope.coverNote.RequestedDate = quotationHeader.RequestedDate;
        //  $scope.coverNote.CoverNoteNo = "TCN-" + $scope.HeaderObj.quotationHeaderID;
        $scope.coverNote.InsuranceCompanyName = $scope.AddedInscompanies[itx].InsuranceCompanyName;
        $scope.coverNote.CreatedBy = 1;
        // $scope.coverNote.ToDate = $scope.coverNote.IssuedDate;//$("#dp-4").val();
        // $scope.coverNote.FromDate = $scope.coverNote.IssuedDate;// $("#dp-3").val();
        //  alert($scope.coverNote.ToDate);

    };


    $scope.filePath = "http://localhost:39705/api/Quotation/SaveDocument";
    $scope.getDocumentIntoArray = function (e) {
        $scope.uploadedDocumentArray.push(e.files[0]);
    };

    $scope.saveDoc = function () {

        angular.forEach($scope.quotationLine.quotations, function (value, index) {

            var reqDate = "";

            try {
                reqDate = $scope.getFormattedDate(value.RequestedDate);
            } catch (e) {
                reqDate = value.RequestedDate;
            }

            var formData = new FormData();
            formData.append("headerId", $scope.quotationHeaderObj.QuotationHeaderID);
            formData.append("insSubClassID", value.InsuranceSubClassID);
            formData.append("insClassId", 1);
            formData.append("compId", value.CompId);
            formData.append("isRequired", value.IsRequest);
            formData.append("requestedDate", reqDate);// value.RequestedDate);//$scope.getFormattedDate(value.RequestedDate));
            formData.append("filePath", $scope.filePath);
            //formData.append("fileName", $scope.quotationLine.UserID);
            formData.append("modifiedBy", $scope.currentUser.UserID);
            formData.append("CreatedBy", $scope.currentUser.UserID);
            formData.append("uploadedDocument", $scope.uploadedDocumentArray[index]); //$scope.uploadedDocument);

            var objXhr = new XMLHttpRequest();

            objXhr.onreadystatechange = function () {
                $scope.showLoader = false;
                if (this.readyState == 4 && this.status == 200 && $scope.saveStatus == false) {
                    $scope.saveStatus = true;
                    noty({
                        text: 'Successfully Saved Document Details',
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
                    $scope.IsEmailDisable = false;
                    //$scope.ClearFields();
                    $scope.refreshContent();
                }
                if (this.readyState == null && this.status == null) {
                    noty({
                        text: 'Error Saving Document Details',
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
            };

            //objXhr.open("POST", "http://192.168.1.5:9810/api/Document/SaveDocument", true);
            objXhr.open("POST", $scope.filePath, true);
            objXhr.setRequestHeader("Authorization", 'Basic QWRtaW5JQk1TOmlibXNJcyMyMDE4');
            objXhr.send(formData);
        })
    };



    function CheckIsExistAddedInscompaniesInsClassScope(insSubClassID, InsClass, AddedInsClassesScopes) {
        //alert("AddedInsClassesScopes " + angular.toJson(AddedInsClassesScopes));
        //alert("f " + angular.toJson($scope.AddedInsClassesScopes));

        var found = $filter('filter')($scope.AddedInsClassesScopes,
            { "CommonInsScopeID": AddedInsClassesScopes.QuotationDetailsInsCompanyScopeID.toString() }, false);
        //alert("found.length " + found.length);

        if (found.length > 0) {
            for (var m = 0; m < found.length; m++) {
                var index = $scope.AddedInsClassesScopes.indexOf(found[m]);
                if (index >= 0) {

                    $scope.AddedInsClassesScopes.splice(index, 1);
                }
            }

            $scope.AddedInsClassesScopes.push({
                "ClientRequestInsSubClassScopeID": AddedInsClassesScopes.QuotationDetailsInsCompanyScopeID,
                "CommonInsScopeID": AddedInsClassesScopes.QuotationDetailsInsCompanyScopeID,
                "InsuranceSubClassID": insSubClassID,
                "InsuranceClassCode": InsClass,
                "Description": AddedInsClassesScopes.ScopeDescription,
                "isChecked": AddedInsClassesScopes.isChecked,
                "ExcessType": AddedInsClassesScopes.ExcessType,
                "ExcessAmount": AddedInsClassesScopes.ExcessAmount
            });

            // alert("AddedInsClassesScopes.ExcessAmount " + AddedInsClassesScopes.ExcessAmount);
            // alert("AddedInsClassesScopes.ExcessType " + AddedInsClassesScopes.ExcessType);

        }









    }

    function LoadCoverNotesById() {
         ReceivedQuotationService.getAllCoverNotes().then(function (results) {

            //alert(angular.toJson(results));
            $scope.coverNotes = results.data;
            //alert(angular.toJson(results));


        });
    }

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

    $scope.saveTemCoverNoteDetails = function () {

        $scope.coverNote.FromDate = $scope.getFormattedDate($scope.coverNote.FromDate);
        $scope.coverNote.ToDate = $scope.getFormattedDate($scope.coverNote.ToDate);
        $scope.coverNote.IssuedDate = new Date();
        $scope.coverNote.IssuedDate = $scope.getFormattedDate($scope.coverNote.IssuedDate);
        $scope.coverNote.QuotationHeaderID = $scope.quotationHeaderObj.QuotationHeaderID;
         ReceivedQuotationService.saveTCN($scope.coverNote, $scope.currentUser.UserID).then(function (results) {

            if (results.status === true) {
                noty({
                    text: 'Successfully Save Temporary Cover Note Details',
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
                    text: 'Error Save Temporary Cover Note Details',
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
            $scope.coverNote = [];
            //alert(angular.toJson(results));


        });


    }

    $scope.manageTemCoverNoteDetailss = function (tempcn) {
        $modal.open({
            templateUrl: 'ngTempTempCoverNotes',
            backdrop: 'static',
            //windowClass: 'app-modal-window-property',
            controller: [
                    '$scope', '$modalInstance', '$http', function ($scopeChild, $modalInstance, $http) {

                        $scopeChild.clientPropertyDetailsTemp = [];
                        $scopeChild.tempObj = {};

                        $scopeChild.tempObj = angular.copy(tempcn);

                        $scopeChild.updateCustomerDetails = function () {
                            $scope.quotationHeaderObj.QuotationLineDetails = [];
                            // $scope.isCustomerAvailable = true;



                            //$scopeChild.customerObj.DOB = $scope.getFormattedDate($scopeChild.customerObj.DOB);

                            $scope.quotationHeaderObj.InsuranceCompanyName = $scopeChild.tempObj.InsuranceCompanyName;
                            $scope.quotationHeaderObj.QuotationLineDetails[0].InsuranceSubClassDescription = $scopeChild.tempObj[0].InsuranceSubClassDescription;
                            $scope.quotationHeaderObj.RequestedDate = $scopeChild.tempObj.RequestedDate;
                            //
                            //$scope.loadQuotationDetailsByID = function () {
                            //   // $scope.showLoader = true;
                            //     ReceivedQuotationService.loadQuotationHeaderByID().then(function (results) {
                            //        $scope.showLoader = false;
                            //        if (results.status === true) {
                            //            $scope.loadClientRequestDetailsByID(results.data);
                            //        }
                            //    });
                            //};
                            $scopeChild.saveTemCoverNoteDetails = function () {
                                 ReceivedQuotationService.saveTemCoverNoteDetails($scopeChild.tempObj, $scope.getCurrentUser.UserID).then(function (results) {
                                    if (results.status === true) {
                                    }

                                });
                                $modalInstance.close();
                            };

                            $modalInstance.close();
                        };
                        //if ($scope.cusReqObj.ClientRequestLineDetails[idx].ClientPropertyDetails.length > 0) {
                        //    $scopeChild.clientPropertyDetailsTemp = angular.copy($scope.cusReqObj.ClientRequestLineDetails[idx].ClientPropertyDetails);
                        //}

                        $scopeChild.cancel = function () {
                            $modalInstance.dismiss('cancel');
                        };
                    }
            ],
        });
    };

    //$scope.saveTemCoverNoteDetails = function () {
    //     ReceivedQuotationService.saveTemCoverNoteDetails($scope.tempObj, $scope.getCurrentUser.UserID).then(function (results) {
    //        if (results.status === true) {
    //        }

    //    });
    //    $modalInstance.close();
    //};

    $scope.updateQuotation = function () {
        $scope.quotationLineTemp = [];
        $scope.quotationLineTempInsuranceCompanies = [];

        for (var i = 0; i < $scope.selectedInsCompanies.length; i++) {
            if (!$scope.isInsSubClassAvailable($scope.selectedInsCompanies[i].InsSubClassID, $scope.quotationLineTemp)) {
                //$scope.quotationLineTemp.push({ "InsuranceSubClassID": $scope.selectedInsCompanies[i].InsSubClassID, "InsuranceSubClassDescription": $scope.selectedInsCompanies[i].InsSubClassName, "RequestedInsuranceCompanyDetails": [] });
                $scope.quotationLineTemp.push({ "QuotationLineID": i, "InsSubClassID": $scope.selectedInsCompanies[i].InsSubClassID, "InsSubClassName": $scope.selectedInsCompanies[i].InsSubClassName, "RequestedInsuranceCompanyDetails": [] });
            }
        }



        if ($scope.quotationHeaderObj.Other === undefined)
            $scope.quotHeaderObj.Other = "";
        else
            $scope.quotHeaderObj.Other = $scope.quotationHeaderObj.Other;

        if ($scope.quotationHeaderObj.TransactionTypeID === undefined)
            $scope.quotHeaderObj.TransactionTypeID = "";
        else
            $scope.quotHeaderObj.TransactionTypeID = $scope.quotationHeaderObj.TransactionTypeID;


        for (var i = 0; i < $scope.quotationLineTemp.length; i++) {
            for (var j = 0; j < $scope.selectedInsCompanies.length; j++) {
                if ($scope.quotationLineTemp[i].InsuranceSubClassID === $scope.selectedInsCompanies[j].InsSubClassID) {
                    //            if ($scope.selectedInsCompanies[j].QuotationDetailsInsCompanyHeaderDetails[0].PremiumIncludingTax === undefined || $scope.selectedInsCompanies[j].QuotationDetailsInsCompanyHeaderDetails[0].PremiumIncludingTax === "" || $scope.selectedInsCompanies[j].QuotationDetailsInsCompanyHeaderDetails[0].PremiumIncludingTax === null) {
                    //                $scope.selectedInsCompanies[j].QuotationDetailsInsCompanyHeaderDetails[0].PremiumIncludingTax = "0";
                    //            }
                    //            if ($scope.selectedInsCompanies[j].QuotationDetailsInsCompanyHeaderDetails[0].ExcessAmount === undefined || $scope.selectedInsCompanies[j].QuotationDetailsInsCompanyHeaderDetails[0].ExcessAmount === "" || $scope.selectedInsCompanies[j].QuotationDetailsInsCompanyHeaderDetails[0].ExcessAmount === null) {
                    //                $scope.selectedInsCompanies[j].QuotationDetailsInsCompanyHeaderDetails[0].ExcessAmount = "0";
                    //            }
                    //$scope.quotationLineTemp[i].RequestedInsuranceCompanyDetails.push({ "InsuranceCompanyID": $scope.selectedInsCompanies[j].InsCompanyID, "InsuranceCompanyName": $scope.selectedInsCompanies[j].InsCompanyName, "Status": true, "QuotationDetailsInsCompanyHeaderDetails": $scope.selectedInsCompanies[j].QuotationDetailsInsCompanyHeaderDetails });
                    //$scope.RequestedInsuranceCompanyDetails[j].push({ "InsuranceCompanyID": $scope.selectedInsCompanies[j].InsCompanyID, "InsuranceCompanyName": $scope.selectedInsCompanies[j].InsCompanyName, "Status": true, "QuotationDetailsInsCompanyHeaderDetails": $scope.selectedInsCompanies[j].QuotationDetailsInsCompanyHeaderDetails });
                }
            }
        }

        $scope.quotHeaderObj.QuotLineDetails = $scope.quotationLineTemp;
        //$scope.quotHeaderObj.QuotationLineDetails = angular.copy($scope.quotationLineTemp);

        $scope.showLoader = true;
        noty({
            text: 'Do you want to Update Quotation Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();

                             ReceivedQuotationService.updateQuotation($scope.quotationHeaderObj, $scope.currentUser.UserID, $scope.selectedInsCompanies).then(function (results) {
                                $scope.showLoader = false;
                                //alert(angular.toJson(results));
                                if (results.status === true) {
                                    noty({
                                        text: 'Successfully Updated Quotation Details',
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
                                    $scope.activateQuotationListTab();
                                }
                                else {
                                    noty({
                                        text: 'Error Updating Quotation Details',
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

    $scope.cancelQuotation = function () {
        $scope.ClearFields();
        $scope.refreshContent();
        $scope.activateQuotationListTab();
    };

    $scope.manageQuotationStatus = function (currentQuotationStatus) {
        $modal.open({
            templateUrl: 'ngTemplateQS',
            backdrop: 'static',
            //windowClass: 'app-modal-window-scopes',
            controller: [
                    '$scope', '$modalInstance', '$http', function ($scopeChild, $modalInstance, $http) {

                        $scopeChild.quotationStatus = currentQuotationStatus;

                        $scopeChild.availableQuotationStatusList = [
                             { value: "QNC", text: "Quotation Not Created" },
                             { value: "QP", text: "Quotation Pending" },
                             { value: "QR", text: "Quotation Ready" },
                             { value: "NA", text: "Not Approved" },
                             { value: "CA", text: "Customer Approved" },
                             { value: "TCNI", text: "Temporary Cover Note Issued" }
                        ];


                        $scopeChild.updateQuotationStatus = function () {
                            $scopeChild.showLoader = true;
                            noty({
                                text: 'Do you want to Update Quotation Status?',
                                layout: 'topCenter',
                                buttons: [
                                        {
                                            addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                                                $noty.close();

                                                 ReceivedQuotationService.updateQuotationStatus($scope.quotationHeaderObj.QuotationHeaderID, $scopeChild.quotationStatus, $scope.currentUser.UserID).then(function (results) {
                                                    $scopeChild.showLoader = false;

                                                    if (results.status === true) {
                                                        if ($scopeChild.quotationStatus === "TCNI") {
                                                            noty({
                                                                text: 'Successfully Updated Quotation Status. Can Issue Temporary Cover Note ',
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
                                                            $scope.loadQuotationDetailsByID($scope.quotationHeaderObj.QuotationHeaderID);
                                                            $scope.refreshContent();
                                                            $modalInstance.close();
                                                        }
                                                        else {
                                                            noty({
                                                                text: 'Successfully Updated Quotation Status',
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

                                                            $scope.loadQuotationDetailsByID($scope.quotationHeaderObj.QuotationHeaderID);
                                                            $scope.refreshContent();
                                                            $modalInstance.close();
                                                        }
                                                    }
                                                    else {
                                                        noty({
                                                            text: 'Error Updating Quotation Status',
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
                                                    $scopeChild.showLoader = false;
                                                });
                                            }
                                        }
                                ]
                            })
                        };

                        $scopeChild.cancel = function () {
                            $modalInstance.dismiss('cancel');
                        };
                    }
            ],
        });
    };

    $scope.uploadeAttachment = "";

    $scope.getAttachment = function (e) {
        $scope.uploadedAttachment = e.files[0];
    };

    $scope.sendEmailRequest = function (index) {

        var quotObj = $scope.quotationLine.quotations[index];
        var subject = $scope.availableInsuranceClass[quotObj.InsuranceSubClassID].InsuranceClassCode; //



        $modal.open({
            templateUrl: 'ngTemplateSendEmail',
            backdrop: 'static',
            //windowClass: 'app-modal-window-scopes',
            controller: [
                    '$scope', '$modalInstance', '$http', function ($scopeChild, $modalInstance, $http) {
                        $scopeChild.emailObj = {};
                        $scopeChild.insuranceCompany = angular.copy(quotObj);

                        $scopeChild.emailObj.userName = $scopeChild.insuranceCompany.InsCompanyName;
                        $scopeChild.emailObj.emailAddress = $scopeChild.insuranceCompany.InsuranceCompanyEmail;
                        //$scopeChild.emailObj.emailHeader = "Quotation Details";
                        $scopeChild.emailObj.emailHeader = subject; //$scopeChild.insuranceCompany.InsSubClassName;
                        $scopeChild.emailObj.insSubClassId = quotObj.InsuranceSubClassID;
                        $scopeChild.emailObj.compId = quotObj.CompId;

                        /*------------------------------------TinyMCE Options-------------------------------------------*/
                        $scopeChild.tinymceOptions = {
                            theme: "modern",
                            plugins: [
                                "advlist autolink lists link image charmap print preview hr anchor pagebreak",
                                "searchreplace wordcount visualblocks visualchars code fullscreen",
                                "insertdatetime media nonbreaking save table contextmenu directionality",
                                "emoticons template paste textcolor"
                            ],
                            toolbar1: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image",
                            toolbar2: "print preview media | forecolor backcolor emoticons",
                            image_advtab: true,
                            height: "200px",
                            //width: "650px"
                        };

                        /*------------------------------------TinyMCE Options-------------------------------------------*/

                        $scopeChild.sendEmail = function () {
                            try {
                                $scopeChild.showLoader = true;
                                noty({
                                    text: 'Do you want to send email?',
                                    layout: 'topCenter',
                                    buttons: [
                                            {
                                                addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                                                    $noty.close();

                                                     ReceivedQuotationService.sendEmailRequest($scopeChild.emailObj).then(function (results) {
                                                        $scopeChild.showLoader = false;

                                                        if (results.status === true) {
                                                            noty({
                                                                text: 'Email sent successfully',
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

                                                            $modalInstance.close();
                                                        }
                                                        else {
                                                            noty({
                                                                text: 'Error occurred in email sending',
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
                                                        $scopeChild.showLoader = false;
                                                    });
                                                }
                                            }
                                    ]
                                })
                            } catch (e) {
                                console.log(e);
                            }
                        };


                        $scopeChild.cancel = function () {
                            $modalInstance.dismiss('cancel');
                        };
                    }
            ],
        });
    };



    $scope.addItem = function () {
        //$scope.paymentObj.DebitNoteList.push({PolicyInfoRecID: "", TotalNonCommissionPremium: 0, TotalGrossPremium: 0, PolicyInfoPaymentLists: [] });
        //$scope.quotationLines.push($scope.quotationLine);
        $scope.quotationLine.quotations.push({
            InsuranceSubClassID: 0,
            CompId: 0,
            IsRequest: false,
            RequestedDate: "",
            File: null


        })
    };

    $scope.deleteItem = function (deleteIndex) {
        $scope.quotationLine.quotations.splice(deleteIndex, 1);
        $scope.uploadedDocumentArray.splice(deleteIndex, 1);
    };

    $scope.getQuotationDetailByID = function (quotationHeaderId) {
         ReceivedQuotationService.getQuotationDetailByID(quotationHeaderId).then(function (results) {
            console.log(results);
            $scope.quotationLine.quotations = [];

            if (results.data.length > 0) {
                $scope.IsEmailDisable = false;
                angular.forEach(results.data, function (value, index) {

                    $scope.quotationLine.quotations.push({
                        InsuranceSubClassID: value.InsSubClassID,
                        CompId: value.CompID,
                        IsRequest: value.IsRequested,
                        RequestedDate: value.RequestedDate,
                        File: null


                    })
                });
            }
            else {
                $scope.addItem();
            }
        });
    };

    $scope.sendAttachmentObj = {}

    $scope.sendAttachment = function (selectIndex) {
        try {
            $scope.showLoader = true;
            $scope.sendAttachmentObj.InsuranceSubClassID = $scope.quotationLine.quotations[selectIndex].InsuranceSubClassID;
            $scope.sendAttachmentObj.companyId = $scope.quotationLine.quotations[selectIndex].CompId;


            noty({
                text: 'Do you want to send email?',
                layout: 'topCenter',
                buttons: [
                        {
                            addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                                $noty.close();

                                ReceivedQuotationService.sendAttachedQuotation($scope.sendAttachmentObj).then(function (results) {
                                    $scope.showLoader = false;

                                    if (results.status === true) {
                                        noty({
                                            text: 'Email sent successfully',
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

                                        $modalInstance.close();
                                    }
                                    else {
                                        noty({
                                            text: 'Error occurred in email sending',
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
                                    $scopeChild.showLoader = false;
                                });
                            }
                        }
                ]
            })

        } catch (e) {
            console.log(e);
        }
    };


    $scope.sendEmailClientRequest = function (insCompanyObj) {
        $modal.open({
            templateUrl: 'ngTemplateClientSendEmail',
            backdrop: 'static',
            //windowClass: 'app-modal-window-scopes',
            controller: [
                    '$scope', '$modalInstance', '$http', function ($scopeChild, $modalInstance, $http) {
                        $scopeChild.emailcliObj = {};
                        $scopeChild.insuranceCompany = angular.copy(insCompanyObj);

                        $scopeChild.emailcliObj.userName = $scopeChild.insuranceCompany.ClientName;
                        $scopeChild.emailcliObj.emailHeader = "Quotation Details";

                        /*------------------------------------TinyMCE Options-------------------------------------------*/
                        $scopeChild.tinymceOptions = {
                            theme: "modern",
                            plugins: [
                                "advlist autolink lists link image charmap print preview hr anchor pagebreak",
                                "searchreplace wordcount visualblocks visualchars code fullscreen",
                                "insertdatetime media nonbreaking save table contextmenu directionality",
                                "emoticons template paste textcolor"
                            ],
                            toolbar1: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image",
                            toolbar2: "print preview media | forecolor backcolor emoticons",
                            image_advtab: true,
                            height: "200px",
                            //width: "650px"
                        };

                        /*------------------------------------TinyMCE Options-------------------------------------------*/

                        $scopeChild.sendEmail = function () {
                            $scopeChild.showLoader = true;
                            noty({
                                text: 'Do you want to send email?',
                                layout: 'topCenter',
                                buttons: [
                                        {
                                            addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                                                $noty.close();

                                                 ReceivedQuotationService.sendEmailRequest($scope.emailObj).then(function (results) {
                                                    $scopeChild.showLoader = false;

                                                    if (results.status === true) {
                                                        noty({
                                                            text: 'Email sent successfully',
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

                                                        $modalInstance.close();
                                                    }
                                                    else {
                                                        noty({
                                                            text: 'Error occurred in email sending',
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
                                                    $scopeChild.showLoader = false;
                                                });
                                            }
                                        }
                                ]
                            })
                        };

                        $scopeChild.cancel = function () {
                            $modalInstance.dismiss('cancel');
                        };
                    }
            ],
        });
    };

    $scope.loadInsClass = function () {
        $scope.showLoader = true;
        ReceivedQuotationService.getAvailableInsuranceDropdown().then(function (results) {

            if (results.status === true) {
                $scope.availableInsClass = results.data;
            }
            else {
                $scope.availableInsClass = [];
            }


        });
    };
});

ibmsApp.filter('return_status', function ($sce) {
    return function (text, length, end) {
        if (text) {
            return $sce.trustAsHtml('<span><i style="color:green" class="glyphicon glyphicon-ok"></i></span>');
        }
        return $sce.trustAsHtml('<span><i style="color:red" class="glyphicon glyphicon-remove"></i></span>');
    }
});
