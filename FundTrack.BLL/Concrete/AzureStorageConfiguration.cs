namespace FundTrack.BLL.Concrete
{
   public static class AzureStorageConfiguration
    {
        private static string _baseUrl = "https://fundrackss.blob.core.windows.net/fundtrackssimages/";

        public static string GetImageUrl(string imageName)
        {
            return _baseUrl + imageName;
        }
    }
}
