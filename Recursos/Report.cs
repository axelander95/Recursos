using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Reporting.WinForms;
using Microsoft.Reporting.WinForms.Internal;
namespace Recursos
{
    class Report
    {
        public ReportViewer Viewer { get; set; }
        public List <ReportDataSource> DataSource { get; set; }
        public List<ReportParameter> Parameters { get; set; }
        public Report(ReportViewer Viewer, List<ReportDataSource> DataSource, List <ReportParameter> Parameters)
        {
            this.Viewer = Viewer;
            this.DataSource = DataSource;
            this.Parameters = Parameters;
        }
        public void Load(string Path){
            Viewer.Clear();
            Viewer.LocalReport.ReportEmbeddedResource = Path;
            Viewer.LocalReport.SetParameters(Parameters);
            foreach (ReportDataSource ReportData in DataSource)
                Viewer.LocalReport.DataSources.Add(ReportData);
            Viewer.LocalReport.Refresh();
        }
    }
}
