module App {
    import Room = Data.Contracts.Room;
    import Device = Data.Contracts.Device;

    export class AddRoomDeviceController {

        public devices: Device[];
        public selectedDevice: Device;
        public scope = true;

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
            self.$scope.roomDevices.push(device);
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