module App {

    export class ResidentController {

        constructor() {

        }
    }

    export function ResidentForm(): ng.IDirective {

        return {
            restrict: "E",
            templateUrl: "/Residents/Edit"
        }
    }

    angular.module("app")
        .controller("residentController", ResidentController)
        .directive("residentForm", ResidentForm);
}