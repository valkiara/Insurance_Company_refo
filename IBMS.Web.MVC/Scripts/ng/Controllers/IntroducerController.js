'use strict';

ibmsApp.controller("IntroducerController", function ($scope, $http, IntroducerService, $window, AuthService, filterFilter) {
    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
            getAllIntroducer();
        });
    };
    //$scope.message = "nnj";
    // alert("");
    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': 'Basic VGVzdDoxMjM=' }

    };



    getAllBusinessUnits();
    //getAllIntroducer();
    $scope.getCurrentUser();

    $scope.Description = null;
    $scope.Address1 = null;
    $scope.Address2 = null;
    $scope.Address3 = null;
    // getAllInsuranceCompany();
    $scope.refreshContent = function () {
        getAllIntroducer();
        $scope.search_query = "";
    };

    $scope.ClearFields = function () {
        $scope.Introducers = null;
        $scope.IntroducerBU = null;
        $scope.Description = null;
        $scope.Address1 = null;
        $scope.Address2 = null;
        $scope.Address3 = null;
        for (var i = 0; i < businessUnit.length; i++) {
            if (businessUnit[i].IsChecked) {
                $scope.IntroducerBU.push(businessUnit[i].BusinessUnitID);
            }
        }
    };
    $scope.BUnits = [];
    //  $scope.InsuranceCompnaies = [];
    $scope.init = function () {
        //TO DO
        $("#edit" + id).show();
    };
    $scope.edit = function (Introducer) {

        // alert(InsCompnay.InsuranceCompanyID);
        $("#view" + Introducer.IntroducerID).hide();
        $("#edit" + Introducer.IntroducerID).show();

    };
    $scope.Delete = function (IntroducerID) {
        $scope.showLoader = true;
        SuccessDelete(IntroducerID);
        //alert("fg");
        //notyConfirm(Introducer, introducers, index);

    };
    $scope.update = function (Introducer) {
        //alert(InsCompnay.InsuranceCompanyName);
        $scope.showLoader = true;
        $("#view" + Introducer.IntroducerID).show();
        $("#edit" + Introducer.IntroducerID).hide();
        //Update(Introducer);
        SuccessUpdate(Introducer)

    };
    $scope.cancel = function (Introducer) {
        //alert(id);
        $("#view" + Introducer.IntroducerID).show();
        $("#edit" + Introducer.IntroducerID).hide();
        $scope.refreshContent();
    };
    $scope.Save = function () {
        $scope.showLoader = true;
        Success();
    }
    function saveIntroducer() {
        $scope.IntroducerBU = [];
        var intruducerName = $scope.Introducers;
        var insCompanyID = $scope.InsuranceCompanyID;
        var businessUnit = $scope.BusinessTest;
        for (var i = 0; i < businessUnit.length; i++) {
            if (businessUnit[i].IsChecked) {
                $scope.IntroducerBU.push( businessUnit[i].BusinessUnitID);
            }
        }
        var BusinessUnits = $scope.IntroducerBU;
        var name = $scope.Name;
        var description = $scope.Description
        var address1 = $scope.Address1;
        var address2 = $scope.Address2;
        var address3 = $scope.Address3;
        var UserID = $scope.currentUser.UserID;

        //var contactPerson = $scope.contactName;
        //var contactNo = $scope.contactNo;
        //var email = $scope.Email;
        //var fax = $scope.fax;

        
            IntroducerService.saveIntroducerData(
                intruducerName, insCompanyID, BusinessUnits, description,
               address1, address2,
               address3, UserID).
               then(function (results) {
                   $scope.showLoader = false;
                   //$Scope.BussinessUnit = results;
                   //$scope.BUnits = results.data;
                   //for (var key in results.data) {
                   //alert(angular.toJson(results.message));
                   if (results.status === true)
                   {
                       noty({
                           text: 'Successfully Saved Introducer Details',
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
                           text: 'Error Saving Introducer Details.' + " " + results.message,
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
            getAllInsuranceCompany();

        };


        function Update(Introducer) {

            //TO DO
        
            $scope.IntroducerUPBU = [];
            var intruducerID = Introducer.IntroducerID;
            var introducername = Introducer.IntroducerName;
            var businessUnitID = Introducer.buidList;
            for (var i = 0; i < businessUnitID.length; i++) {
                if (businessUnitID[i].IsChecked) {
                    $scope.IntroducerUPBU.push(businessUnitID[i].BusinessUnitID);
                }
            }
            var BusinessUnits = $scope.IntroducerUPBU;
            var description = Introducer.Description;
            var address1 = Introducer.Address1;
            var address2 = Introducer.Address2;
            var address3 = Introducer.Address3;
            var userID = $scope.currentUser.UserID;
     
        
            IntroducerService.UpdateIntroducerData(
                intruducerID, introducername, BusinessUnits, description,
               address1, address2,
               address3, userID).
               then(function (results) {
                   $scope.showLoader = false;
                   //$Scope.BussinessUnit = results;
                   //$scope.BUnits = results.data;
                   //for (var key in results.data) {
                   //alert(angular.toJson(results.message));
                   //noty({ text: 'Successful action', layout: 'topRight', type: 'success' })

                   //}
                   if (results.status === true) {
                       noty({
                           text: 'Successfully Updated Introducer Details',
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
                           text: 'Error Update Introducer Details.' + " " + results.message,
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
        function DeleteInt(Introducers) {
           // SuccessDelete(Introducer);
        
            //var intruducerID = Introducer.IntruducerID;


            IntroducerService.DeleteIntroducerData(Introducers).
               then(function (results) {
                   //$Scope.BussinessUnit = results;
                   //$scope.BUnits = results.data;
                   //for (var key in results.data) {
                   //alert(angular.toJson(results.message));
                   //noty({ text: 'Successful action', layout: 'topRight', type: 'success' })

                   //}
                   $scope.showLoader = false;
                   if (results.status === true) {
                       noty({
                           text: 'Successfully Deleted Introducer Details',
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
                           text: 'Error Deleteing Introducer Details',
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
        function getAllBusinessUnits() {
            //alert("hh");
            IntroducerService.getAllBusinessUnits().then(function (results) {
                //$Scope.BussinessUnit = results;
                $scope.BUnits = results.data;
                $scope.BusinessTest = [];
                for (var i = 0; i < $scope.BUnits.length; i++) {
                    $scope.BusinessTest.push({ "BusinessUnitID": $scope.BUnits[i].BusinessUnitID, "BusinessUnitName": $scope.BUnits[i].BusinessUnit, "IsChecked": false });
                }
                //for (var key in results.data) {
                //alert(angular.toJson(results.data[0].BusinessUnit));


                //}


            });
        };
        function getInsuranceCompanyByID() {
            //alert("hh");
            var id = 1;
            IntroducerService.getInsuranceCompanyByID(id).then(function (results) {
                //$Scope.BussinessUnit = results;
                $scope.BUnits = results.data;
                //for (var key in results.data) {
                //alert(angular.toJson(results.data[0].BusinessUnit));


                //}


            });
        };
        //function getAllInsuranceCompany() {
        //    //alert("hh");
        //    IntroducerService.getAllInsuranceCompanny().then(function (results) {
        //        //$Scope.BussinessUnit = results;
        //        $scope.InsuranceCompnaies = results.data;
        //        //alert(angular.toJson(results.data));


        //        //}



        //    });
        //};

        function getAllIntroducer() {
            //alert("hh");
            $scope.showLoader = true;
            IntroducerService.getAllIntroducer($scope.currentUser.BusinessUnitID).then(function (results) {
                $scope.showLoader = false;
                //$Scope.BussinessUnit = results;
                $scope.Introducer = results.data;
                var data = $scope.Introducer;

                $scope.data = angular.copy($scope.Introducer);
                $scope.viewby = "5";
                $scope.totalItems = $scope.data.length;
                $scope.currentPage = 1;
                $scope.itemsPerPage = $scope.viewby;
                $scope.maxSize = 5; //Number of pager buttons to show
                $scope.setItemsPerPage($scope.viewby);
                $scope.IntBusinessTest = [];

                for (var i = 0; i < $scope.BUnits.length; i++) {
                    $scope.IntBusinessTest.push({ "BusinessUnitID": $scope.BUnits[i].BusinessUnitID, "BusinessUnitName": $scope.BUnits[i].BusinessUnit, "IsChecked": false });
                }

                for (var i = 0; i < $scope.data.length; i++) {
                    $scope.data[i].buidList = angular.copy($scope.IntBusinessTest);

                    for (var j = 0; j < $scope.data[i].buidList.length; j++) {
                        if ($.inArray($scope.data[i].buidList[j].BusinessUnitID, $scope.data[i].BusinessUnitIDList) > -1) {
                            $scope.data[i].buidList[j].IsChecked = true;
                        }
                    }
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
        $scope.data = filterFilter($scope.Introducer, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);

        for (var i = 0; i < $scope.data.length; i++) {
            $scope.data[i].buidList = angular.copy($scope.IntBusinessTest);

            for (var j = 0; j < $scope.data[i].buidList.length; j++) {
                if ($.inArray($scope.data[i].buidList[j].BusinessUnitID, $scope.data[i].BusinessUnitIDList) > -1) {
                    $scope.data[i].buidList[j].IsChecked = true;
                }
            }
        }
    };
       
   
    function Success() {
        //alert("cc");
        noty({
            text: 'Do you want to Save Introducer Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            saveIntroducer();
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

    function SuccessUpdate(Introducer) {
        //alert("cc");
        noty({
            text: 'Do you want to Update Introducer Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            Update(Introducer)

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

    function SuccessDelete(Introducers) {
        //alert("cc");
        noty({
            text: 'Are you sure want to Delete Introducer Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            DeleteInt(Introducers);

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