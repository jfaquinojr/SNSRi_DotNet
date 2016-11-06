module App {
    import User = Data.Contracts.User;

    export interface IUsersDataService {
        getAllUsers(): ng.IHttpPromise<User[]>;
        getUser(id: number): ng.IHttpPromise<User>;
        createUser(user: User): ng.IHttpPromise<number>;
        deleteUser(id: number): ng.IHttpPromise<void>;
        updateUser(user: User): ng.IHttpPromise<void>;
    }

    export class UsersDataService implements IUsersDataService {

        static $inject = ["$http"];
        constructor(private $http) {
            console.log("initializing UsersDataService");
        }

        getAllUsers(): ng.IHttpPromise<User[]> {
            return this.$http.get("/api/Users");
        }

        getUser(id: number): ng.IHttpPromise<User> {
            return this.$http.get(`/api/Users/${id}`);
        }

        createUser(user: User): ng.IHttpPromise<number> {
            return this.$http.get(`/api/CreateUser`);
        }

        deleteUser(id: number): ng.IHttpPromise<void> {
            return this.$http.get(`/api/DeleteUser/${id}`);
        }

        updateUser(user: User): ng.IHttpPromise<void> {
            throw new Error("Not implemented");
        }
    }

    angular.module("app")
        .service("usersDataService", UsersDataService);
}

