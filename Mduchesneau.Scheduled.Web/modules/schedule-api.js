(function () {
    angular.module('schedule-api', [])

    .value('corsHeaders', function () {
        return {
            'Access-Control-Allow-Origin': '*',
            'Access-Control-Allow-Methods': 'GET, POST, PUT, DELETE, OPTIONS',
            'Access-Control-Allow-Headers': 'Origin, X-Requested-With, Content-Type, Accept'
        }
    })

    .value('getCalendarEvents', function (start, end, timezone, callback) {
        return callback([]);
    });
})();