module App {

    export function DeviceForm(): ng.IDirective {

        return {
            restrict: "E",
            scope: true,
            templateUrl: "/Devices/Edit"
        }
    }

    angular.module("app")
        .directive("deviceForm", DeviceForm);
}