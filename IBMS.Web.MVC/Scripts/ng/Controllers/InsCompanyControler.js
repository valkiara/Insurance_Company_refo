'use strict';

ibmsApp.controller("InsCompanyController", function ($scope, $http, InsCompanyService, $window, AuthService, filterFilter) {
    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
            $scope.businessUnitID = results.BusinessUnitID;
            $scope.BusinessUnit = results.BusinessUnitID;
            getAllBusinessUnits();
            getAllInsuranceCompany();
        });
    };
    //$scope.message = "nnj";

    



    //getAllBusinessUnits();
    //getAllInsuranceCompany();
    $scope.getCurrentUser();

    $scope.Address1 = null;
    $scope.Address2 = null;
    $scope.Address3 = null;
    $scope.contactName = null;
    $scope.contactNo = null;
    $scope.Email = null;
    $scope.fax = null;

    $scope.BUnits = [];
    $scope.InsuranceCompnaies = [];
    $scope.init = function () {
      //  $scope.BusinessUnit = $scope.currentUser.BusinessUnitID;
      //  $("#edit" + id).show();
    };
    $scope.edit = function (InsCompnay) {
   
       // alert(InsCompnay.InsuranceCompanyID);
        $("#view" + InsCompnay.InsuranceCompanyID).hide();
        $("#edit" + InsCompnay.InsuranceCompanyID).show();

    };
    $scope.Delete = function (InsCompnay) {
            $scope.showLoader = true;
        
        SuccessDelete(InsCompnay);
       // notyConfirm(InsCompnay,insCompanies,index);

    };
    $scope.update = function (InsCompnay) {
        //alert(InsCompnay.InsuranceCompanyName);

        $("#view" + InsCompnay.InsuranceCompanyID).show();
        $("#edit" + InsCompnay.InsuranceCompanyID).hide();
        Update(InsCompnay);

    };
    $scope.cancel = function (InsCompnay) {
        //alert(id);
        $("#view" + InsCompnay.InsuranceCompanyID).show();
        $("#edit" + InsCompnay.InsuranceCompanyID).hide();
        $scope.refreshContent();
    };
    $scope.refreshContent = function () {
        getAllInsuranceCompany();
        $scope.search_query = "";
    };

    $scope.ClearFields = function () 
    {
        $scope.insCompany = null;
        $scope.BusinessUnit = null;
        $scope.Address1 = null;
        $scope.Address2 = null;
        $scope.Address3 = null;
        $scope.contactName = null;
        $scope.contactNo = null;
        $scope.contactNo1 = null;
        $scope.contactNo2 = null;
        $scope.Email = null;
        $scope.fax = null;
        $scope.vatReg = null;
    };
    $scope.Save = function () {
        $scope.showLoader = true;
        Success();
        //TO DO
    }
    function saveCom(){
        var businessUnitID = $scope.BusinessUnit;
        var insCompanyName = $scope.insCompany;
        var address1=$scope.Address1;
        var address2=$scope.Address2;
        var address3=$scope.Address3;
        var contactPerson=$scope.contactName;
        var contactNo=$scope.contactNo;
        var email=$scope.Email;
        var fax = $scope.fax;
        var UserID = $scope.currentUser.UserID;



        InsCompanyService.saveInsCompData(
            businessUnitID, insCompanyName,
           address1, address2,
           address3, contactPerson,
           contactNo,
           email, fax, UserID).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Saved Insurance Company Details',
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
                       text: 'Error Saving Insurance Company Details.' + " " + results.message,
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
    $scope.refresh = function ()
    {
        getAllInsuranceCompany();

    };


    function Update(InsCompnay)
    {
        $scope.showLoader = true;
        SuccessUpdate(InsCompnay);
    }

    function UpdateCom(InsCompnay) {
        
        //TO DO
        var businessUnitID = InsCompnay.BusinessUnitID;
        var insCompanyID = InsCompnay.InsuranceCompanyID;
        var InCompanyName = InsCompnay.InsuranceCompanyName;
        var address1 = InsCompnay.Address1;
        var address2 = InsCompnay.Address2;
        var address3 = InsCompnay.Address3;
        var contactPerson = InsCompnay.ContactPerson;
        var contactNo = InsCompnay.ContactNo;
        var email = InsCompnay.Email;
        var fax = InsCompnay.Fax;
        var UserID = $scope.currentUser.UserID;



        InsCompanyService.UpdateInsCompData(
           businessUnitID,insCompanyID, InCompanyName,
           address1, address2,
           address3, contactPerson,
           contactNo,
           email, fax, UserID).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Updated Insurance Company Details',
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
                       text: 'Error Update Insurance Company Details.' + " " + results.message,
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
    function Delete(InsCompnay) {
        $scope.showLoader = true;
        SuccessDelete(InsCompnay)
    }
    function DeleteCom(InsCompnay) {

        //TO DO
        var insCompanyID = InsCompnay.InsuranceCompanyID;


        InsCompanyService.DeleteInsCompData(insCompanyID).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Deleted Insurance Company Details',
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
                   $scope.refreshContent();
               }
               else {
                   noty({
                       text: 'Error Deleteing Insurance Company Details',
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
        if ($scope.currentUser.AccessLevelTypeName == "Admin") { 
        InsCompanyService.getAllBusinessUnits().then(function (results) {
            //$Scope.BussinessUnit = results;
            $scope.BUnits = results.data;
            if (results.status === true) {
                $scope.filteredBusinessUnits = results.data;

                //for (var i = 0; i < $scope.BUnits.length; i++) {
                //    if ($scope.BUnits[i].BusinessUnitID === $scope.currentUser.BusinessUnitID) {
                //        $scope.filteredBusinessUnits.push({ "BusinessUnitID": $scope.BUnits[i].BusinessUnitID, "BusinessUnit": $scope.BUnits[i].BusinessUnit });
                //        break;
                //    }
                //}

            }
        });

        }
        else {
            $scope.filteredBusinessUnits = [];

            $scope.filteredBusinessUnits.push({
                "BusinessUnitID": $scope.currentUser.BusinessUnitID,
                "BusinessUnit": $scope.currentUser.BusinessUnitName

            });
        }
        
        
    };
    function getInsuranceCompanyByID() {
      //alert("hh");
      var id=1;
      InsCompanyService.getInsuranceCompanyByID(id).then(function (results) {
          //$Scope.BussinessUnit = results;
          $scope.BUnits = results.data;
          //for (var key in results.data) {
          //alert(angular.toJson(results.data[0].BusinessUnit));


          //}


      });
  };
    function getAllInsuranceCompany() {
        $scope.showLoader = true;
        InsCompanyService.getAllInsuranceCompanny($scope.currentUser.BusinessUnitID).then(function (results) {
          $scope.showLoader = false;
          $scope.InsuranceCompnaies = results.data;

          var data = $scope.InsuranceCompnaies;

          $scope.data = angular.copy($scope.InsuranceCompnaies);
          $scope.viewby = "5";
          $scope.totalItems = $scope.data.length;
          $scope.currentPage = 1;
          $scope.itemsPerPage = $scope.viewby;
          $scope.maxSize = 5; //Number of pager buttons to show
          $scope.setItemsPerPage($scope.viewby);
          //alert(angular.toJson(results.data));


          //}


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
        $scope.data = filterFilter($scope.InsuranceCompnaies, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };

    function notyConfirm(InsCompnay, insCompanies, index) {
        var r = confirm("Are You Sure you want Delete this Record of " + InsCompnay.InsuranceCompanyName);
        if (r == true)
        {
          Delete(InsCompnay);
          insCompanies.splice(index, 1);
        }
     // alert("dd");

    }

    function Success() {
        //alert("cc");
        noty({
            text: 'Do you want to Save Insurance Company Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            saveCom();
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

    function SuccessUpdate(InsCompnay) {
        //alert("cc");
        noty({
            text: 'Do you want to Update Insurance Company Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            UpdateCom(InsCompnay);

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

    function SuccessDelete(InsCompnay) {
        //alert("cc");
        noty({
            text: 'Are you sure want to Delete Insurance Company Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            DeleteCom(InsCompnay);

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