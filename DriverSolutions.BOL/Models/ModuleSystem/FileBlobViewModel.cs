using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Models.ModuleSystem
{
    public class FileBlobViewModel : ModelBase
    {
        public FileBlobViewModel() { }

        public FileBlobViewModel(FileBlob poco) : this(new FileBlobModel(poco)) { }

        public FileBlobViewModel(FileBlobModel model)
        {
            this.BlobID = model.BlobID;
            this.BlobName = model.BlobName;
            this.BlobDescription = model.BlobDescription;
            this.BlobExtension = model.BlobExtension;
            this.BlobSize = ((model.BlobData.Length / 1024) / 1024) + " MB";
        }

        public uint BlobID { get; set; }
        public string BlobName { get; set; }
        public string BlobDescription { get; set; }
        public string BlobExtension { get; set; }
        public string BlobSize { get; set; }
    }
}
