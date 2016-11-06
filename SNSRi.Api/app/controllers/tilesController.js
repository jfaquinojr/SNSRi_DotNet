var App;
(function (App) {
    var Controllers;
    (function (Controllers) {
        var TilesController = (function () {
            function TilesController($scope, $window, startScreenService) {
                this.$scope = $scope;
                this.$window = $window;
                this.startScreenService = startScreenService;
                var self = this;
                this.$scope.$on("roomChanged", function (event, roomId) {
                    self.$scope.$broadcast("changeRoom", roomId);
                });
                //this.$scope.$on("EventOpened", (event:any, ticket: Ticket) => {
                //        self.ticket = ticket;
                //        self.$scope.$broadcast("OpenEvent", ticket.id);
                //    });
                this.$scope.$on("TicketClosed", function (event) {
                    self.$scope.$broadcast("CloseTicket", self.ticket.Id);
                });
                //this.$scope.$on("ThemesOpened", event => {
                //        self.$scope.$broadcast("OpenThemes");
                //    });
                //this.$scope.$on("EventsCharmOpened", event => {
                //        self.$scope.$broadcast("OpenEventsCharm");
                //    });
            }
            TilesController.$inject = ["$scope", "$window", "startScreenService"];
            return TilesController;
        }());
        angular.module("app")
            .service("startScreenService", App.StartScreenService)
            .controller("tilesController", TilesController);
    })(Controllers = App.Controllers || (App.Controllers = {}));
})(App || (App = {}));
;
//# sourceMappingURL=tilesController.js.map