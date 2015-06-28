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
using DriverSolutions.BOL.Models.ModuleFinance;
using System.IO;
using DriverSolutions.Core;
using DevExpress.XtraReports.UI;
using System.Diagnostics;

namespace DriverSolutions.ModuleFinance
{
    public partial class XF_Invoices : DevExpress.XtraEditors.XtraForm
    {
        public IInvoiceCatalogManager Manager { get; set; }

        public XF_Invoices(IInvoiceCatalogManager manager)
        {
            InitializeComponent();

            if (manager == null)
                throw new ArgumentNullException("manager");

            this.Manager = manager;
            this.Load += XF_Invoices_Load;
            gridViewInvoices.RowCellClick += gridViewInvoices_RowCellClick;
            gridViewInvoices.CellValueChanging += gridViewInvoices_CellValueChanging;
        }

        private void XF_Invoices_Load(object sender, EventArgs e)
        {
            LoadDefaults();
            BindModel();
            LoadInvoices();
        }

        private void LoadDefaults()
        {
            var comp = this.Manager.GetCompanies();
            CompanyID.Properties.DataSource = comp;
            CompanyID.Properties.DropDownRows = comp.Count + 1;

            var loc = this.Manager.GetLocations();
            LocationID.Properties.DataSource = loc;
            LocationID.Properties.DropDownRows = loc.Count + 1;
        }

        private void BindModel()
        {
            var fil = this.Manager.Filter;
            BindingSource bsFil = new BindingSource();
            bsFil.DataSource = fil;

            InvoiceNumber.DataBindings.Clear();
            InvoiceNumber.DataBindings.Add("EditValue", bsFil, fil.GetName(p => p.InvoiceNumber), true, DataSourceUpdateMode.OnPropertyChanged);

            IssuedFrom.DataBindings.Clear();
            IssuedFrom.DataBindings.Add("EditValue", bsFil, fil.GetName(p => p.IssuedFrom), true, DataSourceUpdateMode.OnPropertyChanged);

            IssuedTo.DataBindings.Clear();
            IssuedTo.DataBindings.Add("EditValue", bsFil, fil.GetName(p => p.IssuedTo), true, DataSourceUpdateMode.OnPropertyChanged);

            PeriodFrom.DataBindings.Clear();
            PeriodFrom.DataBindings.Add("EditValue", bsFil, fil.GetName(p => p.PeriodFrom), true, DataSourceUpdateMode.OnPropertyChanged);

            PeriodTo.DataBindings.Clear();
            PeriodTo.DataBindings.Add("EditValue", bsFil, fil.GetName(p => p.PeriodTo), true, DataSourceUpdateMode.OnPropertyChanged);

            CompanyID.DataBindings.Clear();
            CompanyID.DataBindings.Add("EditValue", bsFil, fil.GetName(p => p.CompanyID), true, DataSourceUpdateMode.OnPropertyChanged);

            LocationID.DataBindings.Clear();
            LocationID.DataBindings.Add("EditValue", bsFil, fil.GetName(p => p.LocationID), true, DataSourceUpdateMode.OnPropertyChanged);

            BindingSource bsInvoices = new BindingSource();
            bsInvoices.DataSource = this.Manager.ActiveModel;

            gridControlInvoices.DataSource = bsInvoices;
        }

        private void LoadInvoices()
        {
            int index = gridViewInvoices.TopRowIndex;
            this.Manager.LoadInvoices(this.Manager.Filter);
            gridViewInvoices.RefreshData();
            gridViewInvoices.TopRowIndex = index;
        }

        private void gridViewInvoices_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == col_IsConfirmed.Name)
            {
                InvoiceCatalogModel row = gridViewInvoices.GetRow(e.RowHandle) as InvoiceCatalogModel;
                if (row != null)
                {
                    bool value = Convert.ToBoolean(e.Value);
                    this.Manager.UpdateIsConfirmed(row.InvoiceID, value);
                }
            }
        }

        private void gridViewInvoices_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Clicks == 2 || e.Column.Name == col_Edit.Name)
            {
                uint invID = (gridViewInvoices.GetRow(e.RowHandle) as InvoiceCatalogModel).InvoiceID;
                var manager = InvoiceManager.CreateEdit(invID);
                using (XF_InvoiceNewEdit form = new XF_InvoiceNewEdit(manager))
                {
                    if (form.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                    {
                        LoadInvoices();
                    }
                }
            }
        }

        private void menuInvoicesGenerate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (XF_InvoicesSelector form = new XF_InvoicesSelector(this.Manager))
            {
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Manager.GenerateInvoices(form.CompanyIDs, form.LocationIDs, form.PeriodFrom, form.PeriodTo);
                    XF_AsyncResult.ShowWindow(this.Manager);
                    LoadInvoices();
                }
            }
        }

        private void menuInvoicesRecalculate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            uint[] ids = GetMarkedInvoiceIDs();

            this.Manager.RecalculateInvoices(ids);
            XF_AsyncResult.ShowWindow(this.Manager);
            LoadInvoices();
        }

        private void menuInvoicesExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            uint[] ids = GetMarkedInvoiceIDs();

            this.Manager.ExportInvoices(ids);
            XF_AsyncResult.ShowWindow(this.Manager);
            string invoicesFolder = Path.Combine(Environment.CurrentDirectory, "invoices");
            Process.Start(invoicesFolder);
        }

        private void menuInvoicesEmail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Mess.Question("Are you sure you wish to email the selected invoices?") != System.Windows.Forms.DialogResult.Yes)
                return;

            uint[] ids = GetMarkedInvoiceIDs();
            this.Manager.EmailInvoices(ids);
            XF_AsyncResult.ShowWindow(this.Manager);
        }

        private void menuInvoicesSendConf_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Mess.Question("Are you sure you wish to email the selected invoices?") != System.Windows.Forms.DialogResult.Yes)
                return;

            uint[] ids = GetMarkedInvoiceIDs();

            this.Manager.EmailConfirmationInvoices(ids);
            XF_AsyncResult.ShowWindow(this.Manager);
        }

        private void menuInvoicesExportConf_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            uint[] ids = GetMarkedInvoiceIDs();

            this.Manager.ExportConfirmations(ids);
            XF_AsyncResult.ShowWindow(this.Manager);
            string confFolder = Path.Combine(Environment.CurrentDirectory, "confirmations");
            Process.Start(confFolder);
        }

        private void menuItemsMarkAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Manager.MarkAll();
            gridControlInvoices.RefreshDataSource();
        }

        private void menuItemsUnmarkAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Manager.UnmarkAll();
            gridControlInvoices.RefreshDataSource();
        }

        private void menuItemsMarkSelected_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            uint[] ids = GetSelectedInvoiceIDs();

            this.Manager.MarkSelected(ids);
            gridControlInvoices.RefreshDataSource();
        }

        private void menuItemsUnmarkSelected_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            uint[] ids = GetSelectedInvoiceIDs();

            this.Manager.UnmarkSelected(ids);
            gridControlInvoices.RefreshDataSource();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;
            btnClear.Enabled = false;

            LoadInvoices();

            btnSearch.Enabled = true;
            btnClear.Enabled = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            InvoiceNumber.EditValue = string.Empty;
            IssuedFrom.EditValue = new DateTime(now.Year, now.Month, 1);
            IssuedTo.EditValue = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));
            PeriodFrom.EditValue = null;
            PeriodTo.EditValue = null;
            CompanyID.SetEditValue(string.Empty);
            LocationID.SetEditValue(string.Empty);
        }

        private uint[] GetSelectedInvoiceIDs()
        {
            int[] indexes = gridViewInvoices.GetSelectedRows();
            uint[] ids = new uint[indexes.Length];
            InvoiceCatalogModel[] rows = new InvoiceCatalogModel[indexes.Length];
            for (int i = 0; i < indexes.Length; i++)
            {
                ids[i] = (gridViewInvoices.GetRow(indexes[i]) as InvoiceCatalogModel).InvoiceID;
            }

            return ids;
        }

        private uint[] GetMarkedInvoiceIDs()
        {
            gridViewInvoices.PostEditor();
            gridViewInvoices.UpdateCurrentRow();
            return this.Manager.ActiveModel.Where(i => i.IsMarked).Select(i => i.InvoiceID).ToArray();
        }

        

        
    }
}