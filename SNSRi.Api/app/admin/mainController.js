var mainController = function($scope) {
    $scope.ActiveMenu = "";
    $scope.Username = "demo@mail.com";

    $scope.Pages = [
        { Name: "Users", Icon: "mif-users" },
        { Name: "Rooms", Icon: "mif-hotel" },
        { Name: "Devices", Icon: "mif-switch" },
        { Name: "Events", Icon: "mif-envelop" }
    ];



    console.log("MainController!!!");
};

angular.module("app-admin")
    .controller("MainController", mainController);
