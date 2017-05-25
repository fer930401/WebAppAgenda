using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Prueba.Presentacion
{
    public partial class ReporteOrdenes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ReportDocument rep = new ReportDocument();
            rep.Load(Server.MapPath("Reports/ReporteOrdenesExcel.rpt"));
            //rep.DataSourceConnections[0].SetConnection("SQL", "skytex", true);
            //rep.DataSourceConnections[0].SetLogon("soludin_develop", "dinamico20");
            //rep.SetDatabaseLogon("soludin_develop", "dinamico20", "SQL", "skytex", false);
            rep.Refresh();
            rep.SetDatabaseLogon("soludin_develop", "dinamico20", "skyhdev3", "develop", false);
            //rep.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath("Activo/ReportName.pdf"));

            ExportOptions CrExportOptions;
            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            //CrDiskFileDestinationOptions.DiskFileName = @"C:\Desarrollo\Desarrollo_web\AppsPrueba\Agenda\Reporte Ordenes.pdf";
            CrDiskFileDestinationOptions.DiskFileName = @"C:\Users\fernando.garcia\Documents\Repositorios\WebAppAgenda\Reporte Ordenes.pdf";
            //CrDiskFileDestinationOptions.DiskFileName = @"C:\Desarrollo\Desarrollo_web\Agenda\Reporte Ordenes.pdf";
            //CrDiskFileDestinationOptions.DiskFileName = "c:\\Reporte Ordenes.pdf";
            CrExportOptions = rep.ExportOptions;
            {
                CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                CrExportOptions.FormatOptions = CrFormatTypeOptions;
            }
            rep.Export();

            ReportDocument rep2 = new ReportDocument();
            rep2.Load(Server.MapPath("Reports/ReporteOrdenesExcel2.rpt"));
            //rep2.DataSourceConnections[0].SetConnection("SQL", "skytex", true);
            //rep2.DataSourceConnections[0].SetLogon("soludin_develop", "dinamico20");
            //rep2.SetDatabaseLogon("soludin_develop", "dinamico20", "SQL", "skytex", false);
            rep2.Refresh();
            rep2.SetDatabaseLogon("soludin_develop", "dinamico20", "skyhdev3", "develop", false);
            ExportOptions CrExportOptions2;
            DiskFileDestinationOptions CrDiskFileDestinationOptions2 = new DiskFileDestinationOptions();
            ExcelFormatOptions CrFormatTypeOptions2 = new ExcelFormatOptions();
            //CrDiskFileDestinationOptions2.DiskFileName = @"c:\Users\fernando.garcia\Documents\Proyectos Skytex\AplicacionWeb\Prueba.Presentacion\Activo\Reporte Ordenes.xls";
            //CrDiskFileDestinationOptions2.DiskFileName = @"C:\Desarrollo\Desarrollo_web\Agenda\Activo\Reporte Ordenes.xls";
            //CrDiskFileDestinationOptions2.DiskFileName = @"C:\Desarrollo\Desarrollo_web\AppsPrueba\Agenda\Activo\Reporte Ordenes.xls";
            CrDiskFileDestinationOptions2.DiskFileName = @"C:\Users\fernando.garcia\Documents\Repositorios\WebAppAgenda\Activo\Reporte Ordenes.xls";
            CrExportOptions2 = rep2.ExportOptions;
            {
                CrExportOptions2.ExportDestinationType = ExportDestinationType.DiskFile;
                CrExportOptions2.ExportFormatType = ExportFormatType.Excel;
                CrExportOptions2.DestinationOptions = CrDiskFileDestinationOptions2;
                CrExportOptions2.FormatOptions = CrFormatTypeOptions2;
            }
            rep2.Export();

            //var eMailSent = true;
            //var to = "jessica.musi@skytex.com.mx";
            var to = "fernando.garcia@skytex.com.mx";
            //var cc = "geovana.contreras@skytex.com.mx";
            var cc = "fernando.garcia@skytex.com.mx";
            var bcc = "fernando.garcia@skytex.com.mx";
            var emailCandidato = "soludin@skytex.com.mx";
            
            var eMailSubject = "Reporte de Ordenes Agenda Web";
            var eMailMessage =
                "<html lang='en'>" +
                "<head>" +
                    "<style>" +
                        "html { font-family: sans-serif; font-size: 11px -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%;}" +
                        "body { font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; font-size: 14px; line-height: 1.428571429; color: #333333; background-color: #ffffff; } " +
                    "</style>" +
                "</head>" +
                    "<body>" +
                    "<h4> Reporte de Ordenes Agenda Web - Skytex México</h4>" +
                    "<table cellpadding='0' cellspacing='0' width='700'>" +
                     "<tr>" +
                      "<td>" +
                        "<img src='http://i64.tinypic.com/2cwph5l.jpg' width='190' height='90' />" +
                      "</td>" +
                     "</tr>" +
                     "<tr>" +
                      "<td style='padding: 40px 30px 40px 30px;'>" +
                       "<table cellpadding='0' cellspacing='0' width='100%'>" +
                          "<tr>" +
                           "<td width='60%'>" +
                            "<strong>Se genero el reporte con las ordenes ingresadas dentro de la Agenda Web</strong>" +
                           "</td>" +
                          "</tr>" +

                         "</table>" +
                      "</td>" +
                     "</tr>" +
                     "<tr>" +
                      "<td bgcolor='#222222'>" +
                       "<p align='center'><font color= '#ffffff'>En la parte superior se encuentra adjunto el reporte en formato Excel</font></p>" +
                      "</td>" +
                     "</tr>" +
                    "</table>" +
                    "</body>" +
                "</html>";

            MailMessage mail = new MailMessage();
            mail.To.Add(new System.Net.Mail.MailAddress(to));
            //mail.To.Add(new System.Net.Mail.MailAddress("fergarciavera91@gmail.com", "Fernando 2"));
            mail.From = new System.Net.Mail.MailAddress(emailCandidato, "Agenda Web", System.Text.Encoding.UTF8);
            mail.CC.Add(new System.Net.Mail.MailAddress(cc));
            mail.Bcc.Add(new System.Net.Mail.MailAddress(bcc));
            mail.Subject = eMailSubject;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = eMailMessage;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            //attachment = new System.Net.Mail.Attachment(fileAttachment);

            // Agregar el Adjunto si deseamos hacerlo
            System.Net.Mail.Attachment attachment;
            //attachment = new System.Net.Mail.Attachment(@"c:\Users\fernando.garcia\Documents\Proyectos Skytex\AplicacionWeb\Prueba.Presentacion\Activo\Reporte Ordenes.xls");
            //attachment = new System.Net.Mail.Attachment(@"C:\Desarrollo\Desarrollo_web\AppsPrueba\Agenda\Activo\Reporte Ordenes.xls");
            attachment = new System.Net.Mail.Attachment(@"C:\Users\fernando.garcia\Documents\Repositorios\WebAppAgenda\Activo\Reporte Ordenes.xls");
            //attachment = new System.Net.Mail.Attachment(@"C:\Desarrollo\Desarrollo_web\Agenda\Activo\Reporte Ordenes.xls");
            mail.Attachments.Add(attachment);

            // Configuración SMTP
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("outlook.skytex.com.mx", 25);

            // Crear Credencial de Autenticacion
            smtp.Credentials = new System.Net.NetworkCredential("soludin", "pluma");
            smtp.EnableSsl = false;

            try
            {
                smtp.Send(mail);   
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Response.ContentType = "application/pdf";
            rep.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Reporte de ordenes"); // aqui importando el espacio de nombres      
            //CrystalReportViewer1.ReportSource = rep;
        }
        /*public void exportarPdf_Click(object sender, EventArgs e)
        {
            ReportDocument rep = new ReportDocument();
            rep.Load(Server.MapPath("Reports/ReporteOrdenes.rpt"));
            CrystalReportViewer1.ReportSource = rep;
            rep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "Reporte de Ordenes");
            var eMailSent = true;
            var to = "fernando.garcia@skytex.com.mx";
            var oferta = "Prueba Email";
            var nomCandidato = "Fernando";
            var emailCandidato = "fer_930401@hotmail.com";
            var comentarios = Request["comentarios"];
            //var fileAttachment = Request["file"];
            //var nuevoAttch = System.IO.File.Exists(fileAttachment);

            var eMailSubject = "Candidato Para: " + oferta;
            var eMailMessage =
                "<html lang='en'>" +
                "<head>" +
                    "<style>" +
                        "html { font-family: sans-serif; font-size: 11px -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%;}" +
                        "body { font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; font-size: 14px; line-height: 1.428571429; color: #333333; background-color: #ffffff; } " +
                    "</style>" +
                "</head>" +
                    "<body>" +
                    "<h4>Candidatura Para: " + oferta + " - Skytex Mexico</h4>" +
                    "<table cellpadding='0' cellspacing='0' width='700'>" +
                     "<tr>" +
                      "<td>" +
                        "<img src='http://i66.tinypic.com/2ymvn1j.png' width='190' height='90' />" +
                      "</td>" +
                     "</tr>" +
                     "<tr>" +
                      "<td style='padding: 40px 30px 40px 30px;'>" +
                       "<table cellpadding='0' cellspacing='0' width='100%'>" +
                          "<tr>" +
                           "<td width='40%'>" +
                            "<strong>Candidato:</strong>" +
                           "</td>" +
                           "<td>" +
                             nomCandidato.ToUpper() +
                           "</td>" +
                          "</tr>" +

                          "<tr>" +
                           "<td>" +
                            "<strong>Se A Postulado Para La Oferta De Trabajo:</strong>" +
                           "</td>" +
                           "<td>" +
                             oferta +
                           "</td>" +
                          "</tr>" +

                          "<tr>" +
                           "<td>" +
                            "<strong>Email De Contacto:</strong>" +
                           "</td>" +
                           "<td>" +
                             emailCandidato +
                           "</td>" +
                          "</tr>" +

                          "<tr>" +
                           "<td>" +
                            "<strong>Comentarios Hechos Por El Candidato:</strong>" +
                           "</td>" +
                           "<td>" +
                             comentarios +
                           "</td>" +
                          "</tr>" +

                         "</table>" +
                      "</td>" +
                     "</tr>" +
                     "<tr>" +
                      "<td bgcolor='#222222'>" +
                       "<p align='center'><font color= '#ffffff'>En la parte superior se encuentra adjunto el CV del candidato</font></p>" +
                      "</td>" +
                     "</tr>" +
                    "</table>" +
                    "</body>" +
                "</html>";

            MailMessage mail = new MailMessage();
            mail.To.Add(new System.Net.Mail.MailAddress(to));
            mail.From = new System.Net.Mail.MailAddress(emailCandidato, eMailSubject, System.Text.Encoding.UTF8);
            mail.Subject = eMailSubject;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = eMailMessage;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            //attachment = new System.Net.Mail.Attachment(fileAttachment);

            // Agregar el Adjunto si deseamos hacerlo
            System.Net.Mail.Attachment attachment;
            //attachment = new System.Net.Mail.Attachment(Server.MapPath("C://Counter.txt"));
            //mail.Attachments.Add(attachment);

            // Configuración SMTP
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);

            // Crear Credencial de Autenticacion
            smtp.Credentials = new System.Net.NetworkCredential("fergarciavera91@gmail.com", "sharingan930401");
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(mail);
                Response.Write("<script language='javascript' type='text/javascript'>alert('Datos Enviado Correctamente\\nSigue Consultando Nuestras Ofertas de Trabajo'); window.location.href='bolsaTrabajo';</script>");
                //return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void exportarExcel_Click(object sender, EventArgs e)
        {
            ReportDocument rep = new ReportDocument();
            rep.Load(Server.MapPath("Reports/ReporteOrdenesExcel2.rpt"));
            rep.DataSourceConnections[0].SetConnection("SQL", "skytex", true);
            rep.DataSourceConnections[0].SetLogon("soludin_develop", "dinamico20");
            rep.Refresh();
            CrystalReportViewer1.ReportSource = rep;
            rep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, true, "Reporte de Ordenes");
        }
        public void exportarWord_Click(object sender, EventArgs e)
        {
            ReportDocument rep = new ReportDocument();
            rep.Load(Server.MapPath("Reports/ReporteOrdenes.rpt"));
            CrystalReportViewer1.ReportSource = rep;
            rep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.WordForWindows, Response, true, "Reporte de Ordenes");
        }
        
        protected void CrystalReportViewer1_Init(object sender, EventArgs e)
        {
            ReportDocument rep = new ReportDocument();
            rep.Load(Server.MapPath("Reports/ReporteOrdenesExcel.rpt"));
            CrystalReportViewer1.ReportSource = rep;
        }
        */
    }
}