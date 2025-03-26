namespace BookEvents;
using System.ComponentModel;
using BookEvents.Events;
using Microsoft.SemanticKernel;

public class EventsPlugin
{
    [KernelFunction("get_nairobi_events")]
    [Description("Gets a list of events in Nairobi")]
    public async Task<List<Event>> GetNairobiEventsAsync()
    {
        NairobiEvents events = new();
        return await events.GetEventsAsync();
    }

    [KernelFunction("book_nairobi_event")]
    [Description("Books a nairobi event given the event id")]
    [return: Description("returns the booked event")]
    public async Task<Event> BookNairobiEvent([Description("the ID of the event that is going to be booked")] int eventId)
    {
        NairobiEvents nrb = new();
        var events = await nrb.GetEventsAsync();
        return events.FirstOrDefault(e => e.ID == eventId);
    }
}