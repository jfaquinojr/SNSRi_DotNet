var App;
(function (App) {
    function DeviceForm() {
        return {
            restrict: "E",
            scope: true,
            templateUrl: "/Devices/Edit"
        };
    }
    App.DeviceForm = DeviceForm;
    angular.module("app")
        .directive("deviceForm", DeviceForm);
})(App || (App = {}));
//# sourceMappingURL=deviceForm.js.map