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
using DriverSolutions.BOL.Models.ModuleSystem;
using DriverSolutions.DAL;

namespace DriverSolutions.ModuleSystem
{
    public partial class XF_LocationNewEdit : DevExpress.XtraEditors.XtraForm
    {
        public LocationModel ActiveModel { get; set; }
        public bool IsDeleted { get; set; }

        public XF_LocationNewEdit(LocationModel location)
        {
            InitializeComponent();

            if (location == null)
                throw new ArgumentNullException("location");

            this.ActiveModel = location;

            this.Load += XF_LocationNewEdit_Load;
        }

        void XF_LocationNewEdit_Load(object sender, EventArgs e)
        {
            if (!GLOB.User.IsAdmin)
                layDelete.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            BindModel(this.ActiveModel);
        }

        private void BindModel(LocationModel mod)
        {
            BindingSource bsMod = new BindingSource();
            bsMod.DataSource = mod;

            LocationName.DataBindings.Add("EditValue", bsMod, mod.GetName(p => p.LocationName), true, DataSourceUpdateMode.OnPropertyChanged);
            LocationCode.DataBindings.Add("EditValue", bsMod, mod.GetName(p => p.LocationCode), true, DataSourceUpdateMode.OnPropertyChanged);
            LocationAddress.DataBindings.Add("EditValue", bsMod, mod.GetName(p => p.LocationAddress), true, DataSourceUpdateMode.OnPropertyChanged);
            LocationPhone.DataBindings.Add("EditValue", bsMod, mod.GetName(p => p.LocationPhone), true, DataSourceUpdateMode.OnPropertyChanged);
            LocationFax.DataBindings.Add("EditValue", bsMod, mod.GetName(p => p.LocationFax), true, DataSourceUpdateMode.OnPropertyChanged);
            TravelPay.DataBindings.Add("EditValue", bsMod, mod.GetName(p => p.TravelPay), true, DataSourceUpdateMode.OnPropertyChanged);
            TravelPayName.DataBindings.Add("EditValue", bsMod, mod.GetName(p => p.TravelPayName), true, DataSourceUpdateMode.OnPropertyChanged);
            LunchTime.DataBindings.Add("EditValue", bsMod, mod.GetName(p => p.LunchTime), true, DataSourceUpdateMode.OnPropertyChanged);
            IsEnabled.DataBindings.Add("Checked", bsMod, mod.GetName(p => p.IsEnabled), true, DataSourceUpdateMode.OnPropertyChanged);
            IncludeConfirmation.DataBindings.Add("Checked", bsMod, mod.GetName(p => p.IncludeConfirmation), true, DataSourceUpdateMode.OnPropertyChanged);

            var conf = mod.ConfirmationContact;
            BindingSource bsConf = new BindingSource();
            bsConf.DataSource = conf;
            ConfirmationContactName.DataBindings.Add("EditValue", bsConf, conf.GetName(p => p.ContactName), true, DataSourceUpdateMode.OnPropertyChanged);
            ConfirmationContactPhone.DataBindings.Add("EditValue", bsConf, conf.GetName(p => p.ContactPhone), true, DataSourceUpdateMode.OnPropertyChanged);
            ConfirmationContactEmail.DataBindings.Add("EditValue", bsConf, conf.GetName(p => p.ContactEmail), true, DataSourceUpdateMode.OnPropertyChanged);

            var inv = mod.InvoiceContact;
            BindingSource bsInv = new BindingSource();
            bsInv.DataSource = inv;
            InvoiceContactName.DataBindings.Add("EditValue", bsInv, inv.GetName(p => p.ContactName), true, DataSourceUpdateMode.OnPropertyChanged);
            InvoiceContactPhone.DataBindings.Add("EditValue", bsInv, inv.GetName(p => p.ContactPhone), true, DataSourceUpdateMode.OnPropertyChanged);
            InvoiceContactEmail.DataBindings.Add("EditValue", bsInv, inv.GetName(p => p.ContactEmail), true, DataSourceUpdateMode.OnPropertyChanged);

            var disp = mod.DispatchContact;
            BindingSource bsDisp = new BindingSource();
            bsDisp.DataSource = disp;
            DispatchContactName.DataBindings.Add("EditValue", bsDisp, disp.GetName(p => p.ContactName), true, DataSourceUpdateMode.OnPropertyChanged);
            DispatchContactPhone.DataBindings.Add("EditValue", bsDisp, disp.GetName(p => p.ContactPhone), true, DataSourceUpdateMode.OnPropertyChanged);
            DispatchContactEmail.DataBindings.Add("EditValue", bsDisp, disp.GetName(p => p.ContactEmail), true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Close();
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (GLOB.User.IsAdmin)
            {
                if (Mess.Question("Are you sure you wish to delete this location?") == System.Windows.Forms.DialogResult.Yes)
                {
                    this.IsDeleted = true;
                    this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                    this.Close();
                }
            }
        }
    }
}