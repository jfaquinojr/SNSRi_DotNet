var App;
(function (App) {
    var Directives;
    (function (Directives) {
        var EventSidebarController = (function () {
            function EventSidebarController($scope, $interval, dataService, notificationService, $window) {
                var _this = this;
                this.$scope = $scope;
                this.$interval = $interval;
                this.dataService = dataService;
                this.notificationService = notificationService;
                this.$window = $window;
                console.log("Inside App.EventSidebarController");
                var self = this;
                this._ = $window._;
                this.loadTickets();
                this.$interval(function () { return _this.loadNewTicketswithinPastMinutes(); }, 3000);
                notificationService.subscribe("#charmEvents", function (event, args) { return _this.openEventsCharm(event, args); }, $scope);
                notificationService.subscribe("changeRoom", function (event, args) { return _this.changeRoom(event, args); }, $scope);
                notificationService.subscribe("CloseTicket", function (event, ticket) { return _this.closeTicket(event, ticket); }, $scope);
            }
            EventSidebarController.prototype.closeTicket = function (event, ticket) {
                var self = this;
                self.tickets = _.without(self.tickets, _.findWhere(self.tickets, {
                    id: ticket.Id
                }));
            };
            EventSidebarController.prototype.changeRoom = function (event, args) {
                this.reloadTickets(args.roomId);
            };
            EventSidebarController.prototype.openEventsCharm = function (event, args) {
                this.showCharms("#charmEvents");
            };
            EventSidebarController.prototype.editTicket = function (ticket) {
                this.notificationService.notify("editTicketEvent", ticket);
                this.hideCharm("#charmEvents");
            };
            EventSidebarController.prototype.loadTickets = function () {
                var _this = this;
                this.dataService.getOpenTickets()
                    .then(function (result) {
                    _this.tickets = result.data;
                });
            };
            EventSidebarController.prototype.loadTicketsByRoom = function (roomId) {
                var self = this;
                this.dataService.getOpenTicketsByRoom(roomId)
                    .then(function (result) {
                    self.tickets = result.data;
                });
            };
            EventSidebarController.prototype.reloadTickets = function (roomId) {
                if (roomId > 0) {
                    this.loadTicketsByRoom(roomId);
                }
                else {
                    this.loadTickets();
                }
            };
            EventSidebarController.prototype.loadNewTicketswithinPastMinutes = function () {
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
            //private closeDialog(id: string): void {
            //    const dialog = $(id).data("dialog");
            //    dialog.close();
            //}
            //private openDialog(id: string): void {
            //    const dialog = $(id).data("dialog");
            //    dialog.open();
            //}
            EventSidebarController.prototype.showCharms = function (id) {
                var charm = $(id).data("charm");
                if (charm.element.data("opened") === true) {
                    charm.close();
                }
                else {
                    charm.open();
                }
            };
            EventSidebarController.prototype.hideCharm = function (id) {
                var charm = $(id).data("charm");
                if (charm.element.data("opened") === true) {
                    charm.close();
                }
            };
            EventSidebarController.$inject = ["$scope", "$interval", "dataService", "notificationService", "$window"];
            return EventSidebarController;
        }());
        Directives.EventSidebarController = EventSidebarController;
        function EventSidebar() {
            console.log("Inside App.Directives.EventSidebar!");
            return {
                restrict: "E",
                scope: true,
                controllerAs: "vm",
                controller: EventSidebarController,
                templateUrl: "/Home/EventsCharm"
            };
        }
        Directives.EventSidebar = EventSidebar;
        angular.module("app")
            .controller("eventSidebarController", EventSidebarController)
            .directive("eventSidebar", EventSidebar);
    })(Directives = App.Directives || (App.Directives = {}));
})(App || (App = {}));
//# sourceMappingURL=eventsSidebar.js.map