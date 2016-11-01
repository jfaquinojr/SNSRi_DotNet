var App;
(function (App) {
    var TilesController = (function () {
        function TilesController($scope, $window, startScreenService) {
            this.$scope = $scope;
            this.$window = $window;
            this.startScreenService = startScreenService;
            var self = this;
            this.$scope.$on("roomChanged", function (event, roomId) {
                self.$scope.$broadcast("changeRoom", roomId);
            });
            this.$scope.$on("EventOpened", function (event, ticket) {
                self.ticket = ticket;
                self.$scope.$broadcast("OpenEvent", ticket.id);
            });
            this.$scope.$on("TicketClosed", function (event) {
                self.$scope.$broadcast("CloseTicket", self.ticket.id);
            });
            this.$scope.$on("ThemesOpened", function (event) {
                self.$scope.$broadcast("OpenThemes");
            });
            this.$scope.$on("EventsCharmOpened", function (event) {
                self.$scope.$broadcast("OpenEventsCharm");
            });
        }
        TilesController.$inject = ["$scope", "$window", "startScreenService"];
        return TilesController;
    }());
    angular.module("app")
        .service("startScreenService", App.StartScreenService)
        .controller("tilesController", TilesController);
})(App || (App = {}));
;
//# sourceMappingURL=tilesController.js.map