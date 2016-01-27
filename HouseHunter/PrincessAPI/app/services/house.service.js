angular
    .module('app.services')
    .factory('HouseService', HouseService);


/*@ngInject*/
function HouseService($resource) {

    return $resource('api/house/:verb', {
    }, {

        predictHouse: {
            method: 'POST',
            params: {verb: 'predictHouse'},
            transformResponse: transform
        },
        predictCondo: {
            method: 'POST',
            params: {verb: 'predictCondo'},
            transformResponse: transform
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

function transform(result) {
    var a ={}
    result = JSON.parse(result);
    a.price = parseInt(result.price.split('.')[0]);
    a.error = parseInt(result.error.split('.')[0]);
    return a;
}