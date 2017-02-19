module App {
    import Device = Data.Contracts.Device;

    export class DevicesController {

        devices: Device[];
        selectedDevice: Device;
        editingDevice: Device;
        vm: any;
        loadingIndicator: any;

        static $inject = ["$scope", "$window", "$location", "deviceDataService"];

        constructor(public $scope, private $window, private $location, private deviceDataService: IDeviceDataService) {
            console.log("initializing DevicesController");

            const self = this;
            self.vm = $scope;

            this.loadDevices();
        }


        private loadDevices() {
            const self = this;
            this.loadingIndicator = this.deviceDataService.getAllDevices()
                .then(result => {
                    //self.devices = result.data;
                    self.devices = result.data;
                });
        }

        deleteDevice(device: Device): void {

            if (!confirm("Are you sure you want to delete this record?"))
                return;

            const self = this;
            this.selectedDevice = null;
            this.editingDevice = null;
            this.loadingIndicator = this.deviceDataService.deleteDevice(device.Id)
                .then(() => {
                    $.Notify({
                        caption: "Deleted.",
                        content: "Device has been deleted.",
                        type: "success"
                    });

                    self.loadDevices();
                });
        }

        createDevice(): void {
            this.selectedDevice = null;
            this.editingDevice = new Device();
            this.$window.showMetroDialog("#dialog-deviceform", null);
        }

        editDevice(device: Device): void {
            this.selectedDevice = device;
            if (!angular.equals(device, this.editingDevice)) {
                this.editingDevice = angular.copy(device, this.editingDevice);
            }
            this.$window.showMetroDialog("#dialog-deviceform", null);
        }

        saveDevice() {
            const device = this.editingDevice;
            const self = this;

            if (this.selectedDevice) {
                self._updateDevice(device);
            } else {
                self._createDevice(device);
            }
        }

        private _updateDevice(device: Device) {
            const self = this;
            this.loadingIndicator = this.deviceDataService.updateDevice(device)
                .then(result => {

                    
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
                .catch(reason => {
                    $.Notify({
                        caption: "Error",
                        content: "Device was not saved. " + reason,
                        type: "error"
                    });
                });
        }

        private _createDevice(device: Device) {
            const self = this;
            this.loadingIndicator = this.deviceDataService.createDevice(device)
                .then(result => {

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
                .catch(reason => {
                    $.Notify({
                        caption: "Error",
                        content: "Device was not created. " + reason,
                        type: "error"
                    });
                });
        }

    }

    angular.module("app")
        .controller("devicesController", DevicesController);


}
