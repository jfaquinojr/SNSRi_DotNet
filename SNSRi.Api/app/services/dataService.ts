module App {

    export interface IDataService {
        createActivity(activity: Data.Contracts.Activity): ng.IHttpPromise<Data.Contracts.Ticket>;
        closeTicket(activity: Data.Contracts.Activity): ng.IHttpPromise<Data.Contracts.Ticket>;
        getHomeSeerDevice(refId: number): any;
        getRooms(): ng.IHttpPromise<Data.Contracts.Room[]>;
        getRoom(id: number): ng.IHttpPromise<Data.Contracts.Room>;
        getDevicesbyRoomId(roomId: number): ng.IHttpPromise<Data.Contracts.Device[]>;
        getOpenTickets(): ng.IHttpPromise<Data.Contracts.Ticket[]>;
        getOpenTicketsByRoom(roomId: number): ng.IHttpPromise<Data.Contracts.Ticket[]>;
        getOpenTicketsRecent(): ng.IHttpPromise<Data.Contracts.Ticket[]>;
        getActivitiesForTicket(ticketId: number): ng.IHttpPromise<Data.Contracts.Activity[]>;
        getOpenTicketsPastMinutes(minutes: number): ng.IHttpPromise<Data.Contracts.Ticket[]>;
        getOpenTicketsPastSeconds(seconds: number) : ng.IHttpPromise<Data.Contracts.Ticket[]>;
    }

    export class DataService implements IDataService {

        private homeSeerUrl: string;

        static $inject = ["$http"];
        constructor(private $http: ng.IHttpService) {
            this.$http.get("/api/Config/HomeSeerUrl").then((url: string) => {
                this.homeSeerUrl = url;
            });
        }
        

        createActivity(activity: Data.Contracts.Activity): angular.IHttpPromise<Data.Contracts.Ticket> {
            return this.$http.post("/api/CreateActivity", JSON.stringify(activity));
        }

        closeTicket(activity: Data.Contracts.Activity): angular.IHttpPromise<Data.Contracts.Ticket> {
            return this.$http.post("/api/CloseTicket", JSON.stringify(activity));
        }

        getHomeSeerDevice(refId: number) {
            return this.$http.get(this.homeSeerUrl + "/JSON?request=getstatus&ref=" + refId);
        }

        getRooms(): angular.IHttpPromise<Data.Contracts.Room[]> {
            return this.$http.get("/api/Rooms");
        }

        getRoom(id: number): angular.IHttpPromise<Data.Contracts.Room> {
            return this.$http.get("/api/Rooms/" + id);
        }

        getDevicesbyRoomId(roomId: number): angular.IHttpPromise<Data.Contracts.Device[]> {
            return this.$http.get("/api/Rooms/" + roomId + "/Devices");
        }

        getOpenTickets(): angular.IHttpPromise<Data.Contracts.Ticket[]> {
            return this.$http.get("/api/Tickets/Open");
        }

        getOpenTicketsByRoom(roomId: number): angular.IHttpPromise<Data.Contracts.Ticket[]> {
            return this.$http.get("/api/Tickets/Open/Room/" + roomId);
        }

        getOpenTicketsRecent(): angular.IHttpPromise<Data.Contracts.Ticket[]> {
            return this.$http.get("/api/Tickets/Open/Past/Minutes/1");
        }

        getActivitiesForTicket(ticketId: number): angular.IHttpPromise<Data.Contracts.Activity[]> {
            return this.$http.get("api/Tickets/" + ticketId + "/Activities");
        }

        getOpenTicketsPastMinutes(minutes: number): angular.IHttpPromise<Data.Contracts.Ticket[]> {
            return this.$http.get("api/Tickets/Open/Past/Minutes/" + minutes);
        }

        getOpenTicketsPastSeconds(seconds: number): angular.IHttpPromise<Data.Contracts.Ticket[]> {
            return this.$http.get(`api/Tickets/Open/Past/Seconds/${seconds}`);
        }
    }

    angular.module("app")
        .service("dataService", DataService);

}


