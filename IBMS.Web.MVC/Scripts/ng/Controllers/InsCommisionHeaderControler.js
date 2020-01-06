'use strict';

ibmsApp.controller("InsCommisionHeaderControler", function ($scope, $http, $rootScope, InsCommissionHeaderService, $location, AuthService, filterFilter) {

    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
            //alert(angular.toJson($scope.currentUser));
            $scope.businessUnitID = results.BusinessUnitID;
            $scope.CommissionObj.buID = results.BusinessUnitID;
            $scope.loadBusinessUnit();
            $scope.loadCommissionHeader();
            //$scope.loadCommissionStrctureDropdown();

        });
    };


    $scope.addItems = function () {

        $scope.policyInfoRecObj.ChargeTypeList.push({ ChargeTypeID: "", Amount: "" });
        //$scope.policyInfoRecObj.GrossPremium = $scope.policyInfoRecObj.policyInfoChargeList[0].Amount;
        //$scope.policyInfoRecObj.GrossPremium = $scope.policyInfoRecObj.GrossPremium + $scope.policyInfoRecObj.policyInfoChargeList[1].Amount;
        //$scope.policyInfoRecObj.GrossPremium = $scope.policyInfoRecObj.GrossPremium + $scope.policyInfoRecObj.policyInfoChargeList[2].Amount;
    }; 

   
    $scope.refreshContent = function () {
        //$scope.loadCommissionHeader();
        $scope.loadChargeTypes();
        $scope.search_query = "";
    };

    $scope.ClearFields = function () {
        $scope.InsCommissionObj = {};
        $scope.InsCommissionObj.policyInfoChargeList = [];
        $scope.loadInsCompany();
        $scope.loadInsClass();
        $scope.loadChargeTypes();
    };

    $scope.init = function () {
        $scope.CommissionObj = {};
        $scope.InsCommissionObj = {};
        $scope.filteredBusinessUnits = [];
        $scope.getCurrentUser();
        $scope.policyInfoRecObj = {};
        $scope.InsCommissionObj.policyInfoChargeList = [];
        $scope.addItems();
        $scope.availableChargeTypes = [];
        $scope.CommissionHeaderList=[];
        $scope.loadChargeTypes();
        $scope.policyInfoChargeModel = [];
        //$scope.loadBusinessUnit();
        //$scope.loadPartner();
        $scope.loadInsCompany();
        $scope.loadInsClass();
        //$scope.loadCommissionStrcture();
        //$scope.loadRateCategory();
        //$scope.loadCommissionStrctureDropdown();
        //$scope.loadInsSubClass();
        //$scope.loadCommissionHeader();
        //$scope.LoadSubInsClass();


        $scope.one = true;
    };

    //$scope.loadCommissionStrctureDropdown = function () {
    //    CommissionStructureLineService.getAvailableCommisionStructure().then(function (results) {
    //        if (results.status === true) {
    //            $scope.availableComStrct = results.data;
    //        }
    //        else {
    //            $scope.availableComStrct = [];
    //        }
    //    });
    //};
    $scope.loadChargeTypes = function () {
        InsCommissionHeaderService.getAllChargeTypes().then(function (results) {
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

    $scope.addItems = function () {
        $scope.InsCommissionObj.policyInfoChargeList.push({ ChargeTypeID: "", Amount: "" });
       
    }; 

    $scope.deletePolicyInfoChargeDetails = function (deleteIndex) {
        $scope.InsCommissionObj.policyInfoChargeList.splice(deleteIndex, 1);
    };


    $scope.loadBusinessUnit = function () {
        if ($scope.currentUser.AccessLevelTypeName == "Admin") {
            $scope.showLoader = true;
            CommissionHeaderService.getAvailableBusinessUnit().then(function (results) {
                $scope.showLoader = false;
                if (results.status === true) {
                    $scope.filteredBusinessUnits = results.data;

                    //for (var i = 0; i < $scope.availableBusinessUnit.length; i++) {
                    //    if ($scope.availableBusinessUnit[i].BusinessUnitID === $scope.businessUnitID) {
                    //        $scope.filteredBusinessUnits.push({ "BusinessUnitID": $scope.availableBusinessUnit[i].BusinessUnitID, "BusinessUnit": $scope.availableBusinessUnit[i].BusinessUnit });
                    //        break;
                    //    }
                    //}
                }
                else {
                    $scope.availableBusinessUnit = [];
                    $scope.filteredBusinessUnits = [];
                }
            });
        }
        //  }

        else {
            $scope.filteredBusinessUnits = [];

            $scope.filteredBusinessUnits.push({
                "BusinessUnitID": $scope.currentUser.BusinessUnitID,
                "BusinessUnit": $scope.currentUser.BusinessUnitName

            });

        }
    }

    $scope.loadCommissionHeader = function () {
        $scope.showLoader = true;
        InsCommissionHeaderService.getAvailableCommissionHeader($scope.currentUser.BusinessUnitID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                var data = $scope.availableCommsionHeader;
                $scope.availableCommsionHeader = results.data;
                

                $scope.data = angular.copy($scope.availableCommsionHeader);
                $scope.viewby = "5";
                $scope.totalItems = $scope.data.length;
                $scope.currentPage = 1;
                $scope.itemsPerPage = $scope.viewby;
                $scope.maxSize = 5; //Number of pager buttons to show
                $scope.setItemsPerPage($scope.viewby);
            }
            else {
                $scope.availableCommsionHeader = [];
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
        $scope.currentPage = 1; //reset to first page
    }
    $scope.searchTextChange = function (searchText) {
        $scope.data = filterFilter($scope.availableCommsionHeader, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };

    $scope.loadPartner = function () {
        $scope.showLoader = true;
        CommissionHeaderService.getAvailablePartner().then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.availablePartner = results.data;
            }
            else {
                $scope.availablePartner = [];
            }
        });
    };

    $scope.loadInsClass = function () {
        $scope.showLoader = true;
        InsCommissionHeaderService.getAvailableInsClass().then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.availableInsClass = results.data;
                //$scope.LoadSubInsClass($scope.availableInsClass.InsuranceClassID);
            }
            else {
                $scope.availableInsClass = [];
            }
        });
    };

    $scope.LoadSubInsClass = function (InsuranceClassID) {
        $scope.showLoader = true;
        //alert(insClass);
        InsCommissionHeaderService.getAvailableInsSubClass(InsuranceClassID).then(function (results) {
            $scope.showLoader = false;
            //  alert(angular.toJson(results));
            if (results.status === true) {
                $scope.availableInsSubClass = results.data;
            }
            else {
                $scope.availableInsSubClass = [];
            }
        });
    };

    $scope.loadInsCompany = function () {
        $scope.showLoader = true;
        InsCommissionHeaderService.getAvailableInsCompany().then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.availableInsCompany = results.data;
            }
            else {
                $scope.availableInsCompany = [];
            }
        });
    };

    //function getChargeList() {
    //    var chargeType = this;
    //    chargeType.ChargeTypeID = "";
    //    chargeType.Amount = "";

    //}

    //$scope.AddList = function (data) {
    //    var eg = new getChargeList();
    //    eg.ID = data.ChargeTypeID;
    //    eg.Amount = data.Amount;
    //    $scope.List.push(eg);
    //}

    $scope.addCommissionHeader = function ()
    {
        
        $scope.showLoader = true;
        Success();
    };

    $scope.LoadChargeTypeDetails = function (idx) {
        $scope.InsCommissionObj.policyInfoChargeList[idx].policyInfoChargeListDetails = [];
    };

    //$scope.manageProperties = function (idx) {

    //    $scope.insChargeTypeDetailsTemp = [];
        

    //    if ($scope.InsCommissionObj.policyInfoChargeList[idx].policyInfoChargeListDetails.length > 0) {
    //        for (var i = 0; i < $scope.policyInfoRecObj.policyInfoChargeList[idx].policyInfoChargeListDetails.length; i++) {
                
    //            $scope.policyInfoRecObj.policyInfoChargeList[idx].policyInfoChargeListDetails[i].ChargeTypeID = $scope.policyInfoRecObj.policyInfoChargeList[idx].policyInfoChargeListDetails[i].ChargeTypeID + "";
    //            $scope.policyInfoRecObj.policyInfoChargeList[idx].policyInfoChargeListDetails[i].Amount = $scope.policyInfoRecObj.policyInfoChargeList[idx].policyInfoChargeListDetails[i].Amount + "";
    //        }

    //        $scopeChild.insSubClassScopeDetailsTemp = angular.copy(($scope.cusReqObj.ClientRequestLineDetails[idx].ClientRequestInsSubClassScopeDetails));
    //    }
       
    //};
   

    function saveCommissionHeader() {
        InsCommissionHeaderService.saveCommissionHeader($scope.InsCommissionObj, $scope.currentUser, $scope.policyInfoRecObj).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {

                noty({
                    text: 'Successfully Saved Commission Header Details',
                    layout: 'topCenter',
                    type: 'success',
                    buttons: [
                        {
                            addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                                $noty.close();
                                $scope.showLoader = false;
                            }
                        }
                    ]
                });
                //setTimeout(function () { window.location.href = "/CommissionHeader/Index" }, 2500)
                $scope.ClearFields();
                $scope.refreshContent();
            }
            else {
                noty({
                    text: 'Error Saving Commission Header Details.' + " " + results.message,
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

    $scope.edit = function (REQ) {
        $("#view" + REQ.CommisionStructureID).hide();
        $("#edit" + REQ.CommisionStructureID).show();

        REQ.BusinessUnitID = $scope.businessUnitID;
    };

    $scope.cancel = function (REQ) {
        $("#view" + REQ.CommisionStructureID).show();
        $("#edit" + REQ.CommisionStructureID).hide();
        $scope.refreshContent();
    };

    $scope.update = function (REQ) {
        $("#view" + REQ.CommisionStructureID).show();
        $("#edit" + REQ.CommisionStructureID).hide();

        $scope.showLoader = true;
        SuccessUpdate(REQ);
    };

    function Update(REQ) {
        var CommisionStructureID = REQ.CommisionStructureID;
        var CommisionStructureName = REQ.CommisionStructureName;
        var BusinessUnitID = REQ.BusinessUnitID;
        var PartnerID = REQ.PartnerID;
        var InsuranceCompanyID = REQ.InsuranceCompanyID;
        var InsuranceClassID = REQ.InsuranceClassID;
        var InsuranceSubClassID = REQ.InsuranceSubClassID;
        var userID = $scope.currentUser.UserID;

        CommissionHeaderService.updateCommissionHeader(
            CommisionStructureID, CommisionStructureName, BusinessUnitID, PartnerID, InsuranceCompanyID, InsuranceClassID, InsuranceSubClassID, userID
        ).
            then(function (results) {
                $scope.showLoader = false;
                if (results.status === true) {
                    noty({
                        text: 'Successfully Updated Commission Header Details',
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

                    //setTimeout(function () { window.location.href = "/CommissionHeader/Index" }, 2500)
                    $scope.ClearFields();
                    $scope.refreshContent();
                }
                else {
                    noty({
                        text: 'Error Update Commission Header Details.' + " " + results.message,
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
                    $scope.ClearFields();
                    $scope.refreshContent();
                }
            });

    };

    $scope.deleteCommissionHeader = function (DOCID) {
        $scope.showLoader = true;
        SuccessDelete(DOCID);
    };

    function Delete(DOCID) {
        CommissionHeaderService.deleteCommissionHeader(DOCID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                noty({
                    text: 'Successfully Deleted Commission Header Details',
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
                //setTimeout(function () { window.location.href = "/CommissionHeader/Index" }, 2500)
                $scope.refreshContent();
            }
            else {
                noty({
                    text: 'Error Deleteing Commission Header Details',
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

    function Success() {
        //alert("cc");
        noty({
            text: 'Do you want to Save Commission Header Details?',
            layout: 'topCenter',
            buttons: [
                {
                    addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                        $noty.close();
                        saveCommissionHeader();
                        //noty({ text: 'You clicked "Ok" button', layout: 'topRight', type: 'success' });
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

    function SuccessUpdate(BU) {
        //alert("cc");
        noty({
            text: 'Do you want to Update Commission Header Details?',
            layout: 'topCenter',
            buttons: [
                {
                    addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                        $noty.close();
                        Update(BU)

                        //noty({ text: 'You clicked "Ok" button', layout: 'topRight', type: 'success' });
                    }
                },
                {
                    addClass: 'btn btn-danger btn-clean', text: 'Cancel', onClick: function ($noty) {
                        $noty.close();
                        $scope.$apply(function () {
                            $scope.showLoader = false;
                        });
                        $scope.refreshContent();
                        //noty({ text: 'You clicked "Cancel" button', layout: 'topRight', type: 'error' });
                    }
                }
            ]
        })
    };

    function SuccessDelete(BUID) {
        //alert("cc");
        noty({
            text: 'Are you sure want to Delete Commission Header Details?',
            layout: 'topCenter',
            buttons: [
                {
                    addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                        $noty.close();
                        Delete(BUID);

                        //noty({ text: 'You clicked "Ok" button', layout: 'topRight', type: 'success' });
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


    //Save Commission Headers

    $scope.addCommisionLine = function () {
        $scope.showLoader = true;
        SuccessUpdate();
    };


    function SuccessUpdate() {
        //alert("cc");
        noty({
            text: 'Do you want to Save Commission Structure Line Details?',
            layout: 'topCenter',
            buttons: [
                {
                    addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                        $noty.close();
                        savecom();
                        //noty({ text: 'You clicked "Ok" button', layout: 'topRight', type: 'success' });
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


    function savecom() {
            InsCommissionHeaderService.addComStructLine($scope.comObj, $scope.currentUser).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                noty({
                    text: 'Successfully Saved Commission Structure Line Details',
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
                //setTimeout(function () { window.location.href = "/CommisionStructure/Index" }, 2500)
                $scope.ClearFields();
                $scope.refreshContent();
            }
            else {
                noty({
                    text: 'Error Saving Commission Structure Line Details',
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


    $scope.loadRateCategory = function () {

        $scope.showLoader = true;
        CommissionHeaderService.getAvailableRateCategory().then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.availableRateCat = results.data;
            }
            else {
                $scope.availableRateCat = [];
            }
            $scope.refreshContent();
        });
    };

});