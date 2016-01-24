(function () {
    'use strict';

    angular
        .module('app.dashboard')
        .controller('GeniusController', GeniusController);

    GeniusController.$inject = ['$q', 'logger', 'Address', '$stateParams'];
    /* @ngInject */
    function GeniusController($q, logger, Address, $stateParams) {
        var vm = this;
        vm.address = '';
        vm.prediction = undefined;
        activate();

        function activate() {
            vm.address = Address.address;
            logger.info('Genius activated');
            logger.info(vm.address);
        }
    }
})();
