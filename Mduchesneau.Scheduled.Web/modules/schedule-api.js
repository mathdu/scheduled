(function () {
    angular.module('schedule-api', [])

    .value('apiBaseUrl', "http://localhost:22249/events")

    .value('corsHeaders', function () {
        return {
            'Access-Control-Allow-Origin': '*',
            'Access-Control-Allow-Methods': 'GET, POST, PUT, DELETE, OPTIONS',
            'Access-Control-Allow-Headers': 'Origin, X-Requested-With, Content-Type, Accept'
        };
    })

    .service('scheduleApi', ['$http', 'apiBaseUrl', function ($http, apiBaseUrl) {

        this.getCalendars = function(callback) {
            $http.get(apiBaseUrl + '/calendars')
            .success(function(data) {
                callback(data);
            });
        };

        this.getCalendarEvents = function (calendarId, start, end, callback) {
            $http.get(apiBaseUrl + '/calendar/' + calendarId, { params: { start: start, end: end } })
            .success(function (data) {
                callback(data);
            });
        };

        this.getOverlappingCalendarEvents = function (calendarId, callback) {
            $http.get(apiBaseUrl + '/calendar/overlapping/' + calendarId)
            .success(function (data) {
                callback(data);
            });
        };
    }])

    .service('apiFileUpload', ['$http', 'apiBaseUrl', function ($http, apiBaseUrl) {
        var importUploadUrl = apiBaseUrl + "/import";

        this.uploadFile = function(file) {
            var fd = new FormData();
            fd.append('file', file);
            $http.post(importUploadUrl, fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            })
            .success(function(result) {
                alert('file uploaded! Result: ' + result.Message);
            })
            .error(function(result) {
                alert('file upload failed! Error: ' + (result.ExceptionMessage));
            });
        };
    }]);
})();