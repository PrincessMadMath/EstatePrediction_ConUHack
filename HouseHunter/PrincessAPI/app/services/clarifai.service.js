angular
    .module('app.services')
    .factory('ClarifaiService', ClarifaiService);


/*@ngInject*/
function ClarifaiService($resource) {

    return $resource('api/clarifai/:verb', {
    }, {
        // [{'url':url1}, {'url':url2}]
        predict: {
            method: 'POST',
            params: {verb: 'predict'},
            transformResponse: transform
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