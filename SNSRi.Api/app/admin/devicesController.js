var App;
(function (App) {
    var DevicesController = (function () {
        function DevicesController($scope, $window, $location, usersDataService) {
            this.$scope = $scope;
            this.$window = $window;
            this.$location = $location;
            this.usersDataService = usersDataService;
            console.log("initializing DevicesController");
            var self = this;
            self.vm = $scope;
            this.loadDevices();
        }
        DevicesController.prototype.loadDevices = function () {
            var self = this;
            this.usersDataService.getAllUsers()
                .then(function (result) {
                //self.rooms = result.data;
            });
            self.devices = [
                { Id: 1, Name: "Device 1", ReferenceId: 1, Status: "ON", Value: "100", HideFromView: false, CreatedOn: null, CreatedBy: 1, ModifiedOn: null, ModifiedBy: 1 },
                { Id: 2, Name: "Device 2", ReferenceId: 2, Status: "OFF", Value: "0", HideFromView: false, CreatedOn: null, CreatedBy: 1, ModifiedOn: null, ModifiedBy: 1 },
                { Id: 3, Name: "Device 3", ReferenceId: 3, Status: "ON", Value: "100", HideFromView: false, CreatedOn: null, CreatedBy: 1, ModifiedOn: null, ModifiedBy: 1 },
            ];
        };
        DevicesController.$inject = ["$scope", "$window", "$location", "usersDataService"];
        return DevicesController;
    }());
    App.DevicesController = DevicesController;
    angular.module("app")
        .controller("devicesController", DevicesController);
})(App || (App = {}));
//# sourceMappingURL=devicesController.js.map