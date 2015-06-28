using DevExpress.XtraReports.UI;
using DriverSolutions.BOL.Models.ModuleReports;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DriverSolutions.BOL
{
    public class ReportBinder
    {
        public static void ShowReport(ReportFile report)
        {
            var xtraRep = ReportBinder.BindReport(report);
            xtraRep.ShowPreviewDialog();
        }

        public static void ShowReport(uint fileID, DataSet data)
        {
            using (var db = DB.GetContext())
            {
                var file = db.FileObjects.Where(f => f.FileID == fileID).FirstOrDefault();

                ReportBinder.ShowReport(new ReportFile(data, file.FileBlob));
            }
        }

        public static XtraReport BindReport(ReportFile report)
        {
            //report.DataSource.WriteXmlSchema("rep.xml");
            XtraReport rep = new XtraReport();
            using (MemoryStream stream = new MemoryStream(report.Report))
            {
                rep.LoadLayout(stream);
                BindingSource bsData = new BindingSource();
                bsData.DataSource = report.DataSource;
                rep.DataSource = bsData;
                rep.CreateDocument();
                return rep;
            }
        }

        public static XtraReport BindReport(uint fileID, DataSet data)
        {
            using (var db = DB.GetContext())
            {
                var file = db.FileObjects.Where(f => f.FileID == fileID).FirstOrDefault();

                return ReportBinder.BindReport(new ReportFile(data, file.FileBlob));
            }
        }
    }
}
