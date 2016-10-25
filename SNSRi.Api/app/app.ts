(function () {
    "use strict";

    var app = angular.module("app", ["ngAnimate", "ngRoute", "angular-lodash"]);

    var tilesController = function ($scope) {

    }

    app.controller("TilesController", tilesController);

    app.config(function ($routeProvider) {
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

})();

module App {
    angular.module("app",
        ["ngAnimate",
            "ngRoute",
            "angular-lodash"
        ]);
}