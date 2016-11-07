var App;
(function (App) {
    var UsersDataService = (function () {
        function UsersDataService($http, $q, $cacheFactory) {
            this.$http = $http;
            this.$q = $q;
            this.$cacheFactory = $cacheFactory;
            console.log("initializing UsersDataService");
        }
        UsersDataService.prototype.getAllUsers = function () {
            var deferred = this.$q.defer();
            var usersDataCache = this.$cacheFactory.get("usersCache");
            if (!usersDataCache) {
                usersDataCache = this.$cacheFactory("usersCache");
            }
            var usersFromCache = usersDataCache.get("users");
            if (usersFromCache) {
                console.log("returning users from cache");
                deferred.resolve(usersFromCache);
            }
            else {
                console.log("returning users from database");
                this.$http.get("/api/Users")
                    .then(function (result) {
                    usersDataCache.put("users", result);
                    deferred.resolve(result);
                });
            }
            return deferred.promise;
        };
        UsersDataService.prototype.getUser = function (id) {
            return this.$http.get("/api/Users/" + id);
        };
        UsersDataService.prototype.createUser = function (user) {
            this.deleteFromCache();
            return this.$http.post("/api/CreateUser", user);
        };
        UsersDataService.prototype.deleteUser = function (id) {
            this.deleteFromCache();
            return this.$http.post("/api/DeleteUser/" + id);
        };
        UsersDataService.prototype.updateUser = function (user) {
            this.deleteFromCache();
            return this.$http.post("/api/UpdateUser", user);
        };
        UsersDataService.prototype.deleteFromCache = function () {
            var usersCache = this.$cacheFactory.get("usersCache");
            usersCache.remove("users");
        };
        UsersDataService.$inject = ["$http", "$q", "$cacheFactory"];
        return UsersDataService;
    }());
    App.UsersDataService = UsersDataService;
    angular.module("app")
        .service("usersDataService", UsersDataService);
})(App || (App = {}));
//# sourceMappingURL=usersDataService.js.map