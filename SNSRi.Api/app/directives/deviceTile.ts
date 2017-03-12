module App {
    import Device = Data.Contracts.Device;
    import HomeSeerDevice = Data.HomeSeer.HomeSeerDevice;

    export class DeviceTileController {

        public vm = this;
        public HomeSeerUrl: string;
        public hs: HomeSeerDevice;
        public device: Device;
        public oldValue: string;
        public newValue: string;
        private sourceId: string;

        static $inject = ["$scope", "$interval", "$q", "dataService", "signalRService"];
        constructor(private $scope, private $interval: ng.IIntervalService,
            private $q, private dataService: IDataService, signalRService: ISignalRService) {
            const self = this;

            self.device = $scope.device;
            self.oldValue = self.device.Value;
            self.newValue = self.device.Value;

            self.loadHomeSeerDevice();

            self.sourceId = "device" + self.device.Id;
            signalRService.addHandler("changeEvent", self.sourceId, self.changeEvent.bind(self));
            signalRService.init(self.sourceId, () => { console.log("initializing signalRService for device: " + self.device.Name); });


            $scope.$on("$destroy",
                () => {
                    console.debug("$destroying for device-tile for " + self.device.Name);
                    signalRService.stop(self.sourceId, () => { console.info("destroying signalRService for device: " + self.device.Name) });
                });
        }

        public changeEvent(response: any): void {
            const self = this;
            const refId = response.ReferenceId;
            const newValue = response.NewValue;
            const oldValue = response.OldValue;
            if (refId === self.device.ReferenceId) {
                self.newValue = newValue;
                self.oldValue = oldValue;
                console.log("changeEvent invoked. refID: " + refId + ". old: " + oldValue + " - new: " + newValue);
                self.loadHomeSeerDevice();
            }
        }

        public clicked(device: Device): void {
            const self = this;
            console.debug("sending old value: " + self.oldValue);
            this.dataService.setHomeSeerDevice(device.ReferenceId, self.oldValue);
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
                        if (self.$scope.device.Value === 100) {
                            self.newValue = "100";
                            self.oldValue = "0";
                        }
                        else {
                            self.newValue = "0";
                            self.oldValue = "100";
                        }
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


