
module App {

    export interface ISignalRService {
        addHandler(eventName: string, sourceId: string, handler: any);
        invoke(methodName: string, sourceId: string): void;
        init(sourceId: string, initFunction: () => any): void;
        stop(sourceId: string, initFunction: () => any): void;
    }

    export class SignalRService implements ISignalRService {

        private handlers = {};
        private isInitialized = {};
        private connection: any;
        private hub: any;

        static $inject = ["$window", "$rootScope"];
        constructor(private $window: any, private $rootScope: ng.IRootScopeService) {
            const self = this;
            const jquery = $ as any;
            self.connection = jquery.connection;
            self.hub = jquery.connection.snsri;
        }

        public addHandler(eventName: string, sourceId: string, handler: any): void {
            const self = this;
            self.isInitialized[sourceId] = self.isInitialized[sourceId] || false;
            if (self.isInitialized[sourceId] === true)
                throw new Error("Service already initialized. You can't add handlers any more");

            self.handlers[sourceId] = self.handlers[sourceId] || [];
            self.handlers[sourceId].push({
                eventName: eventName,
                handler: function (result) {
                    self.$rootScope.$apply(handler(result));
                }
            });

        }

        public invoke(methodName: string, sourceId: string): void {
            const self = this;
            if (self.isInitialized[sourceId] == false)
                throw new Error("Call init() before invoking server methods");

            self.hub.invoke.apply(self.hub, arguments)
                .done(function () {
                    console.log("Successfully called server method " + methodName);
                })
                .fail(function (error) {
                    console.log("Can't call server method: " + error);
                });
        }


        public init(sourceId: string, initFunction: ()=>any): void {
            const self = this;
            

            if (self.handlers[sourceId].length == 0)
                throw new Error("No handlers defined. Please, add handlers first, than call init()");

            self.connection.hub.logging = true;
            self.hub = self.connection.snsri;

            $.each(self.handlers[sourceId], function () {
                var handlerItem = this;
                self.hub.on(handlerItem.eventName, handlerItem.handler);
            });

            self.connection.hub.start()
                .done(function () {
                    console.log("Connection started.");
                    self.isInitialized[sourceId] = true;
                    initFunction();
                })
                .fail(function () {
                    console.log("Failed to connect.");
                }
                );

            self.connection.hub.error(function (error) {
                console.log('SignalR error: ' + error);
            });	

            
        }

        public stop(sourceId: string, initFunction: () => any): void {
            const self = this;
            initFunction();
            self.connection.hub.stop();
            self.isInitialized[sourceId] = false;
        }

    }

    angular.module("app")
        .service("signalRService", SignalRService);
}