module App {

    export class RoomController {

        constructor() {
            
        }
    }

    export function RoomForm(): ng.IDirective {

        return {
            restrict: "E",
            scope: {
                room: "="
            },
            templateUrl: "/Rooms/Edit",
            controller: RoomController,
            controllerAs: "vm"
        }
    }

    angular.module("app")
        .controller("roomController", RoomController)
        .directive("roomForm", RoomForm);
}