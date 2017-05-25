<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Prueba.Presentacion.Inicio"  Debug="false" EnableViewState="false" ViewStateMode="Disabled"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Agenda - Skytex Mexico</title>
    <link rel="shortcut icon" href="<%=ResolveUrl("~/Media/Imagenes/skytex.ico") %>" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveUrl("~/Content/loading.css")%>" />
    <script type="text/javascript">

        var bPreguntar = true;

        window.onbeforeunload = preguntarAntesDeSalir;
        //window.onload = alRecargar;

        function alRecargar() {
            PageMethods.alRecargar()
        }
        function preguntarAntesDeSalir() {
            
            PageMethods.cerrarPagina()
        }

    </script>
</head>
    <body onload="redireccionar()">
        <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <div class="row">
          <div class="col-md-3 ">
              <img class="img-responsive" src="Media/Imagenes/skytex.png" width="400" height="130" />
          </div>
          <div class="col-md-3">
              <div class="loader">
                <span>L</span>
                <span>O</span>
                <span>A</span>
                <span>D</span>
                <span>I</span>
                <span>N</span>
                <span>G</span>
  
                <div class="covers">
                    <span></span>
                    <span></span>
                    <span></span>
                    <span></span>
                    <span></span>
                    <span></span>
                    <span></span>
                </div>
            </div>
          </div>
        </div> 
        </form>    
    </body>
    
    <script>
        function redireccionar() {
            PageMethods.cerrarPagina()
            setTimeout("location.href='Agenda.aspx'");
        }
    </script>
</html>
