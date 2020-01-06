'use strict';

ibmsApp.controller("DesignationController", function ($scope, $http, DesignationService, $location, AuthService,filterFilter) {
    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
            $scope.businessUnitID = results.BusinessUnitID;
            $scope.desObj.BusinessUnit = results.BusinessUnitID;
            $scope.loadBU();
            $scope.loadDesignation();
        });
    };

    $scope.init = function () {
       // $scope.desObj.BusinessUnit = $scope.currentUser.BusinessUnitID;
        $scope.desObj = {};
      //  $scope.loadBU();
        //$scope.loadDesignation();
        $scope.filteredBusinessUnits = [];
        $scope.getCurrentUser();
        $scope.one = true;
    };

    $scope.refreshContent = function () {
        $scope.loadDesignation();
        $scope.search_query = "";
    };

    $scope.ClearFields = function () {
        $scope.desObj = {};
    };

    $scope.buChange = function (companyID) {
        for (var i = 0; i < $scope.availableCompany.length; i++) {
            if ($scope.availableCompany[i].CompanyID.toString() === companyID) {
                $scope.companyName = $scope.availableCompany[i].companyName;
                break;
            }
        }
    };

    $scope.loadBU = function () {
     $scope.showLoader = true;
     if ($scope.currentUser.AccessLevelTypeName == "Admin") {
         DesignationService.getAvailableBUDropdown().then(function (results) {

             if (results.status === true) {
                 $scope.filteredBusinessUnits = results.data;
                 //for (var i = 0; i < $scope.availableBU.length; i++) {
                 //    if ($scope.availableBU[i].BusinessUnitID === $scope.currentUser.BusinessUnitID) {
                 //        $scope.filteredBusinessUnits.push({ "BusinessUnitID": $scope.availableBU[i].BusinessUnitID, "BusinessUnit": $scope.availableBU[i].BusinessUnit });
                 //        break;
                 //    }
                 //}

             }

         });
     }
     else {
           $scope.filteredBusinessUnits = [];
        $scope.filteredBusinessUnits.push({
            "BusinessUnitID": $scope.currentUser.BusinessUnitID,
            "BusinessUnit": $scope.currentUser.BusinessUnitName

        });
     }

       
        
    };

    $scope.loadDesignation = function () {
        $scope.showLoader = true;
        DesignationService.getAvailableDesignation($scope.currentUser.BusinessUnitID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.gridOptionsDesignation = results.data;
                var data = $scope.gridOptionsDesignation;

                $scope.data = angular.copy($scope.gridOptionsDesignation);
                $scope.viewby = "5";
                $scope.totalItems = $scope.data.length;
                $scope.currentPage = 1;
                $scope.itemsPerPage = $scope.viewby;
                $scope.maxSize = 5; //Number of pager buttons to show
                $scope.setItemsPerPage($scope.viewby);
            }
            else {
                $scope.gridOptionsDesignation = [];
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
        $scope.data = filterFilter($scope.gridOptionsDesignation, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };
    $scope.addDesignation = function ()
    {
        $scope.showLoader = true;
        Success();
    };

    function saveDes(){
        DesignationService.saveDesignation($scope.desObj, $scope.currentUser).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                noty({
                    text: 'Successfully Saved Designation Details',
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
                    text: 'Error Saving Designation Details.' + " " + results.message,
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

    $scope.editDES = function (DES) {
        $("#view" + DES.DesignationID).hide();
        $("#edit" + DES.DesignationID).show();
    };

    $scope.cancel = function (Des) {
        //alert(id);
        $("#view" + Des.DesignationID).show();
        $("#edit" + Des.DesignationID).hide();
        $scope.refreshContent();
    };
    $scope.update = function (Des) {
        $scope.showLoader = true;
        $("#view" + Des.DesignationID).show();
        $("#edit" + Des.DesignationID).hide();
        SuccessUpdate(Des);
       // Update(Des);
    };

    function Update(Des) {
        var designationID = Des.DesignationID;
        var designation = Des.DesignationName;
        var businessUnitID = Des.BusinessUnitID;

        DesignationService.updateDES(
           designationID, designation, businessUnitID, $scope.currentUser.UserID
           ).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Updated Designation Details',
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
                       text: 'Error Update Designation Details.' + " " + results.message,
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

    $scope.deleteDES = function (DESID) {
        $scope.showLoader = true;
        SuccessDelete(DESID)
    }
    function DeleteDes(DESID) {
        DesignationService.deleteDES(DESID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                noty({
                    text: 'Successfully Deleted Designation Details',
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
                    text: 'Error Deleting Designation Unit Details',
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
            text: 'Do you want to Save Designation Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            saveDes();
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

    function SuccessUpdate(Des) {
        //alert("cc");
        noty({
            text: 'Do you want to Update Designation Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            Update(Des);

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

    function SuccessDelete(DESID) {
        //alert("cc");
        noty({
            text: 'Are you sure want to Delete Designation Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            DeleteDes(DESID);

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