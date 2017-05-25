using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prueba.AccesoDatos;
using Prueba.Entidades;
using System.Data.SqlClient;
using System.Data;

namespace Prueba.LogicaNegocio
{
    public class LogicaNegocioCls
    {
        //instancia de la clase AccesoDatos.cs
        AccesoDatos.AccesoDatosCls datos;
        //constructor
        public LogicaNegocioCls()
        {
            //inicializacion de la variable datos
            datos = new AccesoDatosCls();
        }

        //public Entidades.xcdconapl_cl obtInfPantalla(string ef_cve, string sp_cve, string tipdoc_cve, string spd_cve)
        //{
        //    return datos.obtInfPantalla(ef_cve, sp_cve, tipdoc_cve, spd_cve);
        //}
        
        //envia los parametros(ef_cve, tipdoc_cve, status_proceso) de busqueda al metodo obtListaE de la clase AccesoDatos
        public List<Entidades.Eventos_Result> obtListaE(string ef_cve , string tipdoc_cve, string status_proceso)
        {
            return datos.obtListaE(ef_cve, tipdoc_cve, status_proceso);
        }

        public List<Entidades.sp_WebAppActAgenda_Result> ListaXtPlaneacion(string ef_cve, string doc_gen, string opc)
        {
            return datos.ListaXtPlaneacion(ef_cve, doc_gen, opc);
        }

        //envia los parametros(status_proceso, opcion, user_cve, ef_cve) de busqueda al metodo obtListaPlaneacion de la clase AccesoDatos
        public List<Entidades.sp_WebAppLlenaPlaneacion_Result> obtListaPlaneacion(short status_proceso, string opcion, string user_cve, string ef_cve)
        {
            return datos.obtListaPlaneacion(status_proceso, opcion, user_cve, ef_cve);
        }

        //envia los parametros(ef_cve, fec_ini, fec_fin, id_maq, id_bacth, nom_batch, doc_gen, accion, opc, user_cve) de insercion al metodo obtListaAgenda de la clase AccesoDatos
        public Entidades.sp_WebAppInsInfo_Result InsInfoAgenda(string ef_cve, DateTime fec_ini, DateTime fec_fin, string id_maq, string id_bacth, string nom_batch, string doc_gen, string accion, string opc, string user_cve)
        {
            return datos.InsInfoAgenda(ef_cve, fec_ini, fec_fin, id_maq, id_bacth, nom_batch, doc_gen, accion, opc, user_cve);

        }

        /*public List<Entidades.qaroc_xrec> obtListaPendientes()
        {
            return datos.obtListaPendientes();
        }
        public List<Entidades.sp_WebAppLlenaLista_Result> obtListaPendientes2()
        {
            return datos.obtListaPendientes2();
        }*/
        public Entidades.sp_WebAppLlenaLista_Result obtListaPendientes2()
        {
            return datos.obtListaPendientes2();
        }
        public List<Entidades.sp_AgendaListaEventos_Result> ListaBodegaSky(string ef_cve, string tipdoc_cve, string status_proceso)
        {
            return datos.ListaBodegaSky(ef_cve, tipdoc_cve, status_proceso);
        }
        public List<Entidades.sp_WebAppAgMuestaEv1_Result> obtEventos()
        {
            return datos.obtEventos();
        }
        public List<Entidades.xcTmpAgen> obtListaPendientesTMP()
        {
            return datos.obtListaPendientesTMP();
        }
    }
}
