module App {
    import HomeSeerDevice = Data.HomeSeer.HomeSeerDevice;
    import DeviceControl = Data.Contracts.DeviceControl;

    export interface IDataService {
        createActivity(activity: Data.Contracts.Activity): ng.IHttpPromise<number>;
        closeTicket(activity: Data.Contracts.Activity): ng.IHttpPromise<Data.Contracts.Ticket>;
        getHomeSeerDevice(refId: number): any;
        setHomeSeerDevice(refId: number, value: string): any;
        getRooms(): ng.IHttpPromise<Data.Contracts.Room[]>;
        getRoom(id: number): ng.IHttpPromise<Data.Contracts.Room>;
        getDevicesbyRoomId(roomId: number): ng.IHttpPromise<Data.Contracts.Device[]>;
        getOpenTickets(): ng.IHttpPromise<Data.Contracts.Ticket[]>;
        getOpenTicketsByRoom(roomId: number): ng.IHttpPromise<Data.Contracts.Ticket[]>;
        getOpenTicketsRecent(): ng.IHttpPromise<Data.Contracts.Ticket[]>;
        getActivitiesForTicket(ticketId: number): ng.IHttpPromise<Data.Contracts.Activity[]>;
        getOpenTicketsPastMinutes(minutes: number): ng.IHttpPromise<Data.Contracts.Ticket[]>;
        getOpenTicketsPastSeconds(seconds: number): ng.IHttpPromise<Data.Contracts.Ticket[]>;
        getHomeSeerUrl(): ng.IHttpPromise<string>;
        getHomeSeerDeviceControls(refId: number): ng.IHttpPromise<DeviceControl[]>;
    }

    export class DataService implements IDataService {

        private homeSeerUrl: string;

        static $inject = ["$http"];
        constructor(private $http: ng.IHttpService) {
            this.getHomeSeerUrl().then((result: any) => {
                this.homeSeerUrl = result.data;
            });
        }
        

        createActivity(activity: Data.Contracts.Activity): angular.IHttpPromise<number> {
            return this.$http.post("/api/CreateActivity", JSON.stringify(activity));
        }

        closeTicket(activity: Data.Contracts.Activity): angular.IHttpPromise<Data.Contracts.Ticket> {
            return this.$http.post("/api/CloseTicket", JSON.stringify(activity));
        }

        getHomeSeerDevice(refId: number): any {
            return this.$http.get(`${this.homeSeerUrl}/JSON?request=getstatus&ref=${refId}`);
        }

        setHomeSeerDevice(refId: number, value: string): any {
            let url = `${this.homeSeerUrl}/JSON?request=controldevicebyvalue&ref=${refId}&value=${value}`;
            return this.$http.get(url);
        }

        getRooms(): angular.IHttpPromise<Data.Contracts.Room[]> {
            return this.$http.get("/api/Rooms");
        }

        getRoom(id: number): angular.IHttpPromise<Data.Contracts.Room> {
            return this.$http.get(`/api/Rooms/${id}`);
        }

        getDevicesbyRoomId(roomId: number): angular.IHttpPromise<Data.Contracts.Device[]> {
            return this.$http.get(`/api/Rooms/${roomId}/Devices`);
        }

        getOpenTickets(): angular.IHttpPromise<Data.Contracts.Ticket[]> {
            return this.$http.get("/api/Tickets/Open");
        }

        getOpenTicketsByRoom(roomId: number): angular.IHttpPromise<Data.Contracts.Ticket[]> {
            return this.$http.get(`/api/Tickets/Open/Room/${roomId}`);
        }

        getOpenTicketsRecent(): angular.IHttpPromise<Data.Contracts.Ticket[]> {
            return this.$http.get("/api/Tickets/Open/Past/Minutes/1");
        }

        getActivitiesForTicket(ticketId: number): angular.IHttpPromise<Data.Contracts.Activity[]> {
            return this.$http.get(`api/Tickets/${ticketId}/Activities`);
        }

        getOpenTicketsPastMinutes(minutes: number): angular.IHttpPromise<Data.Contracts.Ticket[]> {
            return this.$http.get(`api/Tickets/Open/Past/Minutes/${minutes}`);
        }

        getOpenTicketsPastSeconds(seconds: number): angular.IHttpPromise<Data.Contracts.Ticket[]> {
            return this.$http.get(`api/Tickets/Open/Past/Seconds/${seconds}`);
        }

        getHomeSeerUrl(): ng.IHttpPromise<string> {
            return this.$http.get("/api/Config/HomeSeerUrl");
        }

        getHomeSeerDeviceControls(refId: number): ng.IHttpPromise<DeviceControl[]> {
            return this.$http.get(`/api/Devices/${refId}/Controls`);
        }
    }

    angular.module("app")
        .service("dataService", DataService);

}


