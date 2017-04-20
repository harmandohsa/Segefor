using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml;

namespace SEGEFOR.Clases
{
    public class Cl_Poligono
    {

        public bool Actualizar_Poligono(XmlDocument pPoligono, ref int pId, ref String pPoligonoGML, int Usrcre, ref string ErrorMapa)
        {

            int Id = pId;
            SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConexionSql"]);
            cn.Open();
            SqlCommand Comando = new SqlCommand("sp_actualizar_poligonos", cn);
            Comando.CommandType = CommandType.StoredProcedure;

            Comando.Parameters.Add("@GestionId", SqlDbType.Int, 1);
            Comando.Parameters["@GestionId"].Value = Id;

            Comando.Parameters.Add("@ProyectoIntersectaId", SqlDbType.Int, 0);
            Comando.Parameters["@ProyectoIntersectaId"].Direction = ParameterDirection.Output;

            Comando.Parameters.Add("@GMLPoligono", SqlDbType.VarChar, -1);
            Comando.Parameters["@GMLPoligono"].Direction = ParameterDirection.Output;

            Comando.Parameters.Add("@Puntos", SqlDbType.Xml, -1);
            Comando.Parameters["@Puntos"].Value = pPoligono.OuterXml;

            Comando.Parameters.Add("@MensajeError", SqlDbType.VarChar, -1);
            Comando.Parameters["@MensajeError"].Direction = ParameterDirection.Output;

            Comando.Parameters.Add("@Error", SqlDbType.Int, 0);
            Comando.Parameters["@Error"].Direction = ParameterDirection.Output;

            Comando.Parameters.Add("@Usrcre", SqlDbType.Int, 0);
            Comando.Parameters["@usrcre"].Value = Usrcre;
            // Comando.Parameters["@Error"].Value = 1;
            Comando.ExecuteNonQuery();

            //-	Aquí es donde ejecuta el proceso almacena donde se ingresan nuevos polígonos o se actualizan este proceso hace las dos funciones y es en ese proceso donde convierte las coordenadas GTM a coordenadas geográficas las x las convierte en latitud y las Y en longitud

            if (!Convert.IsDBNull(Comando.Parameters["@Error"].Value))
            {
                if (Convert.ToInt32(Comando.Parameters["@Error"].Value) != 0)
                {
                    Int32 Error = Convert.ToInt32(Comando.Parameters["@Error"].Value);
                    string ErrorDescripcion = Comando.Parameters["@MensajeError"].Value.ToString();
                    //pId = Convert.ToInt32(Comando.Parameters["@ProyectoIntersectaId"].Value);
                    pPoligonoGML = Comando.Parameters["@GMLPoligono"].Value.ToString();
                    ErrorMapa = ErrorDescripcion;
                    return false;
                }
                else
                    return true;
            }
            else
                return true;


        }

        public bool Actualizar_Poligono_Finca(XmlDocument pPoligono, ref int pId, ref String pPoligonoGML, ref string ErrorMapa)
        {

            int Id = pId;
            SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConexionSql"]);
            cn.Open();
            SqlCommand Comando = new SqlCommand("sp_actualizar_poligonos_finca", cn);
            Comando.CommandType = CommandType.StoredProcedure;

            Comando.Parameters.Add("@InmuebleId", SqlDbType.Int, 1);
            Comando.Parameters["@InmuebleId"].Value = Id;

            Comando.Parameters.Add("@ProyectoIntersectaId", SqlDbType.Int, 0);
            Comando.Parameters["@ProyectoIntersectaId"].Direction = ParameterDirection.Output;

            Comando.Parameters.Add("@GMLPoligono", SqlDbType.VarChar, -1);
            Comando.Parameters["@GMLPoligono"].Direction = ParameterDirection.Output;

            Comando.Parameters.Add("@Puntos", SqlDbType.Xml, -1);
            Comando.Parameters["@Puntos"].Value = pPoligono.OuterXml;

            Comando.Parameters.Add("@MensajeError", SqlDbType.VarChar, -1);
            Comando.Parameters["@MensajeError"].Direction = ParameterDirection.Output;

            Comando.Parameters.Add("@Error", SqlDbType.Int, 0);
            Comando.Parameters["@Error"].Direction = ParameterDirection.Output;

            
            Comando.ExecuteNonQuery();

            //-	Aquí es donde ejecuta el proceso almacena donde se ingresan nuevos polígonos o se actualizan este proceso hace las dos funciones y es en ese proceso donde convierte las coordenadas GTM a coordenadas geográficas las x las convierte en latitud y las Y en longitud

            if (!Convert.IsDBNull(Comando.Parameters["@Error"].Value))
            {
                if (Convert.ToInt32(Comando.Parameters["@Error"].Value) != 0)
                {
                    Int32 Error = Convert.ToInt32(Comando.Parameters["@Error"].Value);
                    string ErrorDescripcion = Comando.Parameters["@MensajeError"].Value.ToString();
                    //pId = Convert.ToInt32(Comando.Parameters["@ProyectoIntersectaId"].Value);
                    pPoligonoGML = Comando.Parameters["@GMLPoligono"].Value.ToString();
                    ErrorMapa = ErrorDescripcion;
                    return false;
                }
                else
                    return true;
            }
            else
                return true;


        }

        public bool actualizar_poligonos_finca_new(XmlDocument pPoligono, ref int pId, ref string ErrorMapa)
        {

            int Id = pId;
            SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConexionSql"]);
            cn.Open();
            SqlCommand Comando = new SqlCommand("sp_actualizar_poligonos_finca_new", cn);
            Comando.CommandType = CommandType.StoredProcedure;

            Comando.Parameters.Add("@InmuebleId", SqlDbType.Int, 1);
            Comando.Parameters["@InmuebleId"].Value = Id;


            Comando.Parameters.Add("@Puntos", SqlDbType.Xml, -1);
            Comando.Parameters["@Puntos"].Value = pPoligono.OuterXml;

            Comando.Parameters.Add("@Mensaje", SqlDbType.VarChar, -1);
            Comando.Parameters["@Mensaje"].Direction = ParameterDirection.Output;

            Comando.ExecuteNonQuery();

            //-	Aquí es donde ejecuta el proceso almacena donde se ingresan nuevos polígonos o se actualizan este proceso hace las dos funciones y es en ese proceso donde convierte las coordenadas GTM a coordenadas geográficas las x las convierte en latitud y las Y en longitud
            if (!Convert.IsDBNull(Comando.Parameters["@Mensaje"].Value))
            {
                ErrorMapa = Comando.Parameters["@Mensaje"].Value.ToString();
                return false;
            }
            else
                return true;
        }

        public bool Actualizar_Poligono_AreaBosque(XmlDocument pPoligono, ref int AsignacionId, ref int InmuebleId, ref String pPoligonoGML, ref string ErrorMapa)
        {

            
            SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConexionSql"]);
            cn.Open();
            SqlCommand Comando = new SqlCommand("sp_actualizar_poligonos_AreaBosque", cn);
            Comando.CommandType = CommandType.StoredProcedure;

            Comando.Parameters.Add("@AsignacionId", SqlDbType.Int);
            Comando.Parameters["@AsignacionId"].Value = AsignacionId;
            
            Comando.Parameters.Add("@InmuebleId", SqlDbType.Int);
            Comando.Parameters["@InmuebleId"].Value = InmuebleId;

            Comando.Parameters.Add("@ProyectoIntersectaId", SqlDbType.Int, 0);
            Comando.Parameters["@ProyectoIntersectaId"].Direction = ParameterDirection.Output;

            Comando.Parameters.Add("@GMLPoligono", SqlDbType.VarChar, -1);
            Comando.Parameters["@GMLPoligono"].Direction = ParameterDirection.Output;

            Comando.Parameters.Add("@Puntos", SqlDbType.Xml, -1);
            Comando.Parameters["@Puntos"].Value = pPoligono.OuterXml;

            Comando.Parameters.Add("@MensajeError", SqlDbType.VarChar, -1);
            Comando.Parameters["@MensajeError"].Direction = ParameterDirection.Output;

            Comando.Parameters.Add("@Error", SqlDbType.Int, 0);
            Comando.Parameters["@Error"].Direction = ParameterDirection.Output;


            Comando.ExecuteNonQuery();

            //-	Aquí es donde ejecuta el proceso almacena donde se ingresan nuevos polígonos o se actualizan este proceso hace las dos funciones y es en ese proceso donde convierte las coordenadas GTM a coordenadas geográficas las x las convierte en latitud y las Y en longitud

            if (!Convert.IsDBNull(Comando.Parameters["@Error"].Value))
            {
                if (Convert.ToInt32(Comando.Parameters["@Error"].Value) != 0)
                {
                    Int32 Error = Convert.ToInt32(Comando.Parameters["@Error"].Value);
                    string ErrorDescripcion = Comando.Parameters["@MensajeError"].Value.ToString();
                    //pId = Convert.ToInt32(Comando.Parameters["@ProyectoIntersectaId"].Value);
                    pPoligonoGML = Comando.Parameters["@GMLPoligono"].Value.ToString();
                    ErrorMapa = ErrorDescripcion;
                    return false;
                }
                else
                    return true;
            }
            else
                return true;


        }

        public bool Actualizar_Poligono_AreaIntervenir(XmlDocument pPoligono, ref int AsignacionId, ref int InmuebleId, ref String pPoligonoGML, ref string ErrorMapa)
        {


            SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConexionSql"]);
            cn.Open();
            SqlCommand Comando = new SqlCommand("sp_actualizar_poligonos_AreaIntervernir", cn);
            Comando.CommandType = CommandType.StoredProcedure;

            Comando.Parameters.Add("@AsignacionId", SqlDbType.Int);
            Comando.Parameters["@AsignacionId"].Value = AsignacionId;

            Comando.Parameters.Add("@InmuebleId", SqlDbType.Int);
            Comando.Parameters["@InmuebleId"].Value = InmuebleId;

            Comando.Parameters.Add("@ProyectoIntersectaId", SqlDbType.Int, 0);
            Comando.Parameters["@ProyectoIntersectaId"].Direction = ParameterDirection.Output;

            Comando.Parameters.Add("@GMLPoligono", SqlDbType.VarChar, -1);
            Comando.Parameters["@GMLPoligono"].Direction = ParameterDirection.Output;

            Comando.Parameters.Add("@Puntos", SqlDbType.Xml, -1);
            Comando.Parameters["@Puntos"].Value = pPoligono.OuterXml;

            Comando.Parameters.Add("@MensajeError", SqlDbType.VarChar, -1);
            Comando.Parameters["@MensajeError"].Direction = ParameterDirection.Output;

            Comando.Parameters.Add("@Error", SqlDbType.Int, 0);
            Comando.Parameters["@Error"].Direction = ParameterDirection.Output;


            Comando.ExecuteNonQuery();

            //-	Aquí es donde ejecuta el proceso almacena donde se ingresan nuevos polígonos o se actualizan este proceso hace las dos funciones y es en ese proceso donde convierte las coordenadas GTM a coordenadas geográficas las x las convierte en latitud y las Y en longitud

            if (!Convert.IsDBNull(Comando.Parameters["@Error"].Value))
            {
                if (Convert.ToInt32(Comando.Parameters["@Error"].Value) != 0)
                {
                    Int32 Error = Convert.ToInt32(Comando.Parameters["@Error"].Value);
                    string ErrorDescripcion = Comando.Parameters["@MensajeError"].Value.ToString();
                    //pId = Convert.ToInt32(Comando.Parameters["@ProyectoIntersectaId"].Value);
                    pPoligonoGML = Comando.Parameters["@GMLPoligono"].Value.ToString();
                    ErrorMapa = ErrorDescripcion;
                    return false;
                }
                else
                    return true;
            }
            else
                return true;


        }


        public bool Actualizar_Poligono_AreaProteger(XmlDocument pPoligono, ref int AsignacionId, ref int InmuebleId, ref String pPoligonoGML, ref string ErrorMapa)
        {


            SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConexionSql"]);
            cn.Open();
            SqlCommand Comando = new SqlCommand("sp_actualizar_poligonos_AreaProteger", cn);
            Comando.CommandType = CommandType.StoredProcedure;

            Comando.Parameters.Add("@AsignacionId", SqlDbType.Int);
            Comando.Parameters["@AsignacionId"].Value = AsignacionId;

            Comando.Parameters.Add("@InmuebleId", SqlDbType.Int);
            Comando.Parameters["@InmuebleId"].Value = InmuebleId;

            Comando.Parameters.Add("@ProyectoIntersectaId", SqlDbType.Int, 0);
            Comando.Parameters["@ProyectoIntersectaId"].Direction = ParameterDirection.Output;

            Comando.Parameters.Add("@GMLPoligono", SqlDbType.VarChar, -1);
            Comando.Parameters["@GMLPoligono"].Direction = ParameterDirection.Output;

            Comando.Parameters.Add("@Puntos", SqlDbType.Xml, -1);
            Comando.Parameters["@Puntos"].Value = pPoligono.OuterXml;

            Comando.Parameters.Add("@MensajeError", SqlDbType.VarChar, -1);
            Comando.Parameters["@MensajeError"].Direction = ParameterDirection.Output;

            Comando.Parameters.Add("@Error", SqlDbType.Int, 0);
            Comando.Parameters["@Error"].Direction = ParameterDirection.Output;


            Comando.ExecuteNonQuery();

            //-	Aquí es donde ejecuta el proceso almacena donde se ingresan nuevos polígonos o se actualizan este proceso hace las dos funciones y es en ese proceso donde convierte las coordenadas GTM a coordenadas geográficas las x las convierte en latitud y las Y en longitud

            if (!Convert.IsDBNull(Comando.Parameters["@Error"].Value))
            {
                if (Convert.ToInt32(Comando.Parameters["@Error"].Value) != 0)
                {
                    Int32 Error = Convert.ToInt32(Comando.Parameters["@Error"].Value);
                    string ErrorDescripcion = Comando.Parameters["@MensajeError"].Value.ToString();
                    //pId = Convert.ToInt32(Comando.Parameters["@ProyectoIntersectaId"].Value);
                    pPoligonoGML = Comando.Parameters["@GMLPoligono"].Value.ToString();
                    ErrorMapa = ErrorDescripcion;
                    return false;
                }
                else
                    return true;
            }
            else
                return true;


        }




        public void Lee_Poligono(string Id)
        {
            //var geoData = new GeoRssData("obtenerPoligonos", "Some Description");

            /* Debe obtenerse una cadena de puntos x y separados cada punto por un espacio en blanco */
            SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["Conexion"]);
            cn.Open();
            SqlCommand Comando = new SqlCommand("sp_obtener_poligonosCOMP", cn);
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.Parameters.Add("@ProyectoId", SqlDbType.Int, 1);
            Comando.Parameters["@ProyectoId"].Value = Id;


            // Comando.ExecuteNonQuery();


            SqlDataAdapter da = new SqlDataAdapter(Comando);
            DataSet iDtDatos = new DataSet();

            // Por último, rellenamos el DataSet
            da.Fill(iDtDatos);

            if ((iDtDatos != null) && (iDtDatos.Tables.Count > 0) && (iDtDatos.Tables[0].Rows.Count > 0))
            {
                //-	Aquí es donde ejecuta el proceso almacena donde se ingresan nuevos polígonos o se actualizan este proceso hace las dos funciones y es en ese proceso donde convierte las coordenadas GTM a coordenadas geográficas las x las convierte en latitud y las Y en longitud

                foreach (DataRow iDtRow in iDtDatos.Tables[0].Rows)
                {
                    // geoData.AddPolygon(iDtRow["Id"].ToString(), iDtRow["Descripcion"].ToString(), iDtRow["Poligono"].ToString());
                    string Desc = iDtRow["Descripcion"].ToString();
                }
            }

        }

        public bool EliminarPoligonos(int Id, int NoPoligonos)
        {
            SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["Conexion"]);
            cn.Open();
            SqlCommand Comando = new SqlCommand("Sp_Elimina_poligono", cn);
            Comando.CommandType = CommandType.StoredProcedure;

            Comando.Parameters.Add("@ProyectoId", SqlDbType.Int, 1);
            Comando.Parameters["@ProyectoId"].Value = Id;

            Comando.Parameters.Add("@PoligonoId", SqlDbType.Int, 1);
            Comando.Parameters["@PoligonoId"].Value = NoPoligonos;

            Comando.Parameters.Add("@MensajeError", SqlDbType.VarChar, -1);
            Comando.Parameters["@MensajeError"].Direction = ParameterDirection.Output;

            Comando.Parameters.Add("@Error", SqlDbType.Int, 0);
            Comando.Parameters["@Error"].Direction = ParameterDirection.Output;

            Comando.ExecuteNonQuery();

            if (!Convert.IsDBNull(Comando.Parameters["@Error"].Value))
            {
                if (Convert.ToInt32(Comando.Parameters["@Error"].Value) != 0)
                {
                    Int32 Error = Convert.ToInt32(Comando.Parameters["@Error"].Value);
                    string ErrorDescripcion = Comando.Parameters["@MensajeError"].Value.ToString();

                    return false;
                }
                else return true;

            }
            else return true;

        }

        public bool Actualizar_Poligono_AreaRepoblacion(XmlDocument pPoligono, ref int AsignacionId, ref String pPoligonoGML, ref string ErrorMapa)
        {


            SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConexionSql"]);
            cn.Open();
            SqlCommand Comando = new SqlCommand("sp_actualizar_poligonos_AreaRepoblacion", cn);
            Comando.CommandType = CommandType.StoredProcedure;

            Comando.Parameters.Add("@AsignacionId", SqlDbType.Int);
            Comando.Parameters["@AsignacionId"].Value = AsignacionId;

            
            Comando.Parameters.Add("@ProyectoIntersectaId", SqlDbType.Int, 0);
            Comando.Parameters["@ProyectoIntersectaId"].Direction = ParameterDirection.Output;

            Comando.Parameters.Add("@GMLPoligono", SqlDbType.VarChar, -1);
            Comando.Parameters["@GMLPoligono"].Direction = ParameterDirection.Output;

            Comando.Parameters.Add("@Puntos", SqlDbType.Xml, -1);
            Comando.Parameters["@Puntos"].Value = pPoligono.OuterXml;

            Comando.Parameters.Add("@MensajeError", SqlDbType.VarChar, -1);
            Comando.Parameters["@MensajeError"].Direction = ParameterDirection.Output;

            Comando.Parameters.Add("@Error", SqlDbType.Int, 0);
            Comando.Parameters["@Error"].Direction = ParameterDirection.Output;


            Comando.ExecuteNonQuery();

            //-	Aquí es donde ejecuta el proceso almacena donde se ingresan nuevos polígonos o se actualizan este proceso hace las dos funciones y es en ese proceso donde convierte las coordenadas GTM a coordenadas geográficas las x las convierte en latitud y las Y en longitud

            if (!Convert.IsDBNull(Comando.Parameters["@Error"].Value))
            {
                if (Convert.ToInt32(Comando.Parameters["@Error"].Value) != 0)
                {
                    Int32 Error = Convert.ToInt32(Comando.Parameters["@Error"].Value);
                    string ErrorDescripcion = Comando.Parameters["@MensajeError"].Value.ToString();
                    //pId = Convert.ToInt32(Comando.Parameters["@ProyectoIntersectaId"].Value);
                    pPoligonoGML = Comando.Parameters["@GMLPoligono"].Value.ToString();
                    ErrorMapa = ErrorDescripcion;
                    return false;
                }
                else
                    return true;
            }
            else
                return true;


        }


        public bool actualizar_poligonos_region(XmlDocument pPoligono, ref int pId, ref String pPoligonoGML, int Usrcre, ref string ErrorMapa)
        {

            int Id = pId;
            SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConexionSql"]);
            cn.Open();
            SqlCommand Comando = new SqlCommand("sp_actualizar_poligonos_region", cn);
            Comando.CommandType = CommandType.StoredProcedure;

            Comando.Parameters.Add("@RegionId", SqlDbType.Int, 1);
            Comando.Parameters["@RegionId"].Value = Id;

            Comando.Parameters.Add("@ProyectoIntersectaId", SqlDbType.Int, 0);
            Comando.Parameters["@ProyectoIntersectaId"].Direction = ParameterDirection.Output;

            Comando.Parameters.Add("@GMLPoligono", SqlDbType.VarChar, -1);
            Comando.Parameters["@GMLPoligono"].Direction = ParameterDirection.Output;

            Comando.Parameters.Add("@Puntos", SqlDbType.Xml, -1);
            Comando.Parameters["@Puntos"].Value = pPoligono.OuterXml;

            Comando.Parameters.Add("@MensajeError", SqlDbType.VarChar, -1);
            Comando.Parameters["@MensajeError"].Direction = ParameterDirection.Output;

            Comando.Parameters.Add("@Error", SqlDbType.Int, 0);
            Comando.Parameters["@Error"].Direction = ParameterDirection.Output;

            Comando.Parameters.Add("@Usrcre", SqlDbType.Int, 0);
            Comando.Parameters["@usrcre"].Value = Usrcre;
            // Comando.Parameters["@Error"].Value = 1;
            Comando.ExecuteNonQuery();

            //-	Aquí es donde ejecuta el proceso almacena donde se ingresan nuevos polígonos o se actualizan este proceso hace las dos funciones y es en ese proceso donde convierte las coordenadas GTM a coordenadas geográficas las x las convierte en latitud y las Y en longitud

            if (!Convert.IsDBNull(Comando.Parameters["@Error"].Value))
            {
                if (Convert.ToInt32(Comando.Parameters["@Error"].Value) != 0)
                {
                    Int32 Error = Convert.ToInt32(Comando.Parameters["@Error"].Value);
                    string ErrorDescripcion = Comando.Parameters["@MensajeError"].Value.ToString();
                    //pId = Convert.ToInt32(Comando.Parameters["@ProyectoIntersectaId"].Value);
                    pPoligonoGML = Comando.Parameters["@GMLPoligono"].Value.ToString();
                    ErrorMapa = ErrorDescripcion;
                    return false;
                }
                else
                    return true;
            }
            else
                return true;


        }
        
    }
}