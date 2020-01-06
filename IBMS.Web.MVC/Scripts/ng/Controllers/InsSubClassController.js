'use strict';

ibmsApp.controller("InsSubClassController", function ($scope, $http, InsSubClassService, $location, AuthService,filterFilter) {
    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
            $scope.loadInsSubClass();
        });
    };
    $scope.init = function () {
        $scope.getCurrentUser();
        $scope.insObj = {};
        $scope.loadInsClass();
        $scope.loadInsSubClass();
        $scope.one = true;
    }

    $scope.dropDownConfig = {
        maxItems: 1,
        create: function (input) {
            return {
                value: "new-" + input,
                text: input
            }
        }
    };

    $scope.refreshContent = function () {
        $scope.loadInsSubClass();
        $scope.search_query = "";
    };

    $scope.ClearFields = function () {
        $scope.insObj = {};
    };

    $scope.loadInsClass = function () {
        $scope.showLoader = true;
        InsSubClassService.getAvailableInsuranceDropdown().then(function (results) {
            //$scope.showLoader = false;
            // $scope.isCompanyLoaded = true;

            if (results.status === true) {
                $scope.availableInsClass = results.data;
            }
            else {
                $scope.availableInsClass = [];
            }

            
        });
    };

    $scope.loadInsSubClass = function () {
        $scope.showLoader = true;
        InsSubClassService.getAvailableInsSubClass($scope.currentUser.BusinessUnitID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.gridOptionsINS = results.data;
                var data = $scope.gridOptionsINS;

                $scope.data = angular.copy($scope.gridOptionsINS);
                $scope.viewby = "5";
                $scope.totalItems = $scope.data.length;
                $scope.currentPage = 1;
                $scope.itemsPerPage = $scope.viewby;
                $scope.maxSize = 5; //Number of pager buttons to show
                $scope.setItemsPerPage($scope.viewby);
            }
            else {
                $scope.gridOptionsINS = [];
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
        $scope.data = filterFilter($scope.gridOptionsINS, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };

    $scope.Success = function () {
        //alert("cc");
        noty({
            text: 'Do you want to Save Insurance Sub Class Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            saveINS();
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

    $scope.SuccessUpdate = function (INS) {
        //alert("cc");
        noty({
            text: 'Do you want to Update Insurance Sub Class Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            Update(INS)

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

    $scope.SuccessDelete = function (INSID) {
        //alert("cc");
        noty({
            text: 'Are you sure want to Delete Insurance Sub Class Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            Delete(INSID);

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

    $scope.addInsuarance = function () {
        $scope.showLoader = true;
        $scope.Success();
        //SuccessUpdate(INS)
       //saveINS();
    }

    function saveINS(){

        InsSubClassService.saveInsurance($scope.insObj, $scope.currentUser).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) 
            {
                noty({
                    text: 'Successfully Saved Insurance Sub Class Details',
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
                    text: 'Error Saving Insurance Sub Class Details',
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

    $scope.editINS = function (INS) {
        $("#view" + INS.InsuranceSubClassID).hide();
        $("#edit" + INS.InsuranceSubClassID).show();



    };

    $scope.cancel = function (INS) {
        //alert(id);
        $("#view" + INS.InsuranceSubClassID).show();
        $("#edit" + INS.InsuranceSubClassID).hide();
        $scope.refreshContent();
    };
    $scope.update = function (INS) {
        $scope.showLoader = true;
        $("#view" + INS.InsuranceSubClassID).show();
        $("#edit" + INS.InsuranceSubClassID).hide();
        $scope.SuccessUpdate(INS);
       // Update(INS);
    };

    function Update(INS) {


        var insSubClassID = INS.InsuranceSubClassID;
        var insClassID = INS.InsuranceClassID;
        var IsActive = INS.IsActive;
        var description = INS.Description;
        var userID = $scope.currentUser.UserID




        InsSubClassService.updateINS(
           insSubClassID, insClassID, description, IsActive, userID
           ).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Updated Insurance Sub Class Details',
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
                       text: 'Error Update Insurance Sub Class Details',
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

    $scope.deleteINS = function (INSID) {
        $scope.showLoader = true;
        $scope.SuccessDelete(INSID);
        //Delete(INSID);
    }
    function Delete(INSID){
        InsSubClassService.deleteINS(INSID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                if (results.status === true) {
                    noty({
                        text: 'Successfully Deleted Insurance Sub Class Details',
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
                        text: 'Error Deleteing Insurance Sub Class Details',
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
            };
        });


        //$scope.Success = function () {
        //    //alert("cc");
        //    noty({
        //        text: 'Do you want to Save Insurance SubClass Details?',
        //        layout: 'topCenter',
        //        buttons: [
        //                {
        //                    addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
        //                        $noty.close();
        //                        saveINS();
        //                        //noty({ text: 'You clicked "Ok" button', layout: 'topRight', type: 'success' });
        //                    }
        //                },
        //                {
        //                    addClass: 'btn btn-danger btn-clean', text: 'Cancel', onClick: function ($noty) {
        //                        $noty.close();
        //                        $scope.$apply(function () {
        //                            $scope.showLoader = false;
        //                        });

        //                        //noty({ text: 'You clicked "Cancel" button', layout: 'topRight', type: 'error' });
        //                    }
        //                }
        //        ]
        //    })
        //}

        

    };
});