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
    public partial class Wfrm_RepEnmiendasTec : System.Web.UI.Page
    {

        protected CrystalReportSource CrystalReportSource1;
        protected CrystalReportViewer CrystalReportViewer1;
        private string DirApp = System.Configuration.ConfigurationManager.AppSettings["DirApp"];
        private string DirRep = System.Configuration.ConfigurationManager.AppSettings["DirRep"];
        private string DirRepLong = System.Configuration.ConfigurationManager.AppSettings["DirRepLong"];
        Rpt_Oficio_Enmienda_Tecnico Reporte = new Rpt_Oficio_Enmienda_Tecnico();
        Cl_Gestion_Registro ClGestion;
        Cl_Utilitarios ClUtilitarios;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClGestion = new Cl_Gestion_Registro();
            ClUtilitarios = new Cl_Utilitarios();
            DataSet ds = new DataSet();
            DataSet Ds_Enmienda_Tec = new DataSet();
            Ds_Enmienda_Tec = ((DataSet)Session["DatosEnmiendasTecnicas"]);

            Reporte.PrintOptions.PaperOrientation = PaperOrientation.Portrait;
            Reporte.SetDataSource(Ds_Enmienda_Tec);
            string NomReporte = NomReporte = Guid.NewGuid().ToString() + ".pdf";
            string url = Server.MapPath(".") + @"\" + DirRep + NomReporte;
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