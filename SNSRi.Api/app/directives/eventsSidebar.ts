
module App.Directives {

    import EventsController = App.EventsController;

    export function EventSidebar(): ng.IDirective {

        console.log("Inside App.Directives.EventSidebar!");
        return {
            restrict: "E",
            scope: true,
            controller: EventsController,
            templateUrl: "/Home/EventsCharm"
        }
    }

    angular.module("app")
        .controller("eventsController", EventsController)
        .directive("eventSidebar", EventSidebar);

}