//namespace ConsoleApp28.Services;
//using ConsoleApp28.Models;
//using ConsoleApp28.Contexts;
//using ConsoleApp28.Exceptions;
//public class TicketService
//{
//    public async Task CreateTicketAsync(int eventid)
//    {
//        var context = new Context();
//        var @event = context.Events.Find(eventid);
//        if (@event == null)
//        {
//            throw new Exception("Event not found");
//        }
//        if (@event.Date < DateTime.Now)
//        {
//            throw new EventExpiredException("Event has already passed");
//        }
//        var ticket = new Ticket
//        {
//            Price = @event.TicketPrice,
//            EventId = @event.Id
//        };
//        context.Tickets.Add(ticket);
//        await context.SaveChangesAsync();
//    }
//    public void Read()
//    {
//        var context = new Context();
//        var tickets = context.Tickets.ToList();
//        foreach (var ticket in tickets)
//        {
//            Console.WriteLine(ticket.ToString());
//        }
//    }
//    public void Update(int ticketId, bool isUsed)
//    {
//        var context = new Context();
//        var ticket = context.Tickets.Find(ticketId);
//        if (ticket == null)
//        {
//            throw new Exception("Ticket not found");
//        }
//        ticket.IsUsed = isUsed;
//        context.SaveChanges();
//    }
//    public async Task Delete(int ticketId)
//    {
//        var context = new Context();
//        var ticket = context.Tickets.Find(ticketId);
//        if (ticket == null)
//        {
//            throw new Exception("Ticket not found");
//        }
//        context.Tickets.Remove(ticket);
//        await context.SaveChangesAsync();
//    }
//    public void UseTicket(int ticketId)
//    {
//        var context = new Context();
//        var ticket = context.Tickets.Find(ticketId);
//        if (ticket == null)
//        {
//            throw new NotFoundException("Ticket not found");
//        }
//        if (ticket.IsUsed)
//        {
//            throw new TicketAlreadyUsedException("Ticket has already been used");
//        }
//        ticket.IsUsed = true;
//        context.SaveChanges();
//    }
//}
