using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jzo.Services
{
    public interface IUpload
    {
        Task<bool> uploadGroupImage(int? group_Id);
        Task<bool> uploadItemImage(int? item_Id);
    }
}
