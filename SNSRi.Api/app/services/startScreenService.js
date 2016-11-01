var App;
(function (App) {
    var StartScreenService = (function () {
        function StartScreenService($window) {
            this.$window = $window;
        }
        StartScreenService.prototype.refreshStartScreen = function () {
            var plugin = this;
            var width = (this.$window.innerWidth > 0) ? this.$window.innerWidth : screen.width;
        };
        StartScreenService.prototype.setTilesAreaSize = function (width) {
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
        StartScreenService.prototype.addMouseWheel = function () {
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
        StartScreenService.prototype.init = function () {
            this.setTilesAreaSize(this.getWidth());
            if (this.getWidth() > 640)
                this.addMouseWheel();
        };
        StartScreenService.prototype.getWidth = function () {
            return (this.$window.innerWidth > 0) ? this.$window.innerWidth : screen.width;
        };
        StartScreenService.prototype.closeDialog = function (id) {
            var dialog = $(id).data("dialog");
            dialog.close();
        };
        StartScreenService.prototype.openDialog = function (id) {
            var dialog = $(id).data("dialog");
            dialog.open();
        };
        StartScreenService.prototype.showCharms = function (id) {
            var charm = $(id).data("charm");
            if (charm.element.data("opened") === true) {
                charm.close();
            }
            else {
                charm.open();
            }
        };
        StartScreenService.$inject = ["$window"];
        return StartScreenService;
    }());
    App.StartScreenService = StartScreenService;
    angular.module("app")
        .service("startScreenService", StartScreenService);
})(App || (App = {}));
//# sourceMappingURL=startScreenService.js.map