var App;
(function (App) {
    var Directives;
    (function (Directives) {
        var EventsController = App.EventsController;
        function EventSidebar() {
            console.log("Inside App.Directives.EventSidebar!");
            return {
                restrict: "E",
                scope: true,
                controllerAs: "vm",
                controller: EventsController,
                templateUrl: "/Home/EventsCharm"
            };
        }
        Directives.EventSidebar = EventSidebar;
        angular.module("app")
            .controller("eventsController", EventsController)
            .directive("eventSidebar", EventSidebar);
    })(Directives = App.Directives || (App.Directives = {}));
})(App || (App = {}));
//# sourceMappingURL=eventsSidebar.js.map