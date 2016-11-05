module App.Directives {

    import Ticket = Data.Contracts.Ticket;

    export class EventSidebarController {

        tickets: Ticket[];
        private _: any;

        static $inject = ["$scope", "$interval", "dataService", "notificationService", "$window"];

        constructor(
            private $scope: ng.IScope,
            private $interval: ng.IIntervalService,
            private dataService: DataService,
            private notificationService: INotificationService,
            private $window: any) {

            console.log("Inside App.EventSidebarController");

            const self = this;
            this._ = $window._;

            this.loadTickets();

            this.$interval(() => this.loadNewTicketswithinPastMinutes(), 3000);


            notificationService.subscribe("#charmEvents", (event, args) => this.openEventsCharm(event, args), $scope);
            notificationService.subscribe("changeRoom", (event, args) => this.changeRoom(event, args), $scope);
            notificationService.subscribe("CloseTicket",
                (event, ticket: Ticket) => this.closeTicket(event, ticket),
                $scope);

        }

        private closeTicket(event: any, ticket: Ticket): void {
            const self = this;
            self.tickets = _.without(self.tickets,
                _.findWhere(self.tickets,
                {
                    id: ticket.Id
                }));
        }

        private changeRoom(event: any, args: any): void {
            this.reloadTickets(args.roomId);
        }

        private openEventsCharm(event: any, args: any): void {
            this.showCharms("#charmEvents");
        }


        private editTicket(ticket) {
            this.notificationService.notify("editTicketEvent", ticket);
            this.hideCharm("#charmEvents");
        }


        private loadTickets(): void {
            this.dataService.getOpenTickets()
                .then(result => {
                    this.tickets = result.data;
                });
        }

        private loadTicketsByRoom(roomId): void {
            const self = this;
            this.dataService.getOpenTicketsByRoom(roomId)
                .then(result => {
                    self.tickets = result.data;
                });
        }

        private reloadTickets(roomId) {

            if (roomId > 0) {
                this.loadTicketsByRoom(roomId);
            } else {
                this.loadTickets();
            }

        }


        loadNewTicketswithinPastMinutes(): void {

            //console.log("loadNewTicketswithinPastMinutes");

            const self = this;
            this.dataService.getOpenTicketsPastSeconds(4)
                .then(result => {
                    //console.log("getOpenTicketsPastSeconds. count: " + result.data.length);
                    var countBefore = self.tickets.length;
                    self.tickets = self._.uniq(
                        self._.union(result.data, self.tickets),
                        false,
                        o => { return o.Id }
                    );
                    var countAfter = self.tickets.length;
                    if (countBefore !== countAfter) {
                        $.Notify({
                            caption: "New Event",
                            content: "An event has occurred.",
                            type: "info"
                        });
                    }
                });
        }


        //private closeDialog(id: string): void {
        //    const dialog = $(id).data("dialog");
        //    dialog.close();
        //}

        //private openDialog(id: string): void {
        //    const dialog = $(id).data("dialog");
        //    dialog.open();
        //}

        private showCharms(id: string): void {
            const charm = $(id).data("charm");
            if (charm.element.data("opened") === true) {
                charm.close();
            } else {
                charm.open();
            }
        }

        private hideCharm(id: string): void {
            const charm = $(id).data("charm");
            if (charm.element.data("opened") === true) {
                charm.close();
            }
        }

}

    export function EventSidebar(): ng.IDirective {

        console.log("Inside App.Directives.EventSidebar!");
        return {
            restrict: "E",
            scope: true,
            controllerAs: "vm",
            controller: EventSidebarController,
            templateUrl: "/Home/EventsCharm"
        }
    }

    angular.module("app")
        .controller("eventSidebarController", EventSidebarController)
        .directive("eventSidebar", EventSidebar);

}