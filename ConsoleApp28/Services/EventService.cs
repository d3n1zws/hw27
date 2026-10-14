namespace ConsoleApp28.Services;
using ConsoleApp28.Models;
using ConsoleApp28.Exceptions;
public class EventService
{
    public List<Event> Events { get; set; } = new List<Event>();

    public void AddEvent(int id, string name, DateTime date, decimal ticketPrice)
    {
        var newEvent = new Event
        {
            Id = id,
            Name = name,
            Date = date,
            TicketPrice = ticketPrice
        };
        Events.Add(newEvent);
    }
    public void ShowMostSoldEventsDescending()
    {
        if (Events.Count == 0)
        {
            throw new NotFoundException("No events found.");
        }
        List<Event> mostSoldEvents = Events.OrderByDescending(e => e.Tickets.Count(t => t.IsUsed)).ToList();
        foreach (var @event in mostSoldEvents)
        {
            Console.WriteLine($"- {@event.Name} with {@event.Tickets.Count(t => t.IsUsed)} tickets sold.");
        }
    }
    public void GetMostProfitableTickets()
    {
        if (Events.Count == 0)
        {
            throw new NotFoundException("No events found.");
        }
        var mostProfitableTickets = Events.SelectMany(e => e.Tickets)
                                           .Where(t => t.IsUsed)
                                           .OrderByDescending(t => t.Price)
                                           .ToList();
        foreach (var ticket in mostProfitableTickets)
        {
            Console.WriteLine($"- Ticket Id: {ticket.Id}, Price: {ticket.Price}, Event: {ticket.Event.Name}");
        }
    }
    public void ShowFutureEvents()
    {
        if (Events.Count == 0)
        {
            throw new NotFoundException("No events found.");
        }
        var futureEvents = Events.Where(e => e.Date > DateTime.Now).ToList();
        foreach (var @event in futureEvents)
        {
            Console.WriteLine($"- {@event.Name} on {@event.Date}");
        }
    }
    public void ShowPastEventsWithTicketUsage()
    {
        if (Events.Count == 0)
        {
            throw new NotFoundException("No events found.");
        }
        var pastEvents = Events.Where(e => e.Date < DateTime.Now).ToList();
        foreach (var @event in pastEvents)
        {
            int totalTickets = @event.Tickets.Count;
            int usedTickets = @event.Tickets.Count(t => t.IsUsed);
            Console.WriteLine($"{@event.Name}: {usedTickets} / {totalTickets} tickets used.");
        }
    }

}
