var App;
(function (App) {
    var UsersDataService = (function () {
        function UsersDataService($http) {
            this.$http = $http;
            console.log("initializing UsersDataService");
        }
        UsersDataService.prototype.getAllUsers = function () {
            return this.$http.get("/api/Users");
        };
        UsersDataService.prototype.getUser = function (id) {
            return this.$http.get("/api/Users/" + id);
        };
        UsersDataService.prototype.createUser = function (user) {
            return this.$http.post("/api/CreateUser", user);
        };
        UsersDataService.prototype.deleteUser = function (id) {
            return this.$http.post("/api/DeleteUser/" + id);
        };
        UsersDataService.prototype.updateUser = function (user) {
            return this.$http.post("/api/UpdateUser", user);
        };
        UsersDataService.$inject = ["$http"];
        return UsersDataService;
    }());
    App.UsersDataService = UsersDataService;
    angular.module("app")
        .service("usersDataService", UsersDataService);
})(App || (App = {}));
//# sourceMappingURL=usersDataService.js.map