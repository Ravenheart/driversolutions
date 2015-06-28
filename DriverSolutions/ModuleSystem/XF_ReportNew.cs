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

namespace DriverSolutions.ModuleSystem
{
    public partial class XF_ReportNew : DevExpress.XtraEditors.XtraForm
    {
        public string FileName
        {
            get
            {
                return txtName.Text;
            }
        }
        public string FileDescription
        {
            get
            {
                return txtDescription.Text;
            }
        }

        public XF_ReportNew()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.FileName))
            {
                Mess.Info("Please enter a file name!");
                txtName.Select();
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