'use strict';

ibmsApp.controller("EmployeeController", function ($scope, $http, EmployeeService, $window, AuthService,filterFilter) {

    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
            $scope.businessUnitID = results.BusinessUnitID;
            $scope.BusinessUnit = results.BusinessUnitID;
            getAllBusinessUnits();
            getAllEmployee();
        });
    };
    //$scope.message = "nnj";

    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': 'Basic VGVzdDoxMjM=' }

    };
    $scope.Address1 = null;
    $scope.Address2 = null;
    $scope.Address3 = null;
    $scope.designationID = null;
    $scope.contactNo = null;
    $scope.email = null;
    $scope.NIC = null;


    //getAllBusinessUnits();
    getAllcomonay();
    //getAllSetting();
    getAllDesignation();
   // getAllEmployee();
    $scope.getCurrentUser();

    $scope.BUnits = [];
    $scope.Company = [];
    $scope.Employees = [];
    $scope.Settings = [];
    $scope.Designations = [];


    
    $scope.init = function () {
        //TO DO
        $scope.BusinessUnit = $scope.currentUser.BusinessUnitID;
        $scope.filteredBusinessUnits = [];
        $("#edit" + id).show();
    };
    $scope.refreshContent = function () {
        getAllEmployee();
        $scope.search_query = "";
    };

    $scope.ClearFields = function () {
        $scope.empNAme = null;
        $scope.Address1 = null;
        $scope.Address2 = null;
        $scope.Address3 = null;
        $scope.designationID = null;
        $scope.contactNo = null;
        $scope.email = null;
        $scope.NIC = null;

    };
    $scope.edit = function (Employee) {
   
       // alert(InsCompnay.InsuranceCompanyID);
        $("#view" + Employee.EmployeeID).hide();
        $("#edit" + Employee.EmployeeID).show();

    };
    $scope.Delete = function (Employee, Employees, index) {
        
        $scope.showLoader = true;
        SuccessDelete(Employee);

    };
    $scope.update = function (Employee) {
        //alert(InsCompnay.InsuranceCompanyName);
        $scope.showLoader = true;
        $("#view" + Employee.EmployeeID).show();
        $("#edit" + Employee.EmployeeID).hide();
        Update(Employee);

    };
    $scope.cancel = function (Employee) {
        //alert(id);
        $("#view" + Employee.EmployeeID).show();
        $("#edit" + Employee.EmployeeID).hide();
        $scope.refreshContent();
    };
    $scope.Save = function ()
    {
        $scope.showLoader = true;
        Success();
    }
       // alert("m");

        function saveEmp(){
      //  var EmpID = Employee.EmpID;
        var businessUnitID = $scope.BusinessUnit;//Employee.BUID;
        var companyID = 1;// Employee.companyID;
        var EmpName = $scope.empNAme;
        var FirstName = "";// Employee.FirstName;
        var LastName = "";//Employee.LastName;
        var address1 =$scope.Address1;
        var address2 = $scope.Address2;
        var address3 = $scope.Address3;
        var DesignationID = $scope.designationID;
        var SettingID =0 ;//Employee.SettingID;
        var contactNo = $scope.contactNo;
        var email = $scope.email;
        var fax = "";//Employee.Fax;
        var UserID = $scope.currentUser.UserID;
        var NIC = $scope.inputNIC;


        EmployeeService.saveEmployee(
            businessUnitID,
           // companyID,
           EmpName,
          // FirstName,
           //LastName,
           address1,
           address2,
           address3,
           DesignationID,
           //SettingID,
           contactNo,
           email, UserID,
           NIC
           //fax
           ).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Saved Employee Details',
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
               else {
                   noty({
                       text: 'Error Saving Employee Details' +" "+"("+ results.message + ")",
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
        getAllEmployee();

    };


    function Update(Employee) {
        $scope.showLoader = true;
        SuccessUpdate(Employee);
    }
    function UpdateEmp(Employee) {
        //TO DO
        var EmpID = Employee.EmployeeID;
        //  var EmpID = Employee.EmpID;
        var businessUnitID = Employee.BUID;//Employee.BUID;
        var companyID = 1;// Employee.companyID;
        var EmpName = Employee.EmployeeName;
        var FirstName = "";// Employee.FirstName;
        var LastName = "";//Employee.LastName;
        var address1 = Employee.Address1;
        var address2 = Employee.Address2;
        var address3 = Employee.Address3;
        var DesignationID = Employee.DesignationID;
        var SettingID = 0;//Employee.SettingID;
        var contactNo = Employee.ContactNo;
        var email = Employee.Email;
        var fax = "";//Employee.Fax;
        var UserID = $scope.currentUser.UserID;
      //// alert(EmpID);
        //alert(EmpName);
       // alert(address1);
       // alert(address3);
        ///alert(contactNo);
       // alert(email);
        //alert(DesignationID);



        EmployeeService.UpdateEmployee(
           EmpID, businessUnitID, companyID,
           EmpName, FirstName,
           LastName, address1,
           address2,
           address3, DesignationID,
           SettingID,
           contactNo,
           email,
           fax, UserID
           ).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Updated Employee Details',
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

               else {
                   noty({
                       text: 'Error Update Employee Details.' + " " + results.message,
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

                   //noty({ text: 'Error Update Employee Details', layout: 'topCenter', type: 'error' });
               }


           });

    };
    function Delete(Employee) {
        $scope.showLoader = true;
        SuccessDelete(Employee);
    }

    function DeleteEmp(Employee) {
        //TO DO
        var empID = Employee.EmployeeID;


        EmployeeService.DeleteEmployee(empID).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Deleted Employee Details',
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
                       text: 'Error Deleting Employee Details',
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
            EmployeeService.getAllBusinessUnits().then(function (results) {
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
            $scope.filteredBusinessUnits = [];
            $scope.filteredBusinessUnits.push({
                "BusinessUnitID": $scope.currentUser.BusinessUnitID,
                "BusinessUnit": $scope.currentUser.BusinessUnitName

            });
        }
        
        //EmployeeService.getAllBusinessUnits().then(function (results) {
        //    //$Scope.BussinessUnit = results;
        //    $scope.BUnits = results.data;
        //    for (var i = 0; i < $scope.BUnits.length; i++) {
        //        if ($scope.BUnits[i].BusinessUnitID === $scope.currentUser.BusinessUnitID) {
        //            $scope.filteredBusinessUnits.push({ "BusinessUnitID": $scope.BUnits[i].BusinessUnitID, "BusinessUnit": $scope.BUnits[i].BusinessUnit });
        //            break;
        //        }
        //    }

        //});
    };
    function getAllcomonay() {
         //alert("hh");
        EmployeeService.getAllCompany().then(function (results) {

            $scope.Company = results.data;
           // alert(angular.toJson(results.data));

        });
    };
    function getAllSetting() {
        //  alert("hh");
        EmployeeService.getAllSetting().then(function (results) {

            $scope.Settings = results.data;
            // alert(angular.toJson(results.data));

        });
    };
    function getAllDesignation()
    {
        //  alert("hh");
        EmployeeService.getAllDesignation().then(function (results)  {

            $scope.Designations = results.data;
           // alert(angular.toJson(results.data));

        });
    };
    function getAllEmployee() {
      //alert("hh");
      $scope.showLoader = true;
      EmployeeService.getAllEmployee($scope.currentUser.BusinessUnitID).then(function (results) {
            $scope.showLoader = false;
            $scope.Employees = results.data;
            var data = $scope.Employees;

            $scope.data = angular.copy($scope.Employees);
            $scope.viewby = "5";
            $scope.totalItems = $scope.data.length;
            $scope.currentPage = 1;
            $scope.itemsPerPage = $scope.viewby;
            $scope.maxSize = 5; //Number of pager buttons to show
            $scope.setItemsPerPage($scope.viewby);
            angular.forEach(results.data, function (value, key) {
         
                
                var designationRow = $scope.Designations.filter(function (item)
                {
                   return item.DesignationID == value.DesignationID;
                });
                value.DesignationName = designationRow[0].DesignationName;

                var bussinessUnitRow = $scope.BUnits.filter(function (item) {
    
                    return item.BUID == designationRow[0].businessUnitID;
                });
                value.BussinessUnitName = bussinessUnitRow[0].BusinessUnit;
                

            });





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
        $scope.data = filterFilter($scope.Employees, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };

    function Success() {
        //alert("cc");
        noty({
            text: 'Do you want to Save Employee Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            saveEmp();
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

    function SuccessUpdate(Employee) {
        //alert("cc");
        noty({
            text: 'Do you want to Update Employee Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            UpdateEmp(Employee);

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

    function SuccessDelete(Employee) {
        //alert("cc");
        noty({
            text: 'Are you sure want to Delete Employee Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            DeleteEmp(Employee);

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

  //  function notyConfirm(Employee, Employees, index) {
  //      var r = confirm("Are You Sure you want Delete this Record of " + Employee.EmployeeName);
  //      if (r == true)
  //      {
  //          Delete(Employee);
  //          Employees.splice(index, 1);
  //      }
  //   // alert("dd");

  //}






});