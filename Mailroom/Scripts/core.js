var core = angular.module('core', ['ngSanitize', 'ngRoute', 'ui.select', 'ui.bootstrap','angularUtils.directives.dirPagination']);

core.controller('LoginController', LoginController);
core.controller('IndexController', IndexController);
core.controller('DashboardController', DashboardController);
core.controller('ReportesController', ReportesController);
core.controller('EgresosController', EgresosController);
core.controller('ProveedoresController', ProveedoresController);

var configFunction = function ($routeProvider) {
    $routeProvider.
        when('/recepcion', {
            templateUrl: 'routes/recepcion'
        })
        .when('/egresos', {
            templateUrl: 'routes/egresos'
        })
        .when('/dashboard', {
            templateUrl: 'routes/dashboard'
        })
        .when('/reportes', {
            templateUrl: 'routes/reportes'
        })
        .when('/reportespf/:pref', {
            templateUrl: function (params) { return 'routes/reportes?pref=' + params.pref; },
        })
        .when('/proveedor', {
            templateUrl: 'routes/proveedores'
        })
        .when('/proveedor_edit/:id', {
            templateUrl: function (params) { return 'routes/proveedoresadd?id=' + params.id; },
        })
        .when('/proveedor_add', {
            templateUrl: 'routes/proveedoresadd?id=null',
        })
        ;
}

configFunction.$inject = ['$routeProvider'];

core.config(configFunction);