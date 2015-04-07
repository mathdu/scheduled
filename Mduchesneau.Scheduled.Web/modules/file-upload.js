(function() {
    // file upload control module
    // inspired by: https://uncorkedstudios.com/blog/multipartformdata-file-upload-with-angularjs
    angular.module('file-upload', [])
    
    .directive('simpleFileUpload', ['$parse', function($parse) {
        return {
            restrict: 'A',
            link: function(scope, element, attrs) {
                var model = $parse(attrs.simpleFileUpload);
                var modelSetter = model.assign;

                element.bind('change', function() {
                    scope.$apply(function() {
                        modelSetter(scope, element[0].files[0]);
                    });
                });
            }
        };
    }]);
})();