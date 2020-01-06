'use strict';

ibmsApp.controller("PolicyCategoryController", function ($scope, $http, PolicyCategoryService, $location, AuthService,filterFilter) {
    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
        });
    };
    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': 'Basic VGVzdDoxMjM=' }

    };

    getAllPolicyCategories();
    //loadDesignation();
    $scope.refreshContent = function () {
        getAllPolicyCategories();
        $scope.search_query = "";
    };

    $scope.ClearFields = function() {
        $scope.CategoryName = null;

    };
    $scope.init = function () {
       // $("#edit" + PolicyCategoryID).show();
        $scope.getCurrentUser();
    };
    $scope.edit = function (PolicyCategory) {

        $("#view" + PolicyCategory.PolicyCategoryID).hide();
        $("#edit" + PolicyCategory.PolicyCategoryID).show();
    };
    $scope.Delete = function (PolicyCategoryID) {
         $scope.showLoader = true;
        SuccessDelete(PolicyCategoryID);
    };
    $scope.update = function (PolicyCategory) {
        $scope.showLoader = true;
        $("#view" + PolicyCategory.PolicyCategoryID).show();
        $("#edit" + PolicyCategory.PolicyCategoryID).hide();
        SuccessUpdate(PolicyCategory);
       // Update(Policy);
    };
    $scope.cancel = function (PolicyCategory) {
        $("#view" + PolicyCategory.PolicyCategoryID).show();
        $("#edit" + PolicyCategory.PolicyCategoryID).hide();
        $scope.refreshContent();
    };

    $scope.Save = function () {
        $scope.showLoader = true;
        Success();
        //var designationID = $scope.DesignationID;
    }
        //var settingCode = $scope.SettingCode;
        function savePC() {
        var categoryName = $scope.CategoryName;
            var UserID = $scope.currentUser.UserID

            PolicyCategoryService.savePolicyCategoryData(
                categoryName, UserID).
               then(function (results) {
                   $scope.showLoader = false;
                   if (results.status === true) {
                       noty({
                           text: 'Successfully Saved Policy Category Details',
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
                           text: 'Error Saving Policy Category Details.' + " " + results.message,
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
                   //alert(angular.toJson(results.message));
               });
        };
   
    $scope.refresh = function () {
        getAllPolicyCategories();
    };

    function Update(PolicyCategory) {
        var policyCategoryID = PolicyCategory.PolicyCategoryID;
        var categoryName = PolicyCategory.PolicyCategoryName;
        var userID = $scope.currentUser.UserID
        
        PolicyCategoryService.updatePolicyCategoryData(
            policyCategoryID, categoryName, userID).
            then(function (results) {
                $scope.showLoader = false;
                if (results.status === true) {
                    noty({
                        text: 'Successfully Updated Policy Category Details',
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
                        text: 'Error Update Policy Category Details.'+ " " + results.message,
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

    function Delete(PolicyCategoryID) {
       // var policyCategoryID = PolicyCategory.PolicyCategoryID;


        PolicyCategoryService.DeletePolicyCategoryData(PolicyCategoryID).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Deleted Policy Category Details',
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
                       text: 'Error Deleting Policy Category Details',
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
              // alert(angular.toJson(results.message));
           });
    };

    function getAllPolicyCategories() {
        PolicyCategoryService.getAllPolicyCategories().then(function (results) {
            $scope.Pats = results.data;
            var data = $scope.Pats;

            $scope.data = angular.copy($scope.Pats);
            $scope.viewby = "5";
            $scope.totalItems = $scope.data.length;
            $scope.currentPage = 1;
            $scope.itemsPerPage = $scope.viewby;
            $scope.maxSize = 5; //Number of pager buttons to show
            $scope.setItemsPerPage($scope.viewby);
            $scope.PolicyCategory = [];

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
        $scope.data = filterFilter($scope.Pats, searchText);
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

    function getPolicyCategoryByID() {
        var id = 1;
        PolicyCategoryService.getPolicyCategoryByID(id).then(function (results) {
            $scope.Pats= results.data;
        });
    };

    //function loadDesignation() {
    //    $scope.showLoader = true;
    //    SettingService.getAvailableDesignation().then(function (results) {
    //        $scope.showLoader = false;
    //        if (results.status === true) {
    //            $scope.gridOptionsDesignation = results.data;
    //        }
    //        else {
    //            $scope.gridOptionsDesignation = [];
    //        }
    //    });
    //};

    function Success() {
        //alert("cc");
        noty({
            text: 'Do you want to Save Policy Category Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            savePC();
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

    function SuccessUpdate(Policy) {
        //alert("cc");
        noty({
            text: 'Do you want to Update Policy Category Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            Update(Policy);

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

    function SuccessDelete(PolicyCategoryID) {
        //alert("cc");
        noty({
            text: 'Are you sure want to Delete Policy Category Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            Delete(PolicyCategoryID);

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

    function notyConfirm(PolicyCategory, PolicyCategories, index) {
        var r = confirm("Are You Sure you want Delete this Record of " + PolicyCategory.CategoryName);
        if (r == true) {
            Delete(PolicyCategory);
            policyCategories.splice(index, 1);
        }
    }
});