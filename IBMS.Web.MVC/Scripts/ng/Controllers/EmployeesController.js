'use strict';

ibmsApp.controller("EmployeesController", function ($scope, $http, $rootScope, Excel, $timeout, EmployeesService, $location, AuthService, filterFilter) {

    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
            $scope.businessUnitID = results.BusinessUnitID;
            $scope.getAllDesignation();
            GetEmployeesByDesignation();
        });
    };


    $scope.Employee = [];
    //$scope.getAllDesignation();

    $scope.init = function () {
        $scope.getCurrentUser();
        $scope.getAllDesignation();
    };

    //$scope.refreshContent = function () {
    //    GetEmployeesByDesignation();
    //    $scope.search_query = "";
    //};

    $scope.exportToExcel = function (tableId) {
        var exportHref = Excel.tableToExcel(tableId, 'sheet name');
        $timeout(function () { location.href = exportHref; }, 100);
    };

    $scope.getAllDesignation = function () {
        $scope.showLoader = true;
        EmployeesService.getAvailableDesignation().then(function (results) {
            $scope.showLoader = false;

            if (results.status === true) {
                $scope.availableDesignation = results.data;
            }
            else {
                $scope.availableDesignation = [];
            }
        });
    };

    $scope.LoadDesignation = function (DesignationID) {

        GetEmployeesByDesignation(DesignationID);
    };



    function GetEmployeesByDesignation(DesignID) {

        $scope.showLoader = true;
        EmployeesService.GetEmployeesByDesignationID(DesignID).then(function (results) {
            $scope.showLoader = false;
            $scope.Employee = results.data;
            //alert("agent"+angular.toJson(results));
            $scope.data = angular.copy($scope.Employee);
            $scope.viewby = "5";
            $scope.totalItems = results.data.length; //$scope.data.length;
            $scope.currentPage = 1;
            $scope.itemsPerPage = $scope.viewby;
            $scope.maxSize = 5; //Number of pager buttons to show
            $scope.setItemsPerPage($scope.viewby);
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
    };
    
    $scope.searchTextChange = function (searchText) {
        $scope.data = filterFilter($scope.Employee, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };
});
