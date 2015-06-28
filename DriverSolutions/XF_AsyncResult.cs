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
using DriverSolutions.BOL.Core;

namespace DriverSolutions
{
    public partial class XF_AsyncResult : DevExpress.XtraEditors.XtraForm
    {
        public IReportProgress Manager { get; set; }
        private BindingSource DataItem { get; set; }

        public XF_AsyncResult(IReportProgress manager)
        {
            InitializeComponent();

            if (manager == null)
                throw new ArgumentNullException("manager");

            this.Manager = manager;
            this.DataItem = new BindingSource();
            this.DataItem.DataSource = new List<ProgressItem>();
            this.gridControlReport.DataSource = this.DataItem;
            this.gridViewReport.CustomDrawCell += gridViewReport_CustomDrawCell;

            this.Manager.OnProgressChanged += Manager_OnProgressChanged;
            this.Manager.OnComplete += Manager_OnComplete;
            this.FormClosing += XF_AsyncResult_FormClosing;

            this.btnClose.Enabled = false;
        }

        void gridViewReport_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            ProgressItem item = gridViewReport.GetRow(e.RowHandle) as ProgressItem;
            if (item != null && item.Status == ProgressStatus.Error)
                e.Appearance.BackColor = Color.IndianRed;
        }

        void XF_AsyncResult_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Manager.OnProgressChanged -= Manager_OnProgressChanged;
            this.Manager.OnComplete -= Manager_OnComplete;
        }


        void Manager_OnComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate()
            {
                this.btnClose.Enabled = true;
                progStatus.EditValue = 0;
                this.DataItem.Insert(0, e.Result);
            });
        }

        void Manager_OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate()
            {
                progStatus.EditValue = e.ProgressPercentage;
                this.DataItem.Insert(0, e.UserState);
            });
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static DialogResult ShowWindow(IReportProgress manager)
        {
            using (XF_AsyncResult form = new XF_AsyncResult(manager))
                return form.ShowDialog();
        }
    }
}