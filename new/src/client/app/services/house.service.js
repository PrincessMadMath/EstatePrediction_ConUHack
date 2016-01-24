angular
    .module('app.services')
    .factory('HouseService', HouseService);


/*@ngInject*/
function HouseService($resource) {

    return $resource('api/house/:verb', {
    }, {

        predictHouse: {
            method: 'POST',
            params: {verb: 'predictHouse'}
        },
        predictCondo: {
            method: 'POST',
            params: {verb: 'predictCondo'}
        },
        all: {
            method: 'GET',
            isArray: true,
            params: {verb: 'all'}
        },
        localization: {
            method: 'POST',
            isArray: true,
            params: {verb: 'localization'}
        }
    });
}
