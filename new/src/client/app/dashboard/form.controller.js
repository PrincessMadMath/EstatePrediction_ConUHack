(function () {
    'use strict';

    angular
        .module('app.dashboard')
        .controller('MainFormController', MainFormController);

    MainFormController.$inject = ['logger'];
    /* @ngInject */
    function MainFormController(logger) {
        var vm = this;

        activate();

        function activate() {
            logger.info('Main form controller activated');
        }

    }
})();
