var usersController = function($scope, DTOptionsBuilder, DTColumnBuilder, $http) {

    var self = $scope;

    $scope.users = [];

    $scope.SelectedUser = {};

    $http.get("/api/users").then(function(results) {
        $scope.users = results.data;
        console.log("users count: " + $scope.users.length);
        console.log(JSON.stringify($scope.users));
    });

    $scope.dtOptions = DTOptionsBuilder.fromSource("/api/users?json=true")
        .withPaginationType("full_numbers")
        .withOption("rowCallback", rowCallback);
    $scope.dtColumns = [
        DTColumnBuilder.newColumn("Id").withTitle("ID"),
        DTColumnBuilder.newColumn("Username").withTitle("User name"),
        DTColumnBuilder.newColumn("Email").withTitle("Email")
    ];

    function selectUser(user) {
        $scope.message = user.Id + " - " + user.Email;
        $scope.SelectedUser = user;
    }

    function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
        // Unbind first in order to avoid any duplicate handler (see https://github.com/l-lin/angular-datatables/issues/87)
        $("td", nRow).unbind("click");
        $("td", nRow).bind("click", function () {
            $scope.$apply(function () {
                selectUser(aData);
            });
        });
        return nRow;
    }
    
    

};

angular.module("app-admin")
    .controller("UsersController", usersController);
