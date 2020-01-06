'use strict';

ibmsApp.controller("ClaimRejectReasonController", function ($scope, $http, ClaimRejectReasonService, $window, AuthService, filterFilter) {


    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
        });
    };

    $scope.Reject = [];
    GetAllClaimRejectReason();

    $scope.refreshContent = function () {
        GetAllClaimRejectReason();
        $scope.search_query = "";
    };

 
    $scope.init = function () {
        //$("#edit" + id).show();
        $scope.getCurrentUser();
        GetAllClaimRejectReason();
    };

    $scope.edit = function (Reject) {
        $("#view" + Reject.ClaimRejectReasonID).hide();
        $("#edit" + Reject.ClaimRejectReasonID).show();

    };

    $scope.Delete = function (Reject) {
        $scope.showLoader = true;
        SuccessDelete(Reject)
    };
    function SuccessDelete(Reject) {
        noty({
            text: 'Do you want to Delete Claim Reject Reason?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Yes', onClick: function ($noty) {
                            $noty.close();
                            Deletes(Reject);
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
    function Deletes(Reject) {
        var claimRejectReasonID = Reject.ClaimRejectReasonID;

        ClaimRejectReasonService.DeleteClaimRejectReason(claimRejectReasonID).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Claim Reject Reason Successfully Deleted',
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
                   $scope.refreshContent();
               }
               else {
                   noty({
                       text: 'Error in Deleteing Claim Reject Reason',
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

    $scope.update = function (Reject) {
        $scope.showLoader = true;
        $("#view" + Reject.ClaimRejectReasonID).show();
        $("#edit" + Reject.ClaimRejectReasonID).hide();
        SuccessUpdate(Reject);
    };
    function SuccessUpdate(Reject) {
        noty({
            text: 'Do you want to Update Claim Reject Reason?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Yes', onClick: function ($noty) {
                            $noty.close();
                            Update(Reject);
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
    function Update(Reject) {

        var claimRejectReasonID = Reject.ClaimRejectReasonID;
        var claimRejectReason = Reject.ClaimRejectReason;

        ClaimRejectReasonService.UpdateClaimRejectReason(
           claimRejectReasonID, claimRejectReason, $scope.currentUser.UserID).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Updated Claim Reject Reason',
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
                       text: 'There was an error while updating Claim Reject Reason.' + " " + results.message,
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
    $scope.cancel = function (Reject) {
        $("#view" + Reject.claimRejectReasonID).show();
        $("#edit" + Reject.claimRejectReasonID).hide();
        $scope.refreshContent();
    };


    $scope.save = function () {
        $scope.showLoader = true;
        Success();
    };
    function Success() {
        noty({
            text: 'Do you want to Save Claim Reject Reason?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Yes', onClick: function ($noty) {
                            $noty.close();
                            Save();
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

    function Save() {
        var claimRejectReason = $scope.ClaimRejectReason;

        ClaimRejectReasonService.SaveClaimRejectReason(claimRejectReason, $scope.currentUser.UserID).
               then(function (results) {
                   $scope.showLoader = false;

                   if (results.status === true) {
                       noty({
                           text: 'Successfully Saved Claim Reject Reason',
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
                           text: 'Error in Saving Claim Reject Reason.' + " " + results.message,
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

        $scope.ClaimRejectReason = null;
    };

    function GetAllClaimRejectReason() {
        $scope.showLoader = true;
        ClaimRejectReasonService.GetAllClaimRejectReasons().then(function (results) {
            $scope.showLoader = false;
            $scope.Reject = results.data;
            $scope.data = angular.copy($scope.Reject);
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
        $scope.data = filterFilter($scope.Reject, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };


    

});