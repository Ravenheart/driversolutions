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
using DriverSolutions.DAL;
using DriverSolutions.BOL.Repositories.ModuleSystem;
using DevExpress.XtraEditors.Controls;

namespace DriverSolutions.ModuleSystem
{
    public partial class XU_Company : DevExpress.XtraEditors.XtraUserControl, INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;

        private uint _CompanyID;
        public uint CompanyID
        {
            get
            {
                return _CompanyID;
            }
            set
            {
                if (this.DesignMode) return;

                if (_CompanyID != value)
                {
                    _CompanyID = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs(this.GetName(p => p.CompanyID)));

                    CompanyCode.EditValueChanging -= CompanyCode_EditValueChanging;
                    var mod = CompanyRepository.GetCompany(this.DbContext, value);
                    if (mod == null)
                    {
                        this._CompanyID = 0;
                        this.CompanyCode.Text = string.Empty;
                        this.btnCompany.EditValue = 0;
                    }
                    else
                    {
                        this.btnCompany.EditValue = mod.CompanyID;
                        this.CompanyCode.Text = mod.CompanyCode;
                    }
                    CompanyCode.EditValueChanging += CompanyCode_EditValueChanging;
                }
            }
        }

        public bool ReadOnly
        {
            get
            {
                return CompanyCode.Properties.ReadOnly;
            }
            set
            {
                CompanyCode.Properties.ReadOnly = value;
                btnCompany.Properties.ReadOnly = value;
                foreach (EditorButton b in btnCompany.Properties.Buttons)
                    b.Enabled = !value;
            }
        }

        public XU_Company()
        {
            InitializeComponent();

            if (this.DesignMode) return;
            try
            {
                btnCompany.Properties.DataSource = CompanyRepository.FindCompanies(this.DbContext);
            }
            catch { }

            btnCompany.DataBindings.Add("EditValue", this, this.GetName(p => p.CompanyID), true, DataSourceUpdateMode.OnPropertyChanged);
            btnCompany.ButtonClick += btnCompany_ButtonClick;
            CompanyCode.EditValueChanging += CompanyCode_EditValueChanging;
            this.Load += XU_Company_Load;
        }

        void XU_Company_Load(object sender, EventArgs e)
        {
            this.CompanyCode.Select();
            this.CompanyCode.SelectionStart = this.CompanyCode.Text.Length;
        }

        void btnCompany_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Caption == "Search...")
            {
                using (XF_CompanyFinder find = new XF_CompanyFinder(true))
                {
                    if (find.ShowDialog() == DialogResult.Yes)
                    {
                        this.CompanyID = find.CompanyID;
                        this.CompanyCode.Select();
                        this.CompanyCode.SelectionStart = this.CompanyCode.Text.Length;
                    }
                }
            }
        }

        void CompanyCode_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (this.DesignMode)
                return;

            CompanyCode.EditValueChanging -= CompanyCode_EditValueChanging;
            var mod = CompanyRepository.GetCompany(this.DbContext, CompanyCode.Text);
            if (mod == null)
            {
                this._CompanyID = 0;
                this.btnCompany.EditValue = 0;
            }
            else
            {
                this._CompanyID = mod.CompanyID;
                this.btnCompany.EditValue = mod.CompanyID;
                this.CompanyCode.Text = mod.CompanyCode;
                this.CompanyCode.SelectionStart = this.CompanyCode.Text.Length;
            }
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(this.GetName(p => p.CompanyID)));
            CompanyCode.EditValueChanging += CompanyCode_EditValueChanging;
        }


    }
}
