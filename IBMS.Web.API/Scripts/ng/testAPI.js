'use strict';

var testAPIApp = angular.module('TestAPIApp', []);

testAPIApp.controller('TestAPIController', function ($scope, TestAPIService) {
    $scope.init = function () {
        //Make this false before publishing
        $scope.isTest = false;
    };

    $scope.saveBusinessUnitData = function () {
        TestAPIService.saveBusinessUnitData("Test Business Unit 2", 1015, true, 1).then(function (results) {
            alert(angular.toJson(results));
        });
    };

    $scope.updateBusinessUnitData = function () {
        TestAPIService.updateBusinessUnitData(128, "Test Business Unit Mod", 1015, true, 1).then(function (results) {
            alert(angular.toJson(results));
        });
    };

    $scope.deleteBusinessUnitData = function () {
        TestAPIService.deleteBusinessUnitData(128).then(function (results) {
            alert(angular.toJson(results));
        });
    };

    $scope.getAllBusinessUnits = function () {
        TestAPIService.getAllBusinessUnits().then(function (results) {
            alert(angular.toJson(results));
        });
    };

    $scope.getBusinessUnitByID = function () {
        TestAPIService.getBusinessUnitByID(128).then(function (results) {
            alert(angular.toJson(results));
        });
    };

    /*----------------------------Document---------------------------------*/
    $scope.uploadedDocument = "";

    $scope.getDocument = function (e) {
        $scope.uploadedDocument = e.files[0];
    };

    $scope.saveDocument = function () {
        var formData = new FormData();
        formData.append("insSubClassID", 5);
        formData.append("documentName", 'Test');
        formData.append("uploadedDocument", $scope.uploadedDocument);
        formData.append("description", 'Test Description');
        formData.append("userID", 1);
        //formData.append("isUploaded", 'true');

        var objXhr = new XMLHttpRequest();

        objXhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                alert(angular.toJson(JSON.parse(this.responseText)));
            }
        };

        objXhr.open("POST", "/api/Document/SaveDocument", true);
        objXhr.setRequestHeader("Authorization", "Basic QWRtaW5JQk1TOmlibXNJcyMyMDE4");
        objXhr.send(formData);
    };

    $scope.updateDocument = function () {
        var formData = new FormData();
        formData.append("documentID", 29);
        formData.append("insSubClassID", 5);
        formData.append("documentName", 'Test Mod');
        formData.append("uploadedDocument", "");
        formData.append("description", 'Test Description Mod');
        formData.append("userID", 1);

        var objXhr = new XMLHttpRequest();

        objXhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                alert(angular.toJson(JSON.parse(this.responseText)));
            }
        };

        objXhr.open("POST", "/api/Document/UpdateDocument", true);
        objXhr.setRequestHeader("Authorization", "Basic QWRtaW5JQk1TOmlibXNJcyMyMDE4");
        objXhr.send(formData);
    };

    $scope.deleteDocument = function () {
        TestAPIService.deleteDocumentData(10).then(function (results) {
            alert(angular.toJson(results));
        });
    };
    /*----------------------------Document---------------------------------*/
    $scope.businessUnitIDList = [128, 129];

    $scope.saveIntroducer = function () {
        TestAPIService.saveIntroducerData("Test Introducer", "Test", "Test", "Test", "Test", $scope.businessUnitIDList, 1).then(function (results) {
            alert(angular.toJson(results));
        });
    };

    /*----------------------------Client Request---------------------------------*/
    $scope.clientObj = {};
    $scope.ClientRequestHeader = {};
    $scope.clientProperty = [{ "ClientPropertyName": "Test 1", "BRNo": "Test 0001", "VATNo": "Test 0001" }, { "ClientPropertyName": "Test 2", "BRNo": "Test 0002", "VATNo": "Test 0002" }];
    $scope.clientReqInsSubClassScope = [{ "CommonInsScopeID": 6 }];
    $scope.clientRequestLine = [{ "InsSubClassID": 1, "ClientPropertyDetails": $scope.clientProperty, "ClientRequestInsSubClassScopeDetails": $scope.clientReqInsSubClassScope }];

    $scope.clientObj.AdditionalNote = "";
    $scope.clientObj.BusinessUnitID = 129;
    $scope.clientObj.ClientAddress = "Hambantota";
    $scope.clientObj.ClientName = "Dilan Rajapakse"
    $scope.clientObj.ContactNo = "0785445619";
    $scope.clientObj.DOB = "17/05/2017";
    $scope.clientObj.Email = "dilan@gmail.com";
    $scope.clientObj.FamilyDiscount = ""
    $scope.clientObj.FixedLine = "0112940440";
    $scope.clientObj.HomeCountryID = "1";
    $scope.clientObj.NIC = "954545555555";
    $scope.clientObj.PPID = "";
    $scope.clientObj.ResidentCountryID = "1";

    $scope.ClientRequestHeader.PartnerID = 2;
    $scope.ClientRequestHeader.RequestedDate = "09/05/2018";//dd/MM/yyyy
    $scope.ClientRequestHeader.ClientRequestLineDetails = $scope.clientRequestLine;

    $scope.saveClientRequest = function () {
        TestAPIService.saveClientRequest(false, true, $scope.clientObj, $scope.ClientRequestHeader, 1).then(function (results) {
            alert(angular.toJson(results));
        });
    };
    /*----------------------------Client Request---------------------------------*/

    /*----------------------------Client Request Tr---------------------------------*/
    $scope.BankObj = {};
    $scope.ClientRequestHeader = {};
    $scope.clientProperty = [{ "ClientPropertyName": "Test 1", "BRNo": "Test 0001", "VATNo": "Test 0001" }, { "ClientPropertyName": "Test 2", "BRNo": "Test 0002", "VATNo": "Test 0002" }];
    $scope.clientReqInsSubClassScope = [{ "CommonInsScopeID": 6 }];
    $scope.clientRequestLine = [{"ClientRequestLineID": "0", "InsSubClassID": 1, "ClientPropertyDetails": $scope.clientProperty, "ClientRequestInsSubClassScopeDetails": $scope.clientReqInsSubClassScope }];
    $scope.clientRequestLiness = [{ "MemberName": "Sumith", "MemberDOB": "17/05/2017", "ClientID": "9149", "NIC": "8888888888","ContactNo": "888888888888888"}];
    $scope.clientRequestLines = [{ "MemberName": "Sumith", "MemberDOB": "17/05/2017", "ClientID": "9149", "NIC": "8888888888", "ContactNo": "888888888888888", "GroupMemberDetails": $scope.clientRequestLiness }];
        //, { "MemberName": "Srinath", "MemberDOB": "17/05/2017", "ClientID": "9149" }];
 //   $scope.clientRequestLiness = [{ "PremiumHolder": "test", "Premium": "1000", "LoadingRate": "10", "DeductionRate": "10", "NetPremium": "2000" }];
  //  $scope.clientRequestLines = [{ "PaymentID": 1006, "TotalGrossPremium": 210.00 }, { "PaymentID": 1006, "TotalGrossPremium": 510.00 }];
    //   $scope.clientRequestLine = [];
    $scope.clientObj.AdditionalNote = "";
    $scope.clientObj.BusinessUnitID = 129;
    $scope.clientObj.ClientAddress = "Hambantota";
    $scope.clientObj.ClientName = "Dilan Rajapakse"
    $scope.clientObj.ContactNo = "";
    $scope.clientObj.DOB = "";
    $scope.clientObj.Email = "d";
    //$scope.clientObj.ClientID = 1062;
    $scope.clientObj.FamilyDiscount = 0;
    $scope.clientObj.FixedLine = "";
    $scope.clientObj.HomeCountryID = "3";
    $scope.clientObj.NIC = "9";
    $scope.clientObj.PPID = "";
    $scope.clientObj.ResidentCountryID = "3";
    //$scope.BankObj.BankID = "2";
    //$scope.BankObj.BankRate = 5;
    //$scope.BankObj.DraftNo = "3245";
    //$scope.BankObj.ClientName = "Dilan Rajapakse"
    //$scope.BankObj.ClientID = 1061;
    //$scope.BankObj.ContactNo = "0785445619";
    //$scope.BankObj.DOB = "01/07/2018";
    //$scope.BankObj.Email = "dilan@gmail.com";
    //$scope.BankObj.FamilyDiscount = 56;
    //$scope.clientObj.FixedLine = "0112940440";
    //$scope.clientObj.HomeCountryID = "1";
    //$scope.clientObj.NIC = "954545555555";
    //$scope.clientObj.PPID = "";
    //$scope.clientObj.ResidentCountryID = "1";


    //$scope.ClientRequestHeader.AgentID = 3073;
    $scope.ClientRequestHeader.ClientRequestHeaderID = 3164;
    $scope.ClientRequestHeader.PartnerID = 2;
  //  $scope.clientObj.ClientID = 1062;
  //  $scope.ClientRequestHeader.ClientID = 1062;
  //  $scope.ClientRequestHeader.PaymentID = 1006;
    $scope.ClientRequestHeader.RequestedDate = "30/07/2018";//dd/MM/yyyy
    $scope.ClientRequestHeader.ClientRequestLineDetails = $scope.clientRequestLine;
    $scope.clientObj.FamilyDetails = $scope.clientRequestLines;
    $scope.ClientRequestHeader.PaymentDetails = [];
   // $scope.clientObj.DeductionDetails = $scope.clientRequestLiness;
  //  $scope.ClientRequestHeader.DebitNoteDetails = $scope.clientRequestLines;

    $scope.saveRequest = function () {
        TestAPIService.saveRequest(false, true, $scope.clientObj, $scope.ClientRequestHeader, 19).then(function (results) {
      //  TestAPIService.saveRequest($scope.ClientRequestHeader , 19).then(function (results) {
            alert(angular.toJson(results));
        });
    };
    /*----------------------------Client Request Tr---------------------------------*/

    /*----------------------------User Login---------------------------------*/
    $scope.login = function () {
        TestAPIService.login("Fathima", "456", 129).then(function (results) {
            alert(angular.toJson(results));
        });
    };
    /*----------------------------User Login---------------------------------*/

    /*----------------------------Save Quotation---------------------------------*/
    $scope.QuotationDetailsInsCompanyScope = [{ "ScopeDescription": "Test", "ExcessType": "Test", "ExcessAmount": 25.5 }];
    $scope.QuotationDetailsInsCompanyLine = [{ "InsuranceSubClassID": 1, "SumInsured": 45.5, "QuotationDetailsInsCompanyScopeDetails": $scope.QuotationDetailsInsCompanyScope }];
    $scope.QuotationDetailsInsCompanyHeader = [{ "PremiumIncludingTax": 5.5, "ExcessDescription": "Test", "ExcessAmount": 450.5, "QuotationDetailsInsCompanyLineDetails": $scope.QuotationDetailsInsCompanyLine }];
    $scope.QuotationRequestedInsCompany = [{ "InsuranceCompanyID": 1018, "Status": true, "QuotationDetailsInsCompanyHeaderDetails": $scope.QuotationDetailsInsCompanyHeader }];
    $scope.QuotationLine = [{ "InsuranceClassID": 10, "InsuranceSubClassID": 1, "RequestedInsuranceCompanyDetails": $scope.QuotationRequestedInsCompany }];
    $scope.QuotationHeader = { "ClientRequestHeaderID": 5, "Status": true, "QuotationLineDetails": $scope.QuotationLine };

    $scope.saveQuotation = function () {
        TestAPIService.saveQuotation($scope.QuotationHeader).then(function (results) {
            alert(angular.toJson(results));
        });
    };
    /*----------------------------Save Quotation---------------------------------*/

    /*----------------------------Update Quotation---------------------------------*/
    $scope.quotationHeader = { "QuotationHeaderID": 45, "ClientRequestHeaderID": 68, "ClientID": 1036, "ClientName": "Iranga Pathirana", "PartnerID": 3, "PartnerName": "Agent", "RequestedDate": "20/05/2018", "Status": true, "QuotationStatusCode": "QP", "QuotationStatusCodeDescription": "Quotation Pending", "CreatedDate": "5/20/2018 11:36:34 PM", "ModifiedDate": "", "CreatedBy": 18, "ModifiedBy": 0, "QuotationLineDetails": [{ "InsuranceSubClassID": 15, "InsuranceSubClassDescription": "MERINE OPEN COVER", "RequestedInsuranceCompanyDetails": [{ "InsuranceCompanyID": 1035, "InsuranceCompanyName": "Test Insurance Company 1", "Status": true, "QuotationDetailsInsCompanyHeaderDetails": [{ "PremiumIncludingTax": 150, "ExcessDescription": "Test", "ExcessAmount": 50, "QuotationDetailsInsCompanyLineDetails": [{ "InsuranceSubClassID": 15, "InsuranceSubClassDescription": "MERINE OPEN COVER", "SumInsured": 100, "QuotationDetailsInsCompanyScopeDetails": [{ "ScopeDescription": "Test", "ExcessType": "Test", "ExcessAmount": 100 }] }] }] }, { "InsuranceCompanyID": 1037, "InsuranceCompanyName": "Test Insurance Company 2", "Status": true, "QuotationDetailsInsCompanyHeaderDetails": [{ "PremiumIncludingTax": 200, "ExcessDescription": "Test", "ExcessAmount": 45, "QuotationDetailsInsCompanyLineDetails": [{ "InsuranceSubClassID": 15, "InsuranceSubClassDescription": "MERINE OPEN COVER", "SumInsured": 50, "QuotationDetailsInsCompanyScopeDetails": [{ "ScopeDescription": "Test", "ExcessType": "Test", "ExcessAmount": 50 }] }] }] }] }, { "InsuranceSubClassID": 14, "InsuranceSubClassDescription": "MERINE", "RequestedInsuranceCompanyDetails": [{ "InsuranceCompanyID": 1035, "InsuranceCompanyName": "Test Insurance Company 1", "Status": true, "QuotationDetailsInsCompanyHeaderDetails": [{ "PremiumIncludingTax": 250, "ExcessDescription": "Test", "ExcessAmount": 35, "QuotationDetailsInsCompanyLineDetails": [{ "InsuranceSubClassID": 14, "InsuranceSubClassDescription": "MERINE", "SumInsured": 65, "QuotationDetailsInsCompanyScopeDetails": [{ "ScopeDescription": "Test 1", "ExcessType": "Test", "ExcessAmount": 35 }, { "ScopeDescription": "Test 2", "ExcessType": "Test", "ExcessAmount": 55 }] }] }] }, { "InsuranceCompanyID": 1037, "InsuranceCompanyName": "Test Insurance Company 2", "Status": true, "QuotationDetailsInsCompanyHeaderDetails": [{ "PremiumIncludingTax": 500, "ExcessDescription": "Test", "ExcessAmount": 150, "QuotationDetailsInsCompanyLineDetails": [{ "InsuranceSubClassID": 14, "InsuranceSubClassDescription": "MERINE", "SumInsured": 100, "QuotationDetailsInsCompanyScopeDetails": [{ "ScopeDescription": "Test", "ExcessType": "Test", "ExcessAmount": 135 }] }] }] }] }] };
  //  $scope.quotationHeader = { "QuotationHeaderID": 45, "ClientRequestHeaderID": 68, "ClientID": 1036, "ClientName": "Iranga Pathirana", "PartnerID": 3, "PartnerName": "Agent", "RequestedDate": "20/05/2018", "Status": true, "QuotationStatusCode": "QP", "QuotationStatusCodeDescription": "Quotation Pending", "CreatedDate": "5/20/2018 11:36:34 PM", "ModifiedDate": "", "CreatedBy": 18, "ModifiedBy": 0, "QuotationLineDetails": [{ "InsuranceSubClassID": 15, "InsuranceSubClassDescription": "MERINE OPEN COVER", "RequestedInsuranceCompanyDetails": [{ "InsuranceCompanyID": 1035, "InsuranceCompanyName": "Test Insurance Company 1", "Status": true, "QuotationDetailsInsCompanyHeaderDetails": [{ "PremiumIncludingTax": 0, "ExcessDescription": "", "ExcessAmount": 0, "QuotationDetailsInsCompanyLineDetails": [{}] }] }, { "InsuranceCompanyID": 1037, "InsuranceCompanyName": "Test Insurance Company 2", "Status": true, "QuotationDetailsInsCompanyHeaderDetails": [{ "PremiumIncludingTax": 0, "ExcessDescription": "", "ExcessAmount": 0, "QuotationDetailsInsCompanyLineDetails": [{}] }] }] }] } ;
    $scope.updateQuotation = function () {
        TestAPIService.updateQuotation($scope.quotationHeader).then(function (results) {
            alert(angular.toJson(results));
        });
    };
    /*----------------------------Update Quotation---------------------------------*/

    /*----------------------------Send Email---------------------------------*/
    $scope.sendEmail = function () {
        TestAPIService.sendEmail("Iranga Pathirane", "iranga@perfectbss.com", "Test Email", "<p>Dear Iranga,</p><p>This is a test mail.</p><p>Thanks</p>").then(function (results) {
            alert(angular.toJson(results));
        });
    };
    /*----------------------------Send Email---------------------------------*/

    /*----------------------------Search Client---------------------------------*/
    $scope.searchClients = function () {
        TestAPIService.searchClients(0, 1, 1).then(function (results) {
            alert(angular.toJson(results));
        });
    };
    /*----------------------------Search Client---------------------------------*/

    /*----------------------------Search Premium---------------------------------*/
   // $scope.policyInfoRecObj = [{ "PolicyRenewalHistoryID": 13, "PolicyInfoRecID":4, "IsCancel": true, "IsSent": true, "NotificationDate": "25/08/2018","RenewalDate":"21/02/2018" }]
    $scope.getPremium = function () {
        TestAPIService.getPremium(1068).then(function (results) {
            alert(angular.toJson(results));
        });
    };
    /*----------------------------Search Premium---------------------------------*/

    /*------------------------Policy Information Recording----------------------*/
    $scope.policyCommissionPayment = [{ "CommisionTypeID": 2, "Amount": 2500, "ComStructLineID": 1040, "Precentage": 10, "CommisionValue": 200 }, { "CommisionTypeID": 3, "Amount": 3500, "ComStructLineID": 1047, "Precentage": 15, "CommisionValue": 300 }];
    $scope.policyInfoChargeList = [{ "ChargeTypeID": 1, "Amount": 200, IsCR: true }, { "ChargeTypeID": 14, "Amount": 300, IsCR: true }];
    $scope.policyInfoRecording = [{ "PolicyNumber": 38, "QuotationHeaderID": 1, "QuotationDetailsInsCompanyLineID": 3, "SumAssured": 250, "SumAssuredCurrencyTypeID": 1, "PremiumIncludingTax": 350, "PremiumIncludingTaxCurrencyTypeID": 1, "PeriodOfCoverFromDate": "26/02/2018", "PeriodOfCoverToDate": "26/03/2018", CommisssionStructureHeaderID: "1003", GrossPremium: 61020, NonCommissionPremium: 20000, TotalCommission: 700, TransactiontypeID: "1034", "PolicyCommissionPaymentDetails": $scope.policyCommissionPayment, "policyInfoChargeList": $scope.policyInfoChargeList }];

    $scope.savePolicyInformationRecording = function () {
        TestAPIService.savePolicyInfoRecording(2053, $scope.policyInfoRecording, 3).then(function (results) {
            alert(angular.toJson(results));
        });
    };
    /*------------------------Policy Information Recording----------------------*/

    /*---------------------------Claim Recording--------------------------------*/
  //  $scope.claimHistory = { "Description": "Test Description", "RecordingDate": "02/26/2018", "Reason": "Test Reason" };
  //  $scope.pendingDocList = [{ "DocumentID": 13 }, { "DocumentID": 14 }];
    $scope.claimHistory = {};
    $scope.pendingDocList = [{"DocumentID":"0"}];
    $scope.claimRecording = { "PolicyInfoRecID": 1, "ClaimNo": "Test001", "DateOfLoss": "02/02/2018", "DateOfIntimation": "03/02/2018", "DamageDescription": "Test Description", "AmountClaimed": 2500, "AmountPaid": 2000, "WithdrawReason": "Test Withdraw Reason", "IsOpened": true, "IsWithdrawn": true, "ClaimDocumentsEmailedDate": "03/06/2018", "ClaimDocumentsReceivedDate": "03/06/2018", "OriginalDocumentscourieredDate": "03/06/2018", "PaymentAdviceEmailedDate": "03/06/2018", "PaymentAdviceReceviedDate": "03/06/2018", "DischargedDate": "03/06/2018", "ClaimRecHistoryDetails": $scope.claimHistory, "ClaimRecPendingDocDetails": $scope.pendingDocList };

    $scope.saveClaimRecording = function () {
        TestAPIService.saveClaimRecording($scope.claimRecording, 1).then(function (results) {
            alert(angular.toJson(results));
        });
    };
    /*---------------------------Claim Recording--------------------------------*/

    /*---------------------------Claim Payment--------------------------------*/
    $scope.claimPaymentMethodList = [{ "ChequeNo": "12345", "PaymentTypeID": 1, "PaidAmount": 2000, "PaidDate": "02/20/2018", "IsFinal": false }, { "ChequeNo": "12346", "PaymentTypeID": 1, "PaidAmount": 2000, "PaidDate": "02/22/2018", "IsFinal": true }];
    $scope.claimPayment = { "ClaimPaymentID":82,"ClaimRecordingID": 2, "ClaimAmount": 4000, "Notes": "Test Note", "ClaimPaymentMethodDetails": $scope.claimPaymentMethodList };

    $scope.saveClaimPayment = function () {
        TestAPIService.saveClaimPayment($scope.claimPayment, 1).then(function (results) {
            alert(angular.toJson(results));
        });
    };
    /*---------------------------Claim Payment--------------------------------*/

    /*------------------------------------User----------------------------------------*/
    $scope.updateUser = function () {
        TestAPIService.updateUser(18, "Test", "Test", 1006, 18).then(function (results) {
            alert(angular.toJson(results));
        });
    };
    /*------------------------------------User----------------------------------------*/

    /*--------------------------------------Update Payment---------------------------------------*/
    $scope.paymentObj = {"PaymentID":"10041" ,"ClientID": 8153,"Cuurency": "LKR", "PaymentAmount": "350", "DebitNoteList": [{ "TotalNonCommissionPremium": "350", "TotalGrossPremium": "2000","PolicyInfoRecID": "2012",  "PolicyInfoPaymentLists": [{"BankDetailID":"7030","BankID":"1005","DraftNo":"2464","PaymentMethodID":"2","BankAmount":"20000", "PolicyInfoRecID": "2012", "PolicyInfoChargeList": [] }] }] };


   // $scope.paymentObj = { "PaymentID": 2, "ClientID": 1004, "ClientName": "Test Client", "PaymentAmount": 350, "CreatedBy": 1, "CreatedDate": "3/16/2018 4:07:27 PM", "ModifiedBy": 0, "ModifiedDate": "", "DebitNoteList": [{ "DebitNoteID": 2, "TotalNonCommissionPremium": 350, "TotalGrossPremium": 2000, "CreatedBy": 1, "CreatedDate": "3/16/2018 4:07:27 PM", "ModifiedBy": 0, "ModifiedDate": "", "PolicyInfoPaymentList": [{ "PolicyInfoPaymentID": 2, "PolicyInfoRecID": 1, "PolicyInfoRecObj": { "PolicyInfoRecID": 1, "PolicyNumber": "38", "QuotationHeaderID": 1, "QuotationDetailsInsCompanyLineID": 3, "SumAssured": 250, "SumAssuredCurrencyTypeID": 1, "SumAssuredCurrencyCode": "LKR", "PremiumIncludingTax": 350, "PremiumIncludingTaxCurrencyTypeID": 1, "PremiumIncludingTaxCurrencyCode": "LKR", "PeriodOfCoverFromDate": "02/26/2018", "PeriodOfCoverToDate": "03/26/2018", "OtherExcessDescription": null, "OtherExcessAmount": 0, "TaxInvoiceNumber": null, "FileNumber": null, "IsActive": true, "CreatedBy": 1, "CreatedDate": "2/26/2018 2:05:29 PM", "ModifiedBy": 0, "ModifiedDate": "", "PolicyCommissionPaymentDetails": [{ "PolicyCommisionPaymentID": 1, "PolicyInfoRecID": 1, "CommisionTypeID": 2, "CommissionTypeName": "Test Commission 002", "CommisionValue": 2500, "ComStructLineID": 0, "RateValue": 0, "PartnerID": 0, "PartnerName": null, "CreatedBy": 1, "CreatedDate": "2/26/2018 2:05:30 PM", "ModifiedBy": 0, "ModifiedDate": "" }, { "PolicyCommisionPaymentID": 2, "PolicyInfoRecID": 1, "CommisionTypeID": 3, "CommissionTypeName": "Test Commission 003", "CommisionValue": 3500, "ComStructLineID": 0, "RateValue": 0, "PartnerID": 0, "PartnerName": null, "CreatedBy": 1, "CreatedDate": "2/26/2018 2:05:30 PM", "ModifiedBy": 0, "ModifiedDate": "" }] },     "NonCommissionPremium": 350, "GrossPremium": 2000, "CreatedBy": 1, "CreatedDate": "3/16/2018 4:07:27 PM", "ModifiedBy": 0, "ModifiedDate": "", "PolicyInfoChargeList": [{ "PolicyInfoChargeID": 2, "ChargeTypeID": 1, "ChargeTypeName": "SRCC", "Amount": 1, "IsCR": true, "CreatedBy": 1, "CreatedDate": "3/16/2018 4:07:27 PM", "ModifiedBy": 0, "ModifiedDate": "" }] }] }] };

    $scope.updatePaymentDetails = function () {
        TestAPIService.updatePayment($scope.paymentObj, 18).then(function (results) {
            alert(angular.toJson(results));
        });
    };
    /*--------------------------------------Update Payment---------------------------------------*/
});

testAPIApp.factory('TestAPIService', function ($http) {
    var config = {
        //headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': 'Basic ' + btoa('AdminIBMS:ibmsIs#2018') }
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': 'Basic QWRtaW5JQk1TOmlibXNJcyMyMDE4' }
    };

    var saveBusinessUnitData = function (businessUnit, companyID, isActive, userID) {
        var params = $.param({ "businessUnit": businessUnit, "companyID": companyID, "isActive": isActive, "userID": userID });
        return $http.post('/api/BusinessUnit/SaveBusinessUnit', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var updateBusinessUnitData = function (businessUnitID, businessUnit, companyID, isActive, userID) {
        var params = $.param({ "businessUnitID": businessUnitID, "businessUnit": businessUnit, "companyID": companyID, "isActive": isActive, "userID": userID });
        return $http.post('/api/BusinessUnit/UpdateBusinessUnit', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var deleteBusinessUnitData = function (businessUnitID) {
        var params = $.param({ "businessUnitID": businessUnitID });
        return $http.post('/api/BusinessUnit/DeleteBusinessUnit', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getAllBusinessUnits = function () {
        return $http.post('/api/BusinessUnit/GetAllBusinessUnits', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    //var getAllBusinessUnits = function () {
    //    return $http.post('http://192.168.1.5:9810/api/BusinessUnit/GetAllBusinessUnits', null, config).then(function (results) {
    //        return results.data;
    //    }, function (data) {
    //        // Handle error here
    //    })
    //};

    var getBusinessUnitByID = function (businessUnitID) {
        var params = $.param({ "businessUnitID": businessUnitID });
        return $http.post('/api/BusinessUnit/GetBusinessUnitByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var deleteDocumentData = function (documentID) {
        var params = $.param({ "documentID": documentID });
        return $http.post('/api/Document/DeleteDocument', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var saveIntroducerData = function (introducerName, description, address1, address2, address3, businessUnitIDList, UserID) {
        var params = $.param({ "IntroducerName": introducerName, "Description": description, "Address1": address1, "Address2": address2, "Address3": address3, "BusinessUnitIDList": businessUnitIDList, "UserID": UserID });
        return $http.post('/api/Introducer/SaveIntroducer', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var saveClientRequest = function (isClientUpdated, isClientAdded, clientObj, clientRequestHeaderObj, userID) {
        var params = $.param({ "IsClientUpdated": isClientUpdated, "IsClientAdded": isClientAdded, "ClientObj": clientObj, "ClientRequestHeaderObj": clientRequestHeaderObj, "UserID": userID });
        return $http.post('api/ClientRequest/SaveClientRequest', params, config).then(function (results) {
            return results.data;
        }, function (data) {

        })
    };

    var saveRequest = function (isClientUpdated, isClientAdded, clientObj, clientRequestHeaderObj, userID) {
        var params = $.param({ "IsClientUpdated": isClientUpdated, "IsClientAdded": isClientAdded, "ClientObj": clientObj, "ClientRequestHeaderObj": clientRequestHeaderObj, "UserID": userID });
  //  var saveRequest = function (clientRequestHeaderObj, userID) {
       // var params = $.param({ "ClientRequestHeaderObj": clientRequestHeaderObj, "UserID": userID });
        return $http.post('api/ClientRequest/SaveClientRequest', params, config).then(function (results) {
            return results.data;
        }, function (data) {

        })
    };

    var login = function (loginName, password, businessUnitID) {
        var params = $.param({ "loginName": loginName, "password": password, "businessUnitID": businessUnitID });
        return $http.post('/api/Login/AuthenticateUser', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var saveQuotation = function (quotation) {
        var params = $.param({ "QuotationHeaderObj": quotation, "UserID": 1 });
        return $http.post('/api/Quotation/SaveQuotation', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var updateQuotation = function (quotation) {
        var params = $.param({ "QuotationHeaderObj": quotation, "UserID": 1 });
        return $http.post('/api/Quotation/UpdateQuotation', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var sendEmail = function (userName, emailAddress, emailHeader, emailContent) {
        var params = $.param({ "UserName": userName, "EmailAddress": emailAddress, "EmailHeader": emailHeader, "EmailContent": emailContent });
        return $http.post('/api/Email/SendGeneralEmail', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var searchClients = function (businessUnitID, homeCountryID, residentCountryID) {
        var params = $.param({ "businessUnitID": businessUnitID, "homeCountryID": homeCountryID, "residentCountryID": residentCountryID });
        return $http.post('/api/ClientRequest/SearchClients', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var getPremium = function (businessUnitID) {
        var params = $.param({ "BusinessUnitID": businessUnitID });
        return $http.post('/api/Claim/GetAllClaimRecordingsByBUID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var savePolicyInfoRecording = function (quotationHeaderID, policyInfoRecList, userID) {
        var params = $.param({ "QuotationHeaderID": 1, "PolicyInfoRecList": policyInfoRecList, "UserID": userID });
        return $http.post('/api/Policy/SavePolicyInformationRecording', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var saveClaimRecording = function (claimRecording, userID) {
        var params = $.param({ "ClaimRecordingVM": claimRecording, "UserID": userID });
        return $http.post('/api/Claim/SaveClaimRecording', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var saveClaimPayment = function (claimPayment, userID) {
        var params = $.param({ "ClaimPaymentVM": claimPayment, "UserID": userID });
        return $http.post('/api/Claim/UpdateClaimPayment', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            UpdateClaimPayment
            // Handle error here
        })
    };

    var updateUser = function (userID, userName, loginName, designationID, loggedUserID) {
        var params = $.param({ "UserID": userID, "UserName": userName, "LoginName": loginName, "DesignationID": designationID, "LoggedUserID": loggedUserID });
        return $http.post('/api/User/UpdateUser', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        })
    };

    var updatePayment = function (paymentObj, userID) {
        var params = $.param({ "PaymentObj": paymentObj, "UserID": userID });
        return $http.post('/api/Payment/SavePayment', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    return {
        saveBusinessUnitData: saveBusinessUnitData,
        updateBusinessUnitData: updateBusinessUnitData,
        deleteBusinessUnitData: deleteBusinessUnitData,
        getAllBusinessUnits: getAllBusinessUnits,
        getBusinessUnitByID: getBusinessUnitByID,
        deleteDocumentData: deleteDocumentData,
        saveIntroducerData: saveIntroducerData,
        saveClientRequest: saveClientRequest,
        saveRequest:saveRequest,
        login: login,
        saveQuotation: saveQuotation,
        updateQuotation: updateQuotation,
        sendEmail: sendEmail,
        searchClients: searchClients,
        savePolicyInfoRecording: savePolicyInfoRecording,
        saveClaimRecording: saveClaimRecording,
        saveClaimPayment: saveClaimPayment,
        updateUser: updateUser,
        updatePayment: updatePayment,
        getPremium: getPremium
    };
});