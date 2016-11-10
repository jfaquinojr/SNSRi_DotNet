var App;
(function (App) {
    var DeviceDataService = (function () {
        function DeviceDataService($http, $q, $cacheFactory) {
            this.$http = $http;
            this.$q = $q;
            this.$cacheFactory = $cacheFactory;
            console.log("initializing DeviceDataService");
        }
        DeviceDataService.prototype.getAllDevices = function () {
            var deferred = this.$q.defer();
            var devicesDataCache = this.$cacheFactory.get("devicesCache");
            if (!devicesDataCache) {
                devicesDataCache = this.$cacheFactory("devicesCache");
            }
            var devicesFromCache = devicesDataCache.get("devices");
            if (devicesFromCache) {
                console.log("returning devices from cache");
                deferred.resolve(devicesFromCache);
            }
            else {
                console.log("returning devices from database");
                this.$http.get("/api/Devices")
                    .then(function (result) {
                    devicesDataCache.put("devices", result);
                    deferred.resolve(result);
                });
            }
            return deferred.promise;
        };
        DeviceDataService.prototype.getDevicesByRoomId = function (roomId) {
            var deferred = this.$q.defer();
            console.log("returning devices from database");
            this.$http.get("api/Rooms/" + roomId + "/Devices")
                .then(function (result) {
                deferred.resolve(result);
            });
            return deferred.promise;
        };
        DeviceDataService.prototype.getDevice = function (id) {
            return this.$http.get("/api/Devices/" + id);
        };
        DeviceDataService.prototype.createDevice = function (device) {
            this.deleteFromCache();
            return this.$http.post("/api/CreateDevice", device);
        };
        DeviceDataService.prototype.deleteDevice = function (id) {
            this.deleteFromCache();
            return this.$http.post("/api/DeleteDevice/" + id);
        };
        DeviceDataService.prototype.updateDevice = function (device) {
            this.deleteFromCache();
            return this.$http.post("/api/UpdateDevice", device);
        };
        DeviceDataService.prototype.deleteFromCache = function () {
            var devicesCache = this.$cacheFactory.get("devicesCache");
            devicesCache.remove("devices");
        };
        DeviceDataService.$inject = ["$http", "$q", "$cacheFactory"];
        return DeviceDataService;
    }());
    App.DeviceDataService = DeviceDataService;
    angular.module("app")
        .service("deviceDataService", DeviceDataService);
})(App || (App = {}));
//# sourceMappingURL=deviceDataService.js.map