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
using DevExpress.XtraEditors.Controls;
using DriverSolutions.BOL.Repositories.ModuleSystem;

namespace DriverSolutions.ModuleSystem
{
    public partial class XU_Location : DevExpress.XtraEditors.XtraUserControl, INotifyPropertyChanged
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

        private uint _CompanyID;
        public uint CompanyID
        {
            get
            {
                return _CompanyID;
            }
            set
            {
                if (_CompanyID != value)
                {
                    _CompanyID = value;
                    try
                    {
                        btnLocation.Properties.DataSource = LocationRepository.GetLocationsByCompany(this.DbContext, this.CompanyID);
                    }
                    catch { }
                }
            }
        }

        private uint _LocationID;
        public uint LocationID
        {
            get
            {
                return _LocationID;
            }
            set
            {
                if (this.DesignMode) return;

                if (_LocationID != value)
                {
                    _LocationID = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs(this.GetName(p => p.LocationID)));

                    LocationCode.EditValueChanging -= LocationCode_EditValueChanging;
                    var mod = LocationRepository.GetLocation(this.DbContext, value);
                    if (mod == null)
                    {
                        this._LocationID = 0;
                        this.LocationCode.Text = string.Empty;
                        this.btnLocation.EditValue = 0;
                    }
                    else
                    {
                        this.btnLocation.EditValue = mod.LocationID;
                        this.LocationCode.Text = mod.LocationCode;
                    }
                    LocationCode.EditValueChanging -= LocationCode_EditValueChanging;
                }
            }
        }

        public bool ReadOnly
        {
            get
            {
                return LocationCode.Properties.ReadOnly;
            }
            set
            {
                LocationCode.Properties.ReadOnly = value;
                btnLocation.Properties.ReadOnly = value;
                foreach (EditorButton b in btnLocation.Properties.Buttons)
                    b.Enabled = !value;
            }
        }

        public XU_Location()
        {
            InitializeComponent();

            if (this.DesignMode) return;
            try
            {
                btnLocation.Properties.DataSource = LocationRepository.GetLocationsByCompany(this.DbContext, this.CompanyID);
            }
            catch { }

            btnLocation.DataBindings.Add("EditValue", this, this.GetName(p => p.LocationID), true, DataSourceUpdateMode.OnPropertyChanged);
            LocationCode.EditValueChanging += LocationCode_EditValueChanging;
            this.Load += XU_Location_Load;
        }

        void XU_Location_Load(object sender, EventArgs e)
        {
            this.LocationCode.Select();
            this.LocationCode.SelectionStart = this.LocationCode.Text.Length;
        }

        void LocationCode_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (this.DesignMode) return;

            LocationCode.EditValueChanging -= LocationCode_EditValueChanging;
            var mod = LocationRepository.GetLocation(this.DbContext, LocationCode.Text);
            if (mod == null)
            {
                this._LocationID = 0;
                this.btnLocation.EditValue = 0;
            }
            else
            {
                this._LocationID = mod.LocationID;
                this.btnLocation.EditValue = mod.LocationID;
                this.LocationCode.Text = mod.LocationCode;
                this.LocationCode.SelectionStart = this.LocationCode.Text.Length;
            }
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(this.GetName(p => p.LocationID)));
            LocationCode.EditValueChanging += LocationCode_EditValueChanging;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
