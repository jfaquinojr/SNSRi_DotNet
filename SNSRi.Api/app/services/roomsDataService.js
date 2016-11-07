var App;
(function (App) {
    var RoomsDataService = (function () {
        function RoomsDataService($http, $q, $cacheFactory) {
            this.$http = $http;
            this.$q = $q;
            this.$cacheFactory = $cacheFactory;
            console.log("initializing RoomsDataService");
        }
        RoomsDataService.prototype.getAllRooms = function () {
            var deferred = this.$q.defer();
            var roomsDataCache = this.$cacheFactory.get("roomsCache");
            if (!roomsDataCache) {
                roomsDataCache = this.$cacheFactory("roomsCache");
            }
            var roomsFromCache = roomsDataCache.get("rooms");
            if (roomsFromCache) {
                console.log("returning rooms from cache");
                deferred.resolve(roomsFromCache);
            }
            else {
                console.log("returning rooms from database");
                this.$http.get("/api/Rooms")
                    .then(function (result) {
                    roomsDataCache.put("rooms", result);
                    deferred.resolve(result);
                });
            }
            return deferred.promise;
        };
        RoomsDataService.prototype.getRoom = function (id) {
            return this.$http.get("/api/Rooms/" + id);
        };
        RoomsDataService.prototype.createRoom = function (room) {
            this.deleteFromCache();
            return this.$http.post("/api/CreateRoom", room);
        };
        RoomsDataService.prototype.deleteRoom = function (id) {
            this.deleteFromCache();
            return this.$http.post("/api/DeleteRoom/" + id);
        };
        RoomsDataService.prototype.updateRoom = function (room) {
            this.deleteFromCache();
            return this.$http.post("/api/UpdateRoom", room);
        };
        RoomsDataService.prototype.deleteFromCache = function () {
            var roomsCache = this.$cacheFactory.get("roomsCache");
            roomsCache.remove("rooms");
        };
        RoomsDataService.$inject = ["$http", "$q", "$cacheFactory"];
        return RoomsDataService;
    }());
    App.RoomsDataService = RoomsDataService;
    angular.module("app")
        .service("roomsDataService", RoomsDataService);
})(App || (App = {}));
//# sourceMappingURL=roomsDataService.js.map