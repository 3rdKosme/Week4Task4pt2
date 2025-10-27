namespace Week4Task4pt2.Application.Constants;

public static class ErrorMessages
{
    public const string IncorrectId = "Id должен быть целым полодительным числом.";

    public static class Books
    {
        public const string NotFound = "Книги с таким Id не существует.";
        public const string IncorrectPublicationDate = "Некорректная дата публикации.";
    }

    public static class Authors
    {
        public const string NotFound = "Автора с таким Id не существует.";
        public const string IncorrectBirthDate = "Некорректная дата рождения.";
    }
}