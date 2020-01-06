'use strict';

ibmsApp.factory('ManageTransactionsService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8', 'Authorization': $rootScope.authKey }
    };

    var getAllClientRequests = function () {
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetAllClientRequests', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var getAllClientRequestsByBUID = function (businessUnitID) {
        var params = $.param({ "BusinessUnitID": businessUnitID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetAllRequestsByBusinessUnitID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };
    var getAllHistoryClientRequestsByBUID = function (businessUnitID) {
        var params = $.param({ "BusinessUnitID": businessUnitID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/getAllHistoryClientRequestsByBUID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };
    var getAllClients = function () {
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetAllClients', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var getAllClientsByBUID = function (businessUnitID) {
        var params = $.param({ "BusinessUnitID": businessUnitID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetAllClientsByBUID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var getAllCountries = function () {
        return $http.post($rootScope.serviceURL + 'api/Utility/GetAllCountries', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var getClientByID = function (clientID) {
        var params = $.param({ "ClientID": clientID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetBUPAClientByClientID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };


    var getClientHistrotyByID = function (clientID, Year, FrequncyID, FrequncyDID) {
        var params = $.param({ "ClientID": clientID, "Year": Year, "FrequncyID": FrequncyID, "FrequncyDID": FrequncyDID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetBUPAClientHistoryByClientID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };



    var getAgentByID = function (agentID) {
        var params = $.param({ "agentID": agentID });
        return $http.post($rootScope.serviceURL + 'api/Agent/GetAgentByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var loadBankByID = function (bankID) {
        var params = $.param({ "BankID": bankID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetBankByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var getAllPartners = function () {
        return $http.post($rootScope.serviceURL + 'api/Quotation/GetAllPremiums', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var getAllFrequncy = function () {
        return $http.post($rootScope.serviceURL + 'api/Quotation/getAllFrequncy', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };




    var loadAgent = function () {
        return $http.post($rootScope.serviceURL + 'api/Agent/GetAllAgents', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var loadBanks = function (BUID) {
        var params = $.param({ "BusinessUnitID": BUID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetAllBanksByBusinessUnitID', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var loadPaymentMethods = function (BUID) {
        var params = $.param({ "id": BUID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetPaymentMethods', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var loadFamilyDiscount = function () {
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetAllFamilyDiscount', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var getAllInsSubClassesByBUID = function (businessUnitID) {
        var params = $.param({ "businessUnitID": businessUnitID });
        return $http.post($rootScope.serviceURL + 'api/InsClass/GetAllInsSubClassesByBusinessUnit', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var getAllInsSubClassScope = function (insSubClassID) {
        var params = $.param({ "insSubClassID": insSubClassID });
        return $http.post($rootScope.serviceURL + 'api/CommonInsScope/GetAllCommonInsScopesByInsSubClass', params, config).then(function (results) {
            return results.data;
        }, function (data) {

        });
    };


    var saveClientRequest = function (isClientUpdated, isClientAdded, clientObj, clientRequestHeaderObj, userID) {
        var params = $.param({ "IsClientUpdated": isClientUpdated, "IsClientAdded": isClientAdded, "ClientObj": clientObj, "ClientRequestHeaderObj": clientRequestHeaderObj, "UserID": userID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/SaveBUPARequest', params, config).then(function (results) {
            return results.data;
        }, function (data) {

        });
    };

    var saveBUPARenewelClientRequest = function (isClientUpdated, isClientAdded, clientObj, clientRequestHeaderObj, userID) {
        var params = $.param({ "IsClientUpdated": isClientUpdated, "IsClientAdded": isClientAdded, "ClientObj": clientObj, "ClientRequestHeaderObj": clientRequestHeaderObj, "UserID": userID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/SaveBUPARenewelRequest', params, config).then(function (results) {
            return results.data;
        }, function (data) {

        });
    };



    var savePayment = function (clientRequestHeaderObj, userID) {
        var params = $.param({ "ClientRequestHeaderObj": clientRequestHeaderObj, "UserID": userID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/SavePayment', params, config).then(function (results) {
            return results.data;
        }, function (data) {

        });
    };

    var saveBankTransaction = function (clientRequestHeaderObj) {
        var params = $.param({ "BankObj": clientRequestHeaderObj, });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/SaveBUPABankTransaction', params, config).then(function (results) {
            return results.data;
        }, function (data) {

        });
    };

    var getClientRequestByID = function (clientReqHeaderID) {
        var params = $.param({ "ClientRequestHeaderID": clientReqHeaderID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetRequestByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {

        });
    };

    var getClientHistoryRequestByID = function (clientReqHeaderID, clientID, Year, FrequncyID, FrequncyDID) {
        var params = $.param({ "ClientRequestHeaderID": clientReqHeaderID, "clientID": clientID, "Year": Year, "FrequncyID": FrequncyID, "FrequncyDID": FrequncyDID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/getClientHistoryRequestByID', params, config).then(function (results) {
            return results.data;
        }, function (data) {

        });
    };

    var updateClientRequest = function (isClientUpdated, isClientAdded, clientObj, clientRequestHeaderObj, userID) {
        var params = $.param({ "IsClientUpdated": isClientUpdated, "IsClientAdded": isClientAdded, "ClientObj": clientObj, "ClientRequestHeaderObj": clientRequestHeaderObj, "UserID": userID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/UpdateBUPARequest', params, config).then(function (results) {
            return results.data;
        }, function (data) {

        });
    };


    var updateRenwelClientRequest = function (isClientUpdated, isClientAdded, clientObj, clientRequestHeaderObj, userID) {
        var params = $.param({ "IsClientUpdated": isClientUpdated, "IsClientAdded": isClientAdded, "ClientObj": clientObj, "ClientRequestHeaderObj": clientRequestHeaderObj, "UserID": userID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/UpdateBUPARenewelRequest', params, config).then(function (results) {
            return results.data;
        }, function (data) {

        });
    };



    var initializeQuotation = function (quotationHeaderObj, userID) {
        var params = $.param({ "QuotationHeaderObj": quotationHeaderObj, "UserID": userID });
        return $http.post($rootScope.serviceURL + 'api/Quotation/SaveQuotation', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var sendEmailRequest = function (emailObj) {
        var params = $.param({ "UserName": emailObj.userName, "EmailAddress": emailObj.emailAddress, "EmailHeader": emailObj.emailHeader, "EmailContent": emailObj.emailMessage });
        return $http.post($rootScope.serviceURL + 'api/Email/SendGeneralEmail', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var loadTitle = function () {
        return $http.post($rootScope.serviceURL + 'api/Utility/GetAllTitles', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };
    
    var loadRelationship = function () {
        return $http.post($rootScope.serviceURL + 'api/Utility/GetAllRelationship', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var loadGender = function () {
        return $http.post($rootScope.serviceURL + 'api/Utility/GetAllGenders', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };
    
    var getAllPartners = function () {
        return $http.post($rootScope.serviceURL + 'api/Quotation/GetAllPremiums', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var loadCurrencies = function () {
        return $http.post($rootScope.serviceURL + 'api/Utility/GetAllCurrencies', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };

    var loadClientbyMembershipID = function () {
        return $http.post($rootScope.serviceURL + 'api/Utility/GetAllCurrencies', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            // Handle error here
        });
    };
    
    var loadMemberActive = function () {
        return $http.post($rootScope.serviceURL + 'api/Utility/getStatus', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };

    var loadPaymentDetails = function (ClientID) {
        var params = $.param({ "ClientID": ClientID });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/loadPaymentDetails', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };
    
    var loadYear = function () {
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/GetAllYear', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };
    
    var loadFrequncyData = function (fequncyID) {

        var params = $.param({ "fequncyID": fequncyID });

        return $http.post($rootScope.serviceURL + 'api/ClientRequest/loadFrequncyData', params, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };
    
    var changeActiveStatus = function (id, isActive, effectiveDate, user) {
        var params = $.param({ "filterObj": { "RequestId": id, "IsActive": isActive, "EffectiveDate": effectiveDate, "UserId": user } });
        return $http.post($rootScope.serviceURL + 'api/ClientRequest/ChangeActiveStatus', params, config).then(function (results) {
            return results.data;
        }, function (data) {
        });
    };

    return {

        loadCurrencies: loadCurrencies,
        loadMemberActive: loadMemberActive,
        getAllPartners: getAllPartners,
        loadTitle: loadTitle,
        loadGender: loadGender,
        loadRelationship: loadRelationship,
        getAllClientRequests: getAllClientRequests,
        getAllClientRequestsByBUID: getAllClientRequestsByBUID,
        getAllClients: getAllClients,
        getAllClientsByBUID: getAllClientsByBUID,
        getAllCountries: getAllCountries,
        getClientByID: getClientByID,
        getAllPartners: getAllPartners,
        getAllInsSubClassesByBUID: getAllInsSubClassesByBUID,
        getAllInsSubClassScope: getAllInsSubClassScope,
        saveClientRequest: saveClientRequest,
        getClientRequestByID: getClientRequestByID,
        updateClientRequest: updateClientRequest,
        initializeQuotation: initializeQuotation,
        sendEmailRequest: sendEmailRequest,
        loadFamilyDiscount: loadFamilyDiscount,
        loadBanks: loadBanks,
        loadAgent: loadAgent,
        getAgentByID: getAgentByID,
        loadBankByID: loadBankByID,
        savePayment: savePayment,
        saveBankTransaction: saveBankTransaction,
        loadPaymentMethods: loadPaymentMethods,
        getAllPartners: getAllPartners,
        getAllFrequncy: getAllFrequncy,
        loadPaymentDetails: loadPaymentDetails,
        loadYear: loadYear,
        updateRenwelClientRequest: updateRenwelClientRequest,
        getAllHistoryClientRequestsByBUID: getAllHistoryClientRequestsByBUID,
        getClientHistoryRequestByID: getClientHistoryRequestByID,
        getClientHistrotyByID: getClientHistrotyByID,
        loadFrequncyData: loadFrequncyData,
        changeActiveStatus: changeActiveStatus,

    };
});