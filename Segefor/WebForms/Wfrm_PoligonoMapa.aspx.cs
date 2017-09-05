using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEGEFOR.Clases;
using System.Data;
using System.Web.Services;

namespace SEGEFOR.WebForms
{
    public partial class Wfrm_PoligonoMapa : System.Web.UI.Page
    {
        Cl_Poligono ClPoligono;
        Cl_Utilitarios ClUtilitarios;


        
        protected void Page_Load(object sender, EventArgs e)
        {
            ClPoligono = new Cl_Poligono();
            ClUtilitarios = new Cl_Utilitarios();
            int InmuebleId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["ImmobilienId"].ToString()), true));
            int Tipo =  Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["typbericht"].ToString()), true));
            int Proceso = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["processus"].ToString()), true)); //1Finca 2Bosque 3Intervenir 4Protección 5Repoblacion
            

            Session["InmuebleId"] = InmuebleId.ToString();
            Session["Tipo"] = Tipo.ToString();
            Session["Proceso"] = Proceso.ToString();
            DataSet DsPuntoPol = new DataSet();
            DataSet dsPoligono = new DataSet();

            DataSet dsPoligonoDescuento = new DataSet();
            

            if (Proceso == 1)
            {
                DsPuntoPol = ClPoligono.puntos_poligonos_Inmueble(InmuebleId);
                LblTitulo.Text = "Poligono de Finca";
            }
                
            else if (Proceso == 2)
            {
                int Id = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["identificateur"].ToString()), true));
                Session["Id"] = Id.ToString();
                DsPuntoPol = ClPoligono.puntos_poligonos_AreaBosque(InmuebleId, Id, Tipo);
                LblTitulo.Text = "Poligono de Área de Bosque";
            }
            else if (Proceso == 3)
            {
                int Id = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["identificateur"].ToString()), true));
                Session["Id"] = Id.ToString();
                DsPuntoPol = ClPoligono.puntos_poligonos_AreaIntervenir(InmuebleId, Id, Tipo);
                LblTitulo.Text = "Poligono de Área a Intervenir";
            }
            else if (Proceso == 4)
            {
                int Id = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["identificateur"].ToString()), true));
                Session["Id"] = Id.ToString();
                DsPuntoPol = ClPoligono.puntos_poligonos_AreaProteccion(InmuebleId, Id, Tipo);
                LblTitulo.Text = "Poligono de Área de Protección";
            }
            else if (Proceso == 5)
            {
                int Id = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["identificateur"].ToString()), true));
                Session["Id"] = Id.ToString();
                DsPuntoPol = ClPoligono.puntos_poligonos_Repoblacion(Id, Tipo);
                LblTitulo.Text = "Poligono de Área de Repoblación";
            }
            
            GvistaPuntos.DataSource = DsPuntoPol;
            GvistaPuntos.DataBind();
            DsPuntoPol.Clear();
            
            if (Proceso == 1)
                dsPoligono = ClPoligono.obtener_poligonos_Inmueble(InmuebleId);
            else if (Proceso == 2)
                dsPoligono = ClPoligono.obtener_poligonos_AreaBosque(InmuebleId, Convert.ToInt32(Session["Id"]), Tipo);
            else if (Proceso == 3)
                dsPoligono = ClPoligono.obtener_poligonos_AreaIntervenir(InmuebleId, Convert.ToInt32(Session["Id"]), Tipo);
            else if (Proceso == 4)
                dsPoligono = ClPoligono.obtener_poligonos_AreaProteccion(InmuebleId, Convert.ToInt32(Session["Id"]), Tipo);
            else if (Proceso == 5)
            {
                //dsPoligono = ClPoligono.obtener_poligonos_Repoblacion(Convert.ToInt32(Session["Id"]), Tipo);
                //dsPoligonoDescuento = ClPoligono.obtener_poligonos_Repoblacion_Descuento(Convert.ToInt32(Session["Id"]), Tipo);
                dsPoligono = ClPoligono.obtener_poligonos_Repoblacion_Ambos(Convert.ToInt32(Session["Id"]), Tipo);
            }
            LblArea.Text = dsPoligono.Tables["Datos"].Rows[0]["Descripcion"].ToString();
            if (Proceso == 5)
                LblFinca.Visible = false;
            else    
                LblFinca.Text = dsPoligono.Tables["Datos"].Rows[0]["Finca"].ToString();
            dsPoligono.Clear();
            
        }

        [WebMethod]
        public static MAPS[] BindMapPoints(string name, string name1)
        {

            Cl_Poligono ClPoligono;
            Cl_Utilitarios ClUtilitarios;
            
            ClPoligono = new Cl_Poligono();
            ClUtilitarios = new Cl_Utilitarios();
            string InmuebleId = HttpContext.Current.Session["InmuebleId"].ToString();
            string Tipo = HttpContext.Current.Session["Tipo"].ToString();
            string Proceso = HttpContext.Current.Session["Proceso"].ToString();

            DataSet dsPoligono = new DataSet();
            if (Proceso == "1")
                dsPoligono = ClPoligono.obtener_poligonos_Inmueble(Convert.ToInt32(InmuebleId));
            else if (Proceso == "2")
            {
                string Id = HttpContext.Current.Session["Id"].ToString();
                dsPoligono = ClPoligono.obtener_poligonos_Bosque_Ambos(Convert.ToInt32(InmuebleId), Convert.ToInt32(Id), Convert.ToInt32(Tipo));
            }
            else if (Proceso == "3")
            {
                string Id = HttpContext.Current.Session["Id"].ToString();
                dsPoligono = ClPoligono.obtener_poligonos_Intervenir_Ambos(Convert.ToInt32(InmuebleId), Convert.ToInt32(Id), Convert.ToInt32(Tipo));
            }
            else if (Proceso == "4")
            {
                string Id = HttpContext.Current.Session["Id"].ToString();
                dsPoligono = ClPoligono.obtener_poligonos_Proteccion_Ambos(Convert.ToInt32(InmuebleId), Convert.ToInt32(Id), Convert.ToInt32(Tipo));
            }
            else if (Proceso == "5")
            {
                string Id = HttpContext.Current.Session["Id"].ToString();
                dsPoligono = ClPoligono.obtener_poligonos_Repoblacion_Ambos(Convert.ToInt32(Id), Convert.ToInt32(Tipo));
            }

            List<MAPS> lstFencingCircle = new List<MAPS>();
            
            for (int i = 0; i < dsPoligono.Tables["Datos"].Rows.Count; i++)
            {
                string[] puntos = dsPoligono.Tables["Datos"].Rows[i]["Poligono"].ToString().Split(' ');
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                
                try
                {

                    dt.Columns.Add("Latitude");
                    dt.Columns.Add("Longitude");
                    dt.Columns.Add("Tipo");

                    for (int j = 0; j < puntos.Length; j += 2)
                    {
                        string Lat = puntos[j];
                        string Long = puntos[j + 1];
                        dt.Rows.Add(Lat, Long);
                    }
                    foreach (DataRow dtrow in dt.Rows)
                    {
                        MAPS objMAPS = new MAPS();
                        objMAPS.Latitude = dtrow["Latitude"].ToString();
                        objMAPS.Longitude = dtrow["Longitude"].ToString();
                        objMAPS.Tipo = Tipo;
                        objMAPS.Correlativo = (i+1).ToString();
                        lstFencingCircle.Add(objMAPS);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            dsPoligono.Clear();
            return lstFencingCircle.ToArray();
        }

        public class MAPS
        {
            public string Latitude;
            public string Longitude;
            public string Correlativo;
            public string Tipo;
        }


        [WebMethod]
        public static MAPSDESC[] BindMapPointsDesc(string name, string name1)
        {

            Cl_Poligono ClPoligono;
            Cl_Utilitarios ClUtilitarios;

            ClPoligono = new Cl_Poligono();
            ClUtilitarios = new Cl_Utilitarios();
            string InmuebleId = HttpContext.Current.Session["InmuebleId"].ToString();
            string Tipo = HttpContext.Current.Session["Tipo"].ToString();
            string Proceso = HttpContext.Current.Session["Proceso"].ToString();

            DataSet dsPoligonoDescuento = new DataSet();
            if (Proceso == "1")
                dsPoligonoDescuento = ClPoligono.obtener_poligonos_Inmueble(Convert.ToInt32(InmuebleId));
            else if (Proceso == "2")
            {
                string Id = HttpContext.Current.Session["Id"].ToString();
                dsPoligonoDescuento = ClPoligono.obtener_poligonos_AreaBosque(Convert.ToInt32(InmuebleId), Convert.ToInt32(Id), Convert.ToInt32(Tipo));
            }
            else if (Proceso == "3")
            {
                string Id = HttpContext.Current.Session["Id"].ToString();
                dsPoligonoDescuento = ClPoligono.obtener_poligonos_AreaIntervenir(Convert.ToInt32(InmuebleId), Convert.ToInt32(Id), Convert.ToInt32(Tipo));
            }
            else if (Proceso == "4")
            {
                string Id = HttpContext.Current.Session["Id"].ToString();
                dsPoligonoDescuento = ClPoligono.obtener_poligonos_AreaProteccion(Convert.ToInt32(InmuebleId), Convert.ToInt32(Id), Convert.ToInt32(Tipo));
            }
            else if (Proceso == "5")
            {
                string Id = HttpContext.Current.Session["Id"].ToString();
                dsPoligonoDescuento = ClPoligono.obtener_poligonos_Repoblacion_Descuento(Convert.ToInt32(Id), Convert.ToInt32(Tipo));
            }

            List<MAPSDESC> lstFencingCircleDesc = new List<MAPSDESC>();

            for (int i = 0; i < dsPoligonoDescuento.Tables["Datos"].Rows.Count; i++)
            {
                string[] puntos = dsPoligonoDescuento.Tables["Datos"].Rows[i]["Poligono"].ToString().Split(' ');
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                try
                {

                    dt.Columns.Add("Latitude");
                    dt.Columns.Add("Longitude");


                    for (int j = 0; j < puntos.Length; j += 2)
                    {
                        string Lat = puntos[j];
                        string Long = puntos[j + 1];
                        dt.Rows.Add(Lat, Long);
                    }
                    foreach (DataRow dtrow in dt.Rows)
                    {
                        MAPSDESC objMAPS = new MAPSDESC();
                        objMAPS.Latitude = dtrow["Latitude"].ToString();
                        objMAPS.Longitude = dtrow["Longitude"].ToString();
                        objMAPS.Correlativo = (i + 1).ToString();
                        lstFencingCircleDesc.Add(objMAPS);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            dsPoligonoDescuento.Clear();
            return lstFencingCircleDesc.ToArray();
        }

        public class MAPSDESC
        {
            public string Latitude;
            public string Longitude;
            public string Correlativo;
        }
    }
}