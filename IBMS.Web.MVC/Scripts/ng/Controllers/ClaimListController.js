'use strict';

ibmsApp.controller("ClaimListController", function ($scope, $http, ClaimListService, $location, AuthService, Excel, $timeout) {

    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
            $scope.getUserProfileDetails();
        });
    };
    var config = {
        headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': 'Basic VGVzdDoxMjM=' }
    };
    $scope.init = function () {

        $scope.data = [];
        $scope.showLoader = false;
        $scope.availableType = [];
      
        $scope.filterObj = {};
        $scope.inDays = 0;
    };

  

    $scope.calculateDays = function () {
        try {
            if ($scope.filterObj.toDate === undefined || $scope.filterObj.fromDate === undefined) return;

            $scope.inDays = ($scope.filterObj.toDate - $scope.filterObj.fromDate) / 1000 / 60 / 60 / 24;

        } catch (e) {
            console.log(e);
        }
    }

    $scope.ids = [];


    $scope.getClaimListByDate = function () {
        try {
            $scope.showLoader = true;
            var fromDate = $scope.getFormattedDate($scope.filterObj.fromDate);
            var toDate = $scope.getFormattedDate($scope.filterObj.toDate);
            var type = $scope.filterObj.Type == "1" ? "AgentCommssion" : "BupaInvoice";
            ClaimListService.getClaimListByDate(fromDate, toDate, type).then(function (results) {
                $scope.showLoader = false;
                if (results.status === true) {
                    $scope.data = angular.copy(results.data);
                    $scope.viewby = "10";
                    $scope.totalItems = $scope.data.length;
                    $scope.currentPage = 1;
                    $scope.itemsPerPage = $scope.viewby;
                    $scope.maxSize = 10;
                    $scope.setItemsPerPage($scope.viewby);
                    $scope.showLoader = false;
                }
            });

        } catch (e) {
            console.log(e);
        }
    }

    $scope.exportToExcel = function (tableId) {
        var exportHref = Excel.tableToExcel(tableId, 'sheet name');
        $timeout(function () { location.href = exportHref; }, 100);
    }

    //################################################################################################


    $scope.clear = function () {
        try {
            $scope.data = [];
        } catch (e) {
            console.log(e);
        }
    }

    $scope.getFormattedDate = function (date) {
        if (/^\d{2}\/\d{2}\/\d{4}$/.test(date)) {
            return date;
        }
        else {
            var stringDate = date.getDate() + "";
            var stringMonth = date.getMonth() + 1 + "";
            var stringYear = date.getFullYear() + "";

            if (stringDate.length < 2)
                stringDate = '0' + stringDate;
            if (stringMonth.length < 2)
                stringMonth = '0' + stringMonth;

            return [stringMonth, stringDate, stringYear].join('/');
        }
    };

    $scope.setPage = function (pageNo) {
        $scope.currentPage = pageNo;
    };

    $scope.pageChanged = function () {
        console.log('Page changed to: ' + $scope.currentPage);
    };

    $scope.setItemsPerPage = function (num) {
        $scope.itemsPerPage = num;
        $scope.currentPage = 1;
    };

    $scope.searchTextChange = function (searchText) {
        $scope.data = filterFilter($scope.availableClientRequests, searchText);
        $scope.viewby = "10";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 10;
        $scope.setItemsPerPage($scope.viewby);
    };
});