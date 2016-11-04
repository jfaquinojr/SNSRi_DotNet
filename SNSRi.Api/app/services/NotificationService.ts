module App {

    export interface INotificationService {
        subscribe(event: string, callback: Function, scope: ng.IScope): void;
        notify(event: string, args: any): void;
    }


    export class NotificationService implements INotificationService {


        constructor(private $rootScope) {  }

        subscribe(event: string, callback: Function, scope: ng.IScope): void {
            const handler = this.$rootScope.$on(event, callback);
            scope.$on("$destroy", handler);
        }

        notify(event: string, args: any): void {
            this.$rootScope.$emit(event, args);
        }
    }

    angular.module("app")
        .service("notificationService", NotificationService);

}