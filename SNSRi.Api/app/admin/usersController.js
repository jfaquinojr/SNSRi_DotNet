var App;
(function (App) {
    var UsersController = (function () {
        function UsersController($scope, $location, usersDataService) {
            this.$scope = $scope;
            this.$location = $location;
            this.usersDataService = usersDataService;
            console.log("initializing UsersController");
            var self = this;
            usersDataService.getAllUsers()
                .then(function (result) {
                self.users = result.data;
                debugger;
                console.log(JSON.stringify(self.users));
            });
            //this.$scope.dtOptions = DTOptionsBuilder.fromSource(JSON.stringify(data))
            //    .withPaginationType("full_numbers")
            //    .withOption("rowCallback", self.rowCallback);
            //this.$scope.dtColumns = [
            //    self.DTColumnBuilder.newColumn("Id").withTitle("ID"),
            //    self.DTColumnBuilder.newColumn("Username").withTitle("User name"),
            //    self.DTColumnBuilder.newColumn("Email").withTitle("Email")
            //];
        }
        UsersController.$inject = ["$scope", "$location", "usersDataService"];
        return UsersController;
    }());
    App.UsersController = UsersController;
    angular.module("app")
        .controller("UsersController", UsersController);
})(App || (App = {}));
//var usersController = function ($scope, DTOptionsBuilder, DTColumnBuilder, $http, $location) {
//    var self = $scope;
//    $scope.users = [];
//    $scope.SelectedUser = {};
//    $http.get("/api/users").then(function(results) {
//        $scope.users = results.data;
//        console.log("users count: " + $scope.users.length);
//        console.log(JSON.stringify($scope.users));
//    });
//    $scope.dtOptions = DTOptionsBuilder.fromSource("/api/users?json=true")
//        .withPaginationType("full_numbers")
//        .withOption("rowCallback", rowCallback);
//    $scope.dtColumns = [
//        DTColumnBuilder.newColumn("Id").withTitle("ID"),
//        DTColumnBuilder.newColumn("Username").withTitle("User name"),
//        DTColumnBuilder.newColumn("Email").withTitle("Email")
//    ];
//    function selectUser(user) {
//        $scope.message = user.Id + " - " + user.Email;
//        $scope.SelectedUser = user;
//        var url = "/Users/Edit/" + user.Id;
//        //alert(url);
//        $location.path(url);
//    }
//    function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
//        // Unbind first in order to avoid any duplicate handler (see https://github.com/l-lin/angular-datatables/issues/87)
//        $("td", nRow).unbind("click");
//        $("td", nRow).bind("click", function () {
//            $scope.$apply(function () {
//                selectUser(aData);
//            });
//        });
//        return nRow;
//    }
//};
//# sourceMappingURL=usersController.js.map