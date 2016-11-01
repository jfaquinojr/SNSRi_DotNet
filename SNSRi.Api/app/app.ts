module App {

    angular.module("app", [
            "ngAnimate",
            "ngRoute",
            "angular-lodash"
        ]);

    angular.module("app")
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

        });

}