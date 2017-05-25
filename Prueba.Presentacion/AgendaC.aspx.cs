using DayPilot.Web.Ui.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Prueba.Presentacion
{
    public partial class AgendaC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void DayPilotCalendar1_EventMove(object sender, DayPilot.Web.Ui.Events.EventMoveEventArgs e)
        {
            
        }
        //metodo que se ejecuta cuando el usuario da click derecho sobre alguna orden agendada en el grid
        //solo aplica para ordenes con status 0 (agendado)
        protected void DayPilotCalendar1_EventMenuClick(object sender, DayPilot.Web.Ui.Events.EventMenuClickEventArgs e)
        //protected void DayPilotCalendar1_EventMenuClick(object sender, DayPilot.Web.Ui.Events.EventMenuClickEventArgs e, string ef_cve, string opcion, string user_cve)
        {
            
        }
        //metodo que se ejecuta para determinar el color de las ordenes dentro del grid
        protected void DayPilotCalendar1_BeforeEventRender(object sender, DayPilot.Web.Ui.Events.Calendar.BeforeEventRenderEventArgs e)
        {
            
        }
        //metodo que se ejecuta cuando daypilot detecta el envio de algun comando http://code.daypilot.org/33944/event-calendar-day-week-month-for-asp-net-mvc
        protected void DayPilotCalendar1_Command(object sender, DayPilot.Web.Ui.Events.CommandEventArgs e)
        {
            
        }

        protected void DayPilotCalendar1_EventResize(object sender, EventResizeEventArgs e)
        {
           
        }
        [System.Web.Services.WebMethod]
        public static void alRecargar()
        {
            session.appAbierta2();
        }
    }
}