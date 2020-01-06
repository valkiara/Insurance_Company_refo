'use strict';

ibmsApp.controller("INSSubClassesController", function ($scope, $http, $rootScope, Excel, $timeout, INSSubClassesService, $location, AuthService,filterFilter) {

    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
        });
    };

    $scope.InsSubClass = [];

    $scope.init = function () {
        $scope.getCurrentUser();
        $scope.getAllInsuranceClass();
    };
 

    $scope.exportToExcel = function (tableId) {
        var exportHref = Excel.tableToExcel(tableId, 'sheet name');
        $timeout(function () { location.href = exportHref; }, 100);
    };

    //$scope.refreshContent = function () {
    //    GetAllInsSubClassesByInsClasses();
    //    $scope.search_query = "";
    //    $scope.InsSubClass = "";
    //    $scope.availableInsuranceClass="";
    //};

    $scope.getAllInsuranceClass = function () {
        $scope.showLoader = true;
        INSSubClassesService.getAllInsClass().then(function (results) {
            $scope.showLoader = false;
            $scope.isInsuranceClassLoaded = true;

            if (results.status === true) {
                $scope.availableInsuranceClass = results.data;
            }
            else {
                $scope.availableInsuranceClass = [];
            }
        });
    };

    $scope.LoadInsSubClass = function (InsuranceClassID)
    {

        GetAllInsSubClassesByInsClasses(InsuranceClassID);
    };

    function GetAllInsSubClassesByInsClasses(InsClassID) {

        $scope.showLoader = true;
        INSSubClassesService.GetAllInsSubClassesByInsClass(InsClassID).then(function (results) {
            $scope.showLoader = false;
            //alert("agent" + angular.toJson(results));
            if (results.status === true) {
                $scope.InsSubClass = results.data;
                //alert("agent"+angular.toJson(results));
                $scope.data = angular.copy($scope.InsSubClass);
                $scope.viewby = "5";
                $scope.totalItems = results.data.length; //$scope.data.length;
                $scope.currentPage = 1;
                $scope.itemsPerPage = $scope.viewby;
                $scope.maxSize = 5; //Number of pager buttons to show
                $scope.setItemsPerPage($scope.viewby);
            }
            else {
                $scope.InsSubClass = [];
            }

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
        $scope.data = filterFilter($scope.InsSubClass, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };
});

