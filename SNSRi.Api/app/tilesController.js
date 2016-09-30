var app = angular.module("app");

app.controller("TilesController", function($scope, $window) {

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

});

