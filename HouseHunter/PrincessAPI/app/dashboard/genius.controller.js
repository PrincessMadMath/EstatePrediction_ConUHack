(function () {
    'use strict';

    angular
        .module('app.dashboard')
        .controller('GeniusController', GeniusController);

    GeniusController.$inject = ['$q', 'logger', 'Address', '$stateParams', 'NgMap', '$scope'];
    /* @ngInject */
    function GeniusController($q, logger, Address, $stateParams, NgMap, $scope) {
        var vm = this;
        vm.address = '';
        vm.prediction = undefined;
        vm.latlng = [40.741, -74.181];
        vm.radius = 0;

        var map;
        vm.geocoder;

        activate();
        vm.getLowerBound = function(prediction) {
            return parseFloat(prediction.price) + parseFloat(prediction.error);
        }

        function activate() {
            vm.address = Address.address;
            NgMap.getMap().then(function(evtMap) {
                map = evtMap;
                vm.geocoder = new google.maps.Geocoder();
                logger.info('Genius activated');
                if (vm.address) {
                    vm.geocoder.geocode({'address': vm.address}, function (results, status) {
                        if (status === google.maps.GeocoderStatus.OK) {
                            vm.latlng[0] = results[0].geometry.location.lat();
                            vm.latlng[1] = results[0].geometry.location.lng();
                            logger.info(vm.latlng);
                            vm.setCenter(vm.latlng);
                        } else {
                            alert('Geocode was not successful for the following reason: ' + status);
                        }
                    });
                }
            });
        }


        vm.getRadius = function(event) {
            alert('this circle has radius ' + this.getRadius());
        };
        vm.setCenter = function(event) {
            console.log('event', event);
            map.setCenter(event.latLng);
        };
        vm.centerChanger = function(){
        };


    }
})();
