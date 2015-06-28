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
using DriverSolutions.DAL;

namespace DriverSolutions.ModuleSystem
{
    public partial class XF_ReportOpen : DevExpress.XtraEditors.XtraForm
    {
        private DSModel DbContext;
        public uint FileID { get; private set; }

        public XF_ReportOpen(DSModel db)
        {
            InitializeComponent();

            this.DbContext = db;

            this.Load += XF_ReportOpen_Load;
            gridViewReports.RowCellClick += gridViewReports_RowCellClick;
        }

        void gridViewReports_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                var row = gridViewReports.GetRow(e.RowHandle) as UtilityModel<uint>;
                if (row == null)
                {
                    Mess.Info("Please select a report!");
                    return;
                }

                this.FileID = row.Value;
                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                this.Close();
            }
        }

        void XF_ReportOpen_Load(object sender, EventArgs e)
        {
            gridControlReports.DataSource = this.DbContext.FileObjects
                .Where(f => f.FileExtension == "repx")
                .Select(f => new UtilityModel<uint>(f.FileID, f.FileName))
                .ToList();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var row = gridViewReports.GetFocusedRow() as UtilityModel<uint>;
            if (row == null)
            {
                Mess.Info("Please select a report!");
                return;
            }

            this.FileID = row.Value;
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}