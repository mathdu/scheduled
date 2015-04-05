(function () {
    angular.module('schedule', ['schedule-calendar', 'schedule-api'])

    .controller('ImportController', ['$scope', function($scope) {
        
    }])

    .controller('CalendarController', ['$scope', 'configureCalendarUi', 'corsHeaders', function ($scope, configureCalendarUi, corsHeaders) {
        $scope.uiConfig = configureCalendarUi($scope);

        $scope.eventSource = 
        {
            url: "http://localhost:22249/events/calendar/0",
            headers: corsHeaders,
            color: "DarkCyan",
            textColor: "white"
        };

        $scope.events = [$scope.eventSource];
    }]);


})();