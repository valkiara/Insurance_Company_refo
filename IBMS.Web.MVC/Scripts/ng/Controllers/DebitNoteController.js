'use strict';


testAPIApp.controller("DebitNoteController", function ($scope, $http, DebitNoteService, AuthService, $window, $filter, $log) {

    $scope.TransactionTypes = [
{
    "transactionTypeID": "1",
    "TTypeCode": "CR",
    "Description": "Credit"
},
{
    "transactionTypeID": "2",
    "TTypeCode": "DB",
    "Description": "Debit"
}
 ];
    $scope.PolicyDate = $filter('date')(new Date(), 'MM/dd/yyyy');
    $scope.DebitDate = $filter('date')(new Date(), 'MM/dd/yyyy');
    $scope.PolicyNumber = "POLI" + $scope.quotationHeaderID;
    $scope.DebitNoteNumber = "DEBIT" + $scope.DebitDate;
    $scope.quotationHeaderID = null;
    $scope.policyInforList = [];
    $scope.clientObj = {};
    $scope.QutationHeader = {};
    $scope.policyCommissionPayment = [];


    $scope.payment = [];
    $scope.DebitNoteList = [];
    $scope.PolicyInfoRecList = [];
    $scope.DebitChargeList = [];
    getCurrentUser();
    getAllPaymentDebitNotes();



    $scope.Edit = function (paymen)
    {
        $scope.payment = paymen;
       
        $scope.ClientID = paymen.ClientID;
        $scope.LoadClientByID();
        getAllPolicyInfo();


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
      //  alert(angular.toJson($scope.policyInfor));
        DebitNoteService.UpdatePolicyInfoRecording(1, $scope.policyInfor, 1).then(function (results) {
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
         
          alert(angular.toJson($scope.policyCommissionPayment));
    // $scope.policyCommissionPayment = [{ "CommisionTypeID": 2, "CommisionValue": 2500 }, { "CommisionTypeID": 3, "CommisionValue": 3500 }];


    }
    $scope.Add = function (policyInfor)
    {

       // 
        $scope.policyInfor = policyInfor;
        $scope.nonCommission = policyInfor.PremiumIncludingTax;
    
        $scope.gross = $scope.nonCommission - $scope.commitionTotal;
        $scope.ttlnonCommission = 0;
        $scope.ttlgross = 0;
      // policyInfor.PolicyInfoRecID=
            getQuatationHeader();
                LoadCommition();
             getAllCgargeType();


      


    }
    $scope.Load = function (policyInfor)
    {
        $scope.policyInfor = policyInfor;
        getQuatationHeader();
        LoadCommition();
    //    getAllCgargeType();


    }

    $scope.AddToChargeType = function ()
    {
     //   $scope.policyInfor
    
       
        var ChargeType = $filter('filter')($scope.chargetypes, { "ChargeTypeID": $scope.selectedchargetype }, true);
        var TransactionTypes = $filter('filter')($scope.TransactionTypes, { "transactionTypeID": $scope.selectedcrdb }, true);
        var amount =(ChargeType[0].Percentage * $scope.nonCommission)/100;
        //alert(angular.toJson($scope.ChargeType));
        
        $scope.DebitChargeList.push(
            {
                "ChargeTypeID": $scope.selectedchargetype,
                "ChargeType": ChargeType[0].ChargeTypeName,
                "Amount": amount,
                "IsCR": $scope.selectedcrdb == 1 ? true : false,
                "TTypeCode": TransactionTypes[0].TTypeCode

            });
        //alert(angular.toJson($scope.DebitChargeList));

    }
    $scope.AddToDebitNote = function ()
    {
        $scope.ttlnonCommission = $scope.ttlnonCommission + $scope.nonCommission;
        $scope.ttlgross = $scope.ttlgross + $scope.gross
        //policyInfor.PolicyInfoRecID
        $scope.PolicyInfoRecList.push({
            "PolicyNumber": $scope.policyInfor.PolicyNumber,
            "PolicyInfoRecID": $scope.policyInfor.PolicyInfoRecID,
            "NonCommissionPremium": $scope.nonCommission,
            "GrossPremium": $scope.gross,
            "PolicyInfoChargeList": $scope.DebitChargeList

        });
        $scope.DebitChargeList = [];



    }
    $scope.AddToPayment = function ()
    {
        $scope.DebitNoteList.push(
        {
            "DebitNoteNumber": $scope.DebitNoteNumber,
            "TotalNonCommissionPremium": $scope.ttlnonCommission,
            "TotalGrossPremium": $scope.ttlgross,
            "PolicyInfoPaymentList": $scope.PolicyInfoRecList

        });

        $scope.PolicyInfoRecList = [];
        $scope.ttlgross = 0;
        $scope.ttlnonCommission = 0;







    }
    $scope.ConfirmPayment = function ()
    {
       $scope.payment.push(
      {
          "ClientID": $scope.ClentRequestHeader.ClientID,
          "PaymentAmount": $scope.ttlnonCommission,
          "DebitNoteList": $scope.DebitNoteList
      });
       $scope.DebitNoteList = [];
       alert(angular.toJson($scope.payment));
       DebitNoteService.SaveDebitNote($scope.payment[0],1).then(function (results) {

           // $scope.Partners = results.data;
            alert(angular.toJson(results));

        });
        




    }
    $scope.LoadClientByID = function () {
        // $scope.clientObj = $scope.client;
         //alert($scope.ClientID);

        $scope.clientObj = $filter('filter')($scope.Clients, { "ClientID": $scope.ClientID }, false)[0];

        //  alert(angular.toJson($scope.Clients));
        $scope.ClientID = $scope.clientObj.ClientID
        $scope.customerID = $scope.clientObj.ClientID;
        $scope.cusName = $scope.clientObj.ClientName;
        $scope.Address1 = $scope.clientObj.ClientAddress;
        $scope.Address2 = $scope.clientObj.ClientAddress;
        $scope.Address3 = $scope.clientObj.ClientAddress;
        //$("#dp-3").val($scope.clientObj.DOB === "" ? $scope.RequestedDate : $scope.clientObj.DOB);
        //$scope.NIC = $scope.clientObj.NIC;
        //$scope.mobileNo = $scope.clientObj.ContactNo;
        //$scope.fixedLine = $scope.clientObj.FixedLine;
        //$scope.email = $scope.clientObj.Email;
        //$scope.homeCountry = $scope.clientObj.HomeCountryID;
        //$scope.residentCountry = $scope.clientObj.ResidentCountryID;
        // $("#dp-2").val()=;//$scope.inspectiondays;
        $scope.fDiscount = $scope.clientObj.FamilyDiscount;

        //alert($scope.cusName);
        // alert($scope.Address1);





    }

    function getCurrentUser() {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
      
            $scope.BUnits = [];
            
            $scope.BUnit = $scope.currentUser.BusinessUnitID;
           // alert($scope.BUnit);
            getAllClients();
        });
    }
    function getAllPolicyInfo() {
        DebitNoteService.getAllPolicyInfoRecordingByClientID($scope.ClientID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.policyInforList = results.data;
                for (var i = 0; i < $scope.policyInforList.length;i++)
                {
                    $scope.policyInforList[i].isChecked = false;
                }
                //alert(angular.toJson($scope.policyInforList));
                    for (var j = 0; j < $scope.payment.DebitNoteList.length;j++)
                    {
                        var PolicyInfoPaymentList = $scope.payment.DebitNoteList[j].PolicyInfoPaymentList;
                        for (var k = 0; k < PolicyInfoPaymentList.length; k++)
                        {
                            var PolicyInfoRecID = PolicyInfoPaymentList[k].PolicyInfoRecID;
                            var found = $filter('filter')($scope.policyInforList, { "PolicyInfoRecID": PolicyInfoRecID }, true);
                            if (found.length>0)
                            {
                                var index = $scope.policyInforList.indexOf(found[0]);
                                $scope.policyInforList[index].isChecked = true;
                                break;
                            
                            }




                    
                    
                    }
                   // $scope.payment.DebitNoteList.PolicyInfoPaymentList.PolicyInfoRecID
                
                
                
                }
                    //alert("yyu"+angular.toJson($scope.policyInforList));
            }
            else {
                //$scope.Company = [];
            }
        });
    };
    function getQuatationHeader() {
        DebitNoteService.getQuatationHeader($scope.policyInfor.QuotationHeaderID).then(function (results) {
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
          DebitNoteService.getClientRequest($scope.QutationHeader.ClientRequestHeaderID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.ClentRequestHeader = results.data;
                ReLoadClientByID();
                ReLoadProperty();
            
            }
            else {
                $scope.ClentRequestHeader = [];
            }
        });

    
   
    }
    function ReLoadClientByID(ClientID) {

      //  $scope.clientObj = $filter('filter')($scope.Clients, { "ClientID": $scope.ClientID }, false)[0];
        DebitNoteService.getClientByID(ClientID).then(function (results) {
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
    function ReLoadProperty() {
        //alert($scope.selectedSubClassIDFromInsCompany[0].InsuranceSubClassID);

        // alert("ghghj");
        //  $scope.ClientPropertyDetails = {};
        var listMain = $scope.ClentRequestHeader.ClientRequestLineDetails;
        //alert(angular.toJson(listMain));
        var list = $filter('filter')(listMain, { "InsSubClassID": $scope.selectedSubClassIDFromInsCompany[0].InsuranceSubClassID }, true);

        for (var j = 0; j < list.length; j++) {
            $scope.ClientPropertyDetails = list[j].ClientPropertyDetails[0];
            //alert(angular.toJson($scope.ClientPropertyDetails));


        }









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
        DebitNoteService.getAllPartners().then(function (results) {

            $scope.Partners = results.data;
            //alert(angular.toJson(results.data));

        });
    };
    function GetAllComStructureHeaders() {
        //alert("hh");
        DebitNoteService.getAllComStructuresHeaders().then(function (results) {

            $scope.commitionStructuresHeaders = results.data;
           
        });
    };
    function getAllComStructuresLine() {
        //alert("hh");
        DebitNoteService.getAllComStructuresLine().then(function (results) {

            $scope.commitionStructuresLine = results.data;
            //alert(" commitionStructuresLine " + angular.toJson(results.data));

        });
    };
    function LoadCommition() {
        $scope.commitionTotal=0;
        $scope.policyCommissionPayment = [];
        for (var i = 0; i < $scope.policyInfor.PolicyCommissionPaymentDetails.length; i++) {
            var policyCommissionPaymentDetails = $scope.policyInfor.PolicyCommissionPaymentDetails;
            $scope.commitionTotal=$scope.commitionTotal+policyCommissionPaymentDetails[i].CommisionValue;
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
    function getAllCgargeType() {
            DebitNoteService.getAllChargeType().then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.chargetypes = results.data;
                
                // alert(angular.toJson(results.data));
            }
            else {
                $scope.chargetypes = [];
            }
        });
    };


    function getAllPaymentDebitNotes() {
            DebitNoteService.getAllPaymentDebitNotes().then(function (results) {
                $scope.showLoader = false;
                if (results.status === true)
                {
                    $scope.PaymentDebitNotes = results.data;

                       alert(angular.toJson(results.data));
                }
                else {
                    $scope.chargetypes = [];
                }
            });
        };
    function getAllClients() {
        //alert("hh");
        DebitNoteService.getAllClients($scope.BUnit).then(function (results) {
            //$Scope.BussinessUnit = results;
            $scope.Clients = results.data;
           // getAllPolicyInfo();
            getAllPartners();
            GetAllComStructureHeaders();
            getAllComStructuresLine();
           // getAllPaymentDebitNotes();
           // alert(angular.toJson(results));

        });
    };

    
});
