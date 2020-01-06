'use strict';

ibmsApp.controller("CommissionFormatController", function ($scope, $http, $rootScope, CommissionFormatService, $location, AuthService, filterFilter, $modal) {

    $scope.getCurrentUser = function () {
        AuthService.getCurrentUser().then(function (results) {
            $scope.currentUser = results;

            if ($scope.currentUser.AccessLevelTypeName === "Admin") {
                //To Do (using a popup to select the desired business unit)
            }
            else {
                $scope.businessUnitID = $scope.currentUser.BusinessUnitID;
               
            }
        });
    };


    $scope.init = function () {
        $scope.ExcelUploadDoc = {};
        $scope.SelectedFileForUpload = null;


    };

    //$scope.SelectedFileForUpload = null;

    $scope.UploadFile = function (files) {
        $scope.$apply(function () { //I have used $scope.$apply because I will call this function from File input type control which is not supported 2 way binding
            $scope.Message = "";
            $scope.SelectedFileForUpload = files[0];
        })
    };

    $scope.getDocument = function (e) {
        $scope.SelectedFileForUpload = e.files[0];
    };

    // function getDocument(e){
    //    $scope.SelectedFileForUpload = e.files[0];
    //};

    //Parse Excel Data 
    $scope.ParseExcelDataAndSave = function () {
        var file = $scope.SelectedFileForUpload;
        if (file) {
            var reader = new FileReader();
            reader.onload = function (e) {
                var data = e.target.result;

                var workbook = XLSX.read(data, { type: 'binary' });
                var sheetName = workbook.SheetNames[0];
                var excelData = XLSX.utils.sheet_to_row_object_array(workbook.Sheets[sheetName]);
                if (excelData.length > 0) {
                    //Save data 

                    CommissionFormatService.SendExcelData(excelData).then(function (results) {
                        
                        
                    });
                }
                else {
                    $scope.Message = "No data found";
                }
            }
            reader.onerror = function (ex) {
                console.log(ex);
            }

            reader.readAsBinaryString(file);
        }
    };

    //$scope.getDocument = function (files) {
    //    $scope.$apply(function () {
    //        $scope.Message = '';
    //        $scope.SelectedFileForUpload = files[0];


    //    })
    //};

    //$scope.ParseExcel = function () {
    //    var formData = new FormData();
    //    var file = $scope.SelectedFileForUpload;
    //    formData.append('file', file);
    //    $scope.showLoader = true;   
    //    var response = Excelservice.SendExcelData(formData);
    //};


    $scope.SaveData = function (excelData) {
        $http({
            method: "POST",
            url: "http://localhost:39705/api/CommissionFormat/ReadExcel",
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
    }
});



//var app = angular.module("MyApp",[])
//ibmsApp.controller("CommissionFormatController", ['$scope', 'Excelservice', function ($scope, Excelservice) {
//    $scope.SelectedFileForUpload = null; //initially make it null
//    $scope.BindData = null;
//    $scope.showLoader = false;
//    $scope.IsVisible = false;

//    $scope.UploadFiles = function (files) {
//        $scope.$apply(function () {
//            $scope.Message = '';
//            $scope.SelectedFileForUpload = files[0];
//        })
//    }
//    $scope.ParseExcel = function () {
//        var formData = new FormData();
//        var file = $scope.SelectedFileForUpload;
//        formData.append('file', file);
//        $scope.showLoader = true;   //show spinner
//        var response = Excelservice.SendExcelData(formData);   //calling service
//        response.then(function (d) {
//            var res = d.data;
//            $scope.BindData = res;
//            $scope.IsVisible = true; //showing the table after databinding
//            $scope.showLoader = false; //after success hide spinner
//        }, function (err) {
//            console.log(err.data);
//            console.log("error in parse excel");
//        });
//    }

//    $scope.InsertData = function () {
//        var response = Excelservice.InsertToDB();
//        response.then(function (d) {
//            var res = d.data;

//            if (res.toString().length > 0) {
//                $scope.Message = res + "  Records Inserted";
//                $scope.IsVisible = false;   //used to disable the insert button and table after submitting data
//                angular.forEach(
//                angular.element("input[type='file']"),
//                function (inputElem) {
//                    angular.element(inputElem).val(null); //used to clear the file upload after submitting data
//                });
//                $scope.SelectedFileForUpload = null;   //used to disable the preview button after inserting data
//            }

//        }, function (err) {
//            console.log(err.data);
//            console.log("error in insertdata");
//        });
//    }

//}])

ibmsApp.service("Excelservice", function ($http) {
    this.SendExcelData = function (data) {
        var request = $http({
            method: "post",
            withCredentials: true,
            url: '/CommissionFormat/ReadExcel',
            data: data,
            headers: {
                'Content-Type': undefined
            },
            transformRequest: angular.identity
        });
        return request;
    }
    this.InsertToDB = function () {
        var request = $http({
            method: "get",
            url: '/Home/InsertExcelData',
            data: {},
            datatype: 'json'
        });
        return request;
    }
})


//used to check the extension of file while uploading
function checkfile(sender) {
    var validExts = new Array(".xlsx", ".xls");
    var fileExt = sender.value;
    fileExt = fileExt.substring(fileExt.lastIndexOf('.'));
    if (validExts.indexOf(fileExt) < 0) {
        alert("Invalid file selected, valid files are of " +
                 validExts.toString() + " types.");
        return false;
    }
    else return true;
}
