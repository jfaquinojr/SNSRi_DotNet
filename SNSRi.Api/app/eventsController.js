var app = angular.module("app");

app.controller("EventsController",
    function($scope, dataService, $interval) {

        $scope.Tickets = [];

        function loadTickets() {
            dataService.getOpenTickets()
                .then(function(result) {
                    $scope.Tickets = result.data;

                    console.log("loadTickets. loaded " + result.data.length + " recrods.");
                });
        }

        function loadTicketsByRoom(roomId) {
            dataService.getOpenTicketsByRoom(roomId)
                .then(function(result) {
                    $scope.Tickets = result.data;

                    console.log("loadTicketsByRoom. loaded " + result.data.length + " recrods.");
                });
        }

        loadTickets();


        var reloadTickets = function(roomId) {

            if (roomId > 0) {
                loadTicketsByRoom(roomId);
            } else {
                loadTickets();
            }

        }

        $scope.$on("changeRoom",
            function(event, roomId) {
                reloadTickets(roomId);
            });

        $scope.$on("CloseTicket",
            function (event, ticketId) {
                console.log("CloseTicket event triggered...");
                $scope.Tickets = _.without($scope.Tickets, _.findWhere($scope.Tickets, {
                    Id: ticketId
                }));
            });

        $scope.$on("OpenEventsCharm",
             function () {
                 $scope.showCharms("#charmEvents");
             });
        

        $scope.editTicket = function(ticket)
        {
            $scope.$emit("EventOpened", ticket);
        }




        function loadNewTicketswithinPastMinutes() {

            console.log("loadNewTicketswithinPastMinutes");

            dataService.getOpenTicketsPastSeconds(4).then(function (result) {
                console.log("getOpenTicketsPastSeconds. count: " + result.data.length);
                var countBefore = $scope.Tickets.length;
                $scope.Tickets = _.uniq(_.union(result.data, $scope.Tickets), false, function (o) { return o.Id });
                var countAfter = $scope.Tickets.length;
                if (countBefore !== countAfter) {
                    $.Notify({
                        caption: "New Event",
                        content: "An event has occurred.",
                        type: "info"
                    });
                }
            });
        }

        $interval(loadNewTicketswithinPastMinutes, 3000);

    });



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

                $scope.addActivity = function (comment) {

                    var activityData = createActivity($scope.ticket, comment);

                    dataService.createActivity(activityData)
                        .then(
                            function (result) {
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
                            function (error) {
                                $.Notify({
                                    caption: "Create Activity Failed",
                                    content: error,
                                    type: "alert"
                                });
                            });
                }

                $scope.closeTicket = function (comment) {

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
                            hideMetroDialog("#dialog-activities");
                            showMetroDialog("#dialog-activities", null);
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


