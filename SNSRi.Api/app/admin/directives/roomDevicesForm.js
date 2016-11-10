var App;
(function (App) {
    var RoomDevicesController = (function () {
        function RoomDevicesController($scope, deviceDataService) {
            this.$scope = $scope;
            this.deviceDataService = deviceDataService;
        }
        RoomDevicesController.$inject = ["$scope", "deviceDataService"];
        return RoomDevicesController;
    }());
    App.RoomDevicesController = RoomDevicesController;
    function RoomDevicesForm() {
        return {
            scope: true,
            restrict: "E",
            templateUrl: "/Rooms/RoomDevices"
        };
    }
    App.RoomDevicesForm = RoomDevicesForm;
    angular.module("app")
        .directive("roomDevicesForm", RoomDevicesForm);
})(App || (App = {}));
//# sourceMappingURL=roomDevicesForm.js.map