module App {

    import Room = Data.Contracts.Room;

    export class RoomsController {

        rooms: Room[];
        selectedRoom: Room;
        editingRoom: Room;
        vm: any;

        static $inject = ["$scope", "$window", "$location", "usersDataService"];

        constructor(public $scope, private $window, private $location, private usersDataService: IUsersDataService) {
            console.log("initializing RoomsController");

            const self = this;
            self.vm = $scope;

            this.loadRooms();
        }


        private loadRooms() {
            const self = this;
            this.usersDataService.getAllUsers()
                .then(result => {
                    //self.rooms = result.data;
                });

            self.rooms = [
                { Id: 1, Name: "Room1", Description: "Desc for Room1", SortOrder: 1, IsHidden: false, CreatedOn: null, CreatedBy: 1, ModifiedOn: null, ModifiedBy: 1, Devices: [] },
                { Id: 2, Name: "Room2", Description: "Desc for Room2", SortOrder: 2, IsHidden: false, CreatedOn: null, CreatedBy: 1, ModifiedOn: null, ModifiedBy: 1, Devices: [] }
            ];

        }
    }

    angular.module("app")
        .controller("roomsController", RoomsController);


}
