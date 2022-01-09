/// <reference path="angular.js" />
var app = angular.module("Cart", []);
app.controller("CartController", function ($scope, $http) {
    debugger;
    $scope.AddGioHang = function (ID) {
        $http({
            method: "get",
            url: "/Home/AddCart/?IDSP=" + ID,
        }).then(function (res) {
            alert("Thêm thành công")
        })
    }
})