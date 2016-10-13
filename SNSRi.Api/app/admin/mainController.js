var mainController = function($scope) {
    $scope.ActiveMenu = "";
    $scope.Username = "demo@mail.com";

    console.log("MainController!!!");
};

angular.module("app-admin")
    .controller("MainController", mainController);
