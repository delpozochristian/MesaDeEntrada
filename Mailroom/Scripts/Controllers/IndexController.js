var IndexController = function ($scope, $http, $location, $route, $routeParams) {

    $scope.item = {}

    $scope.recepcionActive = '';
    $scope.etiquetaActive = '';
    $scope.remitoActive = '';
    $scope.impresionActive = '';

    $scope.imagenOCASA = dominio + "/Content/img/ocasa.png";

    $scope.recepcionLink = dominio + "/#/recepcion";
    $scope.egresosLink = dominio + "/#/egresos";
    $scope.reportesLink = dominio + "/#/reportes";
    $scope.proveedorLink = dominio + "/#/proveedor";
    $scope.dashboardLink = dominio + "/#/dashboard";

    switch ($location.path()) {
        case '/recepcion':
            $scope.recepcionActive = 'active';
            break;

        case '/etiqueta':
            $scope.etiquetaActive = 'active';
            break;

        case '/remito':
            $scope.remitoActive = 'active';
            break;

        case '/impresion':
            $scope.impresionActive = 'active';
            break;
    }

    $scope.GetLogin = function () {
        try {
            $http.post(
                dominio + '/Login/GetUsername', $scope.item
            ).
                success(function (data) {
                    $scope.nombreUsuario = data.username;
                }).
                error(function () {
                    ShowAlertError("Ha ocurrido un error al realizar la operacion");
                });
        }
        catch (err) {
            ShowAlertError("Ha ocurrido un error al ejecutar la operacion\n" + err);
        }
    }

    $scope.CloseSession = function () {
        swal({
            title: "Está seguro?",
            text: "Desea cerrar la sesión?",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: '#DD6B55',
            confirmButtonText: 'Si',
            cancelButtonText: "No",
            closeOnConfirm: true,
            closeOnCancel: true
        },
            function (isConfirm) {
                if (isConfirm) {
                    window.location.replace(urlLocal + dominio + "/Security");
                }
            });
    }

    $scope.GetLogin();

    $scope.OpenNav = function () {
        document.getElementById("mySidenav").style.width = "250px";
    }

    $scope.CloseNav = function () {
        document.getElementById("mySidenav").style.width = "0";
    }

    $scope.OpenCloseNav = function () {
        if (document.getElementById("mySidenav").style.width == "250px")
            document.getElementById("mySidenav").style.width = "0";
        else
            document.getElementById("mySidenav").style.width = "250px";
    }

}

IndexController.$inject = ['$scope', '$http', '$location', '$route', '$routeParams'];