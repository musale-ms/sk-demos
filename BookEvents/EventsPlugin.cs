namespace BookEvents;
using System.ComponentModel;
using BookEvents.Events;
using Microsoft.SemanticKernel;

public class EventsPlugin
{
    [KernelFunction("get_events")]
    [Description("Gets a list of events in Nairobi")]
    public async Task<List<Event>> GetLightsAsync()
    {
        NairobiEvents events = new();
        return await events.GetEventsAsync();
    }
}