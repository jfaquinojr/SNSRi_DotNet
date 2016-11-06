var App;
(function (App) {
    var Common;
    (function (Common) {
        var DataAccessService = (function () {
            function DataAccessService($resource) {
                this.$resource = $resource;
            }
            DataAccessService.prototype.getUserResource = function () {
                return this.$resource("/api/users/:userId");
            };
            DataAccessService.$inject = ["$resource"];
            return DataAccessService;
        }());
        Common.DataAccessService = DataAccessService;
        angular
            .module("app")
            .service("dataAccessService", DataAccessService);
    })(Common = App.Common || (App.Common = {}));
})(App || (App = {}));
//# sourceMappingURL=dataAccessService.js.map