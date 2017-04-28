
(function () {
    appModule.controller('tenant.views.house.master.supplier.index', [
        '$scope','$state', '$uibModal', 'uiGridConstants', 'abp.services.app.supplier',
        function ($scope, $state, $modal, uiGridConstants, supplierService) {
            var vm = this;

            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.loading = false;
            vm.filterText = null;
            vm.currentUserId = abp.session.userId;

            vm.permissions = {
                create: abp.auth.hasPermission('Pages.Tenant.House.Master.Supplier.Create'),
                edit: abp.auth.hasPermission('Pages.Tenant.House.Master.Supplier.Edit'),
                'delete': abp.auth.hasPermission('Pages.Tenant.House.Master.Supplier.Delete')
            };

            

            var requestParams = {
                skipCount: 0,
                maxResultCount: app.consts.grid.defaultPageSize,
                sorting: null
            };

            vm.userGridOptions = {
                enableHorizontalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                enableVerticalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                paginationPageSizes: app.consts.grid.defaultPageSizes,
                paginationPageSize: app.consts.grid.defaultPageSize,
                useExternalPagination: true,
                useExternalSorting: true,
                appScopeProvider: vm,
                rowTemplate: '<div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader, \'text-muted\': !row.entity.isActive }"  ui-grid-cell></div>',
                columnDefs: [
					{
					    name: app.localize('Actions'),
					    enableSorting: false,
					    width: 120,

					    cellTemplate:
						   "<div class=\"ui-grid-cell-contents\">" +
							    "  <div class=\"btn-group dropdown\" uib-dropdown=\"\" dropdown-append-to-body>" +
							   "    <button class=\"btn btn-xs btn-primary blue\" uib-dropdown-toggle=\"\" aria-haspopup=\"true\" aria-expanded=\"false\"><i class=\"fa fa-cog\"></i> " + app.localize("Actions") + " <span class=\"caret\"></span></button>" +
							   "    <ul uib-dropdown-menu>" +
							   "      <li><a ng-if=\"grid.appScope.permissions.edit\" ng-click=\"grid.appScope.editSupplier(row.entity)\">" + app.localize("Edit") + "</a></li>" +
							   "      <li><a ng-if=\"grid.appScope.permissions.delete\" ng-click=\"grid.appScope.deleteSupplier(row.entity)\">" + app.localize("Delete") + "</a></li>" +
                               "      <li><a ng-if=\"grid.appScope.permissions.edit\" ng-click=\"grid.appScope.contactLink(row.entity)\">" + app.localize("Contacts") + "</a></li>" +
                               "      <li><a ng-if=\"grid.appScope.permissions.edit\" ng-click=\"grid.appScope.materialLink(row.entity)\">" + app.localize("Materials") + "</a></li>" +
							   "    </ul>" +
							   "  </div>" +
							   "</div>"
					},
                    {
                        name: app.localize('Name'),
                        field: 'supplierName',
                    },
                    {
                        name: app.localize('Address'),
                        field: 'address1',
                        width : 320
                    },
                    {
                        name: app.localize('City'),
                        field: 'city'
                    },
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    $scope.gridApi.core.on.sortChanged($scope, function (grid, sortColumns) {
                        if (!sortColumns.length || !sortColumns[0].field) {
                            requestParams.sorting = null;
                        } else {
                            requestParams.sorting = sortColumns[0].field + ' ' + sortColumns[0].sort.direction;
                        }

                        vm.getAll();
                    });
                    gridApi.pagination.on.paginationChanged($scope, function (pageNumber, pageSize) {
                        requestParams.skipCount = (pageNumber - 1) * pageSize;
                        requestParams.maxResultCount = pageSize;

                        vm.getAll();
                    });
                },
                data: []
            };

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

            vm.editSupplier = function (myObj) {
                openCreateOrEditModal(myObj.id);
            };

            vm.createSupplier = function () {
                openCreateOrEditModal(null);
            };

            vm.contactLink = function (myObj) {
                createSupplierContact(myObj.id);
            }

            createSupplierContact = function (objId) {
                var modalInstance = $modal.open({
                    templateUrl: '~/App/tenant/views/house/master/suppliercontact/ViewOnly.cshtml',
                    controller: 'tenant.views.house.master.suppliercontact.ViewOnly as vm',
                    backdrop: 'static',
                    keyboard: false,
                    resolve: {
                        argsupplierIdForLock: function () {
                            return objId;
                        },
                        //suppliercontactId : function () {
                        //    return null;
                        //}
                    }
                });

                modalInstance.result.then(function (result) {
                    vm.getAll();
                });
            }

            vm.materialLink = function (myObj) {
                $state.go("tenant.suppliermaterial", {
                    id: myObj.id
                });
                //createSupplierMaterial(myObj.id);
            }

            createSupplierMaterial = function (objId) {
                var modalInstance = $modal.open({
                    templateUrl: '~/App/tenant/views/house/master/suppliermaterial/createOrEditModal.cshtml',
                    controller: 'tenant.views.house.master.suppliermaterial.createOrEditModal as vm',
                    backdrop: 'static',
                    keyboard: true,
                    resolve: {
                        supplierId: function () {
                            return objId;
                        },
                    }
                });

                modalInstance.result.then(function (result) {
                    vm.getAll();
                });
            }

            vm.deleteSupplier = function (myObject) {
                abp.message.confirm(
                    app.localize('DeleteSupplierWarning', myObject.supplierName),
                    function (isConfirmed) {
                        if (isConfirmed) {
                            supplierService.deleteSupplier({
                                id: myObject.id
                            }).success(function () {
                                vm.getAll();
                                abp.notify.success(app.localize('SuccessfullyDeleted'));
                            });
                        }
                    }
                );
            };

            function openCreateOrEditModal(objId)
            {
                var modalInstance = $modal.open({
                    templateUrl: '~/App/tenant/views/house/master/supplier/createOrEditModal.cshtml',
                    controller: 'tenant.views.house.master.supplier.createOrEditModal as vm',
                    backdrop: 'static',
                    resolve: {
                        supplierId: function () {
                            return objId;
                        }
                    }
                });

                modalInstance.result.then(function (result) {
                    vm.getAll();
                });
            }

            function openCreatOrEditDetail(objId)
            {
                $state.go('tenant.supplierdetail');
            }

            vm.exportToExcel = function () {
                vm.loading = true;
                supplierService.getAllToExcel({
                    skipCount: requestParams.skipCount,
                    maxResultCount: requestParams.maxResultCount,
                    sorting: requestParams.sorting,
                    filter: vm.filterText
                })
                    .success(function (result) {
                        app.downloadTempFile(result);
                        vm.loading = false;
                    });
            };

            vm.import = function () {
                importModal(null);
            };

            function importModal(objId) {
                var modalInstance = $modal.open({
                    templateUrl: '~/App/tenant/views/house/master/supplier/importModal.cshtml',
                    controller: 'tenant.views.house.master.supplier.importModal as vm',
                    backdrop: 'static'
                });

                modalInstance.result.then(function (result) {
                    vm.getAll();
                    vm.loading = false;
                });
            }


            vm.getAll();
        }]);
})();

