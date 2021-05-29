/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('fashionshop.orders', ['fashionshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('orders', {
                url: "/orders",
                parent: 'base',
                templateUrl: "/app/components/orders/orderListView.html",
                controller: "orderListController"
            }).state('order_detail', {
                url: "/order_detail/:id",
                parent: 'base',
                templateUrl: "/app/components/orders/orderDetailView.html",
                controller: "orderDetailController"
            });
    }
})();