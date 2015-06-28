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
using DriverSolutions.BOL.Repositories.ModuleSystem;
using DriverSolutions.DAL;
using DevExpress.XtraEditors.Controls;
using DriverSolutions.BOL.Models.ModuleDriver;
using DriverSolutions.BOL.Managers.ModuleDriver;

namespace DriverSolutions.ModuleSystem
{
    public partial class XU_Driver : DevExpress.XtraEditors.XtraUserControl
    {
        private DSModel _DB = null;
        private DSModel DbContext
        {
            get
            {
                if (this.DesignMode) return null;

                if (_DB == null)
                    _DB = DB.GetContext();

                return _DB;
            }
        }

        private uint _DriverID;
        public uint DriverID
        {
            get
            {
                return _DriverID;
            }
            set
            {
                if (this.DesignMode) return;

                if (_DriverID != value)
                {
                    _DriverID = value;
                    DriverCode.EditValueChanging -= DriverCode_EditValueChanging;
                    var mod = DriverRepository.GetDriver(this.DbContext, value);
                    if (mod == null)
                    {
                        _DriverID = 0;
                        DriverCode.EditValue = string.Empty;
                        btnDriver.EditValue = 0;
                    }
                    else
                    {
                        DriverCode.EditValue = mod.DriverCode;
                        btnDriver.EditValue = mod.DriverID;
                    }
                    DriverCode.EditValueChanging += DriverCode_EditValueChanging;
                }
            }
        }

        public bool ReadOnly
        {
            get
            {
                return DriverCode.Properties.ReadOnly;
            }
            set
            {
                DriverCode.Properties.ReadOnly = value;
                btnDriver.Properties.ReadOnly = value;
                foreach (EditorButton b in btnDriver.Properties.Buttons)
                    b.Enabled = !value;
            }
        }

        public XU_Driver()
        {
            InitializeComponent();

            if (this.DesignMode) return;
            try
            {
                DriverFilterModel filter = new DriverFilterModel();
                filter.IncludeDisabled = true;
                btnDriver.Properties.DataSource = DriverRepository.FindDrivers(this.DbContext, filter);
            }
            catch { }

            btnDriver.DataBindings.Add("EditValue", this, this.GetName(p => p.DriverID), true, DataSourceUpdateMode.OnPropertyChanged);
            btnDriver.ButtonClick += btnDriver_ButtonClick;
            DriverCode.EditValueChanging += DriverCode_EditValueChanging;
            this.Load += XU_Driver_Load;
        }

        void XU_Driver_Load(object sender, EventArgs e)
        {
            this.DriverCode.Select();
            this.DriverCode.SelectionStart = this.DriverCode.Text.Length;
        }

        private void btnDriver_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Caption == "Search...")
            {
                var manager = DriverCatalogManager.Create(true);
                using (XF_DriverFinder find = new XF_DriverFinder(manager))
                {
                    if (find.ShowDialog() == DialogResult.Yes)
                    {
                        this.DriverID = manager.ActiveDriver.DriverID;
                        this.DriverCode.Select();
                        this.DriverCode.SelectionStart = this.DriverCode.Text.Length;
                    }
                }
            }
        }

        private void DriverCode_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (this.DesignMode)
                return;

            DriverCode.EditValueChanging -= DriverCode_EditValueChanging;
            using (var db = DB.GetContext())
            {
                var mod = DriverRepository.GetDriver(db, DriverCode.Text);
                if (mod == null)
                {
                    this._DriverID = 0;
                    this.btnDriver.EditValue = 0;
                }
                else
                {
                    this._DriverID = mod.DriverID;
                    this.btnDriver.EditValue = mod.DriverID;
                    this.DriverCode.Text = mod.DriverCode;
                    this.DriverCode.SelectionStart = this.DriverCode.Text.Length;
                }
            }
            DriverCode.EditValueChanging += DriverCode_EditValueChanging;
        }
    }
}
