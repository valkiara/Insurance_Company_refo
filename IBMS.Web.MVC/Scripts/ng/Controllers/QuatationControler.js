'use strict';
ibmsApp.service("EmailHelperService", function () {
    this.user = { name: 'Suresh Dasari', Location: 'Chennai' }
    this.designation = 'Team Leader'

    this.Customer = {};
    this.QuatationHeader = {};
    this.Subject = "";

});
ibmsApp.controller("QuatationControler", function ($rootScope, $scope, $http, QuatationService, AuthService, $window, $filter, $log, EmailHelperService, Excel,$timeout) {


    $scope.empname = EmailHelperService.user.name;
    EmailHelperService.user.Location = "Hyderabad";

    $scope.data = {};
    $scope.data.backgroundCol = "#ffffff";
   // $rootScope.message = "fathima";

    $scope.loadTransactionTypeDetails();



    $scope.statuses = [
	{
	    "QuotationStatusID": "1",
	    "QuotationStatusCode": "QNC",
	    "Description": "Quotation Not Created"
	},
	{
	    "QuotationStatusID": "2",
	    "QuotationStatusCode": "QP",
	    "Description": "Quotation Pending"
	},
	{
	    "QuotationStatusID": "3",
	    "QuotationStatusCode": "QR",
	    "Description": "Quotation Ready"
	},
	{
	    "QuotationStatusID": "4",
	    "QuotationStatusCode": "NA",
	    "Description": "Not Approved"
	},
	{
	    "QuotationStatusID": "5",
	    "QuotationStatusCode": "CA",
	    "Description": "Customer Approved"
	},
	{
	    "QuotationStatusID": "7",
	    "QuotationStatusCode": "TCNI",
	    "Description": "Temporary Cover Note Issued"
	},
	{
	    "QuotationStatusID": ""
	}
    ];



    $scope.isDisabled = true;
    $scope.ClientRequestHeader = {};
    $scope.requestList = [];
    $scope.AddedInsClassesScopes = [];
    $scope.clientProperty = [];
    $scope.AddedInscompanies = [];
    $scope.AddedInscompaniesSorted = [];
    $scope.QuotationDetailsInsCompanyHeaderDetails = [];
    $scope.selectedSubClassIDFromInsCompanyList = [];
    $scope.quotationHeader = [];
    $scope.quotationHeaderID = null;
    $scope.QuatedDate = $filter('date')(new Date(), 'MM/dd/yyyy');
    $scope.PolicyDate = $filter('date')(new Date(), 'MM/dd/yyyy');
    ClickEvent("tab-second","tab-first", "second" , "first");
 
    getCurrentUser();
    
    $scope.ClickEvent=function(id1,id2,idP1,idp2)
    {
     //   alert("dfg");
    
        ClickEvent(id1, id2, idP1, idp2);
    
    }
    $scope.editMyReq = function (qHeader) {
        $scope.HeaderObj = qHeader;

        $scope.ClientRequestHeader = {};
        $scope.requestList = [];
        $scope.AddedInsClassesScopes = [];
        $scope.clientProperty = [];
        $scope.ClickEvent("tab-first","tab-second",  "first", "second");
        $("#update").css("display", "block");
        $("#add").css("display", "none");
        $("#tab-first").css("display", "block");

        $scope.quotationHeaderID = qHeader.QuotationHeaderID;
        ReloadClientDetails(qHeader.myReqList);
        ReloadClientHeader(qHeader.myReqList);
        ReloadRequestList(qHeader.myReqList);
        ReLoadAddedInsCompany(qHeader);
        $scope.statuseee = qHeader.QuotationStatusCode;
        //alert("qHeader" + angular.toJson($scope.HeaderObj));
    }
    $scope.filterAddedScope = function (addedScoped) {
        //$scope.AddedInsClassesScopes = [];

        FilterScopeBySubClassID(addedScoped.insSubClassID);
        $("#addedScopeList").modal("show");

    }
    $scope.filterAddedProperties = function (propery) {
        $scope.AddedPropertiesInsClass = [];

        //alert(propery.insSubClassID);
        FilterclientPropertiesBySubClassID(propery.insSubClassID);

        $("#addedPropList").modal("show");

    }
    $scope.AddBulkInsCompanies = function ()
    {
        $scope.foundReqList=[];
        $scope.foundReqList = FilterReqInsClasses();
        // alert(angular.toJson(foundReqList));
        if ($scope.foundReqList.length > 0) {
            $("#inscompanyies").modal("show");
        }
        
        else
        {
            noty({ text: 'Please Select At Least One Request List', layout: 'center', type: 'error' });
        
        }

    }

    $scope.loadTransactionTypeDetails = function () {
        QuatationService.loadTransactionTypeDetails().then(function (results) {
            if (results.status === true) {
                for (var i = 0; i < results.data.length; i++) {
                    $scope.availableTransaction.push({ value: results.data[i].TransactionTypeID, text: results.data[i].Description });
                }
            }
            else {
                $scope.availableTransaction = [];
            }
        });
    };
    $scope.AddInsCompanies = function (reqList)
    {
        //alert(reqList);
        $scope.foundReqList=[];
        reqList.isChecked = true;
        $scope.foundReqList = FilterReqInsClasses();
      
        if ($scope.foundReqList.length > 0)
        {
            $("#inscompanyies").modal("show");
        }
        
        else
        {
            noty({ text: 'Please Select At Least One Request List', layout: 'center', type: 'error' });
        
        }

    }
    $scope.AddCompToList = function () {

        //  alert(InsuranceCompies);
        PopulateAddedInsCompanyList();
        $("#inscompanyies").modal("hide");


    }
    $scope.RemoveAddedInsCompany = function (item) {
  
        RemoveAddedInscompanyList(item);
    }
    $scope.Save = function ()
    {  
        // alert("");
        Savequatation();
    }
    $scope.updateScopefinal = function (inscompany)
    {
        var found = $filter('filter')($scope.AddedInsClassesScopes, { "InsuranceSubClassID": inscompany.insSubClassID }, true);
        var QuotationDetailsInsCompanyHeaderDetails = $filter('filter')($scope.QuotationDetailsInsCompanyHeaderDetails, { "insSubClassID": inscompany.insSubClassID, "InsuranceCompanyID": inscompany.InsuranceCompanyID }, true);
      

        for (var i = 0; i < QuotationDetailsInsCompanyHeaderDetails.length; i++)
        {
            $scope.PrimiumInTax=QuotationDetailsInsCompanyHeaderDetails[i].PremiumIncludingTax;
            $scope.OtherExcessDescription = QuotationDetailsInsCompanyHeaderDetails[i].ExcessDescription;
            $scope.OtherExcesssAmount = QuotationDetailsInsCompanyHeaderDetails[i].ExcessAmount;
            //alert("PrimiumInTax " + $scope.PrimiumInTax);
          //  alert("OtherExcessDescription " + $scope.OtherExcessDescription);
             //alert("OtherExcesssAmount" + $scope.OtherExcesssAmount);
        }

        //$scope.QuotationDetailsInsCompanyHeaderDetails
        $scope.selectedSubClassIDFromInsCompany = [];
 
        FilterScopeBySubClassIDFromInsCompany(inscompany.insSubClassID);

      
        $scope.selectedSubClassIDFromInsCompany.push({
            "InsuranceCompanyID":inscompany.InsuranceCompanyID,
            "InsuranceSubClassID": inscompany.insSubClassID,
            "InsuranceCompanyName":inscompany.InsuranceCompanyName,
            "InsClass": inscompany.InsClass,
            "InsSubClass": inscompany.InsSubClass,
            "inslassID": inscompany.inslassID,
            "SumInsured":inscompany.SumInsured,
            "QuotationDetailsInsCompanyScopeDetails":$scope.InsSubClassScopeByInsCompany
        
        });
        
        
     
    }
    $scope.updateQuatationDetails = function ()
    {
         
        //  selectedSubClassIDFromInsCompanyList.push($scope.selectedSubClassIDFromInsCompany);

        $scope.selectedSubClassIDFromInsCompany[0].QuotationDetailsInsCompanyScopeDetails = $scope.InsSubClassScopeByInsCompany;


        $scope.QuotationDetailsInsCompanyHeaderDetails.push({
            "InsuranceCompanyID": $scope.selectedSubClassIDFromInsCompany[0].InsuranceCompanyID,
            "PremiumIncludingTax": $scope.PrimiumInTax,
            "ExcessDescription": $scope.OtherExcessDescription,
            "ExcessAmount": $scope.OtherExcesssAmount,
            "QuotationDetailsInsCompanyLineDetails": $scope.selectedSubClassIDFromInsCompany

        });

        noty({ text: 'Succesfully Update Item ', layout: 'topRight', type: 'success' });

    }
    $scope.TemcoverNote = function (inscompany)
    {
        LoadCoverNotesById();
        $("#coverNote").modal("show");
        $scope.coverNote = {};

      
        $scope.coverNote.IssuedDate = $filter('date')(new Date(), 'MM/dd/yyyy');
        $scope.coverNoteNo = "TCN-" + $scope.quotationHeaderID;
        $scope.coverNote.QuotationHeaderID = $scope.quotationHeaderID;
        $scope.coverNote.inscompany = inscompany;
        $scope.coverNote.InsuranceSubClassID = inscompany.insSubClassID;
        $scope.coverNote.CoverNoteNo = "TCN-" + $scope.HeaderObj.quotationHeaderID;
        $scope.coverNote.InsuranceCompanyName = inscompany.InsuranceCompanyName;
        $scope.coverNote.CreatedBy = 1;
        $scope.coverNote.ToDate = $scope.coverNote.IssuedDate;//$("#dp-4").val();
        $scope.coverNote.FromDate = $scope.coverNote.IssuedDate;// $("#dp-3").val();
      //  alert($scope.coverNote.ToDate);
      //  alert($scope.coverNote.FromDate);
    }
    $scope.AddCovernote = function ()
    {
    
        //alert($scope.coverNote.ToDate);
       // alert($scope.coverNote.FromDate);
        //alert("coverNote" + angular.toJson($scope.coverNote));
        QuatationService.saveTCN($scope.coverNote).then(function (results) {

            // alert(angular.toJson(results));
             notyFy(results.message);
            $scope.coverNote = [];
            //alert(angular.toJson(results));


        });
        

    }
    $scope.SetPolicy = function ()
    {
        $scope.PolicyNumber = "POLI"+$scope.quotationHeaderID;
        $scope.updateQuatationDetails();
        createPolicy();



    }
    $scope.UpdateStatus = function ()
    {
        SaveQuatationStatus();

    }
    $scope.requestByEmail = function (tableid,subject)
    {
       // $("#").modal();
       $("#emailModel").modal("show");
       EmailHelperService.Customer = $scope.clientObj;
       EmailHelperService.Subject = subject;
       EmailHelperService.QuatationHeader = $scope.HeaderObj;
       $scope.exportToExcel(tableid);
       //$scope.$broadcast('drawPieChart', {});
       $scope.$broadcast('SendCustomer', { message: "Hello" });

    }
    $scope.RequestAllInsCompany = function (tableid, subject) {
        // $("#").modal();
        $("#emailModel").modal("show");
        $scope.subject = "Quatation Details";
        EmailHelperService.Customer = subject;
        EmailHelperService.Subject = $scope.subject;
        EmailHelperService.QuatationHeader = subject;
        $scope.exportToExcel(tableid);
        //$scope.$broadcast('drawPieChart', {});
        $scope.$broadcast('SendCompany', { message: "Hello" });

    }




    $scope.exportToExcel = function (tableid)
    {
        //alert(tableid);
        var exportHref = Excel.tableToExcel(tableid, 'sheet name');
        $timeout(function () { location.href = exportHref; }, 100);
    }



    function getCurrentUser() {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
            getAllBusinessUnits();
            //alert(angular.toJson($scope.currentUser));
            loadMyRequestList();
            getCompany();
            getAllInsClass();
            getAllClients();
            getAllInsuranceCompanny();
            //var currentUser = $scope.currentUser;
        });
    }
    function ClickEvent(id1, id2, idP1, idp2)
    {

        $("#" + id2).removeClass('tab-pane active');
        $("#" + id2).addClass('tab-pane');
        $("#" + id1).addClass('tab-pane active');


        $("#" + idp2).removeClass('active');

        $("#" + idP1).addClass('active');

    }
    function createPolicy()
    {

        $scope.policyCommissionPayment = [];
       // $scope.policyCommissionPayment = [{ "CommisionTypeID": 2, "CommisionValue": 2500 }, { "CommisionTypeID": 3, "CommisionValue": 3500 }];
      //  $scope.policyInfoRecording = [{ "PolicyNumber": 38, "QuotationHeaderID": 1, "QuotationDetailsInsCompanyLineID": 3, "SumAssured": 250, "SumAssuredCurrencyTypeID": 1, "PremiumIncludingTax": 350, "PremiumIncludingTaxCurrencyTypeID": 1, "PeriodOfCoverFromDate": "02/26/2018", "PeriodOfCoverToDate": "03/26/2018", "PolicyCommissionPaymentDetails": $scope.policyCommissionPayment }];
        $scope.policyInfoRecording = [{
            "PolicyNumber": $scope.PolicyNumber,
            "QuotationHeaderID": $scope.quotationHeaderID,
            "QuotationDetailsInsCompanyLineID":$scope.QuotationDetailsInsCompanyLineID,
            "SumAssured": $scope.selectedSubClassIDFromInsCompany.SumInsured,
            "SumAssuredCurrencyTypeID": 1,
            "PremiumIncludingTax": $scope.PrimiumInTax ,
            "PremiumIncludingTaxCurrencyTypeID": 1,
            "PeriodOfCoverFromDate": $scope.PolicyDate,
            "PeriodOfCoverToDate": $scope.PolicyDate,
            "PolicyCommissionPaymentDetails": $scope.policyCommissionPayment
        }];

      //  alert(angular.toJson($scope.policyInfoRecording));
            QuatationService.savePolicyInfoRecording(1, $scope.policyInfoRecording, 1).then(function (results) {
                //alert(angular.toJson(results));
                noty({ text: results.message, layout: 'topRight', type: 'success' });

            });
    



    }
    function LoadCoverNotesById()
    {
        QuatationService.getAllCoverNotes().then(function (results) {

            //alert(angular.toJson(results));
            $scope.coverNotes = results.data;
            //alert(angular.toJson(results));


        });



        


    }
    function ReLoadAddedInsCompany(quotationHeader)
    {
        var quatationLine =$scope.AddedInscompanies = [];
        var quatationLine = quotationHeader.QuotationLineDetails;
        for (var i = 0; i < quatationLine.length; i++)
        {
            var inslassID = quatationLine[i].InsuranceClassID;
            var insSubClassID = quatationLine[i].InsuranceSubClassID;
            var InsClass = quatationLine[i].InsuranceCode;
            var InsSubClass = quatationLine[i].InsuranceSubClassDescription;

            var RequestedInsuranceCompanyDetails = [];
            var RequestedInsuranceCompanyDetails = quatationLine[i].RequestedInsuranceCompanyDetails;
            for (var j = 0; j < RequestedInsuranceCompanyDetails.length; j++)
            {
                var InsuranceCompanyName = RequestedInsuranceCompanyDetails[j].InsuranceCompanyName;
                var InsuranceCompanyID = RequestedInsuranceCompanyDetails[j].InsuranceCompanyID;
                var QuotationDetailsInsCompanyHeaderDetails = [];
                var QuotationDetailsInsCompanyHeaderDetails = RequestedInsuranceCompanyDetails[j].QuotationDetailsInsCompanyHeaderDetails;

               // alert("f hjhjh" + angular.toJson(QuotationDetailsInsCompanyHeaderDetails));
//

                        $scope.AddedInscompanies.push({
                            "inslassID": inslassID,
                            "insSubClassID": insSubClassID,
                            "InsClass": InsClass,
                            "InsSubClass": InsSubClass,
                            "InsuranceCompanyName": InsuranceCompanyName,
                            "InsuranceCompanyID": InsuranceCompanyID,
                            "isChecked": true,
                            "SumInsured": "0.00"
                        });

                        for (var k = 0; k < QuotationDetailsInsCompanyHeaderDetails.length; k++)
                        {


                            var QuotationDetailsInsCompanyLineDetails = [];
                            var QuotationDetailsInsCompanyLineDetails = QuotationDetailsInsCompanyHeaderDetails[k].QuotationDetailsInsCompanyLineDetails;
                           // alert(QuotationDetailsInsCompanyHeaderDetails[k].PremiumIncludingTax);
                          //  alert(QuotationDetailsInsCompanyHeaderDetails[k].ExcessDescription);
                          //  alert(QuotationDetailsInsCompanyHeaderDetails[k].ExcessAmount);
                            $scope.QuotationDetailsInsCompanyHeaderDetails.push({
                                "inslassID": inslassID,
                                "insSubClassID": insSubClassID,
                                "InsuranceCompanyID": InsuranceCompanyID,
                                "PremiumIncludingTax": QuotationDetailsInsCompanyHeaderDetails[k].PremiumIncludingTax,
                                "ExcessDescription": QuotationDetailsInsCompanyHeaderDetails[k].ExcessDescription,
                                "ExcessAmount": QuotationDetailsInsCompanyHeaderDetails[k].ExcessAmount,
                                "QuotationDetailsInsCompanyLineDetails": QuotationDetailsInsCompanyLineDetails

                            });


                           




                    

                    for (var l = 0; l < QuotationDetailsInsCompanyLineDetails.length;l++)
                    {
                        $scope.QuotationDetailsInsCompanyLineID=QuotationDetailsInsCompanyLineDetails[l].QuotationDetailsInsCompanyLineID;
                        var QuotationDetailsInsCompanyScopeDetails = [];
                        var QuotationDetailsInsCompanyScopeDetails = QuotationDetailsInsCompanyLineDetails[l].QuotationDetailsInsCompanyScopeDetails;
                        var sumInsured = QuotationDetailsInsCompanyLineDetails[l].SumInsured;
                        var inslassID = QuotationDetailsInsCompanyLineDetails[l].InsuranceClassID;
                        var insSubClassID = QuotationDetailsInsCompanyLineDetails[l].InsuranceSubClassID;
                        var InsClass = QuotationDetailsInsCompanyLineDetails[l].InsuranceCode;
                        var InsSubClass = QuotationDetailsInsCompanyLineDetails[l].InsuranceSubClassDescription;
                        $scope.AddedInscompanies[i].SumInsured = sumInsured;
                        $scope.QuotationDetailsInsCompanyScopeDetails = QuotationDetailsInsCompanyScopeDetails;
                        for (var m = 0; m < QuotationDetailsInsCompanyScopeDetails.length; m++)
                        {

                            CheckIsExistAddedInscompaniesInsClassScope( insSubClassID, InsClass,QuotationDetailsInsCompanyScopeDetails[m]);

                        }
                      

                    }
                }


 


                
            }





        }

      //  alert("f " + angular.toJson($scope.AddedInscompanies));
    
    
    
    
    }
    function CheckIsExistAddedInscompaniesInsClassScope( insSubClassID, InsClass, AddedInsClassesScopes)
    {
        //alert("AddedInsClassesScopes " + angular.toJson(AddedInsClassesScopes));
        //alert("f " + angular.toJson($scope.AddedInsClassesScopes));

        var found = $filter('filter')($scope.AddedInsClassesScopes,
            { "CommonInsScopeID": AddedInsClassesScopes.QuotationDetailsInsCompanyScopeID.toString() }, false);
        //alert("found.length " + found.length);
    
        if (found.length>0)
        {
            for(var m=0;m<found.length;m++)
            {
                var index = $scope.AddedInsClassesScopes.indexOf(found[m]);
                if (index >= 0)
                {

                $scope.AddedInsClassesScopes.splice(index, 1);
                }
            }

            $scope.AddedInsClassesScopes.push({
                "ClientRequestInsSubClassScopeID": AddedInsClassesScopes.QuotationDetailsInsCompanyScopeID,
                "CommonInsScopeID": AddedInsClassesScopes.QuotationDetailsInsCompanyScopeID,
                "InsuranceSubClassID": insSubClassID,
                "InsuranceClassCode": InsClass,
                "Description": AddedInsClassesScopes.ScopeDescription,
                "isChecked": AddedInsClassesScopes.isChecked,
                "ExcessType": AddedInsClassesScopes.ExcessType,
                "ExcessAmount": AddedInsClassesScopes.ExcessAmount
            });

           // alert("AddedInsClassesScopes.ExcessAmount " + AddedInsClassesScopes.ExcessAmount);
           // alert("AddedInsClassesScopes.ExcessType " + AddedInsClassesScopes.ExcessType);

        }


    
    

    
    
    
    
    }
    function getAllQuotationHeaders() {
        // $scope.MyReqLists = results.data;
       // alert("gfyyy" + angular.toJson($scope.MyReqLists));


       // alert("foundhhhhhhhhhh " + angular.toJson($scope.MyReqLists));
        QuatationService.getAllQuotationHeaders().then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
               
                $scope.quotationHeader = results.data;
                for (var i = 0; i < $scope.quotationHeader.length;i++)
                {
                    var ClientRequestHeaderID = $scope.quotationHeader[i].ClientRequestHeaderID;
                    //alert(ClientRequestHeaderID);
                    var found = $filter('filter')($scope.MyReqLists, { "ClientRequestHeaderID": ClientRequestHeaderID }, true);
                    $scope.quotationHeader[i].myReqList = found[0];
                   
                   // alert("found " + angular.toJson($scope.quotationHeader[i].QuotationStatusCode));
                   
                
                }
               
               
               //alert(angular.toJson($scope.quotationHeader));
            }
           
        });




    };
    function getCompany() {
        QuatationService.getAvailableCompany().then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.Company = results.data;
                getAllBusinessUnits();
                //   alert(angular.toJson(results.data));
            }
            else {
                $scope.Company = [];
            }
        });
    };
    function getAllBusinessUnits() {
        //alert("hh");
        $scope.BUnits = [];
        if ($scope.currentUser.BusinessUnitName==="AVIVA")
        {
        //    $("#isAVIVA").show();
            $scope.IsAVIVA = true;
        
        }
        $scope.BUnits.push({
            "BusinessUnitID": $scope.currentUser.BusinessUnitID,
            "BusinessUnit": $scope.currentUser.BusinessUnitName

        });
     
        $scope.BUnit = $scope.BUnits[0].BusinessUnitID;
       // alert($scope.BUnit);
        getAllPartners();

   };
    function getAllPartners() {
        //alert("hh");
        QuatationService.getAllPartnerMapping().then(function (results) {

            $scope.Partners = results.data;
             $scope.partner=$scope.Partners[0].PartnerID;
            //alert(angular.toJson(results.data));

        });
    };
    function getAllClients() {
        QuatationService.getAllClients().then(function (results) {
            $scope.Clients = results.data;
        });
    };
    function getAllInsClass() {
        QuatationService.getAllInsClass().then(function (results) {

            $scope.InsClass = results.data;
        });
    };
    function getAllInsuranceCompanny() {
        QuatationService.getAllInsuranceCompanny().then(function (results) {
           // alert(angular.toJson(results.data));
            for(var i=0;i<results.data.length;i++)
            {
                results.data[i].isChecked = true;
            }

            //alert(angular.toJson(results.data));

            $scope.InsuranceCompies = results.data;
        });
    };
    function loadMyRequestList() {
        QuatationService.getAllClientRequests().then(function (results) {
            $scope.showLoader = false;
           // alert("gfyyy" + angular.toJson(results));
            if (results.status === true) {
                $scope.MyReqLists = results.data;
                //$("#bindedClass").removeClass('table');
               // $("#bindedClass").addClass('table datatable');
                getAllQuotationHeaders();

                //alert("gfyyy"+angular.toJson($scope.MyReqLists));
            }
           
        });
    };
    function ReloadClientDetails(myReqList) {

        $scope.ClientID = myReqList.ClientID;
        ReLoadClientByID();




    }
    function ReloadClientHeader(myReqList) {

        $scope.partner = myReqList.PartnerID;
        $scope.RequestedDate = $filter('date')(new Date(myReqList.RequestedDate), 'MM/dd/yyyy');
        $scope.ClientRequestHeader.ClientRequestHeaderID = myReqList.ClientRequestHeaderID;
        $scope.comp = myReqList.CompanyID;
        $scope.BUnit = myReqList.BusinessUnitID;
       // alert(myReqList.BusinessUnitID);

       // BusinessUnitID
        //BusinessUnitName
        //CompanyID
        //CompanyName
    }
    function ReloadRequestList(myReqList) {
        $scope.ClientRequestLineDetails = myReqList.ClientRequestLineDetails;

        for (var i = 0; i < $scope.ClientRequestLineDetails.length; i++) {
            //FilterInsClassBySubClassID();
            var propertylist = $scope.ClientRequestLineDetails[i].ClientPropertyDetails;
            var scopelist = $scope.ClientRequestLineDetails[i].ClientRequestInsSubClassScopeDetails;

            var insSubClassID = $scope.ClientRequestLineDetails[i].InsSubClassID;
            var insSubClass = $scope.ClientRequestLineDetails[i].InsSubClassName;
            var insclassList =[];// lo(insSubClassID)[0];



            //var found = $filter('filter')($scope.InsSubClass, { "InsuranceSubClassID": insSubClassID.toString() }, false);

            $scope.requestList.push({

                "inslassID": "10",
                "insSubClassID": insSubClassID,
                "InsClass": "",
                "InsSubClass": insSubClass,
                "propertyID": -1,
                "ProertDiscription": "null",
                "ClientPropertyDetails": propertylist,
                "ClientRequestInsSubClassScopeDetails": scopelist,
                "isChecked":false
            });

            for (var j = 0; j < propertylist.length; j++) {

                //$scope.clientProperty.push(list[j]);
                $scope.clientProperty.push({
                    "addedinsClassID": insSubClassID,
                    "addedInsSubClass": insSubClass,
                    "ClientPropertyID": propertylist[j].ClientPropertyID,
                    "ClientPropertyName": propertylist[j].ClientPropertyName,
                    "BRNo": propertylist[j].BRNo,
                    "VATNo": propertylist[j].VATNo,
                    "SVATNo": propertylist[j].SVATNo
                });

            }



            for (var j = 0; j < scopelist.length; j++) {

                //$scope.clientProperty.push(list[j]);
                $scope.AddedInsClassesScopes.push({
                    "ClientRequestInsSubClassScopeID": scopelist[j].ClientRequestInsSubClassScopeID,
                    "CommonInsScopeID": scopelist[j].CommonInsScopeID,
                    "InsuranceSubClassID": insSubClassID,
                    "InsuranceClassCode": "",
                    "Description": scopelist[j].CommonInsScopeName,
                    "isChecked": scopelist[j].isChecked,
                    "ExcessType":"1",
                    "ExcessAmount":"0.00"
                });

            }




        }


       // alert("gggg"+angular.toJson($scope.AddedInsClassesScopes));

    }
    function ReLoadClientByID() {
 
        $scope.clientObj = $filter('filter')($scope.Clients, { "ClientID": $scope.ClientID }, false)[0];


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
    function PopulateAddedInsCompanyList()
    {
  
        for (var i = 0; i < $scope.foundReqList.length; i++)
            {

            $scope.currentInsClassReq = $scope.foundReqList[i];
                FilterInsurance();
            } 
        


    }
    function RemoveAddedInscompanyList(item)
    {
        var index = $scope.AddedInscompanies.indexOf(item);
    
        if (index >= 0) {

            $scope.AddedInscompanies.splice(index, 1);
        }
       // alert($scope.requestList);
       noty({ text: 'Succesfully Deleted Item ', layout: 'topRight', type: 'success' });
    
    
    }
    function FilterclientPropertiesBySubClassID(addedinssubClassID) {
        $scope.AddedPropertiesInsClass = [];
        var found = $filter('filter')($scope.clientProperty, { "addedinsClassID": addedinssubClassID.toString() }, false);
        if (found.length) {
            for (var i = 0; i < found.length; i++) {
                $scope.AddedPropertiesInsClass.push({
                    "addedinsClassID": found[i].addedinsClassID,
                    "addedInsSubClass": found[i].addedInsSubClass,
                    "ClientPropertyID": found[i].ClientPropertyID,
                    "ClientPropertyName": found[i].ClientPropertyName,
                    "BRNo": found[i].BRNo,
                    "VATNo": found[i].VATNo,
                    "SVATNo": found[i].SVATNo
                });
            }
        }

        return $scope.AddedPropertiesInsClass;




    }
    function FilterScopeBySubClassID(addedinssubClassID) {
        // alert(addedinssubClassID);
        $scope.AddedInsClassesScopesByInsSubClassID = [];

        var found = $filter('filter')($scope.AddedInsClassesScopes, { "InsuranceSubClassID": addedinssubClassID.toString() }, false);
        alert("found" + angular.toJson(found));
        // alert("AddedInsClassesScopes " + addedinssubClassID);
        if (found.length) {
            for (var i = 0; i < found.length; i++) {
                $scope.AddedInsClassesScopesByInsSubClassID.push({
                    "CommonInsScopeID": found[i].CommonInsScopeID,
                    "InsuranceSubClassID": found[i].InsuranceSubClassID,
                    "InsuranceClassCode": found[i].InsuranceClassCode,
                    "Description": found[i].Description,
                    "isChecked": found[i].isChecked
                });
            }

        }
        //alert("$scope.AddedInsClassesScopesByInsSubClassID;" + angular.toJson($scope.AddedInsClassesScopesByInsSubClassID));
        return $scope.AddedInsClassesScopesByInsSubClassID;
    }
    function FilterScopeBySubClassIDFromInsCompany(addedinssubClassID) {
       // alert(addedinssubClassID);
        $scope.InsSubClassScopeByInsCompany = [];
        //alert("$scope.AddedInsClassesScopes;" + angular.toJson($scope.AddedInsClassesScopes));

        var found = $filter('filter')($scope.AddedInsClassesScopes, { "InsuranceSubClassID": addedinssubClassID.toString() }, false);
       
        if (found.length)
        {
            for (var i = 0; i < found.length; i++) {
                $scope.InsSubClassScopeByInsCompany.push({
                    "CommonInsScopeID": found[i].CommonInsScopeID,
                    "InsuranceSubClassID": found[i].InsuranceSubClassID,
                    "InsuranceClassCode": found[i].InsuranceClassCode,
                    "Description": found[i].Description,
                    "isChecked": true,
                    "ExcessType": found[i].ExcessType,
                    "ExcessAmount": found[i].ExcessAmount

                });
            }

        }
       // alert("$scope.InsSubClassScopeByInsCompany;" + angular.toJson($scope.InsSubClassScopeByInsCompany));
        return $scope.InsSubClassScopeByInsCompany;
    }
    function FilterInsurance()
    {
                        var found = $filter('filter')($scope.InsuranceCompies, { "isChecked": "true" }, false);
                       // alert(angular.toJson($scope.currentInsClassReq));
                        if (found.length) {
                            for (var j = 0; j < found.length; j++) {
                                var isExist = ISExistInsCompanyForInsSubClass($scope.currentInsClassReq.insSubClassID, found[j].InsuranceCompanyID);
                                if (!isExist) {
                                    $scope.AddedInscompanies.push({
                                        "inslassID": $scope.currentInsClassReq.inslassID,
                                        "insSubClassID": $scope.currentInsClassReq.insSubClassID,
                                        "InsClass": $scope.currentInsClassReq.InsClass,
                                        "InsSubClass": $scope.currentInsClassReq.InsSubClass,
                                        "InsuranceCompanyName": found[j].InsuranceCompanyName,
                                        "InsuranceCompanyID": found[j].InsuranceCompanyID,
                                        "isChecked": found[j].isChecked,
                                        "SumInsured":"0.00"
                                    });





                                }

                                else
                                {

                                    noty({ text: 'Ins Company Already Added For This Ins Sub Class' + $scope.currentInsClassReq.InsSubClass, layout: 'center', type: 'warning' });


                                }

                            }
                        }
                       // alert(angular.toJson($scope.AddedInscompanies));            
            
        return $scope.AddedInscompanies;
}
    function sortByInsCompany()
    {
        for (var j = 0; j < scope.AddedInscompanies.length; j++)
        {

            var InsuranceCompanyID = scope.AddedInscompanies[j].InsuranceCompanyID;
            FilterInsuranceCompanyByCompnay(InsuranceCompanyID);





        }
    
    
    
    
    }
    function FilterInsuranceCompanyByCompnay(InsuranceCompanyID) {

        var found = $filter('filter')($scope.AddedInscompanies, { "InsuranceCompanyID": InsuranceCompanyID.toString() }, false);
        // alert(angular.toJson($scope.currentInsClassReq));
        if (found.length) {
            for (var j = 0; j < found.length; j++) {
                var isExist = ISExistInsCompanyForInsSubClass($scope.currentInsClassReq.insSubClassID, found[j].InsuranceCompanyID);
                if (!isExist) {
                    $scope.AddedInscompaniesSorted.push({
                        "inslassID": $scope.currentInsClassReq.inslassID,
                        "insSubClassID": $scope.currentInsClassReq.insSubClassID,
                        "InsClass": $scope.currentInsClassReq.InsClass,
                        "InsSubClass": $scope.currentInsClassReq.InsSubClass,
                        "InsuranceCompanyName": found[j].InsuranceCompanyName,
                        "InsuranceCompanyID": found[j].InsuranceCompanyID,
                        "isChecked": found[j].isChecked
                    });





                }

                else {

                    noty({ text: 'Ins Company Already Added For This Ins Sub Class' + $scope.currentInsClassReq.InsSubClass, layout: 'center', type: 'warning' });


                }

            }
        }
     //   alert(angular.toJson($scope.AddedInscompaniesSorted));

        return $scope.AddedInscompaniesSorted;
    }
    function ISExistInsCompanyForInsSubClass(insSubCID, InsCompanyID)
{
    var isExist = false;
    for (var k = 0; k < $scope.AddedInscompanies.length; k++)
    {
        var addedInsCom=$scope.AddedInscompanies[k];
        var insSubClassID=$scope.AddedInscompanies[k].insSubClassID;
        var InsuranceCompanyID=$scope.AddedInscompanies[k].InsuranceCompanyID;
        if (insSubClassID===insSubCID  && InsuranceCompanyID===InsCompanyID)
        {

            isExist = true;
            break;


        }


    }
       // alert(isExist);
        return isExist;



}
    function FilterReqInsClasses() {

       var FilteredInsSubClasses= [];
       ///alert(angular.toJson($scope.requestList));
       var found = $filter('filter')($scope.requestList, { "isChecked": "true" }, false);

        if (found.length) {
            for (var i = 0; i < found.length; i++) {
                FilteredInsSubClasses.push({
                    "inslassID": found[i].inslassID,
                    "insSubClassID": found[i].insSubClassID,
                    "InsClass": found[i].InsClass,
                    "InsSubClass": found[i].InsSubClass,
                    "isChecked": found[i].isChecked
                });
            }

        }
       // alert(angular.toJson($scope.FilteredInsSubClasses));
        return FilteredInsSubClasses;
    }
    function Savequatation()
    {
        $scope.quotationHeader = FinalizeQuatationHeader();

       /* $scope.QuotationDetailsInsCompanyScope = [{ "ScopeDescription": "Test", "ExcessType": "Test", "ExcessAmount": 25.5 }];
        $scope.QuotationDetailsInsCompanyLine = [{ "InsuranceSubClassID": 1, "SumInsured": 45.5, "QuotationDetailsInsCompanyScopeDetails": $scope.QuotationDetailsInsCompanyScope }];
        $scope.QuotationDetailsInsCompanyHeader = [{ "PremiumIncludingTax": 5.5, "ExcessDescription": "Test", "ExcessAmount": 450.5, "QuotationDetailsInsCompanyLineDetails": $scope.QuotationDetailsInsCompanyLine }];
        $scope.QuotationRequestedInsCompany =[];// [{ "InsuranceCompanyID": 1018, "Status": true, "QuotationDetailsInsCompanyHeaderDetails": $scope.QuotationDetailsInsCompanyHeader }];
      $scope.QuotationLine = [{ "InsuranceClassID": 10, "InsuranceSubClassID": 1, "RequestedInsuranceCompanyDetails": $scope.QuotationRequestedInsCompany }];
      $scope.quotationHeader = { "ClientRequestHeaderID": 5, "Status": true, "QuotationLineDetails": $scope.QuotationLine };
 */ 
     
        //alert(angular.toJson($scope.statuseee));
        QuatationService.updateQuotation($scope.quotationHeader).then(function (results)
        {

            //alert(angular.toJson(results));
            $scope.quotationHeader = {};
            loadMyRequestList();
            //alert(angular.toJson(results));


        });
        






    }
    function FinalizeQuatationHeader()
    {
        $scope.quotationHeader = {};
        $scope.quotationHeader.quotationHeaderID = $scope.quotationHeaderID;
        $scope.quotationHeader.ClientRequestHeaderID = $scope.ClientRequestHeader.ClientRequestHeaderID;
        $scope.quotationHeader.Status = true;
        $scope.quotationHeader.CreatedBy = $scope.UserID;
        $scope.quotationHeader.CreatedDate = $scope.QuatedDate;
        $scope.quotationHeader.QuotationStatusCode = $scope.statuseee;// "QP"; alert();
        $scope.UserID = $scope.UserID;
        $scope.quotationHeader.QuotationLineDetails = finalizeQuatationLine();
        return $scope.quotationHeader;
        //alert($scope.quotationHeader);

    }
    function finalizeQuatationLine()
    {


        $scope.quotationHeader.QuotationLineDetails = [];
      
        for (var i = 0; i < $scope.requestList.length; i++) {
            var insSubClassID = $scope.requestList[i].insSubClassID;
            var insClassID = $scope.requestList[i].inslassID;
            var InsClass = $scope.requestList[i].InsClass;
            var InsSubClass = $scope.requestList[i].InsSubClass;
            $scope.RequestedInsuranceCompanyDetails = finalizeInsuranceCompany(insSubClassID);

            $scope.quotationHeader.QuotationLineDetails.push({
                "InsuranceClassID": insClassID,
                "InsuranceCode": InsClass,
                "InsuranceSubClassID": insSubClassID,
                "InsuranceSubClassDescription": InsSubClass,
                "RequestedInsuranceCompanyDetails":$scope.RequestedInsuranceCompanyDetails
            });

        }
        return $scope.quotationHeader.QuotationLineDetails;
    }
    function finalizeInsuranceCompany(insSubCID)
    {

        $scope.quotationHeader.QuotationLineDetails.RequestedInsuranceCompanyDetails = [];
      //  $scope.QuotationDetailsInsCompanyHeader = [];
        var found = $filter('filter')($scope.AddedInscompanies, { "insSubClassID": insSubCID.toString() }, false);
       
     
        //alert("found" + angular.toJson(found));
        for (var i = 0; i < found.length; i++) {
            var insSubClassID = found[i].insSubClassID;
            var insClassID = found[i].inslassID;
            var InsClass = found[i].InsClass;
            var InsSubClass = found[i].InsSubClass;
            var InsuranceCompanyName = found[i].InsuranceCompanyName;
            var InsuranceCompanyID = found[i].InsuranceCompanyID;
            var found_QuotationDetailsInsCompanyHeaderDetails =$filter('filter')($scope.QuotationDetailsInsCompanyHeaderDetails, { "InsuranceCompanyID": InsuranceCompanyID.toString() }, false);


            //alert("found" + angular.toJson(founddd));
            $scope.quotationHeader.QuotationLineDetails.RequestedInsuranceCompanyDetails.push({
                "InsuranceClassID": insClassID,
                "InsuranceCode": InsClass,
                "Status": true,
                "InsuranceCompanyID": InsuranceCompanyID,
                "QuotationDetailsInsCompanyHeaderDetails": found_QuotationDetailsInsCompanyHeaderDetails
            });
        }

        return $scope.quotationHeader.QuotationLineDetails.RequestedInsuranceCompanyDetails;


    }
    function finalizeInsComQuatDetHeader()
    {



        $scope.quotationHeader.QuotationLineDetails.RequestedInsuranceCompanyDetails.QuotationDetailsInsCompanyHeaderDetails = [];
        $scope.quotationHeader.QuotationLineDetails.RequestedInsuranceCompanyDetails.QuotationDetailsInsCompanyHeaderDetails.PremiumIncludingTax;
        $scope.quotationHeader.QuotationLineDetails.RequestedInsuranceCompanyDetails.QuotationDetailsInsCompanyHeaderDetails.ExcessDescription;
        $scope.quotationHeader.QuotationLineDetails.RequestedInsuranceCompanyDetails.QuotationDetailsInsCompanyHeaderDetails.ExcessAmount;
        $scope.quotationHeader.QuotationLineDetails.RequestedInsuranceCompanyDetails.QuotationDetailsInsCompanyHeaderDetails.QuotationDetailsInsCompanyLineDetails = [];




    }
    function finalizeInsComQuatDetLine()
    {

        $scope.quotationHeader.QuotationLineDetails.RequestedInsuranceCompanyDetails.QuotationDetailsInsCompanyHeaderDetails.QuotationDetailsInsCompanyLineDetails = [];
        $scope.quotationHeader.QuotationLineDetails.RequestedInsuranceCompanyDetails.QuotationDetailsInsCompanyHeaderDetails.QuotationDetailsInsCompanyLineDetails.InsuranceSubClassID;
        $scope.quotationHeader.QuotationLineDetails.RequestedInsuranceCompanyDetails.QuotationDetailsInsCompanyHeaderDetails.QuotationDetailsInsCompanyLineDetails.InsuranceSubClassDescription;
        $scope.quotationHeader.QuotationLineDetails.RequestedInsuranceCompanyDetails.QuotationDetailsInsCompanyHeaderDetails.QuotationDetailsInsCompanyLineDetails.SumInsured;

    }
    function finalizeInsComQuatDetLineInsClassses()
    {

            $scope.quotationHeader.QuotationLineDetails.RequestedInsuranceCompanyDetails.QuotationDetailsInsCompanyHeaderDetails.QuotationDetailsInsCompanyLineDetails = [];
            $scope.quotationHeader.QuotationLineDetails.RequestedInsuranceCompanyDetails.QuotationDetailsInsCompanyHeaderDetails.QuotationDetailsInsCompanyLineDetails.InsuranceSubClassID;
            $scope.quotationHeader.QuotationLineDetails.RequestedInsuranceCompanyDetails.QuotationDetailsInsCompanyHeaderDetails.QuotationDetailsInsCompanyLineDetails.InsuranceSubClassDescription;
            $scope.quotationHeader.QuotationLineDetails.RequestedInsuranceCompanyDetails.QuotationDetailsInsCompanyHeaderDetails.QuotationDetailsInsCompanyLineDetails.SumInsured;


    }
    function finalizeInsComQuatDetLineInsClasssesScopes()
        {

            $scope.quotationHeader.QuotationLineDetails.RequestedInsuranceCompanyDetails.QuotationDetailsInsCompanyHeaderDetails.QuotationDetailsInsCompanyLineDetails.QuotationDetailsInsCompanyScopeDetails = [];
            $scope.quotationHeader.QuotationLineDetails.RequestedInsuranceCompanyDetails.QuotationDetailsInsCompanyHeaderDetails.QuotationDetailsInsCompanyLineDetails.QuotationDetailsInsCompanyScopeDetails.ScopeDescription;
            $scope.quotationHeader.QuotationLineDetails.RequestedInsuranceCompanyDetails.QuotationDetailsInsCompanyHeaderDetails.QuotationDetailsInsCompanyLineDetails.QuotationDetailsInsCompanyScopeDetails.ExcessType;
            $scope.quotationHeader.QuotationLineDetails.RequestedInsuranceCompanyDetails.QuotationDetailsInsCompanyHeaderDetails.QuotationDetailsInsCompanyLineDetails.QuotationDetailsInsCompanyScopeDetails.ExcessAmount;



        }
    function SaveQuatationStatus() {
        QuatationService.UpdateQuatationStatus($scope.quotationHeaderID, $scope.statuseee, $scope.currentUser.UserID).then(function (results) {
            $scope.showLoader = false;
            if (results.status === true)
            {
                
                noty({ text: 'Successfully Updated Quatation', layout: 'center', type: 'success' });
                getAllQuotationHeaders();


            }
            else
            {

                noty({ text: ' Updated Quatation Unsuccessfull', layout: 'center', type: 'error' });

            }
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


});