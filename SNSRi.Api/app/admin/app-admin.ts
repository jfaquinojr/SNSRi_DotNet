var app = angular.module("app", ["ngRoute", "datatables", "cgBusy", "ui.select2"]);

app.config(function ($routeProvider) {
    //$locationProvider.html5Mode(true);

    $routeProvider
        .when("/Users/",
        {
            templateUrl: "Users/Index"
            //controller: "UsersController"
        })
        .when("/Rooms/",
        {
            templateUrl: "Rooms/Index"
        })
        .when("/Devices/",
        {
            templateUrl: "Devices/Index"
        })
        .when("/FactoryReset/",
        {
            templateUrl: "Admin/FactoryReset"
        })
        .otherwise({
            templateUrl: "Home/Oops"
        });

});