'use strict';


testAPIApp.controller("ClaimRecordingController", function ($scope, $http,ClaimRecordingService,AuthService ,$window, $filter, $log) {

    $scope.ClaimSelectReasons = [
  {
      "selectReasonID": "1",
      "SelectReasonCode": "Reject",
      "Description": "Reject Cliaim"
  },
   {
       "QuotationStatusID": "2",
       "QuotationStatusCode": "ReOpen",
       "Description": "ReOpen Claim"
   }];
    $scope.PaymentPendingReasons = [
{
    "selectReasonID": "1",
    "SelectReasonCode": "Reject",
    "Description": "Reject Cliaim"
},
{
    "QuotationStatusID": "2",
    "QuotationStatusCode": "ReOpen",
    "Description": "ReOpen Claim"
}];
    $scope.PolicyDate = $filter('date')(new Date(), 'MM/dd/yyyy');
    $scope.PolicyNumber = "POLI" + $scope.quotationHeaderID;
    $scope.quotationHeaderID = null;
    $scope.isDisabled = true;
    $scope.policyInforList = [];
    $scope.policyRenewalHistory = {};
    $scope.claimRecording = {};
    $scope.claimRecording.ClaimRecordingID = -1;
    $scope.ClaimHistory = {};
    $scope.policyRenewalHistory.IsSent = false;
    $scope.policyRenewalHistory.IsCancel = false;
    $scope.PolicyRenewalhistories=[];
    $scope.clientObj = {};
    $scope.QutationHeader = {};
    $scope.policyCommissionPayment = [];
    getAllPolicyInfo();
    getAllPartners();
    GetAllComStructureHeaders();
    getAllComStructuresLine();
    $scope.loadCurrencyDetails();
   

    $scope.EditForClaim = function (policyInfor) {

        $scope.policyInfor = policyInfor;
        initialize();
        getQuatationHeader();
        getPolicyRenewal();
        getClaimHistory();
        getAllDocument();
        LoadCommition();
        $scope.loadCurrencyDetails();
        $("#tab-first").removeClass('tab-pane active');
        $("#tab-first").addClass('tab-pane');
        $("#tab-second").addClass('tab-pane active');


        $("#tabView").removeClass('active');

        $("#tabEdit").addClass('active');


    }

    $scope.LoadInsClassType = function (InsuranceClassID) {
        ClaimRecordingService.LoadInsClassType(InsuranceClassID).then(function (results) {
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableInsClassType.push({ value: results.data[i].InsuranceSubClassID, text: results.data[i].InsClassTypeDes });
                }
            }
            else {
                $scope.availableInsClassType = [];
            }
        });
    };

    $scope.LoadClaimStatus = function () {
        ClaimRecordingService.LoadClaimStatus().then(function (results) {
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableClaimStatus.push({ value: results.data[i].StatusId, text: results.data[i].StatusName });
                }
            }
            else {
                $scope.availableClaimStatus = [];
            }
        });
    };



    $scope.LoadavailableClaimePaidStatus = function () {
        ClaimRecordingService.LoadavailableClaimePaidStatus().then(function (results) {
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableClaimePaidStatus.push({ value: results.data[i].ClaimPaidStatus, text: results.data[i].ClaimPaidStatusDescription});
                }
            }
            else {
                $scope.availableClaimePaidStatus = [];
            }
        });
    };

    $scope.calcCurrancyRate = function () {
        var result = 0;
        result = (policyInfoRecObj.AmountClaimed * policyInfoRecObj.SumAssuredCurrencyTypeID);
        $scope.inputAmountClaimed = result;
    };

    $scope.loadCurrencyDetails = function () {
        ClaimRecordingService.loadCurrencies().then(function (results) {
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableCurrencies.push({ value: results.data[i].CurrencyID, text: results.data[i].CurrencyCode });
                }
            }
            else {
                $scope.availableCurrencies = [];
            }
        });
    };

    $scope.SaveClaimRecording = function () {
        $scope.claimRecording.PolicyInfoRecID = $scope.policyInfor.PolicyInfoRecID
        $scope.claimRecording.ClaimRecHistoryDetails = { "Description": $scope.ClaimSelectReason, "RecordingDate": $scope.ClaimHistory.RecordingDate, "Reason": $scope.ClaimSelectReason };

        $scope.claimRecording.ClaimRecPendingDocDetails.push({ "DocumentID": $scope.document });
        $scope.claimRecording.IsOpened = true;
        $scope.claimRecording.IsWithdrawn = false;
        $scope.claimRecording.WithdrawReason = "";
      //  $scope.claimRecording = { "PolicyInfoRecID": 1, "ClaimNo": "Test001", "DateOfLoss": "02/26/2018", "DateOfIntimation": "03/26/2018", "DamageDescription": "Test Description", "AmountClaimed": 2500, "AmountPaid": 2000, "WithdrawReason": "Test Withdraw Reason", "IsOpened": true, "IsWithdrawn": true, "ClaimRecHistoryDetails": $scope.claimRecording.ClaimRecHistoryDetails, "ClaimRecPendingDocDetails": $scope.claimRecording.ClaimRecPendingDocDetails };
       //  alert($scope.ClaimSelectReason);
        //$scope.claimRecording.DateOfLoss = $("#DateOfLoss").val();
        //$scope.claimRecording.DateOfIntimation = $("#DateOfIntimation").val();
       

        //alert(angular.toJson($scope.claimRecording));
      //  $scope.policyRenewalHistory.PolicyInfoRecID = $scope.policyInfor.PolicyInfoRecID;
    
        if ($scope.claimRecording.ClaimRecordingID > 0)
        {
            ClaimRecordingService.UpdateClaimRecording($scope.claimRecording, 1).then(function (results) {
                alert(angular.toJson(results));
                if (results.status === true && results.message === "Successfully Saved") {
                    getClaimHistory(true);
                    noty({ text: results.message, layout: 'center', type: 'success' });

                }
                else {
                    noty({ text: results.message, layout: 'center', type: 'error' });


                }


            });


            
        }
        else
        {


            ClaimRecordingService.saveClaimRecording($scope.claimRecording, 1).then(function (results) {
                //alert(angular.toJson(results));
                if (results.status === true && results.message === "Successfully Saved") {
                    getClaimHistory(true);
                    noty({ text: results.message, layout: 'center', type: 'success' });

                }
                else {
                    noty({ text: results.message, layout: 'center', type: 'error' });


                }


            });





        }


       

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
    $scope.LoadClaim = function (claimHistory)
    {
        $scope.show = true;
        $scope.claimRecording = claimHistory;


    }
    $scope.initialize = function ()
    {

        initialize();

    
    }

    function initialize()
    {

        $scope.claimRecording = {};
        $scope.claimRecording.ClaimRecHistoryDetails = {};
        $scope.claimRecording.ClaimRecPendingDocDetails = [];
        $scope.claimRecording.DateOfLoss = $filter('date')(new Date(), 'MM/dd/yyyy');
        $scope.claimRecording.DateOfIntimation = $filter('date')(new Date(), 'MM/dd/yyyy');
        $scope.ClaimSelectReason = "";
        $scope.policyRenewalHistory.NotificationDate = $filter('date')(new Date(), 'MM/dd/yyyy');
        $scope.ClaimDate = $filter('date')(new Date(), 'MM/dd/yyyy h:mm a');

        $scope.ClaimHistory.RecordingDate = $filter('date')(new Date(), 'MM/dd/yyyy');
        $scope.claimRecording.ClaimNo = "CLAIM-" + $scope.policyInfor.PolicyInfoRecID + "_" + $scope.ClaimDate;
        $scope.availableInsClassType = [];
        





    }
    function LoadCommition() {
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
        ClaimRecordingService.getAllPolicyInfoRecording().then(function (results) {
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
        ClaimRecordingService.getQuatationHeader($scope.policyInfor.QuotationHeaderID).then(function (results) {
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
        ClaimRecordingService.getClientRequest($scope.QutationHeader.ClientRequestHeaderID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.ClentRequestHeader = results.data;
                ReLoadProperty();
                ReLoadClientByID();
                //alert($scope.ClentRequestHeader.ClientID);
               //alert(angular.toJson(results.data));
            }
            else {
                $scope.ClentRequestHeader = {};
            }
        });



    }
    function ReLoadClientByID() {

        //  $scope.clientObj = $filter('filter')($scope.Clients, { "ClientID": $scope.ClientID }, false)[0];
        ClaimRecordingService.getClientByID($scope.ClentRequestHeader.ClientID).then(function (results) {
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
    function ReLoadProperty()
    {
        //alert($scope.selectedSubClassIDFromInsCompany[0].InsuranceSubClassID);
        
           // alert("ghghj");
      //  $scope.ClientPropertyDetails = {};
            var listMain = $scope.ClentRequestHeader.ClientRequestLineDetails;
            //alert(angular.toJson(listMain));
            var list = $filter('filter')(listMain, { "InsSubClassID": $scope.selectedSubClassIDFromInsCompany[0].InsuranceSubClassID }, true);

           for (var j = 0; j < list.length;j++)
                       {
               $scope.ClientPropertyDetails = list[j].ClientPropertyDetails[0];
               //alert(angular.toJson($scope.ClientPropertyDetails));
           
           
                       }
        
        
        
       





    }
    function LoadQuotationDetailsInsCompanyLineDetails(QuotationDetailsInsCompanyLineID) {
        var found = [];
        var quatationLine = $scope.QutationHeader.QuotationLineDetails;
       // alert(QutationHeader.QuotationLineDetails);
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
                    $scope.claimRecording.SIFRefNumber = InsClass + "_2018_" + $scope.policyInfor.PolicyInfoRecID;


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
                   // $scope.selectedSubClassIDFromInsCompany[0].InsuranceSubClassID;

                    //alert("QuotationDetailsInsCompanyScopeDetails" + $scope.reqList);



                }
            }


        }


    }


    function getAllPartners() {
        //alert("hh");
        ClaimRecordingService.getAllPartners().then(function (results) {

            $scope.Partners = results.data;
            //alert(angular.toJson(results.data));

        });
    };

    function GetAllComStructureHeaders() {
        //alert("hh");
        ClaimRecordingService.getAllComStructuresHeaders().then(function (results) {

            $scope.commitionStructuresHeaders = results.data;

        });
    };

    function getAllComStructuresLine() {
        //alert("hh");
        ClaimRecordingService.getAllComStructuresLine().then(function (results) {

            $scope.commitionStructuresLine = results.data;
            //alert(" commitionStructuresLine " + angular.toJson(results.data));

        });
    };

    function getPolicyRenewal() {
      //  alert($scope.policyInfor.PolicyInfoRecID);
        ClaimRecordingService.getAllPolicyRenewal($scope.policyInfor.PolicyInfoRecID).then(function (results) {

            $scope.PolicyRenewalhistories = results.data;
          //  alert(" PolicyRenewalhistories " + angular.toJson(results.data));

        });
    };


    function getClaimHistory(isCallBack) {
        //  alert($scope.policyInfor.PolicyInfoRecID);
        ClaimRecordingService.getClaimHistory($scope.policyInfor.PolicyInfoRecID).then(function (results) {

            //$scope.Claimhistories = results.data;
            $scope.Claimhistories = $filter('filter')(results.data, { "PolicyInfoRecID": $scope.policyInfor.PolicyInfoRecID}, true);

            if (isCallBack && $scope.Claimhistories.length>0)
            {
                $scope.claimRecording = $scope.Claimhistories[$scope.Claimhistories.length-1]
            }
           // alert(" PolicyRenewalhistories " + angular.toJson(results.data));

        });
    };



    function getAllDocument() {
        //  alert($scope.policyInfor.PolicyInfoRecID);
        ClaimRecordingService.getAllDocument().then(function (results) {

            $scope.documnts = results.data;
           // $scope.Claimhistories = $filter('filter')(results.data, { "PolicyInfoRecID": $scope.policyInfor.PolicyInfoRecID }, true);
           // alert(" documnts " + angular.toJson(results.data));

        });
    };


















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

     //   alert(angular.toJson($scope.PolicyInfoRec));


    };
   


    /*****************************CLiaim payment********************/
    $scope.ClaimPayment = function () {
        //$scope.$scope.claimRecording.ClaimRecordingID = -1;
        // $("#claimPayment").model("show");
        $scope.claimAmount = $scope.claimRecording.AmountClaimed;
        $scope.payAmount = $scope.claimRecording.AmountPaid;
        if ($scope.claimRecording.ClaimRecordingID>0)
        {
           getAllClaimPayment();
            $("#claimPaymentModel").modal("show");
        
        }
        else
        {
            noty({ text: 'Cannnot Find Claim To Make Payment!Please Make Claim Record First!', layout: 'center', type: 'error' });
        
        }
    
     


    }

       // 
        //$scope.refresh
    

    var claimHeader = {};
    var claimHeaderLine = [];
    $scope.claimHeader = claimHeader;
    $scope.claimHeaderLine = claimHeaderLine;
    //$scope.paymentDate = new Date();
    //$scope.claimAmount = '50000';
    $scope.paymentType = true;
    $scope.ClaimPaymentID = -1;
    $scope.paymentDate = $filter('date')(new Date(), 'MM/dd/yyyy');
    $scope.addPayment = function () {
        //alert('Hi');
        var addpayment;
        $scope.claimHeaderLine.push({
            'ChequeNo': $scope.chequeNo,
            'PaymentTypeID': 1,
            'PaidAmount': $scope.payAmount,
            'PaidDate': $scope.paymentDate,
            'IsFinal': $scope.paymentType
        });
        $scope.chequeNo = '';
        //$scope.PaymentTypeID = '';
        $scope.payAmount = '';
       // $scope.paymentDate = '';
        $scope.paymentType = true;
      
        
        $scope.tpay = $scope.totalPay();
        $scope.addpayment = !addpayment;
       

    };

    $scope.totalPay = function () {
        var total = 0;
        for (var i = 0; i < $scope.claimHeaderLine.length; i++) {
            total += parseInt($scope.claimHeaderLine[i].PaidAmount);
        }
        return total;
    };
    $scope.save = function () {
        $scope.claimHeader = {};
        $scope.claimHeader.ClaimPaymentMethodDetails = [];
        $scope.claimHeader.ClaimRecordingID = 2;
        $scope.claimHeader.ClaimAmount = $scope.claimAmount;
        $scope.claimHeader.Notes = $scope.notes;
        $scope.claimHeader.ClaimPaymentID = $scope.ClaimPaymentID;
      //  $scope.ClaimPaymentID = ClaimPaymentID
        $scope.claimHeader.ClaimPaymentMethodDetails = $scope.claimHeaderLine;
        $scope.claimHeader.tpay = $scope.tpay;
      
   
        if ($scope.claimHeader.ClaimPaymentID !== -1) {

            ClaimRecordingService.updateClaimPayment($scope.claimHeader, 1).then(function (results) {
                if (results.status == true) {
                    getAllClaimPayment();
                    alert(angular.toJson(results));
                    //setTimeout(function () { window.location.href = "/ClaimPayment/Index" }, 2500)
                }
                else {
                   // alert('failed');
                }
            });
        }
        else {


            ClaimRecordingService.saveClaimPayment($scope.claimHeader, 1).then(function (results) {
                if (results.status == true) {

                    alert(angular.toJson(results));
                   // setTimeout(function () { window.location.href = "/ClaimPayment/Index" }, 2500)

                }
               
            });
        }
};

    $scope.edit = function (CH) {
        var addpayment;
      //  alert(angular.toJson(CH));
        $scope.addpayment = !addpayment;
        $scope.claimHeaderLine=[];
        // tab - pane
        //tab-pane active
        $("#tabViewModel").removeClass('active');
        $("#tabEdit").addClass('active');

        $("#tab-modelfirst").removeClass('tab-pane active');
        $("#tab-modelfirst").addClass('tab-pane');

        $("#tab-secondModel").removeClass('tab-pane');
        $("#tab-secondModel").addClass('tab-pane active');
        $scope.claimHeader = CH;
        $scope.claimHeader.ClaimRecordingID = 2,
        $scope.claimAmount = CH.ClaimAmount;
        $scope.ClaimPaymentID = CH.ClaimPaymentID;
        $scope.notes = CH.Notes;
        $scope.tpay = CH.PaidAmount;

        // $scope.claimHeaderLine = CH.ClaimPaymentMethodDetails;
        //alert(angular.toJson(CH.ClaimPaymentMethodDetails));
        for (var i = 0; i < CH.ClaimPaymentMethodDetails.length; i++)
        {
            var date = CH.ClaimPaymentMethodDetails[i].PaidDate.split(' ');//$filter('date')(CH.ClaimPaymentMethodDetails[i].PaidDate, 'MM/dd/yyyy');
           // alert(date[0]);
            $scope.claimHeaderLine.push({
               
                'ChequeNo': CH.ClaimPaymentMethodDetails[i].ChequeNo,
                'PaymentTypeID': 1,
                'PaidAmount': CH.ClaimPaymentMethodDetails[i].PaidAmount,
                'PaidDate': $scope.paymentDate,
                'IsFinal': CH.ClaimPaymentMethodDetails[i].IsFinal
            });
        
        
        
        }


       // alert(angular.toJson($scope.claimHeaderLine));
       // $scope.save(CH);
    };

    $scope.delete = function (CH) {
     //   alert(CH.ClaimPaymentID);
    }





 $scope.cancel = function () {
     window.location.href = "/ClaimPayment/Index"
 }
function getAllClaimPayment() {
     $scope.showLoader = true;
     ClaimRecordingService.getAllClaimPaymentsByClaimID($scope.claimRecording.ClaimRecordingID).then(function (results) {
         // $scope.showLoader = false;
        //alert(" availableClaimPayment " + angular.toJson(results.data));
         if (results.status === true) {
             $scope.availableClaimPayment = results.data;
            
            }
         else {
             $scope.availableClaimPayment = [];
         }
     });
 };
 $scope.refresh = function()
 {
     $scope.getAllClaimPayment();
 }





    /**************************end of clil payemnt*********************/





});
