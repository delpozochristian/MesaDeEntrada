var ProveedoresController = function ($scope, $http, $location, $route, $routeParams) {

    $scope.item = {};
    $scope.items = {}

    $scope.find = {};

    $scope.listaProveedores = [];
    $scope.listaEstadosProveedores = [];

    $scope.alta = false;

    $('.datepicker').datepicker({
        format: 'dd/mm/yyyy',
        autoclose: true,
        language: 'es',
    });

    //============paginacion=============
    $scope.pageno = 1; // initialize page no to 1
    $scope.totalCount = 0;
    $scope.pageSize = 10; //this could be a dynamic value from a drop down


    $scope.getPaginatedList = function (newPageNumber, oldPageNumber) {
        $scope.GetProveedores(newPageNumber);
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

    $scope.rutaProveedorEdit = dominio + '/#/proveedor_edit/';
    $scope.rutaProveedores = dominio + '/#/proveedor/';
    $scope.rutaProveedorNuevo = dominio + '/#/proveedor_add';

    $scope.LimpiarFiltros = function () {
        $scope.find = {};
        
        $('#dtpFechaIngresoDesde').val("").datepicker("update");
        $('#dtpFechaIngresoHasta').val("").datepicker("update");
        $scope.item.estadoproveedor = null;
        $scope.GetProveedores(1);
    }

    $scope.GetProveedores = function (newPageNumber) {
        if (!newPageNumber)
            newPageNumber = 1;
        ShowLoading();
        try {
            var response = { Error: "", Result: "", Code: 200 };

            var fechaDesdeFilter = ($('#dtpFechaIngresoDesde').datepicker('getFormattedDate') != null ? $('#dtpFechaIngresoDesde').datepicker('getFormattedDate') : "");
            var fechaHastaFilter = ($('#dtpFechaIngresoHasta').datepicker('getFormattedDate') != null ? $('#dtpFechaIngresoHasta').datepicker('getFormattedDate') : "");


            var filters = {
                pageIndex: newPageNumber,
                pageSize: $scope.pageSize,
                razonSocial: $scope.find.razonSocial,
                cuit: $scope.find.cuit,
                fechaDesde: fechaDesdeFilter,
                fechaHasta: fechaHastaFilter,
                estado: $scope.item.estadoproveedor,
            };


            //$scope.find.fechaDesde = fechaDesdeFilter;
            //$scope.find.fechaHasta = fechaHastaFilter;

            $http.post(
                dominio + '/Proveedores/GetAll', filters
            ).
                success(function (data) {
                    if (data.Code == 200) {
                        HideLoading();
                        $scope.items = data.Result.ListaProveedores;
                        $scope.totalCount = parseInt(data.Result.totalCount, 10);
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

    $scope.GetProveedor = function (id) {
        ShowLoading();
        try {
            var response = { Error: "", Result: "", Code: 200 };

            var dataGet = { 'id': id };

            $http.post(
                dominio + '/Proveedores/Get', dataGet
            ).
                success(function (data) {
                    if (data.Code == 200) {
                        HideLoading();

                        $scope.proveedor = data.Result.ListaProveedores[0];

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

    $scope.GuardarProveedor = function () {
        ShowLoading();
        try {
            var response = { Error: "", Result: "", Code: 200 };

            var url = dominio + '/Proveedores/Save';

            if (!$scope.alta)
                url = dominio + '/Proveedores/Edit';

            $http.post(
                url, $scope.proveedor
            ).
                success(function (data) {
                    if (data.Code == 200) {
                        HideLoading();

                        var editAdd = "creado";

                        if (!$scope.alta)
                            editAdd = "editado";

                        ShowAlertSuccess("Proveedor " + editAdd + " con éxito!");

                        window.location.replace(urlLocal + dominio + "/#/proveedor");

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

    $scope.LimpiarComboHabilitado = function () {
        $scope.item.estadoproveedor = null;
    }

    $scope.GetEstadosProveedor = function () {
        try {
            var response = { Error: "", Result: "", Code: 200 };


            $http.post(
                dominio + '/Combos/GetEstadosProveedor',
            ).
                success(function (data) {
                    if (data.Code == 200) {
                        $scope.listaEstadosProveedores = data.Result;
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


    $scope.DeleteProveedor = function (id) {
        ShowLoading();
        try {
            var response = { Error: "", Result: "", Code: 200 };

            var dataItem = { 'id': id };

            $http.post(
                dominio + '/Proveedores/Delete', dataItem
            ).
                success(function (data) {
                    console.log('response', data);
                    if (data.Code == 200) {
                        HideLoading();

                        ShowAlertSuccess("Deshabilitado correctamente");
                        $scope.GetProveedores();
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

    $scope.HabilitarProveedor = function (id) {
        ShowLoading();
        try {
            var response = { Error: "", Result: "", Code: 200 };

            var dataItem = { 'id': id };

            $http.post(
                dominio + '/Proveedores/Enable', dataItem
            ).
                success(function (data) {
                    if (data.Code == 200) {
                        HideLoading();

                        ShowAlertSuccess("Habilitado correctamente");
                        $scope.GetProveedores();
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

    $scope.GetEstadosProveedor();

    if ($location.path() == '/proveedor') {
        $scope.GetProveedores(1);
    } else if ($location.path() == '/proveedor_add') {
        $scope.alta = true;
    }
    else {
        $scope.GetProveedor($routeParams.id);
        $scope.alta = false;
    }

}

ProveedoresController.$inject = ['$scope', '$http', '$location', '$route', '$routeParams'];