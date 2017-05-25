using System;
using System.Data;
using System.Drawing;
using System.Text;
using DayPilot.Json;
using DayPilot.Utils;
using DayPilot.Web.Ui;
using DayPilot.Web.Ui.Enums;
using DayPilot.Web.Ui.Events;
using DayPilot.Web.Ui.Events.Bubble;
using DayPilot.Web.Ui.Events.Scheduler;
using DayPilot.Web.Ui.Events.Calendar;
using System.Collections.Generic;
using System.Configuration;
using Prueba.LogicaNegocio;
using Prueba.Entidades;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Collections;
using System.Web.UI.WebControls;
using Prueba.Presentacion;



namespace Aplicacion_Web
{
    public partial class Agenda1 : System.Web.UI.Page
    {
        //instancia de logicaNegocio.cs
        LogicaNegocioCls logicaNegocio;
        //declaracion de la variable table del tipo DataTable
        DataTable table;
        string mensaje = "";
        short? error = 0;
        protected void Application_Error(Object sender, EventArgs e)
        {
            Session["CurrentError"] = "Global: " +
                Server.GetLastError().Message;
            Response.Redirect("Error.aspx");
        }

        // metodo principal que carga la pantalla al momento de ejecutar la aplicacion
        protected void Page_Load(object sender, EventArgs e)
        {
            //inicializacion del objeto logicaNegocio
            logicaNegocio = new LogicaNegocioCls();
            
            //llamado al metodo initdata para llenar la informacion del grid de la agenda
            initData();
                
            if (!IsPostBack)
            {
                #region Data loading initialization
                /* 
                 * inicializacion de metodos para la carga de informacion, initData = carga los datos en el grid
                 * llenaLista1 llena la lista de pendientes
                 * llenaLista2 llena la lista de bodega
                */
                initData();
                LlenaBodegaParisina();
                LlenaBodegaSky();
                LlenaPlaneacion();
                #endregion
            }
            
        }

        private void initData()
        //private void initData(short status, string opc, string user_cve, string ef_cve)
        {
            if (Session["AllFeatures"] == null)
            {
                Session["AllFeatures"] = DataGeneratorCalendar.GetData();
            }
            if (Session["fechaAgendada"] != null)
            {
                if (Session["command"].ToString().Equals("navigate") == false)
                {
                    DayPilotCalendar1.StartDate = (DateTime)Session["fechaAgendada"];
                }
            }
            else 
            {
                //DayPilotCalendar1.StartDate = System.DateTime.Now;
            }
            table = (DataTable)Session["AllFeatures"];
            DayPilotCalendar1.DataSource = Session["AllFeatures"];
            DayPilotNavigator1.DataSource = Session["AllFeatures"];
            //obtiene la informacion de la busqueda configurada en la clase DataGeneradorCalendar
            DayPilotCalendar1.DataSource = DataGeneratorCalendar.GetData();
            //DayPilotCalendar1.DataSource = DataGeneratorCalendar.GetData(status, opc, user_cve, ef_cve);
            //se le asigna los datos a el grid de daypilot
            DayPilotCalendar1.DataBind();
            //asignacion de la informacion a la variable table
            table = (DataTable)DataGeneratorCalendar.GetData();
            //table = (DataTable)DataGeneratorCalendar.GetData(status, opc, user_cve, ef_cve);
            
        }

        //metodo que se llama cuando se detecta algun movimiento dentro del grid de daypilot
        protected void DayPilotCalendar1_EventMove(object sender, DayPilot.Web.Ui.Events.EventMoveEventArgs e)
        {            
            //se asigna el valor del elemento seleccionado de las listas ya esta dentro del grid
            DataRow dr = table.Rows.Find(e.Id);
            session.appEnUso();
            //si el elemento ya existe en el grid
            if (dr != null)
            {
                //se procede a actualizar la fecha de inicio y la fecha fin de la orden en la base de datos
                Prueba.Entidades.sp_WebAppInsInfo_Result ActualizarOrden = logicaNegocio.InsInfoAgenda("001", e.NewStart, e.NewEnd, "0", e.Id, e.Text, "", "movido", "lcAgen", "ZZZ");
                
                //si la actualizacion regresa algun error(si se quiere mover a una fecha menor a la actual), 
                //el error se muestra en la pantalla
                if (ActualizarOrden != null)
                {
                    error = ActualizarOrden.error;
                    mensaje = ActualizarOrden.ToString();
                    //si no se regreso ningun error
                    if (Convert.ToInt32(error) == 0)
                    {
                        //se actualiza el grid para ver los cambios (las ordenes reagendadas se muestran en color gris)
                        Session["fechaAgendada"] = e.NewEnd;
                        Session["command"] = "movido";
                        Response.Redirect("AgendaC.aspx");
                    }
                    else
                    {
                        //si se quiere mover a una fecha menor a la actual, mostrara el mensaje de error
                        Response.Write("<script type=\"text/javascript\">alert('Error. Fecha Pasada, No se Puede Mover'); window.location.href = 'Agenda.aspx';</script>");
                    }
                }
            }
            else //si el elemento no existe en el grid            
            {
                //se procede a insertar la informacion de la orden dentro de la base de datos
                Prueba.Entidades.sp_WebAppInsInfo_Result NuevaOrden = logicaNegocio.InsInfoAgenda("001", e.NewStart, e.NewEnd, "0", e.Id.ToString(), e.Text, "", "agendado", "lcAgen", "ZZZ");

                //si la insercion regreso algun error (se quiere agendar a una fecha menor a la actual)
                //el error se muestra en pantalla
                if (NuevaOrden != null)
                {
                    error = NuevaOrden.error;
                    //si no se regreso ningun error
                    if (Convert.ToInt32(error) == 0)
                    {
                        //se actualiza el grid para ver los cambios (las ordenes recien agendadas se muestran en color gris)
                        Session["fechaAgendada"] = e.NewEnd;
                        Session["command"] = "agendado";
                        Response.Redirect("AgendaC.aspx");
                    }
                    else {
                        //si se quiere insertar a una fecha menor a la actual, mostrara el mensaje de error
                        Response.Write("<script type=\"text/javascript\">alert('No Se Puede Agendar La Orden \\n Se A Insertado En Una Fecha Anterior');  window.location.href = 'Agenda.aspx';</script>");
                    }
                }
            }            
        }
        //metodo que se ejecuta cuando el usuario da click derecho sobre alguna orden agendada en el grid
        //solo aplica para ordenes con status 0 (agendado)
        protected void DayPilotCalendar1_EventMenuClick(object sender, DayPilot.Web.Ui.Events.EventMenuClickEventArgs e)
        //protected void DayPilotCalendar1_EventMenuClick(object sender, DayPilot.Web.Ui.Events.EventMenuClickEventArgs e, string ef_cve, string opcion, string user_cve)
        {
            session.appEnUso();
            //si se selecciona la opcion de confirmar, la orden se actualiza con el status 2 y la accion "confirmado"
            if (e.Command == "confirmar")
            {
                logicaNegocio.InsInfoAgenda("001", e.Start, e.End, "", e.Id, e.Text, " ", "confirmado", "lcAgen", "ZZZ");
                //logicaNegocio.obtListaAgenda(ef_cve, e.Start, e.End, "", e.Id, e.Text, " ", "confirmado", opcion, user_cve);
                //se actualiza el grid para ver las ordenes confirmadas (color verde)
                DayPilotCalendar1.UpdateWithMessage("Se La Orden A Sido Descargada");
                //se recarga la informacion de las listas y del grid de daypilot
                initData();
            }
            //si se selecciona la opcion "a bodega", la orden se actualiza con el status 4 y la accion "bodega"
            if (e.Command == "bodega")
            {
                logicaNegocio.InsInfoAgenda("001", e.Start, e.End, "", e.Id, e.Text, " ", "bodega", "lcAgen", "ZZZ");
                //logicaNegocio.obtListaAgenda(ef_cve, e.Start, e.End, "", e.Id, e.Text, " ", "bodega", opcion, user_cve);
                //se recarga la pagina
                Response.Redirect("~/AgendaC.aspx");
            }
            //si se selecciona la opcion "a pendientes", la orden se actualiza con el status 5 y la accion "pendientes"
            if (e.Command == "pendiente")
            {
                logicaNegocio.InsInfoAgenda("001", e.Start, e.End, "", e.Id, e.Text, " ", "aPendiente", "lcAgen", "ZZZ");
                //logicaNegocio.obtListaAgenda(ef_cve, e.Start, e.End, "", e.Id, e.Text, " ", "pendiente", opcion, user_cve);
                //se recarga la pagina
                Response.Redirect("~/AgendaC.aspx");
            }
        }
        //metodo que se ejecuta para determinar el color de las ordenes dentro del grid
        protected void DayPilotCalendar1_BeforeEventRender(object sender, DayPilot.Web.Ui.Events.Calendar.BeforeEventRenderEventArgs e)
        {
            //color por status(status 0=color por default, status=3 verde, status=4 rojo)
            string status = e.DataItem["status"].ToString();
            switch (status)
            {
                case "0":
                    e.EventDeleteEnabled = false;
                    e.DurationBarColor = "#FF7700";
                    break;
                case "2":
                    e.BackgroundColor = "#037D34"; //verde
                    e.FontColor = "white";
                    e.EventDeleteEnabled = false;
                    e.EventRightClickEnabled = false;//click derecho desactivado
                    e.EventMoveEnabled = false;//desactivamos el poder mover la orden
                    e.EventClickEnabled = false; //desactivamos la actualizacion de la fecha de termino de la orden
                    e.EventResizeEnabled = false;
                    break;
                case "3":
                    e.BackgroundColor = "#990000"; //rojo
                    e.FontColor = "white";
                    e.EventDeleteEnabled = false;
                    e.EventRightClickEnabled = true;//click derecho desactivado
                    e.EventClickEnabled = false; //desactivamos la actualizacion de la fecha de termino de la orden
                    e.EventResizeEnabled = false;
                    e.DurationBarColor = "#333333";
                    break;
            }
        }
        //metodo que se ejecuta cuando daypilot detecta el envio de algun comando http://code.daypilot.org/33944/event-calendar-day-week-month-for-asp-net-mvc
        protected void DayPilotCalendar1_Command(object sender, DayPilot.Web.Ui.Events.CommandEventArgs e)
        {
            switch (e.Command)
            {
                case "navigate":
                    DateTime start = (DateTime)e.Data["start"];
                    DateTime end = (DateTime)e.Data["end"];
                    DateTime day = (DateTime)e.Data["day"];
                    Session["command"] = "navigate";
                    //coloca el marcado en la fecha que el usuario selecciona
                    DayPilotCalendar1.StartDate = start;
                    //muestra un mensaje con la fecha a la que el usuario se a movido
                    DayPilotCalendar1.UpdateWithMessage("Haz Cambiado La fecha A: " + start);
                    //carga la informacion de esa fecha
                    initData();
                    break;
                case "nuevaFecha":
                    DayPilotCalendar1.DataBind();
                    //muestra un mensaje con la fecha a la que el usuario a actualizado
                    DayPilotCalendar1.UpdateWithMessage("Haz Cambiado La Fecha De La Orden");
                    //carga la informacion de esa fecha
                    initData();
                    break;
                case "error":
                    DayPilotCalendar1.DataBind();
                    //muestra un mensaje con la fecha a la que el usuario a actualizado
                    DayPilotCalendar1.UpdateWithMessage("No Se Pudo Cambiar La Fecha A La Orden");
                    //carga la informacion de esa fecha
                    break;
                case "envioBodega":
                    string fechaInicio = System.DateTime.Now.AddMinutes(5).ToString("dd/MM/yyyy HH:mm:ss");
                    string fechafin = System.DateTime.Now.AddDays(1).ToString("dd/MM/yyyy HH:mm:ss");
                    logicaNegocio.InsInfoAgenda((string)e.Data[3], System.DateTime.Parse(fechaInicio), System.DateTime.Parse(fechafin), "", (string)e.Data[0].ToString().TrimEnd(), (string)e.Data[1].ToString().TrimEnd(), (string)e.Data[2].ToString().TrimEnd(), "askytex", "lcAgen", "ZZZ");
                    DayPilotCalendar1.UpdateWithMessage("Se envio a Bodega Skytex");
                    break;
                case "envioEntregar":
                    string fechaInicioE = System.DateTime.Now.AddMinutes(5).ToString("dd/MM/yyyy HH:mm:ss");
                    string fechafinE = System.DateTime.Now.AddDays(1).ToString("dd/MM/yyyy HH:mm:ss");
                    Prueba.Entidades.sp_WebAppInsInfo_Result entregaBodega = logicaNegocio.InsInfoAgenda((string)e.Data[3], System.DateTime.Parse(fechaInicioE), System.DateTime.Parse(fechafinE), "", (string)e.Data[0], (string)e.Data[1], (string)e.Data[2], "pendiente", "lcAgen", "ZZZ");
                    DayPilotCalendar1.UpdateWithMessage(entregaBodega.mensaje);
                    break;
            }
        }

        protected void DayPilotCalendar1_EventResize(object sender, EventResizeEventArgs e)
        {
            DataRow dr = table.Rows.Find(e.Id);
            if (dr != null)
            {
                Prueba.Entidades.sp_WebAppInsInfo_Result Reasignacion = logicaNegocio.InsInfoAgenda("001", e.NewStart, e.NewEnd, "0", e.Id, e.Text, " ", "movido", "lcAgen", "ZZZ");
                if (Reasignacion != null)
                {
                    error = Reasignacion.error;
                    mensaje = Reasignacion.ToString();
                    DayPilotCalendar1.DataSource = DataGeneratorCalendar.GetData();
                    //si se quiere insertar a una fecha menor a la actual, mostrara el mensaje de error
                    DayPilotCalendar1.UpdateWithMessage("Error. No Se Puede Cambiar El Rango De Entrega La Orden");
                    //Response.Redirect("~/Agenda.aspx");
                }
                //si no se regreso ningun error
                if (Convert.ToInt32(error) == 0)
                {
                    DayPilotCalendar1.DataSource = DataGeneratorCalendar.GetData();
                    DayPilotCalendar1.DataBind();
                    //se actualiza el grid para ver los cambios (las ordenes recien agendadas se muestran en color gris)
                    DayPilotCalendar1.UpdateWithMessage("Se A Modificado El Rango De Entrega De La Orden");
                    //Response.Redirect("~/Agenda.aspx");
                }
            }
            initData();
        }

        public void LlenaBodegaParisina()
        {
            //asignacion de datos obtenidos desde la vista e inicializacion del objeto que nos traera el total de ordenes Por Entregar 
            List<Prueba.Entidades.sp_WebAppAgMuestaEv1_Result> BodegaParisina = logicaNegocio.obtEventos();
            Label1.Text = BodegaParisina.Count.ToString();
            //si se obtuvieron resultados
            if (BodegaParisina != null)
            {
                //se le asigna los datos a la lista 
                Repeater1.DataSource = BodegaParisina;
                Repeater1.DataBind();
            }
        }

        //metodo que llena la lista de Bodega Skytex con la informacion de las ordenes con status 4
        public void LlenaBodegaSky()
        //public void LlenaLista2(string ef_cve, string opcion, string status_proceso)
        {
            //se envian los parametros de filtro para la busqueda de las ordenes en bodega
            List<Prueba.Entidades.sp_AgendaListaEventos_Result> BodegaSky = logicaNegocio.ListaBodegaSky("001", "lcAgen", "4");
            //List<Prueba.Entidades.Eventos_Result> Eventos = logicaNegocio.obtListaE(ef_cve, opcion, status_proceso);
            Label2.Text = BodegaSky.Count.ToString();
            //si se obtuvieron resultados
            if (BodegaSky != null)
            {
                //se le asigna los datos a la lista 
                Repeater2.DataSource = BodegaSky;
                Repeater2.DataBind();
            }
        }

        /* listado de ordenes agendadas, descargadas y vencidas (que se muestran en el grid de day pilot) */
        public void LlenaPlaneacion()
        //public void LlenaLista2(string ef_cve, string opcion, string status_proceso)
        {
            //se envian los parametros de filtro para la busqueda de las ordenes en bodega
            List<Prueba.Entidades.sp_WebAppActAgenda_Result> skyPlaneacion = logicaNegocio.ListaXtPlaneacion("", "", "lcAgen");
            //List<Prueba.Entidades.Eventos_Result> Eventos = logicaNegocio.obtListaE(ef_cve, opcion, status_proceso);
            Label3.Text = skyPlaneacion.Count.ToString();
            //si se obtuvieron resultados
            if (skyPlaneacion != null)
            {
                //se le asigna los datos a la lista 
                Repeater3.DataSource = skyPlaneacion;
                Repeater3.DataBind();
            }
        }
        [System.Web.Services.WebMethod]
        public static void cerrarPagina()
        {
            session.appCerrada();
            session.appCerrada2();
        }
        [System.Web.Services.WebMethod]
        public static void alRecargar()
        {
            session.appAbierta2();
        }
        
        public string colorFondo(string status)
        {
            if (status == "A) Disponible Entrega") {
                return "alert-entrega";
            }
            else if (status == "B) Pedimento")
            {
                return "alert-pedimento";
            }
            else if (status == "C) Embarcada")
            {
                return "alert-embarcada";
            }
            else if (status == "Recepcion")
            {
                return "alert-recepcion";
            }
            else
            {
                return "alert-noaplica";
            }
        }
        public string colorFondo2(string status)
        {
            if (status == "Si")
            {
                return "alert-conTransito";
            }
            else if(status == "No")
            {
                return "alert-sinTransito";
            }
            else
            {
                return "alert-sinTransito";
            }
        }
        public string colorFondo3(string status)
        {
            if (status == "0")
            {
                return "alert-Agendada";
            }
            else if (status == "3")
            {
                return "alert-Vencida";
            }
            else if (status == "2")
            {
                return "alert-Entregada";
            }
            else
            {
                return "alert-sinTransito";
            }
        }
    }
}