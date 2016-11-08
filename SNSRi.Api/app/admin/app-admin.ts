var app = angular.module("app", ["ngRoute", "datatables", "cgBusy"]);

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
            templateUrl: "Rooms/Index",
        })
        .when("/Devices/",
        {
            templateUrl: "Devices/Index",
        })
        .when("/Events/",
        {
            templateUrl: "Events/Index",
        })
        .otherwise({
            templateUrl: "Home/Oops"
        });

});