(function () {
    angular.module('schedule', ['schedule-calendar', 'schedule-api', 'file-upload'])

    .controller('ImportController', [
        '$scope', 'fileUpload', function($scope, fileUpload) {
            var importer = this;

            importer.uploadFile = function() {
                var file = $scope.importFile;
                var uploadUrl = "http://localhost:22249/events/import";
                fileUpload.uploadFileToUrl(file, uploadUrl);
            };

        }
    ])
    .controller('CalendarController', [
        '$scope', 'configureCalendarUi', 'corsHeaders', function($scope, configureCalendarUi, corsHeaders) {
            $scope.uiConfig = configureCalendarUi($scope);

            $scope.eventSource =
            {
                url: "http://localhost:22249/events/calendar/45",
                headers: corsHeaders,
                color: "DarkCyan",
                textColor: "white"
            };

            $scope.events = [$scope.eventSource];
        }
    ]);
})();