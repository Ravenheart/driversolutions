using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Models.ModuleSystem
{
    public class FileBlobModel : ModelBase
    {
        public FileBlobModel()
        {
            this.UserID = GLOB.User.UserID;
            this.LastUpdateTime = DateTime.Now;
        }

        public FileBlobModel(FileBlob poco)
        {
            this.BlobID = poco.BlobID;
            this.BlobName = poco.BlobName;
            this.BlobDescription = poco.BlobDescription;
            this.BlobExtension = poco.BlobExtension;
            this.BlobData = poco.BlobData;
            this.DriverID = poco.DriverID;
            this.UserID = poco.UserID;
            this.LastUpdateTime = poco.LastUpdateTime;
            this.IsChanged = false;
        }

        public uint BlobID { get; set; }
        public string BlobName { get; set; }
        public string BlobDescription { get; set; }
        public string BlobExtension { get; set; }
        public byte[] BlobData { get; set; }
        public uint? DriverID { get; set; }
        public uint UserID { get; set; }
        public DateTime LastUpdateTime { get; set; }

        public void Map(FileBlob poco)
        {
            poco.BlobID = this.BlobID;
            poco.BlobName = this.BlobName;
            poco.BlobDescription = this.BlobDescription;
            poco.BlobExtension = this.BlobExtension;
            poco.BlobData = this.BlobData;
            poco.DriverID = this.DriverID;
            poco.UserID = this.UserID;
            poco.LastUpdateTime = this.LastUpdateTime;
        }

        public static implicit operator FileBlobViewModel(FileBlobModel model)
        {
            return new FileBlobViewModel(model);
        }
    }
}
