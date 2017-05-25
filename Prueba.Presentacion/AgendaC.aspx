<%@ Page Title="" Culture="es-MX" Language="C#" MasterPageFile="~/Agenda.Master" AutoEventWireup="true" CodeBehind="AgendaC.aspx.cs" Inherits="Prueba.Presentacion.AgendaC" %>
<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="REFRESH" content="0;URL=Agenda.aspx"/>
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
          width: 100%;
          height: 100%;
        }
    </style>
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
            dpn.visibleRangeChangedCallBack(); // update free/busy after adding/changing/deleting events in the calendar
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <br />
    <div class="container-fluid">
        <div class="container">
            <div class="row">
              <div class="col-md-2">
                  <img src="Media/Imagenes/skytex.png" width="200" height="80" />
                  
                  <br/>
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
                        CellHeight="17"   >
                      </DayPilot:DayPilotNavigator> 
                  </div>
              </div>
              <!-- configuracion del grid de daypilot mas configuraciones en: http://www.daypilot.org/calendar/ -->
              <div class="col-xs-14 col-sm-8 col-md-10">
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
                    EventMoveJavaScript="dpc1.eventMoveCallBack(e, newStart, newEnd, oldColumn, newColumn);"
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
              <!-- listado de ordenes pendientes -->
              <div class="col-md-6">
                  <div class="alert alert-info" role="alert">
                    <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                    <strong>Por Embarcar</strong> <asp:Label ID="Label1" runat="server"></asp:Label>
                  </div>
                  <ul id="e2" style="height:170px; overflow: scroll;" class="list-unstyled">
                    <asp:Repeater ID="Repeater1"  runat="server">
                        <ItemTemplate>
                            <li onclick="infoClick(<%# Eval("num_fol") %>, '<%#Eval("Articulos") %>')" data-id="<%# Eval("num_fol") %>" data-duration="1800"><span style="cursor:move" class="glyphicon glyphicon-play-circle"><%# Eval("num_fol") %> - <%# Eval("fec_mov_ttant") %> - <%# Eval("art_cve") %> - <%# Eval("Articulos") %></span></li>
                        </ItemTemplate>
                    </asp:Repeater>
                 </ul>
              </div>
              <!-- listado de ordenes en bodega -->
              <div class="col-md-6">
                  <div class="alert alert-info" role="alert">
                    <span class="glyphicon glyphicon-list-alt" aria-hidden="true"></span>
                    <strong>Bodega Skytex</strong>
                  </div>
                    <ul id="b" style="height:170px; overflow: scroll;" class="list-unstyled">
                    <asp:Repeater ID="Repeater2" runat="server" >
                        <ItemTemplate>
                            <li onclick="infoClickB(<%# Eval("id_batch") %>, '<%#Eval("nom_bact") %>')" data-id="<%# Eval("id_batch") %>" data-duration="1800"><span style="cursor:move" class="glyphicon glyphicon-import"><%#Eval("nom_bact") %></span></li>
                        </ItemTemplate>
                    </asp:Repeater>
                 </ul>
              </div>
            </div>        
        </div>
            <div class="overlay" onload="redireccionar()" />
            <div class="overlayContent">
                <h2>Cargando...</h2>
                <img src="Media/Imagenes/Loading3.gif" alt="Loading" width="200" height="300"/>
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
                Text="Enviar A Listado 'Por Embarcar'" 
                Image='Media/Imagenes/pendiente.gif'
                Action="PostBack"
                Command="pendiente" >
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
    function infoClick(id, nombre) {
        reset();
        alertify.alert("El Numero De Folio De La Orden Es : <b>" + id + "</b>, El Articulo es: <b>" + nombre + "</b>");
        return false;
    }
    function infoClickB(id, nombre) {
        reset();
        alertify.alert("La Informacion De La Orden En Bodega Es: <b>" + nombre + "</b>");
        return false;
    }
</script>
<script>
    /*setTimeout("location.reload(true);", 5000);
    location.href = "Agenda.aspx";*/
</script>
</asp:Content>
