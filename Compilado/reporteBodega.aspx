<%@ Page Title="Reporte Ordenes Bodega - Skytex Mexico" Language="C#" MasterPageFile="~/Agenda.Master" AutoEventWireup="true" CodeBehind="reporteBodega.aspx.cs" Inherits="Prueba.Presentacion.reporteBodega" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-6 col-md-offset-3" >
                <h3><p class="text-justify"><img src="Media/Imagenes/skytex.png" width="190" height="90" />Ordenes en Bodega</p></h3>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-6 col-sm-4">
                <ul class="list-inline">
                  <li><img class="" src="Media/Imagenes/pdf.png" width="40" height="40"/></li>
                  <asp:Button OnClick="exportarPdf_Click" CssClass="btn btn-danger" Text="Exportar PDF" runat="server"/>
                </ul>
            </div>
            <div class="col-md-2 ">
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" BestFitPage="False" ToolPanelView="None"/>
            </div>
        </div>
    </div>
</asp:Content>
