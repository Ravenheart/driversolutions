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
using System.Diagnostics;

namespace DriverSolutions.ModuleSystem
{
    public partial class XF_About : DevExpress.XtraEditors.XtraForm
    {
        public XF_About()
        {
            InitializeComponent();

            lblVersion.Text = "v" + Application.ProductVersion;
            lblEmail.MouseEnter += lblEmail_MouseEnter;
            lblEmail.MouseLeave += lblEmail_MouseLeave;
            lblEmail.MouseClick += lblEmail_MouseClick;
        }

        void lblEmail_MouseClick(object sender, MouseEventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo("mailto:rav3nlabs@gmail.com");
            info.UseShellExecute = true;
            try
            {
                Process.Start(info);
            }
            catch { }
        }

        void lblEmail_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        void lblEmail_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}