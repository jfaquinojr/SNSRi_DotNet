module App {
    interface IStartScreenService {
        refreshStartScreen(): void;
    }

    export class StartScreenService implements IStartScreenService {

        static $inject = ["$window"];
        constructor(private $window: any) { }

        public refreshStartScreen() {
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
            ($("body") as any)
                .unmousewheel()
                .mousewheel((event, delta, deltaX, deltaY) => {
                    const page = $(document);
                    const scrollValue = delta * 50;
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

        openDialog(id) {
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


    angular.module("app")
        .service("startScreenService", StartScreenService);
}