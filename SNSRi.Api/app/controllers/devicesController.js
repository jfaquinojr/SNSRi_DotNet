var app = angular.module("app");
app.controller("DevicesController", function ($scope, $http, $routeParams, dataService, startScreenService) {
    var scope = $scope;
    var svc = $http;
    var params = $routeParams;
    scope.Devices = [];
    scope.Room = {
        Id: 0,
        Name: "Loading.."
    };
    var loadDevicesByRoomId = function (roomId) {
        dataService.getDevicesbyRoomId(roomId).then(function (result) {
            scope.Devices = _.reject(result.data, function (device) {
                return device.HideFromView;
            });
        });
    };
    var loadRoom = function (roomId) {
        dataService.getRoom(roomId).then(function (result) {
            scope.Room = result.data;
        });
    };
    loadRoom(params.id);
    loadDevicesByRoomId(params.id);
    startScreenService.refreshStartScreen();
});
app.directive("deviceTile", function () {
    return {
        templateUrl: "/Home/DeviceTile",
        restrict: "E",
        scope: {
            device: "="
        },
        controller: function ($scope, $interval, deviceService) {
            $scope.HomeSeer = {};
            var hs = $scope.HomeSeer;
            $scope.HomeSeerUrl = deviceService.urlHomeSeer;
            $scope.Clicked = function (device) {
                alert(JSON.stringify(device));
            };
            var stop;
            function init() {
                loadHomeSeerDevice();
                stop = $interval(loadHomeSeerDevice, 3000);
            }
            $scope.$on("$destroy", function () {
                if (angular.isDefined(stop)) {
                    $interval.cancel(stop);
                    stop = undefined;
                }
            });
            var loadHomeSeerDevice = function () {
                deviceService.getHomeSeerDevice($scope.device.ReferenceId)
                    .then(function (result) {
                    if (result.data.Devices.length > 0) {
                        var dev = result.data.Devices[0];
                        hs.name = dev.name;
                        hs.location = dev.location;
                        hs.location2 = dev.location2;
                        hs.value = dev.value;
                        hs.status = dev.status;
                        hs.hide_from_view = dev.hide_from_view;
                        hs.device_image = dev.device_image;
                        hs.status_image = dev.status_image;
                    }
                });
            };
            init();
        }
    };
});
//# sourceMappingURL=devicesController.js.map