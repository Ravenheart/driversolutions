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
using DriverSolutions.BOL.Managers.ModuleDispatches;
using DriverSolutions.ModuleSystem;
using DriverSolutions.BOL.Models.ModuleDispatches;
using DevExpress.XtraEditors.Repository;
using DriverSolutions.ModuleReports;
using DriverSolutions.ModuleFinance;
using DriverSolutions.BOL.Managers.ModuleFinance;
using DriverSolutions.DAL;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Base;
using DriverSolutions.ModuleMedicals;
using DriverSolutions.BOL.Managers.ModuleDriver;
using DriverSolutions.ModuleDriver;

namespace DriverSolutions.ModuleDispatches
{
    public partial class XF_Dispatches : DevExpress.XtraEditors.XtraForm
    {
        public IDispatchManager Manager { get; set; }

        public XF_Dispatches(IDispatchManager manager)
        {
            InitializeComponent();

            if (manager == null)
                throw new ArgumentNullException("manager");

            this.Manager = manager;
            this.Load += XF_Dispatches_Load;
            gridViewDisp.RowCellClick += gridViewDisp_RowCellClick;
            gridViewDisp.CustomDrawCell += gridViewDisp_CustomDrawCell;
            gridViewDisp.CellValueChanged += gridViewDisp_CellValueChanged;
            xU_Dispatch.DispatchChanged += xU_Dispatch_DispatchChanged;
        }

        void xU_Dispatch_DispatchChanged(object sender, DispatchEventArgs e)
        {
            LoadDispatches();
        }

        private void XF_Dispatches_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
                return;

            LoadDefaults();

            //touchscreen
            if (GLOB.Settings.Get<bool>(1) == true)
            {
                fromDate.Properties.CalendarView = CalendarView.TouchUI;
                toDate.Properties.CalendarView = CalendarView.TouchUI;
            }

            if (!GLOB.User.IsAdmin)
            {
                menuSettings.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            LoadDispatches();
        }

        private void LoadDefaults()
        {
            fromDate.DateTime = DateTime.Now.PreviousOccurance(DayOfWeek.Sunday);

            var drivers = this.Manager.GetDrivers();
            DriverID.Properties.DataSource = drivers;
            DriverID.Properties.DropDownRows = drivers.Count + 1;

            var companies = this.Manager.GetCompanies();
            CompanyID.Properties.DataSource = companies;
            CompanyID.Properties.DropDownRows = companies.Count + 1;
        }

        private void gridViewDisp_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (this.DesignMode)
                return;

            var row = gridViewDisp.GetRow(e.RowHandle) as DispatchCatalogModel;
            if (row != null)
            {
                xU_Dispatch.LoadDispatch(row);
            }

            if (e.Clicks == 2)
            {
                var manager = DispatchManager.CreateEdit(row.DispatchID);
                using (XF_DispatchNewEdit form = new XF_DispatchNewEdit(manager))
                {
                    if (form.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                    {
                        LoadDispatches();

                        if (form.IsDeleted)
                            xU_Dispatch.ClearDispatch();
                    }
                }
            }
        }

        void gridViewDisp_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            var row = gridViewDisp.GetRow(e.RowHandle) as DispatchCatalogModel;
            if (row != null)
            {
                if (gridViewDisp.FocusedRowHandle == e.RowHandle)
                {
                    e.Appearance.ForeColor = Color.Black;
                }
                if (row.IsConfirmed)
                {
                    e.Appearance.BackColor = Color.LightGreen;
                }
                if (e.Column.Name == col_Cancelled.Name && row.IsCancelled)
                {
                    e.Appearance.BackColor = Color.IndianRed;
                }
                if (this.BoldColumn != null && e.RowHandle == BoldColumn.RowHandle && e.Column.Name == BoldColumn.Column.Name)
                {
                    e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, 10.0f, FontStyle.Bold);
                }
            }
        }

        private CellValueChangedEventArgs BoldColumn;
        void gridViewDisp_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var row = gridViewDisp.GetRow(e.RowHandle) as DispatchCatalogModel;
            if (row != null)
            {
                var manager = DispatchManager.Create(row);
                var res = manager.SaveDispatch(row);
                if (res.Failed)
                {
                    Mess.Warning(res.Message);
                    this.TryShowPopup(res.Property);
                }
                else
                {
                    LoadDispatches();
                    this.BoldColumn = e;
                }
            }
        }

        private void LoadDispatches()
        {
            uint[] drivers = DriverID.GetCheckedValues();
            uint[] companies = CompanyID.GetCheckedValues();
            DateTime fromDt = fromDate.DateTime;
            DateTime toDt = toDate.DateTime;
            bool inCancelled = IncludeCancelled.Checked;

            int index = gridViewDisp.TopRowIndex;
            gridControlDisp.DataSource = this.Manager.GetDispatches(drivers, companies, fromDt, toDt, inCancelled);
            gridViewDisp.TopRowIndex = index;
        }

        private void menuSettingsDrivers_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!GLOB.User.IsAdmin)
                return;

            var manager = DriverCatalogManager.Create(false);
            using (XF_DriverFinder find = new XF_DriverFinder(manager))
                find.ShowDialog();
        }

        private void menuSettingsCompanies_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!GLOB.User.IsAdmin)
                return;

            using (XF_CompanyFinder find = new XF_CompanyFinder(false))
                find.ShowDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            btnClear.Enabled = false;
            btnRefresh.Enabled = false;

            LoadDispatches();

            btnClear.Enabled = true;
            btnRefresh.Enabled = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            xU_Dispatch.ClearDispatch();
            DriverID.SetEditValue(string.Empty);
            CompanyID.SetEditValue(string.Empty);
            fromDate.DateTime = DateTime.Now.StartOfWeek();
            toDate.EditValue = null;
            IncludeCancelled.Checked = false;
        }

        private void menuInfoReports_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (XF_Reports form = new XF_Reports())
                form.ShowDialog();
        }

        private void menuInfoInvoices_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var manager = InvoiceCatalogManager.Create();
            using (XF_Invoices form = new XF_Invoices(manager))
                form.ShowDialog();
        }

        private void menuSettingsReports_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!GLOB.User.IsAdmin)
                return;

            using (XF_ReportEditor form = new XF_ReportEditor())
            {
                form.ShowDialog();
            }
        }

        private void menuSettingsUsers_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!GLOB.User.IsAdmin)
                return;

            using (XF_Users form = new XF_Users())
                form.ShowDialog();
        }

        private void menuSettingsHolidays_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var manager = HolidayManager.Create();
            using (XF_Holidays form = new XF_Holidays(manager))
                form.ShowDialog();
        }

        private void menuHelpAbout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (XF_About form = new XF_About())
                form.ShowDialog();
        }

        private void menuHelpRemoteSupport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Process.Start("teamviewer.exe");
            }
            catch { }
        }

        private void menuInfoMedicals_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XF_DriverMedicals.F_Show();
        }

        private void menuInfoLicenses_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XF_DriverLicenses.ShowWindow();
        }
    }
}