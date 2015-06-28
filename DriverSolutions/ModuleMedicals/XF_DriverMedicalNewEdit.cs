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

namespace DriverSolutions.ModuleMedicals
{
    public partial class XF_DriverMedicalNewEdit : DevExpress.XtraEditors.XtraForm
    {
        public IDriverMedicalManager Manager { get; set; }

        public XF_DriverMedicalNewEdit(IDriverMedicalManager manager)
        {
            InitializeComponent();

            if (manager == null)
                throw new ArgumentNullException("manager");

            this.Manager = manager;
            this.Load += XF_DriverMedicalNewEdit_Load;
            gridViewReminders.RowCellClick += gridViewReminders_RowCellClick;
        }

        void XF_DriverMedicalNewEdit_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
                return;

            LoadDefaults();
            BindModel();
        }

        private void LoadDefaults()
        {
            var medTypes = this.Manager.GetMedicalTypes();
            MedTypeID.Properties.DataSource = medTypes;
            MedTypeID.Properties.DropDownRows = medTypes.Count + 1;

            var reminders = this.Manager.GetReminderTypes();
            rep_ReminderID.DataSource = reminders;
            rep_ReminderID.DropDownRows = reminders.Count + 1;

            var remKinds = this.Manager.GetReminderKinds();
            rep_ReminderType.DataSource = remKinds;
            rep_ReminderType.DropDownRows = remKinds.Count + 1;
        }

        private void BindModel()
        {
            var mod = this.Manager.ActiveModel;
            BindingSource bsMod = new BindingSource();
            bsMod.DataSource = mod;

            DriverID.DataBindings.Clear();
            DriverID.DataBindings.Add("DriverID", bsMod, mod.GetName(p => p.DriverID), true, DataSourceUpdateMode.OnPropertyChanged);

            MedTypeID.DataBindings.Clear();
            MedTypeID.DataBindings.Add("EditValue", bsMod, mod.GetName(p => p.MedTypeID), true, DataSourceUpdateMode.OnPropertyChanged);

            ExaminationDate.DataBindings.Clear();
            ExaminationDate.DataBindings.Add("EditValue", bsMod, mod.GetName(p => p.ExaminationDate), true, DataSourceUpdateMode.OnPropertyChanged);

            ValidityDate.DataBindings.Clear();
            ValidityDate.DataBindings.Add("EditValue", bsMod, mod.GetName(p => p.ValidityDate), true, DataSourceUpdateMode.OnPropertyChanged);

            BindingSource bsRem = new BindingSource();
            bsRem.DataSource = mod.Reminders;
            gridControlReminders.DataSource = bsRem;
        }

        void gridViewReminders_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            var rem = gridViewReminders.GetRow(e.RowHandle) as ReminderModel;
            if (rem == null)
                return;

            if (e.Column.Name == col_Delete.Name)
            {
                if (Mess.Question("Are you sure you want to delete this reminder?") == System.Windows.Forms.DialogResult.Yes)
                    gridViewReminders.DeleteRow(e.RowHandle);
            }
        }

        public static DialogResult F_Show(IDriverMedicalManager manager)
        {
            using (XF_DriverMedicalNewEdit form = new XF_DriverMedicalNewEdit(manager))
                return form.ShowDialog();
        }

        public static DialogResult F_ShowEdit(uint driverMedicalID)
        {
            var manager = DriverMedicalManager.CreateEdit(driverMedicalID);
            return F_Show(manager);
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
            if (this.Manager.ActiveModel.DriverMedicalID != 0 && GLOB.User.IsAdmin)
            {
                if (Mess.Question("Are you sure you want to delete this medical?") == System.Windows.Forms.DialogResult.Yes)
                {
                    var res = this.Manager.Delete();
                    if (res.Failed)
                    {
                        Mess.Error(res.Message);
                        this.TryShowPopup(res.Property);
                        return;
                    }

                    this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                    this.Close();
                }
            }
        }
    }
}