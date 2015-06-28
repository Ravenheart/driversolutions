using AutoMapper;
using DriverSolutions.BOL.Core;
using DriverSolutions.BOL.Models.ModuleDriver;
using DriverSolutions.BOL.Models.ModuleSystem;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions
{
    public static class GLOB
    {
        static GLOB()
        {
            using (var db = DB.GetContext())
            {
                var poco = db.CompanyInfos.FirstOrDefault();
                GLOB.Company = new CompanyInfoModel(poco);
            }
        }

        public static class Formats
        {
            public const string Date = "d";
            public const string DateTime = "g";
        }

        public static class Settings
        {
            private static Dictionary<uint, string> _Settings = new Dictionary<uint, string>();
            private static Dictionary<Type, TypeConverter> _TypeConverters = new Dictionary<Type, TypeConverter>();

            public static T Get<T>(uint settingID)
            {
                var type = typeof(T);
                if (!_Settings.ContainsKey(settingID))
                {
                    using (var db = DB.GetContext())
                    {
                        var set = db.Settings.Where(s => s.SettingID == settingID).FirstOrDefault();
                        if (set == null)
                            throw new ArgumentException("Missing Settings in the database!");

                        _Settings.Add(set.SettingID, set.SettingValue);
                    }
                }

                if (!_TypeConverters.ContainsKey(typeof(T)))
                {
                    var converter = TypeDescriptor.GetConverter(type);
                    if (converter == null)
                        throw new ArgumentException("No TypeConverted for type of " + type.ToString());
                    _TypeConverters.Add(type, converter);
                }

                return (T)(_TypeConverters[type].ConvertFromInvariantString(_Settings[settingID]));
            }
        }

        public static UserModel User { get; set; }
        public static CompanyInfoModel Company { get; set; }

        public static void ConfigureMap()
        {
            Mapper.CreateMap<DriverLicenseFilterModel, DriverLicenseFilterModel>();
        }
    }
}
