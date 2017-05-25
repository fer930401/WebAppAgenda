using System;
using System.Data;
using DayPilot.Utils;
using Prueba.Entidades;
using System.Drawing;
using System.Text;
using DayPilot.Json;
using DayPilot.Web.Ui;
using DayPilot.Web.Ui.Enums;
using DayPilot.Web.Ui.Events;
using DayPilot.Web.Ui.Events.Bubble;
using DayPilot.Web.Ui.Events.Scheduler;
using DayPilot.Web.Ui.Events.Calendar;
using System.Collections.Generic;
using System.Configuration;
using Prueba.LogicaNegocio;
using System.Data.SqlClient;
using System.Linq;

/// <summary>
/// Summary description for DataGenerator
/// </summary>
public class DataGeneratorCalendar
{
    
    public static DataTable GetData()
    //public static DataTable GetData(short status, string opc, string user_cve, string ef_cve)
    {
        DataRow dr;
        DataTable dt;

        Prueba.LogicaNegocio.LogicaNegocioCls logica = new Prueba.LogicaNegocio.LogicaNegocioCls();
        List<Prueba.Entidades.sp_WebAppLlenaPlaneacion_Result> listaEventos = logica.obtListaPlaneacion(0,"lcAgen", "ZZZ", "001");
        //listaEventos = logica.obtListaPlaneacion(status, opc, user_cve, ef_cve);

        dt = new DataTable();
        dt.Columns.Add("start", typeof(DateTime));
        dt.Columns.Add("end", typeof(DateTime));
        dt.Columns.Add("name", typeof(string));
        dt.Columns.Add("id", typeof(string));
        //dt.Columns.Add("color", typeof(string));
        dt.Columns.Add("status", typeof(string));
        dt.Columns.Add("cve", typeof(string));

        if (listaEventos != null)
        {
            int i = 0;
            foreach (var elemento in listaEventos)
            {
                dr = dt.NewRow();
                dr["id"] = elemento.Batch +" - "+ elemento.Renglon;
                dr["start"] = elemento.Fecha_Inicio;
                dr["end"] = elemento.Fecha_Fin;
                dr["name"] = elemento.Nombre_Batch;
                //dr["color"] = "#"+elemento.Color;
                dr["status"] = elemento.Status;
                dr["cve"] = elemento.Documento;
                dt.Rows.Add(dr);
                i++;
            }
        }
        dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };
        return dt;
    }

    public static DataTable GetDataLarge()
    {
        DataTable dt;
        dt = new DataTable();
        dt.Columns.Add("start", typeof(DateTime));
        dt.Columns.Add("end", typeof(DateTime));
        dt.Columns.Add("name", typeof(string));
        dt.Columns.Add("id", typeof(string));
        dt.Columns.Add("color", typeof(string));
        dt.Columns.Add("status", typeof(string));

        DataRow dr;

        for (int i = 0; i < 300; i++)
        {
            dr = dt.NewRow();
            dr["id"] = i + 1000;
            dr["start"] = Convert.ToDateTime("15:50").AddDays(i);
            dr["end"] = Convert.ToDateTime("15:50").AddDays(i);
            dr["name"] = "Event 1";
            //dr["column"] = "D";
            dr["color"] = "red";
            dt.Rows.Add(dr);
        }

        for (int i = 0; i < 300; i++)
        {
            dr = dt.NewRow();
            dr["id"] = i + 2000;
            dr["start"] = Convert.ToDateTime("15:50").AddDays(i);
            dr["end"] = Convert.ToDateTime("15:50").AddDays(i);
            dr["name"] = "Event 1";
            //dr["column"] = "G";
            dr["color"] = "red";
            dt.Rows.Add(dr);
        }

        for (int i = 0; i < 300; i++)
        {
            dr = dt.NewRow();
            dr["id"] = i + 3000;
            dr["start"] = Convert.ToDateTime("15:50").AddDays(i);
            dr["end"] = Convert.ToDateTime("15:50").AddDays(i);
            dr["name"] = "Event 1";
            //dr["column"] = "I";
            dr["color"] = "green";
            dr["status"] = 0;
            dt.Rows.Add(dr);
        }

        dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };

        return dt;
    }
    
    public static DataTable GetDataBigOnePerDay()
    {
        DataTable dt;
        dt = new DataTable();
        dt.Columns.Add("start", typeof(DateTime));
        dt.Columns.Add("end", typeof(DateTime));
        dt.Columns.Add("name", typeof(string));
        dt.Columns.Add("id", typeof(string));
        //dt.Columns.Add("column", typeof(string));
        //dt.Columns.Add("allday", typeof(bool));
        dt.Columns.Add("color", typeof(string));
        dt.Columns.Add("status", typeof(string));

        DataRow dr;

        DateTime start = new DateTime(DateTime.Today.Year, 1, 1);
        int days = Year.Days(start.Year);
        string resources = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        for (int i = 0; i < days; i++)
        {
            for (int r = 0; r < resources.Length; r++)
            {
                dr = dt.NewRow();
                dr["id"] = Guid.NewGuid().ToString();
                dr["start"] = start.AddDays(i);
                dr["end"] = start.AddDays(i + 1);
                dr["name"] = "Event";
                //dr["column"] = resources[r];
                dr["color"] = "pink";
                dr["status"] = 0;
                dt.Rows.Add(dr);
            }
        }

        dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };

        return dt;

    }

    public static DataTable GetDataOvernight()
    {
        DataTable dt;
        dt = new DataTable();
        dt.Columns.Add("start", typeof(DateTime));
        dt.Columns.Add("end", typeof(DateTime));
        dt.Columns.Add("name", typeof(string));
        dt.Columns.Add("id", typeof(string));
        //dt.Columns.Add("column", typeof(string));
        //dt.Columns.Add("allday", typeof(bool));
        dt.Columns.Add("color", typeof(string));
        dt.Columns.Add("status", typeof(string));

        dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };

        return dt;
    }
}
