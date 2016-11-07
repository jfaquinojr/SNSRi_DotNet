module App {

    class AdminPage {
        Name: string;
        Icon: string;
        Title: string;
        Url: string;
    }
    
    export class MainController {

        activeMenu: string;
        userName: string;
        userId: number;
        selectedPage: AdminPage;
        pages: AdminPage[];
        vm: any;

        static $inject = ["$scope", "$location"];
        constructor(public $scope, private $location) {

            console.log("Initializing MainController");

            this.vm = $scope;

            this.userName = "demo@mail.com";
            this.userId = 1;

            this.pages = [
                { Name: "Users", Icon: "mif-users", Title: "Users", Url: "" },
                { Name: "Rooms", Icon: "mif-hotel", Title: "Rooms", Url: "" },
                { Name: "Devices", Icon: "mif-switch", Title: "Devices", Url: "" }
            ];

            this.selectedPage = this.pages[0];

        }

        selectPage(page: AdminPage) {
            console.log("New Page selected");
            this.selectedPage = page;
            this.$location.path("/" + page.Name);
        }
    }

    angular.module("app")
        .controller("mainController", MainController);

}
