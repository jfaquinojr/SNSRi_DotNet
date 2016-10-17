var mainController = function($scope) {
    $scope.ActiveMenu = "";
    $scope.Username = "demo@mail.com";
    $scope.SelectedPage = { Name: "Users", Icon: "mif-users", Title: "Users" };

    $scope.Pages = [
        { Name: "Users", Icon: "mif-users", Title: "Users" },
        { Name: "Rooms", Icon: "mif-hotel", Title: "Rooms" },
        { Name: "Devices", Icon: "mif-switch", Title: "Devices" },
        { Name: "Events", Icon: "mif-envelop", Title: "Events" }
    ];



    console.log("MainController!!!");
};

angular.module("app-admin")
    .controller("MainController", mainController);
