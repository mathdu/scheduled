﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Mduchesneau.Scheduled.Api.Helpers;
using Mduchesneau.Scheduled.Api.Models;
using Mduchesneau.Scheduled.DataModel;
using Newtonsoft.Json;
//using System.Web.Http.HttpPost = HttpPost;

namespace Mduchesneau.Scheduled.Api.Controllers
{
    /// <summary>Retrieve and manage schedule events.</summary>
    [EnableCors(origins: "http://localhost:23876", headers: "*", methods: "*")]
    public class EventsController : ApiController
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

        /// <summary>Get all existing calendars.</summary>
        [HttpGet, Route("events/calendars")]
        public List<Calendar> GetCalendars()
        {
            using (Database database = Database.getInstance())
            {
                return database.Calendars.ToList();
            }
        }

        [HttpPost, Route("events/import")]
        public object ImportFromCsvContent()
        {
            using (Database database = Database.getInstance())
            {
                var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
                if (file == null)
                    throw new InvalidOperationException("Import file data is null!");

                // Gather parsed data
                List<ScheduleEventImportModel> importedEvents = ImportHelper.ParseEventsFromCsv(file.InputStream);

                // Import events
                foreach (ScheduleEventImportModel importedEvent in importedEvents)
                    ImportHelper.ImportScheduleEvent(database, importedEvent);

                database.SaveChanges();

                return new { Message = String.Format("{0} events imported.", importedEvents.Count()) };
            }
        }
    }
}