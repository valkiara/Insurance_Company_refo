'use strict';

ibmsApp.controller("SettingController", function ($scope, $http, SettingService, $location, AuthService, filterFilter) {
    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
            getAllSettings();
        });
    };
    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': 'Basic VGVzdDoxMjM=' }

    };
    $scope.SettingDesc = null;
    //getAllSettings();
    loadDesignation();
  
    $scope.init = function () {
        $scope.getCurrentUser();
       // $("#edit" + SettingID).show();
      //  $("#edit" + DesignationID).show();
    };
    $scope.refreshContent = function () {
        getAllSettings();
        $scope.search_query = "";
    };
    $scope.ClearFields = function () {
        $scope.Designation = null;
        $scope.SettingCode = null;
        $scope.SettingDesc = null;
    }

    $scope.edit = function (Setting) {

        $("#view" + Setting.SettingID).hide();
        $("#edit" + Setting.SettingID).show();
    };
    $scope.Delete = function (SettingID) {
        $scope.showLoader = true;
        SuccessDelete(SettingID);
        //alert("fg");
        //notyConfirm(Setting, Settings, index);
    };

    function DeleteSetting(SettingID) {
        SettingService.DeleteSettingData(SettingID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                noty({
                    text: 'Successfully Deleted Setting Details',
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
                    text: 'Error Deleting Setting Details',
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
    }
    $scope.update = function (Setting) {
        $("#view" + Setting.SettingID).show();
        $("#edit" + Setting.SettingID).hide();
        Update(Setting);
    };
    $scope.cancel = function (Setting) {
        $("#view" + Setting.SettingID).show();
        $("#edit" + Setting.SettingID).hide();
        $scope.refreshContent();
    };

    $scope.Save = function () {
     $scope.showLoader = true;
        Success();
    }

    function saveSetting(){
        
        var designation = $scope.Designation;
        
        var settingCode = $scope.SettingCode;
        var settingDesc = $scope.SettingDesc;
        var userID = $scope.currentUser.UserID;


        SettingService.saveSettingData(
            designation, settingCode, settingDesc,userID).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Saved Settings Details',
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
                       text: 'Error Saving Settings Details' + '/' + results.message,
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
    //$scope.refresh = function () {
    //    getAllSettings();
    //};

    function Update(Setting) {
        $scope.showLoader = true;
        SuccessUpdate(Setting);
    }

    function updateSetting(Setting) {
        var settingID = Setting.SettingID;
        var settingCode = Setting.SettingCode;
        var designationID = Setting.DesignationID;
        var userID = $scope.currentUser.UserID;
        var settingDesc = Setting.SettingDescription;
        
        SettingService.UpdateSettingData(
            settingID, designationID, settingCode, settingDesc, userID).
            then(function (results) {
                $scope.showLoader = false;
                if (results.status === true) {
                    noty({
                        text: 'Successfully Updated Settings Details',
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
                        text: 'Error Update Settings Details.' + " " + results.message,
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
                  //  noty({ text: 'Error Update Setting Details', layout: 'topCenter', type: 'error' });
                }
                //alert(angular.toJson(results.message));
            });
    };

    function Delete(Setting) {
        var settingID = Setting.SettingID;


        SettingService.DeleteSettingData(settingID).
           then(function (results) {
               alert(angular.toJson(results.message));
           });
    };

    function getAllSettings() {
        SettingService.getAllSettings($scope.currentUser.BusinessUnitID).then(function (results) {
            $scope.Sets = results.data;
            var data = $scope.Sets;

            $scope.data = angular.copy($scope.Sets);
            $scope.viewby = "5";
            $scope.totalItems = $scope.data.length;
            $scope.currentPage = 1;
            $scope.itemsPerPage = $scope.viewby;
            $scope.maxSize = 5; //Number of pager buttons to show
            $scope.setItemsPerPage($scope.viewby);
            $scope.Setting = [];
           
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
        $scope.data = filterFilter($scope.Sets, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };

    function getSettingByID() {
        var id = 1;
        SettingService.getSettingByID(id).then(function (results) {
            $scope.Sets = results.data;
        });
    };

    function loadDesignation() {
        $scope.showLoader = true;
        SettingService.getAvailableDesignation().then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.gridOptionsDesignation = results.data;
            }
            else {
                $scope.gridOptionsDesignation = [];
            }
        });
    };

    function Success() {
        //alert("cc");
        noty({
            text: 'Do you want to Save Setting Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            saveSetting();
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

    function SuccessUpdate(Setting) {
        //alert("cc");
        noty({
            text: 'Do you want to Update Setting Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            updateSetting(Setting);

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

    function SuccessDelete(SettingID) {
        //alert("cc");
        noty({
            text: 'Are you sure want to Delete Setting Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            DeleteSetting(SettingID);

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

    function notyConfirm(Setting, Settings, index) {
        var r = confirm("Are You Sure you want Delete this Record of " + Setting.SettingCode);
        if (r == true) {
            Delete(Setting);
            settings.splice(index, 1);
        }
    }
});