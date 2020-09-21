var EtiquetaController = function ($scope, $http, $location, $route, $routeParams) {

    $scope.item = {}

    $scope.item.barcode = "";
    $scope.item.destinatario = "";
    $scope.item.direccion = "";

    $scope.GetIngreso = function (id) {
        ShowLoading();
        try {
            var idIngresoJson = { idIngreso: id };

            $http.post(
                dominio + '/Etiqueta/Obtener', idIngresoJson
            ).
                success(function (data) {
                    if (data.Code === 200) {
                        HideLoading();
                        $http.get(
                            dominio + '/Etiqueta/ObtenerBarcode?barcode=' + data.Result.ListaIngresos[0].Barcode
                        ).
                            success(function (b64barcode) {
                                $scope.item.barcode = b64barcode;
                            }).
                            error(function () {
                                ShowAlertError("Ha ocurrido un error al realizar la operacion");
                            });

                        $scope.item.direccion = data.Result.ListaIngresos[0].Direccion;
                        $scope.item.destinatario = data.Result.ListaIngresos[0].Destinatario;
                        
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
    };

    var id = window.location.search.replace('?idIngreso=', '');
    $scope.GetIngreso(id);
};

EtiquetaController.$inject = ['$scope', '$http', '$location', '$route', '$routeParams'];