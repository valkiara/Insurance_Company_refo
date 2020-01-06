'use strict';


ibmsApp.controller("ClaimPaymentTypeController", function ($scope, $http, ClaimPaymentTypeService, $window, AuthService, filterFilter) {

    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
            // $scope.companyID = results.CompanyID;
        });
    };

    $scope.refreshContent = function () {
        GetAllPaymentType();
        $scope.search_query = "";
    };

    GetAllPaymentType();
    $scope.getCurrentUser();
    $scope.Paymenttype = [];


    $scope.init = function () {
        //$("#edit" + id).show();
        $scope.getCurrentUser();
        GetAllPaymentType();
    };

    $scope.edit = function (Paymenttype) {
        $("#view" + Paymenttype.PaymentTypeID).hide();
        $("#edit" + Paymenttype.PaymentTypeID).show();

    };

    $scope.Delete = function (Paymenttype) {
        $scope.showLoader = true;
        SuccessDelete(Paymenttype)
    };

    function SuccessDelete(Paymenttype) {
        noty({
            text: 'Do you want to Delete Claim Payment Type Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            Deletes(Paymenttype);
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

    function Deletes(Paymenttype) {
        var paymentTypeID = Paymenttype.PaymentTypeID;

        ClaimPaymentTypeService.DeletePaymentType(paymentTypeID).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Deleted Claim Payment Type Details',
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
                       text: 'Error Deleting Claim Payment Type Details' + ' / ' + results.message,
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

    $scope.update = function (Paymenttype) {
        $scope.showLoader = true;
        $("#view" + Paymenttype.PaymentTypeID).show();
        $("#edit" + Paymenttype.PaymentTypeID).hide();
        SuccessUpdate(Paymenttype);
    };


    function SuccessUpdate(Paymenttype) {
        noty({
            text: 'Do you want to Update Claim Payment Type Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            Update(Paymenttype);
                        }
                    },
                    {
                        addClass: 'btn btn-danger btn-clean', text: 'Cancel', onClick: function ($noty) {
                            $noty.close();
                            $scope.$apply(function () {
                                $scope.showLoader = false;
                            });
                            $scope.refreshContent();
                        }
                    }
            ]
        })
    };

    function Update(Paymenttype) {

        var paymentTypeID = Paymenttype.PaymentTypeID;
        var paymentTypeName = Paymenttype.PaymentTypeName;
        var description = Paymenttype.Description;

        ClaimPaymentTypeService.UpdatePaymentType(
           paymentTypeID, paymentTypeName, description, $scope.currentUser.UserID).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Updated Claim Payment Type Details',
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
                       text: 'Error Update Claim Payment Type Details' + ' / ' + results.message,
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
                   $scope.refreshContent();
               }
           });

    };

    $scope.cancel = function (Paymenttype) {
        $("#view" + Paymenttype.PaymentTypeID).show();
        $("#edit" + Paymenttype.PaymentTypeID).hide();
        $scope.refreshContent();
    };

    $scope.Save = function () {
        $scope.showLoader = true;
        Success();
    };

    function Success() {
        noty({
            text: 'Do you want to Save Claim Payment Type Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            save();
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
    function save() {

        var paymentTypeName = $scope.PaymentTypeName;
        var description = $scope.Description;

        ClaimPaymentTypeService.SavePaymentType(paymentTypeName, description, $scope.currentUser.UserID).
               then(function (results) {
                   $scope.showLoader = false;

                   if (results.status === true) {
                       noty({
                           text: 'Successfully Saved Claim Payment Type Details',
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
                           text: 'Error Saving Claim Payment Type Details'+ ' / ' + results.message,
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
        //$scope.companyID = null;
        $scope.PaymentTypeName = null;
        $scope.Description = null;
    };

    function GetAllPaymentType() {
        $scope.showLoader = true;
        ClaimPaymentTypeService.GetAllPaymentTypes().then(function (results) {
            $scope.showLoader = false;
            $scope.Paymenttype = results.data;
            $scope.data = angular.copy($scope.Paymenttype);
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
        $scope.data = filterFilter($scope.Paymenttype, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };
});