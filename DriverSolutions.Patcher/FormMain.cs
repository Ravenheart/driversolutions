using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DriverSolutions.Patcher
{
    public partial class FormMain : Form
    {
        private Updater Manager;
        public FormMain()
        {
            InitializeComponent();

            this.Manager = new Updater();
            this.Manager.OnProgress += Manager_OnProgress;
            this.Manager.OnUpdateComplete += Manager_OnUpdateComplete;
            this.Manager.OnBuildComplete += Manager_OnBuildComplete;

            this.Load += FormMain_Load;
        }

        private void Manager_OnProgress(object sender, ProgressChangedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate()
            {
                lblStatus.Text = e.UserState.ToString();
                progressBar.Value = e.ProgressPercentage;
            });
        }

        private void Manager_OnUpdateComplete(object sender, EventArgs e)
        {
            Process.Start("DriverSolutions.exe");
            Application.Exit();
        }

        private void Manager_OnBuildComplete(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void FormMain_Load(object sender, EventArgs e)
        {
            var args = Environment.GetCommandLineArgs();

            if (args.Contains("BUILD"))
            {
                await this.Manager.Build();
            }
            else
            {
                await this.Manager.Update();
            }
        }


    }
}
