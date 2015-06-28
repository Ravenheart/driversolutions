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
using DriverSolutions.ModuleSystem;
using DriverSolutions.DAL;
using DriverSolutions.BOL.Models.ModuleFinance;
using DriverSolutions.BOL.Repositories.ModuleFinance;
using DriverSolutions.ModuleFinance;
using System.Net.Mail;
using System.Net;
using DriverSolutions.BOL.Models.ModuleSystem;
using DriverSolutions.BOL.Repositories.ModuleSystem;
using DriverSolutions.BOL.Services;

namespace DriverSolutions
{
    public partial class XF_TestForm : DevExpress.XtraEditors.XtraForm
    {
        private DSModel DbContext;

        public XF_TestForm()
        {
            InitializeComponent();

            this.DbContext = DB.GetContext();

            this.Load += XF_TestForm_Load;
        }

        private void XF_TestForm_Load(object sender, EventArgs e)
        {

        }

        public static DialogResult ShowWindow()
        {
            using (XF_TestForm form = new XF_TestForm())
                return form.ShowDialog();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SMS sms = new SMS();

            bool result = sms.SendSMS("12674443111", "12674443111", "Third test from your number, with international code.");
            Mess.Info("Status: " + result);
        }


    }
}