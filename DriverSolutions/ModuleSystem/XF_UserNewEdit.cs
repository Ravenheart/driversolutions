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

namespace DriverSolutions.ModuleSystem
{
    public partial class XF_UserNewEdit : DevExpress.XtraEditors.XtraForm
    {
        public IUserManager Manager { get; set; }

        public XF_UserNewEdit(IUserManager manager)
        {
            InitializeComponent();

            if (manager == null)
                throw new ArgumentNullException("manager");

            this.Manager = manager;
            this.Load += XF_UserNewEdit_Load;
        }

        void XF_UserNewEdit_Load(object sender, EventArgs e)
        {
            BindModel(this.Manager.ActiveModel);
        }

        private void BindModel(UserModel user)
        {
            BindingSource bsUser = new BindingSource();
            bsUser.DataSource = user;

            Username.DataBindings.Add("EditValue", bsUser, user.GetName(p => p.Username), true, DataSourceUpdateMode.OnPropertyChanged);
            Password.DataBindings.Add("EditValue", bsUser, user.GetName(p => p.Password), true, DataSourceUpdateMode.OnPropertyChanged);

            FirstName.DataBindings.Add("EditValue", bsUser, user.GetName(p => p.FirstName), true, DataSourceUpdateMode.OnPropertyChanged);
            SecondName.DataBindings.Add("EditValue", bsUser, user.GetName(p => p.SecondName), true, DataSourceUpdateMode.OnPropertyChanged);
            LastName.DataBindings.Add("EditValue", bsUser, user.GetName(p => p.LastName), true, DataSourceUpdateMode.OnPropertyChanged);

            IsEnabled.DataBindings.Add("Checked", bsUser, user.GetName(p => p.IsEnabled), true, DataSourceUpdateMode.OnPropertyChanged);
            IsAdmin.DataBindings.Add("Checked", bsUser, user.GetName(p => p.IsAdmin), true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var res = this.Manager.SaveUser(this.Manager.ActiveModel);
            if (res.Failed)
            {
                Mess.Info(res.Message);
                this.TryShowPopup(res.Property);
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}