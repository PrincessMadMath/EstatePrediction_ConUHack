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
                state: 'genius',
                config: {
                    url: '/genius',
                    templateUrl: 'app/dashboard/genius.html',
                    controller: 'GeniusController',
                    controllerAs: 'vm',
                    title: 'genius',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Genius'
                    }
                }
            }
        ];
    }

})();
