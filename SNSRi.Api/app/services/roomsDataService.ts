module App {
    import Room = Data.Contracts.Room;

    export interface IRoomsDataService {
        getAllRooms(): ng.IHttpPromise<Room[]>;
        getRoom(id: number): ng.IHttpPromise<Room>;
        createRoom(room: Room): ng.IHttpPromise<number>;
        deleteRoom(id: number): ng.IHttpPromise<void>;
        updateRoom(room: Room): ng.IHttpPromise<void>;
    }

    export class RoomsDataService implements IRoomsDataService {

        static $inject = ["$http", "$q", "$cacheFactory"];
        constructor(private $http, private $q, private $cacheFactory) {
            console.log("initializing RoomsDataService");
        }

        getAllRooms(): ng.IHttpPromise<Room[]> {

            var deferred = this.$q.defer();

            var roomsDataCache = this.$cacheFactory.get("roomsCache");

            if (!roomsDataCache) {
                roomsDataCache = this.$cacheFactory("roomsCache");
            }

            var roomsFromCache = roomsDataCache.get("rooms");
            if (roomsFromCache) {
                console.log("returning rooms from cache");
                deferred.resolve(roomsFromCache);
            } else {

                console.log("returning rooms from database");

                this.$http.get("/api/Rooms")
                    .then(result => {

                        roomsDataCache.put("rooms", result);

                        deferred.resolve(result);
                    });
            }

            return deferred.promise;
        }

        getRoom(id: number): ng.IHttpPromise<Room> {
            return this.$http.get(`/api/Rooms/${id}`);
        }

        createRoom(room: Room): ng.IHttpPromise<number> {
            this.deleteFromCache();
            return this.$http.post(`/api/CreateRoom`, room);
        }

        deleteRoom(id: number): ng.IHttpPromise<void> {
            this.deleteFromCache();
            return this.$http.post(`/api/DeleteRoom/${id}`);
        }

        updateRoom(room: Room): ng.IHttpPromise<void> {
            this.deleteFromCache();
            return this.$http.post(`/api/UpdateRoom`, room);
        }

        private deleteFromCache() {
            var roomsCache = this.$cacheFactory.get("roomsCache");
            roomsCache.remove("rooms");
        }
    }

    angular.module("app")
        .service("roomsDataService", RoomsDataService);
}

