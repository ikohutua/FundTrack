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
        public const string NoUserWithEmail = "Користувача з даним email не існує";
        public const string InvalidGuid = "Недійсний лінк відновлення пароля";
        public const string IncorrectCredentials = "Неправильний логін або пароль.";
        public const string UserIsBaned = "На даний момент ви є заблоковані і не можете увійти в систему. Причина бану:";
        public const string MissedEnterData = "Введіть пароль і логін.";
        public const string InvalidUserRole = "InvalidUserRole";
        public const string InvalidUser = "InvalidUser";
        public const string UpdateDataError = "Неможливо змінити дані";
        public const string DeleteDataError = "Неможливо видалити дані";
        public const string DeleteDependentTarget = "Неможливо видалити зв'язане з рахунком призначення";
        public const string CantFindItem = "Неможливо знайти об'єкт";
        public const string CantCreatedItem = "Неможливо створити об'єкт";
        public const string BadRequestMessage = "Bad request error";
        public const string PatternPhoneMessage = "Невірний формат телефону";
        public const string MoneyFinOpLimit = "Сума повинна бути більшою 0 і меншою 1000000";
        public const string CantFindDataById = "Can't find data with requested id";
        public const string GetOrganizationAccount = "Неможливо отримати рахунок організації";
        public const string UpdateOrganizationAccount = "Неможливо оновити рахунок організації";
        public const string AddNewBalanceMessage = "Неможливо зафіксувати баланс! За вказаний день проведено фінансову операцію.";
        public const string GetFinOpWithoutAccount = "Неможливо присвоїти Id організації, оскільки фінансова операція не прив'язана до акаунта";
        public const string BindingDonationToFinOp = "Неможливо прив'язати пожертву до фінансової операції";
    }
}
