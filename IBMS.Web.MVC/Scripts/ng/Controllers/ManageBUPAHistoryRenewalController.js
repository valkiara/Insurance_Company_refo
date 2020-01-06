'use strict';

ibmsApp.controller("ManageBUPARenewalController", function ($scope, $http, $rootScope, ManageTransactionsService, $location, AuthService, filterFilter, $modal) {

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
                $scope.loadInsSubClassesByBUID($scope.businessUnitID);
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
        $scope.IsBankVisible = false;
        $scope.IsTextBoxvisible = false;

        $scope.businessUnitID = "";
        $scope.availableClients = [];
        $scope.availablePartners = [];
        $scope.availableAgents = [];
        $scope.availableBanks = [];
        $scope.availableFrequncy = [];
        // $scope.cusObj.ChildrenDetailss = [];
        $scope.cusObj = {};
        $scope.cusReqObj = {};
        //   $scope.cusReqObj.ClientRequestLineDetails = [];
        // $scope.customerObj.childrenDetails = [];
        $scope.availableInsSubClasses = []
        $scope.RelationshipDetails = [];
        $scope.GetRealationship();
        $scope.GenderDetails = [];
        $scope.getGenderDetails();
        $scope.loadTitles = [];
        $scope.availableTitle = [];
        $scope.availablePartners = [];
        $scope.availableCurrencies = [];
        //$scope.loadPartners();
        $scope.LoadTitles();
        $scope.loadCurrencyDetails();

        $scope.getCurrentUser();
        
        $scope.loadAgent();
        $scope.loadFrequncy();
        //  $scope.addItem();
    };

    $scope.loadPartners = function () {
        //$scope.showLoader = true;
        var bisid=$scope.businessUnitID;
        ManageTransactionsService.getAllPartners().then(function (results) {
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

    $scope.loadFrequncy = function () {
        //$scope.showLoader = true;


        // alert(bisid);
        ManageTransactionsService.getAllFrequncy().then(function (results) {
            //$scope.showLoader = false;
            if (results.status === true) {


                for (var i = 0; i < results.data.length; i++) {
                    //if (results.data[i].BUID === bisid) {

                    $scope.availableFrequncy.push({ value: results.data[i].FrequncyID, text: results.data[i].Frequncy })
                    // }
                }

            }
            else {
                $scope.availableFrequncy = [];
            }
        });
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

    $scope.loadClientRequestsByBUID = function (businessUnitID) {
        $scope.showLoader = true;
        ManageTransactionsService.getAllClientRequestsByBUID(businessUnitID).then(function (results) {
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
        ManageTransactionsService.getAllClientsByBUID(businessUnitID).then(function (results) {
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

                                                ManageTransactionsService.sendEmailRequest($scopeChild.emailObj).then(function (results) {
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

    $scope.calculateNetPremium = function (PaymentDetails) {
        var PaymentAmount = 0;
        var LinePaymentAmount = 0;

        for (var i = 0; i < PaymentDetails.length; i++) {

            LinePaymentAmount = parseInt(PaymentDetails[i].PremiumAmount) + parseInt(PaymentDetails[i].LoadingRate);
            PaymentAmount = PaymentAmount + parseInt(PaymentDetails[i].PremiumAmount) + parseInt(PaymentDetails[i].LoadingRate);

            PaymentDetails[i].NetPremium = LinePaymentAmount;
            $scope.cusReqObj.PaymentAmount = PaymentAmount;
        }
    }

    $scope.loadClientByID = function (clientID) {
        $scope.showLoader = true;
        ManageTransactionsService.getClientByID(clientID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.isCustomerAvailable = true;
                $scope.isCustomerAdded = false;
                $scope.isCustomerUpdated = false;

                $scope.cusObj = results.data;
                $scope.cusObj.ChildrenDetailss = [];
                var AgentType = "";

                AgentType = $scope.cusObj.PremiumAccept;
                if ($scope.cusObj.DeductionDetails.length == 0) {



                    if ($scope.cusObj.PremiumAccept == '1') {
                        //   for (var k = 0; k < $scope.cusObj.length; k++) {
                        $scope.cusObj.DeductionDetails.push({ PremiumHolder: $scope.cusObj.ClientOtherName, Premium: "", LoadingRate: "", DeductionRate: "", NetPremium: "" });



                        //  }
                    }



                    for (var i = 0; i < $scope.cusObj.FamilyDetails.length; i++) {

                        if ($scope.cusObj.FamilyDetails[i].MemberName != "")
                            $scope.cusObj.DeductionDetails.push({ PremiumHolder: $scope.cusObj.FamilyDetails[i].MemberName, Premium: "", LoadingRate: "", DeductionRate: "", NetPremium: "" });





                        for (var j = 0; j < $scope.cusObj.FamilyDetails[i].GroupMemberDetails.length; j++) {
                            $scope.cusObj.DeductionDetails.push({ PremiumHolder: $scope.cusObj.FamilyDetails[i].GroupMemberDetails[j].MemberName, Premium: "", LoadingRate: "", Deductible: "", NetPremium: "", PremiumHolderID: $scope.cusObj.FamilyDetails[i].FamilyMemberID, PremiumHolderGroupMemberID: $scope.cusObj.FamilyDetails[i].GroupMemberDetails[j].GroupFamilyMemberID });



                        }
                    }

                  
                    }
                 if ($scope.cusObj.PolicyInfoBUPADetails.length > 0) {
                    $scope.cusObj.Premium = $scope.cusObj.PolicyInfoBUPADetails[0].Premium;
                    $scope.cusObj.MemberID = $scope.cusObj.PolicyInfoBUPADetails[0].MemberID;
                    $scope.cusObj.PolicyInfoID = $scope.cusObj.PolicyInfoBUPADetails[0].PolicyInfoID;
                }
                $scope.cusObj.HomeCountryID = results.data.HomeCountryID + "";
                $scope.cusObj.ResidentCountryID = results.data.ResidentCountryID + "";
                $scope.cusObj.ChildrenDetailss = angular.copy($scope.cusReqObj.FamilyDetails);
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
        ManageTransactionsService.loadAgent().then(function (results) {
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

    //$scope.loadPartners = function () {
    //    //$scope.showLoader = true;
    //    ManageTransactionsService.getAllPartners().then(function (results) {
    //        //$scope.showLoader = false;
    //        if (results.status === true) {
    //            for (var i = 0; i < results.data.length; i++) {
    //                $scope.availablePartners.push({ value: results.data[i].PremiumID, text: results.data[i].PremiumName })
    //            }
    //        }
    //        else {
    //            $scope.availablePartners = [];
    //        }
    //    });
    //};

    $scope.GetRealationship = function () {
        ManageTransactionsService.loadRelationship().then(function (results) {
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.RelationshipDetails.push({ value: results.data[i].RelationShipID, text: results.data[i].Relationship })
                }
            }
            else {
                $scope.RelationshipDetails = [];
            }
        });
    };


    $scope.getGenderDetails = function () {
        ManageTransactionsService.loadGender().then(function (results) {
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.GenderDetails.push({ value: results.data[i].GenderID, text: results.data[i].Gender })
                }
            }
            else {
                $scope.GenderDetails = [];
            }
        });
    };

    $scope.loadFamilyDiscount = function () {
        //$scope.showLoader = true;
        ManageTransactionsService.loadFamilyDiscount().then(function (results) {
            //$scope.showLoader = false;
            if (results.status === true) {

                $scope.cusReqObj.NewFamilyDiscount = results.data[0].FamilyDiscountVal;

            }
            else {
                $scope.availablePartners = [];
            }
        });
    };

    



    $scope.LoadTitles = function () {

        ManageTransactionsService.loadTitle().then(function (results) {

            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableTitle.push({ value: results.data[i].TitleID, text: results.data[i].TitleName })
                }
            }
            else {
                $scope.availableTitle = [];
            }
        });
    };


    $scope.loadCurrencyDetails = function () {
        ManageTransactionsService.loadCurrencies().then(function (results) {
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableCurrencies.push({ value: results.data[i].CurrencyID, text: results.data[i].CurrencyCode });
                }
            }
            else {
                $scope.availableCurrencies = [];
            }
        });
    };

    $scope.loadInsSubClassesByBUID = function (buid) {
        //$scope.showLoader = true;
        ManageTransactionsService.getAllInsSubClassesByBUID(buid).then(function (results) {
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

    $scope.deleteItem = function (deleteIndex) {
        $scope.cusReqObj.ClientRequestLineDetails.splice(deleteIndex, 1);
    };

    $scope.LoadInstitutionData = function () {

    };

    $scope.manageCustomer = function (isEdit, customer, Child) {
        $modal.open({
            templateUrl: 'ngTemplateCustomer',
            backdrop: 'static',
            windowClass: 'app-modal-window-pir',
            controller: [
                    '$scope', '$modalInstance', '$http', function ($scopeChild, $modalInstance, $http) {

                        $scopeChild.mode = "Add";
                        $scopeChild.isEditMode = false;
                        $scopeChild.customerObj = {};
                        $scopeChild.customerObj.childrenDetails = [];
                        $scopeChild.availableHomeCountries = [];
                        $scopeChild.availableResidentCountries = [];

                        $scopeChild.FamilyDetails = [];

                        $scopeChild.RelationshipDetails = [];


                        $scopeChild.GenderDetails = [];
                        $scopeChild.RelationshipDetails = angular.copy($scope.RelationshipDetails);
                        $scopeChild.GenderDetails = angular.copy($scope.GenderDetails);
                        $scope.loadTitles = [];



                        $scopeChild.availableTitle = angular.copy($scope.availableTitle); 
                        $scopeChild.availablePartners = angular.copy($scope.availablePartners); 
                        $scopeChild.availableCurrencies = angular.copy($scope.availableCurrencies);


                        $scopeChild.addItem = function () {
                            $scopeChild.FamilyDetails.push({ MemberName: "", MemberDOB: "", NIC: "", Contact: "", GroupMemberDetails: [] });
                        };

                        $scopeChild.deleteItem= function (deleteIndex) {
                            $scopeChild.FamilyDetails.splice(deleteIndex, 1);
                        };
                        if (isEdit) {
                            $scopeChild.FamilyDetails = angular.copy(customer.FamilyDetails);
                        }
                        else {
                            $scopeChild.addItem();
                        }

                        $scopeChild.homeCountries = function () {
                            ManageTransactionsService.getAllCountries().then(function (results) {
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
                            ManageTransactionsService.getAllCountries().then(function (results) {
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

                        if (isEdit) {

                            //  alert("edit Cus");


                            $scopeChild.mode = "Edit";
                            $scopeChild.isEditMode = true;
                            customer.Exclu = Child.Exclu;
                            customer.MembershipID = Child.MembershipID;
                            customer.SchemeID = Child.SchemeID;
                            //  alert(customer.DOB);
                            //  customer.DOB = Child.DOB;
                            // alert(customer.Exclu);
                            $scopeChild.customerObj = angular.copy(customer);

                            $scopeChild.updateCustomerDetails = function () {
                                $scope.isCustomerAvailable = true;
                                //  alert("edit Cus1");
                                if ($scopeChild.customerObj.ClientID !== undefined) {
                                    $scope.isCustomerAdded = false;
                                    $scope.isCustomerUpdated = true;
                                    $scope.cusObj.ClientID = $scopeChild.customerObj.ClientID;
                                }
                                else {
                                    $scope.isCustomerAdded = true;
                                    $scope.isCustomerUpdated = false;
                                }

                                if ($scopeChild.customerObj.AgentType === undefined || $scopeChild.customerObj.AgentType === "" || $scopeChild.customerObj.AgentType === null)
                                    $scope.cusObj.AgentType = 0;
                                else
                                    $scope.cusObj.AgentType = $scopeChild.customerObj.AgentType;

                                if ($scopeChild.customerObj.TitleID === undefined || $scopeChild.customerObj.TitleID === "" || $scopeChild.customerObj.TitleID === null)
                                    $scope.cusObj.TitleID = 0;
                                else
                                    $scope.cusObj.TitleID = $scopeChild.customerObj.TitleID;

                                if ($scopeChild.customerObj.ClientName === undefined || $scopeChild.customerObj.ClientName === "" || $scopeChild.customerObj.ClientName === null)
                                    $scope.cusObj.ClientName = "";
                                else
                                    $scope.cusObj.ClientName = $scopeChild.customerObj.ClientName;


                                if ($scopeChild.customerObj.ClientOtherName === undefined || $scopeChild.customerObj.ClientOtherName === "" || $scopeChild.customerObj.ClientOtherName === null)
                                    $scope.cusObj.ClientOtherName = "";
                                else
                                    $scope.cusObj.ClientOtherName = $scopeChild.customerObj.ClientOtherName;



                                if ($scopeChild.customerObj.ClientAddress === undefined || $scopeChild.customerObj.ClientAddress === "" || $scopeChild.customerObj.ClientAddress === null)
                                    $scope.cusObj.ClientAddress = "";
                                else
                                    $scope.cusObj.ClientAddress = $scopeChild.customerObj.ClientAddress;



                                if ($scopeChild.customerObj.ContactNo === undefined || $scopeChild.customerObj.ContactNo === "" || $scopeChild.customerObj.ContactNo === null)
                                    $scope.cusObj.ContactNo = 0;
                                else
                                    $scope.cusObj.ContactNo = $scopeChild.customerObj.ContactNo;

                                if ($scopeChild.customerObj.FixedLine === undefined || $scopeChild.customerObj.FixedLine === "" || $scopeChild.customerObj.FixedLine === null)
                                    $scope.cusObj.FixedLine = "";
                                else
                                    $scope.cusObj.FixedLine = $scopeChild.customerObj.FixedLine;

                                if ($scopeChild.customerObj.Email === undefined || $scopeChild.customerObj.Email === "" || $scopeChild.customerObj.Email === null)
                                    $scope.cusObj.Email = "";
                                else
                                    $scope.cusObj.Email = $scopeChild.customerObj.Email;

                                if ($scopeChild.customerObj.DOB === undefined || $scopeChild.customerObj.DOB === "" || $scopeChild.customerObj.DOB === null) {
                                    //$scope.customerObj.DOB = "";
                                    //$scope.cusObj.DOB = $scope.getFormattedDate($scope.customerObj.DOB);
                                }
                                else
                                    $scope.cusObj.DOB = $scope.getFormattedDate($scopeChild.customerObj.DOB);

                                if ($scopeChild.customerObj.PartnerID === undefined || $scopeChild.customerObj.PartnerID === "" || $scopeChild.customerObj.PartnerID === null)
                                    $scope.cusObj.PartnerID = 0;
                                else
                                    $scope.cusObj.PartnerID = $scopeChild.customerObj.PartnerID;

                                if ($scopeChild.customerObj.JoinDate === undefined || $scopeChild.customerObj.JoinDate === "" || $scopeChild.customerObj.JoinDate === null) {
                                    //$scope.cusObj.JoinDate = "";
                                    //$scope.cusObj.JoinDate = $scope.getFormattedDate($scopeChild.customerObj.JoinDate);
                                }
                                else
                                    $scope.cusObj.JoinDate = $scope.getFormattedDate($scopeChild.customerObj.JoinDate);




                                if ($scopeChild.customerObj.GroupID === undefined || $scopeChild.customerObj.GroupID === "" || $scopeChild.customerObj.GroupID === null) {
                                    $scope.cusObj.GroupID = "";

                                }
                                else
                                    $scope.cusObj.GroupID = $scopeChild.customerObj.GroupID;

                                if ($scopeChild.customerObj.AdditionalNote === undefined || $scopeChild.customerObj.AdditionalNote === "" || $scopeChild.customerObj.AdditionalNote === null)
                                    $scope.cusObj.AdditionalNote = "";
                                else
                                    $scope.cusObj.AdditionalNote = $scopeChild.customerObj.AdditionalNote;

                                if ($scopeChild.customerObj.HomeCountryID === undefined || $scopeChild.customerObj.HomeCountryID === "" || $scopeChild.customerObj.HomeCountryID === null)
                                    $scope.cusObj.HomeCountryID = 0;
                                else
                                    $scope.cusObj.HomeCountryID = $scopeChild.customerObj.HomeCountryID;


                                if ($scopeChild.customerObj.ResidentCountryID === undefined || $scopeChild.customerObj.ResidentCountryID === "" || $scopeChild.customerObj.ResidentCountryID === null)
                                    $scope.cusObj.ResidentCountryID = 0;
                                else
                                    $scope.cusObj.ResidentCountryID = $scopeChild.customerObj.ResidentCountryID;

                                $scope.cusObj.FamilyDetails = $scopeChild.FamilyDetails;

                                $scope.cusObj.Exclu = $scopeChild.customerObj.Exclu;
                                $scope.cusObj.MembershipID = $scopeChild.customerObj.MembershipID;
                                $scope.cusObj.OptionalCovers = $scopeChild.customerObj.OptionalCovers;
                                $scope.cusObj.Occupation = $scopeChild.customerObj.Occupation;

                                $scope.cusObj.SchemeID = $scopeChild.customerObj.SchemeID;

                                //alert($scopeChild.FamilyDetails.length);
                               // alert($scope.cusObj.DeductionDetails.length);

                                for (var i = 0; i < $scopeChild.FamilyDetails.length; i++) {
                                  //  alert($scopeChild.FamilyDetails[i].MemberName);
                                    for (var j = 0; j < $scope.cusObj.DeductionDetails.length; j++) {
                                      //  alert($scope.cusObj.DeductionDetails[j].PremiumHolder);
                                        if ($scopeChild.FamilyDetails[i].MemberName !== $scope.cusObj.DeductionDetails[j].PremiumHolder) {

                                         //   alert("Loop");

                                            $scope.cusObj.DeductionDetails.push({ PremiumHolder: $scopeChild.FamilyDetails[i].MemberName, Premium: "", LoadingRate: "", DeductionRate: "", NetPremium: "" });
                                        }
                                    }



                                }














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

                                if ($scopeChild.customerObj.AgentType === undefined || $scopeChild.customerObj.AgentType === "" || $scopeChild.customerObj.AgentType === null)
                                    $scope.cusObj.AgentType = 0;
                                else
                                    $scope.cusObj.AgentType = $scopeChild.customerObj.AgentType;

                                if ($scopeChild.customerObj.TitleID === undefined || $scopeChild.customerObj.TitleID === "" || $scopeChild.customerObj.TitleID === null)
                                    $scope.cusObj.TitleID = 0;
                                else
                                    $scope.cusObj.TitleID = $scopeChild.customerObj.TitleID;

                                if ($scopeChild.customerObj.ClientName === undefined || $scopeChild.customerObj.ClientName === "" || $scopeChild.customerObj.ClientName === null)
                                    $scope.cusObj.ClientName = "";
                                else
                                    $scope.cusObj.ClientName = $scopeChild.customerObj.ClientName;


                                if ($scopeChild.customerObj.ClientOtherName === undefined || $scopeChild.customerObj.ClientOtherName === "" || $scopeChild.customerObj.ClientOtherName === null)
                                    $scope.cusObj.ClientOtherName = "";
                                else
                                    $scope.cusObj.ClientOtherName = $scopeChild.customerObj.ClientOtherName;



                                if ($scopeChild.customerObj.ClientAddress === undefined || $scopeChild.customerObj.ClientAddress === "" || $scopeChild.customerObj.ClientAddress === null)
                                    $scope.cusObj.ClientAddress = "";
                                else
                                    $scope.cusObj.ClientAddress = $scopeChild.customerObj.ClientAddress;



                                if ($scopeChild.customerObj.ContactNo === undefined || $scopeChild.customerObj.ContactNo === "" || $scopeChild.customerObj.ContactNo === null)
                                    $scope.cusObj.ContactNo = 0;
                                else
                                    $scope.cusObj.ContactNo = $scopeChild.customerObj.ContactNo;

                                if ($scopeChild.customerObj.FixedLine === undefined || $scopeChild.customerObj.FixedLine === "" || $scopeChild.customerObj.FixedLine === null)
                                    $scope.cusObj.FixedLine = "";
                                else
                                    $scope.cusObj.FixedLine = $scopeChild.customerObj.FixedLine;

                                if ($scopeChild.customerObj.Email === undefined || $scopeChild.customerObj.Email === "" || $scopeChild.customerObj.Email === null)
                                    $scope.cusObj.Email = "";
                                else
                                    $scope.cusObj.Email = $scopeChild.customerObj.Email;

                                if ($scopeChild.customerObj.DOB === undefined || $scopeChild.customerObj.DOB === "" || $scopeChild.customerObj.DOB === null) {
                                    //$scope.customerObj.DOB = "";
                                    //$scope.cusObj.DOB = $scope.getFormattedDate($scope.customerObj.DOB);
                                }
                                else
                                    $scope.cusObj.DOB = $scope.getFormattedDate($scopeChild.customerObj.DOB);

                                if ($scopeChild.customerObj.PartnerID === undefined || $scopeChild.customerObj.PartnerID === "" || $scopeChild.customerObj.PartnerID === null)
                                    $scope.cusObj.PartnerID = 0;
                                else
                                    $scope.cusObj.PartnerID = $scopeChild.customerObj.PartnerID;

                                if ($scopeChild.customerObj.JoinDate === undefined || $scopeChild.customerObj.JoinDate === "" || $scopeChild.customerObj.JoinDate === null) {
                                    //$scope.cusObj.JoinDate = "";
                                    //$scope.cusObj.JoinDate = $scope.getFormattedDate($scopeChild.customerObj.JoinDate);
                                }
                                else
                                    $scope.cusObj.JoinDate = $scope.getFormattedDate($scopeChild.customerObj.JoinDate);

                                if ($scopeChild.customerObj.GroupID === undefined || $scopeChild.customerObj.GroupID === "" || $scopeChild.customerObj.GroupID === null) {
                                    $scope.cusObj.GroupID = "";

                                }
                                else
                                    $scope.cusObj.GroupID = $scopeChild.customerObj.GroupID;

                                if ($scopeChild.customerObj.AdditionalNote === undefined || $scopeChild.customerObj.AdditionalNote === "" || $scopeChild.customerObj.AdditionalNote === null)
                                    $scope.cusObj.AdditionalNote = "";
                                else
                                    $scope.cusObj.AdditionalNote = $scopeChild.customerObj.AdditionalNote;

                                if ($scopeChild.customerObj.HomeCountryID === undefined || $scopeChild.customerObj.HomeCountryID === "" || $scopeChild.customerObj.HomeCountryID === null)
                                    $scope.cusObj.HomeCountryID = 0;
                                else
                                    $scope.cusObj.HomeCountryID = $scopeChild.customerObj.HomeCountryID;


                                if ($scopeChild.customerObj.ResidentCountryID === undefined || $scopeChild.customerObj.ResidentCountryID === "" || $scopeChild.customerObj.ResidentCountryID === null)
                                    $scope.cusObj.ResidentCountryID = 0;
                                else
                                    $scope.cusObj.ResidentCountryID = $scopeChild.customerObj.ResidentCountryID;

                                //if ($scopeChild.customerObj.Exclusion === undefined || $scopeChild.customerObj.Exclusion === "" || $scopeChild.customerObj.Exclusion === null)
                                //    $scope.cusObj.Exclusion = "";
                                //else
                                //    $scope.cusObj.Exclusion = $scopeChild.customerObj.Exclusion;

                                //if ($scopeChild.FamilyDetails.TitleID === undefined || $scopeChild.FamilyDetails.TitleID === "" || $scopeChild.FamilyDetails.TitleID === null)
                                //    $scopeChild.FamilyDetails.TitleID = 0;
                                //else
                                //    $scopeChild.FamilyDetails.TitleID = $scopeChild.FamilyDetails.TitleID;
                                //if ($scopeChild.FamilyDetails.MemberName === undefined || $scopeChild.FamilyDetails.MemberName === "" || $scopeChild.FamilyDetails.MemberName === null)
                                //    $scopeChild.FamilyDetails.MemberName = "";
                                //else
                                //    $scopeChild.FamilyDetails.MemberName = $scopeChild.FamilyDetails.MemberName;
                                //if ($scopeChild.FamilyDetails.MemberOtherName === undefined || $scopeChild.FamilyDetails.MemberOtherName === "" || $scopeChild.FamilyDetails.MemberOtherName === null)
                                //    $scopeChild.FamilyDetails.MemberOtherName = 0;
                                //else
                                //    $scopeChild.FamilyDetails.MemberOtherName = $scopeChild.FamilyDetails.MemberOtherName;

                                //if ($scopeChild.FamilyDetails.JoinDate === undefined || $scopeChild.FamilyDetails.JoinDate === "" || $scopeChild.FamilyDetails.JoinDate === null) {
                                //    $scopeChild.FamilyDetails.JoinDate = "";
                                //    $scopeChild.FamilyDetails.JoinDate = $scope.getFormattedDate($scopeChild.FamilyDetails.JoinDate);
                                //}
                                //else
                                //    $scopeChild.FamilyDetails.JoinDate = $scope.getFormattedDate($scopeChild.FamilyDetails.JoinDate);

                                //if ($scopeChild.FamilyDetails.InceptionDate === undefined || $scopeChild.FamilyDetails.InceptionDate === "" || $scopeChild.FamilyDetails.InceptionDate === null) {
                                //    $scopeChild.FamilyDetails.InceptionDate = "";
                                //    $scopeChild.FamilyDetails.InceptionDate = $scope.getFormattedDate($scopeChild.FamilyDetails.InceptionDate);
                                //}
                                //else
                                //    $scopeChild.FamilyDetails.InceptionDate = $scope.getFormattedDate($scopeChild.FamilyDetails.InceptionDate);
                                //if ($scopeChild.FamilyDetails.GenderID === undefined || $scopeChild.FamilyDetails.GenderID === "" || $scopeChild.FamilyDetails.GenderID === null)
                                //    $scopeChild.FamilyDetails.GenderID = 0;
                                //else
                                //    $scopeChild.FamilyDetails.GenderID = $scopeChild.FamilyDetails.GenderID;

                                //if ($scopeChild.FamilyDetails.RelationShipID === undefined || $scopeChild.FamilyDetails.RelationShipID === "" || $scopeChild.FamilyDetails.RelationShipID === null)
                                //    $scopeChild.FamilyDetails.RelationShipID = 0;
                                //else
                                //    $scopeChild.FamilyDetails.RelationShipID = $scopeChild.FamilyDetails.RelationShipID;

                                //if ($scopeChild.FamilyDetails.NIC === undefined || $scopeChild.FamilyDetails.NIC === "" || $scopeChild.FamilyDetails.NIC === null)
                                //    $scopeChild.FamilyDetails.NIC = "";
                                //else
                                //    $scopeChild.FamilyDetails.NIC = $scopeChild.FamilyDetails.NIC;

                                //if ($scopeChild.FamilyDetails.MembershipID === undefined || $scopeChild.FamilyDetails.MembershipID === "" || $scopeChild.FamilyDetails.MembershipID === null)
                                //    $scopeChild.FamilyDetails.MembershipID = 0;
                                //else
                                //    $scopeChild.FamilyDetails.MembershipID = $scopeChild.FamilyDetails.MembershipID;

                                //if ($scopeChild.FamilyDetails.ContactNo === undefined || $scopeChild.FamilyDetails.ContactNo === "" || $scopeChild.FamilyDetails.ContactNo === null)
                                //    $scopeChild.FamilyDetails.ContactNo = "";
                                //else
                                //    $scopeChild.FamilyDetails.ContactNo = $scopeChild.FamilyDetails.ContactNo;

                                //if ($scopeChild.FamilyDetails.Exclusion === undefined || $scopeChild.FamilyDetails.Exclusion === "" || $scopeChild.FamilyDetails.Exclusion === null)
                                //    $scopeChild.FamilyDetails.Exclusion = "";
                                //else
                                //    $scopeChild.FamilyDetails.Exclusion = $scopeChild.FamilyDetails.Exclusion;

                                //if ($scopeChild.FamilyDetails.HomeCountryID === undefined || $scopeChild.FamilyDetails.HomeCountryID === "" || $scopeChild.FamilyDetails.HomeCountryID === null)
                                //    $scopeChild.FamilyDetails.HomeCountryID = 0;
                                //else
                                //    $scopeChild.FamilyDetails.HomeCountryID = $scopeChild.FamilyDetails.HomeCountryID;

                                //if ($scopeChild.FamilyDetails.ResidentCountryID === undefined || $scopeChild.FamilyDetails.ResidentCountryID === "" || $scopeChild.FamilyDetails.ResidentCountryID === null)
                                //    $scopeChild.FamilyDetails.ResidentCountryID = 0;
                                //else
                                //    $scopeChild.FamilyDetails.ResidentCountryID = $scopeChild.FamilyDetails.ResidentCountryID;



                                $scope.cusObj.FamilyDetails = $scopeChild.FamilyDetails;
                                //$scope.cusObj.PartnerID = $scopeChild.ExtraText1;
                                //$scope.cusObj.PartnerID = $scopeChild.ExtraText1;

                                //$scope.cusObj.FamilyDetails.push({ MemberName: "", MemberDOB: "", NIC: "", Contact: "", GrpFamilyMemberDetails: [] });
                                //$scopeChild.addItem();
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


                                            $scopeSubChild.RelationshipDetails = [];
                                            $scopeSubChild.availableTitle = [];

                                            $scopeSubChild.GenderDetails = [];
                                            $scopeSubChild.RelationshipDetails = angular.copy($scope.RelationshipDetails);
                                            $scopeSubChild.GenderDetails = angular.copy($scope.GenderDetails);
                                            $scopeSubChild.availableTitle = angular.copy($scope.availableTitle);
                                            $scopeSubChild.availablePartners = angular.copy($scope.availablePartners);
                                            $scopeSubChild.availableCurrencies = angular.copy($scope.availableCurrencies);


                                            $scopeSubChild.homeCountries = function () {
                                                ManageTransactionsService.getAllCountries().then(function (results) {
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
                                            
                                            
                                            if (!isEdit) {

                                                //if ($scopeChild.FamilyDetails.TitleID === undefined || $scopeChild.FamilyDetails.TitleID === "" || $scopeChild.FamilyDetails.TitleID === null)
                                                //    $scopeChild.FamilyDetails.TitleID = 0;
                                                //else
                                                //    $scopeChild.FamilyDetails.TitleID = $scopeChild.FamilyDetails.TitleID;
                                                //if ($scopeChild.FamilyDetails.MemberName === undefined || $scopeChild.FamilyDetails.MemberName === "" || $scopeChild.FamilyDetails.MemberName === null)
                                                //    $scopeChild.FamilyDetails.MemberName = "";
                                                //else
                                                //    $scopeChild.FamilyDetails.MemberName = $scopeChild.FamilyDetails.MemberName;
                                                //if ($scopeChild.FamilyDetails.MemberOtherName === undefined || $scopeChild.FamilyDetails.MemberOtherName === "" || $scopeChild.FamilyDetails.MemberOtherName === null)
                                                //    $scopeChild.FamilyDetails.MemberOtherName = 0;
                                                //else
                                                //    $scopeChild.FamilyDetails.MemberOtherName = $scopeChild.FamilyDetails.MemberOtherName;

                                                //if ($scopeChild.FamilyDetails.JoinDate === undefined || $scopeChild.FamilyDetails.JoinDate === "" || $scopeChild.FamilyDetails.JoinDate === null) {
                                                //    $scopeChild.FamilyDetails.JoinDate = "";
                                                //    $scopeChild.FamilyDetails.JoinDate = $scope.getFormattedDate($scopeChild.FamilyDetails.JoinDate);
                                                //}
                                                //else
                                                //    $scopeChild.FamilyDetails.JoinDate = $scope.getFormattedDate($scopeChild.FamilyDetails.JoinDate);

                                                //if ($scopeChild.FamilyDetails.InceptionDate === undefined || $scopeChild.FamilyDetails.InceptionDate === "" || $scopeChild.FamilyDetails.InceptionDate === null) {
                                                //    $scopeChild.FamilyDetails.InceptionDate = "";
                                                //    $scopeChild.FamilyDetails.InceptionDate = $scope.getFormattedDate($scopeChild.FamilyDetails.InceptionDate);
                                                //}
                                                //else
                                                //    $scopeChild.FamilyDetails.InceptionDate = $scope.getFormattedDate($scopeChild.FamilyDetails.InceptionDate);
                                                //if ($scopeChild.FamilyDetails.GenderID === undefined || $scopeChild.FamilyDetails.GenderID === "" || $scopeChild.FamilyDetails.GenderID === null)
                                                //    $scopeChild.FamilyDetails.GenderID = 0;
                                                //else
                                                //    $scopeChild.FamilyDetails.GenderID = $scopeChild.FamilyDetails.GenderID;

                                                //if ($scopeChild.FamilyDetails.RelationShipID === undefined || $scopeChild.FamilyDetails.RelationShipID === "" || $scopeChild.FamilyDetails.RelationShipID === null)
                                                //    $scopeChild.FamilyDetails.RelationShipID = 0;
                                                //else
                                                //    $scopeChild.FamilyDetails.RelationShipID = $scopeChild.FamilyDetails.RelationShipID;

                                              
                                                //if ($scopeChild.FamilyDetails.MembershipID === undefined || $scopeChild.FamilyDetails.MembershipID === "" || $scopeChild.FamilyDetails.MembershipID === null)
                                                //    $scopeChild.FamilyDetails.MembershipID = 0;
                                                //else
                                                //    $scopeChild.FamilyDetails.MembershipID = $scopeChild.FamilyDetails.MembershipID;

                                                //if ($scopeChild.FamilyDetails.ContactNo === undefined || $scopeChild.FamilyDetails.ContactNo === "" || $scopeChild.FamilyDetails.ContactNo === null)
                                                //    $scopeChild.FamilyDetails.ContactNo = "";
                                                //else
                                                //    $scopeChild.FamilyDetails.ContactNo = $scopeChild.FamilyDetails.ContactNo;

                                                //if ($scopeChild.FamilyDetails.Exclusion === undefined || $scopeChild.FamilyDetails.Exclusion === "" || $scopeChild.FamilyDetails.Exclusion === null)
                                                //    $scopeChild.FamilyDetails.Exclusion = "";
                                                //else
                                                //    $scopeChild.FamilyDetails.Exclusion = $scopeChild.FamilyDetails.Exclusion;

                                                //if ($scopeChild.FamilyDetails.HomeCountryID === undefined || $scopeChild.FamilyDetails.HomeCountryID === "" || $scopeChild.FamilyDetails.HomeCountryID === null)
                                                //    $scopeChild.FamilyDetails.HomeCountryID = 0;
                                                //else
                                                //    $scopeChild.FamilyDetails.HomeCountryID = $scopeChild.FamilyDetails.HomeCountryID;

                                                //if ($scopeChild.FamilyDetails.ResidentCountryID === undefined || $scopeChild.FamilyDetails.ResidentCountryID === "" || $scopeChild.FamilyDetails.ResidentCountryID === null)
                                                //    $scopeChild.FamilyDetails.ResidentCountryID = 0;
                                                //else
                                                //    $scopeChild.FamilyDetails.ResidentCountryID = $scopeChild.FamilyDetails.ResidentCountryID;



                                                $scope.cusObj.FamilyDetails = $scopeChild.FamilyDetails;;
                                              //  $scope.cusObj.FamilyDetails.push({ MemberName: "", MemberDOB: "", NIC: "", Contact: "", GroupMemberDetails: [] });
                                            }

                                            //$scopeSubChild.availableHomeCountries = [];

                                            //$scopeSubChild.homeCountries = function () {
                                            //    ManageTransactionsService.getAllCountries().then(function (results) {
                                            //        if (results.status === true) {
                                            //            for (var i = 0; i < results.data.length; i++) {
                                            //                $scopeSubChild.availableHomeCountries.push({ value: results.data[i].CountryID, text: results.data[i].CountryName })
                                            //            }
                                            //        }
                                            //        else {
                                            //            $scopeSubChild.availableHomeCountries = [];
                                            //        }
                                            //    });
                                            //};

                                            $scopeSubChild.addInsCompanyScopeDetails = function () {
                                                $scopeSubChild.quotationDetailsInsCompanyScopeTemp.push({ TitleID: "", MemberName: "", MemberOtherName: "", MemberDOB: "", JoinDate: "", MembershipID: "", ContactNo: "", RelationShipID: "", GenderID: "" });
                                            };

                                            $scopeSubChild.deleteInsCompanyScopeDetails = function (deleteIndex) {
                                                $scopeSubChild.quotationDetailsInsCompanyScopeTemp.splice(deleteIndex, 1);
                                            };

                                            if (isEdit) {
                                                $scopeSubChild.quotationDetailsInsCompanyScopeTemp = angular.copy($scope.cusObj.FamilyDetails[idx].GroupMemberDetails);
                                            }
                                            else {
                                                $scopeSubChild.addInsCompanyScopeDetails();
                                            }

                                            $scopeSubChild.availableHomeCountries = angular.copy($scopeChild.availableHomeCountries);
                                            $scopeSubChild.RelationshipDetails = angular.copy($scopeChild.RelationshipDetails);
                                            $scopeSubChild.GenderDetails = angular.copy($scopeChild.GenderDetails);

                                            //$scopeSubChild.homeCountries = function () {
                                            //    ManageTransactionsService.getAllCountries().then(function (results) {
                                            //        if (results.status === true) {
                                            //            for (var i = 0; i < results.data.length; i++) {
                                            //                $scopeSubChild.availableHomeCountries.push({ value: results.data[i].CountryID, text: results.data[i].CountryName })
                                            //            }
                                            //        }
                                            //        else {
                                            //            $scopeSubChild.availableHomeCountries = [];
                                            //        }
                                            //    });
                                            //};

                                            $scopeSubChild.saveInsCompanyScopeDetails = function () {
                                                $scope.cusObj.FamilyDetails[idx].GroupMemberDetails = $scopeSubChild.quotationDetailsInsCompanyScopeTemp;
                                                $modalInstance.close();
                                            };

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

    //$scope.manageBankDetails = function (BUID, ClientID, ClientReqID) {
    //    $modal.open({
    //        templateUrl: 'ngTemplateBank',
    //        backdrop: 'static',
    //        windowClass: 'app-modal-window-property',
    //        controller: [
    //                '$scope', '$modalInstance', '$http', function ($scopeChild, $modalInstance, $http) {

    //                    $scopeChild.availableBanks = [];

    //                    //  $scope.cusReqObj.PaymentAmount = $scope.cusReqObj.PaymentDetails[0].PaymentAmount;

    //                    $scopeChild.loadBanks = function (BUID) {
    //                        //$scope.showLoader = true;
    //                        ManageTransactionsService.loadBanks(BUID).then(function (results) {
    //                            //$scope.showLoader = false;
    //                            if (results.status === true) {
    //                                for (var i = 0; i < results.data.length; i++) {
    //                                    $scopeChild.availableBanks.push({ value: results.data[i].BankID, text: results.data[i].BankName })
    //                                }
    //                            }
    //                            else {
    //                                $scopeChild.availableBanks = [];
    //                            }
    //                        });
    //                    };

    //                  //  $scopeChild.loadPaymentMethods = function () {
    //                        //$scope.showLoader = true;
    //                        ManageTransactionsService.loadPaymentMethods().then(function (results) {
    //                            //$scope.showLoader = false;
    //                            if (results.status === true) {
    //                                for (var i = 0; i < results.data.length; i++) {
    //                                    $scopeChild.availablePaymentMethods.push({ value: results.data[i].PolicyMemberID, text: results.data[i].PolicyMemberName })
    //                                }
    //                            }
    //                            else {
    //                                $scopeChild.availablePaymentMethods = [];
    //                            }
    //                        });
    //                  //  };

    //                    //$scope.showLoader = true;


    //                    ManageTransactionsService.getClientRequestByID(ClientReqID).then(function (results) {
    //                        $scope.showLoader = false;

    //                        if (results.status === true) {
    //                            $scope.cusReqObj = results.data;
    //                            //$scope.isFamilyDiscountApply = true;
    //                            if ($scope.cusReqObj.PaymentDetails.length > 0) {
    //                                $scope.cusReqObj.PaymentAmount = $scope.cusReqObj.PaymentDetails[0].PaymentAmount;
    //                                $scope.cusReqObj.PaymentID = $scope.cusReqObj.PaymentDetails[0].PaymentID;
    //                            }

    //                            $scopeChild.bankObj = {};


                                
    //                            $scopeChild.bankObj.PremiumAmount = $scope.cusReqObj.PaymentAmount;
    //                            //  $scope.loadClientByID(ClientID);
    //                            ManageTransactionsService.getClientByID(ClientID).then(function (results) {
    //                                $scope.showLoader = false;
    //                                if (results.status === true) {
    //                                    $scope.cusReqObj.Email = results.data.Email;
    //                                    $scope.cusReqObj.ClientAddress = results.data.ClientAddress;
    //                                    $scope.cusReqObj.ContactNo = results.data.ContactNo;
    //                                    $scope.cusReqObj.FixedLine = results.data.FixedLine;
    //                                    $scope.cusReqObj.NIC = results.data.NIC;
    //                                    $scope.cusReqObj.DOB = results.data.DOB;
    //                                   // $scopeChild.bankObj.PremiumAmount = results.data.PolicyInfoBUPADetails[0].Premium;
    //                                    if ($scope.cusReqObj.AgentID == 0) {
    //                                        $scopeChild.bankObj.SGSAmount = ($scopeChild.bankObj.PremiumAmount * (15)) / 100;
    //                                        $scope.cusReqObj.IBSAmount = ($scopeChild.bankObj.PremiumAmount * (100 - (15))) / 100;
    //                                        $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: $scope.cusReqObj.PaymentID, TotalGrossPremium: $scope.cusReqObj.IBSAmount })
    //                                        $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: $scope.cusReqObj.PaymentID, TotalGrossPremium: $scopeChild.bankObj.SGSAmount })
    //                                    }
    //                                    else {
    //                                        $scope.cusReqObj.AgentAmount = ($scopeChild.bankObj.PremiumAmount * $scope.cusReqObj.AgentRate) / 100;
    //                                        $scopeChild.bankObj.SGSAmount = ($scopeChild.bankObj.PremiumAmount * (15 - $scope.cusReqObj.AgentRate)) / 100;
    //                                        $scope.cusReqObj.IBSAmount = ($scopeChild.bankObj.PremiumAmount * (100 - 15)) / 100;
    //                                        $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: $scope.cusReqObj.PaymentID, TotalGrossPremium: $scope.cusReqObj.IBSAmount })
    //                                        $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: $scope.cusReqObj.PaymentID, TotalGrossPremium: $scopeChild.bankObj.SGSAmount })
    //                                        $scopeChild.bankObj.AgentAmount = $scope.cusReqObj.AgentAmount;
    //                                        $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: $scope.cusReqObj.PaymentID, TotalGrossPremium: $scope.cusReqObj.AgentAmount })

    //                                    }
    //                                    $scopeChild.bankObj.Email = $scope.cusReqObj.Email;
    //                                    $scopeChild.bankObj.ClientID = $scope.cusReqObj.ClientID;
    //                                    $scopeChild.bankObj.ClientAddress = $scope.cusReqObj.ClientAddress;
    //                                    $scopeChild.bankObj.ContactNo = $scope.cusReqObj.ContactNo;
    //                                    $scopeChild.bankObj.FixedLine = $scope.cusReqObj.FixedLine;
    //                                    $scopeChild.bankObj.NIC = $scope.cusReqObj.NIC;
    //                                    $scopeChild.bankObj.DOB = $scope.cusReqObj.DOB;
                                        
                                        
    //                                     $scopeChild.bankObj.IBSAmount = $scope.cusReqObj.IBSAmount;
    //                                    if (results.data.BankTransactionDetails.length > 0) {
    //                                        $scopeChild.bankObj.BankID = results.data.BankTransactionDetails[0].BankID;
    //                                        $scopeChild.bankObj.BankID = $scopeChild.bankObj.BankID + "";
    //                                        $scopeChild.bankObj.DraftNo = results.data.BankTransactionDetails[0].DraftNo;
    //                                        $scopeChild.bankObj.IBSAmount = results.data.BankTransactionDetails[0].IBSAmount;
    //                                        $scopeChild.bankObj.PaymentMethodID = results.data.BankTransactionDetails[0].PaymentMethodID;
                                           
    //                                        if ($scopeChild.bankObj.PaymentMethodID == 2 || $scopeChild.bankObj.PaymentMethodID == 3 ||$scopeChild.bankObj.PaymentMethodID == 4  ) {
    //                                            $scopeChild.IsBankVisible = true;
    //                                            $scopeChild.IsTextBoxvisible = true;
    //                                        }
    //                                        $scopeChild.bankObj.PaymentMethodID = $scopeChild.bankObj.PaymentMethodID + "";
    //                                        $scopeChild.bankObj.SGSAmount = results.data.BankTransactionDetails[0].SGSAmount;
    //                                        $scopeChild.bankObj.BankDetailID = results.data.BankTransactionDetails[0].BankDetailID;
    //                                        if ($scopeChild.bankObj.BankDetailID > 0) {
    //                                            $scopeChild.IsPayment = true;
    //                                        }
    //                                        else {
    //                                            $scopeChild.IsPayment = false;
    //                                        }

    //                                    }

    //                                }
    //                            });
    //                            $scope.cusReqObj.DebitNoteDetails = [];
    //                            //  $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: "", DebitNoteAmount: "" })
    //                            $scopeChild.bankObj.BankAmount = $scope.cusReqObj.BankAmount;
    //                            $scopeChild.bankObj.AgentAmount = $scope.cusReqObj.AgentAmount;
                               
    //                            $scopeChild.bankObj.IBSAmount = $scope.cusReqObj.IBSAmount;
    //                          //  $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: $scope.cusReqObj.PaymentID, TotalGrossPremium: $scope.cusReqObj.IBSAmount })
    //                            $scopeChild.bankObj.ClientName = $scope.cusReqObj.ClientName;
    //                            if ($scope.cusReqObj.AgentID > 0) {
    //                                $scopeChild.IsAgentAvailabe = true;
    //                                ManageTransactionsService.getAgentByID($scope.cusReqObj.AgentID).then(function (results) {

    //                                    if (results.status === true) {
    //                                        //  $scope.cusReqObj.DebitNoteDetails = [];
    //                                        //  $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: "", DebitNoteAmount: "" })
    //                                        $scope.cusReqObj.AgentRate = results.data.RateValue;
    //                                        $scopeChild.bankObj.AgentRate = $scope.cusReqObj.AgentRate;
    //                                        $scope.cusReqObj.AgentAmount = ($scopeChild.bankObj.PremiumAmount * $scope.cusReqObj.AgentRate) / 100;
    //                                        $scopeChild.bankObj.SGSAmount = ($scopeChild.bankObj.PremiumAmount * (15 - $scope.cusReqObj.AgentRate)) / 100;
    //                                        $scope.cusReqObj.IBSAmount = ($scopeChild.bankObj.PremiumAmount * (100 - 15)) / 100;
    //                                       // $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: $scope.cusReqObj.PaymentID, TotalGrossPremium: $scope.cusReqObj.IBSAmount })
    //                                      //  $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: $scope.cusReqObj.PaymentID, TotalGrossPremium: $scopeChild.bankObj.SGSAmount })
    //                                        $scopeChild.bankObj.AgentAmount = $scope.cusReqObj.AgentAmount;
    //                                      //  $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: $scope.cusReqObj.PaymentID, TotalGrossPremium: $scope.cusReqObj.AgentAmount })
    //                                    }
    //                                });
    //                            }
    //                            else {
    //                                $scopeChild.IsAgentAvailabe = false;
                                   
                                           
    //                                      //  $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: $scope.cusReqObj.PaymentID, TotalGrossPremium: $scope.cusReqObj.AgentAmount })
                                      
    //                            }
                                
                                
    //                            $scopeChild.loadBankByID = function (bankID) {
    //                                ManageTransactionsService.loadBankByID(bankID).then(function (results) {

    //                                    if (results.status === true) {
    //                                        //     $scope.cusReqObj.DebitNoteDetails = [];
    //                                        //    $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: "", DebitNoteAmount: "" })
    //                                        $scope.cusReqObj.BankRate = results.data.DiscountRatio;
    //                                        $scopeChild.bankObj.BankRate = $scope.cusReqObj.BankRate;
    //                                        $scope.cusReqObj.BankAmount = ($scope.cusReqObj.PaymentAmount * $scope.cusReqObj.BankRate) / 100;
    //                                        $scopeChild.bankObj.BankAmount = $scope.cusReqObj.BankAmount;
    //                                       // $scopeChild.bankObj.SGSAmount = ($scope.cusReqObj.PremiumAmount * (100 - ($scope.cusReqObj.BankRate + $scope.cusReqObj.AgentRate + 25))) / 100;
    //                                    }
    //                                });
    //                            }
    //                            $scopeChild.loadPaymentByID = function (PaymentID) {
    //                                if (PaymentID == 1) {
    //                                    $scopeChild.IsBankVisible = false;
    //                                    $scopeChild.IsTextBoxvisible = false;
                                        
    //                                }
    //                                if (PaymentID == 2) {
    //                                    $scopeChild.IsBankVisible = true;
    //                                    $scopeChild.IsTextBoxvisible = true;
    //                                    $scopeChild.bankObj.textBoxName = "Check No";
    //                                }
    //                                if (PaymentID == 3) {
    //                                    $scopeChild.IsBankVisible = true;
    //                                    $scopeChild.IsTextBoxvisible = true;
    //                                    $scopeChild.bankObj.textBoxName = "Draft No";
    //                                }
    //                                if (PaymentID == 4) {
    //                                    $scopeChild.IsBankVisible = true;
    //                                    $scopeChild.IsTextBoxvisible = true;
    //                                    $scopeChild.bankObj.textBoxName = "Credit/Debit Card No";
    //                                }
    //                            }

    //                            $scopeChild.saveBankDetails = function () {
                                    
    //                                $scope.showLoader = true;
    //                                noty({
    //                                    text: 'Do you want to Save Payment Details Details?',
    //                                    layout: 'topCenter',
    //                                    buttons: [
    //                                            {
    //                                                addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
    //                                                    $noty.close();
    //                                ManageTransactionsService.saveBankTransaction($scopeChild.bankObj).then(function (results) {
    //                                    if (results.status === true) {
    //                                        ManageTransactionsService.savePayment($scope.cusReqObj, $scope.currentUser.UserID).then(function (results) {
    //                                             $scope.showLoader = false;
    //                                            //alert(angular.toJson(results));
    //                                            if (results.status === true) {
    //                                                noty({
    //                                                    text: 'Successfully Saved Payment Details',
    //                                                    layout: 'topCenter',
    //                                                    type: 'success',
    //                                                    buttons: [
    //                                                              {
    //                                                                  addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
    //                                                                      $noty.close();
    //                                                                  }
    //                                                              }
    //                                                    ]
    //                                                });
    //                                                $scope.ClearFields();
    //                                                $scope.refreshContent();
    //                                                $scope.activateClientRequestListTab();
    //                                                $modalInstance.close();
    //                                            }
    //                                            else {
    //                                                noty({
    //                                                    text: 'Successfully Saved Payment Details',
    //                                                    layout: 'topCenter',
    //                                                    type: 'success',
    //                                                    buttons: [
    //                                                              {
    //                                                                  addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
    //                                                                      $noty.close();
    //                                                                  }
    //                                                              }
    //                                                    ]
    //                                                });
    //                                                $scope.ClearFields();
    //                                                $scope.refreshContent();
    //                                                $scope.activateClientRequestListTab();
    //                                                $modalInstance.close();
    //                                            }
    //                                        });
    //                                    }
    //                                    else {
    //                                        noty({
    //                                            text: 'Error Saving Customer Payment Details',
    //                                            layout: 'topCenter',
    //                                            type: 'error',
    //                                            buttons: [
    //                                                       {
    //                                                           addClass: 'btn btn-danger btn-clean', text: 'Ok', onClick: function ($noty) {
    //                                                               $noty.close();
    //                                                           }
    //                                                       }
    //                                            ]
    //                                        });
    //                                    }
    //                                });

    //                                                }
    //                                            },
    //                {
    //                    addClass: 'btn btn-danger btn-clean', text: 'Cancel', onClick: function ($noty) {
    //                        $noty.close();
    //                        $scope.$apply(function () {
    //                            $scope.showLoader = false;
    //                        });
    //                    }
    //                }
    //                                    ]
    //                                })

    //                            }
    //                            $scopeChild.updateBankDetails = function () {
    //                                ManageTransactionsService.saveBankTransaction($scopeChild.bankObj).then(function (results) {
    //                                    if (results.status === true) {
    //                                        noty({
    //                                            text: 'Successfully Saved Payment Details',
    //                                            layout: 'topCenter',
    //                                            type: 'success',
    //                                            buttons: [
    //                                                      {
    //                                                          addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
    //                                                              $noty.close();
    //                                                          }
    //                                                      }
    //                                            ]
    //                                        });
    //                                        $scope.ClearFields();
    //                                        $scope.refreshContent();
    //                                        $scope.activateClientRequestListTab();
    //                                    }
    //                                    else {
    //                                        noty({
    //                                            text: 'Error Saving Customer Payment Details',
    //                                            layout: 'topCenter',
    //                                            type: 'error',
    //                                            buttons: [
    //                                                       {
    //                                                           addClass: 'btn btn-danger btn-clean', text: 'Ok', onClick: function ($noty) {
    //                                                               $noty.close();
    //                                                           }
    //                                                       }
    //                                            ]
    //                                        });
    //                                    }
    //                                    // }
    //                                });
    //                            }
    //                        }
    //                    });

    //                    $scopeChild.loadBanks(BUID);




    //                    $scopeChild.cancel = function () {
    //                        $modalInstance.dismiss('cancel');
    //                    };
    //                    $scopeChild.managePolicyInfoChargeDetails = function () {
    //                        $scopeChild.bankObj = {};
    //                        $scope.cusReqObj.BankAmount = ($scope.cusReqObj.PaymentAmount * 5) / 100;
    //                        $scope.cusReqObj.AgentAmount = ($scope.cusReqObj.PaymentAmount * 10) / 100;
                           
    //                        $scopeChild.bankObj.BankAmount = $scope.cusReqObj.BankAmount;
    //                        $scopeChild.bankObj.AgentAmount = $scope.cusReqObj.AgentAmount;
    //                        $scopeChild.bankObj.IBSAmount = $scope.cusReqObj.IBSAmount;
    //                    }
    //                    $scopeChild.printDiv = function (divName) {
    //                        var printContents = document.getElementById(divName).innerHTML;
    //                        var popupWin = window.open('', '_blank', 'width=300,height=300');
    //                        popupWin.document.open();
    //                        popupWin.document.write('<html><head><link rel="stylesheet" type="text/css" href="style.css" /></head><body onload="window.print()">' + printContents + '</body></html>');
    //                        popupWin.document.close();
    //                    }

    //                    $scopeChild.managePolicyInfoChargeDetailss = function (idx) {
    //                        $modal.open({
    //                            templateUrl: 'ngTemplateAgentDetails',
    //                            backdrop: 'static',
    //                            windowClass: 'app-modal-window-pic',
    //                            controller: [
    //                                    '$scope', '$modalInstance', '$http', function ($scopeSubChild, $modalInstance, $http) {



    //                                        $scopeSubChild.cancel = function () {
    //                                            $modalInstance.dismiss('cancel');
    //                                        };
    //                                    }
    //                            ],
    //                        });
    //                    };
    //                }
    //        ],
    //    });
    //};



    $scope.manageBankDetails = function (BUID, ClientID, ClientReqID) {
        $modal.open({
            templateUrl: 'ngTemplateBank',
            backdrop: 'static',
            windowClass: 'app-modal-window-property',
            controller: [
                    '$scope', '$modalInstance', '$http', function ($scopeChild, $modalInstance, $http) {

                        $scopeChild.availableBanks = [];
                        $scopeChild.availableCurrencies = [];

                        $scope.showLoader = true;
                        //  $scope.loadCurrencyDetail();

                        //  $scope.cusReqObj.PaymentAmount = $scope.cusReqObj.PaymentDetails[0].PaymentAmount;
                        // alert("1");
                        $scopeChild.loadBanks = function (BUID) {
                            //$scope.showLoader = true;
                            //   alert("2");
                            ManageTransactionsService.loadBanks(BUID).then(function (results) {
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




                        //$scopeChild.loadCurrencyDetails = function () {
                        //            // var bisid = $scope.businessUnitID;
                        //           // alert('curranacy');
                        //             ManageTransactionsService.loadCurrencies().then(function (results) {





                        //                if (results.status === true) {


                        //                    for (var i = 0; i < results.data.length; i++) {


                        //                        //if (results.data[i].BUID === bisid) {
                        //                        $scopeChild.availableCurrencies.push({ value: results.data[i].CurrencyID, text: results.data[i].CurrencyCode });
                        //                        //}
                        //                    }
                        //                }
                        //                else {
                        //                    $scopeChild.availableCurrencies = [];
                        //                }
                        //            });
                        //        };




                        //$scopeChild.loadPaymentMethods = function (BUID) {
                        ////$scope.showLoader = true;
                        ////alert("3");
                        //ManageTransactionsService.loadPaymentMethods(BUID).then(function (results) {
                        //        //$scope.showLoader = false;



                        //        if (results.status === true) {
                        //            for (var i = 0; i < results.data.length; i++) {
                        //                $scopeChild.availablePaymentMethods.push({ value: results.data[i].PolicyMemberID, text: results.data[i].PolicyMemberName })
                        //            }
                        //        }
                        //        else {
                        //            $scopeChild.availablePaymentMethods = [];
                        //        }
                        //    });
                        //};

                        //$scope.showLoader = true;

                        //   alert("4");
                        ManageTransactionsService.getClientRequestByID(ClientReqID).then(function (results) {
                            $scope.showLoader = false;

                            if (results.status === true) {













                                //   alert("1");
                                $scope.cusReqObj = results.data;
                                //$scope.isFamilyDiscountApply = true;
                                if ($scope.cusReqObj.PaymentDetails.length > 0) {
                                    $scope.cusReqObj.PaymentAmount = $scope.cusReqObj.PaymentDetails[0].PaymentAmount;
                                    $scope.cusReqObj.PaymentID = $scope.cusReqObj.PaymentDetails[0].PaymentID;
                                }

                                $scopeChild.bankObj = {};



                                $scopeChild.bankObj.PremiumAmount = $scope.cusReqObj.PaymentAmount;
                                $scopeChild.bankObj.CurrancyID = $scope.cusReqObj.CurrancyID;

                                $scopeChild.BalanceAmount = $scope.cusReqObj.PaymentAmount;
                                $scopeChild.InputCurrancy = $scope.cusReqObj.CurrancyCode;


                                //  $scope.loadClientByID(ClientID);
                                ManageTransactionsService.getClientByID(ClientID).then(function (results) {
                                    $scope.showLoader = false;
                                    if (results.status === true) {
                                        $scope.cusReqObj.Email = results.data.Email;
                                        $scope.cusReqObj.ClientAddress = results.data.ClientAddress;
                                        $scope.cusReqObj.ContactNo = results.data.ContactNo;
                                        $scope.cusReqObj.FixedLine = results.data.FixedLine;
                                        $scope.cusReqObj.NIC = results.data.NIC;
                                        $scope.cusReqObj.DOB = results.data.DOB;
                                        // $scopeChild.bankObj.PremiumAmount = results.data.PolicyInfoBUPADetails[0].Premium;
                                        if ($scope.cusReqObj.AgentID == 0) {
                                            $scopeChild.bankObj.SGSAmount = ($scopeChild.bankObj.PremiumAmount * (15)) / 100;
                                            $scope.cusReqObj.IBSAmount = ($scopeChild.bankObj.PremiumAmount * (100 - (15))) / 100;
                                            $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: $scope.cusReqObj.PaymentID, TotalGrossPremium: $scope.cusReqObj.IBSAmount })
                                            $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: $scope.cusReqObj.PaymentID, TotalGrossPremium: $scopeChild.bankObj.SGSAmount })
                                        }
                                        else {
                                            $scope.cusReqObj.AgentAmount = ($scopeChild.bankObj.PremiumAmount * $scope.cusReqObj.AgentRate) / 100;
                                            $scopeChild.bankObj.SGSAmount = ($scopeChild.bankObj.PremiumAmount * (15 - $scope.cusReqObj.AgentRate)) / 100;
                                            $scope.cusReqObj.IBSAmount = ($scopeChild.bankObj.PremiumAmount * (100 - 15)) / 100;
                                            $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: $scope.cusReqObj.PaymentID, TotalGrossPremium: $scope.cusReqObj.IBSAmount })
                                            $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: $scope.cusReqObj.PaymentID, TotalGrossPremium: $scopeChild.bankObj.SGSAmount })
                                            $scopeChild.bankObj.AgentAmount = $scope.cusReqObj.AgentAmount;
                                            $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: $scope.cusReqObj.PaymentID, TotalGrossPremium: $scope.cusReqObj.AgentAmount })

                                        }
                                        $scopeChild.bankObj.Email = $scope.cusReqObj.Email;
                                        $scopeChild.bankObj.ClientID = $scope.cusReqObj.ClientID;
                                        $scopeChild.bankObj.ClientAddress = $scope.cusReqObj.ClientAddress;
                                        $scopeChild.bankObj.ContactNo = $scope.cusReqObj.ContactNo;
                                        $scopeChild.bankObj.FixedLine = $scope.cusReqObj.FixedLine;
                                        $scopeChild.bankObj.NIC = $scope.cusReqObj.NIC;
                                        $scopeChild.bankObj.DOB = $scope.cusReqObj.DOB;


                                        $scopeChild.bankObj.IBSAmount = $scope.cusReqObj.IBSAmount;
                                        if (results.data.BankTransactionDetails.length > 0) {
                                            $scopeChild.bankObj.BankID = results.data.BankTransactionDetails[0].BankID;
                                            $scopeChild.bankObj.BankID = $scopeChild.bankObj.BankID + "";
                                            $scopeChild.bankObj.DraftNo = results.data.BankTransactionDetails[0].DraftNo;
                                            $scopeChild.bankObj.IBSAmount = results.data.BankTransactionDetails[0].IBSAmount;
                                            $scopeChild.bankObj.PaymentMethodID = results.data.BankTransactionDetails[0].PaymentMethodID;

                                            if ($scopeChild.bankObj.PaymentMethodID == 2 || $scopeChild.bankObj.PaymentMethodID == 3 || $scopeChild.bankObj.PaymentMethodID == 4) {
                                                $scopeChild.IsBankVisible = true;
                                                $scopeChild.IsTextBoxvisible = true;
                                            }
                                            $scopeChild.bankObj.PaymentMethodID = $scopeChild.bankObj.PaymentMethodID + "";
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
                                //  $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: $scope.cusReqObj.PaymentID, TotalGrossPremium: $scope.cusReqObj.IBSAmount })
                                $scopeChild.bankObj.ClientName = $scope.cusReqObj.ClientName;
                                if ($scope.cusReqObj.AgentID > 0) {
                                    $scopeChild.IsAgentAvailabe = true;
                                    ManageTransactionsService.getAgentByID($scope.cusReqObj.AgentID).then(function (results) {

                                        if (results.status === true) {
                                            //  $scope.cusReqObj.DebitNoteDetails = [];
                                            //  $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: "", DebitNoteAmount: "" })
                                            $scope.cusReqObj.AgentRate = results.data.RateValue;
                                            $scopeChild.bankObj.AgentRate = $scope.cusReqObj.AgentRate;
                                            $scope.cusReqObj.AgentAmount = ($scopeChild.bankObj.PremiumAmount * $scope.cusReqObj.AgentRate) / 100;
                                            $scopeChild.bankObj.SGSAmount = ($scopeChild.bankObj.PremiumAmount * (15 - $scope.cusReqObj.AgentRate)) / 100;
                                            $scope.cusReqObj.IBSAmount = ($scopeChild.bankObj.PremiumAmount * (100 - 15)) / 100;
                                            // $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: $scope.cusReqObj.PaymentID, TotalGrossPremium: $scope.cusReqObj.IBSAmount })
                                            //  $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: $scope.cusReqObj.PaymentID, TotalGrossPremium: $scopeChild.bankObj.SGSAmount })
                                            $scopeChild.bankObj.AgentAmount = $scope.cusReqObj.AgentAmount;
                                            //  $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: $scope.cusReqObj.PaymentID, TotalGrossPremium: $scope.cusReqObj.AgentAmount })
                                        }
                                    });
                                }
                                else {
                                    $scopeChild.IsAgentAvailabe = false;


                                    //  $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: $scope.cusReqObj.PaymentID, TotalGrossPremium: $scope.cusReqObj.AgentAmount })

                                }


                                $scopeChild.loadBankByID = function (bankID) {
                                    ManageTransactionsService.loadBankByID(bankID).then(function (results) {

                                        if (results.status === true) {
                                            //     $scope.cusReqObj.DebitNoteDetails = [];
                                            //    $scope.cusReqObj.DebitNoteDetails.push({ PaymentID: "", DebitNoteAmount: "" })
                                            $scope.cusReqObj.BankRate = results.data.DiscountRatio;
                                            $scopeChild.bankObj.BankRate = $scope.cusReqObj.BankRate;
                                            $scope.cusReqObj.BankAmount = ($scope.cusReqObj.PaymentAmount * $scope.cusReqObj.BankRate) / 100;
                                            $scopeChild.bankObj.BankAmount = $scope.cusReqObj.BankAmount;
                                            // $scopeChild.bankObj.SGSAmount = ($scope.cusReqObj.PremiumAmount * (100 - ($scope.cusReqObj.BankRate + $scope.cusReqObj.AgentRate + 25))) / 100;
                                        }
                                    });
                                }
                                $scopeChild.loadPaymentByID = function (PaymentID) {
                                    if (PaymentID == 1) {
                                        $scopeChild.IsBankVisible = false;
                                        $scopeChild.IsTextBoxvisible = false;

                                    }
                                    if (PaymentID == 2) {
                                        $scopeChild.IsBankVisible = true;
                                        $scopeChild.IsTextBoxvisible = true;
                                        $scopeChild.bankObj.textBoxName = "Check No";
                                    }
                                    if (PaymentID == 3) {
                                        $scopeChild.IsBankVisible = true;
                                        $scopeChild.IsTextBoxvisible = true;
                                        $scopeChild.bankObj.textBoxName = "Draft No";
                                    }
                                    if (PaymentID == 4) {
                                        $scopeChild.IsBankVisible = true;
                                        $scopeChild.IsTextBoxvisible = true;
                                        $scopeChild.bankObj.textBoxName = "Credit/Debit Card No";
                                    }
                                }

                                $scopeChild.saveBankDetails = function () {

                                    $scope.showLoader = true;
                                    noty({
                                        text: 'Do you want to Save Payment Details Details?',
                                        layout: 'topCenter',
                                        buttons: [
                                                {
                                                    addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                                                        $noty.close();

                                                        //alert($scopeChild.cusObj.Commission);
                                                        //  alert($scopeChild.cusObj.Amount);
                                                        $scopeChild.bankObj.SGSAmount = $scopeChild.cusObj.Commission;
                                                        $scopeChild.bankObj.IBSAmount = $scopeChild.cusObj.Amount;///PaidAmount
                                                   //     alert($scopeChild.inputAgentrate);
                                                        $scopeChild.bankObj.AgentAmount = $scopeChild.cusObj.Commission * $scopeChild.inputAgentrate / 100



                                                        ManageTransactionsService.saveBankTransaction($scopeChild.bankObj).then(function (results) {
                                                            if (results.status === true) {
                                                                ManageTransactionsService.savePayment($scope.cusReqObj, $scope.currentUser.UserID).then(function (results) {
                                                                    $scope.showLoader = false;
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
                                                                        $modalInstance.close();
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
                                                                        $modalInstance.close();
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

                                }
                                $scopeChild.updateBankDetails = function () {
                                    ManageTransactionsService.saveBankTransaction($scopeChild.bankObj).then(function (results) {
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

                        //   $scopeChild.loadBanks(BUID);




                        $scopeChild.cancel = function () {
                            $modalInstance.dismiss('cancel');
                        };
                        $scopeChild.managePolicyInfoChargeDetails = function () {
                            $scopeChild.bankObj = {};
                            $scope.cusReqObj.BankAmount = ($scope.cusReqObj.PaymentAmount * 5) / 100;
                            $scope.cusReqObj.AgentAmount = ($scope.cusReqObj.PaymentAmount * 10) / 100;

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
                        $scopeChild.myFunc = function () {


                            $scopeChild.count = 0.0;
                            $scopeChild.count = $scopeChild.cusObj.Commission * $scopeChild.inputAgentrate / 100




                        };
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
                            ManageTransactionsService.getAllInsSubClassScope(insSubClassID).then(function (results) {
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

        // alert (date);
        if (/^\d{2}\/\d{2}\/\d{4}$/.test(date)) {
            return date;
        }
        else {
            // var stringDate = date.getDate() + "";

            var t="";
            var today = new Date();
            var stringDate = new Date().toJSON().slice(0, 10).replace(/-/g, '/') + "";
            var stringMonth = today.getMonth() + 1 + "";
            var stringYear = today.getFullYear() + "";

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
                        //    $scope.cusObj.FamilyDetails = [];
                            if ($scope.cusObj.FamilyDiscount === undefined || $scope.cusObj.FamilyDiscount === "" || $scope.cusObj.FamilyDiscount === null) {
                                $scope.cusObj.FamilyDiscount = 0;
                            }
                            if ($scope.cusReqObj.AgentID === undefined || $scope.cusReqObj.AgentID === "" || $scope.cusReqObj.AgentID === null) {
                                $scope.cusObj.FamilyDiscount = 0;
                            }
                            if ($scope.cusReqObj.NIC === undefined || $scope.cusReqObj.NIC === "" || $scope.cusReqObj.NIC === null) {
                                $scope.cusObj.NIC = "";
                            }
                            if ($scope.cusReqObj.PPID === undefined || $scope.cusReqObj.PPID === "" || $scope.cusReqObj.PPID === null) {
                                $scope.cusObj.PPID = "";
                            }


                            if ($scope.cusObj.FamilyDetails != undefined) {
                                for (var i = 0; i < $scope.cusObj.FamilyDetails.length; i++) {
                                    $scope.cusObj.FamilyDetails[i].MemberDOB = $scope.getFormattedDate($scope.cusObj.FamilyDetails[i].MemberDOB);
                                    $scope.cusObj.FamilyDetails[i].JoinDate = $scope.getFormattedDate($scope.cusObj.FamilyDetails[i].JoinDate);
                                    if ($scope.cusObj.FamilyDetails[i].GroupMemberDetails.length < 1)
                                    {
                                        $scope.cusObj.FamilyDetails[i].GroupMemberDetails = [];
                                    }
                                    else
                                        {
                                            for (var j = 0; j < $scope.cusObj.FamilyDetails[i].GroupMemberDetails.length; j++) {

                                                $scope.cusObj.FamilyDetails[i].GroupMemberDetails[j].MemberDOB = $scope.getFormattedDate($scope.cusObj.FamilyDetails[i].GroupMemberDetails[j].MemberDOB);
                                                $scope.cusObj.FamilyDetails[i].GroupMemberDetails[j].JoinDate = $scope.getFormattedDate($scope.cusObj.FamilyDetails[i].GroupMemberDetails[j].JoinDate);
                                        }
                                    }
                                }

                            }



                            $scope.cusObj.BusinessUnitID = $scope.businessUnitID;
                            $scope.cusReqObj.RequestedDate = $scope.getFormattedDate($scope.cusReqObj.RequestedDate);
                            $scope.cusReqObj.PolicyStartDate = $scope.getFormattedDate($scope.cusReqObj.PolicyStartDate);
                            $scope.cusReqObj.PolicyEndDate = $scope.getFormattedDate($scope.cusReqObj.PolicyEndDate);
                          
                            $scope.cusReqObj.Exclusions = parseFloat($scope.cusObj.Exclusions);
                            $scope.cusReqObj.OptionalCovers = $scope.cusObj.OptionalCovers;
                            $scope.cusReqObj.Occupation = $scope.cusObj.Occupation;
                            //alert($scope.cusObj.FamilyDetails[0].MemberName);
                            $scope.cusReqObj.FamilyDetails = $scope.cusObj.FamilyDetails;
                            ManageTransactionsService.saveClientRequest($scope.isCustomerUpdated, $scope.isCustomerAdded, $scope.cusObj, $scope.cusReqObj, $scope.currentUser.UserID).then(function (results) {
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

        $scope.showLoader = true;
    
        ManageTransactionsService.getClientRequestByID(clientReqHeaderID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.isViewMode = false;
                $scope.isClientReqAddMode = false;
                $scope.cusReqObj = results.data;
                $scope.cusReqObj.AgentType = 1;
                $scope.cusReqObj.PolicyStartDate = $scope.cusReqObj.PolicyStartDate;
                $scope.cusReqObj.PolicyEndDate = $scope.cusReqObj.PolicyEndDate;
                $scope.cusReqObj.PartnerID = $scope.cusReqObj.PartnerID + "";
                if ($scope.cusReqObj.PaymentDetails.length > 0) {
                    $scope.cusReqObj.PaymentAmount = $scope.cusReqObj.PaymentDetails[0].PaymentAmount;
                    $scope.cusReqObj.PaymentID = $scope.cusReqObj.PaymentDetails[0].PaymentID;

                }
                else {
                    $scope.cusReqObj.PaymentAmount = $scope.cusReqObj.PremiumName;
                }
                $scope.loadClientByID(clientID);
                $scope.cusReqObj.FamilyDiscountDetails = [];
                $scope.cusReqObj.FamilyDiscountDetailsnt = [];
                if ($scope.cusReqObj.FamilyDetails.length > 2) {
                     $scope.isFamilyDiscountApply = true;
                     $scope.cusReqObj.FamilyDiscountDetails.push({ FamilyDiscountDetail: "Group Policy" });
                    //for (var i = 0; i < $scope.cusReqObj.FamilyDetails.length; i++) {

                    //    $scope.cusReqObj.FamilyDiscountDetailsnt = [];
                    //    var myString = $scope.cusReqObj.FamilyDetails[i].MemberDOB;
                    //    var arr = myString.split('/');
                    //    $scope.cusReqObj.FamilyDetails.MemberDOB = arr[2];
                    //    var currentyear = 2018;
                    //    var Age = currentyear - $scope.cusReqObj.FamilyDetails.MemberDOB;
                    //    if (Age < 25) {
                    //        $scope.isFamilyDiscountApply = true;
                    //        $scope.cusReqObj.FamilyDiscountDetails.push({ FamilyDiscountDetail: "Family Discount Applicable for this Customer" });
                    //    }
                    //    else {
                    //        $scope.isFamilyDiscountApply = false;
                    //        $scope.cusReqObj.FamilyDiscountDetailsnt.push({ FamilyDiscountDetail: "Family Discount Not Applicable for this Customer" });
                    //    }
                        if ($scope.cusReqObj.FamilyDiscountDetails.length > 0) {
                            $scope.cusReqObj.FamilyDiscountStatus = $scope.cusReqObj.FamilyDiscountDetails[0].FamilyDiscountDetail;
                        }
                        else {
                            $scope.cusReqObj.FamilyDiscountStatus = $scope.cusReqObj.FamilyDiscountDetailsnt[0].FamilyDiscountDetail;
                        }
                   // }

                }
                else {
                    $scope.isFamilyDiscountApply = false;
                    $scope.cusReqObj.FamilyDiscountDetailsnt.push({ FamilyDiscountDetail: "Individual Policy " });
                    $scope.cusReqObj.FamilyDiscountStatus = $scope.cusReqObj.FamilyDiscountDetailsnt[0].FamilyDiscountDetail;
                }
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

    //$scope.calculateNetPremium = function () {
    //    if ($scope.cusObj.LoadnigRate === undefined) {
    //        $scope.cusReqObj.AftLoading = $scope.cusReqObj.PremiumName;
    //    }
    //    else {
    //        $scope.cusReqObj.AftLoading = ($scope.cusReqObj.PremiumName * (100 - $scope.cusObj.LoadnigRate)) / 100;
    //    }
    //    if ($scope.cusObj.DeductionRate === undefined) {
    //        $scope.cusReqObj.AftDeduction = $scope.cusReqObj.AftLoading;
    //    }
    //    else {
    //        $scope.cusReqObj.AftDeduction = ($scope.cusReqObj.AftLoading * (100 - $scope.cusObj.DeductionRate)) / 100;
    //    }

    //    if ($scope.cusReqObj.FamilyDiscountDetails.length > 0) {
    //        $scope.cusReqObj.PaymentAmount = ($scope.cusReqObj.AftDeduction * (100 - $scope.cusReqObj.NewFamilyDiscount)) / 100;
    //    }
    //    else {
    //        $scope.cusReqObj.PaymentAmount = $scope.cusReqObj.AftDeduction;
    //    }
    //}

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

                            $scope.cusObj.BusinessUnitID = $scope.businessUnitID;
                            if ($scope.cusObj.PaymentDetails.length > 0) {

                            }
                            if ($scope.cusObj.ChildrenDetailss != undefined) {

                                for (var i = 0; i < $scope.cusObj.ChildrenDetailss.length; i++) {

                                    $scope.cusObj.ChildrenDetailss[i].MemberDOB = $scope.getFormattedDate($scope.cusObj.ChildrenDetailss[i].MemberDOB);
                                    $scope.cusObj.ChildrenDetailss[i].JoinDate = $scope.getFormattedDate($scope.cusObj.ChildrenDetailss[i].JoinDate);
                                    //if ($scope.cusObj.ChildrenDetailss[i].FamilyMemberID > 0) {
                                    
                                    //   }

                                }
                                $scope.cusReqObj.FamilyDetails = $scope.cusObj.ChildrenDetailss;
                                $scope.cusReqObj.PolicyStartDate = $scope.getFormattedDate($scope.cusReqObj.PolicyStartDate);
                                $scope.cusReqObj.PolicyEndDate = $scope.getFormattedDate($scope.cusReqObj.PolicyEndDate);

                                //for (var i = 0; i < $scope.cusObj.ChildrenDetailss.length; i++) {
                                //    if ($scope.cusObj.ChildrenDetailss[i].FamilyMemberID > 0) {
                                //    }
                                //    else {
                                //        $scope.cusObj.ChildrenDetailss.MemberDOB = $scope.getFormattedDate($scope.cusObj.ChildrenDetailss[i].MemberDOB);
                                //        $scope.cusReqObj.FamilyDetails.push({ MemberName: $scope.cusObj.ChildrenDetailss[i].MemberName, MemberDOB: $scope.cusObj.ChildrenDetailss.MemberDOB })
                                //    }


                                //}

                            }

                            //  $scope.cusObj.FamilyDetails = $scope.cusObj.ChildrenDetailss;
                            //  $scope.cusReqObj.FamilyDetails = $scope.cusObj.ChildrenDetailss;
                            $scope.cusReqObj.RequestedDate = $scope.getFormattedDate($scope.cusReqObj.RequestedDate);
                            //$scope.cusReqObj.AftLoading = ($scope.cusReqObj.PremiumName * (100 - $scope.cusReqObj.Loading)) / 100;
                            //$scope.cusReqObj.AftDeduction = ($scope.cusReqObj.AftLoading * (100 - $scope.cusReqObj.Deduction)) / 100;

                            // $scope.cusReqObj.Loading = $scope.availablePartners[0].text;

                            ManageTransactionsService.updateClientRequest($scope.isCustomerUpdated, $scope.isCustomerAdded, $scope.cusObj, $scope.cusReqObj, $scope.currentUser.UserID).then(function (results) {
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
        ManageTransactionsService.getClientRequestByID(clientReqHeaderID).then(function (results) {
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
                if ($scope.cusReqObj.FamilyDetails.length > 0) {
                    for (var i = 0; i < $scope.cusReqObj.FamilyDetails.length; i++) {

                        $scope.cusReqObj.FamilyDiscountDetailsnt = [];
                        var myString = $scope.cusReqObj.FamilyDetails[i].MemberDOB;
                        var arr = myString.split('/');
                        $scope.cusReqObj.FamilyDetails.MemberDOB = arr[2];
                        var currentyear = 2018;
                        var Age = currentyear - $scope.cusReqObj.FamilyDetails.MemberDOB;
                        if (Age < 25) {
                            $scope.isFamilyDiscountApply = true;
                            $scope.cusReqObj.FamilyDiscountDetails.push({ FamilyDiscountDetail: "Family Discount Applicable for this Customer" });
                        }
                        else {
                            $scope.isFamilyDiscountApply = false;
                            $scope.cusReqObj.FamilyDiscountDetailsnt.push({ FamilyDiscountDetail: "Family Discount Not Applicable for this Customer" });
                        }
                        if ($scope.cusReqObj.FamilyDiscountDetails.length > 0) {
                            $scope.cusReqObj.FamilyDiscountStatus = $scope.cusReqObj.FamilyDiscountDetails[0].FamilyDiscountDetail;
                        }
                        else {
                            $scope.cusReqObj.FamilyDiscountStatus = $scope.cusReqObj.FamilyDiscountDetailsnt[0].FamilyDiscountDetail;
                        }
                    }

                }
                else {
                    $scope.cusReqObj.FamilyDiscountDetailsnt.push({ FamilyDiscountDetail: "Family Discount Not Applicable for this Customer" });
                    $scope.cusReqObj.FamilyDiscountStatus = $scope.cusReqObj.FamilyDiscountDetailsnt[0].FamilyDiscountDetail;
                }
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
        ManageTransactionsService.initializeQuotation($scope.quotationHeader, $scope.currentUser.UserID).then(function (results) {
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
