
module App {

    import Ticket = Data.Contracts.Ticket;


    interface IEventsController {
        
    }


    class EventsController implements IEventsController {

        tickets: Ticket[];
        private _: any;

        static $inject = ["$scope", "$interval", "dataService", "startScreenService", "$window"];

        constructor(
            private $scope: ng.IScope,
            private $interval: ng.IIntervalService,
            private dataService: DataService,
            private startScreenService: StartScreenService,
            private $window: any) {

            const self = this;
            this._ = $window._;

            this.loadTickets();

            this.$interval(() => this.loadNewTicketswithinPastMinutes(), 3000);


            $scope.$on("changeRoom",
                (event, roomId: number) => {
                    self.reloadTickets(roomId);
                });

            $scope.$on("CloseTicket",
                (event, ticketId: number) => {

                    self.tickets = _.without(self.tickets,
                        _.findWhere(self.tickets,
                            {
                            Id: ticketId
                        }));
                });

            $scope.$on("OpenEventsCharm",
                () => {
                    startScreenService.showCharms("#charmEvents");
                });

        }


        private editTicket(ticket) {
            this.$scope.$emit("EventOpened", ticket);
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

        public loadNewTicketswithinPastMinutes(): void {

            console.log("loadNewTicketswithinPastMinutes");

            const self = this;
            this.dataService.getOpenTicketsPastSeconds(4)
                .then(result => {
                    console.log("getOpenTicketsPastSeconds. count: " + result.data.length);
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

    }

    angular.module("app")
    .service("dataService", App.DataService)
    .controller("eventsController", EventsController);

}


function createActivity(ticket, comment) {
    return {
        TicketId: ticket.Id,
        Comment: comment,
        CreatedBy: 1
    };
}



app.directive("eventSidebar",
    function () {

        return {
            templateUrl: "/Home/EventsCharm",
            restrict: "E"
            //controller: function($scope) {
            //    $scope.Clicked = function(device) {
            //        alert(JSON.stringify(device));
            //    }
            //}
        };

    });

app.directive("eventTile",
    function () {

        return {
            templateUrl: "/Home/EventTile",
            restrict: "E"
            //controller: function($scope) {
            //    $scope.Clicked = function(device) {
            //        alert(JSON.stringify(device));
            //    }
            //}
        };

    });

app.directive("popupShowActivities",
    function () {

        return {
            templateUrl: "Home/PopupShowActivities",
            restrict: "E",
            controller: function ($scope, dataService) {

                $scope.comment = "";

                $scope.addActivity = comment => {

                    var activityData = createActivity($scope.ticket, comment);

                    dataService.createActivity(activityData)
                        .then(
                            result => {
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
                            },
                            error => {
                                $.Notify({
                                    caption: "Create Activity Failed",
                                    content: error,
                                    type: "alert"
                                });
                            });
                }

                $scope.closeTicket = comment => {

                    var activityData = createActivity($scope.ticket, comment);

                    dataService.closeTicket(activityData)
                        .then(
                            function (result) {

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
                            },
                            function (error) {
                                $.Notify({
                                    caption: "Unable to close Ticket",
                                    content: error,
                                    type: "alert"
                                });
                            });
                } // closeTicket

                $scope.$on("OpenEvent", function (data) {
                    //alert("Child EditEvent: " + JSON.stringify(data));
                    //$scope.Ticket = data;
                    loadActivitiesFor($scope.ticket)
                        .then(function() {
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
        }

    }); //directive: popupShowActivities


