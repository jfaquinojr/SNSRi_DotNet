module App {

    export class ToolboxController {

        vm = this;

        static $inject = ["notificationService"];
        constructor(private notificationService: INotificationService) {

            console.log("inside App.ToolboxController..");
        }

        openThemes() {
            console.log("openThemes");
            this.notificationService.notify("#charmThemes", {});
        }

        openEvents() {
            console.log("openEvents");
            this.notificationService.notify("#charmEvents", {});
        }

    }

    angular.module("app")
        .controller("toolboxController", ["$scope", "notificationService", ToolboxController]);
}



