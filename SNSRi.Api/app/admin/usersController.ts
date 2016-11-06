module App {
    import User = Data.Contracts.User;

    export class UsersController {

        users: User[];
        selectedUser: User;
        editingUser: User;
        vm: any;

        static $inject = ["$scope", "$window", "$location", "usersDataService"];

        constructor(public $scope, private $window, private $location, private usersDataService: IUsersDataService) {

            console.log("initializing UsersController");

            const self = this;
            self.vm = $scope;

            usersDataService.getAllUsers()
                .then(result => {
                    self.users = result.data;
                    console.log(JSON.stringify(self.users));
                });

        }

        editUser(user: User): void {
            this.selectedUser = user;
            this.editingUser = angular.copy(user, this.editingUser);
            this.$window.showMetroDialog("#dialog-userform", null);
        }

        createUser(): void {
            this.selectedUser = null;
            this.editingUser = new User();
            this.$window.showMetroDialog("#dialog-userform", null);
        }


        saveUser() {
            const user = this.editingUser;
            const self = this;

            if (this.selectedUser) {
                self._updateUser(user);
            } else {
                self._createUser(user);
            }
        }

        private _updateUser(user: User) {
            const self = this;
            this.usersDataService.updateUser(user)
                .then(result => {

                    self.selectedUser = angular.copy(user, self.selectedUser);

                    $.Notify({
                        caption: "Saved.",
                        content: "User has been updated.",
                        type: "success"
                    });

                    self.$window.hideMetroDialog("#dialog-userform");
                })
                .catch(reason => {
                    $.Notify({
                        caption: "Error",
                        content: "User was not saved. " + reason,
                        type: "error"
                    });
                });
        }

        private _createUser(user: User) {
            const self = this;
            this.usersDataService.createUser(user)
                .then(result => {

                    self.selectedUser = angular.copy(user, self.selectedUser);

                    $.Notify({
                        caption: "Created.",
                        content: "User has been created.",
                        type: "success"
                    });

                    self.$window.hideMetroDialog("#dialog-userform");
                })
                .catch(reason => {
                    $.Notify({
                        caption: "Error",
                        content: "User was not created. " + reason,
                        type: "error"
                    });
                });
        }

    }

    angular.module("app")
        .controller("UsersController", UsersController);
}


