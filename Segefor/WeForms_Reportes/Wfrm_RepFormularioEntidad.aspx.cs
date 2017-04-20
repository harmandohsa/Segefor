using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using SEGEFOR.Clases;
using SEGEFOR.Reportes;
using System.Data;

namespace SEGEFOR.WeForms_Reportes
{
    public partial class Wfrm_RepFormularioEntidad : System.Web.UI.Page
    {
        protected CrystalReportSource CrystalReportSource1;
        protected CrystalReportViewer CrystalReportViewer1;
        private string DirApp = System.Configuration.ConfigurationManager.AppSettings["DirApp"];
        private string DirRep = System.Configuration.ConfigurationManager.AppSettings["DirRep"];
        private string DirRepLong = System.Configuration.ConfigurationManager.AppSettings["DirRepLong"];
        Rpt_FormularioEntidad Reporte = new Rpt_FormularioEntidad();
        Cl_Utilitarios ClUtilitarios;
        Cl_Persona ClPersona;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            DataSet Ds_Formulario_Entidad = new DataSet();
            Ds_Formulario_Entidad = ((DataSet)Session["Dt_Entidad"]);
            Reporte.PrintOptions.PaperOrientation = PaperOrientation.Portrait;
            Reporte.SetDataSource((DataSet)Ds_Formulario_Entidad);

            string llamada = ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["appel"].ToString()), true);
            string NomReporte;
            if (llamada == "1")
                NomReporte = Ds_Formulario_Entidad.Tables["Dt_Entidad"].Rows[0]["NUG"] + ".pdf";
            else
                NomReporte = Guid.NewGuid().ToString() + ".pdf";
            //string url = System.AppDomain.CurrentDomain.BaseDirectory + DirRep + NomReporte;
            string url = Server.MapPath(".") + @"\" + DirRep + NomReporte;
            string attchat = Server.MapPath(".") + @"\" + DirRep + NomReporte;
            //string attchat = System.AppDomain.CurrentDomain.BaseDirectory + DirRep + NomReporte;
            DiskFileDestinationOptions options2 = new DiskFileDestinationOptions
            {
                DiskFileName = url
            };
            ExportOptions exportOptions = Reporte.ExportOptions;
            exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            exportOptions.ExportDestinationOptions = options2;
            Reporte.Export();
            url = DirApp + DirRepLong + NomReporte;
            //if (llamada == "1")
            //{
            //    string Mensaje = "Su solicitud fue enviada exitosamente, con Numero Único de Gestión (NUG): " + Ds_Formulario_Entidad.Tables["Dt_Entidad"].Rows[0]["NUG"] + ". Por lo que solicitamos presentarse a la oficina Subregional " + Ds_Formulario_Entidad.Tables["Dt_Entidad"].Rows[0]["SubRegion"] + ", con la documentación que deberá presentar para dar trámite a su solicitud.";
            //    ClUtilitarios.EnvioCorreo(Session["Correo_Usuario"].ToString(), ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"])).ToString(), "Solicitud SEGEFOR", Mensaje, 1, attchat, NomReporte);
            //}
            base.Response.Redirect(url);
        }


        protected void Page_UnLoad(object sender, EventArgs e)
        {
            Reporte.Close();
            Reporte.Dispose();
            GC.Collect();
        }
    }
}