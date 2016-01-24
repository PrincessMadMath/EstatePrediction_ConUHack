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
            params: {verb: 'predict'}
        }
    });
}
