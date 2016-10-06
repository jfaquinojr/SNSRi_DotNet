var app = angular.module("app");

app.controller("ToolboxController",
    function($scope) {

        $scope.openThemes = function () {
            $scope.$emit("ThemesOpened");
        }

        $scope.openEvents = function () {
            $scope.$emit("EventsCharmOpened");
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