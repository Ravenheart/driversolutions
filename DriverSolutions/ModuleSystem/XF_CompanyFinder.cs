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
using DriverSolutions.BOL.Repositories.ModuleSystem;
using DriverSolutions.BOL.Models.ModuleSystem;
using DriverSolutions.BOL.Managers.ModuleSystem;

namespace DriverSolutions.ModuleSystem
{
    public partial class XF_CompanyFinder : DevExpress.XtraEditors.XtraForm
    {
        private DSModel DbContext { get; set; }
        private bool IsFinder { get; set; }

        public uint CompanyID { get; private set; }

        public XF_CompanyFinder(bool isFinder = true)
        {
            InitializeComponent();

            this.IsFinder = isFinder;

            this.Load += XF_CompanyFinder_Load;
            gridViewCompanies.RowCellClick += gridViewCompanies_RowCellClick;
        }

        void XF_CompanyFinder_Load(object sender, EventArgs e)
        {
            if (!IsFinder)
            {
                laySelect.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                menuNewCompany.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                col_Edit.Visible = false;
            }

            this.DbContext = DB.GetContext();
            LoadDefaults();
            LoadData();

            CompanyCode.Select();
        }

        private void LoadDefaults()
        {

        }

        private void LoadData()
        {
            var data = CompanyRepository.FindCompanies(this.DbContext,
                CompanyName.Text,
                CompanyCode.Text,
                CompanyAddress1.Text,
                CompanyAddress2.Text,
                CompanyCity.Text,
                CompanyState.Text,
                CompanyPostCode.Text,
                CompanyFax.Text,
                CompanyPhone.Text,
                CompanyEmail.Text,
                IncludeDisabled.Checked);

            gridControlCompanies.DataSource = data;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                btnSearch_Click(this, EventArgs.Empty);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void gridViewCompanies_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            var row = gridViewCompanies.GetRow(e.RowHandle) as CompanyModel;
            if (row == null)
                return;

            if (e.Clicks == 2)
            {
                if (IsFinder)
                {
                    this.CompanyID = row.CompanyID;
                    this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                    this.Close();
                }
                else
                {
                    var manager = CompanyManager.CreateEdit(row.CompanyID);
                    using (XF_CompanyNewEdit form = new XF_CompanyNewEdit(manager))
                        form.ShowDialog();
                }
            }
            else if (e.Column.Name == col_Edit.Name)
            {
                var manager = CompanyManager.CreateEdit(row.CompanyID);
                using (XF_CompanyNewEdit form = new XF_CompanyNewEdit(manager))
                    form.ShowDialog();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;
            btnClear.Enabled = false;

            LoadData();

            btnSearch.Enabled = true;
            btnClear.Enabled = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            CompanyName.Text = string.Empty;
            CompanyCode.Text = string.Empty;
            CompanyAddress1.Text = string.Empty;
            CompanyAddress2.Text = string.Empty;
            CompanyCity.Text = string.Empty;
            CompanyState.Text = string.Empty;
            CompanyPostCode.Text = string.Empty;
            CompanyFax.Text = string.Empty;
            CompanyPhone.Text = string.Empty;
            CompanyEmail.Text = string.Empty;
            IncludeDisabled.Checked = true;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            var row = gridViewCompanies.GetFocusedRow() as CompanyModel;
            if (row == null)
            {
                Mess.Info("Please select a company!");
                return;
            }

            this.CompanyID = row.CompanyID;
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Close();
        }

        private void menuNewCompany_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var manager = CompanyManager.CreateNew();
            using (XF_CompanyNewEdit form = new XF_CompanyNewEdit(manager))
            {
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                {
                    int index = gridViewCompanies.TopRowIndex;
                    btnSearch_Click(this, EventArgs.Empty);
                    gridViewCompanies.TopRowIndex = index;
                }
            }
        }




    }
}