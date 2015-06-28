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
using DriverSolutions.BOL.Models.ModuleSystem;
using DriverSolutions.BOL.Managers.ModuleSystem;
using DevExpress.XtraGrid.Views.Base;
using DriverSolutions.BOL.Core;
using DriverSolutions.BOL.Managers.ModuleDriver;

namespace DriverSolutions.ModuleSystem
{
    public partial class XF_DriverFinder : DevExpress.XtraEditors.XtraForm
    {
        public IDriverCatalogManager Manager { get; set; }

        public XF_DriverFinder(IDriverCatalogManager manager)
        {
            InitializeComponent();

            if (manager == null)
                throw new ArgumentNullException("manager");
            this.Manager = manager;

            this.Load += XF_DriverFinder_Load;
            gridViewDrivers.RowCellClick += gridViewDrivers_RowCellClick;
            gridViewDrivers.CustomDrawCell += gridViewDrivers_CustomDrawCell;
        }

        private void XF_DriverFinder_Load(object sender, EventArgs e)
        {
            if (!this.Manager.IsFinder)
            {
                laySelect.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                menuNewDriver.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                col_Edit.Visible = false;
            }

            this.Manager.RefreshDrivers();
            LoadDefaults();
            BindModel();

            DriverCode.Select();
        }

        private void LoadDefaults()
        {
            var lic = this.Manager.GetLicenses();
            LicenseID.Properties.DataSource = lic;
            LicenseID.Properties.DropDownRows = lic.Count + 1;
            rep_License.DataSource = lic;

            var permit = this.Manager.GetPermits();
            PermitID.Properties.DataSource = permit;
            PermitID.Properties.DropDownRows = permit.Count + 1;
        }

        private void BindModel()
        {
            var fil = this.Manager.Filter;
            BindingSource bsFil = new BindingSource();
            bsFil.DataSource = fil;

            DriverCode.DataBindings.Clear();
            DriverCode.DataBindings.Add("EditValue", bsFil, fil.GetName(p => p.DriverCode), true, DataSourceUpdateMode.OnPropertyChanged);

            FirstName.DataBindings.Clear();
            FirstName.DataBindings.Add("EditValue", bsFil, fil.GetName(p => p.FirstName), true, DataSourceUpdateMode.OnPropertyChanged);

            SecondName.DataBindings.Clear();
            SecondName.DataBindings.Add("EditValue", bsFil, fil.GetName(p => p.SecondName), true, DataSourceUpdateMode.OnPropertyChanged);

            LastName.DataBindings.Clear();
            LastName.DataBindings.Add("EditValue", bsFil, fil.GetName(p => p.LastName), true, DataSourceUpdateMode.OnPropertyChanged);

            CellPhone.DataBindings.Clear();
            CellPhone.DataBindings.Add("EditValue", bsFil, fil.GetName(p => p.CellPhone), true, DataSourceUpdateMode.OnPropertyChanged);

            Email.DataBindings.Clear();
            Email.DataBindings.Add("EditValue", bsFil, fil.GetName(p => p.Email), true, DataSourceUpdateMode.OnPropertyChanged);

            LicenseID.DataBindings.Clear();
            LicenseID.DataBindings.Add("EditValue", bsFil, fil.GetName(p => p.LicenseID), true, DataSourceUpdateMode.OnPropertyChanged);

            PermitID.DataBindings.Clear();
            PermitID.DataBindings.Add("EditValue", bsFil, fil.GetName(p => p.PermitID), true, DataSourceUpdateMode.OnPropertyChanged);

            IncludeDisabled.DataBindings.Clear();
            IncludeDisabled.DataBindings.Add("Checked", bsFil, fil.GetName(p => p.IncludeDisabled), true, DataSourceUpdateMode.OnPropertyChanged);

            BindingSource bsDrivers = new BindingSource();
            bsDrivers.DataSource = this.Manager.Drivers;
            gridControlDrivers.DataSource = bsDrivers;
        }

        private void RefreshDrivers()
        {
            int topRow = gridViewDrivers.TopRowIndex;
            this.Manager.RefreshDrivers();
            gridViewDrivers.TopRowIndex = topRow;
        }

        private void gridViewDrivers_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            var row = gridViewDrivers.GetRow(e.RowHandle) as DriverModel;
            if (row == null)
                return;

            if (row.LicenseID == 0)
                e.Appearance.BackColor = StatusColors.Red;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                btnSearch_Click(this, EventArgs.Empty);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;
            btnClear.Enabled = false;

            RefreshDrivers();

            btnSearch.Enabled = true;
            btnClear.Enabled = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Manager.ClearFilter();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            var row = gridViewDrivers.GetFocusedRow() as DriverModel;
            if (row == null)
            {
                Mess.Info("Please select a driver!");
                return;
            }

            this.Manager.ActiveDriver = row;
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Close();
        }

        void gridViewDrivers_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            var row = gridViewDrivers.GetRow(e.RowHandle) as DriverModel;
            if (row == null)
                return;

            if (e.Clicks == 2)
            {
                if (this.Manager.IsFinder)
                {
                    this.Manager.ActiveDriver = row;
                    this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                    this.Close();
                }
                else
                {
                    var manager = DriverManager.CreateEdit(row.DriverID);
                    using (XF_DriverNewEdit form = new XF_DriverNewEdit(manager))
                    {
                        form.ShowDialog();
                        RefreshDrivers();
                    }
                }
            }
            else if (e.Column.Name == col_Edit.Name)
            {
                var manager = DriverManager.CreateEdit(row.DriverID);
                using (XF_DriverNewEdit form = new XF_DriverNewEdit(manager))
                {
                    form.ShowDialog();
                    RefreshDrivers();
                }
            }
        }

        private void menuNewDriver_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var manager = DriverManager.CreateNew();
            using (XF_DriverNewEdit form = new XF_DriverNewEdit(manager))
            {
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                {
                    int index = gridViewDrivers.TopRowIndex;
                    btnSearch_Click(this, EventArgs.Empty);
                    gridViewDrivers.TopRowIndex = index;
                }
            }
        }
    }
}