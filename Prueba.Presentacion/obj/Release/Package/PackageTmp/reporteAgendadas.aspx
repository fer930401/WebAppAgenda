<%@ Page Title="Reporte Agendadas - Skytex Mexico" Language="C#" MasterPageFile="~/Agenda.Master" AutoEventWireup="true" CodeBehind="reporteAgendadas.aspx.cs" Inherits="Prueba.Presentacion.reporteAgendadas" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-6 col-md-offset-3" >
                <h3><p class="text-justify"><img src="Media/Imagenes/skytex.png" width="190" height="90" />Ordenes Agendadas</p></h3>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-6 col-sm-4">
                <ul class="list-inline">
                  <li><image class="" src="Media/Imagenes/pdf.png" width="40" height="40"/></li>
                  <asp:Button OnClick="exportarPdf_Click" CssClass="btn btn-danger" Text="Exportar PDF" runat="server"/>
                </ul>
            </div>
            <div class="col-xs-6 col-sm-4">
                <a href="reporteAgendadas.aspx">reporteAgendadas.aspx</a>
                <ul class="list-inline">
                  <li><image class="" src="Media/Imagenes/excel.png" width="40" height="40"/></li>
                  <asp:Button OnClick="exportarExcel_Click" CssClass="btn btn-success" Text="Exportar Excel" runat="server"/>
                </ul>
            </div>
            <div class="col-xs-6 col-sm-4">
                <ul class="list-inline">
                  <li><image class="" src="Media/Imagenes/word.png" width="40" height="40"/></li>
                  <li><asp:Button OnClick="exportarWord_Click" CssClass="btn btn-primary responsive" Text="Exportar Word" runat="server"/></li>
                </ul>
            </div>
            <div class="col-md-2 ">
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" DisplayStatusbar="false" HasToggleGroupTreeButton="false" Width="800px"/>
            </div>
        </div>
    </div>
    
</asp:Content>
