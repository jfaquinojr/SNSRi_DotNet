
module App {

    import Ticket = Data.Contracts.Ticket;


    export interface IEventsController {
        
    }


    export class EventsController implements IEventsController {



    }

    //angular.module("app")
    //.service("dataService", App.DataService)
    //.controller("eventsController", EventsController);

}


function createActivity(ticket, comment) {
    return {
        TicketId: ticket.Id,
        Comment: comment,
        CreatedBy: 1
    };
}





app.directive("eventTile",
    function () {

        return {
            templateUrl: "/Home/EventTile",
            restrict: "E"
            //controller: function($scope) {
            //    $scope.Clicked = function(device) {
            //        alert(JSON.stringify(device));
            //    }
            //}
        };

    });



