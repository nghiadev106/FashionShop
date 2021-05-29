//import { forEach } from "angular";

(function (app) {
    app.controller('orderDetailController', orderDetailController);

    orderDetailController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService', '$stateParams'];

    function orderDetailController(apiService, $scope, notificationService, $state, commonService, $stateParams) {
        $scope.order = {};
        $scope.total = 0;

        $scope.UpdateOrder = UpdateOrder;

        function loadOrderDetail() {
            apiService.get('/api/orders/getbyid/' + $stateParams.id, null, function (result) {
                $scope.order = result.data;

                angular.forEach($scope.order[0].OrderDetails, function (value, key) {
                    if (value.PromotionPrice !== null) {
                        $scope.total = $scope.total + value.PromotionPrice * value.Quantitty
                    } else {
                        $scope.total = $scope.total + value.Price * value.Quantitty
                    }                   
                });
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }
        function UpdateOrder() {
            $scope.order.MoreImages = JSON.stringify($scope.moreImages)
            apiService.put('/api/orders/update', $scope.order,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật.');
                    $state.go('orders');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }

        loadOrderDetail();
    }

})(angular.module('fashionshop.orders'));