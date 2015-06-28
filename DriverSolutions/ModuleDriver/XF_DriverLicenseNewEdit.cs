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

namespace DriverSolutions.ModuleDriver
{
    public partial class XF_DriverLicenseNewEdit : DevExpress.XtraEditors.XtraForm
    {
        public IDriverLicenseManager Manager { get; set; }

        public XF_DriverLicenseNewEdit(IDriverLicenseManager manager)
        {
            InitializeComponent();

            if (manager == null)
                throw new ArgumentNullException("manager");

            this.Manager = manager;

            this.Load += XF_DriverLicenseNewEdit_Load;
            this.gridViewReminders.RowCellClick += gridViewReminders_RowCellClick;
        }

        private void XF_DriverLicenseNewEdit_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
                return;

            LoadDefaults();
            BindModel();
        }

        private void LoadDefaults()
        {
            var lic = this.Manager.GetLicenseClasses();
            LicenseID.Properties.DataSource = lic;
            LicenseID.Properties.DropDownRows = lic.Count + 1;

            var reminders = this.Manager.GetReminderTypes();
            rep_ReminderID.DataSource = reminders;
            rep_ReminderID.DropDownRows = reminders.Count + 1;

            var kinds = this.Manager.GetReminderKinds();
            rep_ReminderType.DataSource = kinds;
            rep_ReminderType.DropDownRows = kinds.Count + 1;
        }

        private void BindModel()
        {
            var mod = this.Manager.ActiveModel;
            BindingSource bsMod = new BindingSource();
            bsMod.DataSource = mod;

            BindingSource bsPermits = new BindingSource();
            bsPermits.DataSource = this.Manager.Permits;
            Permits.DataSource = bsPermits;

            BindingSource bsReminders = new BindingSource();
            bsReminders.DataSource = mod.Reminders;
            gridControlReminders.DataSource = bsReminders;

            DriverID.DataBindings.Clear();
            DriverID.DataBindings.Add("DriverID", bsMod, mod.GetName(p => p.DriverID), true, DataSourceUpdateMode.OnPropertyChanged);

            LicenseID.DataBindings.Clear();
            LicenseID.DataBindings.Add("EditValue", bsMod, mod.GetName(p => p.LicenseID), true, DataSourceUpdateMode.OnPropertyChanged);

            IssueDate.DataBindings.Clear();
            IssueDate.DataBindings.Add("EditValue", bsMod, mod.GetName(p => p.IssueDate), true, DataSourceUpdateMode.OnPropertyChanged);

            ExpirationDate.DataBindings.Clear();
            ExpirationDate.DataBindings.Add("EditValue", bsMod, mod.GetName(p => p.ExpirationDate), true, DataSourceUpdateMode.OnPropertyChanged);

            MVRReviewDate.DataBindings.Clear();
            MVRReviewDate.DataBindings.Add("EditValue", bsMod, mod.GetName(p => p.MVRReviewDate), true, DataSourceUpdateMode.OnPropertyChanged);

            for (int i = 0; i < this.Manager.Permits.Count; i++)
            {
                var per = this.Manager.Permits[i];
                Permits.SetItemChecked(i, per.IsCheck);
            }
        }

        private void gridViewReminders_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == col_Delete.Name)
            {
                gridViewReminders.DeleteRow(e.RowHandle);
            }
        }

        private bool Save()
        {
            var res = this.Manager.Save();
            if (res.Failed)
            {
                Mess.Error(res.Message);
                this.TryShowPopup(res.Property);
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var res = this.Manager.Delete();
            if (res.Failed)
            {
                Mess.Error(res.Message);
                this.TryShowPopup(res.Property);
            }
            else
            {
                Mess.Info("Successfully deleted license!", "Success");
                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                this.Close();
            }
        }

        public static DialogResult ShowWindow(IDriverLicenseManager manager)
        {
            using (XF_DriverLicenseNewEdit form = new XF_DriverLicenseNewEdit(manager))
                return form.ShowDialog();
        }

        private void Permits_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            var mod = Permits.GetItem(e.Index) as UtilityModel<uint>;
            mod.IsCheck = e.State == CheckState.Checked;
        }
    }
}