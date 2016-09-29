var app = angular.module("app");

app.controller("ToolboxController",
    function($scope) {

        var scope = $scope;

        scope.Username = "Jojo Aquino";

        scope.LogOut = function() {
            alert("Logout not yet implemented!");
        }

        scope.OpenActionCenter = function() {
            alert("Action Center");
        }
    });


app.directive("toolbox",
    function() {

        return {
            templateUrl: "/Home/Toolbox",
            restrict: "E",
            scope: true
        };


    });