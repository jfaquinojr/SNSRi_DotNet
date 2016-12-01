module App {
    import Device = Data.Contracts.Device;
    import UIRoomDevice = Data.Contracts.UIRoomDevice;

    export interface IDeviceDataService {
        getAllDevices(): ng.IHttpPromise<Device[]>;
        getDevicesByRoomId(roomId: number): ng.IHttpPromise<Device[]>;
        getDevice(id: number): ng.IHttpPromise<Device>;
        createDevice(device: Device): ng.IHttpPromise<number>;
        createRoomDevice(roomDevice: UIRoomDevice): ng.IHttpPromise<number>;
        deleteRoomDevice(id: number): ng.IHttpPromise<void>;
        deleteDevice(id: number): ng.IHttpPromise<void>;
        updateDevice(device: Device): ng.IHttpPromise<void>;
    }

    export class DeviceDataService implements IDeviceDataService {

        static $inject = ["$http", "$q", "$cacheFactory"];
        constructor(private $http, private $q, private $cacheFactory) {
            console.log("initializing DeviceDataService");
        }

        getAllDevices(): angular.IHttpPromise<Device[]> {

            var deferred = this.$q.defer();

            var devicesDataCache = this.$cacheFactory.get("devicesCache");

            if (!devicesDataCache) {
                devicesDataCache = this.$cacheFactory("devicesCache");
            }

            const devicesFromCache = devicesDataCache.get("devices");
            if (devicesFromCache) {
                console.log("returning devices from cache");
                deferred.resolve(devicesFromCache);

            } else {

                console.log("returning devices from database");

                this.$http.get("/api/Devices")
                    .then(result => {
                        devicesDataCache.put("devices", result);
                        deferred.resolve(result);
                    });
            }

            return deferred.promise;
            
        }

        getDevicesByRoomId(roomId: number): ng.IHttpPromise<Device[]> {

            var deferred = this.$q.defer();

            console.log("returning devices from database");

            this.$http.get(`api/Rooms/${roomId}/Devices`)
                .then(result => {
                    deferred.resolve(result);
                });

            return deferred.promise;
        }

        getDevice(id: number): angular.IHttpPromise<Device> {
            return this.$http.get(`/api/Devices/${id}`);
            
        }

        createDevice(device: Device): angular.IHttpPromise<number> {
            this.deleteFromCache();
            return this.$http.post(`/api/CreateDevice`, device);
        }

        deleteDevice(id: number): angular.IHttpPromise<void> {
            this.deleteFromCache();
            return this.$http.post(`/api/DeleteDevice/${id}`);
        }

        updateDevice(device: Device): angular.IHttpPromise<void> {
            this.deleteFromCache();
            return this.$http.post(`/api/UpdateDevice`, device);
        }

        createRoomDevice(roomDevice: UIRoomDevice): angular.IHttpPromise<number> {
            return this.$http.post(`/api/CreateRoomDevice`, roomDevice);
        }

        deleteRoomDevice(id: number): angular.IHttpPromise<void> {
            return this.$http.post(`/api/DeleteRoomDevice/${id}`);
        }

        private deleteFromCache() {
            const devicesCache = this.$cacheFactory.get("devicesCache");
            devicesCache.remove("devices");
        }
    }

    angular.module("app")
        .service("deviceDataService", DeviceDataService);
}