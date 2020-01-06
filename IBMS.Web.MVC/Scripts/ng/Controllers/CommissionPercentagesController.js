'use strict';

ibmsApp.controller("CommissionPercentagesController", function ($scope, $http, $rootScope, Excel, $timeout, CommissionPercentagesService, $location, AuthService, filterFilter) {

    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
            $scope.businessUnitID = results.BusinessUnitID;
            $scope.GetAllComStructureHeadersByBU();
            GetAllComStructureLinesByComStructureHeader();
        });
    };

    $scope.ComStructLine = [];
    //$scope.getAllDesignation();

    $scope.init = function () {
        $scope.getCurrentUser();
        $scope.GetAllComStructureHeadersByBU();
    };

    //$scope.refreshContent = function () {
    //    GetAllComStructureLinesByComStructureHeader();
    //    $scope.search_query = "";
    //};

    $scope.exportToExcel = function (tableId) {
        var exportHref = Excel.tableToExcel(tableId, 'sheet name');
        $timeout(function () { location.href = exportHref; }, 100);
    };

    $scope.GetAllComStructureHeadersByBU = function () {
        $scope.showLoader = true;
        CommissionPercentagesService.GetAllComStructureHeadersByBUID($scope.businessUnitID).then(function (results) {
            $scope.showLoader = false;

            if (results.status === true) {
                $scope.availableCommissionHeader = results.data;
            }
            else {
                $scope.availableCommissionHeader = [];
            }
        });
    };

    $scope.LoadCommission = function (ComStructureHeaderID) {

        GetAllComStructureLinesByComStructureHeader(ComStructureHeaderID);
    };

    function GetAllComStructureLinesByComStructureHeader(ComStructureHeaderID) {

        $scope.showLoader = true;
        CommissionPercentagesService.GetAllComStructureLinesByComStructureHeaderID(ComStructureHeaderID).then(function (results) {
            $scope.showLoader = false;
            $scope.ComStructLine = results.data;
            //alert("agent"+angular.toJson(results));
            $scope.data = angular.copy($scope.ComStructLine);
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
        $scope.data = filterFilter($scope.ComStructLine, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };


});
    