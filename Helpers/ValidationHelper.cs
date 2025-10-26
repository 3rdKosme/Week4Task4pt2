using Week4Task4pt2.Domain.Exceptions;

namespace Week4Task4pt2.Helpers;

public static class ValidationHelper
{
    public static void CheckId(int id)
    {
        if (id <= 0)
        {
            throw new ValidationException("ID должен быть целым положительным числом.");
        }
    }
}