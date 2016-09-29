(function () {
    "use strict";

    var app = angular.module("app", []);

    var tilesController = function ($scope) {

    }

    app.controller("TilesController", tilesController);

    app.config(["$locationProvider", function ($locationProvider) {
        $locationProvider.html5Mode(true);
    }]);

})();