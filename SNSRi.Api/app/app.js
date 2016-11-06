var App;
(function (App) {
    angular.module("app", [
        "ngAnimate",
        "ngRoute",
        "angular-lodash"
    ])
        .config(function ($routeProvider) {
        //$locationProvider.html5Mode(true);
        $routeProvider
            .when("/", {
            templateUrl: "Home/Rooms",
            controller: "RoomsController"
        })
            .when("/Devices/:id", {
            templateUrl: "Home/Devices",
            controller: "DevicesController"
        })
            .otherwise({
            templateUrl: "Home/Oops"
        });
    })
        .service("dataService", App.DataService)
        .service("notificationService", App.NotificationService);
})(App || (App = {}));
//# sourceMappingURL=app.js.map