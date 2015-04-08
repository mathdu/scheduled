(function () {
    angular.module('schedule', ['schedule-calendar', 'schedule-api', 'file-upload'])

    .controller('ImportController', ['$scope', 'apiFileUpload', function ($scope, apiFileUpload) {
        var importer = this;

        importer.uploadFile = function() {
            apiFileUpload.uploadFile($scope.importFile);
        };
    }])

    .controller('CalendarController', ['$scope', '$element', 'uiCalendarConfig', 'configureCalendarUi', 'calendarSource', 'scheduleApi', function ($scope, $element, uiCalendarConfig, configureCalendarUi, calendarSource, scheduleApi) {
        var calendar = this;

        // initialize calendar & sources
        $scope.eventSource = [];
        $scope.events = [$scope.eventSource];
        $scope.uiConfig = configureCalendarUi($scope);

        // get calendar list
        scheduleApi.getCalendars(function (data) {
            calendar.calendars = data;
            calendar.selectedCalendar = calendar.calendars[0].Id;
            calendar.loadCalendar(calendar.selectedCalendar);
        });

        // calendar load method
        calendar.loadCalendar = function(calendarId) {
            calendarSource.loadCalendar($scope, calendarId);
        };

        // update calendar view on load
        $scope.$on("calendar.loaded", function(event, args) {
            if (!!args.displayDate)
                //uiCalendarConfig.calendars.scheduleCalendar.fullCalendar('gotoDate', args.displayDate);
                $("#scheduleCalendar").fullCalendar('gotoDate', args.displayDate);
        });

        calendar.validateSchedule = function() {
            scheduleApi.getOverlappingCalendarEvents(calendar.selectedCalendar, function(data) {
                // no overlapping events
                if (data.length === 0)
                    return alert("Schedule is valid.");

                // has overlapping events
                var eventListing = "";
                angular.forEach(data, function (event) {
                    eventListing += event.title + " (" + event.start + ")" + "\n";
                });

                return alert("Schedule is invalid! The following events are overlapping:\n\n" + eventListing);
            });
        };
    }]);
})();