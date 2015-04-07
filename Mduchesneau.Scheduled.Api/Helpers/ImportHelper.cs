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
        public static List<ScheduleEventImportModel> ParseEventsFromCsv(Stream content)
        {
            // read CSV file
            List<ScheduleEventImportModel> importedEvents = new List<ScheduleEventImportModel>();
            using (StreamReader streamReader = new StreamReader(content))
            using (CsvReader reader = new CsvReader(streamReader))
            {
                reader.Configuration.RegisterClassMap<ScheduleEventImportModel.ScheduleEventImportModelMapper>();
                while (reader.Read())
                {
                    try
                    {
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

        /// <summary>Save an imported schedule event object in the database.</summary>
        /// <param name="database">The database instance.</param>
        /// <param name="imported">The imported schedule event.</param>
        /// <returns></returns>
        public static ScheduleEvent ImportScheduleEvent(Database database, ScheduleEventImportModel imported)
        {
            // find or create calendar
            Calendar calendar = database.Calendars.FirstOrDefault(p => p.Name == imported.CalendarName);
            if (calendar == null)
            {
                calendar = new Calendar() { Name = imported.CalendarName, Created = DateTime.UtcNow };
                calendar = database.Calendars.Add(calendar);
            }
            
            ScheduleEvent scheduleEvent = new ScheduleEvent()
            {
                ID = 1,
                CalendarID = calendar.ID,
                Calendar = calendar,
                Title = imported.EventTitle,
                Start = imported.Date.Add(imported.TimeStart),
                End = imported.Date.Add(imported.TimeEnd),
                Created = DateTime.UtcNow
            };
            database.ScheduleEvents.Add(scheduleEvent);

            return scheduleEvent;
        }
    }
}