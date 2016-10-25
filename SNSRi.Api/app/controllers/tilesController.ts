
module App {
    import Ticket = Data.Contracts.Ticket;

    class TilesController {


        $inject = ["$window", "ticket", "$scope"];
        constructor(
            private $window: any,
            private ticket: Ticket,
            public $scope: any) {


                this.$scope.$on("roomChanged",
                    function (event, roomId) {
                        this.$scope.$broadcast("changeRoom", roomId);
                    });

                this.$scope.$on("EventOpened",
                    function (event, ticket) {
                        this.$scope.ticket = ticket;
                        this.$scope.$broadcast("OpenEvent", ticket.Id);
                    });

                this.$scope.$on("TicketClosed",
                    function (event) {
                        this.$scope.$broadcast("CloseTicket", this.$scope.ticket.Id);
                    });

                this.$scope.$on("ThemesOpened",
                    function (event) {
                        this.$scope.$broadcast("OpenThemes");
                    });

                this.$scope.$on("EventsCharmOpened",
                    function(event) {
                        this.$scope.$broadcast("OpenEventsCharm");
                    });

        }

        refreshStartScreen() {
            const plugin = this;
            const width = (this.$window.innerWidth > 0) ? this.$window.innerWidth : screen.width;
        }

        private setTilesAreaSize(width: number) {
            const groups = $(".tile-group");
            let tileAreaWidth = 80;
            $.each(groups,
                (i, t) => {
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

        private addMouseWheel() {
            (<any>$("body"))
                .unmousewheel()
                .mousewheel((event, delta, deltaX, deltaY) => {
                    let page = $(document);
                    let scrollValue = delta * 50;
                    page.scrollLeft(page.scrollLeft() - scrollValue);
                    return false;
                });
        };

        private init() {
            this.setTilesAreaSize(this.getWidth());
            if (this.getWidth() > 640) this.addMouseWheel();
        }

        private getWidth() {
            return (this.$window.innerWidth > 0) ? this.$window.innerWidth : screen.width;
        }


        closeDialog(id) {
            const dialog = $(id).data("dialog");
            dialog.close();
        }

        openDialog (id) {
            const dialog = $(id).data("dialog");
            dialog.open();
        }

        showCharms(id) {
            const charm = $(id).data("charm");
            if (charm.element.data("opened") === true) {
                charm.close();
            } else {
                charm.open();
            }
        }




    }



    angular.module("app");

};
