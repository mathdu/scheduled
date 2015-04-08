(function () {
    angular.module('schedule-calendar', ['schedule-api', 'ui.calendar', 'ui.bootstrap.buttons'])
    
    .directive('calendarSelect', function() {
        return {
            restrict: 'E',
            templateUrl: 'templates/calendar-select.html'
        };
    })

    .service('calendarSource', ['apiBaseUrl', 'apiCalendarEvents', 'corsHeaders', function (apiBaseUrl, apiCalendarEvents, corsHeaders) {

        this.loadCalendar = function ($scope, calendarId) {

            apiCalendarEvents.getCalendarEvents(calendarId, null, null, function(data) {
                $scope.events.splice(0, $scope.events.length);
                $scope.events.push(data);

                if (data.length > 0)
                    $scope.$emit('calendar.loaded', { displayDate: data[0].start });
            });
        };
    }])

    .value('configureCalendarUi', function ($scope) {

        /* alert on eventClick */
        var alertOnDayClick = function(event, jsEvent, view) {
            $scope.alertMessage = (event.format('DD-MMM-YYYY') + ' was clicked ');
        };
        /* alert on eventClick */
        var alertOnEventClick = function (event, jsEvent, view) {
            $scope.alertMessage = (event.title + ' was clicked ');
        };
        /* alert on Drop */
        var alertOnDrop = function (event, delta, revertFunc, jsEvent, ui, view) {
            $scope.alertMessage = ('Event Droped to make dayDelta ' + delta);
        };
        /* alert on Resize */
        var alertOnResize = function (event, delta, revertFunc, jsEvent, ui, view) {
            $scope.alertMessage = ('Event Resized to make dayDelta ' + delta);
        };

        /* config object */
        return {
            calendar: {
                height: 850,
                editable: true,
                header: {
                    left: 'month agendaWeek agendaDay',
                    center: 'title',
                    right: 'today prev,next'
                },
                dayClick: alertOnDayClick,
                eventClick: alertOnEventClick,
                eventDrop: alertOnDrop,
                eventResize: alertOnResize
            }
        };
    });

})();