module App {
    import Device = Data.Contracts.Device;

    export class DevicesController {

        devices: Device[];
        selectedDevice: Device;
        editingRoom: Device;
        vm: any;

        static $inject = ["$scope", "$window", "$location", "usersDataService"];

        constructor(public $scope, private $window, private $location, private usersDataService: IUsersDataService) {
            console.log("initializing DevicesController");

            const self = this;
            self.vm = $scope;

            this.loadDevices();
        }


        private loadDevices() {
            const self = this;
            this.usersDataService.getAllUsers()
                .then(result => {
                    //self.rooms = result.data;
                });

            self.devices = [
                { Id: 1, Name: "Device 1", ReferenceId: 1, Status: "ON", Value: "100", HideFromView: false, CreatedOn: null, CreatedBy: 1, ModifiedOn: null, ModifiedBy: 1 },
                { Id: 2, Name: "Device 2", ReferenceId: 2, Status: "OFF", Value: "0", HideFromView: false, CreatedOn: null, CreatedBy: 1, ModifiedOn: null, ModifiedBy: 1 },
                { Id: 3, Name: "Device 3", ReferenceId: 3, Status: "ON", Value: "100", HideFromView: false, CreatedOn: null, CreatedBy: 1, ModifiedOn: null, ModifiedBy: 1 },
            ];

        }
    }

    angular.module("app")
        .controller("devicesController", DevicesController);


}
