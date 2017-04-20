using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SEGEFOR.Mapas
{
    /// <summary>
    /// Summary description for HandMapas
    /// </summary>
    public class HandMapas : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");


            /* Debe obtenerse una cadena de puntos x y separados cada punto por un espacio en blanco */
            SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["cnMapas"]);
            cn.Open();
            SqlCommand Comando = new SqlCommand("sp_obtener_poligonosCOMP", cn);
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.Parameters.Add("@GestionId", SqlDbType.Int, 1);
            Comando.Parameters["@GestionId"].Value = 14;

            SqlDataAdapter da = new SqlDataAdapter(Comando);
            DataSet iDtDatos = new DataSet();

            // Por último, rellenamos el DataSet
            da.Fill(iDtDatos);

            //if ((iDtDatos != null) && (iDtDatos.Tables.Count > 0) && (iDtDatos.Tables[0].Rows.Count > 0))
            //{
            //    //-	Aquí es donde ejecuta el proceso almacena donde se ingresan nuevos polígonos o se actualizan este proceso hace las dos funciones y es en ese proceso donde convierte las coordenadas GTM a coordenadas geográficas las x las convierte en latitud y las Y en longitud

            //    foreach (DataRow iDtRow in iDtDatos.Tables[0].Rows)
            //    {
            //        geoData.AddPolygon(iDtRow["Id"].ToString(), iDtRow["Descripcion"].ToString(), iDtRow["Poligono"].ToString());

            //    }
            //}
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}