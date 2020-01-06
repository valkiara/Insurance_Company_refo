'use strict';

ibmsApp.controller("CommissionStructureLineController", function ($scope, $http, CommissionStructureLineService, $location, AuthService,filterFilter) {

    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
            $scope.loadCommissionStrctureLine();
        });
    };

    $scope.refreshContent = function () {
        $scope.loadCommissionStrctureLine();
        $scope.search_query = "";
    };

    $scope.ClearFields = function () {
        $scope.comObj = {};
    };
    $scope.checkInputRateByValue = function () {
        if ($scope.comObj.RateValue > 100) {
            $scope.comObj.RateValue = $scope.comObj.RateValue / 10;
        }
    };
    $scope.init = function () {
        $scope.getCurrentUser();
        $scope.comObj = {};
        $scope.loadCommissionStrcture();
        $scope.loadRateCategory();
        //$scope.loadCommissionStrctureLine();
        $scope.one = true;
    };

    $scope.loadCommissionStrcture = function () {
        CommissionStructureLineService.getAvailableCommisionStructure().then(function (results) {
            if (results.status === true) {
                $scope.availableComStrct = results.data;
            }
            else {
                $scope.availableComStrct = [];
            }
        });
    };

    $scope.loadRateCategory = function () {
        CommissionStructureLineService.getAvailableRateCategory().then(function (results) {
            if (results.status === true) {
                $scope.availableRateCat = results.data;
            }
            else {
                $scope.availableRateCat = [];
            }
        });
    };

    $scope.loadCommissionStrctureLine = function () {
        $scope.showLoader = true;
        CommissionStructureLineService.getAvailableCommissionStructureHeader($scope.currentUser.BusinessUnitID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.comStructLineData = results.data;
                //var data = $scope.comStructLineData;

                $scope.data = angular.copy($scope.comStructLineData);

                for (var i = 0; i < $scope.data.length; i++) {
                    $scope.data[i].RowNumber = i;
                    $scope.ageChange($scope.data[i].IsAgeConsider, $scope.data[i].AgeFrom, $scope.data[i].AgeTo, $scope.data[i].RowNumber);
                }

                $scope.viewby = "5";
                $scope.totalItems = $scope.data.length;
                $scope.currentPage = 1;
                $scope.itemsPerPage = $scope.viewby;
                $scope.maxSize = 5; //Number of pager buttons to show
                $scope.setItemsPerPage($scope.viewby);
            }
            else {
                $scope.comStructLineData = [];
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
    };

    $scope.searchTextChange = function (searchText) {
        $scope.data = filterFilter($scope.comStructLineData, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };

    $scope.addCommisionLine = function () {
        $scope.showLoader = true;
        Success();
    };

    function savecom(){
        CommissionStructureLineService.addComStructLine($scope.comObj, $scope.currentUser).then(function (results) {
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

    $scope.addCommisionHeader = function () {
        $scope.activateNewClientRequestTab();
        $scope.showLoader = true;
        

    };

    $scope.activateNewClientRequestTab = function () {
        $("#list-tab-1").removeClass('active');
        $("#tab-first").removeClass('tab-pane active');

        $("#list-tab-2").addClass('active');
        $("#tab-2").addClass('tab-pane active');

        $("#tab-1").css("display", "none");
        $("#tab-2").css("display", "block");
    };


    $scope.editCOM = function (CSL) {
        $("#view" + CSL.CommisionStructureLineID).hide();
        $("#edit" + CSL.CommisionStructureLineID).show();
    };

    $scope.cancel = function (CSL) {
        $("#view" + CSL.CommisionStructureLineID).show();
        $("#edit" + CSL.CommisionStructureLineID).hide();
        $scope.refreshContent();
    };

    $scope.update = function (CSL) {
        $("#view" + CSL.CommisionStructureLineID).show();
        $("#edit" + CSL.CommisionStructureLineID).hide();
        $scope.showLoader = true;
        SuccessUpdate(CSL);
    };

    function Update(CSL) {
        var ComStructLineID = CSL.CommisionStructureLineID;
        var ComStrctID = CSL.CommisionStructureID;
        var rateCategoryID = CSL.RateCategoryID;
        var isAgeConsider = CSL.IsAgeConsider;
        var AgeFrom = CSL.AgeFrom;
        var AgeTo = CSL.AgeTo;
        var isFixed = CSL.IsFixed;
        var RateValue = CSL.RateValue;

        CommissionStructureLineService.updateCSL(
           ComStructLineID, ComStrctID, rateCategoryID, isAgeConsider, AgeFrom, AgeTo, isFixed, RateValue, $scope.currentUser.UserID
           ).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Updated Commission Structure Line Details',
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
                       text: 'Error Update Commission Structure Line Details',
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

    $scope.deleteCSL = function (CSLID) {
        $scope.showLoader = true;
        SuccessDelete(CSLID)
    };

    function Deletecom(CSLID) {
        CommissionStructureLineService.deleteCSL(CSLID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                noty({
                    text: 'Successfully Deleted Commission Structure Line Details',
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
                $scope.refreshContent();
            }
            else {
                noty({
                    text: 'Error Deleteing Commission Structure Line Details',
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

    function SuccessUpdate(CSL) {
        //alert("cc");
        noty({
            text: 'Do you want to Update Commission Structure Line Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            Update(CSL);

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

    function SuccessDelete(CSLID) {
        //alert("cc");
        noty({
            text: 'Are you sure want to Delete Commission Structure Line Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            Deletecom(CSLID);

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

    /*---------------------------Age Validation-----------------------------*/
    $scope.ageChange = function (ageChecked, ageFrom, ageTo, idx) {
        if (ageChecked === true) {
            if (ageFrom) {
                $scope.data[idx].isValidAgeFrom = true;
            }
            else {
                $scope.data[idx].isValidAgeFrom = false;
            }

            if (ageTo) {
                $scope.data[idx].isValidAgeTo = true;
            }
            else {
                $scope.data[idx].isValidAgeTo = false;
            }

            if (ageFrom && ageTo && ageFrom < ageTo) {
                $scope.data[idx].isValidAgeRange = true;
            }
            else {
                $scope.data[idx].isValidAgeRange = false;
            }
        }
        else {
            $scope.data[idx].isValidAgeFrom = true;
            $scope.data[idx].isValidAgeTo = true;
            $scope.data[idx].isValidAgeRange = true;
        }
    };
    /*---------------------------Age Validation-----------------------------*/
});