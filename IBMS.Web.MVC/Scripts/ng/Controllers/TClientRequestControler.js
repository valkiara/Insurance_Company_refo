'use strict';

ibmsApp.controller("ClentRequestController", function ($scope, $http, clientRequestService,AuthService, $window, $filter) {
  $scope.RequestedDate = $filter('date')(new Date(), 'MM/dd/yyyy');

  $scope.message = "";
  $scope.IsAVIVA = true;
    $scope.BUnits = [];
    getCurrentUser();

  
    var config = {  headers: { 'Content-Type': 'application/json; charset=UTF-8', 'Authorization': 'Basic VGVzdDoxMjM=' }};
    function getCurrentUser() {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;
           getAllBusinessUnits();
           // alert(angular.toJson(results));
            //var currentUser = $scope.currentUser;
        });
    }
    //start initialization
    $scope.RequestHeader = [];
    $scope.confirmationNumber = -1;
    $scope.customerID = -1;
    $scope.isUpdate = false;
    $scope.isNewCustomer = true;
    $scope.requestList = [];
    $scope.ClientRequestHeader = {};
    $scope.ClientRequestHeader.ClientRequestHeaderID=-1;
    $scope.RequestedDate = $filter('date')(new Date(), 'MM/dd/yyyy');
    $scope.Countries = [
	{
	    "CountryID": "1",
	    "CountryName": "Sri Lanka",
	    "": ""
	},
	{
	    "CountryID": "2",
	    "CountryName": "USA",
	    "": ""
	}
    ];
  
    $scope.Partners = [];
    $scope.curerntInsClass = [];
    $scope.currentInsSubClass = [];
    $scope.clientProperty = [];
    $scope.AddedInsClassesScopes = [];
    var residentCountry =1;
    $scope.inspectiondays='03/01/2018';
    $scope.fDiscount = 0;
    $scope.partner = 1;
    $scope.IsQuotationCreated = false;
    $scope.DOB = $scope.RequestedDate;


    $("#add").css("display", "block");
    $("#update").css("display", "none");
    //end initialization

    //initial Calls
    
   // LoadCompany();
    getAllInsClass();
    getAllClients();
    loadMyRequestList();

   // FinalizeData();

 //end Initial Class




    //start events
    $scope.init = function () {
        //TO DO
        n();



    };
    
    $scope.Save = function () {
     //   alert("");
        var jvalidate = $("#validate").validate({
            ignore: [],
            rules: {                                            
                'cusName': {
                    required: true
                },               
                email: {
                    required: true,
                    email: true
                },
                date: {
                    required: true,
                    date: true
                }
            }  
        });
       // alert($scope.DOB);
      //  alert($scope.email);
       // alert($scope.cusName);
        if ($scope.cusName)
        {
         //alert(JSON.stringify(jvalidate));
         FinalizeData();
        
        
        
        }

      
    


        //alert("ghg");
        //InitialsClientObject();

        //javascript: alert('Form #validate submited');

        //
    };
    $scope.Update = function () {

        var jvalidate = $("#validate").validate({
            ignore: [],
            rules: {
                'cusName': {
                    required: true
                },
                email: {
                    required: true,
                    email: true
                },
                date: {
                    required: true,
                    date: true
                }
            }
        });

        if ( $scope.cusName) {
            //alert(JSON.stringify(jvalidate));
            FinalizeData();



        }





        //alert("ghg");
        //InitialsClientObject();

        //javascript: alert('Form #validate submited');

        //
    };
    $scope.RemoveAddedInsCompany = function (item) {
        $scope.confirmationNumber=1;
       // confimationDeletetion(item);
        RemoveAddedInsList(item);
    }

    $scope.remove = function (reqList)
    {
        //alert("");
        RemoveAddedInsList(reqList);
    }
    $scope.removeProprty = function (Property) {
        //alert("");
        RemovePropertyFromList(Property);
    }
    $scope.refreash = function ()
    {
        LoadCompany();
        getAllInsClass();
        getAllClients();
        loadMyRequestList();
        initialize();
    };
    $scope.refresh = function () {
        $scope.isUpdate = false;
        $scope.IsQuotationCreated = false;
        $scope.ClientRequestHeader = {};
        $scope.ClientID = null;
        $scope.isNewCustomer = false;
        $scope.clientObj = [];
        $scope.requestList = [];
        $scope.clientRequestLine = [];
        clearPrpoert();
        ClearClient();
        $scope.ClientRequestHeader.ClientRequestHeaderID = -1;
        getAllClients();
        loadMyRequestList();
    };
    $scope.addClass = function (insSubClass)
    {
        //alert(insSubClass.InsuranceSubClassID);
        $scope.currentInsSubClass = insSubClass;


        var insSubClassID = insSubClass.InsuranceSubClassID;
        var inslassID = insSubClass.InsuranceClassID;
        var InsSubClass = insSubClass.Description;
        var InsClass = insSubClass.InsuranceClassCode;
     
       //var BusinessUnitID=insSubClass.BusinessUnitID;
        

        $scope.requestList.push({
            "inslassID": inslassID,
            "insSubClassID": insSubClassID,
            "InsClass": InsClass,
            "InsSubClass": InsSubClass,
            "propertyID": -1,
            "ProertDiscription": "null",
            "ClientPropertyDetails": [],
            "ClientRequestInsSubClassScopeDetails":[]
        });
     //alert(angular.toJson($scope.requestList));
        // $scope. pID
        getInsSubclassScope(insSubClass);
     
        //$scope.loadScope(insSubClass);
    }
    $scope.loadScope = function (insSubClass)
    {
       //alert(insSubClass.InsuranceClassID);
        getInsSubclassScope(insSubClass);
    }
    $scope.loadSubInsClass = function (insClass)
    {
    

       // alert($scope.iClass);
        getInsSubclass(insClass);
    
    
    
    
    
    }  
    $scope.AddScopeToList = function (scopeToList)
    {
      //  alert("");
        AddScopeToList(scopeToList);



    }
    $scope.AddPropertyToList = function ()
    {

       // var insSubClassID = insSubClass.InsuranceSubClassID;
       // var inslassID = insSubClass.InsuranceClassID;
       // var InsSubClass = insSubClass.Description;
       // var InsClass = insSubClass.InsuranceClassCode;

        var addedinsClassID = $scope.currentInsSubClass.InsuranceClassID;
        var addedinsSubClassID = $scope.currentInsSubClass.InsuranceSubClassID;
        var addedInsSubClass = $scope.currentInsSubClass.Description;
        var InsuranceClassCode = $scope.currentInsSubClass.InsuranceClassCode

            //alert("alreadyExsystList" + angular.toJson( $scope.currentInsSubClass));
        var pID=$scope.pID;
        var ClientPropertyName = $scope.pName;
        var BRNo = $scope.BRNo;
        var VATNo = $scope.VATNo;
        var SVATNo = $scope.SVATNo;

        var alreadyExsystList = $filter('filter')($scope.clientProperty, { "addedinsClassID": addedinsSubClassID, "ClientPropertyID": pID }, true);
       // alert("ClientPropertyName" + ClientPropertyName);
        if (alreadyExsystList.length==0)
        {
            $scope.clientProperty.push({
                "addedinsClassID": addedinsSubClassID,

                "addedinsClass": InsuranceClassCode,
                "addedInsSubClass": addedInsSubClass,
                "ClientPropertyID": pID,
                "ClientPropertyName": ClientPropertyName,
                "BRNo": BRNo,
                "VATNo": VATNo,
                "SVATNo": SVATNo
            });
            LoadclientPropertiesBySubClassID(addedinsSubClassID);

            clearPrpoert();

        
        }
        else
        {
         noty({ text: 'Property Already Added For This Sub Class', layout: 'topRight', type: 'error' });

        
        
        }
      //  alert("alreadyExsystList" + angular.toJson($scope.clientProperty));

 
     //   alert(addedinsClassID);
       // AddToRequestedList(addedinsClassID, pID, ClientPropertyName, VATNo, SVATNo, BRNo)
    }
    $scope.loadAddedScope = function (addedScoped)
    {
        //$scope.AddedInsClassesScopes = [];
        
        LoadScopeBySubClassID(addedScoped.insSubClassID);
        $("#addedScopeList").modal("show");

    }
    $scope.loadAddedProperties = function (propery) {
        $scope.AddedPropertiesInsClass = [];

        //alert(propery.insSubClassID);
        LoadclientPropertiesBySubClassID(propery.insSubClassID);
      
        $("#addedPropList").modal("show");




    }
    $scope.loadMyRequestList = function () {
        //alert(insSubClass.InsuranceClassID);
        loadMyRequestList();
    }
    $scope.editMyReq = function (myReqList) {
        //alert(insSubClass.InsuranceClassID);
        // alert(angular.toJson(myReqList));
        clearPrpoert();
        ClearClient();
        $scope.IsQuotationCreated = myReqList.IsQuotationCreated;
        $scope.ClientRequestHeaderID = myReqList.ClientRequestHeaderID;
       // alert($scope.ClientRequestHeaderID);
        $("#tab-second").removeClass('tab-pane active');
        $("#tab-second").addClass('tab-pane');
        $("#tab-first").addClass('tab-pane active');


        $("#second").removeClass('active');

        $("#first").addClass('active');

        $("#update").css("display", "block");
        $("#add").css("display", "none");
        $scope.isUpdate = true;


       ReloadClientDetails(myReqList);
       ReloadClientHeader(myReqList);
       ReloadRequestList(myReqList);
    }
    $scope.LoadClientByID = function ()
    {
       // $scope.clientObj = $scope.client;
       // alert($scope.ClientID);
        
        $scope.clientObj = $filter('filter')($scope.Clients, { "ClientID": $scope.ClientID }, false)[0];


       // alert("clientObj" + angular.toJson($scope.clientObj));


        $scope.customerID = $scope.clientObj.ClientID;
        $scope.cusName = $scope.clientObj.ClientName;
        $scope.Address1 = $scope.clientObj.ClientAddress;
        $scope.Address2 = $scope.clientObj.ClientAddress;
        $scope.Address3 = $scope.clientObj.ClientAddress;
        $("#dp-3").val($scope.clientObj.DOB === "" ? $scope.RequestedDate : $scope.clientObj.DOB);
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
    $scope.Delete = function () {

    }
    $scope.MakeQuatation = function (myReqList)
    {
        //alert("");
        MakeQuatation(myReqList);




    }
    $scope.LoadInsDetails=function()
    {
    
       $("#LoadInsClassMain").modal("show");
    
    
    
    
    }
    $scope.clearPrpoert = function ()
    {
        clearPrpoert();

    }


    function RemovePropertyFromList(item) {
       

        var alreadyExsystList = $filter('filter')($scope.clientProperty, { "addedinsClassID": item.addedinsClassID, "ClientPropertyID": item.ClientPropertyID }, true);
                   // alert(alreadyExsystList.length);
        if (alreadyExsystList.length > 0)
        {
            var index = $scope.clientProperty.indexOf(alreadyExsystList[0]);
            if (index >= 0) {

                $scope.clientProperty.splice(index, 1);
                LoadclientPropertiesBySubClassID(item.addedinsClassID);

                // alert($scope.requestList);
                noty({ text: 'Succesfully Deleted Item ', layout: 'topRight', type: 'success' });
            }
        
        
        }
        //alert("df");




    }
    function RemoveAddedInsList(item)
    {
        var index = $scope.requestList.indexOf(item);
        //alert("df");
        if (index >= 0)
        {

            $scope.requestList.splice(index, 1);
        }
       // alert($scope.requestList);
       noty({ text: 'Succesfully Deleted Item ', layout: 'topRight', type: 'success' });
    
    
    }
    function ReloadClientDetails(myReqList)
    {

        $scope.ClientID = myReqList.ClientID;
       // alert($scope.ClientID);       // $scope.LoadClientByID();
        $scope.LoadClientByID();




    }
    function ReloadClientHeader(myReqList)
    {
        //alert(myReqList.PartnerID);
       // $scope.partner = null;
        $scope.partner = myReqList.PartnerID;
        $scope.RequestedDate = $filter('date')(new Date(myReqList.RequestedDate), 'MM/dd/yyyy');
        $scope.ClientRequestHeader.ClientRequestHeaderID = myReqList.ClientRequestHeaderID;
        
        //alert("PartnerID" + $filter('date')(new Date(myReqList.RequestedDate), 'MM/dd/yyyy'));

        


    }
    function ReloadRequestList(myReqList)
    {
        $scope.requestList = [];
        $scope.propertylist = [];
        $scope.clientProperty = [];
        $scope.AddedInsClassesScopes = [];


        $scope.ClientRequestLineDetails = myReqList.ClientRequestLineDetails;
       
        for (var i = 0; i < $scope.ClientRequestLineDetails.length; i++)
        {
            //FilterInsClassBySubClassID();
            var propertylist = $scope.ClientRequestLineDetails[i].ClientPropertyDetails;
            var scopelist = $scope.ClientRequestLineDetails[i].ClientRequestInsSubClassScopeDetails;

            var insSubClassID = $scope.ClientRequestLineDetails[i].InsSubClassID;
            var insSubClass = $scope.ClientRequestLineDetails[i].InsSubClassName;
            var insclassList = FilterInsSubClassBySubClassID(insSubClassID)[0];

            $scope.requestList.push({

            "inslassID": insclassList.InsuranceClassID,
            "insSubClassID": insSubClassID,
            "InsClass": insclassList.InsuranceClassCode,
            "InsSubClass": insSubClass,
            "propertyID": -1,
            "ProertDiscription": "null",
            "ClientPropertyDetails": propertylist,
            "ClientRequestInsSubClassScopeDetails": scopelist
        });

        for (var j = 0; j < propertylist.length; j++)
        {
         
            //$scope.clientProperty.push(list[j]);
            $scope.clientProperty.push({
                "addedinsClassID": insSubClassID,
                "addedInsSubClass": insSubClass,
                "ClientPropertyID": propertylist[i].ClientPropertyID,
                "ClientPropertyName": propertylist[i].ClientPropertyName,
                "BRNo": propertylist[i].BRNo,
                "VATNo": propertylist[i].VATNo,
                "SVATNo": propertylist[i].SVATNo
            });
        
        }



        for (var j = 0; j < scopelist.length; j++) {

            //$scope.clientProperty.push(list[j]);
            $scope.AddedInsClassesScopes.push({
                "ClientRequestInsSubClassScopeID": scopelist[j].ClientRequestInsSubClassScopeID,
                "CommonInsScopeID": scopelist[j].CommonInsScopeID,
                "InsuranceSubClassID": insSubClassID,
                "InsuranceClassCode": insclassList.InsuranceClassCode,
                "Description": scopelist[j].CommonInsScopeName,
                "isChecked": scopelist[j].isChecked
            });

        }



       
        }


      //  alert(angular.toJson($scope.AddedInsClassesScopes));

    }
    function ReloadPropertyList()
    {




    }
    function ReloadScopeLsit()
    {









    }
    function notyConfirm(InsCompnay, insCompanies, index) {
        var r = confirm("Are You Sure you want Delete this Record of " + InsCompnay.InsuranceCompanyName);
        if (r == true)
        {
          Delete(InsCompnay);
          insCompanies.splice(index, 1);
        }
     // alert("dd");

  }
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
       // alert(angular.toJson($scope.BUnits));
       //clientRequestService.getAllBusinessUnits().then(function (results) {
       //    //$Scope.BussinessUnit = results;
       //    $scope.BUnits = results.data;
       //});
   };
    function getAllClients() {
        //alert("hh");
        clientRequestService.getAllClients().then(function (results) {
            //$Scope.BussinessUnit = results;
            $scope.Clients = results.data;

            //$scope.comp=;
           // $scope.partner=;
            // $scope.BUnit=;












            //alert(angular.toJson(results.data));
         


        });
    };
    function getAllPartners() {
        //alert("hh");
        clientRequestService.getAllPartnerMapping().then(function (results) {

                $scope.Partners = results.data;
                $scope.partner=$scope.Partners[0].PartnerID;
                //alert(angular.toJson(results.data));

            });
        }; 
    function loadMyRequestList() {
        clientRequestService.getAllClientRequests().then(function (results) {
            $scope.showLoader = false;
            if (results.status === true) {
                $scope.MyReqLists = results.data;
             //alert(angular.toJson(results.data));
            }
            else {
                $scope.MyReqLists = [];
            }
        });
    };
    function getAllInsClass() {
       //alert("hh");
       clientRequestService.getAllInsClass().then(function (results) {

           $scope.InsClass = results.data;
           $scope.curerntInsClass = results.data[0];
           $scope.iClass = results.data[0]
           getInsSubclass(results.data[0].InsuranceClassID);
         //  alert(angular.toJson(results.data[0]));

       });
   };
    function getInsSubclass(InsuranceClassID)
   {
       //alert(insClass.InsuranceClassID);
       clientRequestService.getAvailableInsSubClass(InsuranceClassID).then(function (results) {
           $scope.showLoader = false;
 //alert(angular.toJson(results.data));
            if (results.status === true) {
                $scope.InsSubClass = results.data;
               // $scope.currentInsSubClass = results.data[0];
            }
            else {
                $scope.InsSubClass = [];
            }
        });
   
    }
    function getInsSubclassScope(insClass) {
        //alert(insClass.InsuranceClassID);
        clientRequestService.getAvailableInsSubClassScope(insClass.InsuranceSubClassID).then(function (results) {
           //alert("scope "+angular.toJson(results.data));
            if (results.status === true)
            {
                $scope.ScopeInsClasses = results.data;
                $scope.AddScopeToList($scope.ScopeInsClasses);
                //$("#iconPreview").modal("show");
            }
            else {
                $scope.ScopeInsClasses = [];
            }
        });







    }
    function AddScopeToList(scopeToList) {
        //getInsSubclassScope(scopeToList.InsuranceSubClassID);
       // var found = $filter('filter')(scopeToList, { "isChecked": "true" }, false);
      
        //alert(angular.toJson(scopeToList));
       // if (found.length) {

            
          

                for (var i = 0; i < scopeToList.length; i++) {
var alreadyExsystList = $filter('filter')($scope.AddedInsClassesScopes, { "InsuranceSubClassID": scopeToList[i].InsuranceSubClassID, "CommonInsuranceScopeID": scopeToList[i].CommonInsuranceScopeID }, true);
            ////alert(angular.toJson(alreadyExsystList));
            if (alreadyExsystList.length > 0) {
                noty({ text: 'Scope Already Added ', layout: 'topRight', type: 'error' });


            }

  else {


                $scope.AddedInsClassesScopes.push({
                    "CommonInsuranceScopeID": scopeToList[i].CommonInsuranceScopeID,
                    "InsuranceSubClassID": scopeToList[i].InsuranceSubClassID,
                    "InsuranceClassCode": scopeToList[i].InsuranceClassCode,
                    "Description": scopeToList[i].Description,
                    "isChecked": true
                });
            }
}
           //alert(angular.toJson($scope.AddedInsClassesScopes));
            //$("#iconPreview").modal("hide");
        //}
    }
    function Add(propertyToList) {
        //alert("");
       // alert(angular.toJson(scopeToList));

    }
    function AddToRequestedList(addedinsClassID, pID, pName, VATNo, SVATNo, BRNo) {
        var addToArray = true;
        for (var i = 0; i < $scope.requestList.length; i++) {
            //alert("cc");
            //if ($scope.requestList[i].addedinsClassID === addedinsClassID )
            //    {
            //    if ($scope.requestList[i].pID === pID || $scope.requestList[i].pID === -1) {
            //        $scope.requestList[i].propertyID = pID;
            //        $scope.requestList[i].ProertDiscription = "NA";
            //        addToArray = false;



            //    }
            //    else
            //    {





            //    }
              

                
            //}
        }



    }
    function FinalizeData()
    {
        
      $scope.clientObj = InitialsClientObject();//{};
        $scope.ClientRequestHeader = {};
        //$scope.clientProperty = $scope.clientProperty;//[{ "ClientPropertyName": "Test 1", "BRNo": "Test 0001", "VATNo": "Test 0001" }, { "ClientPropertyName": "Test 2", "BRNo": "Test 0002", "VATNo": "Test 0002" }];
        //$scope.clientReqInsSubClassScope = [{ "CommonInsScopeID": 6 }];
        $scope.clientRequestLine = initialiseClientRequestLine();//[{ "InsSubClassID": 1, "ClientPropertyDetails": $scope.clientProperty, "ClientRequestInsSubClassScopeDetails": $scope.clientReqInsSubClassScope }];
      //  alert( angular.toJson($scope.clientRequestLine));
        //$scope.clientObj.ClientName = "Test Client";
        //$scope.clientObj.ClientAddress = "Test Address";
        //$scope.clientObj.Email = "test@gmail.com";
        //$scope.clientObj.DOB = "12/25/1988"
        //$scope.clientObj.BusinessUnitID = 128;
        //$scope.clientObj.HomeCountryID = 1;
        //$scope.clientObj.ResidentCountryID = 2;

        $scope.ClientRequestHeader.PartnerID = $scope.partner;
        $scope.ClientRequestHeader.ClientID = $scope.ClientID;
        $scope.ClientRequestHeader.IsQuotationCreated = $scope.IsQuotationCreated;
        $scope.ClientRequestHeader.ClientRequestHeaderID = $scope.ClientRequestHeaderID;
        $scope.ClientRequestHeader.RequestedDate = $scope.RequestedDate;//mm/dd/yyyy
        $scope.ClientRequestHeader.ClientRequestLineDetails = $scope.clientRequestLine;
        alert(angular.toJson($scope.ClientRequestHeader));
       // alert($scope.isNewCustomer);
        // alert($scope.customerID);

        if ($scope.isUpdate) {
          //  alert(angular.toJson($scope.ClientRequestHeader));
            clientRequestService.UpdateClientRequest($scope.isNewCustomer, $scope.customerID, $scope.clientObj[0], $scope.ClientRequestHeader, 1).then(function (results) {
               
               // noty({ text: 'Cannnot Find Claim To Make Payment!Please Make Claim Record First!', layout: 'center', type: 'error' });
                if (results.status === true && results.message === "Successfully Updated") {

                    noty({ text: results.message, layout: 'center', type: 'success' });
                  
                    initialize();
                    getAllClients();
                    loadMyRequestList();
                }
                else {
                    noty({ text: results.message, layout: 'center', type: 'error' });


                }
                // alert(angular.toJson(results));
                //$scope.ClientRequestHeader = [];
            });



        }
        else {





      
        clientRequestService.saveClientRequest($scope.isNewCustomer, $scope.customerID, $scope.clientObj[0], $scope.ClientRequestHeader, 1).then(function (results) {

            //noty({ text: 'Cannnot Find Claim To Make Payment!Please Make Claim Record First!', layout: 'center', type: 'error' });
            if (results.status === true && results.message === "Successfully Saved") {
           
                noty({ text: results.message, layout: 'center', type: 'success' });
                //$scope.ClientRequestHeader = {};
                //$scope.clientObj = [];
                //$scope.requestList = [];
                //$scope.clientRequestLine = [];
                //$scope.clientProperty = [];
                //$scope.AddedInsClassesScopes = [];
                //$scope.AddedInsClassesScopesByInsSubClassID = [];
                //$scope.requestList = [];
                //$scope.propertylist = [];
                //$scope.clientProperty = [];
                //$scope.AddedInsClassesScopes = [];

                initialize();
           
                getAllClients();
                loadMyRequestList();
            }
            else {
                noty({ text: results.message, layout: 'center', type: 'error' });


            }
               // alert(angular.toJson(results));
        //$scope.ClientRequestHeader = [];
            });
        
          }

    };
    function InitialsClientObject()
    {
          $scope.clientObj = [];
        var company = $scope.comp;
        var partner = $scope.partner;
        var BusinessUnitID = $scope.BUnit;
        var cusName = $scope.cusName;
        var Address1 = $scope.Address1;
        var Address2 = $scope.Address2;
        var Address3 = $scope.Address3;
        var DOB = $("#dp-3").val();//$scope.DOB;
        var NIC = $scope.NIC;
        var mobileNo = $scope.mobileNo;
        var fixedLine = $scope.fixedLine;
        var email = $scope.email;
        var homeCountry = 1;//$scope.homeCountry;
        var residentCountry = 1;//$scope.residentCountry;
        var inspectionType = $scope.inspectiondays;
        var fDiscount = $scope.fDiscount;

        //clientObj.ClientName = "Test Client";
        //clientObj.ClientAddress = "Test Address";
        //clientObj.Email = "test@gmail.com";
        //clientObj.DOB = "12/25/1988"
        //clientObj.BusinessUnitID = 128;
        //clientObj.HomeCountryID = 1;
        //clientObj.ResidentCountryID = 2;
        //alert(DOB);
        $scope.clientObj.push({
            "ClientID":$scope.ClientID,
            "company": company,
            "partner": partner,
            "BusinessUnitID": BusinessUnitID,
            "ClientName": cusName,
            "ClientAddress": Address1 +","+ Address2 +","+ Address3,
            "Address2": Address2,
            "Address3": Address3,
            "DOB": DOB,
            "NIC": NIC,
            "mobileNo": mobileNo,
            "fixedLine": fixedLine,
            "Email": email,
            "HomeCountryID": homeCountry,
            "ResidentCountryID": residentCountry,
            "inspectionType": inspectionType,
            "fDiscount": fDiscount
        });
       // alert(angular.toJson($scope.clientObj));
        return $scope.clientObj;

    }
    function initialiseClientRequestLine()
    {
        for (var i = 0; i < $scope.requestList.length; i++)    
        {
            //alert(i);
            var addedinssubClassID = $scope.requestList[i].insSubClassID;
            var scopeListPerLine=LoadScopeBySubClassID(addedinssubClassID);
            var clientPropertListPerLine = LoadclientPropertiesBySubClassID(addedinssubClassID);
            $scope.requestList[i].ClientPropertyDetails = clientPropertListPerLine;
            $scope.requestList[i].InsSubClassID = addedinssubClassID;
            $scope.requestList[i].ClientRequestInsSubClassScopeDetails = scopeListPerLine;
           
        }

        return $scope.requestList;
        //alert();
        //alert("initialiseClientRequestLine" + angular.toJson($scope.requestList));


    }
    function clientProperty()
    {

 
   
        return clientProperty;






    }
    function LoadclientPropertiesBySubClassID(addedinssubClassID)
    {
        $scope.AddedPropertiesInsClass = [];
        var found = $filter('filter')($scope.clientProperty, { "addedinsClassID": addedinssubClassID.toString() }, false);
        
      //  alert("found"+angular.toJson(found));
        if (found.length) {
            for (var i = 0; i < found.length; i++)
            {
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
    function LoadScopeBySubClassID(addedinssubClassID) {
        // alert(addedinssubClassID);
        $scope.AddedInsClassesScopesByInsSubClassID = [];
     
        var found = $filter('filter')($scope.AddedInsClassesScopes, { "InsuranceSubClassID": addedinssubClassID.toString() }, false);
       //alert("found" + angular.toJson(found));
       // alert("AddedInsClassesScopes " + addedinssubClassID);
        if (found.length) {
            for (var i = 0; i < found.length; i++) {
                $scope.AddedInsClassesScopesByInsSubClassID.push({
                    "CommonInsScopeID": found[i].CommonInsuranceScopeID,
                    "InsuranceSubClassID": found[i].InsuranceSubClassID,
                    "InsuranceClassCode": found[i].InsuranceClassCode,
                    "Description": found[i].Description,
                    "isChecked": found[i].isChecked
                });
            }
       
        }
        //alert("$scope.AddedInsClassesScopesByInsSubClassID;" + angular.toJson($scope.AddedInsClassesScopesByInsSubClassID));
        return  $scope.AddedInsClassesScopesByInsSubClassID;
    }
    function FilterInsClassBySubClassID(InsClassID)
    {

        var found = $filter('filter')($scope.InsClass, { "InsuranceClassID": InsClassID.toString() }, false);


        return found;

    }
    function FilterInsSubClassBySubClassID(InsSubClassID) {

        var found = $filter('filter')($scope.InsSubClass, { "InsuranceSubClassID": InsSubClassID.toString() }, false);


        return found;

    }
    function MakeQuatation(myReqList) {
        $scope.quotationHeader = [];
        $scope.QuotationLine = [];
        $scope.quotationHeader = { "ClientRequestHeaderID": myReqList.ClientRequestHeaderID, "Status": true, "QuotationLineDetails": $scope.QuotationLine };

       // alert(angular.toJson($scope.quotationHeader));
        clientRequestService.saveQuatationRequest($scope.quotationHeader).then(function (results) {
            if (results.status === true)
            {
              $scope.quotationHeader = [];
              noty({ text: '' + "sucessfully Initialize Quatation", layout: 'center', type: 'success' });
              loadMyRequestList();
            
            }


        });




    }

    function confimationDeletetion(item) {
        // noty({ text: 'Left top notify', layout: 'topLeft' });
        //alert("cc");
        noty({
            text: 'Do you want to continue?',
            layout: 'topRight',
            buttons: [
                    {
                        addClass: 'btn btn-success btn-clean', text: 'Ok', onClick: function ($noty) {
                            if ($scope.confirmationNumber == 1) {
                                RemoveAddedInsList(item);

                            }
                            else {

                                noty({ text: 'Event Not Handled ', layout: 'topRight', type: 'success' });




                            }

                            $noty.close();







                        }
                    },
                    {
                        addClass: 'btn btn-danger btn-clean', text: 'Cancel', onClick: function ($noty) {
                            $noty.close();
                            noty({ text: 'You clicked "Cancel" button', layout: 'topRight', type: 'error' });
                        }
                    }
            ]
        })

    }






    //end function
    //initialize
    function clearPrpoert()
    {

        $scope.addedinsClassID = null;
        $scope.addedInsSubClass = null;
        $scope.pID = null;
        $scope.pName = null;
        $scope.BRNo = null;
        $scope.VATNo = null;
         $scope.SVATNo = null;

    }

    function ClearClient()
    {

         $scope.cusName=null;
         $scope.Address1 = null;
         $scope.Address2 = null;
         $scope.Address3 = null;
         $scope.DOB = null;
         $scope.NIC = null;
         $scope.mobileNo = null;
         $scope.fixedLine = null;
         $scope.email = null;
 




    }
    //end initializes
    function initialize()
    {
        $scope.ClientRequestHeader = {};
        $scope.clientObj = [];
        $scope.requestList = [];
        $scope.clientRequestLine = [];
        $scope.clientProperty = [];
        $scope.AddedInsClassesScopes = [];
        $scope.AddedInsClassesScopesByInsSubClassID = [];
        $scope.requestList = [];
        $scope.propertylist = [];
        $scope.clientProperty = [];
        $scope.AddedInsClassesScopes = [];
        clearPrpoert();
        ClearClient();
        $scope.ClientRequestHeader.ClientRequestHeaderID = -1;
        $scope.isUpdate = false;
        $scope.IsQuotationCreated = false;





    }
});