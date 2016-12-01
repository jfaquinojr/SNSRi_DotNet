module App {
    import Room = Data.Contracts.Room;
    import Device = Data.Contracts.Device;
    import UIRoomDevice = Data.Contracts.UIRoomDevice;

    export class AddRoomDeviceController {

        public devices: Device[];
        public selectedDevice: Device;
        public scope = false;

        static $inject = ["$scope", "deviceDataService"];
        constructor(private $scope, private deviceDataService: IDeviceDataService) {
            console.debug("AddRoomDeviceController scope", $scope);
            this.loadData();
        }

        private loadData(): void {
            const self = this;
            this.deviceDataService.getAllDevices()
                .then(response => {
                    self.devices = response.data;
                    console.log(self.devices);
                });
        }

        public addDevice(device: Device): void {
            const self = this;
            self.selectedDevice = null;
            var duplicate = _.find(self.$scope.roomDevices,
                (dev: Device) => {
                    return dev.Id === device.Id;
                });
            if (!duplicate) {

                const roomDevice = {
                    UIRoomId: self.$scope.selectedRoom.Id,
                    DeviceId: device.Id,
                    DisplayText: device.Name
                } as UIRoomDevice;

                self.deviceDataService.createRoomDevice(roomDevice)
                    .then(result => {
                        self.$scope.roomDevices.push(device);

                        $.Notify({
                            caption: "Success.",
                            content: "Room Device created successfully",
                            type: "success"
                        });
                    });

            } else {

                $.Notify({
                    caption: "Alert!",
                    content: "Device has already been added",
                    type: "alert"
                });
            }
        }
    }

    export function AddRoomDevice(): ng.IDirective {

        return {
            scope: true,
            restrict: "E",
            templateUrl: "/Home/Partials?viewName=/Views/Rooms/AddRoomDevice.cshtml",
            controller: AddRoomDeviceController,
            controllerAs: "rdvm"
        }
    }

    angular.module("app")
        .directive("addRoomDevice", AddRoomDevice);
}