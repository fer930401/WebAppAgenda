<%@ Page Title="" Culture="es-MX" Language="C#" MasterPageFile="~/Agenda.Master" AutoEventWireup="true" CodeBehind="Agenda.aspx.cs" Inherits="Aplicacion_Web.Agenda1" Debug="false" EnableViewState="false" ViewStateMode="Disabled" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <style type="text/css">
        .overlay  
        {
          position: fixed;
          z-index: 98;
          top: 0px;
          left: 0px;
          right: 0px;
          bottom: 0px;
            background-color: #aaa; 
            filter: alpha(opacity=80); 
            opacity: 0.8; 
        }
        .overlayContent
        {
          z-index: 99;
          margin: 250px auto;
          width: 80px;
          height: 80px;
        }
        .overlayContent h2
        {
            font-size: 18px;
            font-weight: bold;
            color: #000;
        }
        .overlayContent img
        {
          width: 80px;
          height: 80px;
        }
    </style>
    <script type="text/javascript">

        var bPreguntar = true;

        window.onbeforeunload = preguntarAntesDeSalir;
        var anterior = document.referrer;
        window.onload = alRecargar;
        
        function alRecargar() {
           PageMethods.alRecargar()
        }
        function preguntarAntesDeSalir() {
            PageMethods.cerrarPagina()
        }
        function enviarBodega(num_fol, nombre, tipDoc,ef_cve) {
            var array = [num_fol, nombre, tipDoc, ef_cve];
            if (confirm("Enviar a Bodega Skytex la orden Nº" + num_fol + "?") == true) {
                dpc1.commandCallBack('envioBodega', array);
                setTimeout("location.href='AgendaC.aspx'");
            } else {
                alert("No se envio a Bodega Skytex");
            }
        }
        function enviarEntregar(num_fol, nombre, tipDoc, ef_cve) {
            var arrayE = [num_fol, nombre, tipDoc, ef_cve];
            if (confirm("Enviar a 'Por Entregar Bodega Skytex' la orden Nº" + num_fol + "?") == true) {
                dpc1.commandCallBack('envioEntregar', arrayE);
            } else {
                alert("No se envio a 'Por Entregar Bodega Skytex'");
            }
        }
    </script>
    <script type="text/javascript">
        /* Event editing helpers - modal dialog */
        function dialog() {
            var modal = new DayPilot.Modal();
            modal.top = 100;
            modal.dragDrop = true;
            modal.width = 800;
            modal.opacity = 70;
            modal.border = "10px solid #d0d0d0";
            modal.closed = function (args) {
                if (this.result == 'OK') {
                    dpc1.commandCallBack('nuevaFecha');
                } else if (this.result == 'error') {
                    dpc1.commandCallBack('error');
                }
                dpc1.clearSelection();
            };

            modal.height = 300;
            modal.zIndex = 100;
            return modal;
        }
        function edit(e) {
            var modal = dialog();
            modal.showUrl("nuevaFecha.aspx?id=" + e.id());
        }
        function afterRender(isCallBack) {
            dpn.visibleRangeChangedCallBack();
//nueva linea
        }
    </script>
    
    <script>
        $(document).ready(function () {

            $(document).bind("contextmenu", function (e) {
                $("#menu").css({ 'display': 'block', 'left': e.pageX, 'top': e.pageY });
                return false;
            });
        });
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div class="container-fluid">
        <div class="container">
            <div class="row">
              <div class="col-md-2">
                  <img src="Media/Imagenes/skytex.png" width="200" height="70" />
                  
                  <div class="col-md-offset-2">
                      <!-- configuracion del calendario a la izquierda del grid de daypilot -->
                      <DayPilot:DayPilotNavigator ID="DayPilotNavigator1" runat="server" 
                        ClientObjectName ="dpn"
                        ShowMonths="2"
                        SkipMonths="2"
                        BoundDayPilotID="DayPilotCalendar1" 
                        SelectMode="Day"
                        CssClassPrefix="navigator"
                        ShowWeekNumbers="true"
                        DataStartField="start"
                        DataEndField="end"
                        CellHeight="15"                
                           >
                      </DayPilot:DayPilotNavigator> 
                      <strong>Color de los Status</strong><br />
                      <div class="alert-entrega" role="alert"> - Disponible Entrega</div>
                      <div class="alert-pedimento" role="alert"> - Pedimento</div>
                      <div class="alert-embarcada" role="alert"> - Embarcada</div>
                      <br />
                      <div class="alert-conTransito" role="alert"> - Orden Con Transito</div>
                      <div class="alert-sinTransito" role="alert"> - Orden Sin Transito</div>
                  </div>
              </div>
              <!-- configuracion del grid de daypilot mas configuraciones en: http://www.daypilot.org/calendar/ -->
              <div class="col-xs-14 col-sm-8 col-md-10">
                  <br />
                  <DayPilot:DayPilotCalendar 
                    ID="DayPilotCalendar1" 
                    runat="server" 
                    OnBeforeEventRender="DayPilotCalendar1_BeforeEventRender" 
                    Width="100%"
                    DataEndField="end"
                    ClientObjectName="dpc1" 
                    DataStartField="start" 
                    DataTextField="name" 
                    DataIdField="id" 
                    DataColumnField="cve"
                    OnEventMove="DayPilotCalendar1_EventMove" 
                    ContextMenuID="DayPilotMenu1" 
                    LoadingLabelText="Cargando..." 
                    BusinessBeginsHour="8" 
                    BusinessEndsHour="20" 
                    ViewType="Days" 
                    OnCommand="DayPilotCalendar1_Command"
                    CrosshairColor="Green" 
                    CssClassPrefix="sezer" 
                    DataValueField="id" 
                    DurationBarWidth="5" 
                    HoverColor="Red" 
                    LoadingLabelBackColor="Gray" 
                    LoadingLabelFontColor="255, 255, 255" 
                    ShowAllDayEventStartEnd="true" 
                    TimeFormat="Clock12Hours"
                    OnEventMenuClick="DayPilotCalendar1_EventMenuClick"
                    EventMoveHandling = "PostBack"
                    EventClickHandling="JavaScript"
                    EventClickJavaScript="edit(e)"
                    EventArrangement="Full"
                    EventResizeHandling = "CallBack"
                    OnEventResize="DayPilotCalendar1_EventResize"
                    EventMoveJavaScript="dpc1.eventMoveCallBack(e, newStart, newEnd, oldColumn, newColumn, data);"
                    Days="5"
                    DayBeginsHour="1"
                    ScrollPositionHour="8" 
                    ScrollLabelsVisible="true" 
                    Direction="Auto" 
                    DurationBarColor="Blue" 
                    WeekStarts="Auto"
                    CellHeight="17"
                    HeaderDateFormat="dd/MM/yyyy"
                         >
                 </DayPilot:DayPilotCalendar>
              </div>
            </div>
        <br />
        <div id="external">
            <div class="row">
              <!-- listado de ordenes Por Entregar -->                
              <div class="col-md-6">
                  <div class="alert alert-info" role="alert">
                    <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                    <strong>Por Entregar</strong> <asp:Label ID="Label1" runat="server"></asp:Label>
                  </div>
                  <ul id="e" style="height:170px; overflow: scroll;" class="list-unstyled">
                    <asp:Repeater ID="Repeater1"  runat="server" >
                        <ItemTemplate>
                            <div class="<%# colorFondo(Eval("status_entrega").ToString()) %>" role="alert">
                                <li data-id="<%# Eval("num_fol") %>" data-duration="1800" onclick="infoClickParisina('<%#Eval("num_fol") %>','<%#Eval("Articulos") %>')" oncontextmenu="enviarBodega('<%#Eval("num_fol") %>','<%# Eval("num_fol") %> - <%# Eval("fec_mov_ttant","{0:dd/MM/yyyy}") %> - <%# Eval("art_cve") %> - <%# Eval("Articulos").ToString().TrimEnd() %> - <%# Eval("uns_vta") %> - <%# Eval("uni_med").ToString().TrimEnd() %> - <%# Eval("reng") %>','<%# Eval("tipo_doc") %>','<%# Eval("ef_cve") %>')" onload="fondoRenglon('<%# Eval("status_entrega") %>',<%# Eval("num_fol") %>)"><span style="cursor:move;" class="glyphicon glyphicon-play-circle"><%# Eval("num_fol") %> - <%# Eval("fec_mov_ttant","{0:yyyyMMdd}") %> - <%# Eval("art_cve") %> - <%# Eval("Articulos") %> - <%# Eval("uns_vta") %> - <%# Eval("uni_med") %> - <%# Eval("reng") %></span></li>
                                <!--<li data-id="<# Eval("num_fol") %>" data-duration="1800" onclick="infoClickParisina('<#Eval("num_fol") %>','<#Eval("Articulos") %>')" oncontextmenu="enviarBodega('<#Eval("num_fol") %>','<# Eval("num_fol") %> - <# Eval("fec_mov_ttant","{0:dd/MM/yyyy}") %> - <# Eval("art_cve") %> - <# Eval("Articulos") %> - <# Eval("uns_vta") %> - <# Eval("uni_med") %>','<# Eval("tipo_doc") %>','<# Eval("ef_cve") %>')" onload="fondoRenglon('<# Eval("status_entrega") %>',<# Eval("num_fol") %>)"><span style="cursor:move;" class="glyphicon glyphicon-play-circle"><# Eval("num_fol") %> - <# Eval("fec_mov_ttant","{0:yyyyMMdd}") %> - <# Eval("art_cve") %> - <# Eval("Articulos") %> - <# Eval("uns_vta") %> - <# Eval("uni_med") %></span></li>-->
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                 </ul>
              </div>
              <!-- listado de ordenes en bodega Skytex -->
              <div class="col-md-6">
                  <div class="alert alert-info" role="alert">
                    <span class="glyphicon glyphicon-list-alt" aria-hidden="true"></span>
                    <strong>Bodega Skytex</strong> <asp:Label ID="Label2" runat="server"></asp:Label>
                  </div>
                    <ul id="b" style="height:170px; overflow: scroll;" class="list-unstyled">
                    <asp:Repeater ID="Repeater2" runat="server" >
                        <ItemTemplate>
                            <div class="<%# colorFondo2(Eval("num_reng").ToString()) %> " role="alert">
                                <li onclick="infoClickSkytex('<%#Eval("fec_ins") %>', '<%#Eval("nom_bact") %>')" data-id="<%# Eval("id_batch") %>" oncontextmenu="enviarEntregar('<%#Eval("id_batch") %>','<%# Eval("nom_bact") %>','<%# Eval("doc_dispo") %>>','<%# Eval("ef_cve") %>')" data-duration="1800"><span style="cursor:move" class="glyphicon glyphicon-import"><%#Eval("nom_bact") %></span></li>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                 </ul>
              </div>
            </div>  
            <div class ="row">
              <div class="col-md-12">
                  
              </div>
              <div class="col-md-1">
                  .
              </div>
              <div class="col-md-2">
                <strong>Status de Ordenes</strong><br />
                <div class="alert-Agendada" role="alert"> - Agendada</div>
                <div class="alert-Entregada" role="alert"> - Entregada / Descargada</div>
                <div class="alert-Vencida" role="alert"> - Vencida</div>
              </div>
              <div class="col-md-7">
                  <div class="alert alert-info" role="alert">
                    <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                    <strong>Agendadas / Entregadas / Vencidas</strong> <asp:Label ID="Label3" runat="server"></asp:Label>
                  </div>
                    <ul id="b" style="height:170px; overflow: scroll;" class="list-unstyled">
                    <asp:Repeater ID="Repeater3" runat="server" >
                        <ItemTemplate>
                            <div class="<%# colorFondo3(Eval("status_proceso").ToString()) %> " role="alert">
                                <li onclick="infoClickSkytex('<%#Eval("fec_ins") %>', '<%#Eval("nom_bact") %>')" data-id="<%# Eval("id_batch") %>" data-duration="1800"><span style="cursor:move" class="glyphicon glyphicon-import"><%#Eval("nom_bact") %></span></li>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                 </ul>
              </div>
            </div>      
        </div>
        <daypilot:daypilotmenu 
            id="DayPilotMenu1" 
            runat="server"
            ShowMenuTitle="false">
            <DayPilot:MenuItem 
                Text="Descargado" 
                Image='Media/imagenes/ok.jpg' 
                Action="CallBack" 
                Command="confirmar" >
            </DayPilot:MenuItem>
            <DayPilot:MenuItem 
                Text="Enviar A Bodega Skytex" 
                Image='Media/Imagenes/bodega.jpg' 
                Action="PostBack"
                Command="bodega" >
            </DayPilot:MenuItem>
            <DayPilot:MenuItem 
                Text="Enviar A Listado 'Por Entregar'" 
                Image='Media/Imagenes/pendiente.gif'
                Action="PostBack"
                Command="pendiente">
            </DayPilot:MenuItem>
        </daypilot:daypilotmenu>
    </div>
</div>

<script type="text/javascript">
    var parent = document.getElementById("external");
    var items = parent.getElementsByTagName("li");
        for (var i = 0; i < items.length; i++) {
            var e = items[i];
            var item = {
                element: e,
                id: e.getAttribute("data-id"),
                text: e.innerText,
                duration: e.getAttribute("data-duration")
            };
            DayPilot.Calendar.makeDraggable(item);
        }
</script>

<script type="text/javascript" src="<%= ResolveUrl("~/Scripts/alertify.min.js") %>"></script>
<script>
    /* 
    funcion que reinicia las ventanas de notificacion despues de ser cerradas por el usuario
    */
    function reset() {
        $("#toggleCSS").attr("href", "~/Content/alertify.default.css");
        alertify.set({
            labels: {
                ok: "Aceptar",
                cancel: "Cancel"
            },
            delay: 5000,
            buttonReverse: false,
            
        });
    }
    /* 
    funcion que muestra la ventana de notificacion al momento de dar click a algun elemento 
    de las listas de ordenes 
    */
    function infoClickParisina(id, nombre) {
        reset();
        alertify.alert("El Numero De Folio De La Orden Es : <b>" + id +"</b>, El Articulo es: <b>"+ nombre+"</b>");
        return false;
    }
    function infoClickSkytex(id, nombre) {
        reset();
        alertify.alert("La Informacion De La Orden En Bodega Es: <b>" + nombre + "</b>, Fecha En La Que Se Agendo: <b>"+ id + "</b>");
        return false;
    }
    
    function fondoRenglon(estatus, num_folio) {
            
            if (estatus=='A) Disponible Entrega') {
                alert("es disponible" + num_folio);
            } else if (estatus=="B) Pedimento") {
                alert("es pedimento" + num_folio);
            } else if (estatus == 'C) Embarcada') {
                alert("es embarcada" + num_folio);
            } else if (estatus == 'Recepcion') {
                alert("es recepcion sin OC" + num_folio);
            } else {
                alert("color");
            }
        
        }
</script>
</asp:Content>


