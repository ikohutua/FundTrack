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
        public const string MoneyFinOpLimit = "Сума повинна бути більшою 5 і меншою 15000";
        public const string CantFindDataById = "Can't find data with requested id";
        public const string GetOrganizationAccount = "Неможливо отримати рахунок організації";
        public const string UpdateOrganizationAccount = "Неможливо оновити рахунок організації";
        public const string OperationIncomeError = "Неможливо виконати фінансову операцію \"Прихід\"";
        public const string OperationSpendingError = "Неможливо виконати фінансову операцію \"Розхід\"";
        public const string OperationTransferError = "Неможливо виконати фінансову операцію \"Переміщення\"";
        public const string EmptyFinOpList = "Список фінансових операцій порожній";
        public const string InvalidIdentificator = "Невірне значення ідентифікатора";
        public const string SpendingIsExceeded = "Витрати не можуть перебільшувати баланс рахунку";
        public const string InvalidFinanceOperation = "Непраильна фінансова операція";
        public const string AddNewBalanceMessageReject = "Неможливо зафіксувати баланс через наявність фінансових операцій на вказану дату або після неї!";
        public const string BalanceFixedSuccessfullyMessage = "Баланс зафіксовано!";
        public const string BalanceAlreadyFixedMessage = "Баланс вже зафіксовано!";
        public const string GetFinOpWithoutAccount = "Неможливо присвоїти Id організації, оскільки фінансова операція не прив'язана до акаунта";
        public const string CantCreateAccountWithundefinedType = "Неможливо створити рахунок невизначеного типу";
        public const string CantCreateAccountOfOrganization = "Неможливо створити рахунок організацї";
        public const string YouArentAdminOfThisOrganization = "Ви не адміністратор цієї організації";
        public const string WrongAdminPasswond = "Невірний пароль адміністратора організації";
        public const string OrganizarionAccountWithNameExists = "Рахунок організації з таким іменем уже існує";
        public const string OrganizarionAccountWithNumberExists = "Рахунок з таким номером уже зареєстрований";
        public const string CantGetInfoForAccount = "Неможливо отримати інформацію для рахунку";
        public const string CantEditInfoForAccount = "Неможливо змінити інформацію для рахунку";
        public const string BindingDonationToFinOp = "Неможливо прив'язати пожертву до фінансової операції";
        public const string OrganizationNotFound = "Організацію не знайдено";
        public const string InvalidData = "Невірні дані!";
        public const string CantGetFinOpsForReport = "Неможливо отримати фінансові операції для звіту";
        public const string CantGetImages = "Error while getting image path list from FinOPImages entities by finOpId";
        //report controller
        public const string DateFromErrorMessage = "Невірна або пуста початкова дата.";
        public const string DateToErrorMessage = "Невірна або пуста кінцева дата.";
        public const string FinopImagesIdErrorMessage = "Невірнийб менший за 0 або пустий ідентифікатор операції.";
        public const string CheckIdErrorMessage = "Невірний, менший за 0 або пустий ідентифікатор.";

    }
}
