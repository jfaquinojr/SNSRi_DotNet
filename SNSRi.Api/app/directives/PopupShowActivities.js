var App;
(function (App) {
    var PopupShowActivitiesController = (function () {
        function PopupShowActivitiesController($scope, $window, dataService, notificationService) {
            var _this = this;
            this.$scope = $scope;
            this.$window = $window;
            this.dataService = dataService;
            this.notificationService = notificationService;
            notificationService.subscribe("editTicketEvent", function (event, args) { return _this.eventOpened(event, args); }, $scope);
        }
        PopupShowActivitiesController.prototype.addActivity = function (comment) {
            var self = this;
            var newActivity = this.createActivity(self.ticket, comment);
            this.dataService.createActivity(newActivity)
                .then(function (result) {
                var activityId = result.data;
                // add the newly added activity on our list
                self.activities.unshift({
                    Id: activityId,
                    TicketId: self.ticket.Id,
                    Comment: comment,
                    CreatedOn: new Date(),
                    CreatedBy: 1,
                    ModifiedOn: new Date(),
                    ModifiedBy: 1
                });
                self.comment = "";
                $.Notify({
                    caption: "Success",
                    content: "Activity created.",
                    type: "success"
                });
            })
                .catch(function (reason) {
                $.Notify({
                    caption: "Create Activity Failed",
                    content: reason,
                    type: "alert"
                });
            });
        };
        PopupShowActivitiesController.prototype.closeTicket = function (comment) {
            var self = this;
            var newActivity = this.createActivity(self.ticket, comment);
            this.dataService.closeTicket(newActivity)
                .then(function (result) {
                self.comment = "";
                self.notificationService.notify("CloseTicket", self.ticket);
                $.Notify({
                    caption: "Success",
                    content: "Ticket closed.",
                    type: "success"
                });
            })
                .catch(function (error) {
                $.Notify({
                    caption: "Unable to close Ticket",
                    content: error,
                    type: "alert"
                });
            });
        };
        PopupShowActivitiesController.prototype.eventOpened = function (event, ticket) {
            //console.log("eventOpened captured.");
            var self = this;
            self.loadActivities(ticket)
                .then(function () {
                self.$window.hideMetroDialog("#dialog-activities");
                self.$window.showMetroDialog("#dialog-activities", null);
            });
        };
        PopupShowActivitiesController.prototype.loadActivities = function (ticket) {
            var self = this;
            return self.dataService.getActivitiesForTicket(ticket.Id)
                .then(function (result) {
                self.ticket = ticket;
                self.activities = result.data;
            });
        };
        PopupShowActivitiesController.prototype.createActivity = function (ticket, comment) {
            return {
                TicketId: ticket.Id,
                Comment: comment,
                CreatedBy: 1
            };
        };
        PopupShowActivitiesController.$inject = ["$scope", "$window", "dataService", "notificationService"];
        return PopupShowActivitiesController;
    }());
    App.PopupShowActivitiesController = PopupShowActivitiesController;
    function PopupShowActivitiesDirective() {
        console.log("Inside App.Directives.Toolbox!");
        return {
            restrict: "E",
            scope: {},
            controller: PopupShowActivitiesController,
            controllerAs: "vm",
            templateUrl: "/Home/PopupShowActivities"
        };
    }
    App.PopupShowActivitiesDirective = PopupShowActivitiesDirective;
    angular.module("app")
        .directive("popupShowActivities", PopupShowActivitiesDirective)
        .controller("popupShowActivitiesController", PopupShowActivitiesController);
})(App || (App = {}));
//# sourceMappingURL=PopupShowActivities.js.map