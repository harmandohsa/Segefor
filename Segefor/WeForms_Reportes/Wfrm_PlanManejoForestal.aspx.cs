using CrystalDecisions.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEGEFOR.Reportes;
using SEGEFOR.Data_Set;
using SEGEFOR.Clases;
using CrystalDecisions.Shared;
using System.Data;

namespace SEGEFOR.WeForms_Reportes
{
    public partial class Wfrm_PlanManejoForestal : System.Web.UI.Page
    {
        protected CrystalReportSource CrystalReportSource1;
        protected CrystalReportViewer CrystalReportViewer1;
        private string DirApp = System.Configuration.ConfigurationManager.AppSettings["DirApp"];
        private string DirRep = System.Configuration.ConfigurationManager.AppSettings["DirRep"];
        private string DirRepLong = System.Configuration.ConfigurationManager.AppSettings["DirRepLong"];
        RptPlanManejo Reporte = new RptPlanManejo();
        Cl_Manejo_Impresion ClManejoImpresion;
        Cl_Utilitarios ClUtilitarios;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClManejoImpresion = new Cl_Manejo_Impresion();
            ClUtilitarios = new Cl_Utilitarios();

            Reporte.PrintOptions.PaperOrientation = PaperOrientation.Portrait;
            int Id = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["identificateur"].ToString()), true));
            int Origen = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["source"].ToString()), true));
            int SubCategoria = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["souscategorie"].ToString()), true));
            DataSet ReportePrincipal = ClManejoImpresion.Impresion_PlanManejo(Id, Origen, SubCategoria);
            Reporte.SetDataSource(ReportePrincipal);

            
            if (Origen == 1)
            {
                
                string AsignacionId = ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["identificateur"].ToString()), true);
                DataSet SubReportes = ClManejoImpresion.VistaPrevia_PlanManejo(Convert.ToInt32(AsignacionId), Origen);
                Reporte.Subreports[0].SetDataSource(SubReportes);
                Reporte.Subreports[1].SetDataSource(SubReportes);
                Reporte.Subreports[2].SetDataSource(SubReportes);
                Reporte.Subreports[3].SetDataSource(SubReportes);
                Reporte.Subreports[4].SetDataSource(SubReportes);
                Reporte.Subreports[5].SetDataSource(SubReportes);
                Reporte.Subreports[6].SetDataSource(SubReportes);
                Reporte.Subreports[7].SetDataSource(SubReportes);
                Reporte.Subreports[8].SetDataSource(SubReportes);
                Reporte.Subreports[9].SetDataSource(SubReportes);
                Reporte.Subreports[10].SetDataSource(SubReportes);
                Reporte.Subreports[11].SetDataSource(SubReportes);
                Reporte.Subreports[12].SetDataSource(SubReportes);
                Reporte.Subreports[13].SetDataSource(SubReportes);
                Reporte.Subreports[14].SetDataSource(SubReportes);
                Reporte.Subreports[15].SetDataSource(SubReportes);
                Reporte.Subreports[16].SetDataSource(SubReportes);
                Reporte.Subreports[17].SetDataSource(SubReportes);
                Reporte.Subreports[18].SetDataSource(SubReportes);
                Reporte.Subreports[19].SetDataSource(SubReportes);
                Reporte.Subreports[20].SetDataSource(SubReportes);
                Reporte.Subreports[21].SetDataSource(SubReportes);
                Reporte.Subreports[22].SetDataSource(SubReportes);
                Reporte.Subreports[23].SetDataSource(SubReportes);
            }
            else
            {
                string GestionId = ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["identificateur"].ToString()), true);
                DataSet SubReportes = ClManejoImpresion.VistaPrevia_PlanManejo(Convert.ToInt32(GestionId), Origen);
                Reporte.Subreports[0].SetDataSource(SubReportes);
                Reporte.Subreports[1].SetDataSource(SubReportes);
                Reporte.Subreports[2].SetDataSource(SubReportes);
                Reporte.Subreports[3].SetDataSource(SubReportes);
                Reporte.Subreports[4].SetDataSource(SubReportes);
                Reporte.Subreports[5].SetDataSource(SubReportes);
                Reporte.Subreports[6].SetDataSource(SubReportes);
                Reporte.Subreports[7].SetDataSource(SubReportes);
                Reporte.Subreports[8].SetDataSource(SubReportes);
                Reporte.Subreports[9].SetDataSource(SubReportes);
                Reporte.Subreports[10].SetDataSource(SubReportes);
                Reporte.Subreports[11].SetDataSource(SubReportes);
                Reporte.Subreports[12].SetDataSource(SubReportes);
                Reporte.Subreports[13].SetDataSource(SubReportes);
                Reporte.Subreports[14].SetDataSource(SubReportes);
                Reporte.Subreports[15].SetDataSource(SubReportes);
                Reporte.Subreports[16].SetDataSource(SubReportes);
                Reporte.Subreports[17].SetDataSource(SubReportes);
                Reporte.Subreports[18].SetDataSource(SubReportes);
                Reporte.Subreports[19].SetDataSource(SubReportes);
                Reporte.Subreports[20].SetDataSource(SubReportes);
                Reporte.Subreports[21].SetDataSource(SubReportes);
                Reporte.Subreports[22].SetDataSource(SubReportes);
                Reporte.Subreports[23].SetDataSource(SubReportes);
            }

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