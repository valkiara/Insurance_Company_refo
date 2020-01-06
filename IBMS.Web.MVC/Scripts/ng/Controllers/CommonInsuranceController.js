'use strict';

ibmsApp.controller("CommonInsuranceController", function ($scope, $http, CommonInsuranceService, $location, AuthService, filterFilter) {
    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
            $scope.getAllCommonInsurance();
        });
    };
    $scope.init = function () {
        $scope.buObj1 = {};
        $scope.getAllInsuranceClass();
        //$scope.getAllInsuranceSubClass();
        //$scope.getAllCommonInsurance();
        $scope.getCurrentUser();

        $scope.one = true;
    };

    $scope.refreshContent = function () {
        $scope.getAllCommonInsurance();
        $scope.search_query = "";
    };

    $scope.ClearFields = function () {
        $scope.buObj1 = {};
    };

    $scope.getAllInsuranceClass = function () {
        $scope.showLoader = true;
        CommonInsuranceService.getAvailableInsurance().then(function (results) {
            $scope.showLoader = false;
            $scope.isInsuranceClassLoaded = true;

            if (results.status === true) {
                $scope.availableInsuranceClass = results.data;
            }
            else {
                $scope.availableInsuranceClass = [];
            }

            if ($scope.isInsuranceClassLoaded) {
                $scope.InsuranceClassChange($scope.buObj1.InsClassID);
            }
        });
    };

    $scope.getAllInsuranceSubClassByInsClass = function (insClassID) {
        $scope.showLoader = true;
        CommonInsuranceService.getAvailableInsSubClassByInsClass(insClassID).then(function (results) {
            $scope.showLoader = false;
            $scope.isInsuranceSubClassLoaded = true;

            if (results.status === true) {
                $scope.availableInsuranceSubClass = results.data;
            }
            else {
                $scope.availableInsuranceSubClass = [];
            }

            if ($scope.isInsuranceSubClassLoaded) {
                $scope.InsuranceSubClassChange($scope.buObj1.InsSubClassID);
            }
        });
    };

    $scope.InsuranceClassChange = function (InsuranceClassID) {
        for (var i = 0; i < $scope.availableInsuranceClass.length; i++) {
            if ($scope.availableInsuranceClass[i].InsuranceClassID.toString() === InsuranceClassID) {
                $scope.Insurancecode = $scope.availableInsuranceClass[i].Insurancecode;
                break;
            }
        }
    };

    $scope.InsuranceSubClassChange = function (InsuranceSubClassID) {
        for (var i = 0; i < $scope.availableInsuranceSubClass.length; i++) {
            if ($scope.availableInsuranceSubClass[i].InsuranceSubClassID.toString() === InsuranceSubClassID) {
                $scope.Description = $scope.availableInsuranceSubClass[i].Description;
                break;
            }
        }
    };

    $scope.addCommonInsurance = function () {
        $scope.showLoader = true;
        Success();
    };

    function saveCommonInsurance() {
        CommonInsuranceService.saveCommonInsurance($scope.buObj1, $scope.currentUser).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                noty({
                    text: 'Successfully Saved Common Insurance Details',
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
                    text: 'Error Saving Common Insurance Details',
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

    $scope.editCI = function (commonInsScope) {
        $("#view" + commonInsScope.CommonInsuranceScopeID).hide();
        $("#edit" + commonInsScope.CommonInsuranceScopeID).show();
        $scope.getAllInsuranceSubClassByInsClass(commonInsScope.InsuranceClassID);
    };

    $scope.cancel = function (buObj1) {
        $("#view" + buObj1.CommonInsuranceScopeID).show();
        $("#edit" + buObj1.CommonInsuranceScopeID).hide();
        $scope.refreshContent();

    };
    $scope.updateCI = function (buObj1) {
        $scope.showLoader = true;
        $("#view" + buObj1.CommonInsuranceScopeID).show();
        $("#edit" + buObj1.CommonInsuranceScopeID).hide();
        SuccessUpdate(buObj1);
    };

    $scope.refresh = function () {
        getAllCommonInsurance();
    };

    $scope.getAllCommonInsurance = function () {
        $scope.showLoader = true;
        CommonInsuranceService.getAvailableCommonInsurance($scope.currentUser.BusinessUnitID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.availableCommonInsurance = results.data;
                var data = $scope.availableCommonInsurance;

                $scope.data = angular.copy($scope.availableCommonInsurance);
                $scope.viewby = "5";
                $scope.totalItems = $scope.data.length;
                $scope.currentPage = 1;
                $scope.itemsPerPage = $scope.viewby;
                $scope.maxSize = 5; //Number of pager buttons to show
                $scope.setItemsPerPage($scope.viewby);
            }
            else {
                $scope.availableCommonInsurance = [];
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
        $scope.data = filterFilter($scope.availableCommonInsurance, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };

    function UpdateCI(buObj1) {
        var CommonInsuranceScopeID = buObj1.CommonInsuranceScopeID;
        var Description = buObj1.Description;
        var InsuranceClassID = buObj1.InsuranceClassID;
        var InsuranceSubClassID = buObj1.InsuranceSubClassID;
        var userID = $scope.currentUser.UserID;

        CommonInsuranceService.updateCommonInsurance(CommonInsuranceScopeID, Description, InsuranceClassID, InsuranceSubClassID, userID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                noty({ 
                    text: 'Successfully Updated Common Insurance Details',
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
                    text: 'Error Update Common Insurance Details',
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


    $scope.deleteCI = function (BUID) {
        $scope.showLoader = true;
        SuccessDelete(BUID);
    };

    function deleteCommonInsurance(BUID) {
        CommonInsuranceService.deleteCommonInsurance(BUID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                noty({
                    text: 'Successfully Deleted Common Insurance Details',
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
                    text: 'Error Deleting Common Insurance Details',
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
            text: 'Do you want to Save Common Insurance Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            saveCommonInsurance();
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
            text: 'Do you want to Update Common Insurance Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            UpdateCI(BU)

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
            text: 'Are you sure want to Delete Common Insurance Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            deleteCommonInsurance(BUID);

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