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
using DriverSolutions.BOL.Managers.ModuleMedical;
using DriverSolutions.DAL;
using DriverSolutions.BOL.Models.ModuleMedical;
using DevExpress.XtraGrid.Views.Grid;
using DriverSolutions.BOL.Core;

namespace DriverSolutions.ModuleMedicals
{
    public partial class XF_DriverMedicals : DevExpress.XtraEditors.XtraForm
    {
        public IDriverMedicalCatalogManager Manager { get; set; }

        public XF_DriverMedicals(IDriverMedicalCatalogManager manager)
        {
            InitializeComponent();

            if (manager == null)
                throw new ArgumentNullException("manager");

            this.Manager = manager;

            this.Load += XF_DriverMedicals_Load;
            gridViewMedicals.RowCellClick += gridViewMedicals_RowCellClick;
            gridViewMedicals.CustomDrawCell += gridViewMedicals_CustomDrawCell;
        }

        void XF_DriverMedicals_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
                return;

            LoadDefaults();
            BindFilter();
        }

        private void LoadDefaults()
        {
            var drivers = this.Manager.GetDrivers();
            DriverID.Properties.DataSource = drivers;
            DriverID.Properties.DropDownRows = drivers.Count + 1;

            var medTypes = this.Manager.GetMedicalTypes();
            MedTypeID.Properties.DataSource = medTypes;
            MedTypeID.Properties.DropDownRows = medTypes.Count + 1;
        }

        private void BindFilter()
        {
            var fil = this.Manager.Filter;
            BindingSource bsFil = new BindingSource();
            bsFil.DataSource = fil;

            DriverID.DataBindings.Clear();
            DriverID.DataBindings.Add("EditValue", bsFil, fil.GetName(p => p.DriverID), true, DataSourceUpdateMode.OnPropertyChanged);

            MedTypeID.DataBindings.Clear();
            MedTypeID.DataBindings.Add("EditValue", bsFil, fil.GetName(p => p.MedTypeID), true, DataSourceUpdateMode.OnPropertyChanged);

            ValidityDateFrom.DataBindings.Clear();
            ValidityDateFrom.DataBindings.Add("EditValue", bsFil, fil.GetName(p => p.ValidityDateFrom), true, DataSourceUpdateMode.OnPropertyChanged);

            ValidityDateTo.DataBindings.Clear();
            ValidityDateTo.DataBindings.Add("EditValue", bsFil, fil.GetName(p => p.ValidityDateTo), true, DataSourceUpdateMode.OnPropertyChanged);

            BindingSource bsMedicals = new BindingSource();
            bsMedicals.DataSource = this.Manager.ActiveMedicals;
            gridControlMedicals.DataSource = bsMedicals;

            BindingSource bsReminders = new BindingSource();
            bsReminders.DataSource = this.Manager.ActiveReminders;
            gridControlReminders.DataSource = bsReminders;
        }

        private void gridViewMedicals_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            var mod = gridViewMedicals.GetRow(e.RowHandle) as DriverMedicalCatalogModel;
            if (mod == null)
                return;

            if (e.Clicks == 2 || e.Column.Name == col_Edit.Name)
            {
                if (XF_DriverMedicalNewEdit.F_ShowEdit(mod.DriverMedicalID) == System.Windows.Forms.DialogResult.Yes)
                    RefreshMedicals();
            }
            else if (e.Column.Name == col_Renew.Name)
            {
                var manager = DriverMedicalManager.CreateNewFromOld(mod.DriverMedicalID);
                if (XF_DriverMedicalNewEdit.F_Show(manager) == System.Windows.Forms.DialogResult.Yes)
                    RefreshMedicals();
            }
        }

        private void gridViewMedicals_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            var mod = gridViewMedicals.GetRow(e.RowHandle) as DriverMedicalCatalogModel;
            if (mod == null)
                return;

            if (mod.ReminderStatus == 2)
                e.Appearance.BackColor = StatusColors.Yellow;
            else if (mod.ReminderStatus == 3)
                e.Appearance.BackColor = StatusColors.Red;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;
            btnClear.Enabled = false;

            RefreshMedicals();

            btnSearch.Enabled = true;
            btnClear.Enabled = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;
            btnClear.Enabled = false;

            DriverID.SetEditValue(string.Empty);
            MedTypeID.SetEditValue(string.Empty);
            ValidityDateFrom.EditValue = DateTime.Now.StartOfMonth();
            ValidityDateTo.EditValue = DateTime.Now.EndOfMonth();

            btnSearch.Enabled = true;
            btnClear.Enabled = true;
        }

        private void RefreshMedicals()
        {
            int topRow = gridViewMedicals.TopRowIndex;
            this.Manager.RefreshMedicals();
            gridViewMedicals.TopRowIndex = topRow;
        }

        private void btnNewMedical_Click(object sender, EventArgs e)
        {
            var manager = DriverMedicalManager.CreateNew();
            if (XF_DriverMedicalNewEdit.F_Show(manager) == System.Windows.Forms.DialogResult.Yes)
                RefreshMedicals();
        }

        public static DialogResult F_Show()
        {
            var manager = DriverMedicalCatalogManager.Create();
            using (XF_DriverMedicals form = new XF_DriverMedicals(manager))
                return form.ShowDialog();
        }
    }
}