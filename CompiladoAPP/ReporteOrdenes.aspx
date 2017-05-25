<%@ Page Title="Reporte Ordenes - Skytex Mexico" Language="C#" MasterPageFile="~/Agenda.Master" AutoEventWireup="true" CodeBehind="ReporteOrdenes.aspx.cs" Inherits="Prueba.Presentacion.ReporteOrdenes" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-6 col-md-offset-3" >
                <h3><img src="Media/Imagenes/skytex.png" width="190" height="90" />Ordenes en Bodega</h3>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-6 col-sm-4">
                <ul class="list-inline">
                  <li><img class="" src="Media/Imagenes/pdf.png" width="40" height="40"/></li>
                  <!--<asp:Button  CssClass="btn btn-danger" Text="Exportar PDF" runat="server" />-->
                </ul>
            </div>
        </div>
        <!--<CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />-->
    </div>
</asp:Content>
