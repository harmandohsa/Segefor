using GeoRss;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Syndication;
using System.ServiceModel.Web;
using System.Text;
using LibreriaPinpep;

namespace SEGEFOR.Mapas
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class WSMapas
    {
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        //[OperationContract]
        //public void DoWork()
        //{
        //    // Add your operation implementation here
        //    return;
        //}

        [OperationContract]
        [ServiceKnownType(typeof(Rss20FeedFormatter))]
        [WebGet(UriTemplate = "DoWork")]

        public void DoWork()
        {
            // Add your operation implementation here
            return;
        }

        [OperationContract]
        [ServiceKnownType(typeof(Rss20FeedFormatter))]
        [WebGet(UriTemplate = "obtenerPoligonos/{id},{poly}")]

        public SyndicationFeedFormatter obtenerPoligonos(string Id, string poly)
        {
            string StoreProc = "";
            var geoData = new GeoRssData("obtenerPoligonos", "Some Description");

            /* Debe obtenerse una cadena de puntos x y separados cada punto por un espacio en blanco */
            SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["cnMapas"]);
            cn.Open();
            if (poly == "0")
            {
                StoreProc = "sp_obtener_poligonosCOMP";
            }
            else
            {
                StoreProc = "sp_obtener_poligonosCOMP3";
            }

            SqlCommand Comando = new SqlCommand(StoreProc, cn);
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.Parameters.Add("@GestionId", SqlDbType.Int, 1);
            Comando.Parameters["@GestionId"].Value = Id;

            if (poly != "0")
            {
                Comando.Parameters.Add("@Poligono", SqlDbType.Int, 1);
                Comando.Parameters["@Poligono"].Value = poly;
            }

            SqlDataAdapter da = new SqlDataAdapter(Comando);

            DataSet iDtDatos = new DataSet();

            // Por último, rellenamos el DataSet
            da.Fill(iDtDatos);

            if ((iDtDatos != null) && (iDtDatos.Tables.Count > 0) && (iDtDatos.Tables[0].Rows.Count > 0))
            {
                //-	Aquí es donde ejecuta el proceso almacena donde se ingresan nuevos polígonos o se actualizan este proceso hace las dos funciones y es en ese proceso donde convierte las coordenadas GTM a coordenadas geográficas las x las convierte en latitud y las Y en longitud

                foreach (DataRow iDtRow in iDtDatos.Tables[0].Rows)
                {
                    geoData.AddPolygon(iDtRow["Id"].ToString(), iDtRow["Descripcion"].ToString(), iDtRow["Poligono"].ToString());

                }
            }
            return new Rss20FeedFormatter(GeoRssFeedBuilder.GetFeed(geoData));

        }
        // Add more operations here and mark them with [OperationContract]
    }
}
