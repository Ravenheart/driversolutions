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
using DriverSolutions.BOL.Managers.ModuleSystem;
using DriverSolutions.BOL.Models.ModuleSystem;
using DriverSolutions.DAL;
using DevExpress.XtraEditors.Repository;
using DriverSolutions.BOL.Managers.ModuleDriver;

namespace DriverSolutions.ModuleSystem
{
    public partial class XF_DriverNewEdit : DevExpress.XtraEditors.XtraForm
    {
        public IDriverManager Manager { get; set; }

        public XF_DriverNewEdit(IDriverManager manager)
        {
            InitializeComponent();

            if (manager == null)
                throw new ArgumentNullException("manager");

            this.Manager = manager;
            this.Load += XF_DriverNewEdit_Load;
            gridViewLocations.InitNewRow += gridViewLocations_InitNewRow;
            gridViewLocations.RowCellClick += gridViewLocations_RowCellClick;
            gridViewLocations.CustomRowCellEditForEditing += gridViewLocations_CustomRowCellEditForEditing;
        }

        private void XF_DriverNewEdit_Load(object sender, EventArgs e)
        {
            LoadDefaults();

            if (this.Manager.ActiveModel.DriverID == 0)
            {
                tabLicenses.PageEnabled = false;
                tabLocations.PageEnabled = false;
                tabFiles.PageEnabled = false;
            }

            BindModel(this.Manager.ActiveModel);
        }

        private void LoadDefaults()
        {
            var locs = this.Manager.GetLocations();
            rep_Location.DataSource = locs;
            rep_Location.DropDownRows = locs.Count + 1;

            var com = this.Manager.GetCompanies();
            rep_Company.DataSource = com;
            rep_Company.DropDownRows = com.Count + 1;

            var lic = this.Manager.GetLicenses();
            LicenseID.Properties.DataSource = lic;
            LicenseID.Properties.DropDownRows = lic.Count + 1;
        }

        private void BindModel(DriverModel mod)
        {
            BindingSource bsModel = new BindingSource();
            bsModel.DataSource = mod;

            DriverCode.DataBindings.Add("EditValue", bsModel, mod.GetName(p => p.DriverCode), true, DataSourceUpdateMode.OnPropertyChanged);
            FirstName.DataBindings.Add("EditValue", bsModel, mod.GetName(p => p.FirstName), true, DataSourceUpdateMode.OnPropertyChanged);
            SecondName.DataBindings.Add("EditValue", bsModel, mod.GetName(p => p.SecondName), true, DataSourceUpdateMode.OnPropertyChanged);
            LastName.DataBindings.Add("EditValue", bsModel, mod.GetName(p => p.LastName), true, DataSourceUpdateMode.OnPropertyChanged);
            DateOfBirth.DataBindings.Add("EditValue", bsModel, mod.GetName(p => p.DateOfBirth), true, DataSourceUpdateMode.OnPropertyChanged);
            DateOfHire.DataBindings.Add("EditValue", bsModel, mod.GetName(p => p.DateOfHire), true, DataSourceUpdateMode.OnPropertyChanged);
            CellPhone.DataBindings.Add("EditValue", bsModel, mod.GetName(p => p.CellPhone), true, DataSourceUpdateMode.OnPropertyChanged);
            EmergencyPhone.DataBindings.Add("EditValue", bsModel, mod.GetName(p => p.EmergencyPhone), true, DataSourceUpdateMode.OnPropertyChanged);
            Email.DataBindings.Add("EditValue", bsModel, mod.GetName(p => p.Email), true, DataSourceUpdateMode.OnPropertyChanged);
            //LicenseID.DataBindings.Add("EditValue", bsModel, mod.GetName(p => p.LicenseID), true, DataSourceUpdateMode.OnPropertyChanged);
            //LicenseExpirationDate.DataBindings.Add("EditValue", bsModel, mod.GetName(p => p.LicenseExpirationDate), true, DataSourceUpdateMode.OnPropertyChanged);
            PayRateOverride.DataBindings.Add("EditValue", bsModel, mod.GetName(p => p.PayRateOverride), true, DataSourceUpdateMode.OnPropertyChanged);
            IsEnabled.DataBindings.Add("Checked", bsModel, mod.GetName(p => p.IsEnabled), true, DataSourceUpdateMode.OnPropertyChanged);

            BindingSource bsLocations = new BindingSource();
            bsLocations.DataSource = mod.Locations;
            gridControlLocations.DataSource = bsLocations;

            FileBlobCreationInfoModel info = new FileBlobCreationInfoModel();
            info.DriverID = this.Manager.ActiveModel.DriverID;
            var manager = FileBlobManager.Create(info);
            xu_Files.Load(manager);

            var driverLicensesManager = DriverLicenseCatalogManager.Create(this.Manager.ActiveModel.DriverID);
            xu_DriverLicenses.LoadManager(driverLicensesManager);
        }

        private void gridViewLocations_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            LocationDriverModel row = gridViewLocations.GetRow(e.RowHandle) as LocationDriverModel;
            if (e.Column.Name == col_Location.Name && row != null && row.CompanyID != 0)
            {
                var rep = e.RepositoryItem as RepositoryItemLookUpEdit;
                var clone = rep.Clone() as RepositoryItemLookUpEdit;
                var data = this.Manager.GetLocations(row.CompanyID);
                clone.DataSource = data;
                clone.DropDownRows = data.Count + 1;
                e.RepositoryItem = clone;
            }
        }

        private void gridViewLocations_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == col_Delete.Name)
            {
                if (Mess.Question("Are you sure you want to delete this?") == System.Windows.Forms.DialogResult.Yes)
                {
                    gridViewLocations.DeleteRow(e.RowHandle);
                }
            }
        }

        private void gridViewLocations_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var row = gridViewLocations.GetRow(e.RowHandle) as LocationDriverModel;
            if (row != null)
                row.DriverID = this.Manager.ActiveModel.DriverID;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var result = this.Manager.SaveDriver(this.Manager.ActiveModel);
            if (result.Failed)
            {
                Mess.Info(result.Message);
                this.TryShowPopup(result.Property);
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.Manager.ActiveModel.DriverID != 0 &&
                Mess.Question("Are you sure you want to delete this driver?") == System.Windows.Forms.DialogResult.Yes)
            {
                var result = this.Manager.DeleteDriver(this.Manager.ActiveModel);
                if (result.Failed)
                {
                    Mess.Info(result.Message);
                    this.TryShowPopup(result.Property);
                    return;
                }
                else
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                    this.Close();
                }
            }
        }

        private void DriverCode_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Caption == "Peek")
            {
                this.Manager.UpdateDriverCode();
            }
        }
    }
}
