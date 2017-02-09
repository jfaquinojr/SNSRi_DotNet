module App {

    export class FactoryResetController {

        static $inject = ["$scope", "$http"];

        constructor(public $scope, public $http) {
            
        }

        public clearData(): void {
            const self = this;
            self.$http.post("/api/FactoryReset", { withCredentials: true })
                .then(() => {
                    $.Notify({
                        caption: "Wipe Complete.",
                        content: "HomeSeer data has been reset.",
                        type: "success"
                    });
                });
        }

        public syncData(): void {
            const self = this;
            self.$http.post("/api/FactorySync", { withCredentials: true })
                .then(() => {
                    $.Notify({
                        caption: "Sync Complete.",
                        content: "HomeSeer data has been synced.",
                        type: "success"
                    });
                });
        }

    }

    angular.module("app")
        .controller("factoryResetController", FactoryResetController);
}