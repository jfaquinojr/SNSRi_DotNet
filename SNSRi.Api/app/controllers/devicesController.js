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
//# sourceMappingURL=devicesController.js.map