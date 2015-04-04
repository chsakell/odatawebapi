angular.module('mainApp')
    .controller('appCtrl', function ($scope, employeeService, notificationFactory) {

        $scope.currentEmployee = {};

        // Get Top 10 Employees
        $scope.getTop10Employees = function () {
            (new employeeService()).$getTop10()
                .then(function (data) {
                    $scope.employees = data.value;
                    $scope.currentEmployee = $scope.employees[0];
                    $scope.setCurrentEmployeeAddress();
                    $scope.setCurrentEmployeeCompany();
                    notificationFactory.success('Employeess loaded.');
                });
        };

        // Set active employee for patch update
        $scope.setEmployee = function (employee) {
            $scope.currentEmployee = employee;
            $scope.setCurrentEmployeeAddress();
            $scope.setCurrentEmployeeCompany();
        };

        $scope.setCurrentEmployeeAddress = function () {
            var currentEmployee = $scope.currentEmployee;

            return (new employeeService({
                "ID": currentEmployee.ID,
            })).$getEmployeeAdderss({ key: currentEmployee.ID })
            .then(function (data) {
                $scope.currentEmployee.City = data.City;
                $scope.currentEmployee.Country = data.Country;
                $scope.currentEmployee.State = data.State;
            });
        }

        $scope.setCurrentEmployeeCompany = function () {
            var currentEmployee = $scope.currentEmployee;

            return (new employeeService({
                "ID": currentEmployee.ID,
            })).$getEmployeeCompany({ key: currentEmployee.ID })
            .then(function (data) {
                $scope.currentEmployee.Company = data.Name;
            });
        }

        // Update Selected Employee
        $scope.updateEmployee = function () {
            var currentEmployee = $scope.currentEmployee;
            console.log(currentEmployee.Email);
            if (currentEmployee) {
                return (new employeeService({
                    "ID": currentEmployee.ID,
                    "FirstName": currentEmployee.FirstName,
                    "Surname": currentEmployee.Surname,
                    "Email": currentEmployee.Email
                })).$patch({ key: currentEmployee.ID })
                .then(function (data) {
                    notificationFactory.success('Employee with ID ' + currentEmployee.ID + ' updated.')
                });
            }
        }

        $scope.deleteEmployee = function () {
            var currentEmployee = $scope.currentEmployee;

            return (new employeeService({
                "ID": currentEmployee.ID,
            })).$deleteEmployee({ key: currentEmployee.ID })
            .then(function (data) {
                notificationFactory.success('Employee with ID ' + currentEmployee.ID + ' removed.');
                $scope.getTop10Employees();
            });
        }
    });