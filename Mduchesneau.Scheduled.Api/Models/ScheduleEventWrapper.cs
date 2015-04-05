
using System;
using Newtonsoft.Json;

namespace Mduchesneau.Scheduled.Api.Models
{
    /// <summary>Metadata about a schedule event.</summary>
    public class ScheduleEventWrapper
    {
        /// <summary>Id of the event.</summary>
        public int Id { get; set; }
        
        /// <summary>Id of the calendar owning the event.</summary>
        public int CalendarId { get; set; }

        /// <summary>Title of the event.</summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>Starting date & time of the event.</summary>
        [JsonProperty(PropertyName = "start")]
        public DateTime Start { get; set; }

        /// <summary>Ending date & time of the event.</summary>
        [JsonProperty(PropertyName = "end")]
        public DateTime End { get; set; }

    }
}