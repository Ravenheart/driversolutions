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
using DriverSolutions.BOL.Managers.ModuleDriver;
using DriverSolutions.DAL;
using DriverSolutions.BOL.Models.ModuleDriver;
using DevExpress.XtraGrid.Views.Grid;
using DriverSolutions.ModuleSystem;
using DriverSolutions.BOL.Core;

namespace DriverSolutions.ModuleDriver
{
    public partial class XF_DriverLicenses : DevExpress.XtraEditors.XtraForm
    {
        public IDriverLicenseCatalogManager Manager { get; set; }

        public XF_DriverLicenses(IDriverLicenseCatalogManager manager)
        {
            InitializeComponent();

            if (manager == null)
                throw new ArgumentNullException("manager");
            this.Manager = manager;

            this.Load += XF_DriverLicenses_Load;
            this.gridViewLicenses.RowCellClick += gridViewLicenses_RowCellClick;
            this.gridViewLicenses.CustomDrawCell += gridViewLicenses_CustomDrawCell;
        }

        void XF_DriverLicenses_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
                return;

            LoadDefaults();
            BindModel();
            RefreshLicenses();
        }

        private void LoadDefaults()
        {
            var drivers = this.Manager.GetDrivers();
            DriverID.Properties.DataSource = drivers;
            DriverID.Properties.DropDownRows = drivers.Count + 1;
            rep_DriverID.DataSource = drivers;

            var lic = this.Manager.GetLicenseTypes();
            LicenseID.Properties.DataSource = lic;
            LicenseID.Properties.DropDownRows = lic.Count + 1;
            rep_LicenseID.DataSource = lic;
        }
        private void BindModel()
        {
            var fil = this.Manager.Filter;
            BindingSource bsFil = new BindingSource();
            bsFil.DataSource = fil;

            DriverID.DataBindings.Clear();
            DriverID.DataBindings.Add("EditValue", bsFil, fil.GetName(p => p.DriverID), true, DataSourceUpdateMode.OnPropertyChanged);

            FirstName.DataBindings.Clear();
            FirstName.DataBindings.Add("EditValue", bsFil, fil.GetName(p => p.FirstName), true, DataSourceUpdateMode.OnPropertyChanged);

            LastName.DataBindings.Clear();
            LastName.DataBindings.Add("EditValue", bsFil, fil.GetName(p => p.LastName), true, DataSourceUpdateMode.OnPropertyChanged);

            MVRFromDate.DataBindings.Clear();
            MVRFromDate.DataBindings.Add("EditValue", bsFil, fil.GetName(p => p.MVRFromDate), true, DataSourceUpdateMode.OnPropertyChanged);

            MVRToDate.DataBindings.Clear();
            MVRToDate.DataBindings.Add("EditValue", bsFil, fil.GetName(p => p.MVRToDate), true, DataSourceUpdateMode.OnPropertyChanged);

            LicenseID.DataBindings.Clear();
            LicenseID.DataBindings.Add("EditValue", bsFil, fil.GetName(p => p.LicenseID), true, DataSourceUpdateMode.OnPropertyChanged);

            BindingSource bsLic = new BindingSource();
            bsLic.DataSource = this.Manager.Licenses;
            gridControlLicenses.DataSource = bsLic;
        }

        private void gridViewLicenses_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            var mod = gridViewLicenses.GetRow(e.RowHandle) as DriverLicenseModel;
            if (mod == null)
                return;

            DateTime now = DateTime.Now;
            DateTime month = new DateTime(now.Year, now.Month, 1);
            DateTime exp = new DateTime(mod.ExpirationDate.Year, mod.ExpirationDate.Month, 1);
            if (exp == month)
                e.Appearance.BackColor = StatusColors.Red;
            if (mod.MVRReviewDate.HasValue)
            {
                DateTime mvr = new DateTime(mod.MVRReviewDate.Value.Year, mod.MVRReviewDate.Value.Month, 1);
                if (mvr == month)
                    e.Appearance.BackColor = StatusColors.Red;
            }
        }

        private void gridViewLicenses_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            var mod = gridViewLicenses.GetRow(e.RowHandle) as DriverLicenseModel;
            if (mod == null)
                return;

            if (e.Clicks == 2 || e.Column.Name == col_Edit.Name)
            {
                HandleEdit(mod, e);
            }
            else if (e.Column.Name == col_Renew.Name)
            {
                HandleRenew(mod, e);
            }
            else if (e.Column.Name == col_Delete.Name)
            {
                HandleDelete(mod, e);
            }
        }

        private void HandleEdit(DriverLicenseModel mod, RowCellClickEventArgs e)
        {
            var manager = DriverLicenseManager.CreateEdit(mod.DriverLicenseID);
            if (XF_DriverLicenseNewEdit.ShowWindow(manager) == DialogResult.Yes)
                RefreshLicenses();
        }

        private void HandleRenew(DriverLicenseModel mod, RowCellClickEventArgs e)
        {
            var manager = DriverLicenseManager.CreateRenew(mod.DriverLicenseID);
            if (XF_DriverLicenseNewEdit.ShowWindow(manager) == DialogResult.Yes)
                RefreshLicenses();
        }

        private void HandleDelete(DriverLicenseModel mod, RowCellClickEventArgs e)
        {
            if (Mess.Question("Are you sure you wish to delete this license?", "Caution") != DialogResult.Yes)
                return;

            var manager = DriverLicenseManager.CreateEdit(mod.DriverLicenseID);
            var res = manager.Delete();
            if (res.Failed)
            {
                Mess.Error(res.Message);
            }
            else
            {
                RefreshLicenses();
            }
        }

        private void RefreshLicenses()
        {
            int topRow = gridViewLicenses.TopRowIndex;
            int focused = gridViewLicenses.FocusedRowHandle;

            this.Manager.RefreshLicenses();

            gridViewLicenses.TopRowIndex = topRow;
            gridViewLicenses.FocusedRowHandle = focused;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;
            btnClear.Enabled = false;

            RefreshLicenses();

            btnSearch.Enabled = true;
            btnClear.Enabled = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Manager.ClearFilter();
            RefreshLicenses();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNewLicense_Click(object sender, EventArgs e)
        {
            var manager = DriverCatalogManager.Create(true);
            using (XF_DriverFinder find = new XF_DriverFinder(manager))
            {
                if (find.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                {
                    var dmanager = DriverLicenseManager.CreateNew(manager.ActiveDriver.DriverID);
                    if (XF_DriverLicenseNewEdit.ShowWindow(dmanager) == DialogResult.Yes)
                        RefreshLicenses();
                }
            }
        }

        public static DialogResult ShowWindow(uint driverID = 0)
        {
            var manager = DriverLicenseCatalogManager.Create(driverID);
            using (XF_DriverLicenses form = new XF_DriverLicenses(manager))
                return form.ShowDialog();
        }
    }
}