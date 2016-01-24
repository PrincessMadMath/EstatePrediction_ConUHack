(function () {
    'use strict';

    angular
        .module('app.dashboard')
        .controller('DashboardController', DashboardController);

    DashboardController.$inject = ['$q', 'logger', 'Address', '$state'];
    /* @ngInject */
    function DashboardController($q, logger, Address, $state) {
        var vm = this;
        vm.address = '';
        vm.search = search;


        activate();

        function activate() {

        }

        function search() {
            Address.address = vm.address;
            $state.go('genius', {address: vm.address});
            logger.info('search triggered'+vm.address);
        }

        function getPeople() {

        }
    }
})();
