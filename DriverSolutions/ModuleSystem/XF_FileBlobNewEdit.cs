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
using DriverSolutions.DAL;
using System.IO;

namespace DriverSolutions.ModuleSystem
{
    public partial class XF_FileBlobNewEdit : DevExpress.XtraEditors.XtraForm
    {
        public IFileBlobManager Manager { get; set; }

        public XF_FileBlobNewEdit(IFileBlobManager manager)
        {
            InitializeComponent();

            if (manager == null)
                throw new ArgumentNullException("manager");

            this.Manager = manager;

            this.Load += XF_FileBlobNewEdit_Load;
        }

        void XF_FileBlobNewEdit_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
                return;

            BindModel();
        }

        private void BindModel()
        {
            var mod = this.Manager.ActiveModel;
            BindingSource bsMod = new BindingSource();
            bsMod.DataSource = mod;

            BlobName.DataBindings.Add("EditValue", bsMod, mod.GetName(p => p.BlobName), true, DataSourceUpdateMode.OnPropertyChanged);
            BlobDescription.DataBindings.Add("EditValue", bsMod, mod.GetName(p => p.BlobDescription), true, DataSourceUpdateMode.OnPropertyChanged);
            if (mod.BlobID != 0)
                btnSeek.Text = "== Database ==";
        }

        private void btnSeek_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.AutoUpgradeEnabled = true;
                dlg.CheckFileExists = true;
                dlg.Multiselect = false;
                dlg.Title = "Select a file to attach";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string file = dlg.FileName;
                    btnSeek.Text = file;
                    this.Manager.ActiveModel.BlobData = File.ReadAllBytes(file);
                    this.Manager.ActiveModel.BlobExtension = Path.GetExtension(file);
                    if (chkUpdateFileName.Checked)
                        this.Manager.ActiveModel.BlobName = Path.GetFileNameWithoutExtension(file);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var res = this.Manager.SaveFile(this.Manager.ActiveModel);
            if (res.Failed)
            {
                Mess.Error(res.Message);
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.Manager.ActiveModel.BlobID != 0)
            {
                if (Mess.Question("Are you sure you want to delete the file?") == System.Windows.Forms.DialogResult.Yes)
                {
                    var res = this.Manager.DeleteFile(this.Manager.ActiveModel);
                    if (res.Failed)
                    {
                        Mess.Error(res.Message);
                    }
                    else
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                        this.Close();
                    }
                }
            }
        }



        public static DialogResult F_Show(IFileBlobManager manager)
        {
            using (XF_FileBlobNewEdit form = new XF_FileBlobNewEdit(manager))
                return form.ShowDialog();
        }

        public static DialogResult F_ShowEdit(uint blobID)
        {
            var manager = FileBlobManager.CreateEdit(blobID);
            return F_Show(manager);
        }
    }
}