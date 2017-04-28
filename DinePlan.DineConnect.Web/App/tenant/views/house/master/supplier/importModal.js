(function () {
    appModule.controller('tenant.views.house.master.supplier.importModal', [
        '$scope', 'appSession', '$uibModalInstance', 'FileUploader',
        function ($scope, appSession, $uibModalInstance, fileUploader) {
            var vm = this;
            vm.loading = false;

            vm.uploader = new fileUploader({
                url: abp.appPath + 'Import/ImportSupplier',
                queueLimit: 1,
                filters: [{
                    name: 'imageFilter',
                    fn: function (item, options) {
                        console.log("item : " + item);
                        var type = '|' + item.type.slice(item.type.lastIndexOf('/') + 1) + '|';
                        console.log("Type : " + type);
                        if ('|vnd.ms-excel|vnd.openxmlformats-officedocument.spreadsheetml.sheet|'.indexOf(type) === -1) {
                            abp.message.warn(app.localize('ImportTemplate_Warn_FileType'));
                            return false;
                        }
                        return true;
                    }
                }]
            });

            vm.importpath = abp.appPath + 'Import/ImportSupplierTemplate';

            vm.save = function () {
                vm.loading = true;
                vm.uploader.uploadAll();
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };

            vm.uploader.onSuccessItem = function (fileItem, response, status, headers) {
                if (response.error != null) {
                    abp.message.warn(response.error.message);
                    $uibModalInstance.close();
                    vm.loading = false;
                    return;
                }
                abp.notify.info(app.localize("FINISHED"));
                $uibModalInstance.close();
                vm.loading = false;
            };
        }

    ]);
})();
