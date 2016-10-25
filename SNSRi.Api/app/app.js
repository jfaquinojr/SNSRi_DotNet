(function () {
    "use strict";
    var app = angular.module("app", ["ngAnimate", "ngRoute", "angular-lodash"]);
    var tilesController = function ($scope) {
    };
    app.controller("TilesController", tilesController);
    app.config(function ($routeProvider) {
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
    });
})();
var App;
(function (App) {
    angular.module("app", ["ngAnimate",
        "ngRoute",
        "angular-lodash"
    ]);
})(App || (App = {}));
//# sourceMappingURL=app.js.map