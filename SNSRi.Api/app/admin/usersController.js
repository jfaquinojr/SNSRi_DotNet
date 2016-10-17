var usersController = function($scope, DTOptionsBuilder, DTColumnBuilder, $http) {

    var self = this;

    self.users = [];

    $http.get('/api/users').then(function(results) {
        self.users = results.data;
    });
    
    $scope.dtOptions = DTOptionsBuilder.fromSource(self.users)
        .withPaginationType('full_numbers');
    console.log("UsersController!!!");
};

angular.module("app-admin")
    .controller("UsersController", usersController);
