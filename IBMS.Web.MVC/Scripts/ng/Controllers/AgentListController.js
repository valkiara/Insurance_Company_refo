'use strict';

ibmsApp.controller("AgentListController", function ($scope, $http, $rootScope, Excel, $timeout, AgentListService, $location, AuthService, filterFilter) {
    $scope.Agent = [];

    $scope.init = function () {
        getCurrentUser();
    };

    $scope.exportToExcel = function (tableId) { 
        var exportHref = Excel.tableToExcel(tableId, 'sheet name');
        $timeout(function () { location.href = exportHref; }, 100); 
    }

    function getCurrentUser() {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
            $scope.companyID = results.CompanyID;
            //alert("login"+angular.toJson(results));
           GetAgentsByCompany();

        });
    };

   //$scope.refreshContent = function () {
   //    GetAgentsByCompany();
   //    $scope.search_query = "";
   // };


    

    function GetAgentsByCompany() {

       // var companyID = $scope.companyID;
       // alert("com"+$scope.companyID);
        $scope.showLoader = true;
        AgentListService.GetAgentsByCompanyID($scope.companyID).then(function (results) {
            $scope.showLoader = false;
            $scope.Agent = results.data;
            //alert("agent"+angular.toJson(results));
            $scope.data = angular.copy($scope.Agent);
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
        $scope.data = filterFilter($scope.Agent, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };
    
});


   
