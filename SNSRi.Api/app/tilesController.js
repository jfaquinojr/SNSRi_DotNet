var app = angular.module("app");

app.controller("TilesController",
    function ($scope, $interval) {



    });

app.directive("tileArea",
    function() {

        return {
            restrict: "A",
            link: function (scope, el, attrs) {

                console.log("tileArea link function");

                var tileArea = el[0];
                var groups = $(tileArea).find(".tile-group");
                var tileAreaWidth = 80;
                $.each(groups, function (i, t) {
                    if (width <= 640) {
                        tileAreaWidth = width;
                    } else {
                        tileAreaWidth += $(t).outerWidth() + 80;
                    }
                });
                $(tileArea).css({
                    width: tileAreaWidth
                });

                var width = (window.innerWidth > 0) ? window.innerWidth : screen.width;
                if (width > 640) addMouseWheel();

                groups.animate({ left: 0 });

                //var tiles = $(".tile, .tile-small, .tile-sqaure, .tile-wide, .tile-large, .tile-big, .tile-super");

                //$.each(tiles, function () {
                //    console.log("found tile!");
                //    var tile = $(this);
                //    setTimeout(function () {
                //        tile.css({
                //            opacity: 1,
                //            "-webkit-transform": "scale(1)",
                //            "transform": "scale(1)",
                //            "-webkit-transition": ".3s",
                //            "transition": ".3s"
                //        });
                //        $scope.$apply();
                //    }, Math.floor(Math.random() * 500));
                //});
            }
        };

    });

var addMouseWheel = function () {
    $("body").mousewheel(function (event, delta, deltaX, deltaY) {
        var page = $(document);
        var scroll_value = delta * 50;
        page.scrollLeft(page.scrollLeft() - scroll_value);
        return false;
    });
};

