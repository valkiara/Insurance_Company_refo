'use strict';

ibmsApp.controller("CauseOfLossController", function ($scope, $http, CauseOfLossService, $window, AuthService,filterFilter) {

    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
          //  alert(angular.toJson($scope.currentUser));
            //$scope.companyID = results.CompanyID;
        });
    };


    $scope.refreshContent = function () {
        GetAllCauseOfLoss();
        $scope.search_query = "";
    };

    GetAllCauseOfLoss();
    $scope.getCurrentUser();
    $scope.Cause = [];

    $scope.init = function () {
        //$("#edit" + id).show();
        $scope.buObj1 = {};
        $scope.getAllInsuranceClass();
     
        $scope.getCurrentUser();
        GetAllCauseOfLoss();
    };

    $scope.getAllInsuranceClass = function () {
        $scope.showLoader = true;
        CauseOfLossService.getAllInsClass().then(function (results) {
            $scope.showLoader = false;
            $scope.isInsuranceClassLoaded = true;

            if (results.status === true) {
                $scope.availableInsuranceClass = results.data;
            }
            else {
                $scope.availableInsuranceClass = [];
            }

           
        });
    };
    $scope.LoadInsSubClass = function (InsuranceClassID)
    {

        InsuranceClassChange(InsuranceClassID);
    };
    function InsuranceClassChange (InsuranceClassID) {
       // alert(InsuranceClassID);
       
        CauseOfLossService.getAvailableInsSubClass(InsuranceClassID).then(function (results) {
            $scope.showLoader = false;
         

            if (results.status === true) {
                $scope.availableInsuranceSubClass = results.data;
            }
            else {
                $scope.availableInsuranceSubClass = [];
            }

           
        });
    };
    $scope.edit = function (Cause) {
        $("#view" + Cause.CauseOfLossID).hide();
        $("#edit" + Cause.CauseOfLossID).show();
    };

    $scope.Delete = function (Cause) {
        $scope.showLoader = true;
        SuccessDelete(Cause)
    };

    function SuccessDelete(Cause) {
        noty({
            text: 'Do you want to Delete Cause of Loss Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            Deletes(Cause);
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

    function Deletes(Cause) {
        var causeOfLossID = Cause.CauseOfLossID;

        CauseOfLossService.DeleteCauseOfLoss(causeOfLossID).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Deleted Cause of Loss Details',
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
                       text: 'Error Deleting Cause of Loss Details' + ' / ' + results.message,
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

    $scope.update = function (Cause) {
        $scope.showLoader = true;
        $("#view" + Cause.CauseOfLossID).show();
        $("#edit" + Cause.CauseOfLossID).hide();
        SuccessUpdate(Cause);
    };

    function SuccessUpdate(Cause) {
        noty({
            text: 'Do you want to Update Cause of Loss Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            Update(Cause);
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

    function Update(Cause) {

        var causeOfLossID = Cause.CauseOfLossID;
        var causeOfLoss = Cause.CauseOfLoss;
        var insSubClassID = Cause.InsSubClassID;

        CauseOfLossService.UpdateCauseOfLoss(
           causeOfLossID, causeOfLoss, insSubClassID, $scope.currentUser.UserID).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Updated Cause of Loss Details',
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
                       text: 'Error Updating Cause of Loss Details' + ' / ' + results.message,
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


    $scope.cancel = function (Cause) {
        $("#view" + Cause.CauseOfLossID).show();
        $("#edit" + Cause.CauseOfLossID).hide();
        $scope.refreshContent();
    };

    $scope.Save = function () {
        $scope.showLoader = true;
        Success();
    };

    function Success() {
        noty({
            text: 'Do you want to Save Cause of Loss Details?',
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

        var causeOfLoss = $scope.cause;
        var insSubClassID = $scope.InsSubClassID;

        CauseOfLossService.SaveCauseOfLoss(causeOfLoss, insSubClassID, $scope.currentUser.UserID).
               then(function (results) {
                   $scope.showLoader = false;

                   if (results.status === true) {
                       noty({
                           text: 'Successfully Saved Cause of Loss Details',
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
                           text: 'Error Saving Cause of Loss Details' + ' / ' + results.message,
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
        $scope.cause = null;
        $scope.InsClassID = null;
        $scope.InsSubClassID = null;
    };

    function GetAllCauseOfLoss() {
        $scope.showLoader = true;
        CauseOfLossService.GetAllCauseOfLosses().then(function (results) {
            $scope.showLoader = false;
            $scope.Cause = results.data;
            $scope.data = angular.copy($scope.Cause);
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
        $scope.data = filterFilter($scope.Cause, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };

});
