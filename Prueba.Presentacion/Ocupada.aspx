<%@ Page Title="" Language="C#" MasterPageFile="~/Agenda.Master" AutoEventWireup="true" CodeBehind="Ocupada.aspx.cs" Inherits="Prueba.Presentacion.Ocupada" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="container">
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <div class="row">
              <div class="col-md-4">
                  
              </div>
              <div class="col-md-4 center-block">
                  <img src="Media/Imagenes/usoApp.png" width="350" height="350" class="img-responsive text-center"/>
                  <div class="alert alert-danger" role="alert">      
                      <h2><span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span> <strong>Agenda Web</strong></h2>
                      <p>La Aplicacion esta siendo usada por otro usuario en este momento, intenta mas tarde</p>
                  </div>
              </div>
              <div class="col-md-4">
                 
              </div>
            </div>
        </div>
    </div>
</asp:Content>
