'use strict';

ibmsApp.controller("InternalPolicyNumberSetupController", function ($scope, $http, InternalPolicyNumberSetupService, $location, AuthService,filterFilter) {

    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
            $scope.businessUnitID = results.BusinessUnitID;
            $scope.BusinessUnitID = results.BusinessUnitID;
            $scope.loadBU();
            GetAllInternalPolicyNumSetup();
        });
    };

    $scope.refreshContent = function () {
        GetAllInternalPolicyNumSetup();
        $scope.search_query = "";
    };

    GetAllInternalPolicyNumSetup();
    $scope.getCurrentUser();
    $scope.Policy = [];

    $scope.init = function () {
        $scope.desObj = {};
       // $scope.loadBU();
        $scope.filteredBusinessUnits = [];
        $scope.getCurrentUser();
       // GetAllInternalPolicyNumSetup();
    };

    $scope.dropDownConfig = {
        maxItems: 1,
        create: function (input) {
            return {
                value: "new-" + input,
                text: input
            }
        }
    };

    $scope.buChange = function (companyID) {
        for (var i = 0; i < $scope.availableCompany.length; i++) {
            if ($scope.availableCompany[i].CompanyID.toString() === companyID) {
                $scope.companyName = $scope.availableCompany[i].companyName;
                break;
            }
        }
    };

    $scope.loadBU = function () {
        if ($scope.currentUser.AccessLevelTypeName == "Admin") {
            $scope.showLoader = true;
            InternalPolicyNumberSetupService.getAvailableBUDropdown().then(function (results) {
                //$scope.showLoader = false;
                //$scope.isCompanyLoaded = true;

                if (results.status === true) {
                    $scope.filteredBusinessUnits = results.data;
                    //for (var i = 0; i < $scope.availableBU.length; i++) {
                    //    if ($scope.availableBU[i].BusinessUnitID === $scope.currentUser.BusinessUnitID) {
                    //        $scope.filteredBusinessUnits.push({ "BusinessUnitID": $scope.availableBU[i].BusinessUnitID, "BusinessUnit": $scope.availableBU[i].BusinessUnit });
                    //        break;
                    //    }
                    //}

                }
               
            });
        }

        else {
            $scope.filteredBusinessUnits = [];
            $scope.filteredBusinessUnits.push({
                "BusinessUnitID": $scope.currentUser.BusinessUnitID,
                "BusinessUnit": $scope.currentUser.BusinessUnitName

            });
        }
    };

    $scope.getAllBusinessUnit = function () {
       // $scope.showLoader = true;
        InternalPolicyNumberSetupService.getAllBusiUnit().then(function (results) {
            $scope.showLoader = false;
            $scope.isBusinessUnitLoaded = true;

            if (results.status === true) {
                $scope.availableBusinessUnit = results.data;
            }
            else {
                $scope.availableBusinessUnit = [];
            }


        });
    };

    $scope.edit = function (Policy) {
        $("#view" + Policy.InternalPolicyNumSetupID).hide();
        $("#edit" + Policy.InternalPolicyNumSetupID).show();
    };

    $scope.Delete = function (Policy) {
        $scope.showLoader = true;
        SuccessDelete(Policy)
    };

    function SuccessDelete(Policy) {
        noty({
            text: 'Do you want to Delete Internal Policy Number Setup Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Yes', onClick: function ($noty) {
                            $noty.close();
                            Deletes(Policy);
                        }
                    },
                    {
                        addClass: 'btn btn-danger btn-clean', text: 'No', onClick: function ($noty) {
                            $noty.close();
                            $scope.$apply(function () {
                                $scope.showLoader = false;
                            });
                        }
                    }
            ]
        });
    };

    function Deletes(Policy) {
        var internalPolicyNumSetupID = Policy.InternalPolicyNumSetupID;

        InternalPolicyNumberSetupService.DeleteInternalPolicyNumSetup(internalPolicyNumSetupID).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Deleted Internal Policy Number Setup Details',
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
               }
               else {
                   noty({
                       text: 'Error Deleteing Internal Policy Number Setup Details',
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

    $scope.update = function (Policy) {
        $scope.showLoader = true;
        $("#view" + Policy.InternalPolicyNumSetupID).show();
        $("#edit" + Policy.InternalPolicyNumSetupID).hide();
        SuccessUpdate(Policy);
    };

    function SuccessUpdate(Policy) {
        noty({
            text: 'Do you want to Update Internal Policy Number Setup Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Yes', onClick: function ($noty) {
                            $noty.close();
                            Update(Policy);
                        }
                    },
                    {
                        addClass: 'btn btn-danger btn-clean', text: 'No', onClick: function ($noty) {
                            $noty.close();
                            $scope.$apply(function () {
                                $scope.showLoader = false;
                            });
                        }
                    }
            ]
        })
    };

    function Update(Policy) {

        var internalPolicyNumSetupID = Policy.InternalPolicyNumSetupID;
        var internalPolicyNumber = Policy.InternalPolicyNumber;
        var businessUnitID = Policy.BusinessUnitID;

        InternalPolicyNumberSetupService.UpdateInternalPolicyNumSetup(
           internalPolicyNumSetupID, internalPolicyNumber, businessUnitID, $scope.currentUser.UserID).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Updated Internal Policy Number Setup Details',
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
               }
               else {
                   noty({
                       text: 'Error in Updating Internal Policy Number Setup Details.' + " " + results.message,
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

    $scope.cancel = function (Policy) {
        $("#view" + Policy.InternalPolicyNumSetupID).show();
        $("#edit" + Policy.InternalPolicyNumSetupID).hide();
        $scope.refreshContent();
    };


    $scope.Save = function () {
        $scope.showLoader = true;
        Success();
    };

    function Success() {
        noty({
            text: 'Do you want to Save Internal Policy Number Setup Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Yes', onClick: function ($noty) {
                            $noty.close();
                            save();
                            //noty({ text: 'You clicked "Ok" button', layout: 'topRight', type: 'success' });
                        }
                    },
                    {
                        addClass: 'btn btn-danger btn-clean', text: 'No', onClick: function ($noty) {
                            $noty.close();
                            $scope.$apply(function () {
                                $scope.showLoader = false;
                            });
                            //noty({ text: 'You clicked "Cancel" button', layout: 'topRight', type: 'error' });
                        }
                    }
            ]
        });
    };
    function save() {

        var internalPolicyNumber = $scope.InternalPolicyNumber;
        var businessUnitID = $scope.BusinessUnitID;

        InternalPolicyNumberSetupService.SaveInternalPolicyNumSetup(internalPolicyNumber, businessUnitID, $scope.currentUser.UserID).
               then(function (results) {
                   $scope.showLoader = false;

                   if (results.status === true) {
                       noty({
                           text: 'Successfully Saved Internal Policy Number Setup Details',
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
                       //setTimeout(function () { window.location.href = "/Agent/Index" }, 2500)
                       $scope.ClearFields();
                       $scope.refreshContent();
                   }
                   else {
                       noty({
                           text: 'Error Saving Internal Policy Number Setup Details.' + " " + results.message,
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

    $scope.ClearFields = function() {
        //$scope.companyID = null;
        $scope.InternalPolicyNumber = null;
        $scope.BusinessUnitID = null;
        //$scope.InsSubClassID = null;
    };


    function GetAllInternalPolicyNumSetup() {
        $scope.showLoader = true;
        InternalPolicyNumberSetupService.GetAllInternalPolicyNumSetups().then(function (results) {
            $scope.showLoader = false;
            $scope.Policy = results.data;
            $scope.data = angular.copy($scope.Policy);
            $scope.viewby = "5";
           $scope.totalItems = $scope.data.length;
            $scope.currentPage = 1;
            $scope.itemsPerPage = $scope.viewby;
            $scope.maxSize = 5; //Number of pager buttons to show
            $scope.setItemsPerPage($scope.viewby);
            //alert(angular.toJson(results));
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
        $scope.data = filterFilter($scope.Policy, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };
});