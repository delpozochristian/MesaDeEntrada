var ReportesController = function ($scope, $http, $location, $route, $routeParams) {
    //============paginacion=============
    $scope.pageno = 1; // initialize page no to 1
    $scope.totalCount = 0;
    $scope.pageSize = 10; //this could be a dynamic value from a drop down


    $scope.getPaginatedList = function (newPageNumber, oldPageNumber) {
        switch ($location.path()) {
            case '/reportespf/ingresos':
                $scope.GetIngresosPrefiltrado('ingresos', newPageNumber);
                break;

            case '/reportespf/egresos':
                $scope.GetIngresosPrefiltrado('egresos', newPageNumber);
                break;

            case '/reportespf/correspondencia':
                $scope.GetIngresosPrefiltrado('correspondencia', newPageNumber);
                break;

            case '/reportespf/paquetes':
                $scope.GetIngresosPrefiltrado('paquetes', newPageNumber);
                break;

            case '/reportes':
                $scope.GetIngresosFilter("", newPageNumber);
                break;
        }
    }


    //This method is calling from pagination number
    $scope.pageChanged = function () {
        $scope.getPaginatedList();
    };

    //This method is calling from dropDown
    $scope.changePageSize = function () {
        $scope.pageno = 1;
        $scope.getPaginatedList($scope.pageno);
    };

    //============paginacion=============

    $scope.items = {}
    $scope.unItem = {}

    $scope.item.canalizacion = null;
    $scope.item.estado = null;
    $scope.item.producto = null;

    $scope.itemDetalle = {};

    $scope.listProductos = [{ Id: '1', Nombre: 'Sobres' }, { Id: '2', Nombre: 'Paquetes' }, { Id: '3', Nombre: 'Bolsines' }]
    $scope.listProveedores = []
    $scope.listDestinatarios = []
    $scope.listSector = []
    $scope.listCanalizacion = []
    $scope.listEstados = []

    $scope.totalDisplayed = 40;

    $scope.loadMore = function () {
        $scope.totalDisplayed += 40;
    };

    $scope.rutaDescargaEtiqueta = dominio + '/Etiqueta?idIngreso=';
    $scope.rutaDescargaArchivo = dominio + '/Download?idIngreso=';
    $scope.rutaDescargaComprobante = dominio + '/ComprobanteEgreso?id=';

    $('.datepicker').datepicker({
        format: 'dd/mm/yyyy',
        autoclose: true,
        language: 'es'
    });

    $scope.GetIngresoDetalle = function (idDetalle) {
        //ShowLoading();
        try {
            var response = { Error: "", Result: "", Code: 200 };
            var dataItem = { 'idDetalle': idDetalle };
            $http.post(
                dominio + '/Reportes/GetIngresoDetalle', dataItem
            ).
                success(function (data) {
                    if (data.Code == 200) {
                        //HideLoading();
                        $scope.unItem = data.Result.ListaIngresos[0];
                        console.log($scope.unItem);
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

    $scope.CancelarPedido = function (item) {
        ShowLoading();
        try {
            var response = { Error: "", Result: "", Code: 200 };

            var dataItem = { 'codeBar': item.Barcode };

            $http.post(
                dominio + '/Reportes/Cancel', dataItem
            ).
                success(function (data) {
                    if (data.Code == 200) {
                        HideLoading();
                        ShowAlertSuccess("Cancelado correctamente");
                        $scope.GetIngresos();
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


    $scope.GetIngresosPrefiltrado = function (prefiltro, newPageNumber, oldPageNumber) {
        ShowLoading();
        try {
            $scope.GetIngresosFilter(prefiltro, newPageNumber);
        }
        catch (err) {
            ShowAlertError("Ha ocurrido un error al ejecutar la operacion\n" + err);
        }
    }

    $scope.GetCanalizaciones = function (query) {

        try {
            var response = { Error: "", Result: "", Code: 200 };

            $scope.find = { 'search': query };

            $http.post(
                dominio + '/Combos/GetCanalizacion', $scope.find
            ).
                success(function (data) {
                    if (data.Code == 200) {

                        $scope.listCanalizacion = data.Result.ListaCanalizaciones;

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

    $scope.GetReporteById = function (id = 0) {

        try {
            var response = { Error: "", Result: "", Code: 200 };

            $http.post(
                dominio + '/Reportes/GetReporteById?id=' + id, id
            ).
                success(function (data) {
                    if (data.Code == 200) {
                        $scope.unItem = data.Result.ListaIngresos[0];
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

    $scope.GetIngresosFilter = function (prefiltro = "", newPageNumber, oldPageNumber) {
        ShowLoading();
        try {
            var response = { Error: "", Result: "", Code: 200 };

            var tipoProductoFilter = ($scope.item.producto != null ? $scope.item.producto.Id : 0);
            var sectorFilter = ($scope.item.sector != null ? $scope.item.sector.Id : "");
            var proveedorFilter = ($scope.item.proveedor != null ? $scope.item.proveedor.Id : 0);
            var destinatarioFilter = ($scope.item.destinatario != null ? $scope.item.destinatario.Id : "");
            var canalizacionFilter = ($scope.item.canalizacion != null ? $scope.item.canalizacion.Id : "");
            var estadoFilter = ($scope.item.estado != null ? $scope.item.estado.Id : "");

            //RF
            var fechaDesdeFilter = ($('#dtpFechaIngresoDesde').datepicker('getFormattedDate') != null ? $('#dtpFechaIngresoDesde').datepicker('getFormattedDate') : "");
            var fechaHastaFilter = ($('#dtpFechaIngresoHasta').datepicker('getFormattedDate') != null ? $('#dtpFechaIngresoHasta').datepicker('getFormattedDate') : "");

            if (!newPageNumber)
                newPageNumber = 1;

            var filters = {
                pageIndex: newPageNumber,
                pageSize: $scope.pageSize,
                nroSeguimiento: $scope.item.nroSeguimiento,
                tipoProducto: tipoProductoFilter,
                sector: sectorFilter,
                proveedor: proveedorFilter,
                destinatario: destinatarioFilter,
                canalizacion: canalizacionFilter,
                estado: estadoFilter,
                fechaDesde: fechaDesdeFilter,
                fechaHasta: fechaHastaFilter,
                prefiltrado: prefiltro,

            };

            $http.post(
                dominio + '/Reportes/GetIngresosFilterPaginado', filters
            ).
                success(function (data) {
                    if (data.Code == 200) {
                        HideLoading();
                        $scope.items = data.Result.ListaIngresos;
                        $scope.totalCount = parseInt(data.Result.totalCount, 10);

                        if (data.Result.Prefiltros && data.Result.Prefiltros.Seleccionado) {
                            switch (data.Result.Prefiltros.Seleccionado) {
                                case "ingresos":
                                    $scope.item.estado = data.Result.Prefiltros.EstadoPrefiltradoIngresos;
                                    break;

                                case "egresos":
                                    $scope.item.estado = data.Result.Prefiltros.EstadoPrefiltradoEgresos;
                                    break;

                                case "correspondencia":
                                    $scope.item.canalizacion = data.Result.Prefiltros.CanalizacionesPrefiltradoCorrespondencia;
                                    break;

                                case "paquetes":
                                    $scope.item.producto = data.Result.Prefiltros.TipoProductoPrefiltradoPaquetes;
                                    break;
                            }
                        }

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
    $scope.DownloadExcel = function (prefiltro = "", newPageNumber, oldPageNumber) {
        ShowLoading();
        try {
            var response = { Error: "", Result: "", Code: 200 };

            var tipoProductoFilter = ($scope.item.producto != null ? $scope.item.producto.Id : 0);
            var sectorFilter = ($scope.item.sector != null ? $scope.item.sector.Id : "");
            var proveedorFilter = ($scope.item.proveedor != null ? $scope.item.proveedor.Id : 0);
            var destinatarioFilter = ($scope.item.destinatario != null ? $scope.item.destinatario.Id : "");
            var canalizacionFilter = ($scope.item.canalizacion != null ? $scope.item.canalizacion.Id : "");
            var estadoFilter = ($scope.item.estado != null ? $scope.item.estado.Id : "");

            //RF
            var fechaDesdeFilter = ($('#dtpFechaIngresoDesde').datepicker('getFormattedDate') != null ? $('#dtpFechaIngresoDesde').datepicker('getFormattedDate') : "");
            var fechaHastaFilter = ($('#dtpFechaIngresoHasta').datepicker('getFormattedDate') != null ? $('#dtpFechaIngresoHasta').datepicker('getFormattedDate') : "");

            if (fechaDesdeFilter === '' || fechaHastaFilter === '') {
                ShowAlertError("Se debe especificar al menos las fechas desde y hasta");
                return;
            }

            var filters = {
                nroSeguimiento: $scope.item.nroSeguimiento,
                tipoProducto: tipoProductoFilter,
                sector: sectorFilter,
                proveedor: proveedorFilter,
                destinatario: destinatarioFilter,
                canalizacion: canalizacionFilter,
                estado: estadoFilter,
                fechaDesde: fechaDesdeFilter,
                fechaHasta: fechaHastaFilter,
                prefiltrado: prefiltro,

            };

            $http.post(
                dominio + '/Reportes/GetIngresosFilterExcel', filters, { responseType: "arraybuffer" }
            )
                .success(function (data, status, headers, config) {

                    HideLoading();
                    var type = headers('Content-Type');
                    var disposition = headers('Content-Disposition');
                    if (disposition) {
                        var match = disposition.match(/.*filename=\"?([^;\"]+)\"?.*/);
                        if (match[1])
                            defaultFileName = match[1];
                    }
                    defaultFileName = defaultFileName.replace(/[<>:"\/\\|?*]+/g, '_');
                    var blob = new Blob([data], { type: type });
                    saveAs(blob, defaultFileName);
                })
                .error(function (error) {
                    ShowAlertError("Ha ocurrido un error al realizar la operacion");
                });
        }
        catch (err) {
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

    $scope.GetSectores = function (query) {

        try {
            var response = { Error: "", Result: "", Code: 200 };

            $scope.find = { 'search': query };

            $http.post(
                dominio + '/Combos/GetSectores', $scope.find
            ).
                success(function (data) {
                    if (data.Code == 200) {

                        $scope.listSector = data.Result.ListaSectores;

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

    $scope.GetEstados = function (query) {

        try {
            var response = { Error: "", Result: "", Code: 200 };

            $scope.find = { 'search': query };

            $http.post(
                dominio + '/Combos/GetEstados', $scope.find
            ).
                success(function (data) {
                    if (data.Code == 200) {

                        $scope.listEstados = data.Result.ListaEstados;

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

    $scope.LimpiarFiltros = function () {
        $scope.LimpiarCombos();
    }


    $scope.LimpiarCombos = function () {
        $scope.GetProveedores();
        $scope.GetDestinatarios();
        $scope.GetSectores();
        $scope.GetCanalizaciones();

        $scope.item.canalizacion = null;
        $scope.item.destinatario = null;
        $scope.item.estado = null;
        $scope.item.sector = null;
        $scope.item.proveedor = null;
        $scope.item.producto = null;
        $scope.item.nroSeguimiento = null;
        $scope.item.fechaDesde = null;
        $scope.item.fechaHasta = null;
        $('#dtpFechaIngresoDesde').val("").datepicker("update");
        $('#dtpFechaIngresoHasta').val("").datepicker("update");;

        $scope.getPaginatedList(1);
    }

    $scope.LimpiarComboDestinatario = function () {
        $scope.item.destinatario = null;
    }

    $scope.LimpiarComboProducto = function () {
        $scope.item.producto = null;
    }

    $scope.LimpiarComboProveedor = function () {
        $scope.item.proveedor = null;
    }

    $scope.LimpiarComboEstado = function () {
        $scope.item.estado = null;
    }

    $scope.LimpiarComboSector = function () {
        $scope.item.sector = null;
    }

    $scope.LimpiarComboCanalizacion = function () {
        $scope.item.canalizacion = null;
    }

    $scope.VerDetalle = function (item) {
        console.log('ver detalle de:', item);
        $scope.itemSeleccionado = item;
        var element = angular.element('#ModalDetallePedido');
        element.modal('show');
    }

    $scope.GetProveedores();
    $scope.GetDestinatarios();
    $scope.GetSectores();
    $scope.GetCanalizaciones();
    $scope.GetEstados();

    switch ($location.path()) {
        case '/reportespf/ingresos':
            $scope.GetIngresosPrefiltrado('ingresos', 1);
            break;

        case '/reportespf/egresos':
            $scope.GetIngresosPrefiltrado('egresos', 1);
            break;

        case '/reportespf/correspondencia':
            $scope.GetIngresosPrefiltrado('correspondencia', 1);
            break;

        case '/reportespf/paquetes':
            $scope.GetIngresosPrefiltrado('paquetes', 1);
            break;

        case '/reportes':
            $scope.getPaginatedList(1);
            break;
    }

    $scope.showFiltros = false;

    $("#BotonFiltros").click(function () {

        if (!$scope.showFiltros) {
            $("#FiltrosID").show();
            $scope.showFiltros = true;
        }
        else {
            $("#FiltrosID").hide();
            $scope.showFiltros = false;
        }

    });


}

ReportesController.$inject = ['$scope', '$http', '$location', '$route', '$routeParams'];