'use strict';

ibmsApp.controller("ClientRequestController", function ($scope, $http, $rootScope, ClientRequestService, PolicyInfoRecService, QuotationService, $location, AuthService, filterFilter, $modal) {

    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;

            if ($scope.currentUser.AccessLevelTypeName === "Admin") {
                //To Do (using a popup to select the desired business unit)
            }
            else {
                $scope.businessUnitID = $scope.currentUser.BusinessUnitID;
                $scope.loadClientRequestsByBUID($scope.businessUnitID);
                $scope.loadClientsByBUID($scope.businessUnitID);
                $scope.loadInsSubClassesByBUID($scope.businessUnitID);
            }
        });
    };

    $scope.init = function () {
        $scope.isViewMode = false;
        $scope.isClientReqAddMode = true;
        $scope.isCustomerAvailable = false;
        //$scope.isCustomerAdded = false;
        //$scope.isCustomerUpdated = false;

        $scope.businessUnitID = "";
        $scope.availableClients = [];
        $scope.availablePartners = [];
        $scope.availableAgents = [];
        $scope.availableIntroducers = [];
        $scope.availableloadExecutive = [];
        $scope.cusObj = {};
        $scope.cusReqObj = {};
        $scope.cusReqObj.ClientRequestLineDetails = [];
        $scope.availableInsSubClasses = [];
        $scope.availableTitle = [];
        $scope.quotationLine = {};
        $scope.quotationLine.quotations = []
        $scope.availableQuoteInfoInsCompanys = [];
        $scope.availableInsClass = [];
        $scope.availableInsuranceClass = [];

        $scope.loadInsuranceDetails();
        $scope.getCurrentUser();
        $scope.loadPartners();
        $scope.loadAgent();
        $scope.loadIntroducer();
        $scope.loadExecutive();
        $scope.addItem();
        $scope.loadTitles();
        $scope.getAllInsuranceSubClass();
        $scope.loadInsClass();
        $scope.receivedQuotationArray();
        $scope.saveStatus = false;
    };

    $scope.refreshContent = function () {
        $scope.loadClientRequestsByBUID($scope.businessUnitID);
        $scope.loadClientsByBUID($scope.businessUnitID);
        $scope.search_query = "";
    };

    $scope.ClearFields = function () {
        $scope.isViewMode = false;
        $scope.isClientReqAddMode = true;
        $scope.isCustomerAvailable = false;
        $scope.isCustomerAdded = false;
        $scope.isCustomerUpdated = false;

        $scope.cusObj = {};
        $scope.cusReqObj = {};
        $scope.cusReqObj.ClientRequestLineDetails = [];

        $scope.addItem();
    };

    $scope.loadClientRequestsByBUID = function (businessUnitID) {
        $scope.showLoader = true;
        ClientRequestService.getAllClientRequestsByBUID(businessUnitID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.availableClientRequests = results.data;
                $scope.data = angular.copy($scope.availableClientRequests);
                $scope.viewby = "10";
                $scope.totalItems = $scope.data.length;
                $scope.currentPage = 1;
                $scope.itemsPerPage = $scope.viewby;
                $scope.maxSize = 10; //Number of pager buttons to show

                $scope.setItemsPerPage($scope.viewby);
            }
            else {
                $scope.availableClientRequests = [];
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
        $scope.data = filterFilter($scope.availableClientRequests, searchText);
        $scope.viewby = "10";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 10;
        $scope.setItemsPerPage($scope.viewby);
    };

    $scope.loadClientsByBUID = function (businessUnitID) {
        //$scope.showLoader = true;
        ClientRequestService.getAllClientsByBUID(businessUnitID).then(function (results) {
            //$scope.showLoader = false;
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

    $scope.loadClientByID = function (clientID) {
        $scope.showLoader = true;
        ClientRequestService.getClientByID(clientID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.isCustomerAvailable = true;
                $scope.isCustomerAdded = false;
                $scope.isCustomerUpdated = false;

                $scope.cusObj = results.data;
                $scope.cusObj.HomeCountryID = results.data.HomeCountryID + "";
                $scope.cusObj.ResidentCountryID = results.data.ResidentCountryID + "";
            }
            else {
                $scope.isCustomerAvailable = false;
                $scope.isCustomerAdded = false;
                $scope.isCustomerUpdated = false;
                $scope.cusObj = {};
            }
        });
    };

    $scope.changeCustomer = function () {
        $scope.isCustomerAvailable = false;
        $scope.isCustomerAdded = false;
        $scope.isCustomerUpdated = false;

        $scope.cusObj = {};
        $scope.cusReqObj = {};
        $scope.cusReqObj.ClientRequestLineDetails = [];

        $scope.addItem();
    };

    $scope.loadPartners = function () {
        //$scope.showLoader = true;
        ClientRequestService.getAllPartners().then(function (results) {
            //$scope.showLoader = false;
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availablePartners.push({ value: results.data[i].PartnerID, text: results.data[i].PartnerName })
                }
            }
            else {
                $scope.availablePartners = [];
            }
        });
    };

    $scope.loadInsSubClassesByBUID = function (buid) {
        //$scope.showLoader = true;
        ClientRequestService.getAllInsSubClassesByBUID(buid).then(function (results) {
            //$scope.showLoader = false;
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableInsSubClasses.push({ value: results.data[i].InsuranceSubClassID, text: results.data[i].Description })
                }
            }
            else {
                $scope.availableInsSubClasses = [];
            }
        });
    };

    $scope.loadTitles = function () {

        ClientRequestService.loadTitle().then(function (results) {

            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableTitle.push({ value: results.data[i].TitleID, text: results.data[i].TitleName })
                }
            }
            else {
                $scope.availableTitle = [];
            }
        });
    };




    $scope.insSubClassChange = function (idx) {
        $scope.cusReqObj.ClientRequestLineDetails[idx].ClientRequestInsSubClassScopeDetails = [];
    };

    $scope.addItem = function () {
        $scope.cusReqObj.ClientRequestLineDetails.push({ ClientRequestLineID: 0, InsSubClassID: "", ClientPropertyDetails: [], ClientRequestInsSubClassScopeDetails: [] });
    };

    $scope.deleteItem = function (deleteIndex) {
        $scope.cusReqObj.ClientRequestLineDetails.splice(deleteIndex, 1);
    };




    //$scope.SaveCustomer = function () {
    //    $modal.open({
    //        templateUrl: 'ngTemplateCustomer',
    //        backdrop: 'static',
    //        //windowClass: 'app-modal-window-customer',
    //        controller: [
    //            '$scope', '$modalInstance', '$http', function ($scopeChild, $modalInstance, $http) {

    //                $scopeChild.mode = "Add";
    //                $scopeChild.isEditMode = false;
    //                $scopeChild.customerObj = {};

    //                    $scopeChild.mode = "Add";
    //                    $scopeChild.isEditMode = false;
    //                    $scopeChild.customerObj = {};

    //                    $scopeChild.saveCustomerDetails = function () {

    //                        $scope.isCustomerAvailable = true;
    //                        $scope.isCustomerAdded = true;
    //                        $scope.isCustomerUpdated = false;

    //                        $scope.cusObj.ClientName = $scopeChild.customerObj.ClientName;
    //                        $scope.cusObj.ClientAddress = $scopeChild.customerObj.ClientAddress;
    //                        $scope.cusObj.NIC = $scopeChild.customerObj.NIC;
    //                        $scope.cusObj.ContactNo = $scopeChild.customerObj.ContactNo;
    //                        $scope.cusObj.FixedLine = $scopeChild.customerObj.FixedLine;
    //                        $scope.cusObj.Email = $scopeChild.customerObj.Email;
    //                        if ($scopeChild.customerObj.DOB === undefined || $scopeChild.customerObj.DOB === "" || $scopeChild.customerObj.DOB === null) {
    //                            $scope.cusObj.DOB = null;

    //                        } else {
    //                            $scope.cusObj.DOB = $scope.getFormattedDate($scopeChild.customerObj.DOB);
    //                        }

    //                        $scope.cusObj.PPID = $scopeChild.customerObj.PPID;
    //                        $scope.cusObj.FamilyDiscount = $scopeChild.customerObj.FamilyDiscount;
    //                        $scope.cusObj.AdditionalNote = $scopeChild.customerObj.AdditionalNote;
    //                        $scope.cusObj.HomeCountryID = $scopeChild.customerObj.HomeCountryID;
    //                        $scope.cusObj.ResidentCountryID = $scopeChild.customerObj.ResidentCountryID;

    //                        if ($scope.cusObj.FamilyDiscount === undefined || $scope.cusObj.FamilyDiscount === "" || $scope.cusObj.FamilyDiscount === null) {
    //                            $scope.cusObj.FamilyDiscount = 0;
    //                        }
    //                        if ($scope.cusObj.HomeCountryID === undefined || $scope.cusObj.HomeCountryID === "" || $scope.cusObj.HomeCountryID === null) {
    //                            $scope.cusObj.HomeCountryID = 1;
    //                        }
    //                        if ($scope.cusObj.ResidentCountryID === undefined || $scope.cusObj.ResidentCountryID === "" || $scope.cusObj.ResidentCountryID === null) {
    //                            $scope.cusObj.ResidentCountryID = 1;
    //                        }

    //                        $scope.cusObj.BusinessUnitID = $scope.businessUnitID;
    //                        $scope.cusReqObj.RequestedDate = $scope.getFormattedDate($scope.cusReqObj.RequestedDate);

    //                        ClientRequestService.saveClient($scope.isCustomerUpdated, $scope.isCustomerAdded, $scope.cusObj, $scope.currentUser.UserID).then(function (results) {
    //                            $scope.showLoader = false;
    //                            //alert(angular.toJson(results));
    //                            if (results.status === true) {
    //                                noty({
    //                                    text: 'Successfully Saved Customer Request Details',
    //                                    layout: 'topCenter',
    //                                    type: 'success',
    //                                    buttons: [
    //                                        {
    //                                            addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
    //                                                $noty.close();
    //                                            }
    //                                        }
    //                                    ]
    //                                });
    //                                $scope.ClearFields();
    //                                $scope.refreshContent();
    //                                $scope.activateClientRequestListTab();
    //                            }
    //                            else {
    //                                noty({
    //                                    text: 'Error Saving Customer Request Details',
    //                                    layout: 'topCenter',
    //                                    type: 'error',
    //                                    buttons: [
    //                                        {
    //                                            addClass: 'btn btn-danger btn-clean', text: 'Ok', onClick: function ($noty) {
    //                                                $noty.close();
    //                                            }
    //                                        }
    //                                    ]
    //                                });
    //                            }
    //                        });


    //                        $modalInstance.close();

    //                    };

    //                $scopeChild.cancel = function () {
    //                    if (isEdit) {
    //                        $scopeChild.customerObj = angular.copy(customer);
    //                    }

    //                    $modalInstance.dismiss('cancel');
    //                };
    //            }
    //        ],
    //    });
    //};





    $scope.manageCustomer = function (isEdit, customer) {
        $modal.open({
            templateUrl: 'ngTemplateCustomer',
            backdrop: 'static',
            //windowClass: 'app-modal-window-customer',
            controller: [
                '$scope', '$modalInstance', '$http', function ($scopeChild, $modalInstance, $http) {

                    $scopeChild.mode = "Add";
                    $scopeChild.isEditMode = false;
                    $scopeChild.customerObj = {};
                    $scopeChild.availableHomeCountries = [];
                    $scopeChild.availableResidentCountries = [];
                    $scopeChild.availableinputDistrict = [];
                    $scopeChild.availableTitle = [];


                    $scopeChild.loadTitles = function () {

                        ClientRequestService.loadTitle().then(function (results) {

                            if (results.status === true) {
                                for (var i = 0; i < results.data.length; i++) {
                                    $scopeChild.availableTitle.push({ value: results.data[i].TitleID, text: results.data[i].TitleName })
                                }
                            }
                            else {
                                $scopeChild.availableTitle = [];
                            }
                        });
                    };




                    $scopeChild.homeCountries = function () {
                        ClientRequestService.getAllCountries().then(function (results) {
                            if (results.status === true) {
                                for (var i = 0; i < results.data.length; i++) {
                                    $scopeChild.availableHomeCountries.push({ value: results.data[i].CountryID, text: results.data[i].CountryName })
                                }
                            }
                            else {
                                $scopeChild.availableHomeCountries = [];
                            }
                        });
                    };

                    $scopeChild.residentCountries = function () {
                        ClientRequestService.getAllCountries().then(function (results) {
                            if (results.status === true) {
                                for (var i = 0; i < results.data.length; i++) {
                                    $scopeChild.availableResidentCountries.push({ value: results.data[i].CountryID, text: results.data[i].CountryName })
                                }
                            }
                            else {
                                $scopeChild.availableResidentCountries = [];
                            }
                        });
                    };

                    $scopeChild.Districts = function () {
                        ClientRequestService.getAllDistrict().then(function (results) {
                            if (results.status === true) {
                                for (var i = 0; i < results.data.length; i++) {
                                    $scopeChild.availableinputDistrict.push({ value: results.data[i].DistrictId, text: results.data[i].Description })
                                }
                            }
                            else {
                                $scopeChild.availableinputDistrict = [];
                            }
                        });
                    };

                    $scopeChild.loadTitles();
                    $scopeChild.homeCountries();
                    $scopeChild.residentCountries();
                    $scopeChild.Districts();

                    if (isEdit) {
                        ;
                        $scopeChild.mode = "Edit";
                        $scopeChild.isEditMode = true;
                        $scopeChild.customerObj = angular.copy(customer);

                        $scopeChild.updateCustomerDetails = function () {
                            $scope.isCustomerAvailable = true;

                            if ($scopeChild.customerObj.ClientID !== undefined) {
                                $scope.isCustomerAdded = false;
                                $scope.isCustomerUpdated = true;
                                $scope.cusObj.ClientID = $scopeChild.customerObj.ClientID;
                            }
                            else {
                                $scope.isCustomerAdded = true;
                                $scope.isCustomerUpdated = false;
                            }

                            //$scopeChild.customerObj.DOB = $scope.getFormattedDate($scopeChild.customerObj.DOB);

                            //
                            //$scope.cusObj.ClientName = $scopeChild.customerObj.ClientName;
                            //$scope.cusObj.ClientAddress = $scopeChild.customerObj.ClientAddress;
                            //$scope.cusObj.NIC = $scopeChild.customerObj.NIC;
                            //$scope.cusObj.ContactNo = $scopeChild.customerObj.ContactNo;
                            //$scope.cusObj.FixedLine = $scopeChild.customerObj.FixedLine;
                            //$scope.cusObj.Email = $scopeChild.customerObj.Email;
                            //if ($scopeChild.customerObj.DOB === undefined || $scopeChild.customerObj.DOB === "" || $scopeChild.customerObj.DOB === null) {
                            //    $scope.cusObj.DOB = null;

                            //} else {
                            //    $scope.cusObj.DOB = $scope.getFormattedDate($scopeChild.customerObj.DOB);
                            //}
                            //$scope.cusObj.PPID = $scopeChild.customerObj.PPID;
                            //$scope.cusObj.FamilyDiscount = $scopeChild.customerObj.FamilyDiscount;
                            //$scope.cusObj.AdditionalNote = $scopeChild.customerObj.AdditionalNote;
                            //$scope.cusObj.HomeCountryID = $scopeChild.customerObj.HomeCountryID;
                            //$scope.cusObj.ResidentCountryID = $scopeChild.customerObj.ResidentCountryID;
                            //$scope.cusObj.DistrictId = $scopeChild.customerObj.DistrictId;
                            //$scope.cusObj.Other = $scopeChild.customerObj.Other;




                            if ($scopeChild.customerObj.NIC === undefined || $scopeChild.customerObj.NIC === "" || $scopeChild.customerObj.NIC === null)
                                $scope.cusObj.NIC = "";
                            else
                                $scope.cusObj.NIC = $scopeChild.customerObj.NIC;

                            if ($scopeChild.customerObj.ContactNo === undefined || $scopeChild.customerObj.ContactNo === "" || $scopeChild.customerObj.ContactNo === null)
                                $scope.cusObj.ContactNo = "";
                            else
                                $scope.cusObj.ContactNo = $scopeChild.customerObj.ContactNo;

                            if ($scopeChild.customerObj.FixedLine === undefined || $scopeChild.customerObj.FixedLine === "" || $scopeChild.customerObj.FixedLine === null)
                                $scope.cusObj.FixedLine = "";
                            else
                                $scope.cusObj.FixedLine = $scopeChild.customerObj.FixedLine;

                            if ($scopeChild.customerObj.Email === undefined || $scopeChild.customerObj.Email === "" || $scopeChild.customerObj.Email === null)
                                $scope.cusObj.Email = "";
                            else
                                $scope.cusObj.Email = $scopeChild.customerObj.Email;

                            if ($scopeChild.customerObj.DOB === undefined || $scopeChild.customerObj.DOB === "" || $scopeChild.customerObj.DOB === null) {
                                $scope.cusObj.DOB = "";

                            } else {
                                $scope.cusObj.DOB = $scope.getFormattedDate($scopeChild.customerObj.DOB);
                            }

                            if ($scopeChild.customerObj.PPID === undefined || $scopeChild.customerObj.PPID === "" || $scopeChild.customerObj.PPID === null) {
                                $scope.cusObj.PPID = "";

                            } else {
                                $scope.cusObj.PPID = $scopeChild.customerObj.PPID;
                            }

                            if ($scopeChild.customerObj.FamilyDiscount === undefined || $scopeChild.customerObj.FamilyDiscount === "" || $scopeChild.customerObj.FamilyDiscount === null) {
                                $scope.cusObj.FamilyDiscount = 0;

                            } else {
                                $scope.cusObj.FamilyDiscount = $scopeChild.customerObj.FamilyDiscount;
                            }

                            if ($scopeChild.customerObj.AdditionalNote === undefined || $scopeChild.customerObj.AdditionalNote === "" || $scopeChild.customerObj.AdditionalNote === null) {
                                $scope.cusObj.AdditionalNote = "";

                            } else {
                                $scope.cusObj.AdditionalNote = $scopeChild.customerObj.AdditionalNote;
                            }

                            if ($scopeChild.customerObj.HomeCountryID === undefined || $scopeChild.customerObj.HomeCountryID === "" || $scopeChild.customerObj.HomeCountryID === null) {
                                $scope.cusObj.HomeCountryID = 1;

                            } else {
                                $scope.cusObj.HomeCountryID = $scopeChild.customerObj.HomeCountryID;
                            }

                            if ($scopeChild.customerObj.ResidentCountryID === undefined || $scopeChild.customerObj.ResidentCountryID === "" || $scopeChild.customerObj.ResidentCountryID === null) {
                                $scope.cusObj.ResidentCountryID = 1;

                            } else {
                                $scope.cusObj.ResidentCountryID = $scopeChild.customerObj.ResidentCountryID;
                            }

                            if ($scopeChild.customerObj.Other === undefined || $scopeChild.customerObj.Other === "" || $scopeChild.customerObj.Other === null) {
                                $scope.cusObj.Other = "";

                            } else {
                                $scope.cusObj.Other = $scopeChild.customerObj.Other;
                                ;
                            }

                            if ($scopeChild.customerObj.DistrictId === undefined || $scopeChild.customerObj.DistrictId === "" || $scopeChild.customerObj.DistrictId === null) {
                                $scope.cusObj.DistrictId = 0;

                            } else {
                                $scope.cusObj.DistrictId = $scopeChild.customerObj.DistrictId;

                            }

                            if ($scopeChild.customerObj.CustomerType === undefined || $scopeChild.customerObj.CustomerType === "" || $scopeChild.customerObj.CustomerType === null) {
                                $scope.cusObj.CustomerType = 0;


                            }
                            else {
                                if ($scope.cusObj.CustomerType = "Individual")
                                    $scope.cusObj.CustomerType = 1;
                                else
                                    $scope.cusObj.CustomerType = 2;
                            }

                            if ($scopeChild.customerObj.TitleID === undefined || $scopeChild.customerObj.TitleID === "" || $scopeChild.customerObj.TitleID === null) {
                                $scope.cusObj.TitleID = 0;
                            }
                            else
                                $scope.cusObj.TitleID = $scopeChild.customerObj.TitleID;

                            $scope.cusObj.BusinessUnitID = $scope.currentUser.BusinessUnitID;
                            $scope.cusObj.isCustomerAdded = $scope.isCustomerAdded;
                            $scope.cusObj.isCustomerUpdated = $scope.isCustomerUpdated;


                            ClientRequestService.saveClient($scope.isCustomerUpdated, $scope.isCustomerAdded, $scope.cusObj, $scope.currentUser.UserID).then(function (results) {
                                $scope.showLoader = false;
                                //alert(angular.toJson(results));
                                if (results.status === true) {
                                    noty({
                                        text: 'Successfully Updated Customer Details',
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
                                    //$scope.activateClientRequestListTab();
                                    $modalInstance.close();
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
                                    $modalInstance.close();
                                }
                            });








                        };
                    }
                    else {
                        $scopeChild.mode = "Add";
                        $scopeChild.isEditMode = false;
                        $scopeChild.customerObj = {};

                        $scopeChild.saveCustomerDetails = function () {

                            $scope.isCustomerAvailable = true;
                            $scope.isCustomerAdded = true;
                            $scope.isCustomerUpdated = false;

                            $scope.cusObj.ClientName = $scopeChild.customerObj.ClientName;
                            $scope.cusObj.ClientAddress = $scopeChild.customerObj.ClientAddress;

                            if ($scopeChild.customerObj.NIC === undefined || $scopeChild.customerObj.NIC === "" || $scopeChild.customerObj.NIC === null)
                                $scope.cusObj.NIC = "";
                            else
                                $scope.cusObj.NIC = $scopeChild.customerObj.NIC;

                            if ($scopeChild.customerObj.ContactNo === undefined || $scopeChild.customerObj.ContactNo === "" || $scopeChild.customerObj.ContactNo === null)
                                $scope.cusObj.ContactNo = "";
                            else
                                $scope.cusObj.ContactNo = $scopeChild.customerObj.ContactNo;

                            if ($scopeChild.customerObj.FixedLine === undefined || $scopeChild.customerObj.FixedLine === "" || $scopeChild.customerObj.FixedLine === null)
                                $scope.cusObj.FixedLine = "";
                            else
                                $scope.cusObj.FixedLine = $scopeChild.customerObj.FixedLine;

                            if ($scopeChild.customerObj.Email === undefined || $scopeChild.customerObj.Email === "" || $scopeChild.customerObj.Email === null)
                                $scope.cusObj.Email = "";
                            else
                                $scope.cusObj.Email = $scopeChild.customerObj.Email;

                            if ($scopeChild.customerObj.DOB === undefined || $scopeChild.customerObj.DOB === "" || $scopeChild.customerObj.DOB === null) {
                                $scope.cusObj.DOB = "";

                            } else {
                                $scope.cusObj.DOB = $scope.getFormattedDate($scopeChild.customerObj.DOB);
                            }

                            if ($scopeChild.customerObj.PPID === undefined || $scopeChild.customerObj.PPID === "" || $scopeChild.customerObj.PPID === null) {
                                $scope.cusObj.PPID = "";

                            } else {
                                $scope.cusObj.PPID = $scopeChild.customerObj.PPID;
                            }

                            if ($scopeChild.customerObj.FamilyDiscount === undefined || $scopeChild.customerObj.FamilyDiscount === "" || $scopeChild.customerObj.FamilyDiscount === null) {
                                $scope.cusObj.FamilyDiscount = 0;

                            } else {
                                $scope.cusObj.FamilyDiscount = $scopeChild.customerObj.FamilyDiscount;
                            }

                            if ($scopeChild.customerObj.AdditionalNote === undefined || $scopeChild.customerObj.AdditionalNote === "" || $scopeChild.customerObj.AdditionalNote === null) {
                                $scope.cusObj.AdditionalNote = "";

                            } else {
                                $scope.cusObj.AdditionalNote = $scopeChild.customerObj.AdditionalNote;
                            }

                            if ($scopeChild.customerObj.HomeCountryID === undefined || $scopeChild.customerObj.HomeCountryID === "" || $scopeChild.customerObj.HomeCountryID === null) {
                                $scope.cusObj.HomeCountryID = 1;

                            } else {
                                $scope.cusObj.HomeCountryID = $scopeChild.customerObj.HomeCountryID;
                            }

                            if ($scopeChild.customerObj.ResidentCountryID === undefined || $scopeChild.customerObj.ResidentCountryID === "" || $scopeChild.customerObj.ResidentCountryID === null) {
                                $scope.cusObj.ResidentCountryID = 1;

                            } else {
                                $scope.cusObj.ResidentCountryID = $scopeChild.customerObj.ResidentCountryID;
                            }

                            if ($scopeChild.customerObj.Other === undefined || $scopeChild.customerObj.Other === "" || $scopeChild.customerObj.Other === null) {
                                $scope.cusObj.Other = "";

                            } else {
                                $scope.cusObj.Other = $scopeChild.customerObj.Other;
                                ;
                            }

                            if ($scopeChild.customerObj.DistrictId === undefined || $scopeChild.customerObj.DistrictId === "" || $scopeChild.customerObj.DistrictId === null) {
                                $scope.cusObj.DistrictId = 0;

                            } else {
                                $scope.cusObj.DistrictId = $scopeChild.customerObj.DistrictId;

                            }

                            if ($scopeChild.customerObj.TitleID === undefined || $scopeChild.customerObj.TitleID === "" || $scopeChild.customerObj.TitleID === null) {
                                $scope.cusObj.TitleID = 0;
                            }
                            else
                                $scope.cusObj.TitleID = $scopeChild.customerObj.TitleID;

                            $scope.cusObj.BusinessUnitID = $scope.currentUser.BusinessUnitID;;


                            ClientRequestService.saveClient($scope.isCustomerUpdated, $scope.isCustomerAdded, $scope.cusObj, $scope.currentUser.UserID).then(function (results) {
                                $scope.showLoader = false;
                                //alert(angular.toJson(results));
                                if (results.status === true) {
                                    noty({
                                        text: 'Successfully Saved Customer Details',
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
                                    //$scope.activateClientRequestListTab();
                                    $modalInstance.close();
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
                                    $modalInstance.close();
                                }
                            });



                        };
                    }

                    $scopeChild.cancel = function () {
                        if (isEdit) {
                            $scopeChild.customerObj = angular.copy(customer);
                        }

                        $modalInstance.dismiss('cancel');
                    };
                }
            ],
        });
    };

    $scope.manageProperties = function (idx) {
        $modal.open({
            templateUrl: 'ngTemplateProperty',
            backdrop: 'static',
            windowClass: 'app-modal-window-property',
            controller: [
                '$scope', '$modalInstance', '$http', function ($scopeChild, $modalInstance, $http) {

                    $scopeChild.clientPropertyDetailsTemp = [];

                    $scopeChild.addProperty = function () {
                        $scopeChild.clientPropertyDetailsTemp.push({ ClientPropertyName: "", BRNo: "", VATNo: "" });
                    };

                    $scopeChild.deleteProperty = function (deleteIndex) {
                        $scopeChild.clientPropertyDetailsTemp.splice(deleteIndex, 1);
                    };

                    if ($scope.cusReqObj.ClientRequestLineDetails[idx].ClientPropertyDetails.length > 0) {
                        $scopeChild.clientPropertyDetailsTemp = angular.copy($scope.cusReqObj.ClientRequestLineDetails[idx].ClientPropertyDetails);
                    }
                    else {
                        $scopeChild.addProperty();
                    }

                    $scopeChild.savePropertyDetails = function () {
                        $scope.cusReqObj.ClientRequestLineDetails[idx].ClientPropertyDetails = $scopeChild.clientPropertyDetailsTemp;
                        $modalInstance.close();
                    };

                    $scopeChild.cancel = function () {
                        $modalInstance.dismiss('cancel');
                    };
                }
            ],
        });
    };

    $scope.manageScopes = function (idx, insSubClassID) {
        $modal.open({
            templateUrl: 'ngTemplateScopes',
            backdrop: 'static',
            //windowClass: 'app-modal-window-scopes',
            controller: [
                '$scope', '$modalInstance', '$http', function ($scopeChild, $modalInstance, $http) {

                    $scopeChild.insSubClassScopeDetailsTemp = [];
                    $scopeChild.availableInsSubClassScopes = [];

                    $scopeChild.loadInsSubClassScopes = function (insSubClassID) {
                        $scopeChild.showLoader = true;
                        ClientRequestService.getAllInsSubClassScope(insSubClassID).then(function (results) {
                            $scopeChild.showLoader = false;
                            if (results.status === true) {
                                for (var i = 0; i < results.data.length; i++) {
                                    $scopeChild.availableInsSubClassScopes.push({ value: results.data[i].CommonInsuranceScopeID, text: results.data[i].Description })
                                }
                            }
                            else {
                                $scopeChild.availableInsSubClassScopes = [];
                            }
                        });
                    };

                    $scopeChild.loadInsSubClassScopes(insSubClassID);

                    $scopeChild.addScope = function () {
                        $scopeChild.insSubClassScopeDetailsTemp.push({ CommonInsScopeID: "" });
                    };

                    $scopeChild.deleteScope = function (deleteIndex) {
                        $scopeChild.insSubClassScopeDetailsTemp.splice(deleteIndex, 1);
                    };

                    if ($scope.cusReqObj.ClientRequestLineDetails[idx].ClientRequestInsSubClassScopeDetails.length > 0) {
                        for (var i = 0; i < $scope.cusReqObj.ClientRequestLineDetails[idx].ClientRequestInsSubClassScopeDetails.length; i++) {
                            $scope.cusReqObj.ClientRequestLineDetails[idx].ClientRequestInsSubClassScopeDetails[i].CommonInsScopeID = $scope.cusReqObj.ClientRequestLineDetails[idx].ClientRequestInsSubClassScopeDetails[i].CommonInsScopeID + "";
                        }

                        $scopeChild.insSubClassScopeDetailsTemp = angular.copy(($scope.cusReqObj.ClientRequestLineDetails[idx].ClientRequestInsSubClassScopeDetails));
                    }
                    else {
                        $scopeChild.addScope();
                    }

                    $scopeChild.saveScopeDetails = function () {
                        $scope.cusReqObj.ClientRequestLineDetails[idx].ClientRequestInsSubClassScopeDetails = $scopeChild.insSubClassScopeDetailsTemp;
                        $modalInstance.close();
                    };

                    $scopeChild.cancel = function () {
                        $modalInstance.dismiss('cancel');
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

    $scope.saveCustomerRequest = function () {
        $scope.showLoader = true;
        noty({
            text: 'Do you want to Save Customer Request Details?',
            layout: 'topCenter',
            buttons: [
                {
                    addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                        $noty.close();



                        $scope.cusObj.BusinessUnitID = $scope.currentUser.BusinessUnitID;;



                        if ($scope.cusReqObj.PartnerID == 1) {
                            $scope.cusReqObj.EmployeeID = 0;
                            //$scope.cusReqObj.AgentID = 0;
                            $scope.cusReqObj.IntroducerID = 0;
                            $scope.cusReqObj.AccountHandlerID = 0;
                        }


                        else if ($scope.cusReqObj.PartnerID == 2) {
                            //$scope.cusReqObj.EmployeeID = 0;
                            $scope.cusReqObj.AgentID = 0;
                            $scope.cusReqObj.IntroducerID = 0;
                            $scope.cusReqObj.AccountHandlerID = 0;
                        }


                        else if ($scope.cusReqObj.PartnerID == 3) {
                            $scope.cusReqObj.EmployeeID = 0;
                            //$scope.cusReqObj.AgentID = 0;
                            $scope.cusReqObj.IntroducerID = 0;
                            $scope.cusReqObj.AccountHandlerID = 0;
                        }


                        else if ($scope.cusReqObj.PartnerID == 4) {
                            $scope.cusReqObj.EmployeeID = 0;
                            $scope.cusReqObj.AgentID = 0;
                            $scope.cusReqObj.IntroducerID = 0;
                            //$scope.cusReqObj.AccountHandlerID = 0;
                        }
                        else {
                            $scope.cusReqObj.EmployeeID = 0;
                            $scope.cusReqObj.AgentID = 0;
                            $scope.cusReqObj.IntroducerID = 0;
                            $scope.cusReqObj.AccountHandlerID = 0;
                        }


                        //$scope.cusReqObj.ClientRequestLineDetails = [];
                        $scope.cusObj.BusinessUnitID = $scope.businessUnitID;
                        $scope.cusReqObj.RequestedDate = $scope.getFormattedDate($scope.cusReqObj.RequestedDate);
                        $scope.cusReqObj.ClientRequestLineDetails = [];

                        ClientRequestService.saveClientRequest($scope.isCustomerUpdated, $scope.isCustomerAdded, $scope.cusObj, $scope.cusReqObj, $scope.currentUser.UserID).then(function (results) {
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

    $scope.activateClientRequestListTab = function () {
        $("#list-tab-1").addClass('active');
        $("#tab-1").addClass('tab-pane active');

        $("#list-tab-2").removeClass('active');
        $("#tab-2").removeClass('tab-pane active');

        $("#list-tab-3").removeClass('active');
        $("#tab-3").removeClass('tab-pane active');


        $("#tab-1").css("display", "block");
        $("#tab-2").css("display", "none");
        $("#tab-3").css("display", "none");
    };

    $scope.activateNewClientRequestTab = function () {
        $("#list-tab-1").removeClass('active');
        $("#tab-1").removeClass('tab-pane active');

        $("#list-tab-3").removeClass('active');
        $("#tab-3").removeClass('tab-pane active');

        $("#list-tab-2").addClass('active');
        $("#tab-2").addClass('tab-pane active');

        $("#tab-1").css("display", "none");
        $("#tab-2").css("display", "block");
        $("#tab-3").css("display", "none");
    };

    $scope.activateQuotationTab = function () {
        $("#list-tab-1").removeClass('active');
        $("#tab-1").removeClass('tab-pane active');

        $("#list-tab-2").removeClass('active');
        $("#tab-2").removeClass('tab-pane active');

        $("#list-tab-3").addClass('active');
        $("#tab-3").addClass('tab-pane active');

        $("#tab-1").css("display", "none");
        $("#tab-2").css("display", "none");
        $("#tab-3").css("display", "block");
    };

    $scope.loadClientRequestDetailsByID = function (quotationObj, isView) {
        $scope.ClearFields();
        $scope.getQuotationDetailByID(quotationObj.QuotationNo);
        $scope.isQuotationEditMode = true;

        if (isView === 'true') {
            $scope.isQuotationViewMode = true;
        }
        else {
            $scope.isQuotationViewMode = false;
        }

        $scope.activateQuotationTab();
        $scope.quotationHeaderObj = quotationObj;

        //for (var i = 0; i < $scope.quotationHeaderObj.QuotationLineDetails.length; i++) {
        //    for (var j = 0; j < $scope.quotationHeaderObj.QuotationLineDetails[i].RequestedInsuranceCompanyDetails.length; j++) {
        //        $scope.selectedInsCompanies.push({ "InsSubClassID": $scope.quotationHeaderObj.QuotationLineDetails[i].InsuranceSubClassID, "InsSubClassName": $scope.quotationHeaderObj.QuotationLineDetails[i].InsuranceSubClassDescription, "InsCompanyID": $scope.quotationHeaderObj.QuotationLineDetails[i].RequestedInsuranceCompanyDetails[j].InsuranceCompanyID, "InsCompanyName": $scope.quotationHeaderObj.QuotationLineDetails[i].RequestedInsuranceCompanyDetails[j].InsuranceCompanyName, "InsCompanyEmail": $scope.quotationHeaderObj.QuotationLineDetails[i].RequestedInsuranceCompanyDetails[j].InsuranceCompanyEmail, "QuotationDetailsInsCompanyHeaderDetails": $scope.quotationHeaderObj.QuotationLineDetails[i].RequestedInsuranceCompanyDetails[j].QuotationDetailsInsCompanyHeaderDetails });
        //    }
        //}

        $scope.showLoader = true;
        ClientRequestService.getClientRequestByID($scope.quotationHeaderObj.ClientRequestHeaderID).then(function (results) {
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

                ClientRequestService.getClientByID($scope.quotationHeaderObj.ClientID).then(function (results) {
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

    $scope.getQuotationDetailByID = function (quotationHeaderId) {
        ClientRequestService.getQuotationDetailByID(quotationHeaderId).then(function (results) {
            console.log(results);
            $scope.quotationLine.quotations = [];

            if (results.data.length > 0) {
                $scope.IsEmailDisable = false;
                angular.forEach(results.data, function (value, index) {

                    $scope.quotationLine.quotations.push({
                        InsuranceClassID: value.InsClassID,
                        InsuranceSubClassID: value.InsSubClassID,
                        CompId: value.CompID,
                        IsRequest: value.IsReceived,
                        RequestedDate: value.ReceivedDate,
                        File: null


                    })
                });
            }
            else {
                $scope.addItem();
            }
        });
    };

    $scope.editClientRequest = function (clientReqHeaderID, clientID) {
        $scope.activateNewClientRequestTab();

        $scope.showLoader = true;
        ClientRequestService.getClientRequestByID(clientReqHeaderID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.isViewMode = false;
                $scope.isClientReqAddMode = false;
                $scope.cusReqObj = results.data;
                $scope.cusReqObj.PartnerID = $scope.cusReqObj.PartnerID + "";
                $scope.loadClientByID(clientID);
            }
            else {
                $scope.cusReqObj = {};
            }
        });
    };

    $scope.updateCustomerRequest = function () {
        $scope.showLoader = true;
        noty({
            text: 'Do you want to Update Customer Request Details?',
            layout: 'topCenter',
            buttons: [
                {
                    addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                        $noty.close();

                        if ($scope.cusObj.FamilyDiscount === undefined || $scope.cusObj.FamilyDiscount === "" || $scope.cusObj.FamilyDiscount === null) {
                            $scope.cusObj.FamilyDiscount = 0;
                        }

                        $scope.cusObj.BusinessUnitID = $scope.businessUnitID;
                        $scope.cusReqObj.RequestedDate = $scope.getFormattedDate($scope.cusReqObj.RequestedDate);
                        $scope.cusReqObj.ClientRequestLineDetails = [];


                        ClientRequestService.updateClientRequest($scope.isCustomerUpdated, $scope.isCustomerAdded, $scope.cusObj, $scope.cusReqObj, $scope.currentUser.UserID).then(function (results) {
                            $scope.showLoader = false;
                            //alert(angular.toJson(results));
                            if (results.status === true) {
                                noty({
                                    text: 'Successfully Updated Customer Request Details',
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

    $scope.cancelCustomerRequest = function () {
        $scope.ClearFields();
        $scope.refreshContent();
        $scope.activateClientRequestListTab();
    };

    $scope.viewClientRequest = function (clientReqHeaderID, clientID) {
        $scope.activateNewClientRequestTab();

        $scope.showLoader = true;
        ClientRequestService.getClientRequestByID(clientReqHeaderID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.isViewMode = true;
                $scope.isClientReqAddMode = false;
                $scope.cusReqObj = results.data;
                $scope.cusReqObj.PartnerID = $scope.cusReqObj.PartnerID + "";
                $scope.loadClientByID(clientID);
            }
            else {
                $scope.cusReqObj = {};
            }
        });
    };

    $scope.loadAgent = function () {
        //$scope.showLoader = true;
        ClientRequestService.loadAgent().then(function (results) {
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

    $scope.loadIntroducer = function () {
        //$scope.showLoader = true;
        ClientRequestService.loadIntroducer().then(function (results) {
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

    $scope.loadExecutive = function () {
        //$scope.showLoader = true;
        ClientRequestService.loadExecutive().then(function (results) {
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

    $scope.receivedQuotaion = function (quotationNo) {

    }

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
    $scope.loadInsClass = function () {
        $scope.showLoader = true;
        QuotationService.getAvailableInsuranceDropdown().then(function (results) {

            if (results.status === true) {
                $scope.availableInsClass = results.data;
            }
            else {
                $scope.availableInsClass = [];
            }


        });
    };

    $scope.getAllInsuranceSubClass = function () {
        $scope.showLoader = true;
        QuotationService.getAvailableSubInsurance().then(function (results) {
            //$scope.showLoader = false;
            $scope.isInsuranceClassLoaded = true;

            if (results.status === true) {
                $scope.availableInsuranceClass = results.data;
            }
            else {
                $scope.availableInsuranceClass = [];
            }
        });
    }

    $scope.initializeQuotation = function (clientReqHeaderID) {
        $scope.quotationHeader = {};
        $scope.quotationLineDetails = [];

        $scope.quotationHeader = { "ClientRequestHeaderID": clientReqHeaderID, "Status": true, "QuotationLineDetails": $scope.quotationLineDetails };

        sessionStorage.setItem("Quotation", $scope.quotationHeader);

        $scope.showLoader = true;
        ClientRequestService.initializeQuotation($scope.quotationHeader, $scope.currentUser.UserID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.quotationHeader = {};
                noty({
                    text: 'Quotation is initialized successfully',
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

                $scope.refreshContent();
                window.location.assign("ManageQuotation");
            }

            else {
                noty({
                    text: 'Error occured in quotation initialization',
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
    };

    $scope.receivedQuotationArray = function () {
        $scope.receievedQuotations = [];
        $scope.receievedQuotations.push({
            InsuranceClassID: "",
            InsuranceSubClassID: "",
            CompId: "",
            IsRequest: "",
            RequestedDate: "",
            File: null


        })
    }

    $scope.receivedDoc = function (e) {
        $scope.receivedFile = {};
        $scope.receivedFile = e.files[0];
    }

    $scope.receiveQuotation = function () {
        var formData = new FormData();
        formData.append("headerId", $scope.quotationHeaderObj.QuotationNo);
        formData.append("insClassId", $scope.receievedQuotations[0].InsuranceClassID);
        formData.append("insSubClassID", $scope.receievedQuotations[0].InsuranceSubClassID);
        formData.append("compId", $scope.receievedQuotations[0].CompId);
        formData.append("receivedDate", $scope.getFormattedDate($scope.receievedQuotations[0].RequestedDate)); // $scope.receievedQuotations[0].RequestedDate);//$scope.getFormattedDate($scope.receievedQuotations[0].RequestedDate));
        formData.append("receivedUser", $scope.currentUser.UserID);
        formData.append("uploadedDocument", $scope.receivedFile); //$scope.uploadedDocument);

        var objXhr = new XMLHttpRequest();

        objXhr.onreadystatechange = function () {
            $scope.showLoader = false;
            if (this.readyState == 4 && this.status == 200 && $scope.saveStatus == false) {
                $scope.saveStatus = true;
                noty({
                    text: 'Successfully Received Document',
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

                $scope.receivedFile = {};
                $scope.receivedQuotationArray();
                $scope.activateClientRequestListTab();
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
        $scope.filePath = "http://localhost:39705/api/Quotation/ReceivedQuotation";
        objXhr.open("POST", $scope.filePath, true);
        objXhr.setRequestHeader("Authorization", 'Basic QWRtaW5JQk1TOmlibXNJcyMyMDE4');
        objXhr.send(formData);

    }

    $scope.download = function (quotationHeaderId) {

        ClientRequestService.getReceiveQuotationFileName(quotationHeaderId).then(function (results) {
            if (results.status == true) {

                var fileName = results.data;
                var baseUri = "http://localhost:39705/Uploads/Documents/";

                //window.location.href = "http://localhost:39705/Uploads/Documents/Free Issue SDS.docx";
                window.location.href = baseUri + fileName;
            }
        });


    }

});

ibmsApp.filter('return_status', function ($sce) {
    return function (text, length, end) {
        if (text) {
            return $sce.trustAsHtml('<span><i style="color:green" class="glyphicon glyphicon-ok"></i></span>');
        }
        return $sce.trustAsHtml('<span><i style="color:red" class="glyphicon glyphicon-remove"></i></span>');
    }
});
