namespace FundTrack.Infrastructure
{
    /// <summary>
    /// Class with regex validation messages 
    /// </summary>
    public static class RegexValidationMessages
    {
        public const string LoginRegexValidationMessage = "^[a-zA-Z](.[a-zA-Z0-9_-]*)$";
        public const string EmailRegexValidationMessage = "^([a-z0-9_-]+\\.)*[a-z0-9_-]+@[a-z0-9_-]+(\\.[a-z0-9_-]+)*\\.[a-z]{2,6}$";
    }
}
