namespace ConsoleApp28.Exceptions;

public class TicketAlreadyUsedException : Exception
{
    public TicketAlreadyUsedException(string Message) : base(Message)
    {
         
    }
}
