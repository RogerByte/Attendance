angular.module('Authentication')
.controller('LoginController',
    ['$scope', '$mdDialog', '$rootScope', '$location', 'AuthenticationService',
    function ($scope, $mdDialog, $rootScope, $location, AuthenticationService) {
        AuthenticationService.ClearCredentials();

        $scope.login = function ()
        {
            try{
                $rootScope.dataLoading = true;
                AuthenticationService.Login($scope.credentials, function (response) {
                    if (response.AuthenticateResult.EmpleadoId > 0) {
                        AuthenticationService.SetCredentials(response.AuthenticateResult);
                        $rootScope.dataLoading = false;
                        $location.path('/');
                    }
                    else {
                        $rootScope.dataLoading = false;
                        $scope.showAlert(response.AuthenticateResult.Message);
                    }
                })
            }
            catch (exc) {
                $scope.showAlert(exc);
            }
        } 
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
        }
    }]);