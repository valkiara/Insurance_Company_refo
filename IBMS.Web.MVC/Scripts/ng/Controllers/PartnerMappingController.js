'use strict';

ibmsApp.controller("PartnerMappingController", function ($scope, $http, PartnerMappingService, $location, AuthService, filterFilter) {
    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
        });
    };
    //var config = {
    //    headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': 'Basic QWRtaW5JQk1TOmlibXNJcyMyMDE4' }
    //};

    getAllPartnerMappings();
    //loadDesignation();

    $scope.init = function () {
        $scope.getCurrentUser();
       // $("#edit" + PartnerID).show();
    };
    $scope.edit = function (PartnerMapping) {

        $("#view" + PartnerMapping.PartnerID).hide();
        $("#edit" + PartnerMapping.PartnerID).show();
    };
    $scope.Delete = function (PartnerID) {
        $scope.showLoader = true;
        SuccessDelete(PartnerID);
    };

    $scope.refreshContent = function () {
        getAllPartnerMappings();
        $scope.search_query = "";
    };
    $scope.ClearFields = function () {
        $scope.PartnerName = null;
    }

    function DeletePat(PartnerID) {
        PartnerMappingService.DeletePartnerMappingData(PartnerID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                noty({
                    text: 'Successfully Deleted Partner Mapping Details',
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
                    text: 'Error Deleteing Partner Mapping Details',
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
    $scope.update = function (PartnerMapping) {
        $("#view" + PartnerMapping.PartnerID).show();
        $("#edit" + PartnerMapping.PartnerID).hide();
        Update(PartnerMapping);
    };
    $scope.cancel = function (PartnerMapping) {
        $("#view" + PartnerMapping.PartnerID).show();
        $("#edit" + PartnerMapping.PartnerID).hide();
        $scope.refreshContent();
    };

    $scope.Save = function () {
        $scope.showLoader = true;
        Success();
    }
        //var designationID = $scope.DesignationID;

    //var settingCode = $scope.SettingCode;
       function savePat(){
        var partnerName = $scope.PartnerName;
        var userID = $scope.currentUser.UserID;


        PartnerMappingService.savePartnerMappingData(
            partnerName, userID).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Saved Partner Mapping Details',
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
                       text: 'Error Saving Partner Mapping Details.' + " " + results.message,
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
    $scope.refresh = function () {
        getAllPartnerMappings();
    };

    function Update(PartnerMapping) {
        $scope.showLoader = true;
        SuccessUpdate(PartnerMapping);
    }
    function updatePat(PartnerMapping) {
        var partnerID = PartnerMapping.PartnerID;
        var partnerName = PartnerMapping.PartnerName;
        var userID = $scope.currentUser.UserID;
        //var designationID = $scope.DesignationID;

        //var settingDesc = Setting.SettingDesc;

        PartnerMappingService.updatePartnerMappingData(
            partnerID, partnerName, userID).
            then(function (results) {
                $scope.showLoader = false;
                if (results.status === true) {
                    noty({
                        text: 'Successfully Updated Partner Mapping Details',
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
                        text: 'Error Update Partner Mapping Details.' + " " + results.message,
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
                    //noty({ text: 'Error Update Partner Mapping Details', layout: 'topCenter', type: 'error' });
                }
            });
    };

    function Delete(PartnerMapping) {
        var partnerID = PartnerMapping.PartnerID;


        PartnerMappingService.DeletePartnerMappingData(partnerID).
           then(function (results) {
               if (results.status === true) {
                   noty({
                       text: 'Successfully Deleted Partner Mapping Details',
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
                       text: 'Error Deleteing Partner Mapping Details',
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

    function getAllPartnerMappings() {
        $scope.showLoader = true;
        PartnerMappingService.getAllPartnerMappings().then(function (results) {
            $scope.showLoader = false;
            $scope.Pats = results.data;
            var data = $scope.Pats;

            $scope.data = angular.copy($scope.Pats);
            $scope.viewby = "5";
            $scope.totalItems = $scope.data.length;
            $scope.currentPage = 1;
            $scope.itemsPerPage = $scope.viewby;
            $scope.maxSize = 5; //Number of pager buttons to show
            $scope.setItemsPerPage($scope.viewby);
            $scope.PartnerMapping = [];

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

    

    function getPartnerMappingByID() {
        var id = 1;
        PartnerMappingService.getPartnerMappingByID(id).then(function (results) {
            $scope.Pats = results.data;
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
            text: 'Do you want to Save Partner Mapping Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            savePat();
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

    function SuccessUpdate(PartnerMapping) {
        //alert("cc");
        noty({
            text: 'Do you want to Update Partner Mapping Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            updatePat(PartnerMapping)

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

    function SuccessDelete(PartnerID) {
        //alert("cc");
        noty({
            text: 'Are you sure want to Delete Partner Mapping Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            DeletePat(PartnerID);

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
    function notyConfirm(PartnerMapping, partnerMappings, index) {
        var r = confirm("Are You Sure you want Delete this Record of " + PartnerMapping.PartnerName);
        if (r == true) {
            Delete(PartnerMapping);
            partnerMappings.splice(index, 1);
        }
    }
});