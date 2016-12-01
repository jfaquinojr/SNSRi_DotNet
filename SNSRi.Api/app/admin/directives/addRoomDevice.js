var App;
(function (App) {
    var AddRoomDeviceController = (function () {
        function AddRoomDeviceController($scope, deviceDataService) {
            this.$scope = $scope;
            this.deviceDataService = deviceDataService;
            this.scope = false;
            console.debug("AddRoomDeviceController scope", $scope);
            this.loadData();
        }
        AddRoomDeviceController.prototype.loadData = function () {
            var self = this;
            this.deviceDataService.getAllDevices()
                .then(function (response) {
                self.devices = response.data;
                console.log(self.devices);
            });
        };
        AddRoomDeviceController.prototype.addDevice = function (device) {
            var self = this;
            self.selectedDevice = null;
            var duplicate = _.find(self.$scope.roomDevices, function (dev) {
                return dev.Id === device.Id;
            });
            if (!duplicate) {
                var roomDevice = {
                    UIRoomId: self.$scope.selectedRoom.Id,
                    DeviceId: device.Id,
                    DisplayText: device.Name
                };
                self.deviceDataService.createRoomDevice(roomDevice)
                    .then(function (result) {
                    self.$scope.roomDevices.push(device);
                    $.Notify({
                        caption: "Success.",
                        content: "Room Device created successfully",
                        type: "success"
                    });
                });
            }
            else {
                $.Notify({
                    caption: "Alert!",
                    content: "Device has already been added",
                    type: "alert"
                });
            }
        };
        AddRoomDeviceController.$inject = ["$scope", "deviceDataService"];
        return AddRoomDeviceController;
    }());
    App.AddRoomDeviceController = AddRoomDeviceController;
    function AddRoomDevice() {
        return {
            scope: true,
            restrict: "E",
            templateUrl: "/Home/Partials?viewName=/Views/Rooms/AddRoomDevice.cshtml",
            controller: AddRoomDeviceController,
            controllerAs: "rdvm"
        };
    }
    App.AddRoomDevice = AddRoomDevice;
    angular.module("app")
        .directive("addRoomDevice", AddRoomDevice);
})(App || (App = {}));
//# sourceMappingURL=addRoomDevice.js.map