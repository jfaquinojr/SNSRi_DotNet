var App;
(function (App) {
    var Device = Data.Contracts.Device;
    var DevicesController = (function () {
        function DevicesController($scope, $window, $location, deviceDataService) {
            this.$scope = $scope;
            this.$window = $window;
            this.$location = $location;
            this.deviceDataService = deviceDataService;
            console.log("initializing DevicesController");
            var self = this;
            self.vm = $scope;
            this.loadDevices();
        }
        DevicesController.prototype.loadDevices = function () {
            var self = this;
            this.loadingIndicator = this.deviceDataService.getAllDevices()
                .then(function (result) {
                //self.devices = result.data;
                self.devices = result.data;
            });
        };
        DevicesController.prototype.deleteDevice = function (device) {
            if (!confirm("Are you sure you want to delete this record?"))
                return;
            var self = this;
            this.selectedDevice = null;
            this.editingDevice = null;
            this.loadingIndicator = this.deviceDataService.deleteDevice(device.Id)
                .then(function () {
                $.Notify({
                    caption: "Deleted.",
                    content: "Device has been deleted.",
                    type: "success"
                });
                self.loadDevices();
            });
        };
        DevicesController.prototype.createDevice = function () {
            this.selectedDevice = null;
            this.editingDevice = new Device();
            this.$window.showMetroDialog("#dialog-deviceform", null);
        };
        DevicesController.prototype.editDevice = function (device) {
            this.selectedDevice = device;
            if (!angular.equals(device, this.editingDevice)) {
                this.editingDevice = angular.copy(device, this.editingDevice);
            }
            this.$window.showMetroDialog("#dialog-deviceform", null);
        };
        DevicesController.prototype.saveDevice = function () {
            var device = this.editingDevice;
            var self = this;
            if (this.selectedDevice) {
                self._updateDevice(device);
            }
            else {
                self._createDevice(device);
            }
        };
        DevicesController.prototype._updateDevice = function (device) {
            var self = this;
            this.loadingIndicator = this.deviceDataService.updateDevice(device)
                .then(function (result) {
                if (!angular.equals(device, self.selectedDevice)) {
                    self.selectedDevice = angular.copy(device, self.selectedDevice);
                }
                $.Notify({
                    caption: "Saved.",
                    content: "Device has been updated.",
                    type: "success"
                });
                self.$window.hideMetroDialog("#dialog-deviceform");
            })
                .catch(function (reason) {
                $.Notify({
                    caption: "Error",
                    content: "Device was not saved. " + reason,
                    type: "error"
                });
            });
        };
        DevicesController.prototype._createDevice = function (device) {
            var self = this;
            this.loadingIndicator = this.deviceDataService.createDevice(device)
                .then(function (result) {
                device.Id = result.data;
                self.devices.push(device);
                self.selectedDevice = null;
                $.Notify({
                    caption: "Created.",
                    content: "Device has been created.",
                    type: "success"
                });
                self.$window.hideMetroDialog("#dialog-deviceform");
            })
                .catch(function (reason) {
                $.Notify({
                    caption: "Error",
                    content: "Device was not created. " + reason,
                    type: "error"
                });
            });
        };
        DevicesController.$inject = ["$scope", "$window", "$location", "deviceDataService"];
        return DevicesController;
    }());
    App.DevicesController = DevicesController;
    angular.module("app")
        .controller("devicesController", DevicesController);
})(App || (App = {}));
//# sourceMappingURL=devicesController.js.map