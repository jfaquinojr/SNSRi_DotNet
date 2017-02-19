var app = angular.module("app");
app.controller("DevicesController",
    function ($scope, $http, $routeParams, dataService, startScreenService) {
        var scope = $scope;
        var svc = $http;
        var params = $routeParams;

        scope.DeviceGroups = [];
        
        scope.Devices = [];
        scope.Room = {
            Id: 0,
            Name: "Loading.."
        };


        var loadDevicesByRoomId = function(roomId) {
            dataService.getDevicesbyRoomId(roomId).then(function (result) {

                scope.Devices = _.reject(result.data, (device: Data.Contracts.Device) => {
                    return device.HideFromView;
                });

                //group by TileGroup:
                //[{ TileGroup: "null", Devices: [] }, { TileGroup: "Section1", Devices: [] },]
                scope.DeviceGroups = _
                    .chain(scope.Devices)
                    .groupBy('TileGroup')
                    .map(function (device: any, key: string) {
                        return {
                            TileGroup: key,
                            Devices: device
                        }
                    })
                    .value();

                /*
                for (let deviceGroup of groups) {
                    // further divide groups that result to 8 columns and above
                }
                */

            });
        }

        var loadRoom = function(roomId) {
            dataService.getRoom(roomId).then(function (result) {
                scope.Room = result.data;
            });
        }

        loadRoom(params.id);
        loadDevicesByRoomId(params.id);

        startScreenService.refreshStartScreen();


        scope.sizeOfGroup = function (size: number): string {

            if (size > 24)
                return "seven";
            if (size > 20)
                return "six";
            if (size > 16)
                return "five"
            if (size > 12)
                return "four";
            if (size > 8)
                return "three";
            if (size > 4)
                return "two"
            return "";
        }
        
    });


