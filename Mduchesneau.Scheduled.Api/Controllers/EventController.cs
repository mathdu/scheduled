using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Mduchesneau.Scheduled.Api.Models;
using Mduchesneau.Scheduled.DataModel;
using Newtonsoft.Json;

namespace Mduchesneau.Scheduled.Api.Controllers
{
    /// <summary>Retrieve and manage schedule events.</summary>
    [EnableCors(origins: "http://localhost:23876", headers: "*", methods: "*")]
    public class EventController : ApiController
    {
        /*********
        ** Public methods
        *********/
        /// <summary>Get the event for the specified id.</summary>
        /// <param name="id">The id of the event.</param>
        [HttpGet, Route("events/{id}")]
        public ScheduleEvent GetEvent(int id)
        {
            using (Database database = Database.getInstance())
            {
                //return result.Project<Event, EventWrapper>();*/
                return database.ScheduleEvents.First(p => p.ID == id);
            }
        }

        /// <summary>Get all events within bounds for the calendar specified.</summary>
        /// <param name="calendarId">Id of the calendar.</param>
        /// <param name="start">The earliest date boundary to query events for.</param>
        /// <param name="end">The latest date boundary to query events for.</param>
        [HttpGet, Route("events/calendar/{calendarId}")]
        public List<ScheduleEventWrapper> GetEventsForCalendar(string calendarId, DateTime start, DateTime end)
        {
            using (var client = new WebClient())
            {
                Uri contextUrl = HttpContext.Current.Request.Url;
                string baseUrl = String.Format("{0}://{1}:{2}", contextUrl.Scheme, contextUrl.Host, contextUrl.Port);
                string content = client.DownloadString(String.Format("{0}/events.json", baseUrl));
                List<ScheduleEventWrapper> events = JsonConvert.DeserializeObject<List<ScheduleEventWrapper>>(content);
                return events;
            }
        }
    }
}
