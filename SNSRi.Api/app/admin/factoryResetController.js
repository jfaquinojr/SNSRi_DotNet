var App;
(function (App) {
    var FactoryResetController = (function () {
        function FactoryResetController($scope, $http) {
            this.$scope = $scope;
            this.$http = $http;
        }
        FactoryResetController.prototype.clearData = function () {
            var self = this;
            self.$http.post("/api/FactoryReset", { withCredentials: true })
                .then(function () {
                $.Notify({
                    caption: "Wipe Complete.",
                    content: "HomeSeer data has been reset.",
                    type: "success"
                });
            });
        };
        FactoryResetController.$inject = ["$scope", "$http"];
        return FactoryResetController;
    }());
    App.FactoryResetController = FactoryResetController;
    angular.module("app")
        .controller("factoryResetController", FactoryResetController);
})(App || (App = {}));
//# sourceMappingURL=factoryResetController.js.map