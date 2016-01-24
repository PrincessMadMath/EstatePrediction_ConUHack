(function () {
    'use strict';

    angular
        .module('app.dashboard')
        .controller('MainFormController', MainFormController);

    MainFormController.$inject = ['logger', 'HouseService'];
    /* @ngInject */
    function MainFormController(logger, HouseService) {
        var vm = this;

        vm.predictHouse = predictHouse;
        vm.predictCondo = predictCondo;
        vm.home = {
            numberOfBedrooms: undefined,
            numberOfBathrooms: undefined,
            numberOfRooms: undefined,
            livingSpaceArea: undefined,
            buildingDimensions: undefined,
            address: undefined,
            yearOfConstruction: undefined,
            lotArea: undefined
        };
        vm.condo = {
            numberOfBedrooms: undefined,
            numberOfBathrooms: undefined,
            numberOfRooms: undefined,
            livingSpaceArea: undefined,
            yearOfConstruction: undefined,
            witchFloor: undefined,
            numberOfLevels: undefined,
            interiorParking: undefined
        };

        activate();

        function activate() {
            logger.info('Main form controller activated');
        }

        function predictHouse() {
            vm.home.address = vm.address;
            HouseService.predictHouse(vm.home).$promise
                .then(function(result) {
                    vm.prediction= result;
                });
        }

        function predictCondo() {
            vm.condo.address = vm.address;
            HouseService.predictCondo(vm.condo).$promise
                .then(function(result) {
                    vm.prediction = result;
                });
        }

    }
})();
