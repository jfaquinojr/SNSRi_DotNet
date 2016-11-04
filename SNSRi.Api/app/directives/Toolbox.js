var App;
(function (App) {
    function Toolbox() {
        console.log("Inside App.Directives.Toolbox!");
        return {
            restrict: "E",
            scope: true,
            controller: App.ToolboxController,
            controllerAs: "vm",
            templateUrl: "/Home/Toolbox"
        };
    }
    App.Toolbox = Toolbox;
    angular.module("app")
        .directive("toolbox", Toolbox);
})(App || (App = {}));
//# sourceMappingURL=Toolbox.js.map