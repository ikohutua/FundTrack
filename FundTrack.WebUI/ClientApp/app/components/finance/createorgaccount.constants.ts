export class CreateOrgAccountConstants {
    public static readonly DefaultTargetName: string = "Не вибрано"; 
    public static readonly requiredMessage: string = "*Поле є обов'язковим для заповнення";
    public static readonly numberMessage: string = "*Поле повинно містити тільки цифри";
    public static readonly LengthMessage: string = "*Недопустима кількість символів";
    public static readonly accountNameWrongLength: string = "*Максимально допустима довжина імені рахунку складає 100 символів";
    public static readonly edrpoulength: string = "*ЄДРПОУ повинно містити 8 цифр";
    public static readonly descriptionlength: string = "*Опис рахунку не може перевищувати 100 символів";
    public static readonly currencyselection: string =  "*Необхідно вибрати валюту";
    public static readonly wrongBalanceMessage: string = "*Баланс повинен бути у форматі 1 або 1.23";

}