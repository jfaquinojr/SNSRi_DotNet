var App;
(function (App) {
    var TilesController = (function () {
        function TilesController($window, ticket, $scope) {
            this.$window = $window;
            this.ticket = ticket;
            this.$scope = $scope;
            this.$inject = ["$window", "ticket", "$scope"];
            this.$scope.$on("roomChanged", function (event, roomId) {
                this.$scope.$broadcast("changeRoom", roomId);
            });
            this.$scope.$on("EventOpened", function (event, ticket) {
                this.$scope.ticket = ticket;
                this.$scope.$broadcast("OpenEvent", ticket.Id);
            });
            this.$scope.$on("TicketClosed", function (event) {
                this.$scope.$broadcast("CloseTicket", this.$scope.ticket.Id);
            });
            this.$scope.$on("ThemesOpened", function (event) {
                this.$scope.$broadcast("OpenThemes");
            });
            this.$scope.$on("EventsCharmOpened", function (event) {
                this.$scope.$broadcast("OpenEventsCharm");
            });
        }
        TilesController.prototype.refreshStartScreen = function () {
            var plugin = this;
            var width = (this.$window.innerWidth > 0) ? this.$window.innerWidth : screen.width;
        };
        TilesController.prototype.setTilesAreaSize = function (width) {
            var groups = $(".tile-group");
            var tileAreaWidth = 80;
            $.each(groups, function (i, t) {
                if (width <= 640) {
                    tileAreaWidth = width;
                }
                else {
                    tileAreaWidth += $(t).outerWidth() + 80;
                }
            });
            $(".tile-area")
                .css({
                width: tileAreaWidth
            });
        };
        ;
        TilesController.prototype.addMouseWheel = function () {
            $("body")
                .unmousewheel()
                .mousewheel(function (event, delta, deltaX, deltaY) {
                var page = $(document);
                var scrollValue = delta * 50;
                page.scrollLeft(page.scrollLeft() - scrollValue);
                return false;
            });
        };
        ;
        TilesController.prototype.init = function () {
            this.setTilesAreaSize(this.getWidth());
            if (this.getWidth() > 640)
                this.addMouseWheel();
        };
        TilesController.prototype.getWidth = function () {
            return (this.$window.innerWidth > 0) ? this.$window.innerWidth : screen.width;
        };
        TilesController.prototype.closeDialog = function (id) {
            var dialog = $(id).data("dialog");
            dialog.close();
        };
        TilesController.prototype.openDialog = function (id) {
            var dialog = $(id).data("dialog");
            dialog.open();
        };
        TilesController.prototype.showCharms = function (id) {
            var charm = $(id).data("charm");
            if (charm.element.data("opened") === true) {
                charm.close();
            }
            else {
                charm.open();
            }
        };
        return TilesController;
    }());
    angular.module("app");
})(App || (App = {}));
;
//# sourceMappingURL=tilesController.js.map