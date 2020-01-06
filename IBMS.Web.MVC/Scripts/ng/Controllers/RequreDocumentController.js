'use strict';

ibmsApp.controller("RequreDocumentController", function ($scope, $http, RequreDocumentService, $location, AuthService, filterFilter) {
    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
            $scope.loadRequreDocument();
        });
    };
    $scope.init = function () {
        $scope.reqObj = {};
        $scope.loadInsClass();
        $scope.loadInsSubClass();
        $scope.loadDocCategory();
        //$scope.loadRequreDocument();
        $scope.getCurrentUser();
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
        $scope.loadRequreDocument();
        $scope.search_query = "";
    };

    $scope.ClearFields = function () {
        $scope.reqObj = {};
    };
    //$scope.companyChange = function (companyID) {
    //    for (var i = 0; i < $scope.availableCompany.length; i++) {
    //        if ($scope.availableCompany[i].CompanyID.toString() === companyID) {
    //            $scope.companyName = $scope.availableCompany[i].companyName;
    //            break;
    //        }
    //    }
    //};

    $scope.loadInsClass = function () {
        $scope.showLoader = true;
        RequreDocumentService.getAvailableInsuranceDropdown().then(function (results) {
            //$scope.showLoader = false;
            $scope.isCompanyLoaded = true;

            if (results.status === true) {
                $scope.availableINS = results.data;
            }
            else {
                $scope.availableINS = [];
            }

            //if ($scope.isCategoryLoaded && $scope.isProductTypeLoaded && $scope.isColourLoaded && $scope.isStorageLoaded && $scope.isManufacturerLoaded && $scope.isProductLoaded && $scope.isPhysicalConditionLoaded) {
            //    $scope.showLoader = false;
            //}

           
        });
    };
    $scope.LoadSubInsClass = function (InsuranceClassID) {
        $scope.showLoader = true;
        //alert(insClass);
        CommissionHeaderService.getAvailableInsSubClass(InsuranceClassID).then(function (results) {
            $scope.showLoader = false;
            //  alert(angular.toJson(results));
            if (results.status === true) {
                $scope.availableInsSubClass = results.data;
            }
            else {
                $scope.availableInsSubClass = [];
            }
        });
    };
    $scope.loadInsSubClass = function () {
        $scope.showLoader = true;
        RequreDocumentService.getAvailableInsSubClassDropdown().then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.availableINSSUB = results.data;
            }
            else {
                $scope.availableINSSUB = [];
            }
        });
    };

    $scope.loadDocCategory = function () {
        $scope.showLoader = true;
        RequreDocumentService.getAvailableDocCategoryDropdown().then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.availableDOC = results.data;
            }
            else {
                $scope.availableDOC = [];
            }
        });
    };

    $scope.loadRequreDocument = function () {
        $scope.showLoader = true;
        RequreDocumentService.getAvailableRequreDocument($scope.currentUser.BusinessUnitID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.gridOptionsRequre = results.data;
                var data = $scope.gridOptionsRequre;

                $scope.data = angular.copy($scope.gridOptionsRequre);
                $scope.viewby = "5";
                $scope.totalItems = $scope.data.length;
                $scope.currentPage = 1;
                $scope.itemsPerPage = $scope.viewby;
                $scope.maxSize = 5; //Number of pager buttons to show
                $scope.setItemsPerPage($scope.viewby);
            }
            else {
                $scope.gridOptionsRequre = [];
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
        $scope.data = filterFilter($scope.gridOptionsRequre, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };

    $scope.getAllInsuranceSubClassByInsClass = function (insClassID) {
        $scope.showLoader = true;
        RequreDocumentService.getAvailableInsSubClassByInsClass(insClassID).then(function (results) {
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
    $scope.addReqDocument = function () {
        $scope.showLoader = true;
        Success();
        }
    function saveREQ() {
        RequreDocumentService.saveReqDocument($scope.reqObj, $scope.currentUser).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true)
            {
                noty({
                    text: 'Successfully Saved Required Document Details',
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
                    text: 'Error Saving Required Document Details',
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

    $scope.editREQ = function (REQ) {
        $("#view" + REQ.RequiredDocID).hide();
        $("#edit" + REQ.RequiredDocID).show();



    };

    $scope.cancel = function (REQ) {
        //alert(id);
        $("#view" + REQ.RequiredDocID).show();
        $("#edit" + REQ.RequiredDocID).hide();
        $scope.refreshContent();
    };
    $scope.update = function (REQ) {
        $scope.showLoader = true;
        $("#view" + REQ.RequiredDocID).show();
        $("#edit" + REQ.RequiredDocID).hide();
        SuccessUpdate(REQ);
        //Update(REQ);
    };

    function Update(REQ) {

        var reqDocumentID = REQ.RequiredDocID;
        var insSubClassID = REQ.InsuranceSubClassID;
        var insClassID = REQ.InsuranceClassID;
        var docCategoryID = REQ.DocCategoryID;
        var description = REQ.DocumentName;
        var userID = $scope.currentUser.UserID;




        RequreDocumentService.updateDOC(
          reqDocumentID, insSubClassID, insClassID, docCategoryID, description, userID
           ).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Updated Required Document Details',
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
                       text: 'Error Update Required Document Details',
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

    $scope.deleteREQ = function (DOCID) {
        $scope.showLoader = true;
        SuccessDelete(DOCID);
        }
    function Delete(DOCID) {
        RequreDocumentService.deleteREQ(DOCID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                noty({
                    text: 'Successfully Deleted Required Document Details',
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
                    text: 'Error Deleting Required Document Details',
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
            text: 'Do you want to Save Require Document Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            saveREQ();
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
            text: 'Do you want to Update Require Document Details?',
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
            text: 'Are you sure want to Delete Require Document Details?',
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