'use strict';

ibmsApp.factory('CommissionFormatService', function ($http, $rootScope) {

    var config = {
        headers: { 'Content-Type': 'application/json', 'Authorization': $rootScope.authKey }
    };

   
    var SaveData = function (excelData) {
        $http({
            method: "POST",
            url: "/home/SaveData",
            data: JSON.stringify(excelData),
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(function (data) {
            if (data.status) {
                $scope.Message = excelData.length + " record inserted";
            }
            else {
                $scope.Message = "Failed";
            }
        }, function (error) {
            $scope.Message = "Error";
        })
    };

    var SendExcelData = function (excelData) {
        var params = $.param({ "BusinessUnitID": excelData });
        return $http.post($rootScope.serviceURL + 'api/CommissionFormat/ReadExcel', null, config).then(function (results) {
            return results.data;
        }, function (data) {
            //Handle error here
        });
    };


    //var SendExcelData = function (excelData) {
    //    var request = $http({
    //        method: "post",
    //        url: $rootScope.serviceURL + 'api/Policy/GetAllPolicyRenewalHistoriesByBusinessUnitID',
    //        data: JSON.stringify(excelData),
    //        headers: {
    //            'Content-Type': undefined
    //        },
    //        transformRequest: angular.identity
    //    });
       
    //}

    return {
        SendExcelData: SendExcelData
        };
});