var app = angular.module("app");


app.directive("themePicker",
    function() {


        return {
            templateUrl: "/Home/ThemePicker",
            restrict: "E",
            scope: true,
            controller: function ($scope) {

                function init() {

                    var currentTileAreaScheme = localStorage.getItem('tile-area-scheme') || "tile-area-scheme-dark";
                    $(".tile-area").removeClass(function (index, css) {
                        return (css.match(/(^|\s)tile-area-scheme-\S+/g) || []).join(' ');
                    }).addClass(currentTileAreaScheme);

                    $(".schemeButtons .button").hover(
                        function () {
                            var b = $(this);
                            var scheme = "tile-area-scheme-" + b.data('scheme');
                            $(".tile-area").removeClass(function (index, css) {
                                return (css.match(/(^|\s)tile-area-scheme-\S+/g) || []).join(' ');
                            }).addClass(scheme);
                        },
                        function () {
                            $(".tile-area").removeClass(function (index, css) {
                                return (css.match(/(^|\s)tile-area-scheme-\S+/g) || []).join(' ');
                            }).addClass(currentTileAreaScheme);
                        }
                    )

                    $(".schemeButtons .button").on("click", function () {
                        var b = $(this);
                        var scheme = "tile-area-scheme-" + b.data('scheme');

                        $(".tile-area").removeClass(function (index, css) {
                            return (css.match(/(^|\s)tile-area-scheme-\S+/g) || []).join(' ');
                        }).addClass(scheme);

                        currentTileAreaScheme = scheme;
                        localStorage.setItem('tile-area-scheme', scheme);

                        //showSettings();
                    });
                }

                init();


                $scope.$on("OpenThemes",
                    function() {
                        $scope.showCharms("#charmThemes");
                    });

            }
        };


    });