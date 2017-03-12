module App {
    import Ticket = Data.Contracts.Ticket;
    import Activity = Data.Contracts.Activity;


    export interface IPopupShowActivitiesController {
        addActivity(comment: string): void;
        closeTicket(comment: string): void;

    }

    export class PopupShowActivitiesController implements IPopupShowActivitiesController {

        ticket: Ticket;
        activities: Activity[];
        comment: string;
        private sourceId = "PopupShowActivitiesController";

        static $inject = ["$scope", "$window", "dataService", "notificationService", "signalRService"];
        constructor(private $scope, private $window: any, private dataService: IDataService, private notificationService: INotificationService,
            private signalRService: ISignalRService) {

            const self = this;

            notificationService.subscribe("editTicketEvent", (event, args) => this.eventOpened(event, args), $scope);

            signalRService.addHandler("transmitEmergency", self.sourceId, self.transmitEmergency.bind(self));
            signalRService.init(self.sourceId, () => { console.log("initializing signalRService for PopupShowActivitiesController"); });

            $scope.$on("$destroy",
                () => {
                    signalRService.stop("EventSidebarController", () => { console.info("destroying signalRService for PopupShowActivitiesController"); });
                });
        }

        private transmitEmergency(response): void {
            const self = this;
            self.eventOpened(null, response);
        }

        addActivity(comment: string): void {
            const self = this;
            const newActivity = this.createActivity(self.ticket, comment);
            this.dataService.createActivity(newActivity)
                .then(result => {

                    const activityId = result.data;


                    // add the newly added activity on our list
                    self.activities.unshift({
                        Id: activityId,
                        TicketId: self.ticket.Id,
                        Comment: comment,
                        CreatedOn: new Date(),
                        CreatedBy: 1,
                        ModifiedOn: new Date(),
                        ModifiedBy: 1
                    } as Activity);

                    self.comment = "";


                    $.Notify({
                        caption: "Success",
                        content: "Activity created.",
                        type: "success"
                    });
                })
                .catch(reason => {
                    $.Notify({
                        caption: "Create Activity Failed",
                        content: reason,
                        type: "alert"
                    });
                });
        }

        closeTicket(comment: string): void {
            const self = this;
            const newActivity = this.createActivity(self.ticket, comment);
            this.dataService.closeTicket(newActivity)
                .then(result => {

                    self.comment = "";

                    self.notificationService.notify("CloseTicket", self.ticket);

                    $.Notify({
                        caption: "Success",
                        content: "Ticket closed.",
                        type: "success"
                    });
                })
                .catch(error => {
                    $.Notify({
                        caption: "Unable to close Ticket",
                        content: error,
                        type: "alert"
                    });
                });
        }

        private eventOpened(event: any, ticket: Ticket) {
            //console.log("eventOpened captured.");
            const self = this;
            self.loadActivities(ticket)
                .then(() => {
                    self.$window.hideMetroDialog("#dialog-activities");
                    self.$window.showMetroDialog("#dialog-activities", null);
                });
        }

        private loadActivities(ticket): ng.IPromise<void> {
            const self = this;
            return self.dataService.getActivitiesForTicket(ticket.Id)
                .then(result => {
                    self.ticket = ticket;
                    self.activities = result.data;
                });
        }

        private createActivity(ticket: Ticket, comment: string): Activity {
            return {
                TicketId: ticket.Id,
                Comment: comment,
                CreatedBy: 1
            } as Activity;
        }

        private getPopupClass(ticket: Ticket) {

            if (!ticket) {
                return;
            }

            if (ticket.Severity === "Emergency") {
                return "popup-emergency";
            }
            if (ticket.Severity === "Warning") {
                return "popup-warning";
            }
            if (ticket.Severity === "Alert") {
                return "popup-alert";
            }
        }
    }

    export function PopupShowActivitiesDirective(): ng.IDirective {

        console.log("Inside App.Directives.Toolbox!");

        return {
            restrict: "E",
            scope: {},
            controller: PopupShowActivitiesController,
            controllerAs: "vm",
            templateUrl: "/Home/PopupShowActivities"
        }
    }

    angular.module("app")
        .directive("popupShowActivities", PopupShowActivitiesDirective)
        .controller("popupShowActivitiesController", PopupShowActivitiesController);
}
