module App {
    import Device = Data.Contracts.Device;
    import HomeSeerDevice = Data.HomeSeer.HomeSeerDevice;

    export class DeviceTileController {

        public vm = this;
        public HomeSeerUrl: string;
        public hs: HomeSeerDevice;
        public device: Device;

        private stop: any;

        static $inject = ["$scope", "$interval", "$q", "dataService", "signalRService"];
        constructor(private $scope, private $interval: ng.IIntervalService,
            private $q, private dataService: IDataService, signalRService: ISignalRService) {
            const self = this;

            self.device = $scope.device;


            self.loadHomeSeerDevice();

            //self.stop = self.$interval(() => self.loadHomeSeerDevice(), 3000);

            signalRService.addHandler("changeEvent", self.changeEvent.bind(self));
            signalRService.init(() => { console.log("initializing signalRService"); });


            //$scope.$on("$destroy",
            //    () => {
            //        if (angular.isDefined(self.stop)) {
            //            self.$interval.cancel(self.stop);
            //            self.stop = undefined;
            //        }
            //    });
        }

        public changeEvent(refId: number, newValue: string, oldValue: string): void {
            const self = this;

            if (refId === self.device.ReferenceId) {
                console.log("changeEvent invoked. refID: " + refId);
            }
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
                    if (!self.hs) {
                        console.error("unable to get refid: " + self.$scope.device.ReferenceId);
                    }

                    //update self
                    if (self.hs) {
                        self.$scope.device.Value = parseInt(self.hs.value);
                    }
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
            const directive = () => {
                return new DeviceTile();
            };
            directive["$inject"] = [];
            return directive;
        }

    }

 
    app
        .controller("deviceTileController", DeviceTileController)
        .directive("deviceTile", () => new DeviceTile());
}


