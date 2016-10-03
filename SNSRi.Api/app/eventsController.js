var app = angular.module("app");

app.controller("EventsController",
    function($scope, dataService) {

        $scope.Tickets = [];
        $scope.Ticket = {};

        function loadTickets() {
            dataService.getOpenTickets()
                .then(function(result) {
                    $scope.Tickets = result.data;

                    console.log("loadTickets. loaded " + result.data.length + " recrods.");
                });
        }

        function loadTicketsByRoom(roomId) {
            dataService.getOpenTicketsByRoom(roomId)
               .then(function (result) {
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
                console.log("changeRoom fired! roomId: " + roomId);
                reloadTickets(roomId);
            });

        $scope.editTicket = function(ticket)
        {
            $scope.Ticket = ticket;
            loadActivitiesFor(ticket);
            openDialog(ticket.Id);
        }


        function loadActivitiesFor(ticket) {
            var retval = [];
            dataService.getActivitiesForTicket(ticket.Id)
                .then(function(result) {
                    $scope.Ticket = ticket;
                    $scope.Ticket.Activities = result.data;
                });
        }

    });

function openDialog(ticketId) {
    var dialog = $("#dialog-" + ticketId).data("dialog");
    dialog.open();
}

function createActivity(ticket, comment) {
    return {
        TicketId: ticket.Id,
        Comment: comment,
        CreatedBy: 1
    };
}

function closeDialog() {
    var dialog = $("#dialog").data("dialog");
    dialog.close();
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
            scope: {
                ticket: "="
            },
            controller: function ($scope, $http) {

                $scope.comment = "";

                $scope.addActivity = function (comment) {

                    var activityData = createActivity($scope.ticket, comment);

                    $http.post(global.api.url + "CreateActivity", JSON.stringify(activityData))
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

                    $http.post(global.api.url + "CloseTicket", JSON.stringify(activityData))
                        .then(
                            function (result) {

                                $scope.ticket.Activities.unshift({
                                    TicketId: $scope.ticket.Id,
                                    Comment: comment,
                                    CreatedOn: new Date(),
                                    CreatedBy: 1
                                });

                                $scope.comment = "";

                                closeDialog();

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

            }
        }

    });



