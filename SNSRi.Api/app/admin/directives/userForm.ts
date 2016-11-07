module App {

    export function UserForm(): ng.IDirective {

        return {
            restrict: "E",
            scope: true,
            templateUrl: "/Users/Edit"
        }
    }

    angular.module("app")
        .directive("userForm", UserForm);
}