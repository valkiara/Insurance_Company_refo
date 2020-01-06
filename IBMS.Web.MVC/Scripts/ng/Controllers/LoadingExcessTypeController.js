'use strict';

ibmsApp.controller("LoadingExcessTypeController", function ($scope, $http, LoadingExcessTypeService, $window, AuthService,filterFilter) {

    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
        });
    };


    $scope.Loading = [];
   GetAllLoadingType();

    $scope.refreshContent = function () {
        GetAllLoadingType();
        $scope.search_query = "";
    };

    $scope.ClearFields = function () {
        //$scope.companyID = null;
        //$scope.PaymentTypeName = null;
        $scope.Description = null;
    };
 
    $scope.init = function () {
        //$("#edit" + id).show();
        $scope.getCurrentUser();
        GetAllLoadingType();
    };

    $scope.edit = function (Loading) {
        $("#view" + Loading.LoadingTypeID).hide();
        $("#edit" + Loading.LoadingTypeID).show();

    };

    $scope.Delete = function (Loading) {
        $scope.showLoader = true;
        SuccessDelete(Loading)
    };
    function SuccessDelete(Loading) {
        noty({
            text: 'Do you want to Delete Loading Type Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            Deletes(Loading);
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
    function Deletes(Loading) {
        var loadingTypeID = Loading.LoadingTypeID;

        LoadingExcessTypeService.DeleteLoadingType(loadingTypeID).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Deleted Loading Type Details',
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
                       text: 'Error Deleting Loading Type Details' + ' / ' + results.message,
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

    $scope.update = function (Loading) {
        $scope.showLoader = true;
        $("#view" + Loading.LoadingTypeID).show();
        $("#edit" + Loading.LoadingTypeID).hide();
        SuccessUpdate(Loading);
    };
    function SuccessUpdate(Loading) {
        noty({
            text: 'Do you want to Update Loading Type Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            Update(Loading);
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
    function Update(Loading) {

        var loadingTypeID = Loading.LoadingTypeID;
        var description = Loading.Description;

        LoadingExcessTypeService.UpdateLoadingType(
           loadingTypeID, description, $scope.currentUser.UserID).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Updated Loading Type Details',
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
                       text: 'Error updating Loading Type Details' + ' / ' + results.message,
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
    $scope.cancel = function (Loading) {
        $("#view" + Loading.LoadingTypeID).show();
        $("#edit" + Loading.LoadingTypeID).hide();
        $scope.refreshContent();
    };


    $scope.save = function () {
        $scope.showLoader = true;
        Success();
    };
    function Success() {
        noty({
            text: 'Do you want to Save Loading Type Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            Save();
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

    function Save() {
        var description = $scope.Description;

        LoadingExcessTypeService.SaveLoadingType(description, $scope.currentUser.UserID).
               then(function (results) {
                   $scope.showLoader = false;

                   if (results.status === true) {
                       noty({
                           text: 'Successfully Saved Loading Type Details',
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
                           text: 'Error Saving Loading Type Details' + ' / ' + results.message,
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

   
    function GetAllLoadingType() {
        $scope.showLoader = true;
        LoadingExcessTypeService.GetAllLoadingTypes().then(function (results) {
            $scope.showLoader = false;
            $scope.Loading = results.data;
            $scope.data = angular.copy($scope.Loading);
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
        $scope.data = filterFilter($scope.Loading, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };

    
});