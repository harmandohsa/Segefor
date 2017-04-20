using CrystalDecisions.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEGEFOR.Reportes;
using System.Data;
using CrystalDecisions.Shared;
using SEGEFOR.Clases;

namespace SEGEFOR.WeForms_Reportes
{
    public partial class Wfrm_RepInventarioForestal : System.Web.UI.Page
    {
        protected CrystalReportSource CrystalReportSource1;
        protected CrystalReportViewer CrystalReportViewer1;
        private string DirApp = System.Configuration.ConfigurationManager.AppSettings["DirApp"];
        private string DirRep = System.Configuration.ConfigurationManager.AppSettings["DirRep"];
        private string DirRepLong = System.Configuration.ConfigurationManager.AppSettings["DirRepLong"];
        Cl_Utilitarios ClUtilitarios;

        Rpt_InventarioForestal Reporte = new Rpt_InventarioForestal();
        Rpt_InventarioForestal_SistemasAgro Reporte2 = new Rpt_InventarioForestal_SistemasAgro();
        Rpt_InventarioForestal_FuenteSemillera Reporte3 = new Rpt_InventarioForestal_FuenteSemillera();
        
        

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUtilitarios = new Cl_Utilitarios();
            DataSet Ds_Datos = new DataSet();
            Ds_Datos = ((DataSet)Session["Datos_InventarioForestal"]);
            string llamada = ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["appel"].ToString()), true);
            if (llamada == "0")
            {
                Reporte.PrintOptions.PaperOrientation = PaperOrientation.Portrait;
                Reporte.SetDataSource(Ds_Datos);
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
            else if (llamada == "1")
            {
                Reporte2.PrintOptions.PaperOrientation = PaperOrientation.Portrait;
                Reporte2.SetDataSource(Ds_Datos);
                string NomReporte = NomReporte = Guid.NewGuid().ToString() + ".pdf";
                string url = Server.MapPath(".") + @"\" + DirRep + NomReporte;
                DiskFileDestinationOptions options2 = new DiskFileDestinationOptions
                {
                    DiskFileName = url
                };
                ExportOptions exportOptions = Reporte2.ExportOptions;
                exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                exportOptions.ExportDestinationOptions = options2;
                Reporte2.Export();
                url = DirApp + DirRepLong + NomReporte;
                base.Response.Redirect(url);
            }
            else if (llamada == "2")
            {
                Reporte3.PrintOptions.PaperOrientation = PaperOrientation.Portrait;
                Reporte3.SetDataSource(Ds_Datos);
                string NomReporte = NomReporte = Guid.NewGuid().ToString() + ".pdf";
                string url = Server.MapPath(".") + @"\" + DirRep + NomReporte;
                DiskFileDestinationOptions options2 = new DiskFileDestinationOptions
                {
                    DiskFileName = url
                };
                ExportOptions exportOptions = Reporte3.ExportOptions;
                exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                exportOptions.ExportDestinationOptions = options2;
                Reporte3.Export();
                url = DirApp + DirRepLong + NomReporte;
                base.Response.Redirect(url);
            }
            
        }

        protected void Page_UnLoad(object sender, EventArgs e)
        {
            Reporte.Close();
            Reporte.Dispose();
            GC.Collect();
        }
    }
}