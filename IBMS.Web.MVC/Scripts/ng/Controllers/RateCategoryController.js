'use strict';

ibmsApp.controller("RateCategoryController", function ($scope, $http, RateCategoryService, $location, AuthService, filterFilter) {
    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
        });
    };
    $scope.init = function () {
        $scope.rateObj = {};
        $scope.loadRateCategory();
        $scope.getCurrentUser();

    }

    $scope.refreshContent = function () {
        $scope.loadRateCategory();
        $scope.search_query = "";
    };

    $scope.ClearFields = function () {
        $scope.rateObj = {};
    };

    $scope.loadRateCategory = function () {

        $scope.showLoader = true;
        RateCategoryService.getAvailableRateCategory().then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.gridOptionsRate = results.data;
                var data = $scope.gridOptionsRate;

                $scope.data = angular.copy($scope.gridOptionsRate);
                $scope.viewby = "5";
                $scope.totalItems = $scope.data.length;
                $scope.currentPage = 1;
                $scope.itemsPerPage = $scope.viewby;
                $scope.maxSize = 5; //Number of pager buttons to show
                $scope.setItemsPerPage($scope.viewby);
            }
            else {
                $scope.gridOptionsRate = [];
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
        $scope.data = filterFilter($scope.gridOptionsRate, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };

    $scope.addRateCategory = function () {
        $scope.showLoader = true;
        Success();
    }
    function saveRATE(){
        RateCategoryService.saveRateCategory($scope.rateObj, $scope.currentUser).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true)
            {
                noty({
                    text: 'Successfully Saved Rate Category Details',
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
                    text: 'Error Saving Rate Category Details.' + " " + results.message,
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

    $scope.editRate = function (RATE) {
        $("#view" + RATE.RateCategoryID).hide();
        $("#edit" + RATE.RateCategoryID).show();



    };

    $scope.cancel = function (RATE) {
        //alert(id);
        $("#view" + RATE.RateCategoryID).show();
        $("#edit" + RATE.RateCategoryID).hide();
        $scope.refreshContent();
    };
    $scope.update = function (RATE) {
        $scope.showLoader = true;
        $("#view" + RATE.RateCategoryID).show();
        $("#edit" + RATE.RateCategoryID).hide();
        //Update(RATE);
        SuccessUpdate(RATE);
    };

    function Update(RATE) {


        var rateCategoryID = RATE.RateCategoryID;
        var rateCategoryName = RATE.RateCategoryName;
        var userID = $scope.currentUser.UserID;

        RateCategoryService.updateRATE(
           rateCategoryID, rateCategoryName, userID
           ).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Updated Rate Category Details',
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
                       text: 'Error Update Rate Category Details.' + " " + results.message,
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

    $scope.deleteRate = function (RATEID)
    {
        $scope.showLoader = true;
        SuccessDelete(RATEID);
    }
    function Delete(RATEID) {
    RateCategoryService.deleteRATE(RATEID).then(function (results) {
        $scope.showLoader = false;
        if (results.status === true) {
            noty({
                text: 'Successfully Deleted Rate Category Details',
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
                text: 'Error Deleteing Rate Category Details',
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
            text: 'Do you want to Save Rate Category Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            saveRATE();
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
    }

    function SuccessUpdate(RATE) {
        //alert("cc");
        noty({
            text: 'Do you want to Update Rate Category Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            Update(RATE)

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
    }

    function SuccessDelete(RATEID) {
        //alert("cc");
        noty({
            text: 'Are you sure want to Delete Rate Category Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            Delete(RATEID);

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
    }

});