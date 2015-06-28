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
using DriverSolutions.BOL.Models.ModuleFinance;
using DriverSolutions.BOL.Managers.ModuleFinance;
using DriverSolutions.Core;
using DevExpress.XtraReports.UI;

namespace DriverSolutions.ModuleFinance
{
    public partial class XF_InvoiceNewEdit : DevExpress.XtraEditors.XtraForm
    {
        public IInvoiceManager Manager { get; set; }

        public XF_InvoiceNewEdit(IInvoiceManager manager)
        {
            InitializeComponent();

            if (manager == null)
                throw new ArgumentNullException("manager");

            this.Manager = manager;
            this.Load += XF_InvoiceNewEdit_Load;
            this.InvoiceNumber.ButtonClick += InvoiceNumber_ButtonClick;
            this.gridViewServices.CellValueChanged += gridViewServices_CellValueChanged;
            this.gridViewServices.RowCellClick += gridViewServices_RowCellClick;
        }

        void gridViewServices_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == col_Delete.Name)
            {
                if (Mess.Question("Are you sure you wish to remove this item?") == System.Windows.Forms.DialogResult.Yes)
                {
                    gridViewServices.DeleteRow(e.RowHandle);
                }
            }
        }

        void gridViewServices_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            TotalAmount.EditValue = this.Manager.ActiveModel.TotalAmount;
        }

        void XF_InvoiceNewEdit_Load(object sender, EventArgs e)
        {
            LoadDefaults();
            BindModel(this.Manager.ActiveModel);
        }

        private void LoadDefaults()
        {
            var types = this.Manager.GetInvoiceTypes();
            this.InvoiceTypeID.Properties.DataSource = types;
            this.InvoiceTypeID.Properties.DropDownRows = types.Count + 1;
        }

        private void BindModel(InvoiceModel mod)
        {
            BindingSource bsMod = new BindingSource();
            bsMod.DataSource = mod;

            InvoiceNumber.DataBindings.Clear();
            InvoiceNumber.DataBindings.Add("EditValue", bsMod, mod.GetName(p => p.InvoiceNumber), true, DataSourceUpdateMode.OnPropertyChanged);

            InvoiceTypeID.DataBindings.Clear();
            InvoiceTypeID.DataBindings.Add("EditValue", bsMod, mod.GetName(p => p.InvoiceTypeID), true, DataSourceUpdateMode.OnPropertyChanged);

            InvoiceIssueDate.DataBindings.Clear();
            InvoiceIssueDate.DataBindings.Add("EditValue", bsMod, mod.GetName(p => p.InvoiceIssueDate), true, DataSourceUpdateMode.OnPropertyChanged);

            IsConfirmed.DataBindings.Clear();
            IsConfirmed.DataBindings.Add("Checked", bsMod, mod.GetName(p => p.IsConfirmed), true, DataSourceUpdateMode.OnPropertyChanged);

            InvoicePeriodFrom.DataBindings.Clear();
            InvoicePeriodFrom.DataBindings.Add("EditValue", bsMod, mod.GetName(p => p.InvoicePeriodFrom), true, DataSourceUpdateMode.OnPropertyChanged);

            InvoicePeriodTo.DataBindings.Clear();
            InvoicePeriodTo.DataBindings.Add("EditValue", bsMod, mod.GetName(p => p.InvoicePeriodTo), true, DataSourceUpdateMode.OnPropertyChanged);

            InvoiceNote.DataBindings.Clear();
            InvoiceNote.DataBindings.Add("EditValue", bsMod, mod.GetName(p => p.InvoiceNote), true, DataSourceUpdateMode.OnPropertyChanged);

            LateCharge.DataBindings.Clear();
            LateCharge.DataBindings.Add("EditValue", bsMod, mod.GetName(p => p.LateCharge), true, DataSourceUpdateMode.OnPropertyChanged);

            LateChargeDays.DataBindings.Clear();
            LateChargeDays.DataBindings.Add("EditValue", bsMod, mod.GetName(p => p.LateChargeDays), true, DataSourceUpdateMode.OnPropertyChanged);

            TotalAmount.DataBindings.Clear();
            TotalAmount.DataBindings.Add("EditValue", bsMod, mod.GetName(p => p.TotalAmount), true, DataSourceUpdateMode.OnPropertyChanged);

            ReceiverCompany.DataBindings.Clear();
            ReceiverCompany.DataBindings.Add("EditValue", bsMod, mod.GetName(p => p.ReceiverCompany), true, DataSourceUpdateMode.OnPropertyChanged);

            ReceiverAddress.DataBindings.Clear();
            ReceiverAddress.DataBindings.Add("EditValue", bsMod, mod.GetName(p => p.ReceiverAddress), true, DataSourceUpdateMode.OnPropertyChanged);

            ShipperCompany.Text = GLOB.Company.CompanyName;
            ShipperAddress.Text = GLOB.Company.CompanyInvoiceAddress;

            BindingSource bsDetails = new BindingSource();
            bsDetails.DataSource = mod.Details;

            gridControlServices.DataSource = bsDetails;
        }

        void InvoiceNumber_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            this.Manager.PeekInvoiceNumber(this.Manager.ActiveModel);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var check = this.Manager.SaveInvoice(this.Manager.ActiveModel);
            if (check.Failed)
            {
                Mess.Info(check.Message);
                this.TryShowPopup(check.Property);
                return;
            }

            XtraReport report = this.Manager.PrintInvoice(this.Manager.ActiveModel.InvoiceID);
            report.ShowPreviewDialog();

            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRecalculate_Click(object sender, EventArgs e)
        {
            if (Mess.Question("Are you sure you wish to recalculate this invoice?") == System.Windows.Forms.DialogResult.Yes)
            {
                this.Manager.RecalculateInvoice(this.Manager.ActiveModel);
                BindModel(this.Manager.ActiveModel);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.Manager.ActiveModel.InvoiceID != 0 && GLOB.User.IsAdmin)
            {
                if (Mess.Question("Are you usre you wish to delete this invoice?") == System.Windows.Forms.DialogResult.Yes)
                {
                    var check = this.Manager.DeleteInvoice(this.Manager.ActiveModel);
                    if (check.Failed)
                    {
                        Mess.Info(check.Message);
                        this.TryShowPopup(check.Property);
                        return;
                    }

                    this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                    this.Close();
                }
            }
        }
    }
}