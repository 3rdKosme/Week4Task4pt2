namespace Week4Task4pt2.Domain.Exceptions;

public class NotFoundException : AppException
{
    public NotFoundException(string message) : base(message, 404) { }
}