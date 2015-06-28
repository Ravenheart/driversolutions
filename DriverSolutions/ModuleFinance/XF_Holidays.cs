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
using DriverSolutions.BOL.Managers.ModuleFinance;

namespace DriverSolutions.ModuleFinance
{
    public partial class XF_Holidays : DevExpress.XtraEditors.XtraForm
    {
        public IHolidayManager Manager { get; set; }

        public XF_Holidays(IHolidayManager manager)
        {
            InitializeComponent();

            if (manager == null)
                throw new ArgumentNullException("manager");

            this.Manager = manager;
            this.Load += XF_Holidays_Load;
            this.gridViewHolidays.RowCellClick += gridViewHolidays_RowCellClick;
        }

        void XF_Holidays_Load(object sender, EventArgs e)
        {
            BindingSource bsData = new BindingSource();
            bsData.DataSource = this.Manager.ActiveModel;

            gridControlHolidays.DataSource = bsData;
        }

        void gridViewHolidays_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == col_Remove.Name)
            {
                if (Mess.Question("Are you sure you want to remove this date?") == System.Windows.Forms.DialogResult.Yes)
                {
                    gridViewHolidays.DeleteRow(e.RowHandle);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var res = this.Manager.SaveHolidays(this.Manager.ActiveModel);
            if (res.Failed)
            {
                Mess.Warning(res.Message, "Error");
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