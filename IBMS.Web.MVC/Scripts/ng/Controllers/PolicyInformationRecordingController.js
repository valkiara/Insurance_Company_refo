'use strict';


testAPIApp.controller("PolicyInformationRecordingController", function ($scope, $http, PolicyInfoRecService, AuthService, $window, $filter, $log) {


    $scope.PolicyDate = $filter('date')(new Date(), 'MM/dd/yyyy');
    $scope.PolicyNumber = "POLI" + $scope.quotationHeaderID;
    $scope.quotationHeaderID = null;
    $scope.policyInforList = [];
    $scope.clientObj = {};
    $scope.QutationHeader = {};
    $scope.policyCommissionPayment = [];

    $scope.isLoaded = false;
    getCurrentUser();
    getAllPolicyInfo();
    getAllPartners();
    GetAllComStructureHeaders();
    getAllComStructuresLine();

    $scope.Edit = function (policyInfor)
    {
        $scope.policyInfor = policyInfor;
        getQuatationHeader();
        //alert(angular.toJson($scope.policyInfor));
        LoadCommition();
       // $scope.policyInfor. = policyInfor;
       // alert($scope.policyInfor.PeriodOfCoverToDate);
      //  alert($scope.policyInfor.PeriodOfCoverFromDate);
        $("#tab-first").removeClass('tab-pane active');
        $("#tab-first").addClass('tab-pane');
        $("#tab-second").addClass('tab-pane active');


        $("#tabView").removeClass('active');

        $("#tabEdit").addClass('active');
      

    }

    $scope.UpdatePolicy = function ()
    {
        // $scope.PolicyNumber = "POLI" + $scope.quotationHeaderID;
        $scope.policyInfor.PolicyCommissionPaymentDetails = $scope.policyCommissionPayment;
        alert(angular.toJson($scope.ClentRequestHeader.ClientRequestHeaderID));
        PolicyInfoRecService.UpdatePolicyInfoRecording($scope.ClentRequestHeader.ClientRequestHeaderID, $scope.policyInfor, $scope.currentUser.UserID).then(function (results) {
            alert(angular.toJson(results));
        });



    }
    $scope.AddCommision = function ()
    {

        $scope.found = $filter('filter')($scope.policyCommissionPayment, { "CommisionStructureID": $scope.comHeader, "partner": $scope.partner }, true);

        if ($scope.found.length == 0)
        {
            $scope.ChangeCommition($scope.partner);

        
        }
        else
        {
         noty({ text: 'commition Structure already Added For This Partner', layout: 'center', type: 'error' });
        }
  

    }
    $scope.ChangeCommition = function (partner)
    {
        $scope.commitionStructuresHeader = $filter('filter')($scope.commitionStructuresHeaders, { "PartnerID": partner, "InsuranceSubClassID": $scope.insSubClassID, "InsuranceCompanyID": $scope.InsuranceCompanyID }, true);

        if ($scope.commitionStructuresHeader.length==1)

        {
            $scope.comHeader = $scope.commitionStructuresHeader.CommisionStructureID;
            var CommisionStructureID=$scope.commitionStructuresHeader[0].CommisionStructureID;
            var commitionName=$scope.commitionStructuresHeader[0].CommisionStructureName;
            $scope.commitionStructureLineAdded = $filter('filter')($scope.commitionStructuresLine, { "CommisionStructureID": CommisionStructureID}, true);
        }

        else
        {
          
            $scope.commitionStructureLineAdded = $filter('filter')($scope.commitionStructuresLine, { "CommisionStructureID": $scope.comHeader }, true);

        
        
        
        
        }




          for(var i=0;i<$scope.commitionStructureLineAdded.length;i++)
            {
                var rate=$scope.commitionStructureLineAdded[i].RateValue;
                var isfixed=$scope.commitionStructureLineAdded[i].IsFixed;
                var commmAmount=0;
                if(isfixed)
                {
                    commmAmount=$scope.policyInfor.PremiumIncludingTax-rate;
                
                
                }
                else
                {
                
                    commmAmount=($scope.policyInfor.PremiumIncludingTax*rate);
                
                
                
                }

            
             
               
                $scope.policyCommissionPayment.push
                    ({
                        "partner": partner,
                        "CommisionTypeID": 2,
                    "CommisionStructureID": $scope.comHeader,
                    "CommisionValue": commmAmount,
                    "CommisionRate": rate,
                    "CommitionName": commitionName
                     });

            
            }
         
         // alert(angular.toJson($scope.policyCommissionPayment));
    // $scope.policyCommissionPayment = [{ "CommisionTypeID": 2, "CommisionValue": 2500 }, { "CommisionTypeID": 3, "CommisionValue": 3500 }];


    }

    function getCurrentUser() {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
            if ($scope.currentUser.BusinessUnitName === "AVIVA")
            {
                //    $("#isAVIVA").show();
                $scope.IsAVIVA = true;

            }
            getAllPolicyInfo();
            getAllPartners();
            GetAllComStructureHeaders();
            getAllComStructuresLine();
            //var currentUser = $scope.currentUser;
        });
    }

    function getAllPolicyInfo() {
        PolicyInfoRecService.getAllPolicyInfoRecording().then(function (results) {
            $scope.showLoader = false;
            if (results.status === true)
            {
                $scope.policyInforList = results.data;
                
                  //alert(angular.toJson(results.data));
            }
            else {
                $scope.Company = [];
            }
        });
    };
    function getQuatationHeader() {
        PolicyInfoRecService.getQuatationHeader($scope.policyInfor.QuotationHeaderID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.QutationHeader = results.data;
                ReLoadRequestHeader();
                LoadQuotationDetailsInsCompanyLineDetails($scope.policyInfor.QuotationDetailsInsCompanyLineID);
               
                //alert(angular.toJson(results.data));
            }
            else {
                $scope.QutationHeader = {};
            }
        });
    };
    function ReLoadRequestHeader() {

        //  $scope.clientObj = $filter('filter')($scope.Clients, { "ClientID": $scope.ClientID }, false)[0];

          PolicyInfoRecService.getClientRequest($scope.QutationHeader.ClientRequestHeaderID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.ClentRequestHeader = results.data;
                $scope.isLoaded = true;
                ReLoadClientByID();
               // alert("hhh" + $scope.ClentRequestHeader.ClientRequestHeaderID);
                //alert(angular.toJson($scope.ClentRequestHeader));
            }
            else {
                $scope.ClentRequestHeader = [];
            }
        });

    
   
    }
    function ReLoadClientByID() {

      //  $scope.clientObj = $filter('filter')($scope.Clients, { "ClientID": $scope.ClientID }, false)[0];
        PolicyInfoRecService.getClientByID($scope.ClentRequestHeader.ClientID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.clientObj = results.data;

                //alert(angular.toJson(results.data));

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






            }
            else {
                $scope.clientObj = {};
            }
        });

    
            

        //alert($scope.cusName);
        // alert($scope.Address1);





    }

    function LoadQuotationDetailsInsCompanyLineDetails( QuotationDetailsInsCompanyLineID) {
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


                for (var k = 0; k < QuotationDetailsInsCompanyHeaderDetails.length; k++)
                        {


                            var QuotationDetailsInsCompanyLineDetails = [];
                            var QuotationDetailsInsCompanyLineDetails = QuotationDetailsInsCompanyHeaderDetails[k].QuotationDetailsInsCompanyLineDetails;
                           // alert("QuotationDetailsInsCompanyLineDetails" + angular.toJson(QuotationDetailsInsCompanyLineDetails));
                             var sumInsured = QuotationDetailsInsCompanyLineDetails[0].SumInsured;
                             $scope.insSubClassID = QuotationDetailsInsCompanyLineDetails[0].InsuranceSubClassID;



                            $scope.QuotationDetailsInsCompanyLineDetails = $filter('filter')(QuotationDetailsInsCompanyLineDetails, { "InsuranceSubClassID": insSubClassID.toString() }, false);
                            $scope.QuotationDetailsInsCompanyScopeDetails = [];
                            $scope.selectedSubClassIDFromInsCompany = [];
                            var QuotationDetailsInsCompanyScopeDetails = QuotationDetailsInsCompanyLineDetails[0].QuotationDetailsInsCompanyScopeDetails;
                            
                           
                            for (var l = 0; l < QuotationDetailsInsCompanyScopeDetails.length; l++)
                            {

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
        PolicyInfoRecService.getAllPartners().then(function (results) {

            $scope.Partners = results.data;
            //alert(angular.toJson(results.data));

        });
    };

    function GetAllComStructureHeaders() {
        //alert("hh");
        PolicyInfoRecService.getAllComStructuresHeaders().then(function (results) {

            $scope.commitionStructuresHeaders = results.data;
           
        });
    };

    function getAllComStructuresLine() {
        //alert("hh");
        PolicyInfoRecService.getAllComStructuresLine().then(function (results) {

            $scope.commitionStructuresLine = results.data;
            //alert(" commitionStructuresLine " + angular.toJson(results.data));

        });
    };
    function LoadCommition() {
        $scope.policyCommissionPayment = [];
        for (var i = 0; i < $scope.policyInfor.PolicyCommissionPaymentDetails.length; i++) {
            var policyCommissionPaymentDetails = $scope.policyInfor.PolicyCommissionPaymentDetails;
            $scope.policyCommissionPayment.push
         ({
             "partner": 2,
             "CommisionTypeID": policyCommissionPaymentDetails[i].CommisionTypeID,
             "CommisionStructureID": policyCommissionPaymentDetails[i].ComStructLineID,
             "CommisionValue": policyCommissionPaymentDetails[i].CommisionValue,
             "CommisionRate": policyCommissionPaymentDetails[i].RateValue,
             "CommitionName": policyCommissionPaymentDetails[i].CommissionTypeName
         });



        }







        alert(angular.toJson($scope.policyCommissionPayment));


    }























  //  $scope.PolicyInfoRec = PolicyInfoRec;
    //$scope.partner = 'Partner';
    ////$scope.rate = 'Rate';
    //$scope.amount = '5000';

    $scope.rate = [1,2,3];
    $scope.partner = ['Hi', 'Hello', 'Welcome'];
    $scope.selectedpartner = $scope.partner[0];
    $scope.selectedrate = $scope.rate[0];

    $scope.add = function () {

        $scope.PolicyInfoRec.push({
            "Partner": $scope.selectedpartner,
            "Rate": $scope.selectedrate,
        "Amount" : $scope.amount
        });
       // $scope.partner = '';
       //$scope.rate = ;
        $scope.amount = '';

        //alert(angular.toJson($scope.PolicyInfoRec));


    };

    
});
