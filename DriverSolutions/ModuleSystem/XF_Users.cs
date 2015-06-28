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
using DriverSolutions.BOL.Models.ModuleSystem;
using DriverSolutions.BOL.Managers.ModuleSystem;
using DriverSolutions.DAL;
using DriverSolutions.BOL.Repositories.ModuleSystem;

namespace DriverSolutions.ModuleSystem
{
    public partial class XF_Users : DevExpress.XtraEditors.XtraForm
    {
        private DSModel DbContext;

        public XF_Users()
        {
            InitializeComponent();

            this.Load += XF_Users_Load;
            gridViewUsers.RowCellClick += gridViewUsers_RowCellClick;
        }

        void XF_Users_Load(object sender, EventArgs e)
        {
            this.DbContext = DB.GetContext();

            LoadUsers();
        }

        void gridViewUsers_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Clicks == 2 || e.Column.Name == col_Edit.Name)
            {
                var row = gridViewUsers.GetRow(e.RowHandle) as UserModel;
                if (row != null)
                {
                    var manager = UserManager.CreateEdit(row);
                    using (XF_UserNewEdit form = new XF_UserNewEdit(manager))
                    {
                        if (form.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                        {
                            int index = gridViewUsers.TopRowIndex;
                            LoadUsers();
                            gridViewUsers.TopRowIndex = index;
                        }
                    }
                }
            }
        }

        private void LoadUsers()
        {
            var data = UserRepository.FindUsers(this.DbContext, Username.Text, FirstName.Text, SecondName.Text, LastName.Text);
            gridControlUsers.DataSource = data;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;
            btnClear.Enabled = false;

            LoadUsers();

            btnSearch.Enabled = true;
            btnClear.Enabled = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Username.Text = string.Empty;
            FirstName.Text = string.Empty;
            SecondName.Text = string.Empty;
            LastName.Text = string.Empty;
        }

        private void menuNewUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var manager = UserManager.Create();
            using (XF_UserNewEdit form = new XF_UserNewEdit(manager))
            {
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                {
                    int index = gridViewUsers.TopRowIndex;
                    LoadUsers();
                    gridViewUsers.TopRowIndex = index;
                }
            }
        }
    }
}