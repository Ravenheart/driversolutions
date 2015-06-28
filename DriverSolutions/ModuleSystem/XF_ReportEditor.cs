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
using DevExpress.XtraReports.UserDesigner;
using DriverSolutions.DAL;
using System.IO;
using DevExpress.XtraReports.UI;

namespace DriverSolutions.ModuleSystem
{
    public partial class XF_ReportEditor : DevExpress.XtraEditors.XtraForm
    {
        private DSModel DbContext;

        public XF_ReportEditor()
        {
            InitializeComponent();

            this.Load += XF_ReportEditor_Load;
        }

        void XF_ReportEditor_Load(object sender, EventArgs e)
        {
            this.DbContext = DB.GetContext();
            reportDesigner1.AddCommandHandler(new ReportCommandHandler(reportDesigner1, this.DbContext));

        }
    }

    public class ReportCommandHandler : ICommandHandler
    {
        private XRDesignMdiController Editor;
        private DSModel DbContext;

        public ReportCommandHandler(XRDesignMdiController editor, DSModel db)
        {
            this.Editor = editor;
            this.DbContext = db;
        }

        public bool CanHandleCommand(ReportCommand command, ref bool useNextHandler)
        {
            if (command == ReportCommand.SaveFile || command == ReportCommand.OpenFile || command == ReportCommand.NewReport || command == ReportCommand.NewReportWizard)
            {
                useNextHandler = false;
                return true;
            }

            return false;
        }

        public void HandleCommand(ReportCommand command, object[] args)
        {
            if (command == ReportCommand.SaveFile)
            {
                SaveReport();
            }
            else if (command == ReportCommand.OpenFile)
            {
                OpenReport();
            }
            else if (command == ReportCommand.NewReport || command == ReportCommand.NewReportWizard)
            {
                NewReport();
            }
        }

        private void NewReport()
        {
            using (XF_ReportNew form = new XF_ReportNew())
            {
                if (form.ShowDialog() == DialogResult.Yes)
                {
                    XtraReport report = new XtraReport();
                    report.CreateDocument(false);

                    using (MemoryStream stream = new MemoryStream())
                    {
                        report.SaveLayout(stream);
                        FileObject rep = new FileObject();
                        rep.FileName = form.FileName;
                        rep.FileDescription = form.FileDescription;
                        rep.FileExtension = "repx";
                        rep.FileBlob = stream.ToArray();
                        this.DbContext.Add(rep);
                        this.DbContext.SaveChanges();

                        report.DisplayName = form.FileName;
                        report.Tag = rep.FileID;

                        this.Editor.OpenReport(report);
                        this.Editor.ActiveDesignPanel.ReportState = ReportState.Opened;
                    }
                }
            }
        }

        private void OpenReport()
        {
            using (XF_ReportOpen form = new XF_ReportOpen(this.DbContext))
            {
                if (form.ShowDialog() == DialogResult.Yes)
                {
                    uint fileID = form.FileID;
                    var rep = this.DbContext.FileObjects.Where(f => f.FileID == fileID).FirstOrDefault();
                    if (rep == null)
                        throw new ApplicationException("No report with the specified ID!");

                    using (MemoryStream stream = new MemoryStream(rep.FileBlob))
                    {
                        stream.Position = 0;
                        XtraReport report = new XtraReport();
                        report.LoadLayout(stream);
                        report.Tag = fileID;
                        this.Editor.OpenReport(report);
                        this.Editor.ActiveDesignPanel.ReportState = ReportState.Opened;
                    }
                }
            }
        }

        private void SaveReport()
        {
            uint fileID = (uint)this.Editor.ActiveDesignPanel.Report.Tag;
            var rep = this.DbContext.FileObjects.Where(f => f.FileID == fileID).FirstOrDefault();
            if (rep == null)
                throw new ApplicationException("No report with the specified ID!");

            using (MemoryStream stream = new MemoryStream())
            {
                this.Editor.ActiveDesignPanel.Report.SaveLayout(stream, true);
                stream.Position = 0;
                rep.FileBlob = stream.ToArray();
                this.DbContext.SaveChanges();
                this.Editor.ActiveDesignPanel.ReportState = ReportState.Saved;
            }
        }
    }

}