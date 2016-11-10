var App;
(function (App) {
    var RoomController = (function () {
        function RoomController() {
        }
        return RoomController;
    }());
    App.RoomController = RoomController;
    function RoomForm() {
        return {
            restrict: "E",
            templateUrl: "/Rooms/Edit"
        };
    }
    App.RoomForm = RoomForm;
    angular.module("app")
        .controller("roomController", RoomController)
        .directive("roomForm", RoomForm);
})(App || (App = {}));
//# sourceMappingURL=roomForm.js.map