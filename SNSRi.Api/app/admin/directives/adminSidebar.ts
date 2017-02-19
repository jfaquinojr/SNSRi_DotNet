var app = angular.module("app");

app.directive("adminSidebar", function() {
  return {
    restrict: "E",
    templateUrl: "/Admin/AdminSidebar",
    scope: true,
    controller: function($scope) {
      
    }
  }
})