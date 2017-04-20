using CrystalDecisions.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEGEFOR.Reportes;
using CrystalDecisions.Shared;
using System.Data;
using SEGEFOR.Clases;



namespace SEGEFOR.WeForms_Reportes
{
    public partial class Wfrm_RepFormularioProfesional : System.Web.UI.Page
    {
        protected CrystalReportSource CrystalReportSource1;
        protected CrystalReportViewer CrystalReportViewer1;
        private string DirApp = System.Configuration.ConfigurationManager.AppSettings["DirApp"];
        private string DirRep = System.Configuration.ConfigurationManager.AppSettings["DirRep"];
        private string DirRepLong = System.Configuration.ConfigurationManager.AppSettings["DirRepLong"];
        Rpt_FormularioProfesional Reporte = new Rpt_FormularioProfesional();
        Cl_Utilitarios ClUtilitarios;
        Cl_Persona ClPersona;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            DataSet Ds_FormularioInscripcion = new DataSet();
            Ds_FormularioInscripcion = ((DataSet)Session["DataFormulario"]);
            Reporte.PrintOptions.PaperOrientation = PaperOrientation.Portrait;
            Reporte.SetDataSource((DataSet)Ds_FormularioInscripcion);
            string llamada =  ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["appel"].ToString()), true);
            string NomReporte;
            if (llamada == "1")
                NomReporte = ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["traite"].ToString()), true) + ".pdf";
            else
                NomReporte = Guid.NewGuid().ToString() + ".pdf";
            string url = Server.MapPath(".") + @"\" + DirRep + NomReporte;
            string attchat = Server.MapPath(".") + @"\" + DirRep + NomReporte;
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
            //    string Mensaje = "Su solicitud fue enviada exitosamente, con Numero Único de Gestión (NUG): " + ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["traite"].ToString()), true) + ". Por lo que solicitamos presentarse a la oficina Subregional " + Ds_FormularioInscripcion.Tables["Dt_Formulario"].Rows[0]["SubRegion"] + ", con la documentación que deberá presentar para dar trámite a su solicitud.";
            //    ClUtilitarios.EnvioCorreo(Session["Correo_Usuario"].ToString(), ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"])).ToString(), "Solicitud SEGEFOR", Mensaje, 1, attchat,NomReporte);
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