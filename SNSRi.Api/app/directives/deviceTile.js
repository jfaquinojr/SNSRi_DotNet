var App;
(function (App) {
    var DeviceTileController = (function () {
        function DeviceTileController($scope, $interval, $q, dataService) {
            this.$scope = $scope;
            this.$interval = $interval;
            this.$q = $q;
            this.dataService = dataService;
            this.vm = this;
            var self = this;
            self.loadHomeSeerDevice();
            self.stop = self.$interval(function () { return self.loadHomeSeerDevice(); }, 3000);
            $scope.$on("$destroy", function () {
                if (angular.isDefined(self.stop)) {
                    self.$interval.cancel(self.stop);
                    self.stop = undefined;
                }
            });
        }
        DeviceTileController.prototype.clicked = function (device) {
            //alert(JSON.stringify(device));
            //toggle device
            var newValue = "0";
            if (device.Value == "0") {
                newValue = "100";
            }
            else {
                newValue = "0";
            }
            this.dataService.setHomeSeerDevice(device.ReferenceId, newValue);
        };
        DeviceTileController.prototype.loadHomeSeerDevice = function () {
            var self = this;
            var promises = [
                self.dataService.getHomeSeerUrl(),
                self.dataService.getHomeSeerDevice(self.$scope.device.ReferenceId)
            ];
            self.$q.all(promises)
                .then(function (values) {
                self.HomeSeerUrl = values[0].data;
                self.hs = values[1].data.Devices[0];
                //update self
                self.$scope.device.Value = parseInt(self.hs.value);
                console.log("loadHomeSeerDevice");
            });
        };
        DeviceTileController.inject = ["$scope", "$interval", "$q", "dataService"];
        return DeviceTileController;
    }());
    App.DeviceTileController = DeviceTileController;
    var DeviceTile = (function () {
        function DeviceTile() {
            this.templateUrl = "/Home/DeviceTile";
            this.restrict = "E";
            this.scope = {
                device: "="
            };
            this.controller = DeviceTileController;
            this.controllerAs = "vm";
        }
        DeviceTile.Factory = function () {
            var directive = function () {
                return new DeviceTile();
            };
            directive["$inject"] = [];
            return directive;
        };
        return DeviceTile;
    }());
    App.DeviceTile = DeviceTile;
    app
        .controller("deviceTileController", DeviceTileController)
        .directive("deviceTile", DeviceTile.Factory());
})(App || (App = {}));
//# sourceMappingURL=deviceTile.js.map