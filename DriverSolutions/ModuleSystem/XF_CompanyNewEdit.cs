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
using DriverSolutions.BOL.Repositories.ModuleSystem;
using DriverSolutions.DAL;

namespace DriverSolutions.ModuleSystem
{
    public partial class XF_CompanyNewEdit : DevExpress.XtraEditors.XtraForm
    {
        public ICompanyManager Manager { get; set; }
        public XF_CompanyNewEdit(ICompanyManager manager)
        {
            InitializeComponent();

            if (manager == null)
                throw new ArgumentNullException("manager");

            this.Manager = manager;
            this.Load += XF_CompanyNewEdit_Load;
            gridViewLocations.RowCellClick += gridViewLocations_RowCellClick;
            gridViewLocations.InitNewRow += gridViewLocations_InitNewRow;
            gridViewLicense.InitNewRow += gridViewLicense_InitNewRow;
            gridViewInvoice.InitNewRow += gridViewInvoice_InitNewRow;
        }

        void gridViewLocations_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Clicks == 2 || e.Column.Name == col_Loc_Edit.Name)
            {
                var row = gridViewLocations.GetRow(e.RowHandle) as LocationModel;
                if (row != null)
                {
                    using (XF_LocationNewEdit form = new XF_LocationNewEdit(row))
                    {
                        if (form.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                        {
                            if (form.IsDeleted)
                                gridViewLocations.DeleteRow(e.RowHandle);
                        }
                    }
                }
            }
        }

        void gridViewLocations_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var row = gridViewLocations.GetRow(e.RowHandle) as LocationModel;
            if (row != null)
                row.LocationCode = this.Manager.PeekLocationCode();
        }

        void gridViewLicense_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var row = gridViewLicense.GetRow(e.RowHandle) as CompanyLicensePayRateModel;
            if (row != null)
                row.CompanyID = this.Manager.ActiveModel.CompanyID;
        }

        void gridViewInvoice_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var row = gridViewInvoice.GetRow(e.RowHandle) as CompanyInvoicingPayRateModel;
            if (row != null)
                row.CompanyID = this.Manager.ActiveModel.CompanyID;
        }

        void XF_CompanyNewEdit_Load(object sender, EventArgs e)
        {
            //disable payrates if new company
            if (this.Manager.ActiveModel.CompanyID == 0)
            {
                xtraTabPageLocations.PageEnabled = false;
                xtraTabPageLicenseRates.PageEnabled = false;
                xtraTabPageInvoiceRates.PageEnabled = false;
            }
            if (GLOB.Settings.Get<bool>(1) == true)//touchscreen
            {
                rep_IDate.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
                rep_LDate.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
            }

            LoadDefaults();
            BindModel(this.Manager.ActiveModel);
        }

        private void LoadDefaults()
        {
            var licenses = this.Manager.GetLicenseClasses();
            rep_LLicense.DataSource = licenses;
            rep_LLicense.DropDownRows = licenses.Count;

            rep_ILicense.DataSource = licenses;
            rep_ILicense.DropDownRows = licenses.Count;
        }

        private void BindModel(CompanyModel model)
        {
            BindingSource bsModel = new BindingSource();
            bsModel.DataSource = model;

            CompanyName.DataBindings.Add("EditValue", bsModel, model.GetName(p => p.CompanyName), true, DataSourceUpdateMode.OnPropertyChanged);
            CompanyCode.DataBindings.Add("EditValue", bsModel, model.GetName(p => p.CompanyCode), true, DataSourceUpdateMode.OnPropertyChanged);
            CompanyAddress1.DataBindings.Add("EditValue", bsModel, model.GetName(p => p.CompanyAddress1), true, DataSourceUpdateMode.OnPropertyChanged);
            CompanyAddress2.DataBindings.Add("EditValue", bsModel, model.GetName(p => p.CompanyAddress2), true, DataSourceUpdateMode.OnPropertyChanged);
            CompanyCity.DataBindings.Add("EditValue", bsModel, model.GetName(p => p.CompanyCity), true, DataSourceUpdateMode.OnPropertyChanged);
            CompanyState.DataBindings.Add("EditValue", bsModel, model.GetName(p => p.CompanyState), true, DataSourceUpdateMode.OnPropertyChanged);
            CompanyPostCode.DataBindings.Add("EditValue", bsModel, model.GetName(p => p.CompanyPostCode), true, DataSourceUpdateMode.OnPropertyChanged);
            CompanyContactName.DataBindings.Add("EditValue", bsModel, model.GetName(p => p.CompanyContactName), true, DataSourceUpdateMode.OnPropertyChanged);
            CompanyFax.DataBindings.Add("EditValue", bsModel, model.GetName(p => p.CompanyFax), true, DataSourceUpdateMode.OnPropertyChanged);
            CompanyPhone.DataBindings.Add("EditValue", bsModel, model.GetName(p => p.CompanyPhone), true, DataSourceUpdateMode.OnPropertyChanged);
            CompanyEmail.DataBindings.Add("EditValue", bsModel, model.GetName(p => p.CompanyEmail), true, DataSourceUpdateMode.OnPropertyChanged);
            LunchTime.DataBindings.Add("EditValue", bsModel, model.GetName(p => p.LunchTime), true, DataSourceUpdateMode.OnPropertyChanged);
            TrainingTime.DataBindings.Add("EditValue", bsModel, model.GetName(p => p.TrainingTime), true, DataSourceUpdateMode.OnPropertyChanged);
            IsEnabled.DataBindings.Add("Checked", bsModel, model.GetName(p => p.IsEnabled), true, DataSourceUpdateMode.OnPropertyChanged);

            BindingSource bsLocations = new BindingSource();
            bsLocations.DataSource = model.Locations;
            gridControlLocations.DataSource = bsLocations;

            BindingSource bsLicenseRates = new BindingSource();
            bsLicenseRates.DataSource = this.Manager.LicensePayRates;
            gridControlLicense.DataSource = bsLicenseRates;

            BindingSource bsInvoiceRates = new BindingSource();
            bsInvoiceRates.DataSource = this.Manager.InvoicePayRates;
            gridControlInvoice.DataSource = bsInvoiceRates;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var result = this.Manager.SaveCompany(this.Manager.ActiveModel, this.Manager.LicensePayRates, this.Manager.InvoicePayRates);
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
            if (this.Manager.ActiveModel.CompanyID != 0 &&
                Mess.Question("Are you sure you want to delete this company?") == System.Windows.Forms.DialogResult.Yes)
            {
                var result = this.Manager.DeleteCompany(this.Manager.ActiveModel);
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

        private void CompanyCode_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Caption == "Peek")
            {
                this.Manager.UpdateCompanyPeekCode();
                //if (this.Manager.ActiveModel.CompanyID != 0 &&
                //    this.Manager.ActiveModel.CompanyCode != string.Empty &&
                //    Mess.Question("This company already has a Company Code, are you sure you want to Peek and assign a new one?") == System.Windows.Forms.DialogResult.Yes)
                //{
                //    this.Manager.ActiveModel.CompanyCode = this.Manager.PeekCompanyCode();
                //}
                //else
                //{
                //    this.Manager.ActiveModel.CompanyCode = this.Manager.PeekCompanyCode();
                //}
            }
        }

        private void btnAddLocation_Click(object sender, EventArgs e)
        {

        }
    }
}