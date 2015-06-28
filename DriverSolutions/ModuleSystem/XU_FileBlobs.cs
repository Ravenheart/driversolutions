using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DriverSolutions.BOL.Managers.ModuleSystem;
using DriverSolutions.BOL.Models.ModuleSystem;

namespace DriverSolutions.ModuleSystem
{
    public partial class XU_FileBlobs : DevExpress.XtraEditors.XtraUserControl
    {
        public IFileBlobManager Manager { get; set; }

        public XU_FileBlobs()
        {
            InitializeComponent();

            gridViewFiles.RowCellClick += gridViewFiles_RowCellClick;
        }

        public void Load(IFileBlobManager manager)
        {
            if (manager == null)
                throw new ArgumentNullException("manager");

            this.Manager = manager;
            manager.RefreshFileList();

            BindingSource bsFiles = new BindingSource();
            bsFiles.DataSource = manager.ActiveFiles;
            gridControlFiles.DataSource = bsFiles;
        }

        void gridViewFiles_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            FileBlobViewModel mod = gridViewFiles.GetRow(e.RowHandle) as FileBlobViewModel;
            if (mod == null)
                return;

            if (e.Clicks == 2 || e.Column.Name == col_Preview.Name)
            {
                var res = this.Manager.PreviewFile(mod);
                if (res.Failed)
                    Mess.Error(res.Message);
            }
            else if (e.Column.Name == col_Delete.Name)
            {
                if (Mess.Question("Are you sure you want to delete this file?") == DialogResult.Yes)
                {
                    var res = this.Manager.DeleteFile(mod);
                    if (res.Failed)
                    {
                        Mess.Error(res.Message);
                    }
                    else
                    {
                        this.Manager.RefreshFileList();
                    }
                }
            }
            else if (e.Column.Name == col_Edit.Name)
            {
                if (XF_FileBlobNewEdit.F_ShowEdit(mod.BlobID) == DialogResult.Yes)
                    this.Manager.RefreshFileList();
            }
        }

        private void btnAttachFile_Click(object sender, EventArgs e)
        {
            var manager = FileBlobManager.Create(this.Manager.CreationInfo);
            if (XF_FileBlobNewEdit.F_Show(manager) == DialogResult.Yes)
                this.Manager.RefreshFileList();
        }

        private void btnAttachMultipleFiles_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.AutoUpgradeEnabled = true;
                dlg.CheckFileExists = true;
                dlg.Multiselect = true;
                dlg.Title = "Select files to attach";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    var res = this.Manager.AttachMultipleFiles(dlg.FileNames);
                    if (res.Failed)
                        Mess.Error(res.Message);
                    else
                        this.Manager.RefreshFileList();
                }
            }
        }
    }
}
