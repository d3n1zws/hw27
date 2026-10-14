using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp28.Models;

public class Event
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime Date { get; set; }
    public decimal TicketPrice { get; set; }
    public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    public override string ToString()
    {
        return $"Event Id: {Id}, Name: {Name}, Date: {Date}, Ticket Price: {TicketPrice}, Tickets Count: {Tickets.Count}"; 
    }
}
