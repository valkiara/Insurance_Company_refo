'use strict';

ibmsApp.controller("BusinessUnitController", function ($scope, $http, $rootScope, BusinessUnitService, $location, AuthService,filterFilter) {

    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
        });
    };

    $scope.init = function () {
        $scope.getCurrentUser();
        $scope.buObj = {};
        $scope.loadCompany();
        $scope.loadBusinessUnit();
        $scope.one = true;
    };

    $scope.refreshContent = function () {
        $scope.loadBusinessUnit();
        $scope.search_query = "";
    };
    $scope.ClearFields = function () {
        $scope.buObj = {};
    };

    $scope.companyChange = function (companyID) {
        for (var i = 0; i < $scope.availableCompany.length; i++) {
            if ($scope.availableCompany[i].CompanyID.toString() === companyID) {
                $scope.companyName = $scope.availableCompany[i].companyName;
                break;
            }
        }
    };

    $scope.loadCompany = function () {
        $scope.showLoader = true;
        BusinessUnitService.getAvailableCompanyDropdown().then(function (results) {
            //$scope.showLoader = false;
            $scope.isCompanyLoaded = true;

            if (results.status === true) {
                $scope.availableCompany = results.data;
            }
            else {
                $scope.availableCompany = [];
            }

            if ($scope.isCompanyLoaded) {
                $scope.companyChange($scope.buObj.CompanyID);
            }
        });
    };

    $scope.loadBusinessUnit = function () {
        $scope.showLoader = true;
        BusinessUnitService.getAvailableBusinessUnit().then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.gridOptionsBusinessUnit = results.data;
                var data = $scope.gridOptionsBusinessUnit;
                $scope.data = angular.copy($scope.gridOptionsBusinessUnit);
                $scope.viewby = "5";
                $scope.totalItems = $scope.data.length;
                $scope.currentPage = 1;
                $scope.itemsPerPage = $scope.viewby;
                $scope.maxSize = 5; //Number of pager buttons to show

                $scope.setItemsPerPage($scope.viewby);
            }
            else {
                $scope.gridOptionsBusinessUnit = [];
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
        $scope.currentPage = 1;
    };
    $scope.searchTextChange = function (searchText) {
        $scope.data = filterFilter($scope.gridOptionsBusinessUnit, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };
    $scope.addBusinessUnit = function () {
        $scope.showLoader = true;
        Success();
    }
    function saveBU() {

        BusinessUnitService.saveBusinessUnit($scope.buObj, $scope.currentUser).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                noty({
                    text: 'Successfully Saved Business Unit Details',
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
                    text: 'Error Saving Business Unit Details.' + " " + results.message,
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

    $scope.editBU = function (BU) {
        $("#view" + BU.BusinessUnitID).hide();
        $("#edit" + BU.BusinessUnitID).show();
    };

    $scope.cancel = function (BU) {
        //alert(id);
        $("#view" + BU.BusinessUnitID).show();
        $("#edit" + BU.BusinessUnitID).hide();
        $scope.refreshContent();
    };

    $scope.update = function (BU) {
        $("#view" + BU.BusinessUnitID).show();
        $("#edit" + BU.BusinessUnitID).hide();
        $scope.showLoader = true;
        SuccessUpdate(BU);
    };

    function Update(BU) {
        var businessUnitID = BU.BusinessUnitID;
        var businessUnit = BU.BusinessUnit;
        var IsActive = BU.IsActive;
        var ComapnyID = BU.CompanyID;

        BusinessUnitService.updateBU(
           businessUnitID, businessUnit, ComapnyID, IsActive, $scope.currentUser.UserID
           ).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Updated Business Unit Details',
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
                       text: 'Error Update Business Unit Details.' + " " + results.message,
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
                  // noty({ text: 'Error Update Business Unit Details', layout: 'topCenter', type: 'error' });
               }
           });

    };

    $scope.deleteBU = function (BUID) {
        $scope.showLoader = true;
        SuccessDelete(BUID);
    };

    function Delete(BUID) {
        BusinessUnitService.deleteBU(BUID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                noty({
                    text: 'Successfully Deleted Business Unit Details',
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
                    text: 'Error Deleteing Business Unit Details',
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
            text: 'Do you want to Save Business Unit Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            saveBU();
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

    function SuccessUpdate(BU) {
        //alert("cc");
        noty({
            text: 'Do you want to Update Business Unit Details?',
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
    }

    function SuccessDelete(BUID) {
        //alert("cc");
        noty({
            text: 'Are you sure want to Delete Business Unit Details?',
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
    }

});