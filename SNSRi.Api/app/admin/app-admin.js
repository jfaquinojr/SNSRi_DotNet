var app = angular.module("app", ["ngRoute", "datatables"]);
app.config(function ($routeProvider) {
    //$locationProvider.html5Mode(true);
    $routeProvider
        .when("/Users/", {
        templateUrl: "Users/Index",
        controller: "UsersController"
    })
        .when("/Rooms/", {
        templateUrl: "Rooms/Index",
        controller: "RoomsController"
    })
        .when("/Devices/", {
        templateUrl: "Devices/Index",
        controller: "DevicesController"
    })
        .when("/Events/", {
        templateUrl: "Events/Index",
        controller: "EventsController"
    })
        .otherwise({
        templateUrl: "Home/Oops"
    });
});
//# sourceMappingURL=app-admin.js.map