var app = angular.module("app");

app.controller("DevicesController",
    function ($scope, $http, $routeParams) {
        var scope = $scope;
        var svc = $http;
        var params = $routeParams;

        scope.Devices = [];
        scope.Room = {
            Id: 0,
            Name: "Loading.."
        };

        var loadDevicesByRoomId = function(roomId) {
            svc.get("/api/Rooms/" + roomId + "/Devices").then(function (result) {
                scope.Devices = result.data;
            });
        }

        var loadRoom = function(roomId) {
            svc.get("/api/Rooms/" + roomId).then(function (result) {
                scope.Room = result.data;
            });
        }

        loadRoom(params.id);
        loadDevicesByRoomId(params.id);

        $scope.RefreshStartScreen();
    });

app.directive("deviceTile",
    function() {

        return {
            templateUrl: "/Home/DeviceTile",
            restrict: "E",
            scope: {
                device: '='
            },
            controller: function($scope) {
                $scope.Clicked = function(device) {
                    alert(JSON.stringify(device));
                }
            }
        };

    });

