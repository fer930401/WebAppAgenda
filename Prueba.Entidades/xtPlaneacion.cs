//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Prueba.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class xtPlaneacion
    {
        public System.DateTime fec_ini { get; set; }
        public System.DateTime fec_fin { get; set; }
        public Nullable<System.DateTime> fec_ins { get; set; }
        public string id_maquina { get; set; }
        public string id_batch { get; set; }
        public string nom_bact { get; set; }
        public Nullable<short> status_proceso { get; set; }
        public Nullable<int> fol_dispo { get; set; }
        public string doc_dispo { get; set; }
        public string ef_cve { get; set; }
        public string accion { get; set; }
        public string opcion { get; set; }
        public string user_cve { get; set; }
        public Nullable<short> num_reng { get; set; }
    }
}
