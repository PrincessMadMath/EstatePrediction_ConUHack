(function() {
    'use strict';

    angular
        .module('app.dashboard')
        .directive('hsMainForm', MainForm);

    /* @ngInject */
    function MainForm () {
        var directive = {
            bindToController: true,
            controller: MainFormController,
            controllerAs: 'vm',
            restrict: 'EA',
            templateUrl: 'app/dashboard/main-form.html'
        };


        return directive;
    }
})();
