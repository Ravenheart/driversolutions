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
using DriverSolutions.BOL.Models.ModuleDispatches;
using DriverSolutions.DAL;

namespace DriverSolutions.ModuleDispatches
{
    public partial class XF_DispatchNewEdit : DevExpress.XtraEditors.XtraForm
    {
        public IDispatchManager Manager { get; set; }
        public bool IsDeleted { get; set; }

        public XF_DispatchNewEdit(IDispatchManager manager)
        {
            InitializeComponent();

            if (manager == null)
                throw new ArgumentNullException("manager");

            this.Manager = manager;
            this.Load += XF_DispatchNewEdit_Load;
            CompanyID.PropertyChanged += CompanyID_PropertyChanged;
            LocationID.PropertyChanged += LocationID_PropertyChanged;
            IsCancelled.EditValueChanging += IsCancelled_EditValueChanging;
        }

        void XF_DispatchNewEdit_Load(object sender, EventArgs e)
        {
            LoadDefaults();
            HandleCancelled(this.Manager.ActiveModel.IsCancelled);
            BindModel(this.Manager.ActiveModel);
        }

        private void LoadDefaults()
        {

        }

        private void BindModel(DispatchModel model)
        {
            BindingSource bsMod = new BindingSource();
            bsMod.DataSource = model;

            DriverID.DataBindings.Add("DriverID", bsMod, model.GetName(p => p.DriverID), true, DataSourceUpdateMode.OnPropertyChanged);
            CompanyID.DataBindings.Add("CompanyID", bsMod, model.GetName(p => p.CompanyID), true, DataSourceUpdateMode.OnPropertyChanged);
            LocationID.DataBindings.Add("LocationID", bsMod, model.GetName(p => p.LocationID), true, DataSourceUpdateMode.OnPropertyChanged);
            FromDateTime.DataBindings.Add("EditValue", bsMod, model.GetName(p => p.FromDateTime), true, DataSourceUpdateMode.OnPropertyChanged);
            ToDateTime.DataBindings.Add("EditValue", bsMod, model.GetName(p => p.ToDateTime), true, DataSourceUpdateMode.OnPropertyChanged);
            SpecialPayRate.DataBindings.Add("EditValue", bsMod, model.GetName(p => p.SpecialPayRate), true, DataSourceUpdateMode.OnPropertyChanged);
            MiscCharge.DataBindings.Add("EditValue", bsMod, model.GetName(p => p.MiscCharge), true, DataSourceUpdateMode.OnPropertyChanged);
            IsCancelled.DataBindings.Add("Checked", bsMod, model.GetName(p => p.IsCancelled), true, DataSourceUpdateMode.OnPropertyChanged);
            HasLunch.DataBindings.Add("Checked", bsMod, model.GetName(p => p.HasLunch), true, DataSourceUpdateMode.OnPropertyChanged);
            HasTraining.DataBindings.Add("Checked", bsMod, model.GetName(p => p.HasTraining), true, DataSourceUpdateMode.OnPropertyChanged);
            CancelledHours.DataBindings.Add("EditValue", bsMod, model.GetName(p => p.CancelledHours), true, DataSourceUpdateMode.OnPropertyChanged);
            Note.DataBindings.Add("EditValue", bsMod, model.GetName(p => p.Note), true, DataSourceUpdateMode.OnPropertyChanged);

            LastEdit.Text = model.UserName + " on " + model.LastUpdateTime.ToString(GLOB.Formats.DateTime);

            LocationID.Tag = model;
        }

        void CompanyID_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CompanyID.PropertyChanged -= CompanyID_PropertyChanged;
            LocationID.CompanyID = CompanyID.CompanyID;
            if (LocationID.Tag != null)
            {
                var mod = LocationID.Tag as DispatchModel;
                mod.LocationID = 0;
            }
            CompanyID.PropertyChanged += CompanyID_PropertyChanged;
        }

        void LocationID_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            LocationID.PropertyChanged -= LocationID_PropertyChanged;
            if (this.Manager.ActiveModel.DispatchID == 0)
            {
                int lunch = this.Manager.GetLocationLunchTime(LocationID.LocationID);
                this.Manager.ActiveModel.LunchTime = lunch;
            }
            LocationID.PropertyChanged += LocationID_PropertyChanged;
        }

        void IsCancelled_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            bool isCancel = (bool)e.NewValue;
            HandleCancelled(isCancel);
        }

        private void HandleCancelled(bool isCancelled)
        {
            if (isCancelled)
            {
                layCancelledHours.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                FromDateTime.Enabled = false;
                ToDateTime.Enabled = false;
            }
            else
            {
                layCancelledHours.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                FromDateTime.Enabled = true;
                ToDateTime.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var result = this.Manager.SaveDispatch(this.Manager.ActiveModel);
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
            if (this.Manager.ActiveModel.DispatchID != 0 &&
                Mess.Question("Are you sure you want to delete this dispatch?") == System.Windows.Forms.DialogResult.Yes)
            {
                var result = this.Manager.DeleteDispatch(this.Manager.ActiveModel);
                if (result.Failed)
                {
                    Mess.Info(result.Message);
                    this.TryShowPopup(result.Property);
                    return;
                }
                else
                {
                    this.IsDeleted = true;
                    this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                    this.Close();
                }
            }
        }
    }
}