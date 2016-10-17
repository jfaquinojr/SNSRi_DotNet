var app = angular.module("app-admin", ["ngRoute", "datatables"]);

app.config(function ($routeProvider) {
    //$locationProvider.html5Mode(true);

    $routeProvider
        .when("/Users/",
        {
            templateUrl: "Users/Index",
            controller: "UsersController"
        })
        .when("/Users/Create",
        {
            templateUrl: "Users/Create",
            controller: "UsersController"
        })
        .when("/Users/Edit/:id",
        {
            templateUrl: "Users/Edit",
            controller: "UsersController"
        })
        .when("/Rooms/",
        {
            templateUrl: "Rooms/Index",
            controller: "RoomsController"
        })
        .when("/Rooms/Create",
        {
            templateUrl: "Rooms/Create",
            controller: "RoomsController"
        })
        .when("/Rooms/Edit/:id",
        {
            templateUrl: "Rooms/Edit",
            controller: "RoomsController"
        })
        .when("/Devices/",
        {
            templateUrl: "Devices/Index",
            controller: "DevicesController"
        })
        .when("/Devices/Create",
        {
            templateUrl: "Devices/Create",
            controller: "DevicesController"
        })
        .when("/Devices/Edit/:id",
        {
            templateUrl: "Devices/Edit",
            controller: "DevicesController"
        })
        .when("/Events/",
        {
            templateUrl: "Events/Index",
            controller: "EventsController"
        })
        .when("/Events/Create",
        {
            templateUrl: "Events/Create",
            controller: "EventsController"
        })
        .when("/Events/Edit/:id",
        {
            templateUrl: "Events/Edit",
            controller: "EventsController"
        })
        .otherwise({
            templateUrl: "Home/Oops"
        });

});