using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookEvents.Events
{
    public class NairobiEvents
    {
        private readonly List<Event> events = new()
        {
            new Event {ID = 1, Name = "Pasapasa", Location = "Ngong race course", Date = "2/4/2025"},
            new Event {ID = 2, Name = "Dj Grauchi", Location = "Cavalli", Date = "8/12/2025"},
            new Event {ID = 3, Name = "JIT access", Location = "Yubi Keys", Date = "5/8/2025"},
            new Event {ID = 4, Name = "Ladies night", Location = "DND lounge", Date = "12/4/2025"},
            new Event {ID = 5, Name = "Knight and Day", Location = "Nyayo", Date = "21/5/2025"},
            new Event {ID = 6, Name = "Kombucha", Location = "Sip City", Date = "8/5/2025"},
            new Event {ID = 7, Name = "Tap Dance", Location = "Red Room", Date = "8/9/2025"}
        };

        public async Task<List<Event>> GetEventsAsync()
        {
            return events;
        }
    }

    public class Event
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Date { get; set; }
        public string Price { get; set; }
    }
}
