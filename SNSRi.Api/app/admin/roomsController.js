var App;
(function (App) {
    var RoomsController = (function () {
        function RoomsController($scope, $window, $location, usersDataService) {
            this.$scope = $scope;
            this.$window = $window;
            this.$location = $location;
            this.usersDataService = usersDataService;
            console.log("initializing RoomsController");
            var self = this;
            self.vm = $scope;
            this.loadRooms();
        }
        RoomsController.prototype.loadRooms = function () {
            var self = this;
            this.usersDataService.getAllUsers()
                .then(function (result) {
                //self.rooms = result.data;
            });
            self.rooms = [
                { Id: 1, Name: "Room1", Description: "Desc for Room1", SortOrder: 1, IsHidden: false, CreatedOn: null, CreatedBy: 1, ModifiedOn: null, ModifiedBy: 1, Devices: [] },
                { Id: 2, Name: "Room2", Description: "Desc for Room2", SortOrder: 2, IsHidden: false, CreatedOn: null, CreatedBy: 1, ModifiedOn: null, ModifiedBy: 1, Devices: [] }
            ];
        };
        RoomsController.$inject = ["$scope", "$window", "$location", "usersDataService"];
        return RoomsController;
    }());
    App.RoomsController = RoomsController;
    angular.module("app")
        .controller("roomsController", RoomsController);
})(App || (App = {}));
//# sourceMappingURL=roomsController.js.map