var app = angular.module("app");


app.controller("RoomsController", ["$scope", "dataService", "startScreenService",
    function ($scope, dataService, startScreenService) {

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
    }]);

app.directive("roomTile",
    function() {

        return {
            templateUrl: "/Home/RoomTile",
            restrict: "E",
            scope: {
                room: "="
            },
            controller: function($scope, $interval) {
                $scope.changeRoom = function (roomId) {
                    $scope.$emit("roomChanged", roomId);
                }
            }
        };

    });

