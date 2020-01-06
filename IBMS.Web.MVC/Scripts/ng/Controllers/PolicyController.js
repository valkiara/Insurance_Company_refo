'use strict';

ibmsApp.controller("PolicyController", function ($scope, $http, PolicyServices, $location, AuthService, filterFilter) {
    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
            $scope.businessUnitID = results.BusinessUnitID;
            $scope.PolicyObj.buID = results.BusinessUnitID;
            $scope.loadBusinessUnit();
            $scope.loadPolicy();
        });
    };
    $scope.init = function () {
        $scope.PolicyObj = {};
       // $scope.loadBusinessUnit();
        $scope.loadPolicyCategory();
        $scope.filteredBusinessUnits = [];
        //$scope.loadPolicy();
        $scope.getCurrentUser();

        $scope.one = true;
    }
    $scope.checkInputRate = function (Agent) {
        if (Agent.RateValue > 100) {
            Agent.RateValue = Agent.RateValue / 10;
        }
    };

    $scope.checkInputRateByValue = function () {
        if ($scope.RateValue > 100) {
            $scope.RateValue = $scope.RateValue / 10;
        }
    };
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
        $scope.loadPolicy();
        $scope.search_query = "";
    };

    $scope.ClearFields = function () {
        $scope.PolicyObj = {};
        $scope.RateValue = "";
    };

    $scope.loadBusinessUnit = function () {
        if ($scope.currentUser.AccessLevelTypeName == "Admin") {
            $scope.showLoader = true;
            PolicyServices.getAvailableBusinessUnit().then(function (results) {
                $scope.showLoader = false;
                if (results.status === true) {
                    $scope.filteredBusinessUnits = results.data;
                    //for (var i = 0; i < $scope.availableBusinessUnit.length; i++) {
                    //    if ($scope.availableBusinessUnit[i].BusinessUnitID === $scope.currentUser.BusinessUnitID) {
                    //        $scope.filteredBusinessUnits.push({ "BusinessUnitID": $scope.availableBusinessUnit[i].BusinessUnitID, "BusinessUnit": $scope.availableBusinessUnit[i].BusinessUnit });
                    //        break;
                    //    }
                    //}
                }
                else {
                    $scope.availableBusinessUnit = [];
                }
            });
        }
        $scope.filteredBusinessUnits = [];
        $scope.filteredBusinessUnits.push({
            "BusinessUnitID": $scope.currentUser.BusinessUnitID,
            "BusinessUnit": $scope.currentUser.BusinessUnitName

        });
        //$scope.showLoader = true;
        //PolicyServices.getAvailableBusinessUnit().then(function (results) {
        //    $scope.showLoader = false;
        //    if (results.status === true) {
        //        $scope.availableBusinessUnit = results.data;
        //        for (var i = 0; i < $scope.availableBusinessUnit.length; i++) {
        //            if ($scope.availableBusinessUnit[i].BusinessUnitID === $scope.currentUser.BusinessUnitID) {
        //                $scope.filteredBusinessUnits.push({ "BusinessUnitID": $scope.availableBusinessUnit[i].BusinessUnitID, "BusinessUnit": $scope.availableBusinessUnit[i].BusinessUnit });
        //                break;
        //            }
        //        }
        //    }
        //    else {
        //        $scope.availableBusinessUnit = [];
        //    }
        //});
    };

    $scope.loadPolicyCategory = function () {
        $scope.showLoader = true;
        PolicyServices.getAvailablePolicyCategory().then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.availablePolicyCategory = results.data;
            }
            else {
                $scope.availablePolicyCategory = [];
            }
        });
    };

    $scope.addPolicy = function () {
        $scope.showLoader = true;
        Success();
    }
    function savePolicy() {
        PolicyServices.savePolicy($scope.PolicyObj, $scope.currentUser).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true)
            {
                noty({
                    text: 'Successfully Saved Policy Details',
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
                    text: 'Error Saving Policy Details.' + " " + results.message,
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



    $scope.loadPolicy = function () {
        $scope.showLoader = true;
        PolicyServices.getAvailablePolicy($scope.currentUser.BusinessUnitID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.gridOptionsPolicy = results.data;
               // $scope.gridOptionsRate = results.data;
                var data = $scope.gridOptionsPolicy;

                $scope.data = angular.copy($scope.gridOptionsPolicy);
                $scope.viewby = "5";
                $scope.totalItems = $scope.data.length;
                $scope.currentPage = 1;
                $scope.itemsPerPage = $scope.viewby;
                $scope.maxSize = 5; //Number of pager buttons to show
                $scope.setItemsPerPage($scope.viewby);
            }
            else {
                $scope.gridOptionsPolicy = [];
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
        $scope.data = filterFilter($scope.gridOptionsPolicy, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };

    $scope.editPolicy = function (Policy) {
        $("#view" + Policy.PolicyID).hide();
        $("#edit" + Policy.PolicyID).show();

    };

    $scope.cancel = function (Policy) {
        //alert(id);
        $("#view" + Policy.PolicyID).show();
        $("#edit" + Policy.PolicyID).hide();
        $scope.refreshContent();
    };
    $scope.update = function (Policy) {
        $scope.showLoader = true;
        $("#view" + Policy.PolicyID).show();
        $("#edit" + Policy.PolicyID).hide();
        SuccessUpdate(Policy);
        //Update(Policy);
    };

    function Update(Policy) {


        var PolicyID = Policy.PolicyID;
        var PolicyName = Policy.PolicyName;
        var Rate = Policy.Rate;
        var PolicyCategoryID = Policy.PolicyCategoryID;
        var BUID = Policy.BusinessUnitID;




        PolicyServices.updatePolicy(
           PolicyID, PolicyName, Rate, PolicyCategoryID, BUID, $scope.currentUser
           ).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Updated Policy Details',
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
                       text: 'Error Update Policy Details.'+ " " + results.message,
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

    $scope.deletePolicy = function (PolicyID) {
        $scope.showLoader = true;
        SuccessDelete(PolicyID);
    }
    function Delete(PolicyID) {
        PolicyServices.deletePolicy(PolicyID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                noty({
                    text: 'Successfully Deleted Policy Details',
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
                    text: 'Error Deleting Policy Details',
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
            text: 'Do you want to Save Policy Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            savePolicy();
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

    function SuccessUpdate(Policy) {
        //alert("cc");
        noty({
            text: 'Do you want to Update Policy Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            Update(Policy)

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

    function SuccessDelete(PolicyID) {
        //alert("cc");
        noty({
            text: 'Are you sure want to Delete Policy Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            Delete(PolicyID);

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

});