using System;

namespace CrimeService.Entities
{
    public class Crime
    {
        public Guid Id { get; set; }
        public DateTimeOffset EventDate { get; set; }
        public EventType EventType { get; set; }
        public string Description { get; set; }
        public string EventPlace { get; set; }
        public string Email { get; set; }
        public Status Status { get; set; }
        public string LawEnforcementId { get; set; }
    }
}
