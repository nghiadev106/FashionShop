(function (app) {
    app.controller('homeController', homeController);
    homeController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];
    function homeController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.statistic = [];
        function GetStatistic() {
            apiService.get('/api/home/get-statistic', null, function (result) {               
                $scope.statistic = result.data;
            }, function () {
                notificationService.displayWarning('Không thể tải được dánh sách hóa đơn.');
            });
        }

     GetStatistic();
    }
})(angular.module('app'));