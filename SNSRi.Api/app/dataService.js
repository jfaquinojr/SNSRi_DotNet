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
        getOpenTicketsByRoom: getOpenTicketsByRoom
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

}