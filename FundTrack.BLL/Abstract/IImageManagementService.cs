using System.Threading.Tasks;

namespace FundTrack.BLL.Abstract
{
    public interface IImageManagementService
    {
        Task<string> UploadImageAsync(byte[] file, string imageExtension);
        Task DeleteImageAsync(string name);
    }
}
