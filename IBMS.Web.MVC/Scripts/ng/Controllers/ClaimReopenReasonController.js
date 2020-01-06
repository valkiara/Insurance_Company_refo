'use strict';


ibmsApp.controller("ClaimReopenReasonController", function ($scope, $http, ClaimReopenReasonService, $window, AuthService, filterFilter) {

    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
        });
    };


    $scope.Reopen = [];
    GetAllClaimReOpenReason();

    $scope.refreshContent = function () {
        GetAllClaimReOpenReason();
        $scope.search_query = "";
    };

    $scope.init = function () {
        $scope.getCurrentUser();
        GetAllClaimReOpenReason();
    };

    $scope.edit = function (Reopen) {
        $("#view" + Reopen.ClaimReOpenReasonID).hide();
        $("#edit" + Reopen.ClaimReOpenReasonID).show();

    };

    $scope.Delete = function (Reopen) {
        $scope.showLoader = true;
        SuccessDelete(Reopen)
    };
    function SuccessDelete(Reopen) {
        noty({
            text: 'Do you want to Delete Claim Re-Open Reason?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            Deletes(Reopen);
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
    function Deletes(Reopen) {

        var claimReOpenReasonID = Reopen.ClaimReOpenReasonID;

        ClaimReopenReasonService.DeleteClaimReOpenReason(claimReOpenReasonID).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Deleted Claim Re-Open Reason',
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
                       text: 'Error in Deleting Claim Re-Open Reason',
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

    $scope.update = function (Reopen) {
        $scope.showLoader = true;
        $("#view" + Reopen.ClaimReOpenReasonID).show();
        $("#edit" + Reopen.ClaimReOpenReasonID).hide();
        SuccessUpdate(Reopen);
    };
    function SuccessUpdate(Reopen) {
        noty({
            text: 'Do you want to Update Claim Re-Open Reason?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            Update(Reopen);
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
    function Update(Reopen) {

        var claimReOpenReasonID = Reopen.ClaimReOpenReasonID;
        var claimReOpenReason = Reopen.ClaimReOpenReason;

        ClaimReopenReasonService.UpdateClaimReOpenReason(
           claimReOpenReasonID, claimReOpenReason, $scope.currentUser.UserID).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Updated Claim Re-Open Reason',
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
                       text: 'Error updating Claim Re-Open Reason.' + " " + results.message,
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

    $scope.cancel = function (Reopen) {
        $("#view" + Reopen.ClaimReOpenReasonID).show();
        $("#edit" + Reopen.ClaimReOpenReasonID).hide();
        $scope.refreshContent();
    };

    $scope.save = function () {
        $scope.showLoader = true;
        Success();
    };
    function Success() {
        noty({
            text: 'Do you want to Save Claim Re-Open Reason?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            Save();
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

    function Save() {

        var claimReOpenReason = $scope.ClaimReOpenReason;

        ClaimReopenReasonService.SaveClaimReOpenReason(claimReOpenReason, $scope.currentUser.UserID).
               then(function (results) {
                   $scope.showLoader = false;

                   if (results.status === true) {
                       noty({
                           text: 'Successfully Saved Claim Re-Open Reason',
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
                           text: 'Error in Saving Claim Re-Open Reason.' + " " + results.message,
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

        $scope.ClaimReOpenReason = null;
    };


    function GetAllClaimReOpenReason() {
        $scope.showLoader = true;
        ClaimReopenReasonService.GetAllClaimReOpenReasons().then(function (results) {
            $scope.showLoader = false;
            $scope.Reopen = results.data;
            $scope.data = angular.copy($scope.Reopen);
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
        $scope.data = filterFilter($scope.Reopen, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };

});