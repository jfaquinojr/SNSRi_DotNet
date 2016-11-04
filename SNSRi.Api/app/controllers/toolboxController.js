var App;
(function (App) {
    var ToolboxController = (function () {
        function ToolboxController(notificationService) {
            this.notificationService = notificationService;
            this.vm = this;
            console.log("inside App.ToolboxController..");
        }
        ToolboxController.prototype.openThemes = function () {
            console.log("openThemes");
            this.notificationService.notify("#charmThemes", {});
        };
        ToolboxController.prototype.openEvents = function () {
            console.log("openEvents");
            this.notificationService.notify("#charmEvents", {});
        };
        ToolboxController.$inject = ["notificationService"];
        return ToolboxController;
    }());
    App.ToolboxController = ToolboxController;
    angular.module("app")
        .controller("toolboxController", ["$scope", "notificationService", ToolboxController]);
})(App || (App = {}));
//# sourceMappingURL=toolboxController.js.map