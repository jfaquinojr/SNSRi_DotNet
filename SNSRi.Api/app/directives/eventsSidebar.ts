module App.Directives {

    import Ticket = Data.Contracts.Ticket;

    export class EventSidebarController {

        tickets: Ticket[];
        private _: any;
        private isOpen: boolean;

        static $inject = ["$scope", "$interval", "dataService", "notificationService", "$window", "signalRService"];

        constructor(
            private $scope: ng.IScope,
            private $interval: ng.IIntervalService,
            private dataService: DataService,
            private notificationService: INotificationService,
            private $window: any,
            private signalRService: ISignalRService) {

            const self = this;
            this._ = $window._;
            self.isOpen = false;

            this.loadTickets();

            notificationService.subscribe("#charmEvents", (event, args) => this.openEventsCharm(event, args), $scope);
            notificationService.subscribe("changeRoom", (event, args) => this.changeRoom(event, args), $scope);
            notificationService.subscribe("CloseTicket",
                (event, ticket: Ticket) => this.closeTicket(event, ticket),
                $scope);

            signalRService.addHandler("changeEvent", "EventSidebarController", self.changeEvent.bind(self));
            signalRService.addHandler("transmitEmergency", "EventSidebarController", self.transmitEmergency.bind(self));
            signalRService.init("EventSidebarController", () => { console.log("initializing signalR for SideBar"); });

            $scope.$on("$destroy",
                () => {
                    signalRService.stop("EventSidebarController", () => { console.info("destroying signalRService for SideBar"); });
                });
        }

        public changeEvent(response: any): void {
            const self = this;
            const refId = response.ReferenceId;
            const newValue = response.NewValue;
            const oldValue = response.OldValue;
            $.Notify({
                caption: "Device status Changed",
                content: "A device with Id " + refId + " has changed value from " + oldValue + " to " + newValue + ".",
                type: "info"
            });
        }

        public transmitEmergency(response: any): void {
            const self = this;
            self.loadTickets();
        }

        private closeTicket(event: any, ticket: Ticket): void {
            const self = this;
            self.loadTickets();
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

        private toggleCharms(id: string): void {
            const charm = $(id).data("charm");
            if (charm.element.data("opened") === true) {
                charm.close();
            } else {
                charm.open();
            }
        }

        private showCharms(id: string): void {
            const charm = $(id).data("charm");
            if (charm.element.data("opened") !== true) {
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