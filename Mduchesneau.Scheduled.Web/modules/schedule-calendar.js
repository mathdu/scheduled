(function () {
    angular.module('schedule-calendar', ['ui.calendar'])

    .value('configureCalendarUi', function ($scope) {

        /* alert on eventClick */
        var alertOnDayClick = function(event, jsEvent, view) {
            $scope.alertMessage = (event.format('DD-MMM-YYYY') + ' was clicked ');
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
                    left: 'month basicWeek basicDay agendaWeek agendaDay',
                    center: 'title',
                    right: 'today prev,next'
                },
                dayClick: alertOnDayClick,
                //eventClick: alertOnEventClick,
                eventDrop: alertOnDrop,
                eventResize: alertOnResize
            }
        };
    });

})();