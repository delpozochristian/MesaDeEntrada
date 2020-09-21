var EgresosController = function ($scope, $http, $location, $route, $routeParams) {

    $scope.item = {}

    $scope.find = {}

    $scope.itemsEncontrados = []
    $scope.itemsEntregar = []

    $scope.listProveedores = []
    $scope.listDestinatarios = []

    $scope.rutaDescargaComprobante = dominio + '/ComprobanteEgreso?id=';

    $scope.finalizado = false;
    $scope.link = '';
    $scope.item.idEntrega = 0;

    $('.datepicker').datepicker({
        format: 'dd/mm/yyyy'
    });

    $scope.GetIngreso = function () {
        ShowLoading();
        $scope.checkboxes = []
        var proveedorFilter = ($scope.item.proveedor != null ? $scope.item.proveedor.Id : 0);
        var destinatarioFilter = ($scope.item.destinatario != null ? $scope.item.destinatario.Id : "");
        var fechaDesdeFilter = ($('#dtpFechaIngresoDesde').datepicker('getFormattedDate') != null ? $('#dtpFechaIngresoDesde').datepicker('getFormattedDate') : "");
        var fechaHastaFilter = ($('#dtpFechaIngresoHasta').datepicker('getFormattedDate') != null ? $('#dtpFechaIngresoHasta').datepicker('getFormattedDate') : "");

        try {
            var getIngresoData = {
                nroSeguimiento: $scope.find.nroSeguimiento,
                proveedor: proveedorFilter,
                destinatario: destinatarioFilter,
                fechaDesde: fechaDesdeFilter,
                fechaHasta: fechaHastaFilter,
                autorizado: $scope.find.autorizado
            };

            $http.post(
                dominio + '/Egresos/GetIngresos', getIngresoData
            ).
                success(function (data) {
                    if (data.Code == 200) {
                        HideLoading();
                        $scope.itemsEncontrados = data.Result.ListaIngresos;

                        var nuevaLista = [];

                        for (var i = 0; i < $scope.itemsEncontrados.length; i++) {

                            var agregar = true;

                            for (var n = 0; n < $scope.itemsEntregar.length; n++) {

                                if ($scope.itemsEncontrados[i].NroSeguimiento == $scope.itemsEntregar[n].NroSeguimiento) {
                                    agregar = false;
                                }

                            }

                            if (agregar)
                                nuevaLista.push($scope.itemsEncontrados[i]);
                        }

                        $scope.itemsEncontrados = nuevaLista;

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
    }

    $scope.GetProveedores = function (query) {

        try {
            var response = { Error: "", Result: "", Code: 200 };

            $scope.find = { 'search': query };

            $http.post(
                dominio + '/Combos/GetProveedoresReportes', $scope.find
            ).
                success(function (data) {
                    if (data.Code == 200) {

                        $scope.listProveedores = data.Result.ListaProveedores;
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

    $scope.GetDestinatarios = function (query) {

        try {
            var response = { Error: "", Result: "", Code: 200 };

            $scope.find = { 'search': query };

            $http.post(
                dominio + '/Combos/GetDestinatarios', $scope.find
            ).
                success(function (data) {
                    if (data.Code == 200) {

                        $scope.listDestinatarios = data.Result.ListaDestinatarios;

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

    $scope.AgregarIngreso = function () {
        try {
            //RF

            $('.checkboxes').each(function () {
                if ($(this).is(':checked') == true) {
                    var idCheckbox = $(this).attr('id');
                    var nroSeguimiento = idCheckbox.split('_')[1];

                    var getIngresoData = { nroSeguimiento: nroSeguimiento };

                    var agregar = true;

                    //RF
                    for (var ix = 0; ix < $scope.itemsEntregar.length; ix++) {
                        if ($scope.itemsEntregar[ix].NroSeguimiento == nroSeguimiento)
                            agregar = false;
                    }

                    //RF
                    if (agregar) {
                        $http.post(
                            dominio + '/Egresos/GetIngresoUnico', getIngresoData
                        ).
                            success(function (data) {
                                if (data.Code == 200) {

                                    $scope.itemsEntregar.push(data.Result.ListaIngresos[0]);

                                    var nuevaLista = [];

                                    for (var i = 0; i < $scope.itemsEncontrados.length; i++) {
                                        if ($scope.itemsEncontrados[i].NroSeguimiento != nroSeguimiento) {
                                            nuevaLista.push($scope.itemsEncontrados[i]);
                                        }
                                    }

                                    $scope.itemsEncontrados = nuevaLista;

                                } else {
                                    HideLoading();
                                    ShowAlertError(data.Error);
                                }
                            }).
                            error(function () {
                                HideLoading();
                                ShowAlertError("Ha ocurrido un error al realizar la operacion");
                            });
                    }
                }
            });
        }
        catch (err) {
            HideLoading();
            ShowAlertError("Ha ocurrido un error al ejecutar la operacion\n" + err);
        }
    }

    $scope.Entregar = function () {
        ShowLoading();
        try {

            var entregarData = $scope.itemsEntregar;

            $http.post(
                dominio + '/Egresos/EntregarPedidos', entregarData
            ).
                success(function (data) {
                    if (data.Code == 200) {
                        HideLoading();

                        $scope.item.idEntrega = data.Result.IdEgreso;

                        ShowAlertSuccess("Egreso realizado con éxito!");

                        $scope.link = urlLocal + $scope.rutaDescargaComprobante + $scope.item.idEntrega;
                        $scope.finalizado = true;

                        var title = 'Comprobante';
                        var w = 900;
                        var h = 500;

                        var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : window.screenX;
                        var dualScreenTop = window.screenTop != undefined ? window.screenTop : window.screenY;

                        var width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
                        var height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

                        var left = ((width / 2) - (w / 2)) + dualScreenLeft;
                        var top = ((height / 2) - (h / 2)) + dualScreenTop;
                        var newWindow = window.open($scope.link, title, 'scrollbars=yes, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);

                        // Puts focus on the newWindow
                        if (window.focus) {
                            newWindow.focus();
                        }
                        
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
    }

    $scope.MarcarTodos = function () {
        $('.checkboxes').each(function () {
            $(this).prop('checked', true);
        });
    }

    $scope.LimpiarFiltros = function () {
        $scope.find.fechaDesde = '';
        $scope.find.fechaHasta = '';
        $scope.item.destinatario = null;
        $scope.item.proveedor = null;
        $scope.find.nroSeguimiento = '';
        $scope.find.autorizado = '';
        $scope.GetIngreso();

        $scope.GetProveedores();
        $scope.GetDestinatarios();
    }

    $scope.Limpiar = function () {
        $scope.itemsEncontrados = [];
        $scope.itemsEntregar = [];
        $scope.link = '';
        $scope.finalizado = false;
        $scope.GetIngreso();

        $scope.GetProveedores();
        $scope.GetDestinatarios();
    }

    $scope.GetIngreso();
    $scope.GetProveedores();
    $scope.GetDestinatarios();
}

EgresosController.$inject = ['$scope', '$http', '$location', '$route', '$routeParams'];