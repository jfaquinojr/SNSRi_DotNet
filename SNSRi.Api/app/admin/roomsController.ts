module App {

    import Room = Data.Contracts.Room;

    export class RoomsController {

        rooms: Room[];
        selectedRoom: Room;
        editingRoom: Room;
        vm: any;
        loadingIndicator: any;

        static $inject = ["$scope", "$window", "$location", "roomsDataService"];

        constructor(public $scope, private $window, private $location, private roomsDataService: IRoomsDataService) {
            console.log("initializing RoomsController");

            const self = this;
            self.vm = $scope;

            this.loadRooms();
        }


        private loadRooms() {
            const self = this;
            this.loadingIndicator = this.roomsDataService.getAllRooms()
                .then(result => {
                    self.rooms = result.data;
                });
        }

        deleteRoom(room: Room): void {

            if (!confirm("Are you sure you want to delete this record?"))
                return;

            const self = this;
            this.selectedRoom = null;
            this.editingRoom = null;
            this.loadingIndicator = this.roomsDataService.deleteRoom(room.Id)
                .then(() => {
                    $.Notify({
                        caption: "Deleted.",
                        content: "Room has been deleted.",
                        type: "success"
                    });

                    self.loadRooms();
                });
        }

        createRoom(): void {
            this.selectedRoom = null;
            this.editingRoom = new Room();
            this.$window.showMetroDialog("#dialog-roomform", null);
        }

        editRoom(room: Room): void {
            this.selectedRoom = room;
            if (!angular.equals(room, this.editingRoom)) {
                this.editingRoom = angular.copy(room, this.editingRoom);
            }
            
            this.$window.showMetroDialog("#dialog-roomform", null);
        }

        saveRoom() {
            const room = this.editingRoom;
            const self = this;

            if (this.selectedRoom) {
                self._updateRoom(room);
            } else {
                self._createRoom(room);
            }
        }

        private _updateRoom(room: Room) {
            const self = this;
            this.loadingIndicator = this.roomsDataService.updateRoom(room)
                .then(result => {

                    
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
                .catch(reason => {
                    $.Notify({
                        caption: "Error",
                        content: "Room was not saved. " + reason,
                        type: "error"
                    });
                });
        }

        private _createRoom(room: Room) {
            const self = this;
            this.loadingIndicator = this.roomsDataService.createRoom(room)
                .then(result => {

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
                .catch(reason => {
                    $.Notify({
                        caption: "Error",
                        content: "Room was not created. " + reason,
                        type: "error"
                    });
                });
        }
    }

    angular.module("app")
        .controller("roomsController", RoomsController);


}
