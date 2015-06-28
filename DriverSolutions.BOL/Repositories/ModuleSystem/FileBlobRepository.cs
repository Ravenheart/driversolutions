using DriverSolutions.BOL.Models.ModuleSystem;
using DriverSolutions.DAL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;

namespace DriverSolutions.BOL.Repositories.ModuleSystem
{
    public class FileBlobRepository
    {
        public static FileBlobModel GetBlob(DSModel db, uint blobID)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            return db.FileBlobs
                .Where(b => b.BlobID == blobID)
                .Select(b => new FileBlobModel(b))
                .FirstOrDefault();
        }

        public static void SaveBlob(DSModel db, KeyBinder key, FileBlobModel model)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            if (key == null)
                throw new ArgumentNullException("key");
            if (model == null)
                throw new ArgumentNullException("model");

            model.UserID = GLOB.User.UserID;
            model.LastUpdateTime = DateTime.Now;
            if (model.BlobID == 0)
                InsertBlob(db, key, model);
            else
                UpdateBlob(db, key, model);
        }
        private static void InsertBlob(DSModel db, KeyBinder key, FileBlobModel model)
        {
            key.AddRollback(model.BlobID, model, model.GetName(p => p.BlobID));
            FileBlob poco = new FileBlob();
            model.Map(poco);
            db.Add(poco);

            key.AddKey(poco, model, model.GetName(p => p.BlobID));
        }
        private static void UpdateBlob(DSModel db, KeyBinder key, FileBlobModel model)
        {
            FileBlob poco = db.FileBlobs.Where(b => b.BlobID == model.BlobID).FirstOrDefault();
            if (poco == null)
                throw new ArgumentException("File does not exist!", "BlobID");
            model.Map(poco);
        }

        public static void SaveBlobView(DSModel db, KeyBinder key, FileBlobViewModel model)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            if (key == null)
                throw new ArgumentNullException("key");
            if (model == null)
                throw new ArgumentNullException("model");
            if (model.BlobID == 0)
                throw new ArgumentException("BlobID cannot be 0!", "model");

            string sql = @"
                UPDATE file_blobs f
                SET
                  f.BlobName = @BlobName, f.BlobDescription = @BlobDescription, f.BlobExtension = @BlobExtension
                WHERE
                  f.BlobID = @BlobID;";
            db.ExecuteNonQuery(sql,
                new MySqlParameter("BlobID", model.BlobID),
                new MySqlParameter("BlobName", model.BlobName),
                new MySqlParameter("BlobDescription", model.BlobDescription),
                new MySqlParameter("BlobExtension", model.BlobExtension));
        }

        public static void DeleteBlob(DSModel db, KeyBinder key, FileBlobModel model)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            if (key == null)
                throw new ArgumentNullException("key");
            if (model == null)
                throw new ArgumentNullException("model");

            db.FileBlobs
                .Where(b => b.BlobID == model.BlobID)
                .DeleteAll();
        }

        public static List<FileBlobViewModel> GetViewBlobsByDriver(DSModel db, uint driverID)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            string sql = @"
                SELECT f.BlobID
                     , f.BlobName
                     , f.BlobDescription
                     , f.BlobExtension
                     , concat(round((length(f.BlobData) / 1024) / 1024, 2), ' MB') AS BlobSize
                FROM
                  file_blobs f
                WHERE
                  f.DriverID = @DriverID
                ORDER BY
                  f.LastUpdateTime DESC;";

            return db.ExecuteQuery<FileBlobViewModel>(sql, new MySqlParameter("DriverID", driverID)).ToList();
        }

    }
}
