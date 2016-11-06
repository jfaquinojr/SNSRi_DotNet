var App;
(function (App) {
    function UserForm() {
        return {
            restrict: "E",
            scope: true,
            templateUrl: "/Users/Edit"
        };
    }
    App.UserForm = UserForm;
    angular.module("app")
        .directive("userForm", UserForm);
})(App || (App = {}));
//# sourceMappingURL=userForm.js.map