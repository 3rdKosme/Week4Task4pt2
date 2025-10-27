using Week4Task4pt2.Domain.Exceptions;
using Week4Task4pt2.Application.Constants;

namespace Week4Task4pt2.Helpers;

public static class ValidationHelper
{
    public static void CheckId(int id)
    {
        if (id <= 0)
        {
            throw new ValidationException(ErrorMessages.IncorrectId);
        }
    }
}