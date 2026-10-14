using ConsoleApp28.Contexts;
using ConsoleApp28.Models;
using ConsoleApp28.Services;
using ConsoleApp28.Exceptions;
using System.Globalization;

var ticketService = new TicketService();

while (true)
{
    ShowMenu();
    var choice = Console.ReadLine()?.Trim();
    try
    {
        switch (choice)
        {
            case "1": AddEventInteractive(); break;
            case "2": ListFutureEvents(); break;
            case "3": BuyTicket(ticketService); break;
            case "4": ticketService.Read(); break;
            case "5": return;
            default:
                Console.WriteLine("Geçersiz seçim. Tekrar deneyin.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Hata: {ex.Message}");
    }
    Console.WriteLine();
}

static void ShowMenu()
{
    Console.WriteLine("--- Menü ---");
    Console.WriteLine("1) Yeni tədbir əlavə et");
    Console.WriteLine("2) Gələcək tədbirləri göstər");
    Console.WriteLine("3) Tədbir üçün bilet al");
    Console.WriteLine("4) Bütün biletləri göstər");
    Console.WriteLine("5) Çıxış");
    Console.Write("Seçiminiz: ");
}

static void AddEventInteractive()
{
    using var context = new Context();
    Console.Write("Tədbir adı: ");
    var name = Console.ReadLine() ?? string.Empty;
    DateTime date;
    while (true)
    {
        Console.Write("Tarix (örn. 2026-10-14 20:30): ");
        var input = Console.ReadLine();
        if (DateTime.TryParseExact(input, new[] { "yyyy-MM-dd HH:mm", "yyyy-MM-dd", "MM/dd/yyyy HH:mm", "MM/dd/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            break;
        if (DateTime.TryParse(input, out date))
            break;
        Console.WriteLine("Tarix formatı düzgün deyil. Tekrar deneyin.");
    }
    decimal price;
    while (true)
    {
        Console.Write("Bilet qiyməti: ");
        var p = Console.ReadLine();
        if (decimal.TryParse(p, out price)) break;
        Console.WriteLine("Qiymət düzgün deyil. Tekrar deneyin.");
    }
    var @event = new Event { Name = name, Date = date, TicketPrice = price };
    context.Events.Add(@event);
    context.SaveChanges();
    Console.WriteLine($"Tədbir əlavə edildi. Id: {@event.Id}");
}

static void ListFutureEvents()
{
    using var context = new Context();
    var future = context.Events.Where(e => e.Date > DateTime.Now).ToList();
    if (!future.Any())
    {
        Console.WriteLine("Gələcək tədbir tapılmadı.");
        return;
    }
    Console.WriteLine("Gələcək tədbirlər:");
    foreach (var e in future)
    {
        Console.WriteLine($"Id: {e.Id} | {e.Name} | {e.Date} | Qiymət: {e.TicketPrice}");
    }
}

static void BuyTicket(TicketService ticketService)
{
    using var context = new Context();
    var future = context.Events.Where(e => e.Date > DateTime.Now).ToList();
    if (!future.Any())
    {
        Console.WriteLine("Bilet alınacaq gələcək tədbir tapılmadı.");
        return;
    }
    Console.WriteLine("Gələcək tədbirlər:");
    foreach (var e in future)
    {
        Console.WriteLine($"Id: {e.Id} | {e.Name} | {e.Date} | Qiymət: {e.TicketPrice}");
    }
    int id;
    while (true)
    {
        Console.Write("Bilet alınacaq tədbirin Id-sini daxil edin: ");
        var input = Console.ReadLine();
        if (int.TryParse(input, out id) && future.Any(ev => ev.Id == id)) break;
        Console.WriteLine("Düzgün Id daxil edin.");
    }
    ticketService.CreateTicketAsync(id).GetAwaiter().GetResult();
    Console.WriteLine("Bilet alındı.");
}

