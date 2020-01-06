'use strict';

ibmsApp.controller("DocCategoryController", function ($scope, $http, DocCategoryService, $location, AuthService, filterFilter) {
    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
        });
    };

    $scope.init = function () {
        $scope.docObj = {};
        $scope.loadDocCategory();
        $scope.getCurrentUser();
        $scope.getCurrentUser();
    };

    $scope.loadDocCategory = function () {

        $scope.showLoader = true;
        DocCategoryService.getAvailableDocCategory().then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.gridOptionsDOC = results.data;
                var data = $scope.gridOptionsDOC;

                $scope.data = angular.copy($scope.gridOptionsDOC);
                $scope.viewby = "5";
                $scope.totalItems = $scope.data.length;
                $scope.currentPage = 1;
                $scope.itemsPerPage = $scope.viewby;
                $scope.maxSize = 5; //Number of pager buttons to show
                $scope.setItemsPerPage($scope.viewby);
            }
            else {
                $scope.gridOptionsDOC = [];
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
        $scope.data = filterFilter($scope.gridOptionsDOC, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };

    $scope.addDocCategory = function () {
        $scope.showLoader = true;
        Success();
    };

    $scope.refreshContent = function () {
        $scope.loadDocCategory();
        $scope.search_query = "";
    };

    $scope.ClearFields = function () {
        $scope.docObj = {};
    };

    function saveDoc() {
        DocCategoryService.saveDocCategory($scope.docObj, $scope.currentUser).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                noty({
                    text: 'Successfully Saved Document Category Details',
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
                    text: 'Error Saving Document Category Details.' + " " + results.message,
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

    $scope.editDOC = function (DOC) {
        $("#view" + DOC.DocCategoryID).hide();
        $("#edit" + DOC.DocCategoryID).show();
    };

    $scope.cancel = function (DOC) {
        $("#view" + DOC.DocCategoryID).show();
        $("#edit" + DOC.DocCategoryID).hide();
        $scope.refreshContent();

    };
    $scope.update = function (DOC) {
        $scope.showLoader = true;
        $("#view" + DOC.DocCategoryID).show();
        $("#edit" + DOC.DocCategoryID).hide();
        SuccessUpdate(DOC)
    };

    function Update(DOC) {
        var docCategoryID = DOC.DocCategoryID;
        var docCategoryName = DOC.CategoryName;
        var userID = $scope.currentUser.UserID;

        DocCategoryService.updateDOC(
           docCategoryID, docCategoryName, userID
           ).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Updated Document Category Details',
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
                       text: 'Error Update Document Category Details.' + " " + results.message,
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

    $scope.deleteDOC = function (DOCID) {
        $scope.showLoader = true;
        SuccessDelete(DOCID)
    };

    function DeleteDoc(DOCID) {
        DocCategoryService.deleteDOC(DOCID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                noty({
                    text: 'Successfully Deleted Document Category Details',
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
                    text: 'Error Deleting Document Category Details',
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
            text: 'Do you want to Save Document Category Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            saveDoc();
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

    function SuccessUpdate(DOC) {
        //alert("cc");
        noty({
            text: 'Do you want to Update Document Category Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            Update(DOC);

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

    function SuccessDelete(DOCID) {
        //alert("cc");
        noty({
            text: 'Are you sure want to Delete Document Category Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            DeleteDoc(DOCID);

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