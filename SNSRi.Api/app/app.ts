module App {

    angular.module("app",
        [
            "ngAnimate",
            "ngRoute",
            "angular-lodash"
        ])
        .config($routeProvider => {
            //$locationProvider.html5Mode(true);

            $routeProvider
                .when("/",
                {
                    templateUrl: "Home/Rooms",
                    controller: "RoomsController"
                })
                .when("/Devices/:id",
                {
                    templateUrl: "Home/Devices",
                    controller: "DevicesController"
                })
                .otherwise({
                    templateUrl: "Home/Oops"
                });

        })
        .service("dataService", App.DataService)
        .service("notificationService", App.NotificationService);
    //.controller("eventsController", EventsController);
    //.controller("toolboxController", ToolboxController); <-- no need. see _startscreen.cshtml

}