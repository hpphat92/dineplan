
(function () {
    appModule.controller('tenant.views.house.master.supplier.createOrEditModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.supplier', 'supplierId', 'abp.services.app.commonLookup',
        function ($scope, $modalInstance, supplierService, supplierId, commonLookupService) {
            var vm = this;

            vm.saving = false;
            vm.supplier = null;
			$scope.existall = true;
			
			
			vm.save = function () {
			    
			   if ($scope.existall == false)
			       return;
			   $scope.errmessage = "";

			   //if (vm.supplier.defaultCreditDays == 0)
			   //    $scope.errmessage = $scope.errmessage + app.localize('DefaultCrDtZeroErr', vm.supplier.defaultCreditDays)

			   if ($scope.errmessage != "")
			   {
			       abp.notify.warn($scope.errmessage, 'Required');
			       return;
			   }

			   vm.saving = true;
			   vm.supplier.supplierName = vm.supplier.supplierName.toUpperCase();
			   if (vm.supplier.taxRegistrationNumber!=null && vm.supplier.taxRegistrationNumber.length>0)
			       vm.supplier.taxRegistrationNumber = vm.supplier.taxRegistrationNumber.toUpperCase();

                supplierService.createOrUpdateSupplier({
                    supplier: vm.supplier
                }).success(function () {
                    abp.notify.info('\' Supplier \'' + app.localize('SavedSuccessfully'));
                    $modalInstance.close();
                }).finally(function () {
                    vm.saving = false;
                });
            };

			 vm.existall = function ()
            {
                
			     if (vm.supplier.supplierName == null) {
			         vm.existall = false;
			         return;
			     }
				 
                supplierService.getAll({
                    skipCount: 0,
                    maxResultCount: 2,
                    sorting: 'supplierName',
                    filter: vm.supplier.supplierName,
                    operation: 'SEARCH'
                }).success(function (result) {
                    
                    $scope.existall = true;
                    if (result.totalCount > 0 && vm.supplier.id == null) {
                        vm.saving = false;
                        vm.loading = false;
                        abp.notify.info('\' ' + vm.supplier.supplierName + '\' ' + app.localize('NameExist'));
                        $scope.existall = false;
                    }
                });
            };

            vm.cancel = function () {
                $modalInstance.dismiss();
            };
			
			 vm.getComboValue = function (item) {
                return parseInt(item.value);
            };
			
	        function init() {
                supplierService.getSupplierForEdit({
                    Id: supplierId
                }).success(function (result) {
                    //result.supplier.countryRefId = null;
                    //result.supplier.stateRefId = null;
                    if (result.supplier.id != null) {
                        //commonLookupService.getStateCodeFromCity({ Id: result.supplier.cityRefId }).success(function (stateResult) {
                        //    vm.supplier.stateRefId = stateResult.items[0].stateRefId;
                        //    vm.getCountry();
                        //});
                    }
                 
                  
                    vm.supplier = result.supplier;
                    if (result.supplier.id == null) {
                        vm.supplier.defaultCreditDays = "";
                    }
                    if (parseFloat(vm.supplier.defaultCreditDays) == 0)
                        vm.supplier.defaultCreditDays = "";

                });
            }
            init();

        }
    ]);
})();

