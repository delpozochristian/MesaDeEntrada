var ComprobanteEgresoController = function ($scope, $http, $location, $route, $routeParams) {

    $scope.itemsEgreso = [];

    $scope.ObtenerComprobanteEgreso = function (id) {
        ShowLoading();
        try {

            var getData = { id: id };

            $http.post(
                dominio + '/ComprobanteEgreso/Get', getData
            ).
                success(function (data) {
                    if (data.Code === 200) {
                        HideLoading();

                        $scope.itemsEgreso = data.Result.ListaIngresos;

                    } else {
                        ShowAlertError(data.Error);
                    }
                }).
                error(function () {
                    HideLoading();
                    ShowAlertError("Ha ocurrido un error al realizar la operacion");
                });
        }
        catch (err) {
            HideLoading();
            ShowAlertError("Ha ocurrido un error al ejecutar la operacion\n" + err);
        }
    };

    var id = window.location.search.replace('?id=', '');
    $scope.ObtenerComprobanteEgreso(id);

};

ComprobanteEgresoController.$inject = ['$scope', '$http', '$location', '$route', '$routeParams'];