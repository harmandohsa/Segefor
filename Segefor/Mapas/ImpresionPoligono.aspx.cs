using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibreriaPinpep;

namespace SEGEFOR.Mapas
{
    public partial class ImpresionPoligono : System.Web.UI.Page
    {
        //private clsUsuario _Usuario = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            //fechade.Text = "Fecha de elaboración: " + DateTime.Today.ToShortDateString();
            try
            {

                String proyectoNo = Request.QueryString.Get("dato1");
                String poligonoNo = Request.QueryString.Get("dato2");

                
                
                idproyecto.Value = LibreriaPinpep.clsBase.StrNoNull(Request["dato1"]);
                Nopoligono.Value = LibreriaPinpep.clsBase.StrNoNull(Request["dato2"]);

               // idproyecto.Value = proyectoNo;
                //Nopoligono.Value = "1";

                Lblpoligno.Text = Request.QueryString.Get("dato2");
                


                

                // ReSharper disable once UnusedVariable.Compiler
                String funcion = "ver_mapa();";

                

               // * Debe obtenerse una cadena de puntos x y separados cada punto por un espacio en blanco */
                SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["cnMapas"]);
                cn.Open();
                SqlCommand Comando = new SqlCommand("sp_Lee_poligonoIndividual", cn);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@GestionId", SqlDbType.Int, 1);
                Comando.Parameters["@GestionId"].Value = proyectoNo.ToString();

                Comando.Parameters.Add("@poligonoId", SqlDbType.Int, 1);
                Comando.Parameters["@poligonoId"].Value = Convert.ToInt32(poligonoNo.ToString());

                SqlDataAdapter da = new SqlDataAdapter(Comando);
                DataSet iDtDatos = new DataSet();

                // Por último, rellenamos el DataSet
                da.Fill(iDtDatos);


                if ((iDtDatos != null) && (iDtDatos.Tables.Count > 0) && (iDtDatos.Tables[0].Rows.Count > 0))
                {
                    //Lbltitular.Text = iDtDatos.Tables[0].Rows[0]["propietario"].ToString();
                    //lbldepto.Text = iDtDatos.Tables[0].Rows[0]["municipio"].ToString() + ", " + iDtDatos.Tables[0].Rows[0]["depto"].ToString();
                    //lbltipoproyecto.Text = iDtDatos.Tables[0].Rows[0]["TipoCompromiso"].ToString();
                    //Lblexpediente.Text = iDtDatos.Tables[0].Rows[0]["expendiente"].ToString();
                    //Lblfaseproyecto.Text = iDtDatos.Tables[0].Rows[0]["fase"].ToString();
                    //lbltotalarea.Text = iDtDatos.Tables[0].Rows[0]["area"].ToString();
                    //lbltecnico.Text = iDtDatos.Tables[0].Rows[0]["tecnico"].ToString();
                    //lblareatotal.Text = iDtDatos.Tables[1].Rows[0]["total"].ToString() + " ha";
                    //Lblpoligno.Text = iDtDatos.Tables[0].Rows[0]["correlativo"].ToString(); // + " (" + poligonoNo.ToString() + ")";

                    ScriptManager.RegisterStartupScript(this, typeof(Page), "menu", funcion, true);



                }






                   
           // *********puntos ******
                    /* Debe obtenerse una cadena de puntos x y separados cada punto por un espacio en blanco */
                    SqlConnection cn2 = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["cnMapas"]);
                    cn2.Open();
                    SqlCommand Comando2 = new SqlCommand("sp_obtener_puntos_poligonos_individual", cn);
                    Comando2.CommandType = CommandType.StoredProcedure;
                    Comando2.Parameters.Add("@GestionId", SqlDbType.Int, 1);
                    Comando2.Parameters["@GestionId"].Value = proyectoNo.ToString();

                    Comando2.Parameters.Add("@Correlativo", SqlDbType.Int, 1);
                    Comando2.Parameters["@Correlativo"].Value = Convert.ToInt32(poligonoNo.ToString());

                    SqlDataAdapter da2 = new SqlDataAdapter(Comando2);
                    DataSet iDtDatos2 = new DataSet();

                    // Por último, rellenamos el DataSet
                    da2.Fill(iDtDatos2);
                        
                    if ((iDtDatos2 != null) && (iDtDatos2.Tables.Count > 0) && (iDtDatos2.Tables[0].Rows.Count > 0))
                    {
                        GvistaPuntos.DataSource = iDtDatos2;
                        GvistaPuntos.DataBind();
                              

                         GvistaPuntos.HeaderRow.Cells[0].Text = "Punto No.";
                         GvistaPuntos.HeaderRow.Cells[1].Text = "Punto X";
                         GvistaPuntos.HeaderRow.Cells[2].Text = "Punto Y";
                    }
                            

                    // ******** FIN Puntos **
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "menu", funcion, true);
                // ***** INICIO SHAPE *******
                    

                    ///* Debe obtenerse una cadena de puntos x y separados cada punto por un espacio en blanco */
                    SqlConnection cn3 = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["cnMapas"]);
                    cn3.Open();
                    SqlCommand Comando3 = new SqlCommand("sp_obtener_puntos_poligonos", cn);
                    Comando3.CommandType = CommandType.StoredProcedure;
                    Comando3.Parameters.Add("@GestionId", SqlDbType.Int, 1);
                    Comando3.Parameters["@GestionId"].Value = proyectoNo.ToString();

                    SqlDataAdapter da3 = new SqlDataAdapter(Comando3);
                    DataSet iDtDato3 = new DataSet();

                    // Por último, rellenamos el DataSet
                    da3.Fill(iDtDato3);


                    // ReSharper disable once UnusedVariable.Compiler
                    String insertarvertice = "insertar_vertice();";

                    List<string> VerticePOl = new List<string>();
                    List<string> PuntoenX = new List<string>();
                    List<string> puntoenY = new List<string>();


                    int cantidadfilas;

                    if ((iDtDato3 != null) && (iDtDato3.Tables.Count > 0) && (iDtDato3.Tables[0].Rows.Count > 0))
                    {

                        cantidadfilas = clsBase.intNoNull(iDtDato3.Tables[0].Rows.Count) - 1;
                        cantidadf.Value = cantidadfilas.ToString();

                        for (int inicio = 0; inicio <= cantidadfilas; inicio++)
                        {
                            VerticePOl.Add(iDtDato3.Tables[0].Rows[inicio]["Punto"].ToString());
                            PuntoenX.Add(iDtDato3.Tables[0].Rows[inicio]["Punto_X"].ToString());
                            puntoenY.Add(iDtDato3.Tables[0].Rows[inicio]["Punto_Y"].ToString());
                        }

                        StringBuilder sn = new StringBuilder();
                        StringBuilder sx = new StringBuilder();
                        StringBuilder sy = new StringBuilder();

                        sn.Append("<script>");
                        sn.Append("var tnumero = new Array;");
                        foreach (string str in VerticePOl)
                        {
                            sn.Append("tnumero.push('" + str + "');");
                        }
                        sn.Append("</script>");

                        ClientScript.RegisterStartupScript(this.GetType(), "TestArrayScript", sn.ToString());

                        sx.Append("<script>");
                        sx.Append("var tposix = new Array;");
                        foreach (string str in PuntoenX)
                        {
                            sx.Append("tposix.push('" + str + "');");
                        }
                        sx.Append("</script>");

                        ClientScript.RegisterStartupScript(this.GetType(), "TestArrayScript1", sx.ToString());

                        sy.Append("<script>");
                        sy.Append("var tposiy = new Array;");
                        foreach (string str in puntoenY)
                        {
                            sy.Append("tposiy.push('" + str + "');");
                        }
                        sy.Append("</script>");

                        ClientScript.RegisterStartupScript(this.GetType(), "TestArrayScript2", sy.ToString());

                    }

                    ScriptManager.RegisterStartupScript(this, typeof(Page), "vertice", insertarvertice, true);                              


                // ***** FIN SHAPE **********



          } //try
          catch (Exception ex)
            {

            }

        
        }
    }
}