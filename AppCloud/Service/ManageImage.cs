using Microsoft.AspNetCore.StaticFiles;
using static Azure.Core.HttpHeader;
using Microsoft.AspNetCore.StaticFiles;
using AppCloud.Helper;

namespace AppCloud.Service
{
    public class ManageImage : IManageImage
    {
        public async Task<string> UploadFile(IFormFile _IFormFile, FileModelTemporary filedata)
        {
            string FileName = "" ;
            
            

            try
            {   
                
                    FileInfo _FileInfo = new FileInfo(_IFormFile.FileName);
                if (FileName != "Defult") { FileName = filedata.Name + "_" + DateTime.Now.Ticks.ToString() + _FileInfo.Extension; }
                else { FileName = Path.GetFileNameWithoutExtension(_IFormFile.FileName) + "_" + DateTime.Now.Ticks.ToString() + _FileInfo.Extension; }
                    var _GetFilePath = Helper.Common.GetFilePath(FileName);
                    using (var _FileStream = new FileStream(_GetFilePath, FileMode.Create))
                    {
                        await _IFormFile.CopyToAsync(_FileStream);
                    }
                    return FileName;
                
            
         
            }   
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<(byte[], string, string)> DownloadFile(string FileName)
        {
            try
            {
                var _GetFilePath = Helper.Common.GetFilePath(FileName);
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(_GetFilePath, out var _ContentType))
                {
                    _ContentType = "application/octet-stream";
                }
                var _ReadAllBytesAsync = await File.ReadAllBytesAsync(_GetFilePath);
                return (_ReadAllBytesAsync, _ContentType, Path.GetFileName(_GetFilePath));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
