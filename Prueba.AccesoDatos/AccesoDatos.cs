using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prueba.Entidades;
using System.Data.SqlClient;
using System.Data;

namespace Prueba.AccesoDatos
{  
    
    public class AccesoDatosCls
    {
        //instancia de la clase skytexEntities
        skytexEntities contexto;
        //constructor
        public AccesoDatosCls()
        {
            //inicializacion de la variable contexto
            contexto = new skytexEntities();
        }

        public Entidades.xcdconapl_cl obtInfPantalla(string ef_cve, string sp_cve, string tipdoc_cve, string spd_cve)
        {
            return (from r in contexto.xcdconapl_cl
                    where
                        r.tipdoc_cve.Equals(tipdoc_cve) &&
                        r.sp_cve.Equals(sp_cve) &&
                        r.spd_cve.Equals(spd_cve) &&
                        r.prm1.Equals(ef_cve)
                    select r).FirstOrDefault();
        }

        //envia los parametros(ef_cve, tipdoc_cve, status_proceso) de busqueda al metodo obtListaE de la clase skytexEntities
        public List<Entidades.Eventos_Result> obtListaE(string ef_cve, string tipdoc_cve, string status_proceso)
        {
            //retorna los resultados de la busqueda en forma de lista
            return (contexto.Eventos(ef_cve, tipdoc_cve, status_proceso)).ToList();
        }
        //envia los parametros(status_proceso, opcion, user_cve, ef_cve) de busqueda al metodo obtListaPlaneacion de la clase skytexEntities
        public List<Entidades.sp_WebAppLlenaPlaneacion_Result> obtListaPlaneacion(short status_proceso, string opcion, string user_cve, string ef_cve)
        {
            //retorna los resultados de la busqueda en forma de lista
            return (contexto.sp_WebAppLlenaPlaneacion(status_proceso, opcion, user_cve, ef_cve)).ToList();
        }
        //envia los parametros(ef_cve, fec_ini, fec_fin, id_maq, id_bacth, nom_batch, doc_gen, accion, opc, user_cve) de insercion al metodo obtListaAgenda de la clase skytexEntities
        public Entidades.sp_WebAppInsInfo_Result InsInfoAgenda(string ef_cve, DateTime fec_ini, DateTime fec_fin, string id_maq, string id_bacth, string nom_batch, string doc_gen, string accion, string opc, string user_cve)
        {
            //retorna los resultados de la busqueda de cada elemento
            return (contexto.sp_WebAppInsInfo(ef_cve, fec_ini, fec_fin, id_maq, id_bacth, nom_batch, doc_gen, accion, opc, user_cve)).FirstOrDefault();
        }

        public List<Entidades.xtPlaneacion> obtListaPendientes()
        {
            return (from p in contexto.xtPlaneacion
                    select p).ToList();
        }
        /*public List<Entidades.sp_WebAppLlenaLista_Result> obtListaPendientes2()
        {
            return (contexto.sp_WebAppLlenaLista()).ToList();
        }*/
        public Entidades.sp_WebAppLlenaLista_Result obtListaPendientes2()
        {
            return (contexto.sp_WebAppLlenaLista()).FirstOrDefault();
        }
        public List<Entidades.sp_AgendaListaEventos_Result> ListaBodegaSky(string ef_cve, string tipdoc_cve, string status_proceso)
        {
            //retorna los resultados de la busqueda en forma de lista
            return (contexto.sp_AgendaListaEventos(ef_cve, tipdoc_cve, status_proceso)).ToList();
        }
        public List<Entidades.sp_WebAppActAgenda_Result> ListaXtPlaneacion(string ef_cve, string doc_generar, string opcion)
        {
            //retorna los resultados de la busqueda en forma de lista
            return (contexto.sp_WebAppActAgenda(ef_cve, doc_generar, opcion)).ToList();
        }
        public List<Entidades.sp_WebAppAgMuestaEv1_Result> obtEventos()
        {
            return (contexto.sp_WebAppAgMuestaEv1()).ToList();
        }
        public List<Entidades.xcTmpAgen> obtListaPendientesTMP()
        {
            return (from tempAgenda in contexto.xcTmpAgens
                    select tempAgenda).ToList();
        }
    }
}
