module App {
    export interface ISignalRService {
    }

    export class SignalRService implements ISignalRService {

        static $inject = ["$"];
        constructor(private $: any) { }

    }


    angular.module("app")
        .service("signalRService", SignalRService);
}