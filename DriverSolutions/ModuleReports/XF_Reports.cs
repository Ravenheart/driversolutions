using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DriverSolutions.DAL;
using DriverSolutions.BOL.Repositories.ModuleSystem;
using DriverSolutions.BOL.Repositories.ModuleDispatches;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraReports.UI;
using System.IO;
using DriverSolutions.BOL.Repositories.ModuleReports;
using DriverSolutions.Core;
using DriverSolutions.BOL;
using DriverSolutions.BOL.Models.ModuleDriver;

namespace DriverSolutions.ModuleReports
{
    public partial class XF_Reports : DevExpress.XtraEditors.XtraForm
    {
        private DSModel DbContext;

        public XF_Reports()
        {
            InitializeComponent();

            this.Load += XF_Reports_Load;
            CompanyID.EditValueChanging += CompanyID_EditValueChanging;
        }

        void CompanyID_EditValueChanging(object sender, ChangingEventArgs e)
        {
            CompanyID.EditValueChanging -= CompanyID_EditValueChanging;
            if (CompanyID.EditValue != null)
            {
                uint[] ids = CompanyID.GetCheckedValues(e.NewValue);
                var locs = LocationRepository.GetLocationsByCompanies(this.DbContext, ids);
                LocationID.Properties.DataSource = locs;
                LocationID.Properties.DropDownRows = locs.Count + 1;
            }
            CompanyID.EditValueChanging += CompanyID_EditValueChanging;
        }

        void XF_Reports_Load(object sender, EventArgs e)
        {
            if (this.DesignMode) return;

            this.DbContext = DB.GetContext();

            if (GLOB.Settings.Get<bool>(1) == true)
            {
                FromDate.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
                ToDate.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
            }

            FromDate.DateTime = DateTime.Now.AddDays(-7).StartOfWeek().PreviousOccurance(DayOfWeek.Sunday);
            ToDate.DateTime = FromDate.DateTime.AddDays(6);

            LoadDefaults();
        }

        private void LoadDefaults()
        {
            DriverFilterModel filter = new DriverFilterModel();
            filter.IncludeDisabled = false;
            var drivers = DriverRepository.FindDrivers(this.DbContext, filter);
            DriverID.Properties.DataSource = drivers;
            DriverID.Properties.DropDownRows = drivers.Count + 1;

            var companies = CompanyRepository.FindCompanies(this.DbContext);
            CompanyID.Properties.DataSource = companies;
            CompanyID.Properties.DropDownRows = companies.Count + 1;

            var reports = ReportRepository.GetReports(this.DbContext);
            ReportID.Properties.DataSource = reports;
            ReportID.Properties.DropDownRows = reports.Count + 1;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (ReportID.EditValue == null || ReportID.EditValue.ToString() == "0")
            {
                Mess.Info("Please select a report!");
                ReportID.ShowPopup();
                return;
            }

            using (var db = DB.GetContext())
            {
                uint reportID = Convert.ToUInt32(ReportID.EditValue);
                uint[] companies = CompanyID.GetCheckedValues();
                uint[] locations = LocationID.GetCheckedValues();
                uint[] drivers = DriverID.GetCheckedValues();
                DateTime fromDt = FromDate.DateTime;
                DateTime toDt = ToDate.DateTime;

                var report = ReportRepository.GenerateReport(db, reportID, companies, locations, drivers, fromDt, toDt);
                //report.DataSource.WriteXmlSchema("D:\\schema.xml");
                ReportBinder.ShowReport(report);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}