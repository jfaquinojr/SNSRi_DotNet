
module App.Controllers {
    import Ticket = Data.Contracts.Ticket;


    class TilesController {


        private ticket: Ticket;

        static $inject = ["$scope", "$window", "startScreenService"];
        constructor(
            public $scope: ng.IScope,
            private $window,
            private startScreenService: StartScreenService) {


                const self = this;

                this.$scope.$on("roomChanged", (event, roomId: number) => {
                        self.$scope.$broadcast("changeRoom", roomId);
                    });

                //this.$scope.$on("EventOpened", (event:any, ticket: Ticket) => {
                //        self.ticket = ticket;
                //        self.$scope.$broadcast("OpenEvent", ticket.id);
                //    });

                this.$scope.$on("TicketClosed", event => {
                        self.$scope.$broadcast("CloseTicket", self.ticket.Id);
                    });

                //this.$scope.$on("ThemesOpened", event => {
                //        self.$scope.$broadcast("OpenThemes");
                //    });

                //this.$scope.$on("EventsCharmOpened", event => {
                //        self.$scope.$broadcast("OpenEventsCharm");
                //    });

        }

    }


    angular.module("app")
        .service("startScreenService", StartScreenService)
        .controller("tilesController", TilesController);

};
