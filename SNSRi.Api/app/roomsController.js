var app = angular.module("app");


app.controller("RoomsController", ["$scope", "dataService",
    function ($scope, dataService) {

        $scope.Rooms = [];

        var loadRooms = function() {
            dataService.getRooms()
                .then(function(result) {
                    $scope.Rooms = result.data;
                    $scope.$emit("roomChanged", 0);
                });
            
        }


        loadRooms();
        $scope.RefreshStartScreen();
    }]);

app.directive("roomTile",
    function() {

        return {
            templateUrl: "/Home/RoomTile",
            restrict: "E",
            scope: {
                room: "="
            },
            controller: function($scope) {
                $scope.changeRoom = function (roomId) {
                    //alert("ChangeRoom! room: " + roomId);
                    $scope.$emit("roomChanged", roomId);
                }
            }
        };

    });

