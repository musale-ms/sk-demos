using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookEvents.Events
{
    public class NairobiEvents
    {
        private readonly List<Event> events = new()
        {
            new Event {ID = Guid.NewGuid(), Name = "Pasapasa", Location = "Ngong race course", Date = "2/4/2025"},
            new Event {ID = Guid.NewGuid(), Name = "Dj Grauchi", Location = "Cavalli", Date = "8/12/2025"},
            new Event {ID = Guid.NewGuid(), Name = "JIT access", Location = "Yubi Keys", Date = "5/8/2025"},
            new Event {ID = Guid.NewGuid(), Name = "Ladies night", Location = "DND lounge", Date = "12/4/2025"},
            new Event {ID = Guid.NewGuid(), Name = "Knight and Day", Location = "Nyayo", Date = "21/5/2025"},
            new Event {ID = Guid.NewGuid(), Name = "Kombucha", Location = "Sip City", Date = "8/5/2025"},
            new Event {ID = Guid.NewGuid(), Name = "Tap Dance", Location = "Red Room", Date = "8/9/2025"}
        };

        public Task<List<Event>> GetEventsAsync()
        {
            return Task.FromResult(events);
        }
    }

    public class Event
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Date { get; set; }
        public string Price { get; set; }
    }
}
