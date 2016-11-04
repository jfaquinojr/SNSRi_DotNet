var app = angular.module("app");


app.controller("RoomsController", ["$scope", "dataService", "startScreenService", "notificationService",
    function ($scope, dataService, startScreenService, notificationService) {

        $scope.Rooms = [];

        var loadRooms = function() {
            dataService.getRooms()
                .then(function(result) {
                    $scope.Rooms = result.data;
                    $scope.$emit("roomChanged", 0);
                });
            
        }


        loadRooms();
        startScreenService.refreshStartScreen();

        $scope.changeRoom = function(id)
        {
            notificationService.notify("changeRoom", { roomId: id });
        }
    }]);

app.directive("roomTile",
    function() {

        return {
            templateUrl: "/Home/RoomTile",
            restrict: "E",
            scope: {
                room: "="
            },
            controllerAs: "vm",
            controller: "RoomsController"
            //controller: function($scope, $interval) {
            //    $scope.changeRoom = function (roomId) {
            //        $scope.$emit("roomChanged", roomId);
            //    }
            //}
        };

    });

