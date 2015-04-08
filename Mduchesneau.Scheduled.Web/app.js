(function () {
    angular.module('schedule', ['schedule-calendar', 'schedule-api', 'file-upload'])

    .controller('ImportController', ['$scope', 'apiFileUpload', function ($scope, apiFileUpload) {
        var importer = this;

        importer.uploadFile = function() {
            apiFileUpload.uploadFile($scope.importFile);
        };
    }])

    .controller('CalendarSelectController', ['$scope', 'apiCalendars', function ($scope,apiCalendars) {
        
    }])

    .controller('CalendarController', ['$scope', '$element', 'uiCalendarConfig', 'configureCalendarUi', 'calendarSource', 'apiCalendars', function ($scope, $element, uiCalendarConfig, configureCalendarUi, calendarSource, apiCalendars) {
        var calendar = this;

        $scope.eventSource = [];
            /*{
                url: "http://localhost:22249/events/calendar/63",
                //headers: corsHeaders,
                color: "DarkCyan",
                textColor: "white"
            };*/
        $scope.events = [$scope.eventSource];

        $scope.uiConfig = configureCalendarUi($scope);

        // get calendar list
        apiCalendars.getCalendars(function (data) {
            calendar.calendars = data;
            calendar.selectedCalendar = calendar.calendars[0].Id;
            calendar.loadCalendar(calendar.selectedCalendar);
        });

        // calendar load
        calendar.loadCalendar = function(calendarId) {
            calendarSource.loadCalendar($scope, calendarId);
        };

        // update calendar view on load
        $scope.$on("calendar.loaded", function(event, args) {
            if (!!args.displayDate)
                //uiCalendarConfig.calendars.scheduleCalendar.fullCalendar('gotoDate', args.displayDate);
                $("#scheduleCalendar").fullCalendar('gotoDate', args.displayDate);
        });
    }]);
})();