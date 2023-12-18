
namespace AppCloud.Service
{
    public interface IManageImage
    {
        Task<string> UploadFile(IFormFile _IFormFile, FileModelTemporary filedata);
        Task<(byte[], string, string)> DownloadFile(string FileName);
    }
}
