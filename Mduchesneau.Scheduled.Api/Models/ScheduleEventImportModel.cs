using System;
using System.Globalization;
using CsvHelper.Configuration;

namespace Mduchesneau.Scheduled.Api.Models
{
    public class ScheduleEventImportModel
    {
        public string CalendarName { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan TimeStart { get; set; }

        public TimeSpan TimeEnd { get; set; }

        public string Title { get; set; }

        /// <summary>Maps the model fields to a Schedule CSV import file's columns.</summary>
        public sealed class ScheduleEventImportModelMapper : CsvClassMap<ScheduleEventImportModel>
        {
            /// <summary>Define mapping directives.</summary>
            public ScheduleEventImportModelMapper()
            {
                // simple mappings
                Map(p => p.CalendarName).Name("Nom");
                Map(p => p.Title).Name("Description");

                // type or data conversions
                Map(p => p.Date).Name("Date").TypeConverterOption(DateTimeStyles.None);
                Map(p => p.TimeStart).Name("Heure début").TypeConverterOption(DateTimeStyles.None);
                Map(p => p.TimeStart).Name("Heure fin").TypeConverterOption(DateTimeStyles.None);
            }
        }
    }
}