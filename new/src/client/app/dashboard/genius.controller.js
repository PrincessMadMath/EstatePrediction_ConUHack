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
        var geocoder;

        activate();

        function activate() {
            vm.address = Address.address;
            NgMap.getMap().then(function(evtMap) {
                map = evtMap;
                geocoder = map.Geocoder();
                logger.info('Genius activated');
            });
        }

        NgMap.getMap().then(function(evtMap) {
            map = evtMap;
            geocoder = map.Geocoder();
        });

        $scope.$watch('vm.search', function() {
            if (vm.search) {
                geocoder.geocode({'address': vm.search}, function (results, status) {
                    if (status === google.maps.GeocoderStatus.OK) {
                        resultsMap.setCenter(results[0].geometry.location);

                    } else {
                        alert('Geocode was not successful for the following reason: ' + status);
                    }
                });
            }
        });

        vm.getRadius = function(event) {
            alert('this circle has radius ' + this.getRadius());
        };
        vm.setCenter = function(event) {
            console.log('event', event);
            map.setCenter(event.latLng);
        };
        vm.foo = function(event, arg1, arg2) {
            alert('this is at '+ this.getPosition());
            alert(arg1+arg2);
        };
        vm.dragStart = function(event) {
            console.log("drag started");
        };
        vm.drag = function(event) {
            console.log("dragging");
        };
        vm.dragEnd = function(event) {
            console.log("drag ended");
        };
        vm.zoom = function(){
            var dist = 0;
            switch(map.getZoom()) {
                case 20 : dist = 1128.497220; break;
                case 19 : dist = 2256.994440; break;
                case 18 : dist = 4513.988880; break;
                case 17 : dist = 9027.977761; break;
                case 16 : dist = 18055.955520; break;
                case 15 : dist = 36111.911040; break;
                case 14 : dist = 72223.822090; break;
                case 13 : dist = 144447.644200; break;
                case 12 : dist = 288895.288400; break;
                case 11 : dist = 577790.576700; break;
                case 10 : dist = 1155581.153000; break;
                case 9  : dist = 2311162.307000; break;
                case 8  : dist = 4622324.614000; break;
                case 7  : dist = 9244649.227000; break;
                case 6  : dist = 18489298.450000; break;
                case 5  : dist = 36978596.910000; break;
                case 4  : dist = 73957193.820000; break;
                case 3  : dist = 147914387.600000; break;
                case 2  : dist = 295828775.300000; break;
                case 1  : dist = 591657550.500000; break;
            }
            vm.radius = dist / 10;
            console.log(dist);
            console.log(map.getZoom());
        };
        vm.centerChanger = function(){
        };


    }
})();
