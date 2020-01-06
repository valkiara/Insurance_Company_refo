'use strict';

ibmsApp.controller("InsClassController", function ($scope, $http, InsClassService, $window, AuthService, filterFilter) {
    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
            $scope.businessUnitID = results.BusinessUnitID;
            $scope.BusinessUnitID = results.BusinessUnitID;
            getAllBusinessUnits();
            getAllInsClass();

        });
    };
    //$scope.message = "nnj";
    //alert("vnhg");
    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': 'Basic VGVzdDoxMjM=' }

    };
    getAllcomonay();
    //getAllInsClass();

    $scope.getCurrentUser();
    $scope.BUnits = [];
    $scope.filteredBusinessUnits = [];
    $scope.Company = [];
    $scope.InsClass = [];
    $scope.isActive = null;

    $scope.init = function () {
        //TO DO
        $scope.BusinessUnitID = $scope.currentUser.BusinessUnitID;
        $("#edit" + id).show();
    };
    $scope.edit = function (InsClas) {

        //  alert(Agent.AgentID);
        $("#view" + InsClas.InsuranceClassID).hide();
        $("#edit" + InsClas.InsuranceClassID).show();

    };
    $scope.Delete = function (InsClas) {
        $scope.showLoader = true;
        SuccessDelete(InsClas);

    };
    $scope.refreshContent = function () {
        getAllInsClass();
        $scope.search_query = "";
    };

    $scope.ClearFields = function () {
        $scope.BusinessUnitID = null;
        $scope.Code = null;
        $scope.Description = null;
        $scope.isActive = null;
    };
    $scope.update = function (InsClas) {
        //alert(InsCompnay.InsuranceCompanyName);

        $("#view" + InsClas.InsuranceClassID).show();
        $("#edit" + InsClas.InsuranceClassID).hide();
        Update(InsClas);

    };
    $scope.cancel = function (InsClas) {
        //alert(id);
        $("#view" + InsClas.InsuranceClassID).show();
        $("#edit" + InsClas.InsuranceClassID).hide();
        $scope.refreshContent();
    };
    $scope.Save = function () {
        $scope.showLoader = true;
        Success();
        //TO DO
    }
    function saveIns() {
        var businessUnitID = $scope.BusinessUnitID;
        var insuranceCode = $scope.Code;
        var description = $scope.Description;
        var isActive = $scope.isActive;
        var UserID = $scope.currentUser.UserID;

        InsClassService.saveInsClass(businessUnitID,
            insuranceCode,
            description,
            isActive, UserID).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Saved Insurance Class Details',
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
                       text: 'Error Saving Insurance Class Details.' + " " + results.message,
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

    function ClearFields() {

        //TO DO
        $scope.BusinessUnitID = null;
        $scope.Code = null;
        $scope.Description = null;
        $scope.isActive = null;






    }

    function Update(InsClas) {
        $scope.showLoader = true;
        SuccessUpdate(InsClas);
        //TO DO
    }
    function UpdateINS(InsClas) {
        var businessUnitID = InsClas.BusinessUnitID;
        var insClassID = InsClas.InsuranceClassID;
        var insuranceCode = InsClas.InsuranceCode;
        var description = InsClas.Description;
        var isActive = InsClas.IsActive;
        var UserID = $scope.currentUser.UserID;


        InsClassService.UpdateInsClass(
           insClassID,
            businessUnitID,
            insuranceCode,
            description,
            isActive, UserID).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Updated Insurance Class Details',
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
                       text: 'Error Updating Insurance Class Details.' + " " + results.message,
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
    function Delete(InsClas) {

        //TO DO
        var insuranceClassID = InsClas.InsuranceClassID;


        InsClassService.deleteInsClass(insuranceClassID).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Deleted Insurance Class Details',
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
                       text: 'Error Deleting Insurance Class Details',
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
    function getAllcomonay() {
        //  alert("hh");
        InsClassService.getAllCompany().then(function (results) {

            $scope.Company = results.data;
            // alert(angular.toJson(results.data));

        });
    };
    function getAllInsClass() {
        $scope.showLoader = true;
        InsClassService.getAllInsClass($scope.currentUser.BusinessUnitID).then(function (results) {
            $scope.showLoader = false;
            $scope.InsClass = results.data;
            var data = $scope.InsClass;

            $scope.data = angular.copy($scope.InsClass);
            $scope.viewby = "5";
            $scope.totalItems = $scope.data.length;
            $scope.currentPage = 1;
            $scope.itemsPerPage = $scope.viewby;
            $scope.maxSize = 5; //Number of pager buttons to show
            $scope.setItemsPerPage($scope.viewby);
            // alert(angular.toJson(results.data));

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
        $scope.data = filterFilter($scope.InsClass, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };

    function notyConfirm(InsClas, InsClass, index) {
        var r = confirm("Are You Sure you want Delete this Record of " + InsClas.Description);
        if (r == true) {
            Delete(InsClas);
            InsClass.splice(index, 1);
        }
        // alert("dd");

    }
    function getAllBusinessUnits() {
        if ($scope.currentUser.AccessLevelTypeName == "Admin") {
            InsClassService.getAllBusinessUnits().then(function (results) {
                //$Scope.BussinessUnit = results;
                $scope.filteredBusinessUnits = results.data;

                //for (var i = 0; i < $scope.BUnits.length; i++) {
                //    if ($scope.BUnits[i].BusinessUnitID === $scope.currentUser.BusinessUnitID) {
                //        $scope.filteredBusinessUnits.push({ "BusinessUnitID": $scope.BUnits[i].BusinessUnitID, "BusinessUnit": $scope.BUnits[i].BusinessUnit });
                //        break;
                //    }
                //}
            });
        }
        else {
            $scope.filteredBusinessUnits.push({
                "BusinessUnitID": $scope.currentUser.BusinessUnitID,
                "BusinessUnit": $scope.currentUser.BusinessUnitName

            });
        }


    };

    function Success() {
        //alert("cc");
        noty({
            text: 'Do you want to Save Insurance Class Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            saveIns();
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

    function SuccessUpdate(InsClas) {
        //alert("cc");
        noty({
            text: 'Do you want to Update Insurance Class Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            UpdateINS(InsClas);

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

    function SuccessDelete(InsClas) {
        //alert("cc");
        noty({
            text: 'Are you sure want to Delete Insurance Class Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            Delete(InsClas);

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