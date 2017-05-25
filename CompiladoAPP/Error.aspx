<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Prueba.Presentacion.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>Agenda - Skytex México</title>
    <!-- asignacion de icono de barra de navegador -->
    <link rel="shortcut icon" href="<%=ResolveUrl("~/Media/Imagenes/skytex.ico") %>" />
    <!-- cargar de archivos de estilos -->
    <!-- estilo de controles de la aplicacion -->
    <link type="text/css" rel="stylesheet" href="<%=ResolveUrl("Content/bootstrap.css")%>" />
    <!-- estilo de ventanas de notificacion emergentes -->
    <link type="text/css" rel="stylesheet" href="<%=ResolveUrl("Content/alertify.core.css")%>" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveUrl("Content/alertify.default.css")%>" />
    <!-- estilo para el grud de daypilot -->
    <link type="text/css" rel="stylesheet" href="<%=ResolveUrl("Content/sezer.css")%>" />
    <!-- estilo para el calendario a la izquierda del grid de daypilot -->
    <link type="text/css" rel="stylesheet" href="<%=ResolveUrl("Content/navigator.css")%>" />
    <!-- carga de archivos JS -->
    <script type="text/javascript" src="<%= ResolveClientUrl("Scripts/modal.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("bin/DayPilot/common.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("Scripts/jquery-1.9.1.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("Scripts/bootstrap.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("bin/DayPilot/calendar.js") %>"></script>    
    <script type="text/javascript" src="<%= ResolveClientUrl("Scripts/jquery-1.9.1.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveClientUrl("crystalreportviewers13/js/crviewer/crv.js")%>" ></script>

    <link type="text/css" rel="stylesheet" href="<%=ResolveUrl("Content/jAlert-v3.css")%>" />
    <script type="text/javascript" src="<%= ResolveClientUrl("Content/jAlert-v3.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveClientUrl("Content/jAlert-functions.js") %>"></script>
    
</head>
<body onload="alRecargar()">
    <form id="form1" runat="server">
    <div>
    <div class="container-fluid">
        <div class="container">
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
            <div class="row">
              <div class="col-md-4">
                  
              </div>
              <div class="col-md-4 center-block">
                  <img src="Media/Imagenes/error.png" width="450" height="350" class="img-responsive text-center"/>
                  <br />
                  <div class="alert alert-danger" role="alert">      
                      <h2><span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span> <strong>Agenda Web</strong></h2>
                      <p>Error al cargar la aplicacion, intente de nuevo</p>
                  </div>
              </div>
              <div class="col-md-4">
                 
              </div>
            </div>
        </div>
    </div>
    </div>
    </form>
</body>
    <script type="text/javascript">

        var bPreguntar = true;

        window.onbeforeunload = preguntarAntesDeSalir;
        var anterior = document.referrer;
        window.onload = alRecargar;

        function alRecargar() {
            PageMethods.cerrarPagina()
        }
        function preguntarAntesDeSalir() {
            PageMethods.cerrarPagina()
        }
        window.onload = function () {
            PageMethods.cerrarPagina()
        };
    </script>
</html>
