module App {

    import Resident = Data.Contracts.Resident;
    import Room = Data.Contracts.Room;

    export class ResidentsController {

        public residents: Resident[];
        public editingResident: Resident;
        public selectedRoom: Room;
        public Rooms: Room[];
        public vm: any;
        public loadingIndicator: any;
        public loadingRoomsIndicator: any;
        public selectedTab: number;

        static $inject = ["$scope", "$window", "$location", "$http"];

        constructor(public $scope: any, private $window: any, private $location: ng.ILocationService, private $http: ng.IHttpService) {
            const self = this;
            self.vm = $scope;
            self.selectedTab = 1;
            self.loadResidents();
            self.loadRooms();
            //self.$scope.selectedRoom = {} as Resident;
        }


        private loadResidents() {
            const self = this;

            self.loadingIndicator = self.$http.get("/api/Residents")
                .then(result => {
                    self.residents = result.data as Resident[];
                });
        }

        private loadRooms() {
            const self = this;

            self.loadingRoomsIndicator = self.$http.get("/api/Rooms")
                .then(result => {
                    self.Rooms = result.data as Room[];
                });
        }

        deleteRoom(resident: Resident): void {

            if (!confirm("Are you sure you want to delete this record?"))
                return;

            const self = this;
            this.$scope.selectedRoom = null;
            this.editingResident = null;
            self.loadingIndicator = self.$http.post(`/api/DeleteResident/${resident.Id}`, resident)
                .then(result => {
                    $.Notify({
                        caption: "Deleted.",
                        content: "Resident has been deleted.",
                        type: "success"
                    });

                    self.loadResidents();
                });
        }

        createResident(): void {
            this.selectedTab = 1;
            this.$scope.selectedRoom = null;
            this.editingResident = new Resident();
            this.$window.showMetroDialog("#dialog-residentform", null);
        }

        editResident(resident: Resident): void {
            this.selectedTab = 1;
            this.$scope.selectedRoom = resident;
            if (!angular.equals(resident, this.editingResident)) {
                this.editingResident = angular.copy(resident, this.editingResident);
            }
            
            this.$window.showMetroDialog("#dialog-residentform", null);
        }

        saveResident() {
            const resident = this.editingResident;
            const self = this;
            resident.UIRoomId = resident.UIRoom.Id;
            if (this.$scope.selectedRoom) {
                self._updateRoom(resident);
            } else {
                self._createRoom(resident);
            }
        }

        selectTab(tab: number): void {
            this.selectedTab = tab;
        }


        private _updateRoom(resident: Resident) {
            const self = this;
            self.loadingIndicator = self.$http.post(`/api/UpdateResident/`, resident)
                .then(result => {
                    $.Notify({
                        caption: "Saved.",
                        content: "Resident has been updated.",
                        type: "success"
                    });

                    if (!angular.equals(resident, self.$scope.selectedRoom)) {
                        self.$scope.selectedRoom = angular.copy(resident, self.$scope.selectedRoom);
                    }

                    self.$window.hideMetroDialog("#dialog-residentform");
                })
                .catch(reason => {
                    $.Notify({
                        caption: "Error",
                        content: "Resident was not saved. " + reason,
                        type: "error"
                    });
                });
        }


        private _createRoom(resident: Resident) {
            const self = this;
            this.loadingIndicator = self.$http.post(`/api/CreateResident/`, resident)
                .then(result => {

                    resident.Id = result.data as number;
                    self.residents.push(resident);
                    self.$scope.selectedRoom = null;

                    $.Notify({
                        caption: "Created.",
                        content: "Resident has been created.",
                        type: "success"
                    });

                    self.$window.hideMetroDialog("#dialog-residentform");
                })
                .catch(reason => {
                    $.Notify({
                        caption: "Error",
                        content: "Resident was not created. " + reason,
                        type: "error"
                    });
                });
        }
    }

    angular.module("app")
        .controller("residentsController", ResidentsController);


}
