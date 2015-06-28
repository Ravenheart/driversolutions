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
using DriverSolutions.BOL.Models.ModuleDispatches;
using DriverSolutions.DAL;
using DriverSolutions.BOL.Managers.ModuleDispatches;

namespace DriverSolutions.ModuleDispatches
{
    public partial class XU_Dispatch : DevExpress.XtraEditors.XtraUserControl
    {
        public DispatchModel Dispatch { get; set; }

        public event EventHandler<DispatchEventArgs> DispatchChanged;

        public XU_Dispatch()
        {
            InitializeComponent();
        }

        public void ClearDispatch()
        {
            this.Dispatch = null;
            DriverID.DataBindings.Clear();
            DriverID.DriverID = 0;
            CompanyID.DataBindings.Clear();
            CompanyID.CompanyID = 0;
            TotalTime.EditValue = null;
            OverTime.EditValue = null;
        }

        public void LoadDispatch(DispatchModel disp)
        {
            this.Dispatch = disp;

            BindingSource bsDisp = new BindingSource();
            bsDisp.DataSource = disp;

            DriverID.DataBindings.Clear();
            DriverID.DataBindings.Add("DriverID", bsDisp, disp.GetName(p => p.DriverID), true, DataSourceUpdateMode.OnPropertyChanged);

            CompanyID.DataBindings.Clear();
            CompanyID.DataBindings.Add("CompanyID", bsDisp, disp.GetName(p => p.CompanyID), true, DataSourceUpdateMode.OnPropertyChanged);

            double totalTime = (disp.ToDateTime - disp.FromDateTime).TotalHours;
            totalTime = totalTime - (disp.LunchTime / 60.0d);
            double overTime = (totalTime > 8.0d ? totalTime - 8.0d : 0.0d);

            TotalTime.EditValue = totalTime;
            OverTime.EditValue = overTime;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.Dispatch == null)
                return;

            var manager = DispatchManager.Create(this.Dispatch);
            using (XF_DispatchNewEdit form = new XF_DispatchNewEdit(manager))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.Yes)
                {
                    if (form.IsDeleted)
                        ClearDispatch();
                    else
                        this.LoadDispatch(manager.ActiveModel);
                }

                if (DispatchChanged != null)
                    DispatchChanged(this, new DispatchEventArgs(manager.ActiveModel, result));
            }
        }

        private void btnNextDay_Click(object sender, EventArgs e)
        {
            if (this.Dispatch == null)
                return;

            var copy = this.Dispatch.NewCopy();
            copy.FromDateTime = copy.FromDateTime.AddDays(1);
            copy.ToDateTime = copy.ToDateTime.AddDays(1);
            var manager = DispatchManager.Create(copy);
            using (XF_DispatchNewEdit form = new XF_DispatchNewEdit(manager))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.Yes)
                {
                    this.LoadDispatch(manager.ActiveModel);
                }

                if (DispatchChanged != null)
                    DispatchChanged(this, new DispatchEventArgs(this.Dispatch, result));
            }
        }

        private void btnNewDispatch_Click(object sender, EventArgs e)
        {
            DispatchModel disp = new DispatchModel();
            if (this.Dispatch != null)
                disp = this.Dispatch.NewCopy();

            disp.FromDateTime = DateTime.Now.Date.AddHours(9);
            disp.ToDateTime = disp.FromDateTime.AddHours(8);
            disp.CompanyID = 0;
            disp.LocationID = 0;

            var manager = DispatchManager.Create(disp);
            using (XF_DispatchNewEdit form = new XF_DispatchNewEdit(manager))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.Yes)
                {
                    this.LoadDispatch(manager.ActiveModel);
                }

                if (DispatchChanged != null)
                    DispatchChanged(this, new DispatchEventArgs(this.Dispatch, result));
            }
        }
    }
}
