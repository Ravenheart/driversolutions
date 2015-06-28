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
using DriverSolutions.BOL.Managers.ModuleFinance;
using DriverSolutions.DAL;

namespace DriverSolutions.ModuleFinance
{
    public partial class XF_InvoicesSelector : DevExpress.XtraEditors.XtraForm
    {
        public IInvoiceCatalogManager Manager { get; set; }

        public uint[] CompanyIDs
        {
            get
            {
                return CompanyID.GetCheckedValues();
            }
        }
        public uint[] LocationIDs
        {
            get
            {
                return LocationID.GetCheckedValues();
            }
        }
        public DateTime PeriodFrom
        {
            get
            {
                return DateFrom.DateTime.Date;
            }
        }
        public DateTime PeriodTo
        {
            get
            {
                return DateTo.DateTime.Date;
            }
        }

        public XF_InvoicesSelector(IInvoiceCatalogManager manager)
        {
            InitializeComponent();

            if (manager == null)
                throw new ArgumentNullException("manager");

            this.Manager = manager;
            this.Load += XF_InvoicesSelector_Load;
            CompanyID.EditValueChanged += CompanyID_EditValueChanged;
        }

        private void XF_InvoicesSelector_Load(object sender, EventArgs e)
        {
            var comp = this.Manager.GetCompanies();
            CompanyID.Properties.DataSource = comp;
            CompanyID.Properties.DropDownRows = comp.Count + 1;

            DateFrom.DateTime = DateTime.Now.AddDays(-7).StartOfWeek().PreviousOccurance(DayOfWeek.Sunday);
            DateTo.DateTime = DateFrom.DateTime.AddDays(6);
        }

        private void CompanyID_EditValueChanged(object sender, EventArgs e)
        {
            uint[] comp = CompanyID.GetCheckedValues();
            var loc = this.Manager.GetLocations(comp);
            LocationID.Properties.DataSource = loc;
            LocationID.Properties.DropDownRows = loc.Count + 1;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.CompanyID.GetCheckedValues().Length == 0)
            {
                Mess.Info("Please select at least one company!");
                CompanyID.ShowPopup();
                return;
            }
            if (this.LocationID.GetCheckedValues().Length == 0)
            {
                Mess.Info("Please select at least one location!");
                LocationID.ShowPopup();
                return;
            }
            if (DateFrom.EditValue == null || DateFrom.DateTime == DateTime.MinValue)
            {
                Mess.Info("Please select a Period From!");
                DateFrom.ShowPopup();
                return;
            }
            if (DateTo.EditValue == null || DateTo.DateTime == DateTime.MinValue)
            {
                Mess.Info("Please select a Period To!");
                DateTo.ShowPopup();
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}