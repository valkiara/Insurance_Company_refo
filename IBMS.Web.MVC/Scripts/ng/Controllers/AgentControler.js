'use strict';

ibmsApp.controller("AgentController", function ($scope, $http, AgentService, $window, AuthService, filterFilter) {

    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
            $scope.companyID = results.CompanyID;
        });
    };

    $scope.refreshContent = function () {
        getAllAgent();
        $scope.search_query = "";
    };

    $scope.Address1 = null;
    $scope.Address2 = null;
    $scope.Address3 = null;
    $scope.message = "";
    $scope.paginationTopNumberList = [];

    var Agent = null;

    getAllAgent();
    $scope.getCurrentUser();

    $scope.Agents = [];
    $scope.init = function () {
        $("#edit" + id).show();
        $scope.getCurrentUser();
    };

    $scope.edit = function (Agent) {
        $("#view" + Agent.AgentID).hide();
        $("#edit" + Agent.AgentID).show();

    };

    $scope.Delete = function (Agent, Agents, index) {
        $scope.showLoader = true;
        SuccessDelete(Agent)

    };

    $scope.update = function (Agent) {
        $scope.showLoader = true;
        $("#view" + Agent.AgentID).show();
        $("#edit" + Agent.AgentID).hide();
        SuccessUpdate(Agent);
    };

    $scope.cancel = function (Agent) {
        $("#view" + Agent.AgentID).show();
        $("#edit" + Agent.AgentID).hide();
        $scope.refreshContent();
    };

    $scope.Save = function () {
        $scope.showLoader = true;
        Success();
    };

    $scope.checkInputRate = function (Agent) {
        if (Agent.RateValue > 100) {
            Agent.RateValue = Agent.RateValue / 10;
        }
    };

    $scope.checkInputRateByValue = function () {
        if ($scope.RateValue > 100) {
            $scope.RateValue = $scope.RateValue / 10;
        }
    };

    function save() {
        var companyID = $scope.companyID;
        var agentName = $scope.AgentName;
        var address1 = $scope.Address1;
        var address2 = $scope.Address2;
        var address3 = $scope.Address3;
        var rateValue = $scope.RateValue;
        var agentType = $scope.AgentType;
        var agentNIC = $scope.AgentNIC;
        var agentBR = $scope.AgentBR;
        var agentCode = $scope.AgentCode;
        

        if ($scope.RateValue === undefined || $scope.RateValue === "" || $scope.RateValue === null) {
            rateValue = 0;
        }

        if ($scope.AgentType === undefined || $scope.AgentType === "" || $scope.AgentType === null) {
            agentType ="";
        }

        if ($scope.AgentBR === undefined || $scope.AgentBR === "" || $scope.AgentBR === null) {
            agentBR = "";
        }

        AgentService.saveAgent(companyID, agentName, address1, address2, address3, rateValue,agentType,agentNIC,agentBR, $scope.currentUser.UserID,agentCode).
               then(function (results) {
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
                       //setTimeout(function () { window.location.href = "/Agent/Index" }, 2500)
                       $scope.ClearFields();
                       $scope.refreshContent();
                   }
                   else {
                       noty({
                           text: 'Error Saving Agent Details.' +" "+ results.message,
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
    };
    $scope.ClearFields = function () {
        $scope.AgentName = null;
        $scope.Address1 = null;
        $scope.Address2 = null;
        $scope.Address3 = null;
        $scope.RateValue = null;
        $scope.AgentType = null;
        $scope.AgentNIC = null;
        $scope.AgentBR = null;
        $scope.AgentCode = null;
       
    };
    

    function Success() {
        noty({
            text: 'Do you want to Save Agent Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            save();
                            //noty({ text: 'You clicked "Ok" button', layout: 'topRight', type: 'success' });
                        }
                    },
                    {
                        addClass: 'btn btn-danger btn-clean', text: 'Cancel', onClick: function ($noty) {
                            $noty.close();
                            $scope.$apply(function () {
                                $scope.showLoader = false;
                            });
                            //noty({ text: 'You clicked "Cancel" button', layout: 'topRight', type: 'error' });
                        }
                    }
            ]
        })
    };

    function SuccessUpdate(Agent) {
        noty({
            text: 'Do you want to Update Agent Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            Update(Agent);
                        }
                    },
                    {
                        addClass: 'btn btn-danger btn-clean', text: 'Cancel', onClick: function ($noty) {
                            $noty.close();
                            $scope.$apply(function () {
                                $scope.showLoader = false;
                            });
                            $scope.refreshContent();
                        }
                    }
            ]
        })
    };

    function SuccessDelete(Agent) {
        noty({
            text: 'Do you want to Delete Agent Details?',
            layout: 'topCenter',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            Deletes(Agent);
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

    function Update(Agent) {
        
        //var agentID = Agent.AgentID;
        //var companyID = $scope.companyID;
        //var agentName = $scope.AgentName;
        //var address1 = $scope.Address1;
        //var address2 = $scope.Address2;
        //var address3 = $scope.Address3;
        //var rateValue = $scope.RateValue;
        //var agentType = $scope.AgentType;
        //var agentNIC = $scope.AgentNIC;
        //var agentBR = $scope.AgentBR;
        //var agentCode = $scope.AgentCode;


        var agentID = Agent.AgentID;
        var companyID = $scope.companyID;
        var agentName = Agent.AgentName;
        var address1 = Agent.Address1;
        var address2 = Agent.Address2;
        var address3 = Agent.Address3;
        var rateValue = Agent.RateValue;
        var agentType = Agent.AgentType;
        var agentNIC = Agent.AgentNIC;
        var agentBR = Agent.AgentBR;
        var agentCode = Agent.AgentCode;

        //if ($scope.RateValue ===  || $scope.RateValue === "" || $scope.RateValue === null) {
        //    rateValue = 0;
        //}

        //if (agentNIC === null || agentNIC === "" || agentNIC === undefined) {
        //    agentNIC = "";
        //}

        //if (agentBR === null || agentBR === "" || agentBR === undefined) {
        //    agentBR = "";
        //}

        //if (agentNIC !== "" || agentNIC !== null && agentBR === null) {
        //    agentType = "Individual";
        //    agentBR = "";
        //}
        //else if (agentBR !== null && agentBR !== ""){
        //    agentType = "Organizational";
        //    agentNIC = "";
        //}

        //if (agentBR === undefined || agentBR === "" || agentBR === null) {
        //    agentBR = "";
        //}

        AgentService.updateAgent(
           agentID, companyID, agentName,
           address1, address2,
            address3, rateValue, $scope.currentUser.UserID, agentType, agentNIC, agentBR, agentCode).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Updated Agent Details',
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

                   //setTimeout(function () { window.location.href = "/Agent/Index" }, 2500)

                   $scope.ClearFields();
                   $scope.refreshContent();
               }
               else {
                   noty({
                       text: 'Error Update Agent Details.' + " " + results.message,
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
                   $scope.ClearFields();
                   $scope.refreshContent();
               }
           });

    };

    function Delete(Agent) {
        $scope.showLoader = true;
        SuccessDelete(Agent)
    };

    function Deletes(Agent) {
        var agentID = Agent.AgentID;

        AgentService.deleteAgent(agentID).
           then(function (results) {
               $scope.showLoader = false;
               if (results.status === true) {
                   noty({
                       text: 'Successfully Deleted Agent Details',
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
                   //setTimeout(function () { window.location.href = "/Agent/Index" }, 2500)
                   $scope.refreshContent();
               }
               else {
                   noty({
                       text: 'Error Deleteing Agent Details',
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

    };

    function getAllAgent() {
        $scope.paginationTopNumberList = [];
        $scope.showLoader = true;
        AgentService.getAllAgent().then(function (results) {
            $scope.showLoader = false;
            $scope.Agents = results.data;
            $scope.data = angular.copy($scope.Agents);
            $scope.viewby = "5";
            $scope.totalItems = $scope.data.length;
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
        $scope.data = filterFilter($scope.Agents, searchText);
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