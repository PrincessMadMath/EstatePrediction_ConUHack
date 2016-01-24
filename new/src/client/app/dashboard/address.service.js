angular
    .module('app.core')
    .factory('Address', address);


function address() {
    return {
        address: ''
    };
}
