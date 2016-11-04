var App;
(function (App) {
    var NotificationService = (function () {
        function NotificationService($rootScope) {
            this.$rootScope = $rootScope;
        }
        NotificationService.prototype.subscribe = function (event, callback, scope) {
            var handler = this.$rootScope.$on(event, callback);
            scope.$on("$destroy", handler);
        };
        NotificationService.prototype.notify = function (event, args) {
            this.$rootScope.$emit(event, args);
        };
        return NotificationService;
    }());
    App.NotificationService = NotificationService;
    angular.module("app")
        .service("notificationService", NotificationService);
})(App || (App = {}));
//# sourceMappingURL=NotificationService.js.map