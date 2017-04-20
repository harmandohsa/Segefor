using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using SEGEFOR.Clases;
using SEGEFOR.Reportes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SEGEFOR.WeForms_Reportes
{
    public partial class Wfrm_RepFormularioPlantacion : System.Web.UI.Page
    {
        protected CrystalReportSource CrystalReportSource1;
        protected CrystalReportViewer CrystalReportViewer1;
        private string DirApp = System.Configuration.ConfigurationManager.AppSettings["DirApp"];
        private string DirRep = System.Configuration.ConfigurationManager.AppSettings["DirRep"];
        private string DirRepLong = System.Configuration.ConfigurationManager.AppSettings["DirRepLong"];
        Rpt_FormularioPlantacion Reporte = new Rpt_FormularioPlantacion();
        Cl_Utilitarios ClUtilitarios;
        Cl_Persona ClPersona;
        Cl_Gestion_Registro ClGestio_Registro;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            ClGestio_Registro = new Cl_Gestion_Registro();
            DataSet Ds_Formulario_Plantacion_Voluntaria = new DataSet();
            Ds_Formulario_Plantacion_Voluntaria = ((DataSet)Session["Ds_Formulario_Plantacion_Voluntaria"]);
            Reporte.PrintOptions.PaperOrientation = PaperOrientation.Portrait;
            Reporte.SetDataSource((DataSet)Ds_Formulario_Plantacion_Voluntaria);

            if (Convert.ToInt32(Session["TipoReporte"]) == 1)
                Reporte.Subreports[0].SetDataSource(ClGestio_Registro.Fincas_Registro(Convert.ToInt32(Session["UsuarioId"]),Convert.ToInt32(Session["TipoReporte"])));
            else
            {
                string[] GestionId = Ds_Formulario_Plantacion_Voluntaria.Tables["Dt_Plantacion_Voluntaria"].Rows[0]["NUG"].ToString().Split('-');
                string IdGestion = HttpUtility.UrlDecode(ClUtilitarios.Decrypt(Request.QueryString["traiteId"].ToString(),true));
                Reporte.Subreports[0].SetDataSource(ClGestio_Registro.Fincas_Registro(Convert.ToInt32(IdGestion), Convert.ToInt32(Session["TipoReporte"])));
            }
                

            string llamada = ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["appel"].ToString()), true);
            string NomReporte;
            if (llamada == "1")
                NomReporte = Ds_Formulario_Plantacion_Voluntaria.Tables["Dt_Plantacion_Voluntaria"].Rows[0]["NUG"] + ".pdf";
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
            //    string Mensaje = "Su solicitud fue enviada exitosamente, con Numero Único de Gestión (NUG): " + Ds_Formulario_Plantacion_Voluntaria.Tables["Dt_Plantacion_Voluntaria"].Rows[0]["NUG"] + ". Por lo que solicitamos presentarse a la oficina Subregional " + Ds_Formulario_Plantacion_Voluntaria.Tables["Dt_Plantacion_Voluntaria"].Rows[0]["SubRegion"] + ", con la documentación que deberá presentar para dar trámite a su solicitud.";
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