namespace Week4Task4pt2.Helpers;

public static class ValidationHelper
{
    public static void CheckId(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID должен быть целым положительным числом.");
        }
    }
}