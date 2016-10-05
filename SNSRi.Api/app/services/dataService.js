var app = angular.module("app");

app.factory("dataService", dataService);

function dataService($http) {


    var svc = $http;

    return {
        getRoom: getRoom,
        getRooms: getRooms,
        getDevicesbyRoomId: getDevicesbyRoomId,
        getOpenTickets: getOpenTickets,
        getOpenTicketsRecent: getOpenTicketsRecent,
        getOpenTicketsByRoom: getOpenTicketsByRoom,
        getActivitiesForTicket: getActivitiesForTicket,
        getOpenTicketsPastMinutes: getOpenTicketsPastMinutes,
        getOpenTicketsPastSeconds: getOpenTicketsPastSeconds,
        createActivity: createActivity,
        closeTicket: closeTicket
    }

    function createActivity(activityData) {
        return svc.post("/api/CreateActivity", JSON.stringify(activityData));
    }

    function closeTicket(activityData) {
        return svc.post("/api/CloseTicket", JSON.stringify(activityData));
    }

    function getHomeSeerDevice(refId) {
        return svc.get(urlHomeSeer + "/JSON?request=getstatus&ref=" + refId);
    }

    function getRooms() {
        return svc.get("/api/Rooms");
    }

    function getRoom(id) {
        return svc.get("/api/Rooms/" + id);
    }

    function getDevicesbyRoomId(roomId) {
        return svc.get("/api/Rooms/" + roomId + "/Devices");
    }

    function getOpenTickets() {
        return svc.get("/api/Tickets/Open");
    }

    function getOpenTicketsByRoom(roomId) {
        return svc.get("/api/Tickets/Open/Room/" + roomId);
    }

    function getOpenTicketsRecent() {
        return svc.get("/api/Tickets/Open/Past/Minutes/1");
    }

    function getActivitiesForTicket(ticketId) {
        return svc.get("api/Tickets/" + ticketId + "/Activities");
    }

    function getOpenTicketsPastMinutes(minutes) {
        return svc.get("api/Tickets/Open/Past/Minutes/" + minutes);
    }

    function getOpenTicketsPastSeconds(seconds) {
        return svc.get("api/Tickets/Open/Past/Seconds/" + seconds);
    }

    

}