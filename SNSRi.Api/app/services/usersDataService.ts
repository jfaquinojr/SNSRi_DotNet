module App {
    import User = Data.Contracts.User;

    export interface IUsersDataService {
        getAllUsers(): ng.IHttpPromise<User[]>;
        getUser(id: number): ng.IHttpPromise<User>;
        createUser(user: User): ng.IHttpPromise<number>;
        deleteUser(id: number): ng.IHttpPromise<void>;
        updateUser(user: User): ng.IHttpPromise<void>;
    }

    export class UsersDataService implements IUsersDataService {

        static $inject = ["$http", "$q", "$cacheFactory"];
        constructor(private $http, private $q, private $cacheFactory) {
            console.log("initializing UsersDataService");
        }

        getAllUsers(): ng.IHttpPromise<User[]> {

            var deferred = this.$q.defer();

            var usersDataCache = this.$cacheFactory.get("usersCache");

            if (!usersDataCache) {
                usersDataCache = this.$cacheFactory("usersCache");
            }

            var usersFromCache = usersDataCache.get("users");
            if (usersFromCache) {
                console.log("returning users from cache");
                deferred.resolve(usersFromCache);
            } else {

                console.log("returning users from database");

                this.$http.get("/api/Users")
                    .then(result => {

                        usersDataCache.put("users", result);

                        deferred.resolve(result);
                    });
            }

            return deferred.promise;
        }

        getUser(id: number): ng.IHttpPromise<User> {
            return this.$http.get(`/api/Users/${id}`);
        }

        createUser(user: User): ng.IHttpPromise<number> {
            this.deleteFromCache();
            return this.$http.post(`/api/CreateUser`, user);
        }

        deleteUser(id: number): ng.IHttpPromise<void> {
            this.deleteFromCache();
            return this.$http.post(`/api/DeleteUser/${id}`);
        }

        updateUser(user: User): ng.IHttpPromise<void> {
            this.deleteFromCache();
            return this.$http.post(`/api/UpdateUser`, user);
        }

        private deleteFromCache() {
            var usersCache = this.$cacheFactory.get("usersCache");
            usersCache.remove("users");
        }
    }

    angular.module("app")
        .service("usersDataService", UsersDataService);
}

