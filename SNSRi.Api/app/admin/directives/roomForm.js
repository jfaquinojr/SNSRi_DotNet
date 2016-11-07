var App;
(function (App) {
    function RoomForm() {
        return {
            restrict: "E",
            scope: true,
            templateUrl: "/Rooms/Edit"
        };
    }
    App.RoomForm = RoomForm;
    angular.module("app")
        .directive("roomForm", RoomForm);
})(App || (App = {}));
//# sourceMappingURL=roomForm.js.map