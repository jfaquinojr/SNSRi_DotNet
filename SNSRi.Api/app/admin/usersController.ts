module App {
    import User = Data.Contracts.User;

    export class UsersController {

        users: User[];
        selectedUser: User;
        editingUser: User;
        vm: any;
        loadingPromise: any; // cg-busy indicator

        static $inject = ["$scope", "$window", "$location", "usersDataService"];

        constructor(public $scope, private $window, private $location, private usersDataService: IUsersDataService) {

            console.log("initializing UsersController");

            const self = this;
            self.vm = $scope;

            this.loadUsers();

        }

        editUser(user: User): void {
            this.selectedUser = user;
            if (!angular.equals(user, this.editingUser)) {
                this.editingUser = angular.copy(user, this.editingUser);
            }
            this.$window.showMetroDialog("#dialog-userform", null);
        }

        createUser(): void {
            this.selectedUser = null;
            this.editingUser = new User();
            this.$window.showMetroDialog("#dialog-userform", null);
        }

        deleteUser(user: User): void {

            if (!confirm("Are you sure you want to delete this record?"))
                return;

            const self = this;
            this.selectedUser = null;
            this.editingUser = null;
            this.loadingPromise = this.usersDataService.deleteUser(user.Id)
                .then(() => {
                    $.Notify({
                        caption: "Deleted.",
                        content: "User has been deleted.",
                        type: "success"
                    });

                    self.loadUsers();
                });
        }


        saveUser() {
            const user = this.editingUser;
            const self = this;

            if (this.selectedUser) {
                self._updateUser(user);
            } else {
                self._createUser(user);
            }
            //self.loadUsers();
        }

        private loadUsers() {
            const self = this;
            this.loadingPromise = this.usersDataService.getAllUsers()
                .then(result => {
                    self.users = result.data;
                });
        }

        private _updateUser(user: User) {
            const self = this;
            this.loadingPromise = this.usersDataService.updateUser(user)
                .then(result => {

                    
                    if (!angular.equals(user, self.selectedUser)) {
                        self.selectedUser = angular.copy(user, self.selectedUser);
                    }

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
            this.loadingPromise = this.usersDataService.createUser(user)
                .then(result => {

                    user.Id = result.data;
                    self.users.push(user);
                    self.selectedUser = user;

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


