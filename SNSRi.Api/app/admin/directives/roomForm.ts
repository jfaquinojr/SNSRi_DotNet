module App {

    export function RoomForm(): ng.IDirective {

        return {
            restrict: "E",
            scope: true,
            templateUrl: "/Rooms/Edit"
        }
    }

    angular.module("app")
        .directive("roomForm", RoomForm);
}