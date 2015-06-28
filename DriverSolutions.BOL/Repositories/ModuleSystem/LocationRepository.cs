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
    public class LocationRepository
    {
        public static List<LocationModel> GetLocations(DSModel db, uint companyID)
        {
            return LocationRepository.GetLocations(db, new uint[] { companyID });
        }

        public static List<LocationModel> GetLocations(DSModel db, uint[] companies = null)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            var query = PredicateBuilder.True<Location>();
            if (companies != null && companies.Length > 0)
                query = query.And(q => companies.Contains(q.CompanyID));

            return db.Locations
                .Where(query)
                .Select(l => new LocationModel()
                {
                    LocationID = l.LocationID,
                    LocationName = l.LocationName,
                    LocationCode = l.LocationCode,
                    LocationAddress = l.LocationAddress,
                    LocationPhone = l.LocationPhone,
                    LocationFax = l.LocationFax,
                    ConfirmationContactID = l.ConfirmationContactID,
                    ConfirmationContact = (l.ConfirmationContactID.HasValue ? ContactRepository.GetContact(db, l.ConfirmationContactID.Value) : new ContactModel()),
                    InvoiceContactID = l.InvoiceContactID,
                    InvoiceContact = (l.InvoiceContactID.HasValue ? ContactRepository.GetContact(db, l.InvoiceContactID.Value) : new ContactModel()),
                    DispatchContactID = l.DispatchContactID,
                    DispatchContact = (l.DispatchContactID.HasValue ? ContactRepository.GetContact(db, l.DispatchContactID.Value) : new ContactModel()),
                    CompanyID = l.CompanyID,
                    TravelPay = l.TravelPay,
                    TravelPayName = l.TravelPayName,
                    LunchTime = l.LunchTime,
                    IsEnabled = l.IsEnabled,
                    IncludeConfirmation = l.IncludeConfirmation,
                    IsChanged = false
                })
                .OrderBy(l => l.LocationName)
                .ToList();
        }

        public static LocationModel GetLocation(DSModel db, uint locationID)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            var poco = db.Locations.Where(l => l.LocationID == locationID).FirstOrDefault();
            if (poco == null)
                return null;

            LocationModel mod = new LocationModel();
            mod.LocationID = poco.LocationID;
            mod.LocationName = poco.LocationName;
            mod.LocationCode = poco.LocationCode;
            mod.LocationAddress = poco.LocationAddress;
            mod.LocationPhone = poco.LocationPhone;
            mod.LocationFax = poco.LocationFax;
            mod.ConfirmationContactID = poco.ConfirmationContactID;
            mod.ConfirmationContact = (poco.ConfirmationContactID.HasValue ? ContactRepository.GetContact(db, poco.ConfirmationContactID.Value) : new ContactModel());
            mod.InvoiceContactID = poco.InvoiceContactID;
            mod.InvoiceContact = (poco.InvoiceContactID.HasValue ? ContactRepository.GetContact(db, poco.InvoiceContactID.Value) : new ContactModel());
            mod.DispatchContactID = poco.DispatchContactID;
            mod.DispatchContact = (poco.DispatchContactID.HasValue ? ContactRepository.GetContact(db, poco.DispatchContactID.Value) : new ContactModel());
            mod.CompanyID = poco.CompanyID;
            mod.TravelPay = poco.TravelPay;
            mod.TravelPayName = poco.TravelPayName;
            mod.LunchTime = poco.LunchTime;
            mod.IsEnabled = poco.IsEnabled;
            mod.IncludeConfirmation = poco.IncludeConfirmation;
            mod.IsChanged = false;

            return mod;
        }

        public static LocationModel GetLocation(DSModel db, string locationCode)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            uint locID = db.Locations.Where(l => l.LocationCode == locationCode).Select(l => l.LocationID).FirstOrDefault();
            if (locID == 0)
                return null;

            return LocationRepository.GetLocation(db, locID);
        }

        public static string PeekLocationCode(DSModel db, string prefix)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            return db.ExecuteScalar<string>("SELECT PeekLocationCode(@Prefix)", new MySqlParameter("Prefix", prefix));
        }

        public static Location SaveLocation(DSModel db, KeyBinder key, LocationModel model, Company company = null)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            if (key == null)
                throw new ArgumentNullException("key");
            if (model == null)
                throw new ArgumentNullException("model");

            if (model.LocationID == 0)
                return InsertLocation(db, key, model, company);
            else
                return UpdateLocation(db, key, model, company);
        }

        private static Location InsertLocation(DSModel db, KeyBinder key, LocationModel model, Company company = null)
        {
            Location poco = new Location();
            poco.LocationName = model.LocationName;
            if (model.LocationCode == string.Empty)
                poco.LocationCode = "L" + LocationRepository.PeekLocationCode(db, "L");
            else
                poco.LocationCode = model.LocationCode;

            if (company == null)
                poco.CompanyID = model.CompanyID;
            else
                poco.Company = company;

            poco.LocationAddress = model.LocationAddress;
            poco.LocationPhone = model.LocationPhone;
            poco.LocationFax = model.LocationFax;

            if (model.ConfirmationContact.IsChanged)
            {
                poco.ConfirmationContact = ContactRepository.SaveContact(db, key, model.ConfirmationContact);
                key.AddKey(poco.ConfirmationContact, model.ConfirmationContact, poco.ConfirmationContact.GetName(p => p.ContactID));
                key.AddKey(poco.ConfirmationContact, model, poco.ConfirmationContact.GetName(p => p.ContactID), model.GetName(p => p.ConfirmationContactID));
            }
            if (model.InvoiceContact.IsChanged)
            {
                poco.InvoiceContact = ContactRepository.SaveContact(db, key, model.InvoiceContact);
                key.AddKey(poco.InvoiceContact, model.InvoiceContact, poco.InvoiceContact.GetName(p => p.ContactID));
                key.AddKey(poco.InvoiceContact, model, poco.InvoiceContact.GetName(p => p.ContactID), model.GetName(p => p.InvoiceContactID));
            }
            if (model.DispatchContact.IsChanged)
            {
                poco.DispatchContact = ContactRepository.SaveContact(db, key, model.DispatchContact);
                key.AddKey(poco.DispatchContact, model.DispatchContact, poco.DispatchContact.GetName(p => p.ContactID));
                key.AddKey(poco.DispatchContact, model, poco.DispatchContact.GetName(p => p.ContactID), model.GetName(p => p.DispatchContactID));
            }

            poco.TravelPay = model.TravelPay;
            poco.TravelPayName = model.TravelPayName;
            poco.LunchTime = model.LunchTime;
            poco.IsEnabled = model.IsEnabled;
            poco.IncludeConfirmation = model.IncludeConfirmation;
            db.Add(poco);
            key.AddKey(poco, model, model.GetName(p => p.LocationID));
            return poco;
        }

        private static Location UpdateLocation(DSModel db, KeyBinder key, LocationModel model, Company company = null)
        {
            var poco = db.Locations.Where(l => l.LocationID == model.LocationID).FirstOrDefault();
            if (poco == null)
                throw new ArgumentException("No Location with the specified ID!");

            poco.LocationName = model.LocationName;
            poco.LocationCode = model.LocationCode;

            if (company == null)
                poco.CompanyID = model.CompanyID;
            else
                poco.Company = company;

            poco.LocationAddress = model.LocationAddress;
            poco.LocationPhone = model.LocationPhone;
            poco.LocationFax = model.LocationFax;

            if (model.ConfirmationContact.IsChanged)
            {
                poco.ConfirmationContact = ContactRepository.SaveContact(db, key, model.ConfirmationContact);
                key.AddKey(poco.ConfirmationContact, model.ConfirmationContact, poco.ConfirmationContact.GetName(p => p.ContactID));
                key.AddKey(poco.ConfirmationContact, model, poco.ConfirmationContact.GetName(p => p.ContactID), model.GetName(p => p.ConfirmationContactID));
            }
            if (model.InvoiceContact.IsChanged)
            {
                poco.InvoiceContact = ContactRepository.SaveContact(db, key, model.InvoiceContact);
                key.AddKey(poco.InvoiceContact, model.InvoiceContact, poco.InvoiceContact.GetName(p => p.ContactID));
                key.AddKey(poco.InvoiceContact, model, poco.InvoiceContact.GetName(p => p.ContactID), model.GetName(p => p.InvoiceContactID));
            }
            if (model.DispatchContact.IsChanged)
            {
                poco.DispatchContact = ContactRepository.SaveContact(db, key, model.DispatchContact);
                key.AddKey(poco.DispatchContact, model.DispatchContact, poco.DispatchContact.GetName(p => p.ContactID));
                key.AddKey(poco.DispatchContact, model, poco.DispatchContact.GetName(p => p.ContactID), model.GetName(p => p.DispatchContactID));
            }

            poco.TravelPay = model.TravelPay;
            poco.TravelPayName = model.TravelPayName;
            poco.LunchTime = model.LunchTime;
            poco.IsEnabled = model.IsEnabled;
            poco.IncludeConfirmation = model.IncludeConfirmation;
            return poco;
        }

        public static void DeleteLocation(DSModel db, LocationModel model)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            if (model.LocationID != 0)
            {
                var poco = db.Locations.Where(l => l.LocationID == model.LocationID).FirstOrDefault();
                if (poco != null)
                {
                    if (poco.ConfirmationContactID.HasValue)
                        db.Delete(poco.ConfirmationContact);
                    if (poco.InvoiceContactID.HasValue)
                        db.Delete(poco.InvoiceContact);
                    if (poco.DispatchContactID.HasValue)
                        db.Delete(poco.DispatchContact);
                    db.Delete(poco);
                }
            }
        }

        public static int GetLunchTime(DSModel db, uint locationID)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            return db.Locations
                .Where(l => l.LocationID == locationID)
                .Select(l => l.LunchTime)
                .FirstOrDefault();
        }
    }
}
