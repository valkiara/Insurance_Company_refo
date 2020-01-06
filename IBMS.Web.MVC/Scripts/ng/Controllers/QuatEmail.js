ibmsApp.controller("DocumentController", function ($scope, $http, DocumentService, EmailService, $location, AuthService, filterFilter, EmailHelperService) {


    $scope.iscustomer = false;
    $scope.IsCompany = false;

    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
        });
    };
    $scope.$on('SendCustomer', function (event, args) {
        $scope.Message = args.message;
        $scope.iscustomer = true;
        $scope.IsCompany = false;
        // alert($scope.Message);
        $scope.initCustomer();
    });
    $scope.$on('SendCompany', function (event, args) {
        $scope.Message = args.message;
        $scope.IsCompany = true;
        $scope.iscustomer = false;
        // alert($scope.Message);


        $scope.init();
    })
    $scope.init = function () {
       
                //$('#fileupload').hide();
        $scope.docObj = {};
        $scope.emlObj = {};
        //$scope.loadInsSubClass();
        $scope.getCurrentUser();
        //$scope.loadDocument();
        $scope.customer = EmailHelperService.Customer;
        $scope.Subject = EmailHelperService.Subject;
        $scope.HeaderObj = EmailHelperService.QuatationHeader;
        
        $scope.emlObj.Name = $scope.customer.InsuranceCompanyName;
        $scope.InsuranceCompanyID = $scope.customer.InsuranceCompanyID;
        $scope.emlObj.email = $scope.customer.Email;
        $scope.emlObj.Subject = $scope.Subject;
       // InsuranceCompanyID
        $scope.emlObj.Messege = "Dear Company,</br> Please Go Through This Link to View Your Attachment of Qutation  ";
       //alert(angular.toJson($scope.emlObj));
        GetInsuranceCompanyByID();
        
        


    }

    $scope.initCustomer = function () {

        //$('#fileupload').hide();
        $scope.docObj = {};
        $scope.emlObj = {};
        //$scope.loadInsSubClass();
        $scope.getCurrentUser();
        //$scope.loadDocument();
        $scope.customer = EmailHelperService.Customer;
        $scope.Subject = EmailHelperService.Subject;
        $scope.HeaderObj = EmailHelperService.QuatationHeader;

        $scope.emlObj.Name = $scope.customer.cusName;
        $scope.emlObj.email = $scope.customer.Email;
        $scope.emlObj.Subject = $scope.Subject;
        // InsuranceCompanyID
        $scope.emlObj.Messege = "Dear Customer,</br> Please Go Through This Link to View Your Attachment of Your query Regarding Insurance at Senarathne ";
        //alert(angular.toJson($scope.emlObj));





    }



  
   
   
   
    
    
        $scope.uploadedDocument = "";

        $scope.getDocument = function (e) {
            $scope.uploadedDocument = e.files[0];
            saveDoc();
           // alert(e);
        };

  

        function saveDoc() {
            
            var formData = new FormData();
            formData.append("insSubClassID", "5");
            formData.append("documentName", $scope.docObj.DocumentName);
            formData.append("uploadedDocument", $scope.uploadedDocument);
            formData.append("description", $scope.docObj.Description);
            formData.append("userID", $scope.currentUser.UserID);
            //formData.append("isUploaded", 'true');
           // alert(angular.toJson($scope.docObj));
            var objXhr = new XMLHttpRequest();

            objXhr.onreadystatechange = function () {
                $scope.showLoader = false;
                if (this.readyState == 4 && this.status == 200) {
                    $scope.emlObj.Messege = $scope.emlObj.Messege+"http://192.168.1.5:9810/Uploads/Documents/undefined_20180325144941.xls";
                    //Successemail();
                    //alert($scope.emlObj.Messege);
                    notyFy("Successfully Saved Your Document");
                   // $scope.ClearFields();
                   // $scope.refreshContent();
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
           
         //   return url;
            
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


        $scope.saveEmail = function () {
            $scope.showLoader = true;

            // EmailHelperService.user.Location;
            //alert(angular.toJson($scope.HeaderObj));


            if ($scope.uploadedDocument == "")
            {
                alert("Nofile");
            }
            else {
                $scope.showLoader = true;
                Successemail();
                //saveDoc();
            }
            // saveDoc();
        }

        function SendEMial() {
            $scope.emlObj.Header = "Quataion Details";
           // alert($("#emailc").val());
            EmailService.saveEmail($scope.emlObj, $scope.customer).then(function (results) {
                $scope.showLoader = false;
               // alert(angular.toJson(results));
                if (results.status === true) {
                    noty({ text: 'Successfully Send Email', layout: 'topRight', type: 'success' });
                    //setTimeout(function () { window.location.href = "/Email/EmailSend" }, 2500)
                    //ClearFields();
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

 
        function Successemail() {
            //alert("cc");
            noty({
                text: 'Do you want to Send this Email?',
                layout: 'topRight',
                buttons: [
                        {
                            addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                                $noty.close();
                                SendEMial();
                                $("#emailModel").modal("hide");
                                notyFy("email Successfully Sent");
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

        function notyFy(message)
        {

            noty({
                text: message,
                layout: 'topRight',
                buttons: [
                        
                        {
                            addClass: 'btn btn-danger btn-clean', text: 'OK', onClick: function ($noty) {
                                $noty.close();
                                //noty({ text: 'You clicked "Cancel" button', layout: 'topRight', type: 'error' });
                            }
                        }
                ]
            })





        }

        function GetInsuranceCompanyByID() {
            //alert(angular.toJson($scope.InsuranceCompanyID));
            EmailService.GetInsuranceCompanyByID($scope.InsuranceCompanyID).then(function (results) {
                $scope.showLoader = false;
                //alert(angular.toJson(results));
                if (results.status === true) {


                }

               
            });
        };



});