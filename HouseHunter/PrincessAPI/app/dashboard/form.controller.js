(function () {
    'use strict';

    angular
        .module('app.dashboard')
        .controller('MainFormController', MainFormController);

    MainFormController.$inject = ['logger', 'HouseService', 'ClarifaiService'];
    /* @ngInject */
    function MainFormController(logger, HouseService, ClarifaiService) {
        var vm = this;

        vm.predictHouse = predictHouse;
        vm.predictCondo = predictCondo;
        vm.predictUrls = predictUrls;
        vm.addUrl =addUrl;

        vm.urls = [];
        vm.currentUrl = '';
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

        function addUrl() {
            vm.urls.push(
                {url: vm.currentUrl}
            );
            vm.currentUrl = '';
        }
        function predictUrls(){
            ClarifaiService.predict(vm.urls).$promise
            .then(function(result) {
                vm.prediction = result;
            });
            vm.urls=[];
            vm.currentUrl='';
        }

        function predictHouse() {
            vm.home.address = vm.address;
            vm.home.lat = vm.lat;
            vm.home.lon = vm.lon;
            HouseService.predictHouse(vm.home).$promise
                .then(function(result) {
                    vm.prediction= result;
                });
        }

        function predictCondo() {
            vm.condo.address = vm.address;
            vm.condo.lat = vm.lat;
            vm.condo.lon = vm.lon;
            HouseService.predictCondo(vm.condo).$promise
                .then(function(result) {
                    vm.prediction = result;
                });
        }

    }
})();
