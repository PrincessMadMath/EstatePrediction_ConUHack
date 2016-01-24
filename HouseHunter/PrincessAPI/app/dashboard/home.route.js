(function() {
    'use strict';

    angular
        .module('app.dashboard')
        .run(appRun);

    appRun.$inject = ['routerHelper'];
    /* @ngInject */
    function appRun(routerHelper) {
        routerHelper.configureStates(getStates());
    }

    function getStates() {
        return [
            {
                state: 'home',
                config: {
                    url: '/',
                    templateUrl: 'app/dashboard/home.html',
                    controller: 'DashboardController',
                    controllerAs: 'vm',
                    title: 'home',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Home'
                    }
                }
            }
        ];
    }
})();
