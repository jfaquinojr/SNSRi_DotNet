module App {


    export function Toolbox(): ng.IDirective {

        console.log("Inside App.Directives.Toolbox!");

        return {
            restrict: "E",
            scope: true,
            controller: ToolboxController,
            controllerAs: "vm",
            templateUrl: "/Home/Toolbox"
        }
    }


    angular.module("app")
        .directive("toolbox", Toolbox);

}