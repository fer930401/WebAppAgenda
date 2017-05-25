using Prueba.LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Prueba.Presentacion
{
    public partial class Inicio : System.Web.UI.Page
    {
        LogicaNegocioCls logicaNegocio;
        protected void Application_Error(Object sender, EventArgs e)
        {
            Session["CurrentError"] = "Global: " +
                Server.GetLastError().Message;
            Response.Redirect("Error.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            logicaNegocio = new LogicaNegocioCls();
            if (session.estaEnUso() == 0)
            {
                session.appAbierta();
                //llenaAgenda();
                //Prueba.Entidades.sp_WebAppLlenaLista_Result lista = logicaNegocio.obtListaPendientes2();
                //Response.Redirect("~/Agenda.aspx");
            }
            else if (session.estaEnUso2() == 0)
            {
                session.appAbierta2();
                //Prueba.Entidades.sp_WebAppLlenaLista_Result lista = logicaNegocio.obtListaPendientes2();
                //Response.Redirect("~/Agenda.aspx");
            }
            else
            {
                Response.Redirect("~/Ocupada.aspx");
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
            session.appAbierta();
            session.appAbierta2();
        }
        private void llenaAgenda()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["skytexConnectionString"].ConnectionString);
            SqlCommand da = new SqlCommand("sp_llenaAgenda", conn);
            da.CommandTimeout = 5000;
            da.CommandType = System.Data.CommandType.StoredProcedure;
            conn.Open();
            da.ExecuteNonQuery();
            conn.Close();
        }
    }
}