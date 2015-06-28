using DriverSolutions.BOL.Models.ModuleSystem;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Managers.ModuleSystem
{
    public interface IFileBlobManager
    {
        FileBlobCreationInfoModel CreationInfo { get; set; }
        FileBlobModel ActiveModel { get; set; }
        BindingList<FileBlobViewModel> ActiveFiles { get; set; }

        void RefreshFileList();

        CheckResult SaveFile(FileBlobModel model);
        CheckResult SaveFile(FileBlobViewModel model);
        CheckResult DeleteFile(FileBlobModel model);
        CheckResult DeleteFile(FileBlobViewModel model);

        CheckResult PreviewFile(FileBlobViewModel model);
        CheckResult AttachMultipleFiles(string[] files);
    }
}
