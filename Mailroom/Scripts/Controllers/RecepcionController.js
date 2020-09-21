var RecepcionController = function ($scope, $http, $location, $route, $routeParams) {

    $scope.item = {}
    $scope.test = 0;



    $scope.listProveedores = []
    $scope.listDestinatarios = []
    $scope.listSector = []
    $scope.listDireccion = []
    $scope.listCanalizacion = []

    $scope.item.nroOrdenCompra = "";
    $scope.item.nroRemito = "";
    $scope.item.tipoProducto = 1;
    $scope.item.escaneoPieza = false;
    $scope.item.IdArchivo = 0;
    $scope.item.FileName = "";
    $scope.item.observacion = "";
    $scope.fileUploaded = false;

    $scope.item.IdRemitente = "";
    $scope.item.IdDireccionRemitente = "";
    $scope.item.IdSectorRemitente = "";
    $scope.item.IdSucursalRemitente = "";
    $scope.item.imprimirEtiqueta = false;

    $scope.rutaDescargaEtiqueta = dominio + '/Etiqueta?idIngreso=';

    $scope.botonSubir = false;

    $scope.fileName = "Seleccionar archivo";

    $scope.GetProveedores = function (query) {

        try {
            var response = { Error: "", Result: "", Code: 200 };

            $scope.find = { 'search': query };

            $http.post(
                dominio + '/Combos/GetProveedores', $scope.find
            ).
                success(function (data) {
                    if (data.Code == 200) {

                        $scope.listProveedores = data.Result.ListaProveedores;
                    } else {
                        ShowAlertError(data.Error);
                    }
                }).
                error(function () {
                    ShowAlertError("Ha ocurrido un error al realizar la operación");
                });
        }
        catch (err) {
            ShowAlertError("Ha ocurrido un error al ejecutar la operación\n" + err);
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

    $scope.GetDestinatarios = function (query) {
        if ($scope.item.sector != null)
            $scope.GetDestinatariosBySector(query, $scope.item.sector.Id);
        else if ($scope.item.direccion != null)
            $scope.GetDestinatariosByDireccion(query, $scope.item.direccion.Id);
        else
            $scope.GetDestinatariosByFilter(query);
    }

    $scope.GetDestinatariosByFilter = function (query) {

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

    $scope.GetDestinatariosBySector = function (query, idSector) {

        try {
            var response = { Error: "", Result: "", Code: 200 };

            $scope.find = { 'search': query, 'idSector': idSector };

            $http.post(
                dominio + '/Combos/GetDestinatariosBySector', $scope.find
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

    $scope.GetDestinatariosByDireccion = function (query, idDireccion) {

        try {
            var response = { Error: "", Result: "", Code: 200 };

            $scope.find = { 'search': query, 'idDireccion': idDireccion };

            $http.post(
                dominio + '/Combos/GetDestinatariosByDireccion', $scope.find
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
        if ($scope.item.direccion != null)
            $scope.GetSectoresByDireccion(query, $scope.item.direccion.Id);
        else
            $scope.GetSectoresByFilter(query);
    }

    $scope.GetSectoresByFilter = function (query) {

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

    $scope.GetSectoresByDireccion = function (query, idDireccion) {

        try {
            var response = { Error: "", Result: "", Code: 200 };

            $scope.find = { 'search': query, 'idDireccion': idDireccion };

            $http.post(
                dominio + '/Combos/GetSectoresByDireccion', $scope.find
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

    $scope.GetDirecciones = function (query) {

        try {
            var response = { Error: "", Result: "", Code: 200 };

            $scope.find = { 'search': query };

            $http.post(
                dominio + '/Combos/GetDirecciones', $scope.find
            ).
                success(function (data) {
                    if (data.Code == 200) {

                        $scope.listDireccion = data.Result.ListaDirecciones;

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

    $scope.GetDireccionesByFilter = function (query) {
        if ($scope.item.sector != null)
            $scope.GetDireccionesBySector(query, $scope.item.sector.Id);
        else
            $scope.GetDireccionesByFilter(query);
    }

    $scope.GetDireccionesBySector = function (query, idSector) {

        try {
            var response = { Error: "", Result: "", Code: 200 };

            $scope.find = { 'search': query, 'idSector': idSector };

            $http.post(
                dominio + '/Combos/GetDireccionesBySector', $scope.find
            ).
                success(function (data) {
                    if (data.Code == 200) {

                        $scope.listDireccion = data.Result.ListaDirecciones;

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

    $scope.GetDestinatario = function (id) {

        try {
            var response = { Error: "", Result: "", Code: 200 };

            $scope.find = { 'id': id };

            $http.post(
                dominio + '/Combos/GetDestinatario', $scope.find
            ).
                success(function (data) {
                    if (data.Code == 200) {

                        $scope.item.destinatario = data.Result;

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

    $scope.GetSector = function (id) {

        try {
            var response = { Error: "", Result: "", Code: 200 };

            $scope.find = { 'id': id };

            $http.post(
                dominio + '/Combos/GetSector', $scope.find
            ).
                success(function (data) {
                    if (data.Code == 200) {

                        $scope.item.sector = data.Result;

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

    $scope.GetDireccion = function (id) {

        try {
            var response = { Error: "", Result: "", Code: 200 };

            $scope.find = { 'id': id };

            $http.post(
                dominio + '/Combos/GetDireccion', $scope.find
            ).
                success(function (data) {
                    if (data.Code == 200) {

                        $scope.item.direccion = data.Result;

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

    $scope.ReloadCombosByDestinatario = function () {
        $scope.GetSector($scope.item.destinatario.IdSector);
        $scope.GetDireccion($scope.item.destinatario.IdBandeja);
    }

    $scope.ReloadCombosBySector = function () {
        $scope.GetDestinatarios('');
        $scope.GetDirecciones('');
    }

    $scope.ReloadCombosByDireccion = function () {
        $scope.GetDestinatarios('');
        $scope.GetSectores('');
    }

    $scope.Guardar = function () {
        ShowLoading();
        try {

            if ($scope.ValidarCampos()) {
                var response = { Error: "", Result: "", Code: 200 };

                $http.post(
                    dominio + '/Recepcion/Guardar', $scope.item
                ).
                    success(function (data) {
                        
                        if (data.Code == 200) {
                            HideLoading();

                            var nroSeguimiento = data.Result.NroSeguimiento;
                            var mensaje = "";

                            if ($scope.item.imprimirEtiqueta) {
                                $scope.link = urlLocal + dominio + '/Etiqueta?idIngreso=' + data.Result.IdIngreso;

                                var title = 'Etiqueta';
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
                            }

                            if (nroSeguimiento != null) {
                                mensaje = " con Nro de Seguimiento " + nroSeguimiento
                            }

                            ShowAlertSuccessWithRedirect("Se creó con éxito el pedido" + mensaje + ".", urlLocal + dominio + "/#/dashboard");

                            

                        } else {
                            ShowAlertError(data.Error);
                        }
                    }).
                    error(function () {
                        ShowAlertError("Ha ocurrido un error al realizar la operacion");
                    });
            }


        }
        catch (err) {
            ShowAlertError("Ha ocurrido un error al ejecutar la operacion\n" + err);
        }
    }

    $scope.SetFileName = function () {
        var file = $('#fileUpload')[0].files[0].name;
        $scope.fileName = file;
    }

    $('#fileUpload').change(function () {
        $scope.SetFileName();
        $scope.botonSubir = true;
    });

    $scope.ValidarCampos = function () {

        var validaciones = "";

        if ($scope.item.proveedores == null)
            validaciones = "Proveedor - ";
        /*
        if ($scope.item.nroSeguimiento == "")
            validaciones += "Nro de Seguimiento - ";
        */
        if ($scope.item.destinatario == null)
            validaciones += "Destinatario - ";

        if ($scope.item.direccion == null)
            validaciones += "Dirección - ";

        if ($scope.item.sector == null)
            validaciones += "Sector - ";

        if ($scope.item.canalizacion == null)
            validaciones += "Canalización - ";

        if (validaciones.length > 0) {
            ShowAlertError("Los siguientes campos son obligatorios: " + validaciones);
            return false;
        }


        return true;
    }

    $scope.Consultar = function () {

        try {
            var response = { Error: "", Result: "", Code: 200 };

            if ($scope.item.proveedores != null && $scope.item.proveedores.RazonSocial == "OCASA" && ($scope.item.nroSeguimiento != '' && $scope.item.nroSeguimiento != null)) {
                ShowLoading();
                $http.post(
                    dominio + '/Recepcion/Consultar', $scope.item
                ).
                    success(function (data) {
                        if (data.Code == 200) {
                            if (data.Result.Pedidos != null) {
                                HideLoading();

                                $scope.findIdDireccion = data.Result.Pedidos[0].Destinatario.Bandeja;
                                $scope.findIdSector = data.Result.Pedidos[0].Destinatario.Sector;

                                $scope.item.observacion = data.Result.Pedidos[0].Observacion;

                                $scope.item.IdRemitente = data.Result.Pedidos[0].Remitente.IDUsuario;
                                $scope.item.IdDireccionRemitente = data.Result.Pedidos[0].Remitente.Bandeja;
                                $scope.item.IdSectorRemitente = data.Result.Pedidos[0].Remitente.Sector;
                                $scope.item.IdSucursalRemitente = data.Result.Pedidos[0].Remitente.Sucursal;

                                //set destinatario
                                $scope.GetDestinatario(data.Result.Pedidos[0].Destinatario.IDUsuario);

                                //set sectores
                                $scope.GetSector(data.Result.Pedidos[0].Destinatario.Sector);

                                //set direccion
                                $scope.GetDireccion(data.Result.Pedidos[0].Destinatario.Bandeja);
                            }
                            else
                            {
                                HideLoading();
                            }
                        } else {
                            ShowAlertError(data.Error);
                        }
                    }).
                    error(function () {
                        ShowAlertError("Ha ocurrido un error al realizar la operacion");
                    });
            }

        }
        catch (err) {
            ShowAlertError("Ha ocurrido un error al ejecutar la operacion\n" + err);
        }
    }

    $scope.LimpiarComboDestinatario = function () {
        $scope.item.destinatario = null;
    }

    $scope.LimpiarComboDireccion = function () {
        $scope.item.direccion = null;
    }

    $scope.LimpiarComboSector = function () {
        $scope.item.sector = null;
    }

    $scope.LimpiarComboCanalizacion = function () {
        $scope.item.canalizacion = null;
    }

    $scope.Limpiar = function () {
        try {

            $(".btn").removeClass('active');
            $(".btn-group>.btn").first().button('toggle');
            $scope.item.tipoProducto = 1;
            $scope.item.proveedores = null;
            $scope.item.nroRemito = "";
            $scope.item.nroOrdenCompra = "";
            $scope.item.nroSeguimiento = "";
            $scope.item.canalizacion = null;
            $scope.item.destinatario = null;
            $scope.item.sector = null;
            $scope.item.direccion = null;
            $scope.item.escaneoPieza = false;
            $scope.item.imprimirEtiqueta = false;
            $scope.autoSelect = false;

            $scope.item.IdArchivo = 0;
            $scope.item.FileName = "";
            $scope.fileUploaded = false;

            $scope.botonSubir = false;
            $scope.findIdSector = '';
            $scope.findIdDireccion = '';

        }
        catch (err) {
            ShowAlertError("Ha ocurrido un error al ejecutar la operacion\n" + err);
        }
    }

    $scope.uploadFile = function () {
        ShowLoading();

        var fileInput = document.getElementById("fileUpload");

        if (fileInput.files.length === 0) return;

        var file = fileInput.files[0];

        var payload = new FormData();
        payload.append("file", file);

        $http.post(dominio + '/Recepcion/SubirArchivo', payload, {
            transformRequest: angular.identity,
            headers: { "Content-Type": undefined }
        }).then(function (data) {
            HideLoading();
            $scope.fileUploaded = true;
            $scope.item.IdArchivo = data.data.Result.IdFile;
            $scope.item.FileName = data.data.Result.NameFile;
            $scope.fileName = "Seleccionar archivo";
            $scope.botonSubir = false;
        }, function (error) {
            HideLoading();
        });
    }

    $scope.SelectTipoProducto = function (value) {
        try {
            $scope.item.tipoProducto = value;
        }
        catch (err) {
            ShowAlertError("Ha ocurrido un error al ejecutar la operacion\n" + err);
        }
    }

    ShowLoading();
    $scope.GetProveedores();
    $scope.GetDestinatarios();
    $scope.GetSectores();
    $scope.GetDirecciones();
    $scope.GetCanalizaciones();
    $scope.SelectTipoProducto(1);
    HideLoading();
}

RecepcionController.$inject = ['$scope', '$http', '$location', '$route', '$routeParams'];