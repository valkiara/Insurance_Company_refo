'use strict';


ibmsApp.controller("ChargeTypeController", function ($scope, $http, ChargeTypeService, $window, AuthService, filterFilter) {

    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
           // $scope.companyID = results.CompanyID;
        });
    };

    $scope.refreshContent = function () {
        GetAllChargeType();
        $scope.search_query = "";
    };

    GetAllChargeType();
    $scope.getCurrentUser();
    $scope.Charge = [];


    $scope.init = function () {
        //$("#edit" + id).show();
        $scope.getCurrentUser();
        GetAllChargeType();
    };

    $scope.edit = function (Charge) {
        $("#view" + Charge.ChargeTypeID).hide();
        $("#edit" + Charge.ChargeTypeID).show();

    };

    $scope.Delete = function (Charge) {
        $scope.showLoader = true;
        SuccessDelete(Charge)
    };

    function SuccessDelete(Charge) {
        noty({
            text: 'Do you want to Delete Charge Type Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Yes', onClick: function ($noty) {
                            $noty.close();
                            Deletes(Charge);
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

    function Deletes(Charge) {
        var chargeTypeID = Charge.ChargeTypeID;

        ChargeTypeService.DeleteChargeType(chargeTypeID).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Deleted Charge Type Details',
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
                       text: 'Error Deleteing Charge Type Details',
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

    $scope.update = function (Charge) {
        $scope.showLoader = true;
        $("#view" + Charge.ChargeTypeID).show();
        $("#edit" + Charge.ChargeTypeID).hide();
        SuccessUpdate(Charge);
    };


    function SuccessUpdate(Charge) {
        noty({
            text: 'Do you want to Update Charge Type Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Yes', onClick: function ($noty) {
                            $noty.close();
                            Update(Charge);
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

    function Update(Charge) {

        var chargeTypeID = Charge.ChargeTypeID;
        var chargeTypeName = Charge.ChargeTypeName;
        var percentage = Charge.Percentage;

        ChargeTypeService.UpdateChargeType(
           chargeTypeID, chargeTypeName, percentage,$scope.currentUser.UserID).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Updated Charge Type Details',
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
                       text: 'Error Update Charge Type Details.' + " " + results.message,
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

    $scope.cancel = function (Charge) {
        $("#view" + Charge.ChargeTypeID).show();
        $("#edit" + Charge.ChargeTypeID).hide();
        $scope.refreshContent();
    };

    $scope.Save = function () {
        $scope.showLoader = true;
        Success();
    };

    function Success() {
        noty({
            text: 'Do you want to Save Charge Type Details?',
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
        })
    };
    function save() {

        var chargeTypeName = $scope.ChargeTypeName;
        var percentage = $scope.Percentage;

        ChargeTypeService.SaveChargeType(chargeTypeName, percentage, $scope.currentUser.UserID).
               then(function (results) {
                   $scope.showLoader = false;

                   if (results.status === true) {
                       noty({
                           text: 'Successfully Saved Charge Type Details',
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
                           text: 'Error Saving Charge Type Details.' + " " + results.message,
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
    $scope.ClearFields = function () {
        $scope.ChargeTypeName = null;
        $scope.Percentage = null;
    }
    
    function GetAllChargeType() {
        $scope.showLoader = true;
        ChargeTypeService.GetAllChargeTypes().then(function (results) {
            $scope.showLoader = false;
            $scope.Charge = results.data;
            $scope.data = angular.copy($scope.Charge);
            $scope.viewby = "5";
            $scope.totalItems = $scope.data.length;
            $scope.currentPage = 1;
            $scope.itemsPerPage = $scope.viewby;
            $scope.maxSize = 5; //Number of pager buttons to show
            $scope.setItemsPerPage($scope.viewby);
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
        $scope.data = filterFilter($scope.Charge, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };

});