'use strict';

ibmsApp.controller("EmailController", function ($rootScope, $scope, $http, EmailService, $location, AuthService, EmailHelperService) {


    //scope.ulocation = EmailHelperService.user.Location;

    $scope.uploadedDocument = null;
    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
        });
    };
    $scope.init = function () {
        $scope.getCurrentUser();
        $scope.emlObj = {};
        
    }
    $scope.saveEmail = function () {
        $scope.showLoader = true;
        $scope.customer = EmailHelperService.Customer;
        $scope.Subject = EmailHelperService.Subject;
        $scope.HeaderObj = EmailHelperService.QuatationHeader;
       // EmailHelperService.user.Location;
        alert(angular.toJson($scope.HeaderObj));


        if ($scope.uploadedDocument == "") {
            alert("Nofile");
        }
        else {
            $scope.showLoader = true;
            Succ();
        }
       // saveDoc();
    }
    $scope.UploadDocument = function () {
        saveDoc();




    }
    $scope.getDocument = function (e) {
        alert(e);
        $scope.uploadedDocument = e.files[0];
       // alert($scope.uploadedDocument);
     //   saveDoc();
    };
    function Succ() {
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
    function saveDoc() {
        //alert("g");
        var formData = new FormData();
        formData.append("insSubClassID", 5);
        formData.append("documentName", 'Test');
        formData.append("uploadedDocument", $scope.uploadedDocument);
        formData.append("description", 'Test Description');
        formData.append("userID", 1);
        //formData.append("isUploaded", 'true');
        ///alert("ghjgh" + formData);
        //alert(angular.toJson($scope.uploadedDocument));

        var objXhr = new XMLHttpRequest();

        objXhr.onreadystatechange = function ()
        {
            $scope.showLoader = false;
            alert(angular.toJson(this.responseText));
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
                //ClearFields();
                //$scope.refreshContent();
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

        objXhr.open("POST", "http://192.168.1.168:8003/api/Document/SaveDocument", true);
        objXhr.setRequestHeader("Authorization", 'Basic QWRtaW5JQk1TOmlibXNJcyMyMDE4');
        objXhr.send(formData);



    };

    function saveBU() {
        EmailService.saveEmail($scope.emlObj,$scope.customer).then(function (results) {
            $scope.showLoader = false;
            alert(angular.toJson(results));
            if (results.status === true) {
                noty({ text: 'Successfully Send Email', layout: 'topRight', type: 'success' });
                //setTimeout(function () { window.location.href = "/Email/EmailSend" }, 2500)
                ClearFields();
            }
            else {
                noty({ text: 'Error Send Email Details', layout: 'topRight', type: 'error' });
            }

        });
    };
        /*------------------------------------TinyMCE Options-------------------------------------------*/
    $scope.tinymceOptions = {
        theme: "modern",
        plugins: [
            "advlist autolink lists link image charmap print preview hr anchor pagebreak",
            "searchreplace wordcount visualblocks visualchars code fullscreen",
            "insertdatetime media nonbreaking save table contextmenu directionality",
            "emoticons template paste textcolor"
        ],
        toolbar1: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image",
        toolbar2: "print preview media | forecolor backcolor emoticons",
        image_advtab: true,
        height: "200px",
        //width: "650px"
    };

    /*------------------------------------TinyMCE Options-------------------------------------------*/

    $scope.editBU = function (BU) {
        $("#view" + BU.BusinessUnitID).hide();
        $("#edit" + BU.BusinessUnitID).show();



    };

    $scope.cancel = function (BU) {
        //alert(id);
        $("#view" + BU.BusinessUnitID).show();
        $("#edit" + BU.BusinessUnitID).hide();

    };
    $scope.update = function (BU) {
        $("#view" + BU.BusinessUnitID).show();
        $("#edit" + BU.BusinessUnitID).hide();
        SuccessUpdate(BU);
        //Update(BU);
    };

    function Update(BU) {


        var businessUnitID = BU.BusinessUnitID;
        var businessUnit = BU.BusinessUnit;
        var IsActive = BU.IsActive;
        var ComapnyID = BU.CompanyID;




        BusinessUnitService.updateBU(
           businessUnitID, businessUnit, ComapnyID, IsActive, $scope.currentUser.UserID
           ).
           then(function (results) {

               if (results.status === true) {
                   noty({ text: 'Successfully Updated BusinessUnit Details', layout: 'topRight', type: 'success' });

                   setTimeout(function () { window.location.href = "/BusinessUnit/Index" }, 2500)

                   ClearFields();
               }
               else {
                   noty({ text: 'Error Update BusinessUnit Details', layout: 'topRight', type: 'error' });
               }
           });

    };

    $scope.deleteBU = function (BUID) {
        $scope.showLoader = true;
        SuccessDelete(BUID);
    }
    function Delete(BUID) {
        BusinessUnitService.deleteBU(BUID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                noty({ text: 'Successfully Deleted BusinessUnit Details', layout: 'topRight', type: 'success' });
                setTimeout(function () { window.location.href = "/BusinessUnit/Index" }, 2500)
            }
            else {
                noty({ text: 'Error Deleteing BusinessUnit Details', layout: 'topRight', type: 'error' });
            }
        });
    };

    function Success() {
        //alert("cc");
        noty({
            text: 'Do you want to Send this Email?',
            layout: 'topRight',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            saveBU();
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

    function SuccessUpdate(BU) {
        //alert("cc");
        noty({
            text: 'Do you want to Update BusinessUnit Details?',
            layout: 'topRight',
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
                            //noty({ text: 'You clicked "Cancel" button', layout: 'topRight', type: 'error' });
                        }
                    }
            ]
        })
    }

    function SuccessDelete(BUID) {
        //alert("cc");
        noty({
            text: 'Are you sure want to Delete BusinessUnit Details?',
            layout: 'topRight',
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
                            //noty({ text: 'You clicked "Cancel" button', layout: 'topRight', type: 'error' });
                        }
                    }
            ]
        })
    }

});