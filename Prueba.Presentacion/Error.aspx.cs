using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Prueba.Presentacion
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            session.appCerrada();
            session.appCerrada2();
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
    }
}