var App;
(function (App) {
    var AdminPage = (function () {
        function AdminPage() {
        }
        return AdminPage;
    }());
    var MainController = (function () {
        function MainController($scope, $location) {
            this.$scope = $scope;
            this.$location = $location;
            console.log("Initializing MainController");
            this.vm = $scope;
            this.userName = "demo@mail.com";
            this.userId = 1;
            this.pages = [
                { Name: "Users", Icon: "mif-users", Title: "Users", Url: "" },
                { Name: "Rooms", Icon: "mif-hotel", Title: "Rooms", Url: "" },
                { Name: "Devices", Icon: "mif-switch", Title: "Devices", Url: "" }
            ];
            this.selectedPage = this.pages[0];
        }
        MainController.prototype.selectPage = function (page) {
            console.log("New Page selected");
            this.selectedPage = page;
            this.$location.path("/" + page.Name);
        };
        MainController.$inject = ["$scope", "$location"];
        return MainController;
    }());
    App.MainController = MainController;
    angular.module("app")
        .controller("mainController", MainController);
})(App || (App = {}));
//# sourceMappingURL=mainController.js.map