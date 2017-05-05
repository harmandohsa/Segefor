using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml;

namespace SEGEFOR.Clases
{
    public class Cl_Poligono
    {
        OleDbConnection cn = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["Conexion"]);
        DataSet ds = new DataSet();

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

        public bool actualizar_poligonos_finca_new(XmlDocument pPoligono, ref int pId, ref string ErrorMapa, int RegionId)
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

            Comando.Parameters.Add("@RegionId", SqlDbType.Int, -1);
            Comando.Parameters["@RegionId"].Value = RegionId;

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

        public bool Actualizar_Poligono_AreaBosque(XmlDocument pPoligono, ref int AsignacionId, ref int InmuebleId, ref string ErrorMapa)
        {

            
            SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConexionSql"]);
            cn.Open();
            SqlCommand Comando = new SqlCommand("sp_poligonos_AreaBosque", cn);
            Comando.CommandType = CommandType.StoredProcedure;

            Comando.Parameters.Add("@AsignacionId", SqlDbType.Int);
            Comando.Parameters["@AsignacionId"].Value = AsignacionId;
            
            Comando.Parameters.Add("@InmuebleId", SqlDbType.Int);
            Comando.Parameters["@InmuebleId"].Value = InmuebleId;

            Comando.Parameters.Add("@Puntos", SqlDbType.Xml, -1);
            Comando.Parameters["@Puntos"].Value = pPoligono.OuterXml;

            Comando.Parameters.Add("@Mensaje", SqlDbType.VarChar, -1);
            Comando.Parameters["@Mensaje"].Direction = ParameterDirection.Output;


            Comando.ExecuteNonQuery();

            //-	Aquí es donde ejecuta el proceso almacena donde se ingresan nuevos polígonos o se actualizan este proceso hace las dos funciones y es en ese proceso donde convierte las coordenadas GTM a coordenadas geográficas las x las convierte en latitud y las Y en longitud

            if (!Convert.IsDBNull(Comando.Parameters["@Mensaje"].Value))
            {
                string ErrorDescripcion = Comando.Parameters["@Mensaje"].Value.ToString();
                ErrorMapa = ErrorDescripcion;
                return false;
            }
            else
                return true;


        }




        public bool Actualizar_Poligono_AreaIntervenir(XmlDocument pPoligono, ref int AsignacionId, ref int InmuebleId, ref string ErrorMapa)
        {


            SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConexionSql"]);
            cn.Open();
            SqlCommand Comando = new SqlCommand("sp_poligonos_AreaIntevenir", cn);
            Comando.CommandType = CommandType.StoredProcedure;

            Comando.Parameters.Add("@AsignacionId", SqlDbType.Int);
            Comando.Parameters["@AsignacionId"].Value = AsignacionId;

            Comando.Parameters.Add("@InmuebleId", SqlDbType.Int);
            Comando.Parameters["@InmuebleId"].Value = InmuebleId;


            Comando.Parameters.Add("@Puntos", SqlDbType.Xml, -1);
            Comando.Parameters["@Puntos"].Value = pPoligono.OuterXml;

            Comando.Parameters.Add("@Mensaje", SqlDbType.VarChar, -1);
            Comando.Parameters["@Mensaje"].Direction = ParameterDirection.Output;


            Comando.ExecuteNonQuery();

            //-	Aquí es donde ejecuta el proceso almacena donde se ingresan nuevos polígonos o se actualizan este proceso hace las dos funciones y es en ese proceso donde convierte las coordenadas GTM a coordenadas geográficas las x las convierte en latitud y las Y en longitud

            if (!Convert.IsDBNull(Comando.Parameters["@Mensaje"].Value))
            {
                string ErrorDescripcion = Comando.Parameters["@Mensaje"].Value.ToString();
                ErrorMapa = ErrorDescripcion;
                return false;
            }
            else
                return true;


        }


        public bool Actualizar_Poligono_AreaProteger(XmlDocument pPoligono, ref int AsignacionId, ref int InmuebleId, ref string ErrorMapa)
        {


            SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConexionSql"]);
            cn.Open();
            SqlCommand Comando = new SqlCommand("sp_poligonos_AreaProteccion", cn);
            Comando.CommandType = CommandType.StoredProcedure;

            Comando.Parameters.Add("@AsignacionId", SqlDbType.Int);
            Comando.Parameters["@AsignacionId"].Value = AsignacionId;

            Comando.Parameters.Add("@InmuebleId", SqlDbType.Int);
            Comando.Parameters["@InmuebleId"].Value = InmuebleId;

            Comando.Parameters.Add("@Puntos", SqlDbType.Xml, -1);
            Comando.Parameters["@Puntos"].Value = pPoligono.OuterXml;

            Comando.Parameters.Add("@Mensaje", SqlDbType.VarChar, -1);
            Comando.Parameters["@Mensaje"].Direction = ParameterDirection.Output;


            Comando.ExecuteNonQuery();

            //-	Aquí es donde ejecuta el proceso almacena donde se ingresan nuevos polígonos o se actualizan este proceso hace las dos funciones y es en ese proceso donde convierte las coordenadas GTM a coordenadas geográficas las x las convierte en latitud y las Y en longitud

            if (!Convert.IsDBNull(Comando.Parameters["@Mensaje"].Value))
            {
                    
                    string ErrorDescripcion = Comando.Parameters["@Mensaje"].Value.ToString();
                    ErrorMapa = ErrorDescripcion;
                    return false;
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

        public DataSet puntos_poligonos_Inmueble(int InmuebleId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_puntos_poligonos_Inmueble", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                adp.Fill(ds, "DATOS");
                cn.Close();
                return ds;

            }
            catch (Exception ex)
            {
                cn.Close();
                return ds;
            }
        }

        public int Existe_Poligono_Inmueble(int InmuebleId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Existe_Poligono_Inmueble", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value);
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public DataSet obtener_poligonos_Inmueble(int InmuebleId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_obtener_poligonos_Inmueble", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                adp.Fill(ds, "DATOS");
                cn.Close();
                return ds;

            }
            catch (Exception ex)
            {
                cn.Close();
                return ds;
            }
        }


        public DataSet puntos_poligonos_AreaBosque(int InmuebleId, int Id, int Tipo)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_puntos_poligonos_AreaBosque", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                adp.Fill(ds, "DATOS");
                cn.Close();
                return ds;

            }
            catch (Exception ex)
            {
                cn.Close();
                return ds;
            }
        }

        public int Existe_Poligono_AreaBosque(int InmuebleId, int Id, int Tipo)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Existe_Poligono_AreaBosque", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value);
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public DataSet obtener_poligonos_AreaBosque(int InmuebleId, int Id, int Tipo)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_obtener_poligonos_AreaBosque", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                adp.Fill(ds, "DATOS");
                cn.Close();
                return ds;

            }
            catch (Exception ex)
            {
                cn.Close();
                return ds;
            }
        }

        public DataSet puntos_poligonos_AreaIntervenir(int InmuebleId, int Id, int Tipo)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_puntos_poligonos_AreaIntervenir", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                adp.Fill(ds, "DATOS");
                cn.Close();
                return ds;

            }
            catch (Exception ex)
            {
                cn.Close();
                return ds;
            }
        }

        public int Existe_Poligono_AreaIntervenir(int InmuebleId, int Id, int Tipo)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Existe_Poligono_AreaIntervenir", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value);
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public DataSet obtener_poligonos_AreaIntervenir(int InmuebleId, int Id, int Tipo)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_obtener_poligonos_AreaIntervenir", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                adp.Fill(ds, "DATOS");
                cn.Close();
                return ds;

            }
            catch (Exception ex)
            {
                cn.Close();
                return ds;
            }
        }


        public DataSet puntos_poligonos_AreaProteccion(int InmuebleId, int Id, int Tipo)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_puntos_poligonos_AreaProteccion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                adp.Fill(ds, "DATOS");
                cn.Close();
                return ds;

            }
            catch (Exception ex)
            {
                cn.Close();
                return ds;
            }
        }

        public int Existe_Poligono_AreaProteccion(int InmuebleId, int Id, int Tipo)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Existe_Poligono_AreaProteccion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value);
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public DataSet obtener_poligonos_AreaProteccion(int InmuebleId, int Id, int Tipo)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_obtener_poligonos_AreaProteccion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                adp.Fill(ds, "DATOS");
                cn.Close();
                return ds;

            }
            catch (Exception ex)
            {
                cn.Close();
                return ds;
            }
        }


        public DataSet puntos_poligonos_Repoblacion(int Id, int Tipo)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_puntos_poligonos_Repoblacion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                adp.Fill(ds, "DATOS");
                cn.Close();
                return ds;

            }
            catch (Exception ex)
            {
                cn.Close();
                return ds;
            }
        }

        public int Existe_Poligono_Repoblacion(int Id, int Tipo)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Existe_Poligono_Repoblacion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value);
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public DataSet obtener_poligonos_Repoblacion(int Id, int Tipo)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_obtener_poligonos_Repoblacion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                adp.Fill(ds, "DATOS");
                cn.Close();
                return ds;

            }
            catch (Exception ex)
            {
                cn.Close();
                return ds;
            }
        }

    }
}