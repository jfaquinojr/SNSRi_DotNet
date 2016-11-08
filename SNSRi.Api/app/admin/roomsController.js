var App;
(function (App) {
    var Room = Data.Contracts.Room;
    var RoomsController = (function () {
        function RoomsController($scope, $window, $location, roomsDataService) {
            this.$scope = $scope;
            this.$window = $window;
            this.$location = $location;
            this.roomsDataService = roomsDataService;
            console.log("initializing RoomsController");
            var self = this;
            self.vm = $scope;
            this.loadRooms();
        }
        RoomsController.prototype.loadRooms = function () {
            var self = this;
            this.loadingIndicator = this.roomsDataService.getAllRooms()
                .then(function (result) {
                self.rooms = result.data;
            });
        };
        RoomsController.prototype.deleteRoom = function (room) {
            if (!confirm("Are you sure you want to delete this record?"))
                return;
            var self = this;
            this.selectedRoom = null;
            this.editingRoom = null;
            this.loadingIndicator = this.roomsDataService.deleteRoom(room.Id)
                .then(function () {
                $.Notify({
                    caption: "Deleted.",
                    content: "Room has been deleted.",
                    type: "success"
                });
                self.loadRooms();
            });
        };
        RoomsController.prototype.createRoom = function () {
            this.selectedRoom = null;
            this.editingRoom = new Room();
            this.$window.showMetroDialog("#dialog-roomform", null);
        };
        RoomsController.prototype.editRoom = function (room) {
            this.selectedRoom = room;
            if (!angular.equals(room, this.editingRoom)) {
                this.editingRoom = angular.copy(room, this.editingRoom);
            }
            this.$window.showMetroDialog("#dialog-roomform", null);
        };
        RoomsController.prototype.saveRoom = function () {
            var room = this.editingRoom;
            var self = this;
            if (this.selectedRoom) {
                self._updateRoom(room);
            }
            else {
                self._createRoom(room);
            }
        };
        RoomsController.prototype._updateRoom = function (room) {
            var self = this;
            this.loadingIndicator = this.roomsDataService.updateRoom(room)
                .then(function (result) {
                if (!angular.equals(room, self.selectedRoom)) {
                    self.selectedRoom = angular.copy(room, self.selectedRoom);
                }
                $.Notify({
                    caption: "Saved.",
                    content: "Room has been updated.",
                    type: "success"
                });
                self.$window.hideMetroDialog("#dialog-roomform");
            })
                .catch(function (reason) {
                $.Notify({
                    caption: "Error",
                    content: "Room was not saved. " + reason,
                    type: "error"
                });
            });
        };
        RoomsController.prototype._createRoom = function (room) {
            var self = this;
            this.loadingIndicator = this.roomsDataService.createRoom(room)
                .then(function (result) {
                room.Id = result.data;
                self.rooms.push(room);
                self.selectedRoom = null;
                $.Notify({
                    caption: "Created.",
                    content: "Room has been created.",
                    type: "success"
                });
                self.$window.hideMetroDialog("#dialog-roomform");
            })
                .catch(function (reason) {
                $.Notify({
                    caption: "Error",
                    content: "Room was not created. " + reason,
                    type: "error"
                });
            });
        };
        RoomsController.$inject = ["$scope", "$window", "$location", "roomsDataService"];
        return RoomsController;
    }());
    App.RoomsController = RoomsController;
    angular.module("app")
        .controller("roomsController", RoomsController);
})(App || (App = {}));
//# sourceMappingURL=roomsController.js.map