module App {
    import Device = Data.Contracts.Device;
    import HomeSeerDevice = Data.HomeSeer.HomeSeerDevice;

    export class DeviceTileController {

        public vm = this;
        public HomeSeerUrl: string;

        private stop: any;
        private hs: HomeSeerDevice;

        static inject = ["$scope", "$interval", "$q", "dataService"];
        constructor(private $scope, private $interval: ng.IIntervalService, private $q, private dataService: IDataService) {
            const self = this;

            self.loadHomeSeerDevice();

            self.stop = self.$interval(() => self.loadHomeSeerDevice(), 3000);

            $scope.$on("$destroy",
                () => {
                    if (angular.isDefined(self.stop)) {
                        self.$interval.cancel(self.stop);
                        self.stop = undefined;
                    }
                });
        }

        public clicked(device: Device): void {
            //alert(JSON.stringify(device));
            //toggle device
            let newValue = "0";
            if (device.Value == "0") {
                newValue = "100";
            } else {
                newValue = "0";
            }
            this.dataService.setHomeSeerDevice(device.ReferenceId, newValue);
        }


        private loadHomeSeerDevice(): void {
            const self = this;
            let promises = [
                self.dataService.getHomeSeerUrl(),
                self.dataService.getHomeSeerDevice(self.$scope.device.ReferenceId)
            ];

            self.$q.all(promises)
                .then((values) => {
                    self.HomeSeerUrl = values[0].data;

                    self.hs = values[1].data.Devices[0] as HomeSeerDevice;

                    //update self
                    self.$scope.device.Value = parseInt(self.hs.value);

                    console.log("loadHomeSeerDevice");
                });
        }

    }

    export class DeviceTile {

        public templateUrl = "/Home/DeviceTile";
        public restrict = "E";
        public scope = {
            device: "="
        };
        public controller = DeviceTileController;
        public controllerAs = "vm";

        public static Factory() {
            const directive = () =>
            {
                return new DeviceTile();
            };
            directive["$inject"] = [];
            return directive;
        }

    }

 
    app
        .controller("deviceTileController", DeviceTileController)
        .directive("deviceTile", DeviceTile.Factory());
}


