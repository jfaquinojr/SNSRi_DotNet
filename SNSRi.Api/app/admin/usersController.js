var App;
(function (App) {
    var User = Data.Contracts.User;
    var UsersController = (function () {
        function UsersController($scope, $window, $location, usersDataService) {
            this.$scope = $scope;
            this.$window = $window;
            this.$location = $location;
            this.usersDataService = usersDataService;
            console.log("initializing UsersController");
            var self = this;
            self.vm = $scope;
            usersDataService.getAllUsers()
                .then(function (result) {
                self.users = result.data;
                console.log(JSON.stringify(self.users));
            });
        }
        UsersController.prototype.editUser = function (user) {
            this.selectedUser = user;
            this.editingUser = angular.copy(user, this.editingUser);
            this.$window.showMetroDialog("#dialog-userform", null);
        };
        UsersController.prototype.createUser = function () {
            this.selectedUser = null;
            this.editingUser = new User();
            this.$window.showMetroDialog("#dialog-userform", null);
        };
        UsersController.prototype.saveUser = function () {
            var user = this.editingUser;
            var self = this;
            if (this.selectedUser) {
                self._updateUser(user);
            }
            else {
                self._createUser(user);
            }
        };
        UsersController.prototype._updateUser = function (user) {
            var self = this;
            this.usersDataService.updateUser(user)
                .then(function (result) {
                self.selectedUser = angular.copy(user, self.selectedUser);
                $.Notify({
                    caption: "Saved.",
                    content: "User has been updated.",
                    type: "success"
                });
                self.$window.hideMetroDialog("#dialog-userform");
            })
                .catch(function (reason) {
                $.Notify({
                    caption: "Error",
                    content: "User was not saved. " + reason,
                    type: "error"
                });
            });
        };
        UsersController.prototype._createUser = function (user) {
            var self = this;
            this.usersDataService.createUser(user)
                .then(function (result) {
                self.selectedUser = angular.copy(user, self.selectedUser);
                $.Notify({
                    caption: "Created.",
                    content: "User has been created.",
                    type: "success"
                });
                self.$window.hideMetroDialog("#dialog-userform");
            })
                .catch(function (reason) {
                $.Notify({
                    caption: "Error",
                    content: "User was not created. " + reason,
                    type: "error"
                });
            });
        };
        UsersController.$inject = ["$scope", "$window", "$location", "usersDataService"];
        return UsersController;
    }());
    App.UsersController = UsersController;
    angular.module("app")
        .controller("UsersController", UsersController);
})(App || (App = {}));
//# sourceMappingURL=usersController.js.map