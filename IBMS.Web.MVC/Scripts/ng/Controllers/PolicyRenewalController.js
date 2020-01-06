'use strict';


testAPIApp.controller("PolicyRenewalController", function ($scope, $http, PolicyRenewalService, AuthService, $window, $filter, $log) {



    initialize();


    $scope.Edit = function (policyInfor) {
        $scope.policyInfor = policyInfor;
        getQuatationHeader();
        getPolicyRenewal();
        //alert(angular.toJson($scope.policyInfor));
       //$scope.policyCommissionPayment = $scope.policyInfor.PolicyCommissionPaymentDetails;
        LoadCommition();
        $("#tab-first").removeClass('tab-pane active');
        $("#tab-first").addClass('tab-pane');
        $("#tab-second").addClass('tab-pane active');


        $("#tabView").removeClass('active');

        $("#tabEdit").addClass('active');


    }

    $scope.RenewPolicy = function () {
        
        // $scope.policyInfor.PolicyCommissionPaymentDetails = $scope.policyCommissionPayment;
        $scope.policyRenewalHistory.PolicyInfoRecID = $scope.policyInfor.PolicyInfoRecID;
        //$scope.policyRenewalHistory.IsCancel = $scope.IsCancel;
        //$scope.policyRenewalHistory.IsSent = $scope.IsSent;
        //alert($scope.policyRenewalHistory);
        //alert($scope.IsSent);
        PolicyRenewalService.savePolicyRenewal( $scope.policyRenewalHistory, 1).then(function (results) {
            //alert(angular.toJson(results));
            notyFy(results.message);
            initialize();
        });



    }
    $scope.AddCommision = function () {

        $scope.found = $filter('filter')($scope.policyCommissionPayment, { "CommisionStructureID": $scope.comHeader, "partner": $scope.partner }, true);

        if ($scope.found.length == 0) {
            $scope.ChangeCommition($scope.partner);


        }
        else {
            noty({ text: 'commition Structure already Added For This Partner', layout: 'center', type: 'error' });
        }


    }
    function LoadCommition () {
        $scope.policyCommissionPayment = [];
       for(var i=0;i<$scope.policyInfor.PolicyCommissionPaymentDetails.length;i++)
       {
           var policyCommissionPaymentDetails = $scope.policyInfor.PolicyCommissionPaymentDetails;
           $scope.policyCommissionPayment.push
        ({
            "partner": "Agent",
            "CommisionTypeID": 2,
            "CommisionStructureID": 1,
            "CommisionValue": policyCommissionPaymentDetails[i].CommisionValue,
            "CommisionRate": 10,
            "CommitionName": policyCommissionPaymentDetails[i].CommissionTypeName
        });
       
       
       
       }





        

       // alert(angular.toJson($scope.policyCommissionPayment));
    

    }
    function getAllPolicyInfo() {
        PolicyRenewalService.getAllPolicyInfoRecording().then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.policyInforList = results.data;

                //alert(angular.toJson(results.data));
            }
            else {
                $scope.Company = [];
            }
        });
    };
    function getQuatationHeader() {
        PolicyRenewalService.getQuatationHeader($scope.policyInfor.QuotationHeaderID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.QutationHeader = results.data;
                ReLoadRequestHeader();
                LoadQuotationDetailsInsCompanyLineDetails($scope.policyInfor.QuotationDetailsInsCompanyLineID);

                // alert(angular.toJson(results.data));
            }
            else {
                $scope.QutationHeader = {};
            }
        });
    };
    function ReLoadRequestHeader() {

        //  $scope.clientObj = $filter('filter')($scope.Clients, { "ClientID": $scope.ClientID }, false)[0];
        PolicyRenewalService.getClientRequest($scope.QutationHeader.ClientRequestHeaderID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.ClentRequestHeader = results.data;
                ReLoadClientByID();
                //alert($scope.ClentRequestHeader.ClientID);
                //alert(angular.toJson(results.data));
            }
            else {
                $scope.ClentRequestHeader = [];
            }
        });



    }
    function ReLoadClientByID() {

        //  $scope.clientObj = $filter('filter')($scope.Clients, { "ClientID": $scope.ClientID }, false)[0];
        PolicyRenewalService.getClientByID($scope.ClentRequestHeader.ClientID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.clientObj = results.data;

                //alert(angular.toJson(results.data));
            }
            else {
                $scope.clientObj = {};
            }
        });



        //alert("clientObj" + angular.toJson($scope.clientObj));
        $scope.customerID = $scope.clientObj.ClientID;
        $scope.cusName = $scope.clientObj.ClientName;
        $scope.Address1 = $scope.clientObj.ClientAddress;
        $scope.Address2 = $scope.clientObj.ClientAddress;
        $scope.Address3 = $scope.clientObj.ClientAddress;
        $scope.DOB = $scope.clientObj.DOB;
        $scope.NIC = $scope.clientObj.NIC;
        $scope.mobileNo = $scope.clientObj.ContactNo;
        $scope.fixedLine = $scope.clientObj.FixedLine;
        $scope.email = $scope.clientObj.Email;
        $scope.homeCountry = $scope.clientObj.HomeCountryID;
        $scope.residentCountry = $scope.clientObj.ResidentCountryID;
        // $("#dp-2").val()=;//$scope.inspectiondays;
        $scope.fDiscount = $scope.clientObj.FamilyDiscount;

        //alert($scope.cusName);
        // alert($scope.Address1);





    }

    function LoadQuotationDetailsInsCompanyLineDetails(QuotationDetailsInsCompanyLineID) {
        var found = [];
        var quatationLine = $scope.QutationHeader.QuotationLineDetails;
        //alert(QuotationDetailsInsCompanyLineID);
        for (var i = 0; i < quatationLine.length; i++) {
            var inslassID = quatationLine[i].InsuranceClassID;
            var insSubClassID = quatationLine[i].InsuranceSubClassID;
            var InsClass = quatationLine[i].InsuranceCode;
            var InsSubClass = quatationLine[i].InsuranceSubClassDescription;

            var RequestedInsuranceCompanyDetails = [];
            var RequestedInsuranceCompanyDetails = quatationLine[i].RequestedInsuranceCompanyDetails;
            for (var j = 0; j < RequestedInsuranceCompanyDetails.length; j++) {
                var InsuranceCompanyName = RequestedInsuranceCompanyDetails[j].InsuranceCompanyName;
                var InsuranceCompanyID = RequestedInsuranceCompanyDetails[j].InsuranceCompanyID;
                $scope.InsuranceCompanyID = InsuranceCompanyID;





                var QuotationDetailsInsCompanyHeaderDetails = [];
                var QuotationDetailsInsCompanyHeaderDetails = RequestedInsuranceCompanyDetails[j].QuotationDetailsInsCompanyHeaderDetails;


                for (var k = 0; k < QuotationDetailsInsCompanyHeaderDetails.length; k++) {


                    var QuotationDetailsInsCompanyLineDetails = [];
                    var QuotationDetailsInsCompanyLineDetails = QuotationDetailsInsCompanyHeaderDetails[k].QuotationDetailsInsCompanyLineDetails;
                    // alert("QuotationDetailsInsCompanyLineDetails" + angular.toJson(QuotationDetailsInsCompanyLineDetails));
                    var sumInsured = QuotationDetailsInsCompanyLineDetails[0].SumInsured;
                    $scope.insSubClassID = QuotationDetailsInsCompanyLineDetails[0].InsuranceSubClassID;



                    $scope.QuotationDetailsInsCompanyLineDetails = $filter('filter')(QuotationDetailsInsCompanyLineDetails, { "InsuranceSubClassID": insSubClassID.toString() }, false);
                    $scope.QuotationDetailsInsCompanyScopeDetails = [];
                    $scope.selectedSubClassIDFromInsCompany = [];
                    var QuotationDetailsInsCompanyScopeDetails = QuotationDetailsInsCompanyLineDetails[0].QuotationDetailsInsCompanyScopeDetails;


                    for (var l = 0; l < QuotationDetailsInsCompanyScopeDetails.length; l++) {

                        $scope.QuotationDetailsInsCompanyScopeDetails.push({
                            "CommonInsScopeID": QuotationDetailsInsCompanyScopeDetails[l].QuotationDetailsInsCompanyScopeID,
                            "InsuranceSubClassID": insSubClassID,
                            "InsSubClass": InsSubClass,
                            "InsuranceClassCode": InsClass,
                            "ScopeDescription": QuotationDetailsInsCompanyScopeDetails[l].ScopeDescription,
                            "isChecked": true,
                            "ExcessType": QuotationDetailsInsCompanyScopeDetails[l].ExcessType,
                            "ExcessAmount": QuotationDetailsInsCompanyScopeDetails[l].ExcessAmount
                        });



                    }


                    $scope.selectedSubClassIDFromInsCompany.push({
                        "InsuranceCompanyID": InsuranceCompanyID,
                        "InsuranceSubClassID": insSubClassID,
                        "InsuranceCompanyName": InsuranceCompanyName,
                        "InsClass": InsClass,
                        "InsSubClass": InsSubClass,
                        "inslassID": inslassID,
                        "SumInsured": sumInsured,
                        "QuotationDetailsInsCompanyScopeDetails": $scope.QuotationDetailsInsCompanyScopeDetails

                    });


                    //alert("QuotationDetailsInsCompanyScopeDetails" + angular.toJson($scope.QuotationDetailsInsCompanyScopeDetails));



                }
            }


        }


    }


    function getAllPartners() {
        //alert("hh");
        PolicyRenewalService.getAllPartners().then(function (results) {

            $scope.Partners = results.data;
            //alert(angular.toJson(results.data));

        });
    };

    function GetAllComStructureHeaders() {
        //alert("hh");
        PolicyRenewalService.getAllComStructuresHeaders().then(function (results) {

            $scope.commitionStructuresHeaders = results.data;

        });
    };

    function getAllComStructuresLine() {
        //alert("hh");
        PolicyRenewalService.getAllComStructuresLine().then(function (results) {

            $scope.commitionStructuresLine = results.data;
            //alert(" commitionStructuresLine " + angular.toJson(results.data));

        });
    };

    function getPolicyRenewal() {
      //  alert($scope.policyInfor.PolicyInfoRecID);
        PolicyRenewalService.getAllPolicyRenewal($scope.policyInfor.PolicyInfoRecID).then(function (results) {

            $scope.PolicyRenewalhistories = results.data;
          //  alert(" PolicyRenewalhistories " + angular.toJson(results.data));

        });
    };

    function notyFy(message) {

        noty({
            text: message,
            layout: 'topRight',
            buttons: [

                    {
                        addClass: 'btn btn-danger btn-clean', text: 'OK', onClick: function ($noty) {
                            $noty.close();
                            //noty({ text: 'You clicked "Cancel" button', layout: 'topRight', type: 'error' });
                        }
                    }
            ]
        });





    }

    function initialize()
    {
        getCurrentUser();

        $scope.PolicyDate = $filter('date')(new Date(), 'MM/dd/yyyy');
        $scope.PolicyNumber = "POLI" + $scope.quotationHeaderID;
        $scope.quotationHeaderID = null;
        $scope.isDisabled = true;
        $scope.policyInforList = [];
        $scope.policyRenewalHistory = {};


        $scope.policyRenewalHistory.NotificationDate = $filter('date')(new Date(), 'MM/dd/yyyy');
        $scope.policyRenewalHistory.RenewalDate = $filter('date')(new Date(), 'MM/dd/yyyy');
        $scope.policyRenewalHistory.IsSent = false;
        $scope.policyRenewalHistory.IsCancel = false;
        $scope.PolicyRenewalhistories = [];
        $scope.clientObj = {};
        $scope.QutationHeader = {};
        $scope.policyCommissionPayment = [];
        getAllPolicyInfo();
        getAllPartners();
        GetAllComStructureHeaders();
        getAllComStructuresLine();
    
    
    
    }

    function getCurrentUser() {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
            if ($scope.currentUser.BusinessUnitName === "AVIVA") {
                //    $("#isAVIVA").show();
                $scope.IsAVIVA = true;

            }
            //var currentUser = $scope.currentUser;
        });
    }

















    //  $scope.PolicyInfoRec = PolicyInfoRec;
    //$scope.partner = 'Partner';
    ////$scope.rate = 'Rate';
    //$scope.amount = '5000';

    $scope.rate = [1, 2, 3];
    $scope.partner = ['Hi', 'Hello', 'Welcome'];
    $scope.selectedpartner = $scope.partner[0];
    $scope.selectedrate = $scope.rate[0];

    $scope.add = function () {

        $scope.PolicyInfoRec.push({
            "Partner": $scope.selectedpartner,
            "Rate": $scope.selectedrate,
            "Amount": $scope.amount
        });
        // $scope.partner = '';
        //$scope.rate = ;
        $scope.amount = '';

      //  alert(angular.toJson($scope.PolicyInfoRec));


    };


});
