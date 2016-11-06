module App.Common {
    interface IDataAccessService {
        getUserResource(): ng.resource.IResourceClass<IUserResource>;
    }

    interface IUserResource extends ng.resource.IResource<Data.Contracts.User> {

    }

    export class DataAccessService
        implements IDataAccessService {

        static $inject = ["$resource"];

        constructor(private $resource: ng.resource.IResourceService) {

        }

        getUserResource(): ng.resource.IResourceClass<IUserResource> {
            return this.$resource("/api/users/:userId");
        }
    }

    angular
        .module("app")
        .service("dataAccessService",
            DataAccessService);
}