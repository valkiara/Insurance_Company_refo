'use strict';
// edited
ibmsApp.controller("SingaporeAdmissionController", function ($scope, $http, $rootScope, SingaporeAdmissionService, AuthService, $modal) {


    $scope.paginationTopNumberList = [];


    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;

            if ($scope.currentUser.AccessLevelTypeName === "Admin") {

            }
            else {
                //Load SGS Customers
                $scope.businessUnitID = $scope.currentUser.BusinessUnitID;
                $scope.businessUnitID = "1068";
                //$scope.businessUnitID = $scope.currentUser.BusinessUnitID;
                $scope.loadClientDeductionByBUID($scope.businessUnitID);
             $scope.GetAllAdmissions($scope.currentUser.BusinessUnitID);
                 $scope.loadAllHospitals("1");
                     $scope.isUpdate = false;

            }
        });
    };


    $scope.init = function () {
        //alert("Test");
            $scope.isUpdate = false;
        $scope.isAddtMode = true;
        $scope.businessUnitID = "";
        $scope.currentUser = [];
        $scope.availableClients = [];
        $scope.availableAdmissions = [];
        $scope.availablePartners = [];
        $scope.availableHospitals = [];
        $scope.Client = {};
        $scope.admission = {};
        $scope.availableCountries = [];
        //$scope.admission.FamilyDetailsGlobal = [];
        $scope.availableFamilyMembers = [];
        $scope.singaporAdmissionObj = {};
        $scope.singaporAdmissionObjDesction = {};
        $scope.getCurrentUser();
        $scope.loadPartners();
        //getAllLocalAdmission();
      
        $scope.Countries();


    }
    $scope.getFormattedDate = function (date) {
        if (/^\d{2}\/\d{2}\/\d{4}$/.test(date)) {
            return date;
        }
        else {

            //if (date.length > 0) {
            var stringDate = date.getDate() + "";
            var stringMonth = date.getMonth() + 1 + "";
            var stringYear = date.getFullYear() + "";

            if (stringDate.length < 2)
                stringDate = '0' + stringDate;
            if (stringMonth.length < 2)
                stringMonth = '0' + stringMonth;

            return [stringDate, stringMonth, stringYear].join('/');
            // }
        }
    };
    $scope.loadClientDeductionByBUID = function (businessUnitID) {
        //$scope.showLoader = true;


        // alert(bisid);
        SingaporeAdmissionService.getAllDeductions(businessUnitID).then(function (results) {
            //$scope.showLoader = false;
            if (results.status === true) {


                for (var i = 0; i < results.data.length; i++) {
                    //if (results.data[i].BUID === bisid) {

                    $scope.availableClients.push({ value: results.data[i].DeductionID, text: results.data[i].PremiumHolder + " (Client ID: " + results.data[i].ClientID + ")" })
                    // }
                }

            }
            else {
                $scope.availableClients = [];
            }
        });
    };






    $scope.GetAllAdmissions = function (businessUnitID) {
        $scope.showLoader = true;
        SingaporeAdmissionService.GetAllSingporeAdmissions(businessUnitID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                
                $scope.data = angular.copy(results.data);
                //  $scope.admission= $scope.data;
               
                $scope.viewby = "10";
                $scope.totalItems = $scope.data.length;
                $scope.currentPage = 1;
                $scope.itemsPerPage = $scope.viewby;
                $scope.maxSize = 10; //Number of pager buttons to show

                $scope.setItemsPerPage($scope.viewby);
            }
            else {
               
            }
        });
    };

    $scope.Countries = function () {
        SingaporeAdmissionService.getAllCountries().then(function (results) {
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableCountries.push({ value: results.data[i].CountryID, text: results.data[i].CountryName })

                }
            }
            else {
                $scope.availableCountries = [];
            }
        });
    };

    $scope.LoadHospital = function (countryId) {
        var countryIdHospital = countryId;
        SingaporeAdmissionService.getAllHospitals().then(function (results) {
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {

                    if ($scope.admission.CountryID === results.data[i].CountryID) {

                        $scope.availableHospitals.push({ value: results.data[i].HospitalID, text: results.data[i].HospitalName });
                    }
                }
            }
            else {
                $scope.availableHospitals = [];
            }
        });
    };

    $scope.refreshContent = function () {
        getAllAgent();
        $scope.search_query = "";
    };

    function save() {
        $scope.showLoader = true;

        //SingaporeAdmissionService.save($scope.admission).then(function (result) {
        //    $scope.showLoader = false;

        //    if (result.status === true) {
        //        noty({
        //            text: 'Admission Successfully Saved',
        //            layout: 'topCenter',
        //            type: 'success',
        //            buttons: [
        //                         {
        //                             addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
        //                                 $noty.close();
        //                             }
        //                         }
        //            ]
        //        });

        //        $scope.ClearFields();
        //        $scope.refreshContent();
        //        // redirect page to invoice
        //        window.location.assign("GetSingaporeInvoice");
        //    }
        //    else {
        //        noty({
        //            text: 'Error Saving Local Admissions Detail.' + " " + result.message,
        //            layout: 'topCenter',
        //            type: 'error',
        //            buttons: [
        //                       {
        //                           addClass: 'btn btn-danger btn-clean', text: 'Ok', onClick: function ($noty) {
        //                               $noty.close();
        //                           }
        //                       }
        //            ]
        //        });
        //    }
        //})




        SingaporeAdmissionService.save($scope.admission).then(function (results) {
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
    function Success() {
        noty({
            text: 'Do you want to Save Admissions Detail?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            save($scope.admission);
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
    };



    $scope.saveRequest = function () {
        $scope.showLoader = true;
        noty({
            text: 'Do you want to Save  Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {

                            alert($scope.admission.DateOfBirth);

                            if ($scope.admission.DateOfBirth === undefined || $scope.admission.DateOfBirth === "" || $scope.admission.DateOfBirth === null)
                                $scope.admission.DateOfBirth = "";
                            else
                                $scope.admission.DateOfBirth = $scope.getFormattedDate($scope.admission.DateOfBirth);


                            if ($scope.admission.AdmissionDate === undefined || $scope.admission.AdmissionDate === "" || $scope.admission.AdmissionDate === null)
                                $scope.admission.AdmissionDate = "";
                            else


                                $scope.admission.AdmissionDate = $scope.getFormattedDate($scope.admission.AdmissionDate);


                            if ($scope.admission.DischargedDate === undefined || $scope.admission.DischargedDate === "" || $scope.admission.DischargedDate === null)
                                $scope.admission.DischargedDate = "";
                            else


                                $scope.admission.DischargedDate = $scope.getFormattedDate($scope.admission.DischargedDate);



                            if ($scope.admission.ExtendedGOPDate === undefined || $scope.admission.ExtendedGOPDate === "" || $scope.admission.ExtendedGOPDate === null)
                                $scope.admission.ExtendedGOPDate = "";
                            else


                                $scope.admission.ExtendedGOPDate = $scope.getFormattedDate($scope.admission.ExtendedGOPDate);

                            if ($scope.admission.ReferalFeeReceivedDate === undefined || $scope.admission.ReferalFeeReceivedDate === "" || $scope.admission.ReferalFeeReceivedDate === null)
                                $scope.admission.ReferalFeeReceivedDate = "";
                            else


                                $scope.admission.ReferalFeeReceivedDate = $scope.getFormattedDate($scope.admission.ReferalFeeReceivedDate);

                            if ($scope.admission.FinalBillRecievedDate === undefined  || $scope.admission.FinalBillRecievedDate === "" || $scope.admission.FinalBillRecievedDate === null)
                                $scope.admission.FinalBillRecievedDate = "";
                            else


                                $scope.admission.FinalBillRecievedDate = $scope.getFormattedDate($scope.admission.FinalBillRecievedDate);

                            if ($scope.admission.InimatedDate === undefined || $scope.admission.InimatedDate === "" || $scope.admission.InimatedDate === null)
                                $scope.admission.InimatedDate = "";
                            else


                                $scope.admission.InimatedDate = $scope.getFormattedDate($scope.admission.InimatedDate);

                            if ($scope.admission.InceptionDate === undefined || $scope.admission.InceptionDate === "" || $scope.admission.InceptionDate === null)
                                $scope.admission.InceptionDate = "";
                            else


                                $scope.admission.InceptionDate = $scope.getFormattedDate($scope.admission.InceptionDate);
                
                            

                            SingaporeAdmissionService.save($scope.admission).then(function (results) {
                                $scope.showLoader = false;
                                //  alert(angular.toJson(results));
                                if (results.status === true) {
                                    noty({
                                        text: 'Successfully  Request Details',
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












    $scope.ClearFields = function () {

        $scope.admission.ReferenceNo = "";
        $scope.admission.PatientName = "";
        $scope.admission.DateOfBirth = "";
        $scope.admission.PassportNumber = "";
        $scope.admission.Scheme = "";
        $scope.admission.InceptionDate = "";
        $scope.admission.InimatedDate = "";
        $scope.admission.ExtendedGOPDate = "";
        $scope.admission.FinalBillAmount = "";
        $scope.admission.CMAInvoiceNumber = "";
        
        $scope.admission.Deductible = "";
        $scope.admission.DeductibleUsedForTheYear = "";
        $scope.admission.Exclusions = "";
        $scope.admission.Hospital = "";
        $scope.admission.AdmissionDate = "";
        $scope.admission.DischargedDate = "";
        $scope.admission.BHTNumber = "";
        $scope.admission.Illness = "";
        $scope.admission.ConsultantName = "";
        $scope.admission.InformedBy = "";
        $scope.admission.GOPAmount = "";
        $scope.admission.GOPConfirmBy = "";
        $scope.admission.GOPIssueDate = "";
        $scope.admission.ExtendedGOP = "";
        $scope.admission.HandledBy = "";
        $scope.admission.FinalAmount = "";
        $scope.admission.ConsultantFee = "";
        $scope.admission.FinalBillRecievedDate = "";
        $scope.admission.FinalBillGivenToSgs = "";
        $scope.admission.ClaimsDcsSentToAviva = "";
        $scope.admission.ClaimSettledDate = "";
        $scope.admission.ReferalFee = "";
        $scope.admission.ReferalFeeReceivedDate = "";
        $scope.admission.ReferalFeeReceivedBank = "";
        $scope.admission.ReferalFeeReceivedChequeNumber = "";
        $scope.admission.ReferalFeeReceivedTtTransfer = "";
        $scope.admission.PaymentGivenToAccount = "";
        $scope.admission.Remark = "";
        $scope.admission.CaseNumber = "";
        $scope.admission.IntimatedDate = "";
        $scope.isAddtMode = false;
    };

    $scope.loadAllHospitals = function () {
        SingaporeAdmissionService.getAllHospitals().then(function (results) {
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableHospitals.push({ value: results.data[i].HospitalID, text: results.data[i].HospitalName });
                }
            }
            else {
                $scope.availableHospitals = [];
            }
        });
    };


    $scope.cancelAdmission = function () {
        $scope.ClearFields();
        $scope.refreshContent();
        $scope.activateAdmissionListTab();

        $scope.isUpdate = false;
    };

    $scope.Save = function () {
        $scope.showLoader = true;
        Success();
    };

    $scope.loadClientsByBUID = function (businessUnitID) {
        //$scope.showLoader = true;
        SingaporeAdmissionService.getAllDeductions(businessUnitID).then(function (results) {
            //$scope.showLoader = false;
            $scope.singaporAdmissionObjDesction = results.data;
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {

                    $scope.availableClients.push({ value: results.data[i].DeductionID, text: results.data[i].PremiumHolder + " (Client ID: " + results.data[i].ClientID + ")" })

                    //if(results.data[i].FamilyMemberID===null)
                    //{

                    //    $scope.availableClients.push({ value: results.data[i].DeductionID, text: results.data[i].PremiumHolder + " (Client ID: " + results.data[i].ClientID + ")" })
                    //    $scope.singaporAdmissionObj = results.data;
                    //    $scope.singaporAdmissionObj.PatientName = results.data[i].PremiumHolder;
                    //    $scope.singaporAdmissionObj.FamilyMemberID = results.data[i].FamilyMemberID;
                    //    $scope.singaporAdmissionObj.PatientID = results.data[i].ClientID;

                    //}
                    //else 
                    //{

                    //    $scope.availableClients.push({ value: results.data[i].FamilyMemberID, text: results.data[i].PremiumHolder + " (Client ID: " + results.data[i].ClientID + ")" })
                    //    $scope.singaporAdmissionObj = results.data;
                    //    $scope.singaporAdmissionObj.PatientName = results.data[i].PremiumHolder;
                    //    $scope.singaporAdmissionObj.FamilyMemberID = results.data[i].FamilyMemberID;
                    //    $scope.singaporAdmissionObj.PatientID = results.data[i].FamilyMemberID;

                    //}

                             

                }

                //$scope.isAddtMode = false;
            }
            else {
                $scope.singaporAdmissionObj = [];
                $scope.availableClients = [];
            }
        });
    };

    $scope.loadSingaporAdmissionByID = function (ClientRequestHeaderID) {
        //Get  deductionID
        $scope.isAddtMode = false;
        $scope.isUpdate = true;
        var clientheaderid = ClientRequestHeaderID;
        for (var i = 0; i < $scope.singaporAdmissionObjDesction.length ; i++) {
            if ($scope.singaporAdmissionObjDesction[i].DeductionID == ClientRequestHeaderID) {

                $scope.admission.PatientID = $scope.singaporAdmissionObjDesction[i].DeductionID;
                $scope.admission.PatientName = $scope.singaporAdmissionObjDesction[i].PremiumHolder;
                $scope.admission.InceptionDate = $scope.singaporAdmissionObjDesction[i].JoinDate;
                $scope.admission.Exclusions = $scope.singaporAdmissionObjDesction[i].Exclusions;
                $scope.admission.DeductionID = $scope.singaporAdmissionObjDesction[i].DeductionID;
                $scope.admission.Deductible = $scope.singaporAdmissionObjDesction[i].DeductionRate;
                $scope.admission.PremiumID = $scope.singaporAdmissionObjDesction[i].PremiumID;
                //$scope.admission.Deductible = 1000;
                //$scope.admission.PassportNumber = $scope.Client.PPID;

          

            SingaporeAdmissionService.loadClientsByBUID($scope.singaporAdmissionObjDesction[i].ClientID).then(function (results) {
                if (results.status === true) {
                    $scope.Client = results.data;





                    if ($scope.singaporAdmissionObjDesction[i].PremiumHolderType === "1") {

                        $scope.admission.DateOfBirth = $scope.Client.DOB;
                        $scope.admission.PassportNumber = $scope.Client.PPID;



                    }
                    else if ($scope.singaporAdmissionObjDesction[i].PremiumHolderType === "2") {

                        if ($scope.Client.FamilyDetails.length > 0) {

                            if ($scope.Client.FamilyDetails[i].FamilyMemberID === $scope.singaporAdmissionObjDesction[i].FamilyMemberID) {


                                $scope.admission.DateOfBirth = $scope.Client.FamilyDetails[i].MemberDOB;
                                $scope.admission.PassportNumber = $scope.Client.FamilyDetails[i].NICNo;

                            }

                            else {


                            }


                        }

                    }




                }
                else {

                }
            });

            }

        }
        //singaporAdmissionObjDesction get  deduction table details
  
      
                
              
                

               
               
       
    };

    $scope.loadMemberDetails = function () {


        var clientId = $scope.admission.ClientID;
        SingaporeAdmissionService.getClientByID(clientId).then(function (results) {
            if (results.status === true) {

                $scope.Client = results.data;

                $scope.admission.DateOfBirth = $scope.Client.DOB;
                $scope.admission.PassportNumber = $scope.admission.PPID;


            }
            else {

            }

        });
    };

    $scope.LoadFamilyMemberDetails = function (FamilyMemberID) {

        $scope.availableFamilyMembers;
        $scope.admission.FamilyDetailsGlobal;
    };

    $scope.loadPartners = function () {
        //$scope.showLoader = true;
        SingaporeAdmissionService.getAllPartners().then(function (results) {
            //$scope.showLoader = false;
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availablePartners.push({ value: results.data[i].PremiumID, text: results.data[i].PremiumName })
                }
            }
            else {
                $scope.availablePartners = [];
            }
        });
    };

    function getAllLocalAdmission() {
        try {
            $scope.paginationTopNumberList = [];
            $scope.showLoader = true;
            SingaporeAdmissionService.getAllLocalAdmission().then(function (results) {
                $scope.showLoader = false;
                $scope.Admissions = results.data;

                $scope.data = angular.copy($scope.Admissions);
                $scope.viewby = "5";
                $scope.totalItems = $scope.data.length;
                $scope.currentPage = 1;
                $scope.itemsPerPage = $scope.viewby;
                $scope.maxSize = 5; //Number of pager buttons to show
                $scope.setItemsPerPage($scope.viewby);
            });
        } catch (e) {
            console.log(e);
        }
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
        $scope.data = filterFilter($scope.Agents, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };
    $scope.refreshContent = function () {
        getAllLocalAdmission();
        $scope.search_query = "";
    };
    $scope.getAdmissionByRefNo = function (referenceNo) {
        $scope.activateNewAdmissionTab();
        $scope.isAddtMode = true;
        $scope.showLoader = true;
        SingaporeAdmissionService.getAdmissionByRefNo(referenceNo).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.isUpdate = true;
                console.log(results.data);
                $scope.admission = results.data;
            }
            else {
                $scope.isUpdate = false;
            }
        });
    };

    $scope.activateNewAdmissionTab = function () {
        $("#list-tab-1").removeClass('active');
        $("#tab-1").removeClass('tab-pane active');

        $("#list-tab-2").addClass('active');
        $("#tab-2").addClass('tab-pane active');

        $("#tab-1").css("display", "none");
        $("#tab-2").css("display", "block");
    };
    $scope.activateAdmissionListTab = function () {
        $("#list-tab-1").addClass('active');
        $("#tab-1").addClass('tab-pane active');

        $("#list-tab-2").removeClass('active');
        $("#tab-2").removeClass('tab-pane active');

        $("#tab-1").css("display", "block");
        $("#tab-2").css("display", "none");
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



});

