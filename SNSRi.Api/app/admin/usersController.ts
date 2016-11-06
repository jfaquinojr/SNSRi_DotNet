module App {
    import User = Data.Contracts.User;

    export class UsersController {

        users: User[];
        selectedUser: User;

        static $inject = ["$scope", "$location", "usersDataService"];
        constructor(public $scope, private $location, private usersDataService: IUsersDataService) {

            console.log("initializing UsersController");

            const self = this;

            usersDataService.getAllUsers()
                .then(result => {
                    self.users = result.data;
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

        //private rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
        //    // Unbind first in order to avoid any duplicate handler (see https://github.com/l-lin/angular-datatables/issues/87)
        //    $("td", nRow).unbind("click");
        //    $("td", nRow)
        //        .bind("click", () => {
        //                this.$scope.$apply(function() {
        //                    this.selectUser(aData);
        //                });
        //            });
        //    return nRow;
        //}

        //private selectUser(user) {
        //    this.$scope.message = user.Id + " - " + user.Email;
        //    this.$scope.SelectedUser = user;
        //    const url = `/Users/Edit/${user.Id}`;
        //    //alert(url);
        //    this.$location.path(url);
        //}
    }

    angular.module("app")
        .controller("UsersController", UsersController);
}

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

