var DashboardController = function ($scope, $http, $location, $route, $routeParams) {

    $scope.item = {}

    $scope.item.CantidadRecepciones = 0;
    $scope.item.CantidadEgresos = 0;
    $scope.item.CantidadCorrespondencia = 0;
    $scope.item.CantidadPaquetes = 0;

    $scope.linkReportesIngresos = dominio + '/#/reportespf/ingresos';
    $scope.linkReportesEgresos = dominio + '/#/reportespf/egresos';
    $scope.linkReportesCorrespondencia = dominio + '/#/reportespf/correspondencia';
    $scope.linkReportesPaquetes = dominio + '/#/reportespf/paquetes';

    $scope.GetDashboard = function () {
        try {
            var response = { Error: "", Result: "", Code: 200 };

            $http.post(
                dominio + '/Dashboard/GetDashboard', $scope.find
            ).
                success(function (data) {
                    if (data.Code == 200) {
                        $scope.item = data.Result;
                    } else {
                        ShowAlertError(data.Error);
                    }
                }).
                error(function () {
                    ShowAlertError("Ha ocurrido un error al realizar la operacion");
                });
        }
        catch (err) {
            ShowAlertError("Ha ocurrido un error al ejecutar la operacion\n" + err);
        }
    }

    $scope.GetDashboard();

    setInterval(function () {
        $scope.GetDashboard();
    }, ejecucionRefrescoDashboard)
}

DashboardController.$inject = ['$scope', '$http', '$location', '$route', '$routeParams'];