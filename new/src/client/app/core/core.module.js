(function () {
    'use strict';

    angular
        .module('app.core', [
            'ngAnimate', 'ngSanitize', 'ngResource', 'ngMap',
            'blocks.exception', 'blocks.logger', 'blocks.router', 'app.services',
            'ui.router', 'ui.bootstrap'
        ]);
})();
