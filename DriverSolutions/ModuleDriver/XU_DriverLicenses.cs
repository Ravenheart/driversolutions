using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DriverSolutions.BOL.Managers.ModuleDriver;
using DevExpress.XtraGrid.Views.Grid;
using DriverSolutions.BOL.Models.ModuleDriver;

namespace DriverSolutions.ModuleDriver
{
    public partial class XU_DriverLicenses : DevExpress.XtraEditors.XtraUserControl
    {
        public IDriverLicenseCatalogManager Manager { get; set; }

        public XU_DriverLicenses()
        {
            InitializeComponent();

            this.gridViewLicenses.RowCellClick += gridViewLicenses_RowCellClick;
        }

        public void LoadManager(IDriverLicenseCatalogManager manager)
        {
            if (manager == null)
                throw new ArgumentNullException("manager");
            this.Manager = manager;

            RefreshLicenses();
            LoadDefaults();
            BindModel();
        }

        private void LoadDefaults()
        {
            if (rep_LicenseID.DataSource == null)
            {
                var types = this.Manager.GetLicenseTypes();
                rep_LicenseID.DataSource = types;
                rep_LicenseID.DropDownRows = types.Count + 1;
            }
        }

        private void BindModel()
        {
            BindingSource bsLicenses = new BindingSource();
            bsLicenses.DataSource = this.Manager.Licenses;
            gridControlLicenses.DataSource = bsLicenses;
        }

        private void RefreshLicenses()
        {
            int topRow = gridViewLicenses.TopRowIndex;
            this.Manager.RefreshLicenses();
            gridViewLicenses.TopRowIndex = topRow;
        }

        private void gridViewLicenses_RowCellClick(object sender, RowCellClickEventArgs e)
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

        private void btnNewLicense_Click(object sender, EventArgs e)
        {
            var manager = DriverLicenseManager.CreateNew(this.Manager.Filter.DriverIDUint);
            if (XF_DriverLicenseNewEdit.ShowWindow(manager) == DialogResult.Yes)
                RefreshLicenses();
        }
    }
}
