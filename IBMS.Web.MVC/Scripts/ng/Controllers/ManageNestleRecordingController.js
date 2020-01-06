'use strict';

ibmsApp.controller("ManageNestleRecordingController", function ($scope, $http, $rootScope, ManageNestleRecordingService, $location, AuthService, filterFilter, $modal) {

    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;

            if ($scope.currentUser.AccessLevelTypeName === "Admin") {
                //To Do (using a popup to select the desired business unit)
            }
            else {
                $scope.businessUnitID = $scope.currentUser.BusinessUnitID;
                $scope.loadClientRequestsByBUID($scope.businessUnitID);
                $scope.loadClientsByBUID($scope.businessUnitID);
                
                
                $scope.loadPartners();

                // $scope.loadBanks($scope.businessUnitID);
            }
        });
    };

    $scope.init = function () {
        $scope.isViewMode = false;
        $scope.isClientReqAddMode = true;
        $scope.isCustomerAvailable = false;
        $scope.isCustomerAdded = false;
        $scope.isCustomerUpdated = false;


        $scope.businessUnitID = "";
        $scope.availableClients = [];
        $scope.availablePartners = [];
        $scope.availableAgents = [];
        $scope.availableBanks = [];
        $scope.availableTitle = [];
        // $scope.cusObj.ChildrenDetailss = [];
        $scope.cusObj = {};
        $scope.cusReqObj = {};
        $scope.cusReqObj.PaymentDetails = [];
        $scope.cusObj.DeductionDetails = [];
        //   $scope.cusReqObj.ClientRequestLineDetails = [];
        // $scope.customerObj.childrenDetails = [];
        $scope.availableInsSubClasses = []

        $scope.getCurrentUser();
        //$scope.loadPartners();
        $scope.loadAgent();
        $scope.addItem();
        //$scope.loadTitle();

        
    };

 

    $scope.calAmount = function () {
        
        $scope.policyCommissionPayment.LoadingRateAmount = ($scope.PaymentDetails.PremiumAmount * ($scope.PaymentDetails.LoadingRate)) / 100;

    };
    $scope.refreshContent = function () {
        $scope.loadClientRequestsByBUID($scope.businessUnitID);
        $scope.loadClientsByBUID($scope.businessUnitID);
        $scope.search_query = "";
    };

    $scope.ClearFields = function () {
        $scope.isViewMode = false;
        $scope.isClientReqAddMode = true;
        $scope.isCustomerAvailable = false;
        $scope.isCustomerAdded = false;
        $scope.isCustomerUpdated = false;

        $scope.cusObj = {};
        $scope.cusReqObj = {};
        //  $scope.cusReqObj.ClientRequestLineDetails = [];


        // $scope.addItem();
    };
    $scope.addItem = function () {
        $scope.cusObj.DeductionDetails = [];
        //$scope.cusObj.DeductionDetails.push({ PremiumHolder: "", Premium: '', LoadingRate: "", DeductionRate: "", NetPremium: "" });
    };
    $scope.deleteItem = function (deleteIndex) {
        $scope.cusObj.DeductionDetails.splice(deleteIndex, 1);
    };

    $scope.loadClientRequestsByBUID = function (businessUnitID) {
        $scope.showLoader = true;
        ManageNestleRecordingService.getAllClientRequestsByBUID(businessUnitID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.availableClientRequests = results.data;
                if ($scope.availableClientRequests.length > 0) {
                    for (var i = 0; i < $scope.availableClientRequests.length; i++) {
                        if ($scope.availableClientRequests[i].PaymentDetails.length > 0) {
                            $scope.availableClientRequests[i].isManagePayment = false;
                        }
                        else {
                            $scope.availableClientRequests[i].isManagePayment = true;
                        }

                    }
                }

                $scope.data = angular.copy($scope.availableClientRequests);
                $scope.viewby = "10";
                $scope.totalItems = $scope.data.length;
                $scope.currentPage = 1;
                $scope.itemsPerPage = $scope.viewby;
                $scope.maxSize = 10; //Number of pager buttons to show

                $scope.setItemsPerPage($scope.viewby);
            }
            else {
                $scope.availableClientRequests = [];
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
        $scope.currentPage = 1;
    };

    $scope.searchTextChange = function (searchText) {
        $scope.data = filterFilter($scope.availableClientRequests, searchText);
        $scope.viewby = "10";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 10;
        $scope.setItemsPerPage($scope.viewby);
    };

    
    $scope.loadClientsByBUID = function (businessUnitID) {
        //$scope.showLoader = true;
        ManageNestleRecordingService.getAllClientsByBUID(businessUnitID).then(function (results) {
            //$scope.showLoader = false;
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableClients.push({ value: results.data[i].ClientID, text: results.data[i].ClientName })
                }
            }
            else {
                $scope.availableClients = [];
            }
        });
    };

    $scope.loadClientRequestsByBUID = function (businessUnitID) {
        $scope.showLoader = true;
        ManageNestleRecordingService.getAllClientRequestsByBUID(businessUnitID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.availableClientRequests = results.data;
                if ($scope.availableClientRequests.length > 0) {
                    for (var i = 0; i < $scope.availableClientRequests.length; i++) {
                        if ($scope.availableClientRequests[i].PaymentDetails.length > 0) {
                            $scope.availableClientRequests[i].isManagePayment = false;
                        }
                        else {
                            $scope.availableClientRequests[i].isManagePayment = true;
                        }

                    }
                }

                $scope.data = angular.copy($scope.availableClientRequests);
                $scope.viewby = "10";
                $scope.totalItems = $scope.data.length;
                $scope.currentPage = 1;
                $scope.itemsPerPage = $scope.viewby;
                $scope.maxSize = 10; //Number of pager buttons to show

                $scope.setItemsPerPage($scope.viewby);
            }
            else {
                $scope.availableClientRequests = [];
            }
        });
    };

   

    $scope.sendEmailRequest = function (insCompanyObj) {
        $modal.open({
            templateUrl: 'ngTemplateSendEmail',
            backdrop: 'static',
            //windowClass: 'app-modal-window-scopes',
            controller: [
                    '$scope', '$modalInstance', '$http', function ($scopeChild, $modalInstance, $http) {
                        $scopeChild.emailObj = {};
                        $scopeChild.insuranceCompany = angular.copy(insCompanyObj);
                        //  $scopeChild.insuranceCompany.InsCompanyName = "AVIVA";
                        $scopeChild.emailObj.userName = "AVIVA";
                        $scopeChild.emailObj.emailHeader = "Quotation Details";


                        /*------------------------------------TinyMCE Options-------------------------------------------*/
                        $scopeChild.tinymceOptions = {
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

                        $scopeChild.sendEmail = function () {
                            $scopeChild.showLoader = true;
                            noty({
                                text: 'Do you want to send email?',
                                layout: 'topCenter',
                                buttons: [
                                        {
                                            addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                                                $noty.close();

                                                ManageNestleRecordingService.sendEmailRequest($scopeChild.emailObj).then(function (results) {
                                                    $scopeChild.showLoader = false;

                                                    if (results.status === true) {
                                                        noty({
                                                            text: 'Email sent successfully',
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

                                                        $modalInstance.close();
                                                    }
                                                    else {
                                                        noty({
                                                            text: 'Error occurred in email sending',
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
                                        },
                                        {
                                            addClass: 'btn btn-danger btn-clean', text: 'Cancel', onClick: function ($noty) {
                                                $noty.close();
                                                $scope.$apply(function () {
                                                    $scopeChild.showLoader = false;
                                                });
                                            }
                                        }
                                ]
                            })
                        };

                        $scopeChild.cancel = function () {
                            $modalInstance.dismiss('cancel');
                        };
                    }
            ],
        });
    };

    $scope.loadClientByID = function (clientID) {
        $scope.showLoader = true;
        ManageNestleRecordingService.getClientByID(clientID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.isCustomerAvailable = true;
                $scope.isCustomerAdded = false;
                $scope.isCustomerUpdated = false;
                $scope.addItem();
                $scope.cusObj = results.data;
               // $scope.cusObj.DeductionDetails = [];

                if ($scope.cusReqObj.PaymentDetails.length > 0) {
                    $scope.cusObj.LoadnigRate = $scope.cusReqObj.PaymentDetails[0].LoadingRate;
                    $scope.cusObj.DeductionRate = $scope.cusReqObj.PaymentDetails[0].DeductionRate;
                    $scope.cusObj.DeductionID = $scope.cusReqObj.PaymentDetails[0].DeductionID;
                 
                }
                else {
                    
                    $scope.cusObj.DeductionDetails.push({ PremiumHolder: $scope.cusObj.ClientName, Premium: '', LoadingRate: "", DeductionRate: "", NetPremium: "", FamilyMemberID: "" });
                    for (var i = 0; i < $scope.cusReqObj.FamilyDetails.length; i++) {
                        $scope.cusObj.DeductionDetails.push({ PremiumHolder: $scope.cusObj.FamilyDetails[i].MemberName, Premium: "", LoadingRate: "", DeductionRate: "", NetPremium: "", FamilyMemberID: $scope.cusObj.FamilyDetails[i].FamilyMemberID });
                    
                }

                    //$scope.cusObj.DeductionDetails.push({ PremiumHolder: cusObj.ClientName, Premium: "", LoadingRate: "", DeductionRate: "", NetPremium: "", FamilyMemberID: ""});



                //if ($scope.cusObj.DeductionDetails.length >0) {
                //    $scope.cusObj.DeductionDetails.push({ PremiumHolder: $scope.cusObj.ClientName, Premium: '', LoadingRate: "", DeductionRate: "", NetPremium: "", FamilyMemberID: "" });
                   
                //        for (var j = 0; j < $scope.cusObj.FamilyDetails[j].length; j++) {
                //            $scope.cusObj.DeductionDetails.push({ PremiumHolder: $scope.cusObj.FamilyDetails[j].MemberName, Premium: "", LoadingRate: "", Deductible: "", NetPremium: "", FamilyMemberID: $scope.cusObj.FamilyDetails[j].FamilyMemberID });
                //        }

                    
                    
                }



                $scope.cusObj.HomeCountryID = results.data.HomeCountryID + "";
                $scope.cusObj.ResidentCountryID = results.data.ResidentCountryID + "";
            }
            else {
                $scope.isCustomerAvailable = false;
                $scope.isCustomerAdded = false;
                $scope.isCustomerUpdated = false;
                $scope.cusObj = {};
            }
        });
    };

    $scope.changeCustomer = function () {
        $scope.isCustomerAvailable = false;
        $scope.isCustomerAdded = false;
        $scope.isCustomerUpdated = false;

        $scope.cusObj = {};
        $scope.cusReqObj = {};
        //   $scope.cusReqObj.ClientRequestLineDetails = [];

        //$scope.addItem();
    };

    $scope.loadAgent = function () {
        //$scope.showLoader = true;
        ManageNestleRecordingService.loadAgent().then(function (results) {
            //$scope.showLoader = false;
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableAgents.push({ value: results.data[i].AgentID, text: results.data[i].AgentName })
                }
            }
            else {
                $scope.availablePartners = [];
            }
        });
    };

    $scope.loadPartners = function () {
        //$scope.showLoader = true;
        var bisid = $scope.businessUnitID;
        ManageNestleRecordingService.getAllPartners().then(function (results) {
            //$scope.showLoader = false;
            if (results.status === true) {


                for (var i = 0; i < results.data.length; i++) {
                    if (results.data[i].BUID === bisid) {

                        $scope.availablePartners.push({ value: results.data[i].PremiumID, text: results.data[i].PremiumName })
                    }
                }

            }
            else {
                $scope.availablePartners = [];
            }
        });
    };

    $scope.loadFamilyDiscount = function () {
        //$scope.showLoader = true;
        ManageNestleRecordingService.loadFamilyDiscount().then(function (results) {
            //$scope.showLoader = false;
            if (results.status === true) {

                $scope.cusReqObj.NewFamilyDiscount = results.data[0].FamilyDiscountVal;
                $scope.cusReqObj.NewFamilyDiscount=0;
            }
            else {
                $scope.availablePartners = [];
            }
        });
    };


    $scope.loadInsSubClassesByBUID = function (buid) {
        //$scope.showLoader = true;
        ManageNestleRecordingService.getAllInsSubClassesByBUID(buid).then(function (results) {
            //$scope.showLoader = false;
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableInsSubClasses.push({ value: results.data[i].InsuranceSubClassID, text: results.data[i].Description })
                }
            }
            else {
                $scope.availableInsSubClasses = [];
            }
        });
    };

    $scope.insSubClassChange = function (idx) {
        $scope.cusReqObj.ClientRequestLineDetails[idx].ClientRequestInsSubClassScopeDetails = [];
    };

    //$scope.addItem = function () {
    //    $scope.cusReqObj.ClientRequestLineDetails.push({ ClientRequestLineID: 0, InsSubClassID: "", ClientPropertyDetails: [], ClientRequestInsSubClassScopeDetails: [] });
    //};

    //$scope.deleteItem = function (deleteIndex) {
    //    $scope.cusReqObj.ClientRequestLineDetails.splice(deleteIndex, 1);
    //};


    $scope.manageCustomer = function (isEdit, customer, Child) {
        $modal.open({
            templateUrl: 'ngTemplateCustomer',
            backdrop: 'static',
            // windowClass: 'app-modal-window-pir',
            controller: [
                    '$scope', '$modalInstance', '$http', function ($scopeChild, $modalInstance, $http) {

                        $scopeChild.mode = "Add";
                        $scopeChild.isEditMode = false;
                        $scopeChild.customerObj = {};
                        $scopeChild.customerObj.childrenDetails = [];
                        $scopeChild.availableHomeCountries = [];
                        $scopeChild.availableResidentCountries = [];
                        $scopeChild.availableTitle = [];



                        $scopeChild.loadTitles = function () {

                            ManageNestleRecordingService.loadTitle().then(function (results) {

                                if (results.status === true) {
                                    for (var i = 0; i < results.data.length; i++) {
                                        $scopeChild.availableTitle.push({ value: results.data[i].TitleID, text: results.data[i].TitleName })
                                    }
                                }
                                else {
                                    $scopeChild.availableTitle = [];
                                }
                            });
                        };


                        $scopeChild.homeCountries = function () {
                            ManageNestleRecordingService.getAllCountries().then(function (results) {
                                if (results.status === true) {
                                    for (var i = 0; i < results.data.length; i++) {
                                        $scopeChild.availableHomeCountries.push({ value: results.data[i].CountryID, text: results.data[i].CountryName })

                                    }
                                }
                                else {
                                    $scopeChild.availableHomeCountries = [];
                                }
                            });
                        };

                        $scopeChild.residentCountries = function () {
                            ManageNestleRecordingService.getAllCountries().then(function (results) {
                                if (results.status === true) {
                                    for (var i = 0; i < results.data.length; i++) {
                                        $scopeChild.availableResidentCountries.push({ value: results.data[i].CountryID, text: results.data[i].CountryName })
                                    }
                                }
                                else {
                                    $scopeChild.availableResidentCountries = [];
                                }
                            });
                        };

                        $scopeChild.homeCountries();
                        $scopeChild.residentCountries();
                        $scopeChild.loadTitles();


                        if (isEdit) {
                            $scopeChild.mode = "Edit";
                            $scopeChild.isEditMode = true;
                            $scopeChild.customerObj = angular.copy(customer);

                            $scopeChild.updateCustomerDetails = function () {
                                $scope.isCustomerAvailable = true;

                                if ($scopeChild.customerObj.ClientID !== undefined) {
                                    $scope.isCustomerAdded = false;
                                    $scope.isCustomerUpdated = true;
                                    $scope.cusObj.ClientID = $scopeChild.customerObj.ClientID;
                                }
                                else {
                                    $scope.isCustomerAdded = true;
                                    $scope.isCustomerUpdated = false;
                                }

                                //$scopeChild.customerObj.DOB = $scope.getFormattedDate($scopeChild.customerObj.DOB);

                                $scope.cusObj.ClientName = $scopeChild.customerObj.ClientName;
                                $scope.cusObj.ClientAddress = $scopeChild.customerObj.ClientAddress;
                                $scope.cusObj.NIC = $scopeChild.customerObj.NIC;
                                $scope.cusObj.ContactNo = $scopeChild.customerObj.ContactNo;
                                $scope.cusObj.FixedLine = $scopeChild.customerObj.FixedLine;
                                $scope.cusObj.Email = $scopeChild.customerObj.Email;
                                $scope.cusObj.DOB = $scope.getFormattedDate($scopeChild.customerObj.DOB);
                                $scope.cusObj.PPID = $scopeChild.customerObj.PPID;
                                $scope.cusObj.FamilyDiscount = $scopeChild.customerObj.FamilyDiscount;
                                $scope.cusObj.AdditionalNote = $scopeChild.customerObj.AdditionalNote;
                                $scope.cusObj.HomeCountryID = $scopeChild.customerObj.HomeCountryID;
                                $scope.cusObj.ResidentCountryID = $scopeChild.customerObj.ResidentCountryID;

                                $modalInstance.close();
                            };
                        }
                        else {
                            $scopeChild.mode = "Add";
                            $scopeChild.isEditMode = false;
                            $scopeChild.customerObj = {};
                            $scopeChild.customerObj.childrenDetails = [];

                            $scopeChild.saveCustomerDetails = function () {

                                $scope.isCustomerAvailable = true;
                                $scope.isCustomerAdded = true;
                                $scope.isCustomerUpdated = false;

                                if ($scopeChild.customerObj.TitleID === undefined || $scopeChild.customerObj.TitleID === "" || $scopeChild.customerObj.TitleID === null) {
                                    $scope.cusObj.TitleID = "";
                                }
                                else
                                    $scope.cusObj.TitleID = $scopeChild.customerObj.TitleID;

                                if ($scopeChild.customerObj.ClientName === undefined || $scopeChild.customerObj.ClientName === "" || $scopeChild.customerObj.ClientName === null) {
                                    $scope.cusObj.ClientName = "";
                                }
                                else
                                    $scope.cusObj.ClientName = $scopeChild.customerObj.ClientName;

                                if ($scopeChild.customerObj.ClientAddress === undefined || $scopeChild.customerObj.ClientAddress === "" || $scopeChild.customerObj.ClientAddress === null) {
                                    $scope.cusObj.ClientAddress = "";
                                }
                                else
                                    $scope.cusObj.ClientAddress = $scopeChild.customerObj.ClientAddress;

                                if ($scopeChild.customerObj.NIC === undefined || $scopeChild.customerObj.NIC === "" || $scopeChild.customerObj.NIC === null) {
                                    $scope.cusObj.NIC = "";
                                }
                                else
                                    $scope.cusObj.NIC = $scopeChild.customerObj.NIC;

                                if ($scopeChild.customerObj.ContactNo === undefined || $scopeChild.customerObj.ContactNo === "" || $scopeChild.customerObj.ContactNo === null) {
                                    $scope.cusObj.ContactNo = "";
                                }
                                else
                                    $scope.cusObj.ContactNo = $scopeChild.customerObj.ContactNo;


                                if ($scopeChild.customerObj.FixedLine === undefined || $scopeChild.customerObj.FixedLine === "" || $scopeChild.customerObj.FixedLine === null) {
                                    $scope.cusObj.FixedLine = "";
                                }
                                else
                                    $scope.cusObj.FixedLine = $scopeChild.customerObj.FixedLine;


                                if ($scopeChild.customerObj.Email === undefined || $scopeChild.customerObj.Email === "" || $scopeChild.customerObj.Email === null) {
                                    $scope.cusObj.Email = "";
                                }
                                else
                                    $scope.cusObj.Email = $scopeChild.customerObj.Email;


                                if ($scopeChild.customerObj.DOB === undefined || $scopeChild.customerObj.DOB === "" || $scopeChild.customerObj.DOB === null) {
                                    $scope.cusObj.DOB = "";
                                }
                                else
                                    $scope.cusObj.DOB = $scope.getFormattedDate($scopeChild.customerObj.DOB);
                           



                                





                                if ($scopeChild.customerObj.PPID === undefined || $scopeChild.customerObj.PPID === "" || $scopeChild.customerObj.PPID === null) {
                                    $scope.cusObj.PPID = "";
                                }
                                else
                                    $scope.cusObj.PPID = $scopeChild.customerObj.PPID;


                                if ($scopeChild.customerObj.FamilyDiscount === undefined || $scopeChild.customerObj.FamilyDiscount === "" || $scopeChild.customerObj.FamilyDiscount === null) {
                                    $scope.cusObj.FamilyDiscount = 0;
                                }
                                else
                                    $scope.cusObj.FamilyDiscount = $scopeChild.customerObj.FamilyDiscount;



                                if ($scopeChild.customerObj.HomeCountryID === undefined || $scopeChild.customerObj.HomeCountryID === "" || $scopeChild.customerObj.HomeCountryID === null) {
                                    $scope.cusObj.HomeCountryID = "";
                                }
                                else
                                    $scope.cusObj.HomeCountryID = $scopeChild.customerObj.HomeCountryID;


                                if ($scopeChild.customerObj.ResidentCountryID === undefined || $scopeChild.customerObj.ResidentCountryID === "" || $scopeChild.customerObj.ResidentCountryID === null) {
                                    $scope.cusObj.ResidentCountryID = "";
                                }
                                else
                                    $scope.cusObj.ResidentCountryID = $scopeChild.customerObj.ResidentCountryID;

                                $modalInstance.close();

                            };
                        }

                        $scopeChild.cancel = function () {
                            if (isEdit) {
                                $scopeChild.customerObj = angular.copy(customer);
                            }

                            $modalInstance.dismiss('cancel');
                        };
                        $scopeChild.manageInsCompanyScopeDetails = function (idx) {
                            $modal.open({
                                templateUrl: 'ngTemplateInsCompanyScope',
                                backdrop: 'static',
                                windowClass: 'app-modal-window-pir',
                                controller: [
                                        '$scope', '$modalInstance', '$http', function ($scopeSubChild, $modalInstance, $http) {

                                            $scopeSubChild.quotationDetailsInsCompanyScopeTemp = [];
                                            $scopeSubChild.availableHomeCountries = [];
                                            $scopeSubChild.availableResidentCountries = [];
                                                        $scopeSubChild.availableTitle = [];
                                                         $scopeSubChild.GenderDetails = [];
                                                         $scopeSubChild.RelationshipDetails = [];


                                            $scopeChild.loadTitles = function () {

                                                ManageNestleRecordingService.loadTitle().then(function (results) {

                                                    if (results.status === true) {
                                                        for (var i = 0; i < results.data.length; i++) {
                                                            $scopeSubChild.availableTitle.push({ value: results.data[i].TitleID, text: results.data[i].TitleName })
                                                        }
                                                    }
                                                    else {
                                                        $scopeSubChild.availableTitle = [];
                                                    }
                                                });
                                            };

                                            $scopeSubChild.GetRealationship = function () {
                                                ManageNestleRecordingService.loadRelationship().then(function (results) {
                                                    if (results.status === true) {
                                                        for (var i = 0; i < results.data.length; i++) {
                                                            $scopeSubChild.RelationshipDetails.push({ value: results.data[i].RelationShipID, text: results.data[i].Relationship })
                                                        }
                                                    }
                                                    else {
                                                        $scope.RelationshipDetails = [];
                                                    }
                                                });
                                            };
                                           
                                            $scopeSubChild.getGenderDetails = function () {
                                                ManageNestleRecordingService.loadGender().then(function (results) {
                                                    if (results.status === true) {
                                                        for (var i = 0; i < results.data.length; i++) {
                                                            $scopeSubChild.GenderDetails.push({ value: results.data[i].GenderID, text: results.data[i].Gender })
                                                        }
                                                    }
                                                    else {
                                                        $scopeSubChild.GenderDetails = [];
                                                    }
                                                });
                                            };

                                            $scopeSubChild.addInsCompanyScopeDetails = function () {
                                                $scopeSubChild.quotationDetailsInsCompanyScopeTemp.push({ MemberName: "", MemberDOB: "" });
                                            };

                                            $scopeSubChild.deleteInsCompanyScopeDetails = function (deleteIndex) {
                                                $scopeSubChild.quotationDetailsInsCompanyScopeTemp.splice(deleteIndex, 1);
                                            };

                                            if (isEdit) {
                                                $scopeSubChild.quotationDetailsInsCompanyScopeTemp = angular.copy(customer.FamilyDetails);
                                            }
                                            else {
                                                $scopeSubChild.addInsCompanyScopeDetails();
                                            }
                                            $scopeSubChild.saveInsCompanyScopeDetails = function () {
                                                $scope.cusObj.ChildrenDetailss = $scopeSubChild.quotationDetailsInsCompanyScopeTemp;
                                                $modalInstance.close();
                                            };

                                            $scopeSubChild.cancel = function () {
                                                $modalInstance.dismiss('cancel');
                                            };


                                            $scopeSubChild.homeCountries = function () {
                                                ManageNestleRecordingService.getAllCountries().then(function (results) {
                                                    if (results.status === true) {
                                                        for (var i = 0; i < results.data.length; i++) {

                                                            $scopeSubChild.availableHomeCountries.push({ value: results.data[i].CountryID, text: results.data[i].CountryName })
                                                        }
                                                    }
                                                    else {
                                                        $scopeSubChild.availableHomeCountries = [];
                                                    }
                                                });
                                            };

                                            $scopeSubChild.residentCountries = function () {
                                                ManageNestleRecordingService.getAllCountries().then(function (results) {
                                                    if (results.status === true) {
                                                        for (var i = 0; i < results.data.length; i++) {
                                                            $scopeSubChild.availableResidentCountries.push({ value: results.data[i].CountryID, text: results.data[i].CountryName })
                                                        }
                                                    }
                                                    else {
                                                        $scopeSubChild.availableResidentCountries = [];
                                                    }
                                                });
                                            };

                                            $scopeSubChild.homeCountries();
                                            $scopeSubChild.residentCountries();
                                            $scopeChild.loadTitles();
                                            $scopeSubChild.getGenderDetails();
                                            $scopeSubChild.GetRealationship();

                                            //$scopeSubChild.availableHomeCountries = angular.copy($scopeChild.availableHomeCountries);
                                        }
                                ],
                            });
                        };

                    }
            ],
        });
    };

    $scope.manageBankDetails = function (BUID, ClientID, ClientReqID) {
        // $('#printable').hide();
        $modal.open({
            templateUrl: 'ngTemplateBank',
            backdrop: 'static',
            windowClass: 'app-modal-window-property',
            controller: [
                    '$scope', '$modalInstance', '$http', function ($scopeChild, $modalInstance, $http) {

                        $scopeChild.availableBanks = [];

                        //  $scope.cusReqObj.PaymentAmount = $scope.cusReqObj.PaymentDetails[0].PaymentAmount;

                        $scopeChild.loadBanks = function (BUID) {
                            //$scope.showLoader = true;
                            ManageNestleRecordingService.loadBanks(BUID).then(function (results) {
                                //$scope.showLoader = false;
                                if (results.status === true) {
                                    for (var i = 0; i < results.data.length; i++) {
                                        $scopeChild.availableBanks.push({ value: results.data[i].BankID, text: results.data[i].BankName })
                                    }
                                }
                                else {
                                    $scopeChild.availableBanks = [];
                                }
                            });
                        };


                        //$scope.showLoader = true;


                        ManageNestleRecordingService.getClientRequestByID(ClientReqID).then(function (results) {
                            $scope.showLoader = false;

                            if (results.status === true) {
                                $scope.cusReqObj = results.data;
                                //$scope.isFamilyDiscountApply = true;
                                if ($scope.cusReqObj.PaymentDetails.length > 0) {
                                    $scope.cusReqObj.PaymentAmount = $scope.cusReqObj.PaymentDetails[0].PaymentAmount;
                                    $scope.cusReqObj.PaymentID = $scope.cusReqObj.PaymentDetails[0].PaymentID;
                                }

                                $scopeChild.bankObj = {};


                                $scope.cusReqObj.IBSAmount = ($scope.cusReqObj.PaymentAmount * 25) / 100;
                                $scopeChild.bankObj.PaymentAmount = $scope.cusReqObj.PaymentAmount;
                                //  $scope.loadClientByID(ClientID);
                                ManageNestleRecordingService.getClientByID(ClientID).then(function (results) {
                                    $scope.showLoader = false;
                                    if (results.status === true) {
                                        $scope.cusReqObj.Email = results.data.Email;
                                        $scope.cusReqObj.ClientAddress = results.data.ClientAddress;
                                        $scope.cusReqObj.ContactNo = results.data.ContactNo;
                                        $scope.cusReqObj.FixedLine = results.data.FixedLine;
                                        $scope.cusReqObj.NIC = results.data.NIC;
                                        $scope.cusReqObj.DOB = results.data.DOB;
                                        $scopeChild.bankObj.Email = $scope.cusReqObj.Email;
                                        $scopeChild.bankObj.ClientID = $scope.cusReqObj.ClientID;
                                        $scopeChild.bankObj.ClientAddress = $scope.cusReqObj.ClientAddress;
                                        $scopeChild.bankObj.ContactNo = $scope.cusReqObj.ContactNo;
                                        $scopeChild.bankObj.FixedLine = $scope.cusReqObj.FixedLine;
                                        $scopeChild.bankObj.NIC = $scope.cusReqObj.NIC;
                                        $scopeChild.bankObj.DOB = $scope.cusReqObj.DOB;
                                        if (results.data.BankTransactionDetails.length > 0) {
                                            $scopeChild.bankObj.BankID = results.data.BankTransactionDetails[0].BankID;
                                            $scopeChild.bankObj.BankID = $scopeChild.bankObj.BankID + "";
                                            $scopeChild.bankObj.DraftNo = results.data.BankTransactionDetails[0].DraftNo;
                                            $scopeChild.bankObj.BankAmount = results.data.BankTransactionDetails[0].BankAmount;
                                            $scopeChild.bankObj.BankRate = results.data.BankTransactionDetails[0].BankRate;
                                            $scopeChild.bankObj.SGSAmount = results.data.BankTransactionDetails[0].SGSAmount;
                                            $scopeChild.bankObj.BankDetailID = results.data.BankTransactionDetails[0].BankDetailID;
                                            if ($scopeChild.bankObj.BankDetailID > 0) {
                                                $scopeChild.IsPayment = true;
                                            }
                                            else {
                                                $scopeChild.IsPayment = false;
                                            }

                                        }

                                    }
                                });
                                $scope.cusReqObj.DebitNoteDetails = [];
                                //  $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: "", DebitNoteAmount: "" })
                                $scopeChild.bankObj.BankAmount = $scope.cusReqObj.BankAmount;
                                $scopeChild.bankObj.AgentAmount = $scope.cusReqObj.AgentAmount;
                                $scopeChild.bankObj.IBSAmount = $scope.cusReqObj.IBSAmount;
                                $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: $scope.cusReqObj.PaymentID, TotalGrossPremium: $scope.cusReqObj.IBSAmount })
                                $scopeChild.bankObj.ClientName = $scope.cusReqObj.ClientName;

                                ManageNestleRecordingService.getAgentByID($scope.cusReqObj.AgentID).then(function (results) {

                                    if (results.status === true) {
                                        //  $scope.cusReqObj.DebitNoteDetails = [];
                                        //  $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: "", DebitNoteAmount: "" })
                                        $scope.cusReqObj.AgentRate = 0;
                                        //$scopeChild.bankObj.AgentRate = $scope.cusReqObj.AgentRate;
                                        //$scope.cusReqObj.AgentAmount = ($scope.cusReqObj.PaymentAmount * $scope.cusReqObj.AgentRate) / 100;
                                        //$scopeChild.bankObj.AgentAmount = $scope.cusReqObj.AgentAmount;
                                        $scopeChild.bankObj.AgentRate = 0;
                                        $scope.cusReqObj.AgentAmount = ($scope.cusReqObj.PaymentAmount * $scope.cusReqObj.AgentRate) / 100;
                                        $scopeChild.bankObj.AgentAmount = 0;

                                        $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: $scope.cusReqObj.PaymentID, TotalGrossPremium: $scope.cusReqObj.AgentAmount })
                                    }
                                });
                                $scopeChild.loadBankByID = function (bankID) {
                                    ManageNestleRecordingService.loadBankByID(bankID).then(function (results) {

                                        if (results.status === true) {
                                            //     $scope.cusReqObj.DebitNoteDetails = [];
                                            //    $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: "", DebitNoteAmount: "" })
                                            $scope.cusReqObj.BankRate = results.data.DiscountRatio;
                                            $scopeChild.bankObj.BankRate = $scope.cusReqObj.BankRate;
                                            $scope.cusReqObj.BankAmount = ($scope.cusReqObj.PaymentAmount * $scope.cusReqObj.BankRate) / 100;
                                            $scopeChild.bankObj.BankAmount = $scope.cusReqObj.BankAmount;
                                            //$scopeChild.bankObj.SGSAmount = ($scope.cusReqObj.PaymentAmount * (100 - ($scope.cusReqObj.BankRate + $scope.cusReqObj.AgentRate + 25))) / 100;
                                            $scopeChild.bankObj.SGSAmount = ($scope.cusReqObj.PaymentAmount * (15 / 100));
                                        }
                                    });
                                }


                                $scopeChild.saveBankDetails = function () {
                                    $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: $scope.cusReqObj.PaymentID, TotalGrossPremium: $scope.cusReqObj.BankAmount })
                                    $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: $scope.cusReqObj.PaymentID, TotalGrossPremium: $scopeChild.bankObj.SGSAmount })

                                    ManageNestleRecordingService.saveBankTransaction($scopeChild.bankObj).then(function (results) {
                                        if (results.status === true) {
                                            ManageNestleRecordingService.savePayment($scope.cusReqObj, $scope.currentUser.UserID).then(function (results) {
                                                // $scope.showLoader = false;
                                                //alert(angular.toJson(results));
                                                if (results.status === true) {
                                                    noty({
                                                        text: 'Successfully Saved Payment Details',
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
                                                    $scope.activateClientRequestListTab();
                                                }
                                                else {
                                                    noty({
                                                        text: 'Successfully Saved Payment Details',
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
                                                    $scope.activateClientRequestListTab();
                                                }
                                            });
                                        }
                                        else {
                                            noty({
                                                text: 'Error Saving Customer Payment Details',
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
                                $scopeChild.updateBankDetails = function () {
                                    ManageNestleRecordingService.saveBankTransaction($scopeChild.bankObj).then(function (results) {
                                        if (results.status === true) {
                                            noty({
                                                text: 'Successfully Saved Payment Details',
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
                                            $scope.activateClientRequestListTab();
                                        }
                                        else {
                                            noty({
                                                text: 'Error Saving Customer Payment Details',
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
                                        // }
                                    });
                                }
                            }
                        });

                        $scopeChild.loadBanks(BUID);




                        $scopeChild.cancel = function () {
                            $modalInstance.dismiss('cancel');
                        };
                        $scopeChild.managePolicyInfoChargeDetails = function () {
                            $scopeChild.bankObj = {};
                            $scope.cusReqObj.BankAmount = ($scope.cusReqObj.PaymentAmount * 5) / 100;
                            $scope.cusReqObj.AgentAmount = ($scope.cusReqObj.PaymentAmount * 10) / 100;
                            $scope.cusReqObj.IBSAmount = ($scope.cusReqObj.PaymentAmount * 25) / 100;
                            $scopeChild.bankObj.BankAmount = $scope.cusReqObj.BankAmount;
                            $scopeChild.bankObj.AgentAmount = $scope.cusReqObj.AgentAmount;
                            $scopeChild.bankObj.IBSAmount = $scope.cusReqObj.IBSAmount;
                        }
                        $scopeChild.printDiv = function (divName) {
                            var printContents = document.getElementById(divName).innerHTML;
                            var popupWin = window.open('', '_blank', 'width=300,height=300');
                            popupWin.document.open();
                            popupWin.document.write('<html><head><link rel="stylesheet" type="text/css" href="style.css" /></head><body onload="window.print()">' + printContents + '</body></html>');
                            popupWin.document.close();
                        }

                        $scopeChild.managePolicyInfoChargeDetailss = function (idx) {
                            $modal.open({
                                templateUrl: 'ngTemplateAgentDetails',
                                backdrop: 'static',
                                windowClass: 'app-modal-window-pic',
                                controller: [
                                        '$scope', '$modalInstance', '$http', function ($scopeSubChild, $modalInstance, $http) {



                                            $scopeSubChild.cancel = function () {
                                                $modalInstance.dismiss('cancel');
                                            };
                                        }
                                ],
                            });
                        };
                    }
            ],
        });
    };

    $scope.manageProperties = function (idx) {
        $modal.open({
            templateUrl: 'ngTemplateProperty',
            backdrop: 'static',
            windowClass: 'app-modal-window-property',
            controller: [
                    '$scope', '$modalInstance', '$http', function ($scopeChild, $modalInstance, $http) {

                        $scopeChild.clientPropertyDetailsTemp = [];

                        $scopeChild.addProperty = function () {
                            $scopeChild.clientPropertyDetailsTemp.push({ ClientPropertyName: "", BRNo: "", VATNo: "" });
                        };

                        $scopeChild.deleteProperty = function (deleteIndex) {
                            $scopeChild.clientPropertyDetailsTemp.splice(deleteIndex, 1);
                        };

                        if ($scope.cusReqObj.ClientRequestLineDetails[idx].ClientPropertyDetails.length > 0) {
                            $scopeChild.clientPropertyDetailsTemp = angular.copy($scope.cusReqObj.ClientRequestLineDetails[idx].ClientPropertyDetails);
                        }
                        else {
                            $scopeChild.addProperty();
                        }

                        $scopeChild.savePropertyDetails = function () {
                            $scope.cusReqObj.ClientRequestLineDetails[idx].ClientPropertyDetails = $scopeChild.clientPropertyDetailsTemp;
                            $modalInstance.close();
                        };

                        $scopeChild.cancel = function () {
                            $modalInstance.dismiss('cancel');
                        };
                    }
            ],
        });
    };

    $scope.manageScopes = function (idx, insSubClassID) {
        $modal.open({
            templateUrl: 'ngTemplateScopes',
            backdrop: 'static',
            //windowClass: 'app-modal-window-scopes',
            controller: [
                    '$scope', '$modalInstance', '$http', function ($scopeChild, $modalInstance, $http) {

                        $scopeChild.insSubClassScopeDetailsTemp = [];
                        $scopeChild.availableInsSubClassScopes = [];

                        $scopeChild.loadInsSubClassScopes = function (insSubClassID) {
                            $scopeChild.showLoader = true;
                            ManageNestleRecordingService.getAllInsSubClassScope(insSubClassID).then(function (results) {
                                $scopeChild.showLoader = false;
                                if (results.status === true) {
                                    for (var i = 0; i < results.data.length; i++) {
                                        $scopeChild.availableInsSubClassScopes.push({ value: results.data[i].CommonInsuranceScopeID, text: results.data[i].Description })
                                    }
                                }
                                else {
                                    $scopeChild.availableInsSubClassScopes = [];
                                }
                            });
                        };

                        $scopeChild.loadInsSubClassScopes(insSubClassID);

                        $scopeChild.addScope = function () {
                            $scopeChild.insSubClassScopeDetailsTemp.push({ CommonInsScopeID: "" });
                        };

                        $scopeChild.deleteScope = function (deleteIndex) {
                            $scopeChild.insSubClassScopeDetailsTemp.splice(deleteIndex, 1);
                        };

                        if ($scope.cusReqObj.ClientRequestLineDetails[idx].ClientRequestInsSubClassScopeDetails.length > 0) {
                            for (var i = 0; i < $scope.cusReqObj.ClientRequestLineDetails[idx].ClientRequestInsSubClassScopeDetails.length; i++) {
                                $scope.cusReqObj.ClientRequestLineDetails[idx].ClientRequestInsSubClassScopeDetails[i].CommonInsScopeID = $scope.cusReqObj.ClientRequestLineDetails[idx].ClientRequestInsSubClassScopeDetails[i].CommonInsScopeID + "";
                            }

                            $scopeChild.insSubClassScopeDetailsTemp = angular.copy(($scope.cusReqObj.ClientRequestLineDetails[idx].ClientRequestInsSubClassScopeDetails));
                        }
                        else {
                            $scopeChild.addScope();
                        }

                        $scopeChild.saveScopeDetails = function () {
                            $scope.cusReqObj.ClientRequestLineDetails[idx].ClientRequestInsSubClassScopeDetails = $scopeChild.insSubClassScopeDetailsTemp;
                            $modalInstance.close();
                        };

                        $scopeChild.cancel = function () {
                            $modalInstance.dismiss('cancel');
                        };
                    }
            ],
        });
    };

    $scope.getFormattedDate = function (date) {
        if (/^\d{2}\/\d{2}\/\d{4}$/.test(date)) {
            return date;
        }
        else {
            var stringDate = date.getDate() + "";
            var stringMonth = date.getMonth() + 1 + "";
            var stringYear = date.getFullYear() + "";

            if (stringDate.length < 2)
                stringDate = '0' + stringDate;
            if (stringMonth.length < 2)
                stringMonth = '0' + stringMonth;

            return [stringDate, stringMonth, stringYear].join('/');
        }
    };

    $scope.saveCustomerRequest = function () {
        $scope.showLoader = true;
        noty({
            text: 'Do you want to Save Customer Request Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            //   $scope.cusObj.ChildrenDetailss = [];
                            $scope.cusObj.FamilyDetails = [];
                            if ($scope.cusObj.FamilyDiscount === undefined || $scope.cusObj.FamilyDiscount === "" || $scope.cusObj.FamilyDiscount === null) {
                                $scope.cusObj.FamilyDiscount = 0;
                            }
                            if ($scope.cusObj.ChildrenDetailss != undefined) {
                                for (var i = 0; i < $scope.cusObj.ChildrenDetailss.length; i++) {

                                    if ($scope.cusObj.ChildrenDetailss[i].MemberDOB === undefined ||$scope.cusObj.ChildrenDetailss[i].MemberDOB === "" || $scope.cusObj.ChildrenDetailss[i].MemberDOB=== null) {
                                        $scope.cusObj.ChildrenDetailss[i].MemberDOB= "";
                                    }
                                    else
                                    {
                                    $scope.cusObj.ChildrenDetailss.MemberDOB = $scope.getFormattedDate($scope.cusObj.ChildrenDetailss[i].MemberDOB);

                                    }
                                    $scope.cusObj.FamilyDetails.push({ MemberName: $scope.cusObj.ChildrenDetailss[i].MemberName, MemberDOB: $scope.cusObj.ChildrenDetailss.MemberDOB })
                                }
                            }

                            $scope.cusObj.BusinessUnitID = $scope.businessUnitID;

                            if ($scope.cusReqObj.RequestedDate === undefined || $scope.cusReqObj.RequestedDate === "" || $scope.cusReqObj.RequestedDate === null) {
                                $scope.cusReqObj.RequestedDate = "";
                            }

                            else
                                $scope.cusReqObj.RequestedDate = $scope.getFormattedDate($scope.cusReqObj.RequestedDate);

                            if ($scope.cusReqObj.InspectionDate === undefined || $scope.cusReqObj.InspectionDate === "" || $scope.cusReqObj.InspectionDate === null) {
                                $scope.cusReqObj.InspectionDate = "";
                            }
                            else
                                $scope.cusReqObj.InspectionDate = $scope.getFormattedDate($scope.cusReqObj.InspectionDate);

                            if ($scope.cusReqObj.joinDate === undefined || $scope.cusReqObj.joinDate === "" || $scope.cusReqObj.joinDate === null) {
                                $scope.cusReqObj.joinDate = "";
                            }
                            else
                                $scope.cusReqObj.joinDate = $scope.getFormattedDate($scope.cusReqObj.joinDate);

                            if ($scope.cusReqObj.PolicyStartDate === undefined || $scope.cusReqObj.PolicyStartDate === "" || $scope.cusReqObj.PolicyStartDate === null) {
                                $scope.cusReqObj.PolicyStartDate = "";
                            }
                            else
                                $scope.cusReqObj.PolicyStartDate = $scope.getFormattedDate($scope.cusReqObj.PolicyStartDate);

                            if ($scope.cusReqObj.PolicyEndDate === undefined || $scope.cusReqObj.PolicyEndDate === "" || $scope.cusReqObj.PolicyEndDate === null) {
                                $scope.cusReqObj.PolicyEndDate = "";
                            }
                            else
                                $scope.cusReqObj.PolicyEndDate = $scope.getFormattedDate($scope.cusReqObj.PolicyEndDate);

                            $scope.cusReqObj.FileNo = $scope.cusReqObj.FileNo;
                            //  $scope.cusReqObj.RequestedDate = "09/05/2018";

                            ManageNestleRecordingService.saveClientRequest($scope.isCustomerUpdated, $scope.isCustomerAdded, $scope.cusObj, $scope.cusReqObj, $scope.currentUser.UserID).then(function (results) {
                                $scope.showLoader = false;
                                //alert(angular.toJson(results));
                                if (results.status === true) {
                                    noty({
                                        text: 'Successfully Saved Customer Request Details',
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
                                    $scope.activateClientRequestListTab();
                                }
                                else {
                                    noty({
                                        text: 'Error Saving Customer Request Details',
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
                    },
                    {
                        addClass: 'btn btn-danger btn-clean', text: 'Cancel', onClick: function ($noty) {
                            $noty.close();
                            $scope.$apply(function () {
                                $scope.showLoader = false;
                            });
                        }
                    }
            ]
        })
    };

    $scope.activateClientRequestListTab = function () {
        $("#list-tab-1").addClass('active');
        $("#tab-1").addClass('tab-pane active');

        $("#list-tab-2").removeClass('active');
        $("#tab-2").removeClass('tab-pane active');

        $("#tab-1").css("display", "block");
        $("#tab-2").css("display", "none");
    };

    $scope.activateNewClientRequestTab = function () {
        $("#list-tab-1").removeClass('active');
        $("#tab-1").removeClass('tab-pane active');

        $("#list-tab-2").addClass('active');
        $("#tab-2").addClass('tab-pane active');

        $("#tab-1").css("display", "none");
        $("#tab-2").css("display", "block");
    };

    $scope.editClientRequest = function (clientReqHeaderID, clientID) {
        $scope.activateNewClientRequestTab();
        $scope.cusObj.DeductionDetails = [];
        $scope.showLoader = true;
        ManageNestleRecordingService.getClientRequestByID(clientReqHeaderID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.isViewMode = false;
                $scope.isClientReqAddMode = false;

                $scope.cusReqObj = results.data;
                $scope.cusObj.DeductionDetails.push({ PremiumHolder: $scope.cusReqObj.ClientName, Premium: '', LoadingRate: "", DeductionRate: "", NetPremium: "" });
                for (var i = 0; i < $scope.cusReqObj.FamilyDetails.length; i++) {
                    $scope.cusObj.DeductionDetails.push({ PremiumHolder: $scope.cusReqObj.FamilyDetails[i].MemberName, Premium: "", LoadingRate: "", DeductionRate: "", NetPremium: "" });
                }

                // $scope.addItem();
                $scope.cusReqObj.PartnerID = $scope.cusReqObj.PartnerID + "";
                if ($scope.cusReqObj.PaymentDetails.length > 0) {
                    $scope.cusReqObj.PaymentAmount = $scope.cusReqObj.PaymentDetails[0].PaymentAmount;
                    $scope.cusReqObj.PaymentID = $scope.cusReqObj.PaymentDetails[0].PaymentID;

                }
                else {
                    $scope.cusReqObj.PaymentAmount = $scope.cusReqObj.PremiumName;
                }
                $scope.deleteItem(0);
                $scope.loadClientByID(clientID);

               


                $scope.cusReqObj.FamilyDiscountDetails = [];
                $scope.cusReqObj.FamilyDiscountDetailsnt = [];
                //if ($scope.cusReqObj.FamilyDetails.length > 0) {
                //    for (var i = 0; i < $scope.cusReqObj.FamilyDetails.length; i++) {

                //        $scope.cusReqObj.FamilyDiscountDetailsnt = [];
                //        var myString = $scope.cusReqObj.FamilyDetails[i].MemberDOB;
                //        var arr = myString.split('/');
                //        $scope.cusReqObj.FamilyDetails.MemberDOB = arr[2];
                //        var currentyear = (new Date()).getFullYear();
                //        var Age = currentyear - $scope.cusReqObj.FamilyDetails.MemberDOB;

                //        $scope.policyCommissionPayment.PremiumAmount = 0.0;

                     
                //        if (Age < 25) {

                //            $scope.cusReqObj.FamilyDiscountDetails.push({ FamilyDiscountDetail: "Family Discount Applicable for this Customer" });
                //        }
                //        else {

                //            $scope.cusReqObj.FamilyDiscountDetailsnt.push({ FamilyDiscountDetail: "Family Discount Not Applicable for this Customer" });
                //        }
                //        if ($scope.cusReqObj.FamilyDiscountDetails.length > 0) {
                //            $scope.isFamilyDiscountApply = true;
                //            $scope.cusReqObj.FamilyDiscountStatus = $scope.cusReqObj.FamilyDiscountDetails[0].FamilyDiscountDetail;
                //        }
                //        else {
                //            $scope.isFamilyDiscountApply = false;
                //            $scope.cusReqObj.FamilyDiscountStatus = $scope.cusReqObj.FamilyDiscountDetailsnt[0].FamilyDiscountDetail;
                //        }
                //    }

                //}
                //else {
                //    $scope.cusReqObj.FamilyDiscountDetailsnt.push({ FamilyDiscountDetail: "Family Discount Not Applicable for this Customer" });
                //    $scope.cusReqObj.FamilyDiscountStatus = $scope.cusReqObj.FamilyDiscountDetailsnt[0].FamilyDiscountDetail;
                //}
                //$scope.loadFamilyDiscount();
                $scope.loadFamilyDiscount();
            }
            else {
                $scope.cusReqObj = {};
            }
        });
    };

    $scope.checkInputRateByValue = function () {
        if ($scope.cusReqObj.Loading > 100) {
            $scope.cusReqObj.Loading = $scope.cusReqObj.Loading / 10;
        }
    };
    $scope.checkInputDeduction = function () {
        if ($scope.cusReqObj.Deduction > 100) {
            $scope.cusReqObj.Deduction = $scope.cusReqObj.Deduction / 10;
        }
    };

    $scope.CalculateProRate = function () {

        var from_date = new Date("2018-07-01");
        var to_date = new Date("2019-06-30");
        var joinDate = new Date("2019-03-20");
        if (joinDate > from_date && joinDate < to_date) {

            if (joinDate > from_date) {
                var timediff = Math.abs(to_date.getTime() - joinDate.getTime());
                var noofdays = Math.ceil(timediff / (1000 * 3600 * 24));
                var proRate = parseFloat(noofdays) / 365;
                $scope.ProRate = parseFloat(proRate);
            }


        }
        else {

        }

    };


    $scope.calculateNetPremium = function (PaymentDetails) {
        var PaymentAmount = 0;
        $scope.LoadingRateAmount = "";
        

        for (var i = 0; i < PaymentDetails.length; i++) {
                $scope.CalculateProRate();
                var rate = 0;
                rate = $scope.ProRate;

            if (PaymentDetails[i].LoadingRate === undefined) {
                $scope.cusReqObj.AftLoading = PaymentDetails[i].PremiumAmount;
                $scope.LoadingRateAmount = 0;
            }
            else {
                $scope.cusReqObj.Prem = parseInt(PaymentDetails[i].PremiumAmount) * rate;
                $scope.cusReqObj.AfLoading = (PaymentDetails[i].PremiumAmount * (PaymentDetails[i].LoadingRate)) / 100;
                $scope.cusReqObj.AftLoading = $scope.cusReqObj.Prem + $scope.cusReqObj.AfLoading;
                $scope.LoadingRateAmount = (PaymentDetails[i].PremiumAmount * (PaymentDetails[i].LoadingRate)) / 100;
            }
            if (PaymentDetails[i].DeductionRate === undefined) {
                $scope.cusReqObj.AftDeduction = $scope.cusReqObj.AftLoading;
            }
            else {
                $scope.cusReqObj.AftDeduction = ($scope.cusReqObj.AftLoading * (100 - PaymentDetails[i].DeductionRate)) / 100;
            }

            if ($scope.cusReqObj.FamilyDiscountDetails.length > 0) {
                PaymentDetails[i].NetPremium = ($scope.cusReqObj.AftDeduction * (100 - $scope.cusReqObj.NewFamilyDiscount)) / 100;
                PaymentAmount = PaymentAmount + PaymentDetails[i].NetPremium;
               
                $scope.FamilyDiscountAmount = $scope.cusReqObj.NewFamilyDiscount;
            }
            else {
                PaymentDetails[i].NetPremium = $scope.cusReqObj.AftDeduction;
                PaymentAmount = PaymentAmount + $scope.cusReqObj.AftDeduction;
                $scope.FamilyDiscountAmount = "0";
            }

            $scope.cusReqObj.PaymentAmount = PaymentAmount;
        }
    }

    $scope.updateCustomerRequest = function () {
        $scope.showLoader = true;
        noty({
            text: 'Do you want to Update Customer Request Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();

                            if ($scope.cusObj.FamilyDiscount === undefined || $scope.cusObj.FamilyDiscount === "" || $scope.cusObj.FamilyDiscount === null) {
                                $scope.cusObj.FamilyDiscount = 0;
                            }
                            //    $scope.cusObj.DeductionRate = 0;
                            //    $scope.cusObj.LoadingRate = 0;
                            $scope.cusObj.DeductionID = 111;
                            $scope.cusObj.MemberID = 111;
                            $scope.cusObj.PolicyInfoID = 111;
                            //    $scope.cusObj.PaymentID = 111;
                            //   $scope.cusReqObj.PaymentID = 111;
                           
                            $scope.cusObj.BusinessUnitID = $scope.businessUnitID;
                            if ($scope.cusObj.PaymentDetails.length > 0) {

                            }
                            if ($scope.cusObj.ChildrenDetailss != undefined) {

                                for (var i = 0; i < $scope.cusObj.ChildrenDetailss.length; i++) {

                                    if ($scope.cusObj.ChildrenDetailss[i].MemberDOB === undefined || $scope.cusObj.ChildrenDetailss[i].MemberDOB === "" || $scope.cusObj.ChildrenDetailss[i].MemberDOB === null) {
                                        $scope.cusObj.ChildrenDetailss[i].MemberDOB = "";

                                    }
                                    else
                                    $scope.cusObj.ChildrenDetailss[i].MemberDOB = $scope.getFormattedDate($scope.cusObj.ChildrenDetailss[i].MemberDOB);
                                    //if ($scope.cusObj.ChildrenDetailss[i].FamilyMemberID > 0) {
                                    $scope.cusReqObj.FamilyDetails = $scope.cusObj.ChildrenDetailss;
                                    //   }

                                }

                                //    for (var i = 0; i < $scope.cusObj.ChildrenDetailss.length; i++) {
                                //        if ($scope.cusObj.ChildrenDetailss[i].FamilyMemberID > 0) {

                                //       }
                                //        else{
                                //            $scope.cusObj.ChildrenDetailss.MemberDOB = $scope.getFormattedDate($scope.cusObj.ChildrenDetailss[i].MemberDOB);
                                //            $scope.cusReqObj.FamilyDetails.push({ MemberName: $scope.cusObj.ChildrenDetailss[i].MemberName, MemberDOB: $scope.cusObj.ChildrenDetailss.MemberDOB })
                                //        }


                                //}

                            }

                            //  $scope.cusObj.FamilyDetails = $scope.cusObj.ChildrenDetailss;
                            //  $scope.cusReqObj.FamilyDetails = $scope.cusObj.ChildrenDetailss;


                            

                            if ($scope.cusReqObj.RequestedDate === undefined || $scope.cusReqObj.RequestedDate === "" || $scope.cusReqObj.RequestedDate === null)
                            {
                                $scope.cusReqObj.RequestedDate  = "";
                            }
                            else 
                                $scope.cusReqObj.RequestedDate = $scope.getFormattedDate($scope.cusReqObj.RequestedDate);

                            if ($scope.cusReqObj.InspectionDate === undefined || $scope.cusReqObj.InspectionDate === "" ||  $scope.cusReqObj.InspectionDate  === null) {
                                $scope.cusReqObj.InspectionDate   = "";
                            }
                            else 
                                


                                $scope.cusReqObj.InspectionDate = $scope.getFormattedDate($scope.cusReqObj.InspectionDate);
                            if ($scope.cusReqObj.JoinDate === undefined || $scope.cusReqObj.JoinDate === "" || $scope.cusReqObj.JoinDate === null) {
                                $scope.cusReqObj.JoinDate = "";
                            }
                            else



                                $scope.cusReqObj.JoinDate = $scope.getFormattedDate($scope.cusReqObj.JoinDate);
                            //$scope.cusReqObj.AftLoading = ($scope.cusReqObj.PremiumName * (100 - $scope.cusReqObj.Loading)) / 100;
                            //$scope.cusReqObj.AftDeduction = ($scope.cusReqObj.AftLoading * (100 - $scope.cusReqObj.Deduction)) / 100;

                            // $scope.cusReqObj.Loading = $scope.availablePartners[0].text;


                          
                            ManageNestleRecordingService.updateClientRequest($scope.isCustomerUpdated, $scope.isCustomerAdded, $scope.cusObj, $scope.cusReqObj, $scope.currentUser.UserID).then(function (results) {
                                $scope.showLoader = false;
                                //alert(angular.toJson(results));
                                if (results.status === true) {
                                    noty({
                                        text: 'Successfully Updated Customer Request Details',
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
                                    $scope.activateClientRequestListTab();
                                  
                                }
                                else {
                                    noty({
                                        text: 'Error Updating Customer Request Details',
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
                    },
                    {
                        addClass: 'btn btn-danger btn-clean', text: 'Cancel', onClick: function ($noty) {
                            $noty.close();
                            $scope.$apply(function () {
                                $scope.showLoader = false;
                            });
                        }
                    }
            ]
        })
    };

    $scope.cancelCustomerRequest = function () {
        $scope.ClearFields();
        $scope.refreshContent();
        $scope.activateClientRequestListTab();
    };

    $scope.viewClientRequest = function (clientReqHeaderID, clientID) {
        $scope.activateNewClientRequestTab();

        $scope.showLoader = true;
        ManageNestleRecordingService.getClientRequestByID(clientReqHeaderID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.isViewMode = true;
                $scope.isClientReqAddMode = false;
                $scope.cusReqObj = results.data;
                $scope.cusReqObj.PartnerID = $scope.cusReqObj.PartnerID + "";
                $scope.loadClientByID(clientID);
                if ($scope.cusReqObj.PaymentDetails.length > 0) {
                    $scope.cusReqObj.PaymentAmount = $scope.cusReqObj.PaymentDetails[0].PaymentAmount;
                }
                else {
                    $scope.cusReqObj.PaymentAmount = $scope.cusReqObj.PremiumName;
                }
                $scope.loadClientByID(clientID);
                $scope.cusReqObj.FamilyDiscountDetails = [];
                $scope.cusReqObj.FamilyDiscountDetailsnt = [];
                //if ($scope.cusReqObj.FamilyDetails.length > 0) {
                //    for (var i = 0; i < $scope.cusReqObj.FamilyDetails.length; i++) {

                //        $scope.cusReqObj.FamilyDiscountDetailsnt = [];
                //        var myString = $scope.cusReqObj.FamilyDetails[i].MemberDOB;
                //        var arr = myString.split('/');
                //        $scope.cusReqObj.FamilyDetails.MemberDOB = arr[2];
                //        var currentyear = 2018;
                //        var Age = currentyear - $scope.cusReqObj.FamilyDetails.MemberDOB;
                //        if (Age < 25) {
                //            $scope.isFamilyDiscountApply = true;
                //            $scope.cusReqObj.FamilyDiscountDetails.push({ FamilyDiscountDetail: "Family Discount Applicable for this Customer" });
                //        }
                //        else {
                //            $scope.isFamilyDiscountApply = false;
                //            $scope.cusReqObj.FamilyDiscountDetailsnt.push({ FamilyDiscountDetail: "Family Discount Not Applicable for this Customer" });
                //        }
                //        if ($scope.cusReqObj.FamilyDiscountDetails.length > 0) {
                //            $scope.cusReqObj.FamilyDiscountStatus = $scope.cusReqObj.FamilyDiscountDetails[0].FamilyDiscountDetail;
                //        }
                //        else {
                //            $scope.cusReqObj.FamilyDiscountStatus = $scope.cusReqObj.FamilyDiscountDetailsnt[0].FamilyDiscountDetail;
                //        }
                //    }

                //}
                //else {
                //    $scope.cusReqObj.FamilyDiscountDetailsnt.push({ FamilyDiscountDetail: "Family Discount Not Applicable for this Customer" });
                //    $scope.cusReqObj.FamilyDiscountStatus = $scope.cusReqObj.FamilyDiscountDetailsnt[0].FamilyDiscountDetail;
                //}
                $scope.loadFamilyDiscount();
            }
            else {
                $scope.cusReqObj = {};
            }
        });
    };

    $scope.initializeQuotation = function (clientReqHeaderID) {
        $scope.quotationHeader = {};
        $scope.quotationLineDetails = [];

        $scope.quotationHeader = { "ClientRequestHeaderID": clientReqHeaderID, "Status": true, "QuotationLineDetails": $scope.quotationLineDetails };

        $scope.showLoader = true;
        ManageNestleRecordingService.initializeQuotation($scope.quotationHeader, $scope.currentUser.UserID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.quotationHeader = {};
                noty({
                    text: 'Quotation is initialized successfully',
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

                $scope.refreshContent();
            }

            else {
                noty({
                    text: 'Error occured in quotation initialization',
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

   
});

ibmsApp.filter('return_status', function ($sce) {
    return function (text, length, end) {
        if (text) {
            return $sce.trustAsHtml('<span><i style="color:green" class="glyphicon glyphicon-ok"></i></span>');
        }
        return $sce.trustAsHtml('<span><i style="color:red" class="glyphicon glyphicon-remove"></i></span>');
    }
});
