using System.Threading.Tasks;

namespace FundTrack.BLL.Abstract
{
    public interface IImageManagementService
    {
        Task<string> UploadImageAsync(byte[] file, string imageExtension);
        void DeleteImageAsync(string name);
    }
}
