using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundTrack.BLL.Abstract
{
    public interface IImageManagementService
    {
        Task<string> UploadImage(byte[] file);
    }
}
