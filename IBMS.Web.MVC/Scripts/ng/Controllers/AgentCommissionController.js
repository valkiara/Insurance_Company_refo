'use strict';

ibmsApp.controller("AgentCommissionController", function ($scope, $http, AgentCommissionService, IntegrationService, $location, AuthService, AgentService, Excel, $timeout) {

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
        $scope.data2 = [];
        $scope.data = [];
        $scope.showLoader = false;
        $scope.availableType = [];
        $scope.getTypes();
        $scope.filterObj = {};
       
    };

    $scope.getTypes = function () {
        $scope.availableType.push({ value: "1", text: "Agent Commission" });
        $scope.availableType.push({ value: "2", text: "Invoice Amount" });
    }

    $scope.ids = [];


    $scope.getAgentCommissionByDate = function () {
        try {
            $scope.showLoader = true;
            var fromDate = $scope.getFormattedDate($scope.filterObj.fromDate);
            var toDate = $scope.getFormattedDate($scope.filterObj.toDate);
            var type = $scope.filterObj.Type == "1" ? "AgentCommssion" : "BupaInvoice"; 
            AgentCommissionService.getAgentCommissionByDate(fromDate, toDate, type).then(function (results) {
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

    $scope.getBUPACommission = function () {
        try {
            $scope.showLoader = true;
            var fromDate = $scope.getFormattedDate($scope.filterObj.fromDate);
            var toDate = $scope.getFormattedDate($scope.filterObj.toDate);
            var type = $scope.filterObj.Type == "1" ? "AgentCommssion" : "BupaInvoice";
            AgentCommissionService.getBUPACommissionByDate(fromDate, toDate, type).then(function (results) {
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

    $scope.getAmount = function () {
        try {


            if ($scope.filterObj.Type == "1") {
                $scope.getAgentCommissionByDate();
            }
            else if ($scope.filterObj.Type == "2") {
                $scope.getBUPACommission();
            }
            else {
                return;
            }

        } catch (e) {
            console.log(e);
        }
    }
    $scope.getAgentCommission = function () {
        try {
            $scope.showLoader = true;
            AgentService.getAgentCommission().then(function (results) {
                if (results.status == 1) {
                    $scope.ids = results.data.Ids;
                    $scope.data = [];
                    //$scope.data = angular.copy(results.data);
                    $scope.data = angular.copy(results.data.AmountDetailMappers);
                    $scope.viewby = "10";
                    $scope.totalItems = $scope.data.length;
                    $scope.currentPage = 1;
                    $scope.itemsPerPage = $scope.viewby;
                    $scope.maxSize = 10;
                    $scope.setItemsPerPage($scope.viewby);
                    $scope.showLoader = false;
                }
                else {
                    $scope.showLoader = true;
                }
            });
        } catch (e) {
            $scope.showLoader = false;
            console.log(e);
        }
    }

    $scope.getBupaAmount = function () {
        try {
            $scope.showLoader = true;
            IntegrationService.getBupaAmount().then(function (results) {

                if (results.status == 1) {
                    $scope.ids = results.data.Ids;
                    $scope.data = [];
                    $scope.data = angular.copy(results.data.AmountDetailMappers);
                    $scope.viewby = "10";
                    $scope.totalItems = $scope.data.length;
                    $scope.currentPage = 1;
                    $scope.itemsPerPage = $scope.viewby;
                    $scope.maxSize = 10;
                    $scope.setItemsPerPage($scope.viewby);
                    $scope.showLoader = false;
                }
                else {
                    $scope.showLoader = false;
                }
            });
        } catch (e) {
            $scope.showLoader = false;
            console.log(e);
        }
    }

    $scope.SaveConfirmation = function() {
        noty({
            text: 'Do you want to Transfer Transaction Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            $scope.saveTransaction();
                        }
                    },
                    {
                        addClass: 'btn btn-danger btn-clean', text: 'Cancel', onClick: function ($noty) {
                            $noty.close();
                            $scope.$apply(function () {
                                $scope.showLoader = false;
                            });
                        }
                    }
            ]
        })
    };

    $scope.saveTransaction = function () {
        try {
            $scope.showLoader = true;
            var type = $scope.filterObj.Type == "1" ? "AgentCommssion" : "BupaInvoice";
            var payDate = $scope.getFormattedDate($scope.filterObj.PaymentDate);

            IntegrationService.saveTransaction(payDate, type, $scope.data, $scope.ids).then(function (results) {
                $scope.showLoader = false;
                if (results.status === true) {
                    noty({
                        text: 'Successfully Saved Agent Details',
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
                    $scope.clear();
                }
                else {
                    noty({
                        text: 'Error Saving Agent Details.' + " " + results.message,
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
        } catch (e) {
            $scope.showLoader = false;
            console.log(e);
        }
    }

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