using DriverSolutions.BOL.Models.ModuleSystem;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Repositories.ModuleSystem
{
    public class ContactRepository
    {
        public static List<ContactModel> GetContacts(DSModel db, params uint[] contactIDs)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            if (contactIDs.Length == 0)
                return new List<ContactModel>();

            string sql = @"
                SELECT
                  c.*,
                  ToBool(0) AS IsChanged
                FROM contacts c
                WHERE c.ContactID IN (@ContactID);";
            sql = sql.Replace("@ContactID", string.Join<uint>(",", contactIDs));
            return db.ExecuteQuery<ContactModel>(sql).ToList();
        }

        public static ContactModel GetContact(DSModel db, uint contactID)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            return ContactRepository.GetContacts(db, contactID).FirstOrDefault();

            //var poco = db.Contacts
            //    .Where(c => c.ContactID == contactID)
            //    .FirstOrDefault();
            //if (poco == null)
            //    return null;

            //var mod = new ContactModel();
            //mod.ContactID = poco.ContactID;
            //mod.ContactName = poco.ContactName;
            //mod.ContactPhone = poco.ContactPhone;
            //mod.ContactEmail = poco.ContactEmail;
            //mod.IsChanged = false;

            //return mod;
        }

        public static Contact SaveContact(DSModel db, KeyBinder key, ContactModel model)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            if (key == null)
                throw new ArgumentNullException("key");
            if (model == null)
                throw new ArgumentNullException("model");

            if (model.ContactID == 0)
                return InsertContact(db, key, model);
            else
                return UpdateContact(db, key, model);
        }

        private static Contact InsertContact(DSModel db, KeyBinder key, ContactModel model)
        {
            Contact poco = new Contact();
            poco.ContactName = model.ContactName;
            poco.ContactPhone = model.ContactPhone;
            poco.ContactEmail = model.ContactEmail;
            db.Add(poco);
            key.AddKey(poco, model, model.GetName(p => p.ContactID));
            return poco;
        }

        private static Contact UpdateContact(DSModel db, KeyBinder key, ContactModel model)
        {
            var poco = db.Contacts.Where(c => c.ContactID == model.ContactID).FirstOrDefault();
            if (poco == null)
                throw new ArgumentException("No contact with the specified ID!");

            poco.ContactName = model.ContactName;
            poco.ContactPhone = model.ContactPhone;
            poco.ContactEmail = model.ContactEmail;
            return poco;
        }
    }
}
