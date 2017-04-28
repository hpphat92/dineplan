
(function () {
    appModule.controller('tenant.views.xero.xero', [
        '$scope','$state', '$uibModal', 'uiGridConstants', 'abp.services.app.supplier',
        function ($scope, $state, $modal, uiGridConstants, supplierService) {
            var vm = this;

            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.loading = false;
            vm.filterText = null;
            vm.currentUserId = abp.session.userId;

            vm.getAll = function () {
                vm.loading = true;
                supplierService.getAll({
                    skipCount: requestParams.skipCount,
                    maxResultCount: requestParams.maxResultCount,
                    sorting: requestParams.sorting,
                    filter: vm.filterText
                }).success(function (result) {
                    vm.userGridOptions.totalItems = result.totalCount;
                    vm.userGridOptions.data = result.items;
                }).finally(function () {
                    vm.loading = false;
                });
            };
          
        
        }]);
})();

