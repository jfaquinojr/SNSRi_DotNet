module App {

    export class RoomController {

        constructor() {
            
        }
    }

    export function RoomForm(): ng.IDirective {

        return {
            restrict: "E",
            templateUrl: "/Rooms/Edit"
        }
    }

    angular.module("app")
        .controller("roomController", RoomController)
        .directive("roomForm", RoomForm);
}