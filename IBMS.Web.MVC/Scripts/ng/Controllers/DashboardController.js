'use strict';

ibmsApp.controller("DashboardController", function ($scope, $http, $rootScope, DashboardService, $location, AuthService, filterFilter) {




    $scope.init = function () {
        $scope.getCurrentUser();
        $scope.buObj = {};
        $scope.labelsBar = [];
        $scope.dataBar = [];
        $scope.labels = [];
        $scope.datas = [];
        $scope.ClientPaymentList = [];
        $scope.one = true;
        $scope.seriesLine = [];
        $scope.dataLine = [];
        $scope.dataMD = [];
        $scope.dataSubMD = [];
        $scope.BusinessCountList = [];
        $scope.clientLineList = [];
        $scope.getDeliveryCountDetails();
        $scope.getLineChart();
        $scope.getPie();
       // $scope.getPies();
        $scope.getClientDetails();
        $scope.getBar();
    };



    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
        });
    };


    $scope.refreshContent = function () {
        $scope.loadBusinessUnit();
        $scope.search_query = "";
    };
    $scope.ClearFields = function () {
        $scope.buObj = {};
    };

    $scope.companyChange = function (companyID) {
        for (var i = 0; i < $scope.availableCompany.length; i++) {
            if ($scope.availableCompany[i].CompanyID.toString() === companyID) {
                $scope.companyName = $scope.availableCompany[i].companyName;
                break;
            }
        }
    };



    //--------------NO OF QUOTATION---------------------------
    $scope.getDeliveryCountDetails = function () {
        DashboardService.CountQuotation().then(function (results) {

            $scope.deliveryCountDay = results.data.PendingQuotationCount;
            $scope.deliveryCountMonth = results.data.ApprovedQuotationCount;
            $scope.deliveryCountYear = results.data.ClientRequestCount;
            $scope.totalDeliveryCount = results.data.TCNIQuotationCount;
            //});
        });
    };

    $scope.getClientDetails = function () {
        DashboardService.ClientListCOD().then(function (results) {
            $scope.clientLineList = results.data;
            var data = $scope.clientLineList;
            $scope.data = angular.copy($scope.clientLineList);
            $scope.viewby = "5";
            $scope.totalItems = $scope.data.length;
            $scope.currentPage = 1;
            $scope.itemsPerPage = $scope.viewby;
            $scope.maxSize = 5; //Number of pager buttons to show

            $scope.setItemsPerPage($scope.viewby);
            //for (var i = 0; i < $scope.clientLineList.length; i++) {

            //    $scope.ClientName.push($scope.clientLineList[i].ClientName)
            //    $scope.NoOfQuitation.push($scope.clientLineList[i].NoOfQuitation)
            //    $scope.NoOfClientRequest.push($scope.clientLineList[i].NoOfClientRequest)
            //    $scope.PaymentAmount.push($scope.clientLineList[i].PaymentAmount)
            //}
            //});
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
        $scope.currentPage = 1;
    };
    $scope.searchTextChange = function (searchText) {
        $scope.data = filterFilter($scope.gridOptionsBusinessUnit, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };



    /*---------------------------Line CHART----------------------------*/
    $scope.labelsLine = ["January", "February", "March", "April", "May", "June", "July", "August", 'September', 'October', 'November', 'December'];


    $scope.getLineChart = function () {
        DashboardService.CountClientWithQuotation().then(function (results) {
            $scope.seriesLineList = results.data;


            for (var i = 0; i < $scope.seriesLineList.length; i++) {
                $scope.seriesLine.push($scope.seriesLineList[i].count);

                $scope.dataSubMD = [];

                $scope.dataSubMD.push($scope.seriesLineList[i].January);
                $scope.dataSubMD.push($scope.seriesLineList[i].Febrary);
                $scope.dataSubMD.push($scope.seriesLineList[i].March);
                $scope.dataSubMD.push($scope.seriesLineList[i].April);
                $scope.dataSubMD.push($scope.seriesLineList[i].May);
                $scope.dataSubMD.push($scope.seriesLineList[i].June);
                $scope.dataSubMD.push($scope.seriesLineList[i].July);
                $scope.dataSubMD.push($scope.seriesLineList[i].August);
                $scope.dataSubMD.push($scope.seriesLineList[i].September);
                $scope.dataSubMD.push($scope.seriesLineList[i].October);
                $scope.dataSubMD.push($scope.seriesLineList[i].November);
                $scope.dataSubMD.push($scope.seriesLineList[i].December);

                $scope.dataLine.push($scope.dataSubMD);
            }
            $scope.optionsLine = { legend: { display: true } };
        });




        //$scope.labelsLine = monthsMD;
        //$scope.seriesLine = ['Series A', 'Series B'];
        ////$scope.dataLine = [
        ////  [65, 59, 80, 81, 56, 55, 40, 28, 48, 40, 19, 86],
        ////    [85, 69, 40, 51, 56, 65, 70, 58, 38, 40, 10, 67]

        ////];

        //$scope.onClick = function (points, evt) {
        //    console.log(points, evt);
        //};
        //$scope.datasetOverride = [{ yAxisID: 'y-axis-1' }, { yAxisID: 'y-axis-2' }];
        //$scope.optionsLine = {
        //    scales: {
        //        yAxes: [
        //          {
        //              id: 'y-axis-1',
        //              type: 'linear',
        //              display: true,
        //              position: 'left',
        //              fill: true,
        //              color: '#979193'
        //          },
        //          {
        //              id: 'y-axis-2',
        //              type: 'linear',
        //              display: true,
        //              position: 'right',
        //              fill: true,
        //              color: '#2A516E'
        //          }
        //        ]
        //    }
        //};
        //$scope.colors = [{
        //    "fillColor": "#2A516E"

        //}];


    };


    /*---------------------------BAR CHART----------------------------*/


    $scope.getBar = function () {
        //$scope.labelsBar = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Frd', 'Sat'];
        //$scope.seriesBar = ['Series A'];

        //$scope.dataBar = [
        //  [65, 59, 80, 81, 56, 55, 40]
        //];

        //$scope.colorsBar = ['#16a085'];

        DashboardService.clientPaymentAmount().then(function (results) {
            $scope.ClientPaymentList = results.data;
            for (var i = 0; i < $scope.ClientPaymentList.length; i++) {
                $scope.labelsBar.push($scope.ClientPaymentList[i].ClientName)
                $scope.dataBar.push($scope.ClientPaymentList[i].PaymentAmount)
            }
        });

    };

    /*---------------------------DOUGHNUT CHART----------------------------*/

    //$scope.getPies = function () {
    //    $scope.labelslll = ["Download Sales", "In-Store Sales", "Mail-Order Sales"];
    //    $scope.dataslll = [300, 500, 100];
    //    $scope.colorspie111 = ['#3D449C', '#268FB2', '#74DE00']
    //};

    $scope.getPie = function () {
        DashboardService.CountBusinessUnit().then(function (results) {
            $scope.BusinessCountList = results.data;
            for (var i = 0; i < $scope.BusinessCountList.length; i++) {
                $scope.labels.push($scope.BusinessCountList[i].BusinessUnit)
                $scope.datas.push($scope.BusinessCountList[i].count)
            }
            //$scope.optionspie = { legend: { display: true } };
        });

    };


});
