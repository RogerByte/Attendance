angular.module('home')
.controller('HomeController', ['$scope', '$rootScope', '$cookieStore', '$mdDialog', '$mdMedia', 'Colaboradores',
    function ($scope, $rootScope, $cookieStore, $mdDialog, $mdMedia, Colaboradores) {
        $scope.Colaborador = {
            NumeroEmpleado: $rootScope.globals.currentUser.NumeroEmpleado,
            Nombre: $rootScope.globals.currentUser.NombreEmpleado,
            Puesto: $rootScope.globals.currentUser.Puesto,
            SaldoVacacional: $rootScope.globals.currentUser.SaldoVacacional,
            Compania: $rootScope.globals.currentUser.Compania
        };
        $scope.Incidencias = $rootScope.globals.currentUser.Incidencias;
        $scope.ActualizarListaSolicitudes = function () {
            Colaboradores.ListaSolicitudes($rootScope.globals.currentUser.EmpleadoId, function (response) {
                $rootScope.globals.currentUser.Incidencias = response.ListaSolicitudesResult;
                $scope.Incidencias = $rootScope.globals.currentUser.Incidencias;
                $cookieStore.put('globals', $rootScope.globals);
            })
        };
        $scope.showAdvanced = function (ev) {
            var useFullScreen = ($mdMedia('sm') || $mdMedia('xs')) && $scope.customFullscreen;
            $mdDialog.show({
                controller: DialogController,
                templateUrl: 'Presentation/app/colaborador/chdAddRequest.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: true,
                fullscreen: useFullScreen
            })
            .then(function (solicitud) {
                $scope.ActualizarListaSolicitudes();
            }, function () {
                $scope.status = 'You cancelled the dialog.';
            });
            $scope.$watch(function () {
                return $mdMedia('xs') || $mdMedia('sm');
            }, function (wantsFullScreen) {
                $scope.customFullscreen = (wantsFullScreen === true);
            });
        };
        $scope.showAlert = function (Mensaje) {
            $mdDialog.show(
                $mdDialog.alert()
                .parent(angular.element(document.querySelector('#contenedor')))
                .clickOutsideToClose(true)
                .title('Mensaje del sistema')
                .content(Mensaje)
                .ariaLabel('MENSAJE DIALOGO')
                .ok('Aceptar')
                .targetEvent()
            )
        };
}])
function DialogController($scope, $rootScope, $mdDialog, $http) {
    $scope.CatalogoTipoSolicitudes = function (callback) {
        $http.post('http://' + location.host + '/AttendanceKiosco/Kiosco.svc/json/CatalogoSolicitudes'
            ).success(function (response) {
                callback(response)
            })
    };
    var init = function () {
        $scope.CatalogoTipoSolicitudes(function (response) {
            $scope.requestTypes = response.CatalogoSolicitudesResult;
        });
    };
    init();
        
    $scope.hide = function () {
        $mdDialog.hide();
    };
    $scope.cancel = function () {
        $mdDialog.cancel();
    };
    $scope.showAlert = function (Mensaje) {
        $mdDialog.show(
            $mdDialog.alert()
            .parent(angular.element(document.querySelector('#requestDialog')))
            .clickOutsideToClose(true)
            .title('Mensaje del sistema')
            .content(Mensaje)
            .ariaLabel('MENSAJE DIALOGO')
            .ok('Aceptar')
            .targetEvent()
        )
    };
    //------------------------------
    $scope.EnviarSolicitud = function (solicitud) {
        $rootScope.solicitud = solicitud;
        if ($rootScope.solicitud.fechaInicio <= $rootScope.solicitud.fechaFin)
            $mdDialog.hide(solicitud);
        else
            alert('Su solicitud no pudo ser completada porque la fecha de inicio es mayor a la fecha fin');
    };
    $scope.isFechaInicioValid = function (FechaInicio, FechaFin) {
        if(FechaInicio <= FechaFin)
            return true;
        else
            return false;
    };
}