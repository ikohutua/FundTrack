namespace PrivatService
{
    public class Constants
    {
        public const string ConnectionStringName = "azure-main";
        public const string AllBankAccounts =
            @"SELECT * FROM BankAccounts WHERE OrgId = @orgId 
                AND ExtractMerchantId IS NOT NULL 
                AND ExtractMerchantPassword IS NOT NULL 
                AND IsExtractEnabled = 1  ";

        public const string AllAppCodes =
            @"SELECT AppCode
                FROM BankImportDetails";

        public const string InsertBankImport =
            @"INSERT INTO BankImportDetails
                (Amount,AppCode,Card,CardAmount,Description,Rest,Terminal,Trandate,IsLooked)
            VALUES(@amount,@AppCode,@Card,@CardAmount,@Description,@Rest,@Terminal,@Trandate,@IsLooked)";
    }
}
