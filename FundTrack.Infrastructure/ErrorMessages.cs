namespace FundTrack.Infrastructure
{
    /// <summary>
    /// Class for contain error messages
    /// </summary>
    public static class ErrorMessages
    {
        public const string RequiredFieldMessage = "Обовязкове поле для заповнення";
        public const string MinLengthMessage = "Мінімальна кількість символів повинна бути більша 7";
        public const string MaxLengthMessage = "Значення не повинно бути більше 20 символів";
        public const string PatternLoginMessage = "Невірний формат login";
        public const string PatternEmailMessage = "Невірний формат email адреса";
        public const string AddUserMessage = "Користувач не був доданий";
        public const string GetAllUsersMessage = "Неможливо отримати список користувачів";
        public const string UserExistsMessage = "Користувач вже існує";
        public const string NoEntriesInDatabase = "Немає жодного запису в базі даних";
        public const string IncorrectCredentials = "Неправильний логін або пароль";
    }
}
