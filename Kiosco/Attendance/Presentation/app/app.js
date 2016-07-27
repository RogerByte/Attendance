angular.module('Authentication', ['ngMaterial', 'ngMessages']);
angular.module('home', ['ngMaterial', 'ngMessages']);
angular.module('attendance', ['ngMaterial', 'Authentication', 'home', 'ngRoute', 'ngCookies'])
.config(['$routeProvider',
    function ($routeProvider) {
        $routeProvider
            .when('/login', {
                templateUrl: 'Presentation/app/login/authentication.html',
                controller: 'LoginController'
            })

            .when('/', {
                templateUrl: 'Presentation/app/home/home.html',
                controller: 'HomeController'
            })
            .otherwise({ redirectTo: '/login' });
    }])
    .config(function ($mdThemingProvider) {
        $mdThemingProvider.theme('default')
            .primaryPalette('teal', {
                'default': '500', // by default use shade 400 from the teal palette for primary intentions
                'hue-1': '100', // use shade 100 for the <code>md-hue-1</code> class
                'hue-2': '600', // use shade 600 for the <code>md-hue-2</code> class
                'hue-3': 'A100' // use shade A100 for the <code>md-hue-3</code> class
            })
            .accentPalette('red', {
                'default': '200'
            })
            .backgroundPalette('grey', {
                'default': '200'
            })
    },
    function ($httpProvider) {
        delete $httpProvider.defaults.headers.common['X-Requested-With'];
    }
)
.run(['$rootScope', '$location', '$cookieStore', '$http', //lo seguido son los procedimientos al momento de iniciar la aplicación.
    function ($rootScope, $location, $cookieStore, $http) {
        // keep user logged in after page refresh
        $rootScope.logeado = false;
        $rootScope.globals = $cookieStore.get('globals') || {};
        if ($rootScope.globals.currentUser) {
            $http.defaults.headers.common['Authorization'] = 'Basic ' + $rootScope.globals.currentUser.authdata; // jshint ignore:line
            $rootScope.logeado = true;
        }
        $rootScope.$on('$locationChangeStart', function (event, next, current) {
            // redirect to login page if not logged in
            if ($location.path() !== '/login' && !$rootScope.globals.currentUser) {
                $location.path('/login');
            }
        })
    }]);