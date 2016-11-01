var App;
(function (App) {
    var DataService = (function () {
        function DataService($http) {
            var _this = this;
            this.$http = $http;
            this.$http.get("/api/Config/HomeSeerUrl").then(function (url) {
                _this.homeSeerUrl = url;
            });
        }
        DataService.prototype.createActivity = function (activity) {
            return this.$http.post("/api/CreateActivity", JSON.stringify(activity));
        };
        DataService.prototype.closeTicket = function (activity) {
            return this.$http.post("/api/CloseTicket", JSON.stringify(activity));
        };
        DataService.prototype.getHomeSeerDevice = function (refId) {
            return this.$http.get(this.homeSeerUrl + "/JSON?request=getstatus&ref=" + refId);
        };
        DataService.prototype.getRooms = function () {
            return this.$http.get("/api/Rooms");
        };
        DataService.prototype.getRoom = function (id) {
            return this.$http.get("/api/Rooms/" + id);
        };
        DataService.prototype.getDevicesbyRoomId = function (roomId) {
            return this.$http.get("/api/Rooms/" + roomId + "/Devices");
        };
        DataService.prototype.getOpenTickets = function () {
            return this.$http.get("/api/Tickets/Open");
        };
        DataService.prototype.getOpenTicketsByRoom = function (roomId) {
            return this.$http.get("/api/Tickets/Open/Room/" + roomId);
        };
        DataService.prototype.getOpenTicketsRecent = function () {
            return this.$http.get("/api/Tickets/Open/Past/Minutes/1");
        };
        DataService.prototype.getActivitiesForTicket = function (ticketId) {
            return this.$http.get("api/Tickets/" + ticketId + "/Activities");
        };
        DataService.prototype.getOpenTicketsPastMinutes = function (minutes) {
            return this.$http.get("api/Tickets/Open/Past/Minutes/" + minutes);
        };
        DataService.prototype.getOpenTicketsPastSeconds = function (seconds) {
            return this.$http.get("api/Tickets/Open/Past/Seconds/" + seconds);
        };
        DataService.$inject = ["$http"];
        return DataService;
    }());
    App.DataService = DataService;
    angular.module("app")
        .service("dataService", DataService);
})(App || (App = {}));
//# sourceMappingURL=dataService.js.map