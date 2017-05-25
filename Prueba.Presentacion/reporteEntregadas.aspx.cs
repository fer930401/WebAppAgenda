using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Excel = Microsoft.Office.Interop.Excel;

namespace Prueba.Presentacion
{
    public partial class reporteEntregadas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ReportDocument rep = new ReportDocument();
            rep.Load(Server.MapPath("Reports/reporteEntregadas.rpt"));
            CrystalReportViewer1.ReportSource = rep;
        }
        public void exportarPdf_Click(object sender, EventArgs e)
        {
            ReportDocument rep = new ReportDocument();
            rep.Load(Server.MapPath("Reports/reporteEntregadas.rpt"));
            CrystalReportViewer1.ReportSource = rep;
            rep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "ReporteOrdenesConfirmadas");
        }
        public void exportarExcel_Click(object sender, EventArgs e)
        {
            
        }
        public void exportarWord_Click(object sender, EventArgs e)
        {
            ReportDocument rep = new ReportDocument();
            rep.Load(Server.MapPath("Reports/reporteEntregadas.rpt"));
            CrystalReportViewer1.ReportSource = rep;
            rep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.WordForWindows, Response, true, "ReporteOrdenesConfirmadas");
        }
    }
}