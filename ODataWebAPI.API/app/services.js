angular.module('mainApp')
    .factory('employeeService', function ($resource) {
        var odataUrl = '/odata/Employees';
        return $resource('', {},
            {
                'getAll': { method: 'GET', url: odataUrl },
                'getTop10': { method: 'GET', url: odataUrl + '?$top=10' },
                'create': { method: 'POST', url: odataUrl },
                'patch': { method: 'PATCH', params: { key: '@key' }, url: odataUrl + '(:key)' },
                'getEmployee': { method: 'GET', params: { key: '@key' }, url: odataUrl + '(:key)' },
                'getEmployeeAdderss': { method: 'GET', params: { key: '@key' }, url: odataUrl + '(:key)' + '/Address' },
                'getEmployeeCompany': { method: 'GET', params: { key: '@key' }, url: odataUrl + '(:key)' + '/Company' },
                'deleteEmployee': { method: 'DELETE', params: { key: '@key' }, url: odataUrl + '(:key)' },
                'addEmployee': { method: 'POST', url: odataUrl }
            });
    }).factory('notificationFactory', function () {
        return {
            success: function (text) {
                toastr.success(text, "Success");
            },
            error: function (text) {
                toastr.error(text, "Error");
            }
        };
    })