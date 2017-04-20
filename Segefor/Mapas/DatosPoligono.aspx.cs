using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEGEFOR.Mapas
{
    public partial class DatosPoligono : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int Id = 5;
            //clsProyecto iProyecto = new clsProyecto();
            //iProyecto.Id = Request["id"];            
            //clsBD iConn = new clsBD();
            //iConn.AgregarParametro("@ProyectoId", SqlDbType.Int, clsBase.intNoNull(iProyecto.Id));

            //DataSet iDtDatos = iConn.Execute("sp_obtener_poligonos2", "tblPoligonos");
                        
           
            /* Debe obtenerse una cadena de puntos x y separados cada punto por un espacio en blanco */
            SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["cnMapas"]);
            cn.Open();
            SqlCommand Comando = new SqlCommand("sp_Lee_poligonos", cn);
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.Parameters.Add("@ProyectoId", SqlDbType.Int, 1);
            Comando.Parameters["@ProyectoId"].Value = Id;

            SqlDataAdapter da = new SqlDataAdapter(Comando);
            DataSet iDtDatos = new DataSet();

            // Por último, rellenamos el DataSet
            da.Fill(iDtDatos);



            if ((iDtDatos != null) && (iDtDatos.Tables.Count > 0) && (iDtDatos.Tables[0].Rows.Count > 0))
            {                                
                string propietario = iDtDatos.Tables[0].Rows[0]["propietario"].ToString();
                string expediente  = iDtDatos.Tables[0].Rows[0]["expendiente"].ToString();
                string faseproyecto = iDtDatos.Tables[0].Rows[0]["fase"].ToString();
                string depto = iDtDatos.Tables[0].Rows[0]["depto"].ToString();
                string muni = iDtDatos.Tables[0].Rows[0]["municipio"].ToString();
                string totalarea = iDtDatos.Tables[1].Rows[0]["total"].ToString();
                Label1.Text = "Expediente: " + expediente;
                Label2.Text = "Fase de " + faseproyecto;
                Label3.Text = "Titular del Proyecto " + propietario;
                Label4.Text = "Departamento: " + depto;
                Label5.Text = "Municipio: " + muni;
                Label6.Text = "Area Total del Proyecto: "+ totalarea+" ha";
                foreach (DataRow iDtRow in iDtDatos.Tables[0].Rows)
                {
                   txtdatopoli.Text += "Poligono No.: " + iDtRow["Id"].ToString() + " Area: " + iDtRow["area"].ToString() +"\n";
                }
                
            }            
        }
        
    }
}