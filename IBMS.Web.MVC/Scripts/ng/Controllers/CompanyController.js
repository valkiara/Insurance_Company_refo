'use strict';

ibmsApp.controller("CompanyController", function ($scope, $http, uiGridConstants, CompanyService, $location, AuthService) {
    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
        });
    };
    $scope.init = function () {
        $scope.getCurrentUser();
        $scope.companyObj = {};
        $scope.loadCompany();
        $scope.one = true;
       // $scope.two = false;
        
    }

    $scope.loadCompany = function () {
        $scope.showLoader = true;
        CompanyService.getAvailableCompany().then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.gridOptionsCompany = results.data;
            }
            else {
                $scope.gridOptionsCompany = [];
            }
        });
    };

   

    $scope.addCompany = function () {
        $scope.showLoader = true;
        Success();
    }
    function saveCompany(){
        CompanyService.saveCompany($scope.companyObj, $scope.currentUser).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                noty({ text: 'Successfully Saved Company Details', layout: 'topRight', type: 'success' });
                setTimeout(function () { window.location.href = "/Company/Company" }, 2500)
                ClearFields();
            }
            else {
                noty({ text: 'Error Saving Company Details', layout: 'topRight', type: 'error' });
            }
        });
    };
    $scope.editCompany = function (Company) {
        
        $("#view" + Company.CompanyID).hide();
        $("#edit" + Company.CompanyID).show();

        
    };

    $scope.cancel = function (Company) {
        //alert(id);
        $("#view" + Company.CompanyID).show();
        $("#edit" + Company.CompanyID).hide();

    };

    $scope.update = function (Company) {
        $("#view" + Company.CompanyID).show();
        $("#edit" + Company.CompanyID).hide();
        SuccessUpdate(Company)
        //Update(Company);
    };
    function Update(Company) {


        var companyID = Company.CompanyID;
        var companyName = Company.CompanyName;
        var IsActive = Company.IsActive;
        




        CompanyService.updateCompany(
           companyID, companyName, IsActive, $scope.currentUser.UserID
           ).
           then(function (results) {
               if (results.status === true) {
                   noty({ text: 'Successfully Updated Company Details', layout: 'topRight', type: 'success' });

                   setTimeout(function () { window.location.href = "/Company/Company" }, 2500)

                   ClearFields();
               }
               else {
                   noty({ text: 'Error Update Company Details', layout: 'topRight', type: 'error' });
               }
           });

    };

    $scope.deleteCompany = function (companyID) {
        $scope.showLoader = true;
        SuccessDelete(companyID);
    }
    function DeleteCom(companyID) {
        CompanyService.deleteCompany(companyID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                noty({ text: 'Successfully Deleted Company Details', layout: 'topRight', type: 'success' });
                setTimeout(function () { window.location.href = "/Company/Company" }, 2500)
            }
            else {
                noty({ text: 'Error Deleteing Company Details', layout: 'topRight', type: 'error' });
            }
        });
    };

    function Success() {
        //alert("cc");
        noty({
            text: 'Do you want to Save Company Details?',
            layout: 'topRight',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            saveCompany();
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

    function SuccessUpdate(Company) {
        //alert("cc");
        noty({
            text: 'Do you want to Update Company Details?',
            layout: 'topRight',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            Update(Company);

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

    function SuccessDelete(companyID) {
        //alert("cc");
        noty({
            text: 'Are you sure want to Delete Company Details?',
            layout: 'topRight',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            DeleteCom(companyID);

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