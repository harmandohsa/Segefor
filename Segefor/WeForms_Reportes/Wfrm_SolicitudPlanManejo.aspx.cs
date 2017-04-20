using CrystalDecisions.Web;
using SEGEFOR.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEGEFOR.WeForms_Reportes
{
    public partial class Wfrm_SolicitudPlanManejo : System.Web.UI.Page
    {
        protected CrystalReportSource CrystalReportSource1;
        protected CrystalReportViewer CrystalReportViewer1;
        private string DirApp = System.Configuration.ConfigurationManager.AppSettings["DirApp"];
        private string DirRep = System.Configuration.ConfigurationManager.AppSettings["DirRep"];
        private string DirRepLong = System.Configuration.ConfigurationManager.AppSettings["DirRepLong"];
        Cl_Manejo_Impresion ClManejoImpresion;
        Cl_Utilitarios ClUtilitarios;


        protected void Page_Load(object sender, EventArgs e)
        {
            ClManejoImpresion = new Cl_Manejo_Impresion();
            ClUtilitarios = new Cl_Utilitarios();

            int AsignacionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["identificateur"].ToString()), true));
            int RegionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["idnoiger"].ToString()), true));
            int SubRegionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["sousnoigerid"].ToString()), true));

            DataSet Ds_Solicitud = ClManejoImpresion.Impresion_Solicitud(AsignacionId, RegionId, SubRegionId);

        }
    }
}