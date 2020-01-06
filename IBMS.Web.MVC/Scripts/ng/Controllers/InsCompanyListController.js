'use strict';

ibmsApp.controller("InsCompanyListController", function ($scope, $http, $rootScope, Excel, $timeout, InsCompanyListService, $location, AuthService, filterFilter) {

    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
            $scope.businessUnitID = results.BusinessUnitID;
            GetInsuranceCompaniesByBusinessUnit();
        });
    };
    
    $scope.InsComp = [];

    $scope.init = function () {
        $scope.getCurrentUser();
    };

    $scope.exportToExcel = function (tableId) {
        var exportHref = Excel.tableToExcel(tableId, 'sheet name');
        $timeout(function () { location.href = exportHref; }, 100);
    };

    //$scope.refreshContent = function () {
    //    GetInsuranceCompaniesByBusinessUnit();
    //    $scope.search_query = "";
    //};


    function GetInsuranceCompaniesByBusinessUnit() {

        // var companyID = $scope.companyID;
        // alert("com"+$scope.companyID);
        $scope.showLoader = true;
        InsCompanyListService.GetInsuranceCompaniesByBusinessUnitID($scope.businessUnitID).then(function (results) {
            $scope.showLoader = false;
            $scope.InsComp = results.data;
            //alert("agent"+angular.toJson(results));
            $scope.data = angular.copy($scope.InsComp);
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
        $scope.data = filterFilter($scope.InsComp, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };
 

});

