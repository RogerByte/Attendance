angular.module('home')
.factory('Colaboradores', 
        ['$http', function ($http) {
            var service = {};
            service.ListaSolicitudes = function (EmpleadoId, callback) {
                $http.post('http://' + location.host + '/AttendanceKiosco/Kiosco.svc/json/ListaSolicitudes',
                    { Empleado: EmpleadoId }
                ).success(function (response) {
                    callback(response);
                })
            };
            service.CatalogoTipoSolicitudes = function (callback) {
                $http.post('http://' + location.host + '/AttendanceKiosco/Kiosco.svc/json/CatalogoSolicitudes',
                    {}
                    ).success(function (response) {
                        callback(response)
                    })
            };
            return service;
}]);
