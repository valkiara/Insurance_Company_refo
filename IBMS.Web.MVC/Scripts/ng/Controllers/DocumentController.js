ibmsApp.controller("DocumentController", function ($scope, $http, DocumentService, $location, AuthService, filterFilter) {
    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
            $scope.loadDocument();
        });
    };
    $scope.init = function () {
        $('#fileupload').hide();
        $scope.docObj = {};
        $scope.loadInsSubClass();
        $scope.getCurrentUser();
        $scope.loadDocument();
    }


    $scope.loadDocument = function () {
        $scope.showLoader = true;
        DocumentService.getAvailableDocuments($scope.currentUser.BusinessUnitID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.getAvailableDocuments = results.data;
                var data = $scope.getAvailableDocuments;

                $scope.data = angular.copy($scope.getAvailableDocuments);
                $scope.viewby = "5";
                $scope.totalItems = $scope.data.length;
                $scope.currentPage = 1;
                $scope.itemsPerPage = $scope.viewby;
                $scope.maxSize = 5; //Number of pager buttons to show
                $scope.setItemsPerPage($scope.viewby);
            }
            else {
                $scope.getAvailableDocuments = [];
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
        $scope.data = filterFilter($scope.getAvailableDocuments, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };


    $scope.loadInsSubClass = function () {
        $scope.showLoader = true;
        DocumentService.getAvailableInsSubClass().then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.availableInsSubClass = results.data;
            }
            else {
                $scope.availableInsSubClass = [];
            }


        });
    };

    
        $scope.uploadedDocument = "";

        $scope.getDocument = function (e) {
            $scope.uploadedDocument = e.files[0];
        };

        $scope.addDocument = function () {
            if ($scope.uploadedDocument == "") {
                //alert("Nofile");
                $('#fileupload').show();
            }
            else {
                $scope.showLoader = true;
                Success();
            }
        }
        $scope.refreshContent = function () {
            $scope.loadDocument();
            $scope.search_query = "";
        };
        $scope.ClearFields = function () {
            $scope.docObj = {};
        };


        function saveDoc() {
            
            var formData = new FormData();
            formData.append("insSubClassID", $scope.docObj.InsuranceSubClassID);
            formData.append("documentName", $scope.docObj.DocumentName);
            formData.append("uploadedDocument", $scope.uploadedDocument);
            formData.append("description", $scope.docObj.Description);
            formData.append("userID", $scope.currentUser.UserID);
            //formData.append("isUploaded", 'true');

            var objXhr = new XMLHttpRequest();

            objXhr.onreadystatechange = function () {
                $scope.showLoader = false;
                if (this.readyState == 4 && this.status == 200) {
                    noty({
                        text: 'Successfully Saved Document Details',
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
                if (this.readyState == null && this.status == null) {
                    noty({
                        text: 'Error Saving Document Details',
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

            objXhr.open("POST", "http://192.168.1.5:9810/api/Document/SaveDocument", true);
            objXhr.setRequestHeader("Authorization", 'Basic QWRtaW5JQk1TOmlibXNJcyMyMDE4');
            objXhr.send(formData);
            
        };
   
        function updateDocument (Doc) {
            var formData = new FormData();
            formData.append("documentID", Doc.DocumentID);
            formData.append("insSubClassID", Doc.InsuranceSubClassID);
            formData.append("documentName", Doc.DocumentName);
            formData.append("uploadedDocument",$scope.uploadedDocument);
            formData.append("description", Doc.Description);
            formData.append("userID", $scope.currentUser.UserID);

            var objXhr = new XMLHttpRequest();

            objXhr.onreadystatechange = function () {
                $scope.showLoader = false;
                if (this.readyState == 4 && this.status == 200) {
                    noty({
                        text: 'Successfully Updated Document Details',
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
               if (this.readyState == null && this.status == null) {
                   noty({
                       text: 'Error Updating Document Details',
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
            };

            objXhr.open("POST", "http://192.168.1.5:9810/api/Document/UpdateDocument", true);
            objXhr.setRequestHeader("Authorization", "Basic QWRtaW5JQk1TOmlibXNJcyMyMDE4");
            objXhr.send(formData);
        };

        $scope.editDoc = function (Doc) {
            $("#view" + Doc.DocumentID).hide();
            $("#edit" + Doc.DocumentID).show();



        };

        $scope.cancel = function (Doc) {
            //alert(id);
            $("#view" + Doc.DocumentID).show();
            $("#edit" + Doc.DocumentID).hide();
            $scope.refreshContent();

        };
        $scope.update = function (Doc) {
            $("#view" + Doc.DocumentID).show();
            $("#edit" + Doc.DocumentID).hide();
            $scope.showLoader = true;
            //SuccessUpdate(BU);
            updateDocument(Doc);
        };

        $scope.deleteDoc = function (DocID) {
            $scope.showLoader = true;
            SuccessDelete(DocID);
        }
        function Delete(DocID) {
            DocumentService.deleteDoc(DocID).then(function (results) {
                $scope.showLoader = false;
                if (results.status === true) {
                    noty({
                        text: 'Successfully Deleted Document Details',
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

                    //setTimeout(function () { window.location.href = "/CommissionHeader/Index" }, 2500)
                    $scope.ClearFields();
                    $scope.refreshContent();
                }
                else {
                    noty({
                        text: 'Error Deleting Document Details',
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
                text: 'Do you want to Save Document Details?',
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

        function SuccessUpdate(BU) {
            //alert("cc");
            noty({
                text: 'Do you want to Update Document Details?',
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

        function SuccessDelete(DocID) {
            //alert("cc");
            noty({
                text: 'Are you sure want to Delete Document Details?',
                layout: 'topCenter',
                buttons: [
                        {
                            addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                                $noty.close();
                                Delete(DocID);

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
            });
        }
});