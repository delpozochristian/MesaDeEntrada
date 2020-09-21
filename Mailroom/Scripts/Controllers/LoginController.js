var LoginController = function ($scope, $http, $location, $route, $routeParams) {

    $scope.item = {}

    $scope.imagenOCASA = dominio + "/Content/img/ocasa.png";

    $scope.Login = function () {
        ShowLoading();
        try {
            var response = { Error: "", Result: "", Code: 200 };

            $http.post(
                dominio + '/Login/Do', $scope.item
            ).
                success(function (data) {
                    if (data.Code == 200) {
                        HideLoading();
                        if (data.Result.Estado == true)
                            window.location.replace(urlLocal + dominio + "/#/dashboard");
                            else
                                ShowAlertError(data.Result.Response);
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

    $scope.pressEnter = function (keyEvent) {
        if (keyEvent.which === 13)
            $scope.Login();
    }

}

LoginController.$inject = ['$scope', '$http', '$location', '$route', '$routeParams'];