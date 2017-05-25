using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


namespace Prueba.Presentacion
{
    public partial class reporteAgendadas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ReportDocument rep = new ReportDocument();
            rep.Load(Server.MapPath("Reports/reporteAgendadas.rpt"));
            CrystalReportViewer1.ReportSource = rep;
        }
        public void exportarPdf_Click(object sender, EventArgs e)
        {
            ReportDocument rep = new ReportDocument();
            rep.Load(Server.MapPath("Reports/reporteAgendadas.rpt"));
            CrystalReportViewer1.ReportSource = rep;
            rep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "ReporteOrdenesAgendadas");
        }
        public void exportarExcel_Click(object sender, EventArgs e)
        {
            ReportDocument rep = new ReportDocument();
            rep.Load(Server.MapPath("Reports/reporteAgendadas.rpt"));
            CrystalReportViewer1.ReportSource = rep;
            rep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Response, true, "ReporteOrdenesAgendadas");
        }
        public void exportarWord_Click(object sender, EventArgs e)
        {
            ReportDocument rep = new ReportDocument();
            rep.Load(Server.MapPath("Reports/reporteAgendadas.rpt"));
            CrystalReportViewer1.ReportSource = rep;
            rep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.WordForWindows, Response, true, "ReporteOrdenesAgendadas");
        }
    }
}