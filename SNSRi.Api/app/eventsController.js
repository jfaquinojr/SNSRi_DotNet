var app = angular.module("app");

app.controller("EventsController",
    function ($scope, dataService) {

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


    });

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



