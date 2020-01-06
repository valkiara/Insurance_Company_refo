'use strict';

ibmsApp.factory('EmployeeService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var getAllBusinessUnits = function () {
        return $http.post($rootScope.serviceURL + 'api/BusinessUnit/GetAllBusinessUnits', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAllCompany = function () {
        return $http.post($rootScope.serviceURL + 'api/Company/GetAllCompanies', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            alert(data);
            // Handle error here
        })
    };

    var getAllDesignation = function () {
        return $http.post($rootScope.serviceURL + 'api/Designation/GetAllDesignations', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            alert(data);
            // Handle error here
        })
    };

    var getAllEmployee = function (BUID) {
        var params = { "BusinessUnitID": BUID };
        return $http.post($rootScope.serviceURL + 'api/Employee/GetAllEmployeesByBUID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            alert(data);
            // Handle error here
        })
    };

    var saveEmployee = function (
             businessUnitID,
           EmpName,
           address1,
           address2,
           address3,
           DesignationID,
           contactNo,
           email, UserID
        ) {

        var params = {
            "businessUnitID": businessUnitID,
            "EmpName": EmpName,
            "Address1": address1,
            "Address2": address2,
            "Address3": address3,
            "ContactNo": contactNo,
            "Email": email,
            "DesignationID": DesignationID,
            "UserID": UserID
        };

        return $http.post($rootScope.serviceURL + 'api/Employee/SaveEmployee',
           params, config).then(function (results) {
               return results.data;
           }, function (data) {
               //alert("Errror : "+data);
           });
    };

    var UpdateEmployee = function (
            EmpID, businessUnitID, companyID,
           EmpName, FirstName,
           LastName, address1,
           address2,
           address3, DesignationID,
           SettingID,
           contactNo,
           email,
           fax, UserID) {


        var params = {
            "EmpID": EmpID,
            "EmpName": EmpName,
            "Address1": address1,
            "Address2": address2,
            "Address3": address3,
            "ContactNo": contactNo,
            "Email": email,
            "DesignationID": DesignationID,
            "UserID": UserID
        };


        return $http.post($rootScope.serviceURL + 'api/Employee/UpdateEmployee',
           params, config).then(function (results) {

               return results.data;
           }, function (data) {
               alert("Errror : " + data);

           });
    };

    var DeleteEmployee = function (empID) {

        var params = { "EmpID": empID };

        return $http.post($rootScope.serviceURL + 'api/Employee/DeleteEmployee', params, config).then(function (results) {

            return results.data;
        }, function (data) {
            //alert("Errror : " + data);
        });
    };


    return {
        getAllCompany: getAllCompany,
        getAllBusinessUnits: getAllBusinessUnits,
        saveEmployee: saveEmployee,
        UpdateEmployee: UpdateEmployee,
        DeleteEmployee: DeleteEmployee,
        getAllEmployee: getAllEmployee,
        getAllDesignation: getAllDesignation

    };
});