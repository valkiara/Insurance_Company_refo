'use strict';

ibmsApp.controller("ClientListController", function ($scope, $http, $rootScope, Excel, $timeout, ClientListService, $location, AuthService, filterFilter) {

    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
            $scope.businessUnitID = results.BusinessUnitID;
            $scope.GetAllCountry();
            SearchClient();

        });
    };
   
    $scope.Client = [];
    $scope.BupaClient = [];

    $scope.init = function () {
        $scope.getCurrentUser();
        $scope.GetAllCountry();
        GetBupaPremiumClients();
    };

    $scope.exportToExcel = function (tableId) {
        var exportHref = Excel.tableToExcel(tableId, 'sheet name');
        $timeout(function () { location.href = exportHref; }, 100);
    };

    //$scope.refreshContent = function () {
    //    $scope.Client = "";
    //    $scope.filterdData = "";
    //    $scope.search_query = "";
    //    $scope.HomeCountryID = "";
    //    $scope.ResidentCountryID = "";
    //};

    $scope.GetAllCountry = function () {
        $scope.showLoader = true;
        ClientListService.GetAllCountries().then(function (results) {
            $scope.showLoader = false;

            if (results.status === true) {
                $scope.availableCountry = results.data;
            }
            else {
                $scope.availableCountry = [];
            }
        });
    };

    $scope.LoadHomeCountry = function (HomeCountryID) {

        SearchClient(HomeCountryID);
    };

    $scope.LoadResidentCountry = function (ResidentCountryID) {

        SearchClient(ResidentCountryID);
    };


    function SearchClient() {

        var homeCountryID = $scope.HomeCountryID;
        var residentCountryID = $scope.ResidentCountryID;

        $scope.showLoader = true;
        ClientListService.SearchClients($scope.businessUnitID, homeCountryID, residentCountryID).then(function (results) {
            $scope.showLoader = false;
            $scope.Client = results.data;
            //alert("agent"+angular.toJson(results));
            $scope.data = angular.copy($scope.Client);
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
        $scope.data = filterFilter($scope.Client, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };

    function GetBupaPremiumClients() {


        $scope.showLoader = true;
        ClientListService.GetBupaPremiumClients().then(function (results) {
            $scope.showLoader = false;
            $scope.BupaClient = results.data;
            ////alert("agent"+angular.toJson(results));
            //$scope.data = angular.copy($scope.BupaClient);
            //$scope.viewby = "5";
            //$scope.totalItems = results.data.length; //$scope.data.length;
            //$scope.currentPage = 1;
            //$scope.itemsPerPage = $scope.viewby;
            //$scope.maxSize = 5; //Number of pager buttons to show
            //$scope.setItemsPerPage($scope.viewby);
        });
    };
  
});

