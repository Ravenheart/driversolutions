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
using DriverSolutions.BOL;

namespace DriverSolutions
{
    public partial class XF_Login : DevExpress.XtraEditors.XtraForm
    {
        public IUserManager Manager { get; set; }

        public XF_Login(IUserManager manager)
        {
            InitializeComponent();

            if (manager == null)
                throw new ArgumentNullException("manager");

            this.Manager = manager;
            this.Load += XF_Login_Load;
        }

        private void XF_Login_Load(object sender, EventArgs e)
        {
            txtUsername.Select();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                Mess.Warning("Please enter a username!");
                txtUsername.Select();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                Mess.Warning("Please enter a password!");
                txtPassword.Select();
                return;
            }

            var user = this.Manager.GetUser(txtUsername.Text, txtPassword.Text);
            if (user == null)
            {
                Mess.Warning("The specified username and/or password is invalid!");
                txtPassword.Select();
                return;
            }

            GLOB.User = user;
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                btnLogin_Click(this, EventArgs.Empty);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public static DialogResult ShowWindow()
        {
            var manager = UserManager.Create();
            using (XF_Login login = new XF_Login(manager))
                return login.ShowDialog();
        }
    }
}