var app = angular.module("app");


app.controller("RoomsController",
    function ($scope, $http) {
        var scope = $scope;
        var svc = $http;

        scope.Rooms = [];


        var loadRooms = function() {
            svc.get("/api/Rooms").then(function(result) {
                scope.Rooms = result.data;
            });
        }


        loadRooms();
        $scope.RefreshStartScreen();
    });

app.directive("roomTile",
    function() {

        return {
            templateUrl: "/Home/RoomTile",
            restrict: "E",
            scope: {
                room: '='
            },
            controller: function($scope) {
                $scope.OpenRoom = function(roomId) {
                    alert(roomId);
                }
            }
        };

    });

