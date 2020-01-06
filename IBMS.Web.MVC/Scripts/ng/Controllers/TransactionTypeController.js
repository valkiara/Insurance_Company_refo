'use strict';

ibmsApp.controller("TransactionTypeController", function ($scope, $http, TransactionTypeService, $location, AuthService, filterFilter) {
    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
            $scope.businessUnitID = results.BusinessUnitID;
            $scope.traObj.BusinessUnitID = results.BusinessUnitID;
            $scope.loadBU();
            $scope.loadTransaction();
        });
    };
    $scope.init = function () {
        $scope.traObj = {};
       // $scope.loadTransaction();
        $scope.filteredBusinessUnits = [];
       // $scope.loadBU();
        $scope.getCurrentUser();
       
    }
    $scope.refreshContent = function () {
        $scope.loadTransaction();
        $scope.search_query = "";
    };
    $scope.ClearFields = function () {
        $scope.traObj = {};
    };
    $scope.loadTransaction = function () {
        
        $scope.showLoader = true;
        TransactionTypeService.getAvailableTransaction($scope.currentUser.BusinessUnitID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.gridOptionsTRA = results.data;
                var data = $scope.gridOptionsTRA;

                $scope.data = angular.copy($scope.gridOptionsTRA);
                $scope.viewby = "5";
                $scope.totalItems = $scope.data.length;
                $scope.currentPage = 1;
                $scope.itemsPerPage = $scope.viewby;
                $scope.maxSize = 5; //Number of pager buttons to show
                $scope.setItemsPerPage($scope.viewby);
            }
            else {
                $scope.gridOptionsTRA = [];
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
        $scope.data = filterFilter($scope.gridOptionsTRA, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };

    $scope.loadBU = function () {

        if ($scope.currentUser.AccessLevelTypeName == "Admin") { 
            $scope.showLoader = true;
            TransactionTypeService.getAvailableBUDropdown().then(function (results) {
                $scope.showLoader = false;
                //$scope.isCompanyLoaded = true;

                if (results.status === true) {
                    $scope.availableBU = results.data;
                    if (results.status === true) {
                        $scope.filteredBusinessUnits = results.data;
                        //for (var i = 0; i < $scope.availableBU.length; i++) {
                        //    if ($scope.availableBU[i].BusinessUnitID === $scope.currentUser.BusinessUnitID) {
                        //        $scope.filteredBusinessUnits.push({ "BusinessUnitID": $scope.availableBU[i].BusinessUnitID, "BusinessUnit": $scope.availableBU[i].BusinessUnit });
                        //        break;
                        //    }
                        //}

                    }

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

    $scope.addTransactionType = function () {
        $scope.showLoadeTransactionTyper = true;
        Success();
        }
    function saveDOC(){
        TransactionTypeService.saveTransactionType($scope.traObj, $scope.currentUser).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true)
            {
                noty({
                    text: 'Successfully Saved Transaction Type Details',
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
                    text: 'Error Saving Transaction Type Details.' + " " + results.message,
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

    $scope.editTRA = function (DOC) {
        $("#view" + DOC.TransactionTypeID).hide();
        $("#edit" + DOC.TransactionTypeID).show();



    };

    $scope.cancel = function (DOC) {
        //alert(id);
        $("#view" + DOC.TransactionTypeID).show();
        $("#edit" + DOC.TransactionTypeID).hide();
        $scope.refreshContent();
    };
    $scope.update = function (DOC) {
        $("#view" + DOC.TransactionTypeID).show();
        $("#edit" + DOC.TransactionTypeID).hide();
        SuccessUpdate(DOC);
        //Update(DOC);
    };

    function Update(DOC) {


        var transactionTypeID = DOC.TransactionTypeID;
        var transactionType = DOC.Description;
        var businessunitID = DOC.BusinessUnitID;
        var userID = $scope.currentUser.UserID
       
        TransactionTypeService.updateTRA(
           transactionTypeID, transactionType, businessunitID, userID
           ).
           then(function (results) {
               if (results.status === true) {
                   noty({
                       text: 'Successfully Updated Transaction Type Details',
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
                       text: 'Error Update Transaction Type Details.' + " " + results.message,
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
                 //  noty({ text: 'Error Update Designation Details', layout: 'topCenter', type: 'error' });
                 $scope.ClearFields();
                   $scope.refreshContent();
               }
           });

    };

    $scope.deleteTRA = function (DOCID) {
        $scope.showLoader = true;
        SuccessDelete(DOCID);
    }
    function Delete(DOCID){
        TransactionTypeService.deleteTRA(DOCID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                noty({
                    text: 'Successfully Deleted Transaction Type Details',
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
                    text: 'Error Deleting Transaction Type Details',
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
            text: 'Do you want to Save Transaction Type Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            saveDOC();
                            //noty({ text: 'You clicked "Ok" button', layout: 'topRight', type: 'success' });
                        }
                    },
                    {
                        addClass: 'btn btn-danger btn-clean', text: 'Cancel', onClick: function ($noty) {
                            $noty.close();
                            //noty({ text: 'You clicked "Cancel" button', layout: 'topRight', type: 'error' });
                        }
                    }
            ]
        })
    }

    function SuccessUpdate(DOC) {
        //alert("cc");
        noty({
            text: 'Do you want to Update Transaction Type Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            Update(DOC)

                            //noty({ text: 'You clicked "Ok" button', layout: 'topRight', type: 'success' });
                        }
                    },
                    {
                        addClass: 'btn btn-danger btn-clean', text: 'Cancel', onClick: function ($noty) {
                            $noty.close();
                            $scope.refreshContent();
                            //noty({ text: 'You clicked "Cancel" button', layout: 'topRight', type: 'error' });
                        }
                    }
            ]
        })
    }

    function SuccessDelete(DOCID) {
        //alert("cc");
        noty({
            text: 'Are you sure want to Delete Transaction Type Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            Delete(DOCID);

                            //noty({ text: 'You clicked "Ok" button', layout: 'topRight', type: 'success' });
                        }
                    },
                    {
                        addClass: 'btn btn-danger btn-clean', text: 'Cancel', onClick: function ($noty) {
                            $noty.close();
                            $scope.refreshContent();
                            //noty({ text: 'You clicked "Cancel" button', layout: 'topRight', type: 'error' });
                        }
                    }
            ]
        })
    }

});