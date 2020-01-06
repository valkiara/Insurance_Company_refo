'use strict';

ibmsApp.controller("PremiumControler", function ($scope, $http, PremiumService, $window, AuthService, filterFilter) {
    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;

            if ($scope.currentUser.AccessLevelTypeName === "Admin") {
                //To Do (using a popup to select the desired business unit)
            }
            else {
                $scope.businessUnitID = $scope.currentUser.BusinessUnitID;
                $scope.loadPremiumByBUID($scope.businessUnitID);
               
                // $scope.loadBanks($scope.businessUnitID);
            }
        });
    }


    $scope.init = function () {

        $scope.getCurrentUser();
        $scope.Premium = {};
        $scope.businessUnitID = "";
        $scope.availablePremium = [];

    };


    $scope.saveRequest = function () {
        $scope.showLoader = true;
        noty({
            text: 'Do you want to Save Customer Request Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                           // $scope.cusObj.ChildrenDetailss = [];
                            //   $scope.cusObj.FamilyDetails = [];


                            //  alert('com');

                            
                            $scope.Premium.BUID = $scope.businessUnitID;

                           // alert($scope.businessUnitID);
                         //   alert($scope.cusObj.Exclusions);
                            PremiumService.saveRequest($scope.Premium).then(function (results) {
                                $scope.showLoader = false;
                                //  alert(angular.toJson(results));
                                if (results.status === true) {
                                    noty({
                                        text: 'Successfully Saved Customer Request Details',
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
                                    $scope.ClearFields();
                                    $scope.refreshContent();
                                    $scope.activateClientRequestListTab();
                                }
                                else {
                                    noty({
                                        text: 'Error Saving Customer Request Details',
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







    $scope.refreshContent = function () {
        // getAllAgent();

        $scope.loadPremiumByBUID($scope.businessUnitID)
        $scope.search_query = "";
    };


    $scope.loadPremiumByBUID = function (businessUnitID) {
        $scope.showLoader = true;

        //alert("ok");
        PremiumService.getAllPremum(businessUnitID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.availablePremium = results.data;
              
              

                $scope.data = angular.copy($scope.availablePremium);
                $scope.viewby = "10";
                $scope.totalItems = $scope.data.length;
                $scope.currentPage = 1;
                $scope.itemsPerPage = $scope.viewby;
                $scope.maxSize = 10; //Number of pager buttons to show

                $scope.setItemsPerPage($scope.viewby);
            }
            else {
                $scope.availablePremium = [];
            }
        });
    };

    $scope.activateClientRequestListTab = function () {
        $("#list-tab-1").addClass('active');
        $("#tab-1").addClass('tab-pane active');

        $("#list-tab-2").removeClass('active');
        $("#tab-2").removeClass('tab-pane active');

        $("#tab-1").css("display", "block");
        $("#tab-2").css("display", "none");
    };

    $scope.activateNewClientRequestTab = function () {
        $("#list-tab-1").removeClass('active');
        $("#tab-1").removeClass('tab-pane active');

        $("#list-tab-2").addClass('active');
        $("#tab-2").addClass('tab-pane active');

        $("#tab-1").css("display", "none");
        $("#tab-2").css("display", "block");
    };

    $scope.editRequest = function (clientID) {
        $scope.activateNewClientRequestTab();
      //  alert("Ok");
        $scope.showLoader = true;
        PremiumService.getClientRequestByID(clientID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                
                $scope.Premium = results.data;
               // alert(results.data[0].PremiumCode);
                
            }
            else {
                $scope.Premium = {};
            }
        });
    };




    



    
    $scope.ClearFields = function () {
        $scope.Premium = {};
      
       
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
        $scope.data = filterFilter($scope.availablePremium, searchText);
        $scope.viewby = "5";
        $scope.totalItems = $scope.data.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5;
        $scope.setItemsPerPage($scope.viewby);
    };
});
ibmsApp.filter('return_status', function ($sce) {
    return function (text, length, end) {
        if (text) {
            return $sce.trustAsHtml('<span><i style="color:green" class="glyphicon glyphicon-ok"></i></span>');
        }
        return $sce.trustAsHtml('<span><i style="color:red" class="glyphicon glyphicon-remove"></i></span>');
    }
});