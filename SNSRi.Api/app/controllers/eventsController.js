var App;
(function (App) {
    var EventsController = (function () {
        function EventsController() {
        }
        return EventsController;
    }());
    App.EventsController = EventsController;
})(App || (App = {}));
function createActivity(ticket, comment) {
    return {
        TicketId: ticket.Id,
        Comment: comment,
        CreatedBy: 1
    };
}
app.directive("eventTile", function () {
    return {
        templateUrl: "/Home/EventTile",
        restrict: "E"
    };
});
//# sourceMappingURL=eventsController.js.map