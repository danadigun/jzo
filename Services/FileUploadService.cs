using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jzo.Services
{
    public class FileUploadService : IUpload
    {      
        Task<bool> IUpload.uploadGroupImage(int? group_Id)
        {
            throw new NotImplementedException();
        }

        Task<bool> IUpload.uploadItemImage(int? item_Id)
        {
            throw new NotImplementedException();
        }
    }
}
