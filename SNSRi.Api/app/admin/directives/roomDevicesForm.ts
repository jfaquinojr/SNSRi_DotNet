module App {
    import Room = Data.Contracts.Room;
    import Device = Data.Contracts.Device;

    export class RoomDevicesController {

        public devices: Device[];

        static $inject = ["$scope", "deviceDataService"];
        constructor(private $scope, private deviceDataService: IDeviceDataService) {

        }


    }

    export function RoomDevicesForm(): ng.IDirective {

        return {
            scope: true,
            restrict: "E",
            templateUrl: "/Rooms/RoomDevices"
        }
    }

    angular.module("app")
        .directive("roomDevicesForm", RoomDevicesForm);
}