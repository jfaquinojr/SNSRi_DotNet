var app = angular.module("app");

app.controller("TilesController", function($scope, $window) {

    $scope.ticket = {};
    $scope.RefreshStartScreen = function() {

        var plugin = this;
        var width = ($window.innerWidth > 0) ? $window.innerWidth : screen.width;

        plugin.init = function() {
            setTilesAreaSize();
            if (width > 640) addMouseWheel();
        };

        var setTilesAreaSize = function() {
            var groups = $(".tile-group");
            var tileAreaWidth = 80;
            $.each(groups,
                function(i, t) {
                    if (width <= 640) {
                        tileAreaWidth = width;
                    } else {
                        tileAreaWidth += $(t).outerWidth() + 80;
                    }
                });
            $(".tile-area")
                .css({
                    width: tileAreaWidth
                });
        };

        var addMouseWheel = function() {
            $("body")
                .mousewheel(function(event, delta, deltaX, deltaY) {
                    var page = $(document);
                    var scroll_value = delta * 50;
                    page.scrollLeft(page.scrollLeft() - scroll_value);
                    return false;
                });
        };

        plugin.init();
    }

    $scope.$on("roomChanged",
        function (event, roomId) {
            $scope.$broadcast("changeRoom", roomId);
        });

    $scope.$on("EventOpened",
        function (event, ticket) {
            $scope.ticket = ticket;
            $scope.$broadcast("OpenEvent", ticket.Id);
        });

    $scope.$on("TicketClosed",
        function (event) {
            $scope.$broadcast("CloseTicket", $scope.ticket.Id);
        });

    $scope.$on("ThemesOpened",
        function (event) {
            $scope.$broadcast("OpenThemes");
        });

    $scope.$on("EventsCharmOpened",
        function(event) {
            $scope.$broadcast("OpenEventsCharm");
        });


    $scope.closeDialog = function(id) {
        var dialog = $(id).data("dialog");
        dialog.close();
    }

    $scope.openDialog = function (id) {
        var dialog = $(id).data("dialog");
        dialog.open();
    }

    $scope.showCharms = function (id) {
        var charm = $(id).data("charm");
        if (charm.element.data("opened") === true) {
            charm.close();
        } else {
            charm.open();
        }
    }
});

