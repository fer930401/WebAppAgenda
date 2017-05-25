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

namespace Prueba.Presentacion
{
    public partial class nuevaFecha : System.Web.UI.Page
    {
        //instancia de logicaNegocio.cs
        LogicaNegocioCls logicaNegocio;
        DataTable table;
        string mensaje = "";
        short? error = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            //initData();
            logicaNegocio = new LogicaNegocioCls();
            if (!IsPostBack)
            {
                //initData();
                //carga los valores enviados desde el grid de daypilot a los controles del formulario
                DataRow ev = loadEvent(Request.QueryString["id"]);
                //datepicker.Text = ev["end"].ToString();
                timepickerIni.Value = ev["start"].ToString();
                idOrden.Value = ev["id"].ToString();
                statusOrden.Value = ev["status"].ToString();
                nombreOrden.Value = ev["name"].ToString();
            }
        }
        protected void ButtonOK_Click(object sender, EventArgs e)
        {
            //toma los valores ingresados a los controles del formulario y los asigna a una variable temporal
            DateTime end = Convert.ToDateTime(datepicker.Text);
            DateTime start = Convert.ToDateTime(timepickerIni.Value);
            string idOrd = idOrden.Value.ToString();
            string statusOrd = statusOrden.Value.ToString();
            string nomOrd = nombreOrden.Value.ToString();
            
            //se envian los datos para la insercion en la base de datos, la validacion de estos datos se hace en el sp_webappinsinfo
            Prueba.Entidades.sp_WebAppInsInfo_Result Errores = logicaNegocio.InsInfoAgenda("001", start, end, statusOrd, idOrd, nomOrd, " ", "movido", "lcAgen", "ZZZ");
            //se valida si la insercion de la informacion retorno algun error
            if (Errores != null)
            {
                error = Errores.error;
                mensaje = Errores.ToString();
                //si se quiere insertar a una fecha menor a la actual, mostrara el mensaje de error
                //Modal.Close(this, "error");
            }
            //si no se regreso ningun error
            if (Convert.ToInt32(error) == 0)
            {
                //se actualiza el grid para ver los cambios (las ordenes recien agendadas se muestran en color gris)
                //DayPilotCalendar1.UpdateWithMessage("Se A Modificado El Rango De Entrega De La Orden");
                Modal.Close(this, "OK");
                //initData();
            }
        }

        private string dbUpdateEvent(string id, DateTime start, DateTime end, string status, string resource)
        {
            initData();

            #region Simulation of database update

            /*Prueba.Entidades.sp_WebAppInsInfo_Result Errores = logicaNegocio.obtListaAgenda("001", e.NewStart, e.NewEnd, "0", e.Id, e.Text, " ", "movido", "lcAgen", "ZZZ");
            if (Errores != null)
            {
                error = Errores.error;
                mensaje = Errores.ToString();
                //si se quiere insertar a una fecha menor a la actual, mostrara el mensaje de error
                DayPilotCalendar1.UpdateWithMessage("Error. No Se Puede Cambiar El Rango De Entrega La Orden");
            }
            //si no se regreso ningun error
            if (Convert.ToInt32(error) == 0)
            {
                //se actualiza el grid para ver los cambios (las ordenes recien agendadas se muestran en color gris)
                DayPilotCalendar1.UpdateWithMessage("Se A Modificado El Rango De Entrega De La Orden");
            }*/

            DataRow dr = loadEvent(id);
            dr["end"] = end;
            dr["id"] = id;

            //table.Rows.Add(dr);
            table.AcceptChanges();
            #endregion

            return id;
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Modal.Close(this, 2);
        }
        private void initData()
        //private void initData(short status, string opc, string user_cve, string ef_cve)20
        {
            string id = "AllFeatures";

            if (Session[id] == null)
            {
                Session[id] = DataGeneratorCalendar.GetData();
            }
            table = (DataTable)Session[id];


            //asignacion de la informacion a la variable table
            table = (DataTable)DataGeneratorCalendar.GetData();
            //table = (DataTable)DataGeneratorCalendar.GetData(status, opc, user_cve, ef_cve);
        }
        private DataRow loadEvent(string id)
        {
            initData();
            return table.Rows.Find(id);
        }
    }
}