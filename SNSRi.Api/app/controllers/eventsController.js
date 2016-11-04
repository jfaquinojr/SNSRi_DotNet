var App;
(function (App) {
    var EventsController = (function () {
        function EventsController($scope, $interval, dataService, notificationService, $window) {
            var _this = this;
            this.$scope = $scope;
            this.$interval = $interval;
            this.dataService = dataService;
            this.notificationService = notificationService;
            this.$window = $window;
            console.log("Inside App.EventsController");
            var self = this;
            this._ = $window._;
            this.loadTickets();
            this.$interval(function () { return _this.loadNewTicketswithinPastMinutes(); }, 3000);
            notificationService.subscribe("#charmEvents", function (event, args) { return _this.openEventsCharm(event, args); }, $scope);
            notificationService.subscribe("changeRoom", function (event, args) { return _this.changeRoom(event, args); }, $scope);
            notificationService.subscribe("CloseTicket", function (event, args) { return _this.closeTicket(event, args); }, $scope);
        }
        EventsController.prototype.closeTicket = function (event, args) {
            console.log(JSON.stringify(args));
            var self = this;
            self.tickets = _.without(self.tickets, _.findWhere(self.tickets, {
                Id: args.ticketId
            }));
        };
        EventsController.prototype.changeRoom = function (event, args) {
            this.reloadTickets(args.roomId);
        };
        EventsController.prototype.openEventsCharm = function (event, args) {
            this.showCharms("#charmEvents");
        };
        EventsController.prototype.editTicket = function (ticket) {
            this.$scope.$emit("EventOpened", ticket);
        };
        EventsController.prototype.loadTickets = function () {
            var _this = this;
            this.dataService.getOpenTickets()
                .then(function (result) {
                _this.tickets = result.data;
            });
        };
        EventsController.prototype.loadTicketsByRoom = function (roomId) {
            var self = this;
            this.dataService.getOpenTicketsByRoom(roomId)
                .then(function (result) {
                self.tickets = result.data;
            });
        };
        EventsController.prototype.reloadTickets = function (roomId) {
            if (roomId > 0) {
                this.loadTicketsByRoom(roomId);
            }
            else {
                this.loadTickets();
            }
        };
        EventsController.prototype.loadNewTicketswithinPastMinutes = function () {
            //console.log("loadNewTicketswithinPastMinutes");
            var self = this;
            this.dataService.getOpenTicketsPastSeconds(4)
                .then(function (result) {
                //console.log("getOpenTicketsPastSeconds. count: " + result.data.length);
                var countBefore = self.tickets.length;
                self.tickets = self._.uniq(self._.union(result.data, self.tickets), false, function (o) { return o.Id; });
                var countAfter = self.tickets.length;
                if (countBefore !== countAfter) {
                    $.Notify({
                        caption: "New Event",
                        content: "An event has occurred.",
                        type: "info"
                    });
                }
            });
        };
        EventsController.prototype.closeDialog = function (id) {
            var dialog = $(id).data("dialog");
            dialog.close();
        };
        EventsController.prototype.openDialog = function (id) {
            var dialog = $(id).data("dialog");
            dialog.open();
        };
        EventsController.prototype.showCharms = function (id) {
            var charm = $(id).data("charm");
            if (charm.element.data("opened") === true) {
                charm.close();
            }
            else {
                charm.open();
            }
        };
        EventsController.$inject = ["$scope", "$interval", "dataService", "notificationService", "$window"];
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
app.directive("popupShowActivities", function () {
    return {
        templateUrl: "Home/PopupShowActivities",
        restrict: "E",
        controller: function ($scope, dataService) {
            $scope.comment = "";
            $scope.addActivity = function (comment) {
                var activityData = createActivity($scope.ticket, comment);
                dataService.createActivity(activityData)
                    .then(function (result) {
                    $scope.ticket.Activities.unshift({
                        TicketId: $scope.ticket.Id,
                        Comment: comment,
                        CreatedOn: new Date(),
                        CreatedBy: 1
                    });
                    $scope.comment = "";
                    $.Notify({
                        caption: "Success",
                        content: "Activity created.",
                        type: "success"
                    });
                }, function (error) {
                    $.Notify({
                        caption: "Create Activity Failed",
                        content: error,
                        type: "alert"
                    });
                });
            };
            $scope.closeTicket = function (comment) {
                var activityData = createActivity($scope.ticket, comment);
                dataService.closeTicket(activityData)
                    .then(function (result) {
                    $scope.ticket.Activities.unshift({
                        TicketId: $scope.ticket.Id,
                        Comment: comment,
                        CreatedOn: new Date(),
                        CreatedBy: 1
                    });
                    $scope.comment = "";
                    $scope.$emit("TicketClosed");
                    $scope.closeDialog("#dialog-activities");
                    $.Notify({
                        caption: "Success",
                        content: "Ticket closed.",
                        type: "success"
                    });
                }, function (error) {
                    $.Notify({
                        caption: "Unable to close Ticket",
                        content: error,
                        type: "alert"
                    });
                });
            }; // closeTicket
            $scope.$on("OpenEvent", function (data) {
                //alert("Child EditEvent: " + JSON.stringify(data));
                //$scope.Ticket = data;
                loadActivitiesFor($scope.ticket)
                    .then(function () {
                    //$scope.openDialog("#dialog-activities");
                    $scope.hideMetroDialog("#dialog-activities");
                    $scope.showMetroDialog("#dialog-activities", null);
                });
            });
            function loadActivitiesFor(ticket) {
                var retval = [];
                return dataService.getActivitiesForTicket(ticket.Id)
                    .then(function (result) {
                    $scope.ticket = ticket;
                    $scope.ticket.Activities = result.data;
                });
            }
        } // controller
    };
}); //directive: popupShowActivities
//# sourceMappingURL=eventsController.js.map