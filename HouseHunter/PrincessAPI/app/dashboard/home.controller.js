(function () {
    'use strict';

    angular
        .module('app.dashboard')
        .controller('DashboardController', DashboardController);

    DashboardController.$inject = ['$q', 'logger', 'Address', '$state', '$http'];
    /* @ngInject */
    function DashboardController($q, logger, Address, $state, $http) {
        var vm = this;
        vm.address = '';
        vm.search = search;
        vm.getLocation = function(val) {
            return $http.get('//maps.googleapis.com/maps/api/geocode/json', {
                params: {
                    address: val,
                    sensor: false
                }
            }).then(function(response){
                return response.data.results.map(function(item){
                    return item.formatted_address;
                });
            });
        };

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
