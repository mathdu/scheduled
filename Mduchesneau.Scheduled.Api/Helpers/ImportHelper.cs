using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using Mduchesneau.Scheduled.Api.Models;
using Mduchesneau.Scheduled.DataModel;

namespace Mduchesneau.Scheduled.Api.Helpers
{
    public static class ImportHelper
    {
        /// <summary>Parse a CSV import file into schedule event import objects.</summary>
        /// <param name="content">The import file content.</param>
        /// <returns></returns>
        public static IEnumerable<ScheduleEventImportModel> ParseEventsFromCsv(Stream content)
        {
            // read csv file
            List<ScheduleEventImportModel> importedEvents = new List<ScheduleEventImportModel>();
            using (StreamReader streamReader = new StreamReader(content))
            using (CsvReader reader = new CsvReader(streamReader))
            {
                // set csv mapping
                reader.Configuration.RegisterClassMap<ScheduleEventImportModel.ScheduleEventImportModelMapper>();
                while (reader.Read())
                {
                    try
                    {
                        // parse and add import row
                        importedEvents.Add(reader.GetRecord<ScheduleEventImportModel>());
                    }
                    catch (CsvReaderException exception)
                    {
                       throw new Exception(String.Format("Couldn't parse row #{0}: {1}", reader.Row, exception.Message)); 
                    }
                }
                return importedEvents;
            }
        }

        /// <summary>Create and add schedule events and their corresponding calendars from the list of import events specified.</summary>
        /// <param name="database">The database instance.</param>
        /// <param name="importedEvents">The events to import.</param>
        /// <returns></returns>
        public static IEnumerable<ScheduleEvent> ImportScheduleEvents(Database database, IEnumerable<ScheduleEventImportModel> importedEvents)
        {
            List<ScheduleEvent> events = new List<ScheduleEvent>();

            // group by calendar name
            Calendar calendar = null;
            foreach (ScheduleEventImportModel importedEvent in importedEvents.OrderBy(p => p.CalendarName))
            {
                // reuse or find calendar
                calendar = (calendar == null || calendar.Name != importedEvent.CalendarName)
                            ? database.Calendars.FirstOrDefault(p => p.Name == importedEvent.CalendarName)
                            : calendar;
                // create calendar if none of the existing calendars fit
                calendar = calendar ?? database.Calendars.Add(new Calendar() { Name = importedEvent.CalendarName, Created = DateTime.UtcNow });

                // create event
                events.Add(CreateScheduleEvent(database, calendar, importedEvent));
            }
            // import
            return database.ScheduleEvents.AddRange(events);
        }

        /********
         * Private methods
         ********/
        /// <summary>Save an imported schedule event object in the database.</summary>
        /// <param name="database">The database instance.</param>
        /// <param name="calendar">The calendar object to link the event to.</param>
        /// <param name="imported">The imported schedule event.</param>
        /// <returns></returns>
        private static ScheduleEvent CreateScheduleEvent(Database database, Calendar calendar, ScheduleEventImportModel imported)
        {
            // verify if event already exists before creating
            DateTime startDateTime = imported.Date.Add(imported.TimeStart);
            var eventExists = database.ScheduleEvents.Any(p => p.CalendarID == calendar.ID
                                                            && p.Title == imported.Title
                                                            && p.Start == startDateTime);
            if (eventExists)
                throw new InvalidOperationException(String.Format("Can't import '{0}' in '{1}': Event already exists!", imported.Title, imported.CalendarName));

            // create event
            return new ScheduleEvent()
            {
                ID = 1,
                CalendarID = calendar.ID,
                Calendar = calendar,
                Title = imported.Title,
                Start = startDateTime,
                End = imported.Date.Add(imported.TimeEnd),
                Created = DateTime.UtcNow
            };
        }
    }
}