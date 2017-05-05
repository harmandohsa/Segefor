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
    public class Cl_Manejo
    {
        OleDbConnection cn = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["Conexion"]);
        DataSet ds = new DataSet();
        SqlConnection cnSql = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConexionSql"]);
        Cl_Manejo_Impresion ClManejoImpresion;
        Cl_Xml ClXml;

        public DataSet Get_Regentes(double Area, int SubCategoriaId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Regentes", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Area", OleDbType.Double).Value = Area;
                cmd.Parameters.Add("@SubCategoriaId", OleDbType.Integer).Value = SubCategoriaId;
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

        public void AsignarElaborador(int RegistroId, int UsuarioId, double Area, int CategoriaId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("SP_AsignarElaborador", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@RegistroId", SqlDbType.Int).Value = RegistroId;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                cmd.Parameters.Add("@Area", SqlDbType.Float).Value = Area;
                cmd.Parameters.Add("@CategoriaId", SqlDbType.Int).Value = CategoriaId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public DataSet Get_PlanesManejo(int Tipo, int PersonaId, int UsuarioId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_PlanesManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = PersonaId;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
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

        public void ActualizaEstatusFechaAsignacionElaborador(int AsignacionId, int EstatusId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("SP_ActualizaEstatusFechaAsignacionElaborador", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@EstatusId", SqlDbType.Int).Value = EstatusId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void ActualizaSubCategoriaPlan(int AsignacionId, int SubCategoriaId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("SP_ActualizaSubCategoriaPlan", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@SubCategoriaId", SqlDbType.Int).Value = SubCategoriaId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public string Get_SubCategoria(int SubCategoriaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_SubCategoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SubCategoriaId", OleDbType.Integer).Value = SubCategoriaId;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 400).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "";
            }
        }

        public string Get_Categoria(int CategoriaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Nombre_SubcategoriaMF", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CategoriaId", OleDbType.Integer).Value = CategoriaId;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 200).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "";
            }
        }

        public int Get_SubCategoriaPlanManejo(int AsignacionId, int Tipo)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_SubCategoriaPlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = @AsignacionId;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 400).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public void InsertTempFincaPlanManejo(int AsignacionId, int InmuebleId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("sp_InsertTempFincaPlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@InmuebleId", SqlDbType.Int).Value = InmuebleId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public DataSet Get_TempFincaPlanManejo(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_GetTempFincaPlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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



        public DataSet GetFincaPlanManejoPol(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_GetFincaPlanManejoPol", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = GestionId;
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

        public void EliminarTempFincaPlanManejo(int AsignacionId, int InmuebleId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("sp_EliminarTempFincaPlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@InmuebleId", SqlDbType.Int).Value = InmuebleId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void InsertTempFincaPropietarioPlanManejo(int AsignacionId, int InmuebleId, int PersonaId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("sp_InsertTempFincaPropietarioPlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@InmuebleId", SqlDbType.Int).Value = InmuebleId;
                cmd.Parameters.Add("@PersonaId", SqlDbType.Int).Value = PersonaId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void InsertTempRepresentantePlanManejo(int AsignacionId, int PersonaId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("sp_InsertTempRepresentantePlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@PersonaId", SqlDbType.Int).Value = PersonaId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public DataSet GetPropietarios_Inmuebles_Manejo(int AsignacionId, int InmuebleId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_GetPropietarios_Inmuebles_Manejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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


        public DataSet GetRepresentantes_Manejo(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_GetRepresentantes_Manejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public void ActualizarTipoPersonaTempFincaPlanManejo(int AsignacionId, int InmuebleId, int Tipo_PersonaId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("sp_ActualizarTipoPersonaTempFincaPlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@InmuebleId", SqlDbType.Int).Value = InmuebleId;
                cmd.Parameters.Add("@Tipo_PersonaId", SqlDbType.Int).Value = Tipo_PersonaId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public DataSet GetTipoPersonaTempFincaPlanManejo(int AsignacionId, int InmuebleId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_GetTipoPersonaTempFincaPlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public void ActualizarNombre_EmpresaTempFincaPlanManejo(int AsignacionId, int InmuebleId, string Nombre_Empresa)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("sp_ActualizarNombre_EmpresaTempFincaPlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@InmuebleId", SqlDbType.Int).Value = InmuebleId;
                cmd.Parameters.Add("@Nombre_Empresa", SqlDbType.VarChar,300).Value = Nombre_Empresa;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void EliminarTempPropietariosPlanManejo(int AsignacionId, int InmuebleId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("sp_EliminarTempPropietariosPlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@InmuebleId", SqlDbType.Int).Value = InmuebleId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Insertar_AreasInmueble(int AsignacionId, int InmuebleId, double Forestal, double PorForestal, double Agricultura, double PorAgricultura, double Ganaderia, double PorGanaderia, double Agroforestal, double PorAgroforestal, string OtroEspecifique, double Otro, double PorOtro, int Tipo_BosqueId, XmlDocument Especies, double AreaBosque, double AreaIntervenir, double AreaProteccion, double Pendiente, double PorPendiente, double Profundidad, double PorProfundidad, double Pedregosidad, double PorPedregosidad, double Anegamiento, double PorAnegamiento, double BosqueGaleria, double PorBosuqeGaleria, double EspeciesProtegidas, double PorEspeciesProtegidas, string OtroProteccionEspecifique, double OtroProteccion, double PorOtroProteccion)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insertar_AreasInmueble", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@InmuebleId", SqlDbType.Int).Value = InmuebleId;
                cmd.Parameters.Add("@Forestal", SqlDbType.Float).Value = Forestal;

                cmd.Parameters.Add("@PorForestal", SqlDbType.Float).Value = PorForestal;
                cmd.Parameters.Add("@Agricultura", SqlDbType.Float).Value = Agricultura;
                cmd.Parameters.Add("@PorAgricultura", SqlDbType.Float).Value = PorAgricultura;
                cmd.Parameters.Add("@Ganaderia", SqlDbType.Float).Value = Ganaderia;
                cmd.Parameters.Add("@PorGanaderia", SqlDbType.Float).Value = PorGanaderia;
                cmd.Parameters.Add("@Agroforestal", SqlDbType.Float).Value = Agroforestal;
                cmd.Parameters.Add("@PorAgroforestal", SqlDbType.Float).Value = PorAgroforestal;
                cmd.Parameters.Add("@OtroEspecifique", SqlDbType.VarChar,300).Value = OtroEspecifique;
                cmd.Parameters.Add("@Otro", SqlDbType.Float).Value = Otro;
                cmd.Parameters.Add("@PorOtro", SqlDbType.Float).Value = PorOtro;
                cmd.Parameters.Add("@Tipo_BosqueId", SqlDbType.Int).Value = Tipo_BosqueId;
                cmd.Parameters.Add("@Especies", SqlDbType.Xml).Value = Especies.OuterXml.ToString();
                cmd.Parameters.Add("@AreaBosque", SqlDbType.Float).Value = AreaBosque;
                cmd.Parameters.Add("@AreaIntervenir", SqlDbType.Float).Value = AreaIntervenir;
                cmd.Parameters.Add("@AreaProteccion", SqlDbType.Float).Value = AreaProteccion;
                cmd.Parameters.Add("@Pendiente", SqlDbType.Float).Value = Pendiente;
                cmd.Parameters.Add("@PorPendiente", SqlDbType.Float).Value = PorPendiente;
                cmd.Parameters.Add("@Profundidad", SqlDbType.Float).Value = Profundidad;
                cmd.Parameters.Add("@PorProfundidad", SqlDbType.Float).Value = PorProfundidad;
                cmd.Parameters.Add("@Pedregosidad", SqlDbType.Float).Value = Pedregosidad;
                cmd.Parameters.Add("@PorPedregosidad", SqlDbType.Float).Value = PorPedregosidad;
                cmd.Parameters.Add("@Anegamiento", SqlDbType.Float).Value = Anegamiento;
                cmd.Parameters.Add("@PorAnegamiento", SqlDbType.Float).Value = PorAnegamiento;
                cmd.Parameters.Add("@BosqueGaleria", SqlDbType.Float).Value = BosqueGaleria;
                cmd.Parameters.Add("@PorBosqueGaleria", SqlDbType.Float).Value = PorBosuqeGaleria;
                cmd.Parameters.Add("@EspeciesProtegidas", SqlDbType.Float).Value = EspeciesProtegidas;
                cmd.Parameters.Add("@PorEspeciesProtegidas", SqlDbType.Float).Value = PorEspeciesProtegidas;
                cmd.Parameters.Add("@OtroProteccionEspecifique", SqlDbType.VarChar, 300).Value = OtroProteccionEspecifique;
                cmd.Parameters.Add("@OtroProteccion", SqlDbType.Float).Value = OtroProteccion;
                cmd.Parameters.Add("@PorOtroProteccion", SqlDbType.Float).Value = PorOtroProteccion;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Insertar_AreasInmueble_PlanSanitario(int AsignacionId, int InmuebleId, int Tipo_BosqueId, XmlDocument Especies, double AreaBosque)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insertar_AreasInmueble_PlanSanitario", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@InmuebleId", SqlDbType.Int).Value = InmuebleId;
                cmd.Parameters.Add("@Tipo_BosqueId", SqlDbType.Int).Value = Tipo_BosqueId;
                cmd.Parameters.Add("@Especies", SqlDbType.Xml).Value = Especies.OuterXml.ToString();
                cmd.Parameters.Add("@AreaBosque", SqlDbType.Float).Value = AreaBosque;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Eliminar_AreasInmueble(int AsignacionId, int InmuebleId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Eliminar_AreasInmueble", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@InmuebleId", SqlDbType.Int).Value = InmuebleId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Eliminar_PoligonoFinca_Bosque(int AsignacionId, int InmuebleId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Eliminar_PoligonoFinca_Bosque", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@InmuebleId", SqlDbType.Int).Value = InmuebleId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Eliminar_PoligonoFinca_Intervenir(int AsignacionId, int InmuebleId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Eliminar_PoligonoFinca_Intervenir", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@InmuebleId", SqlDbType.Int).Value = InmuebleId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Eliminar_PoligonoFinca_Proteccion(int AsignacionId, int InmuebleId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Eliminar_PoligonoFinca_Proteccion", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@InmuebleId", SqlDbType.Int).Value = InmuebleId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Insertar_ClaseDesarrolloFinca_PlanManejo(int AsignacionId, int InmuebleId, int Clase_DesarrolloId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insertar_ClaseDesarrolloFinca_PlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@InmuebleId", SqlDbType.Int).Value = InmuebleId;
                cmd.Parameters.Add("@Clase_DesarrolloId", SqlDbType.Int).Value = Clase_DesarrolloId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Eliminar_ClaseDesarrolloFinca_PlanManejo(int AsignacionId, int InmuebleId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Eliminar_ClaseDesarrolloFinca_PlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@InmuebleId", SqlDbType.Int).Value = InmuebleId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Eliminar_Propietarios_Finca(int AsignacionId, int InmuebleId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Eliminar_Propietarios_Finca", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@InmuebleId", SqlDbType.Int).Value = InmuebleId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public DataSet GetAreasFinca_PlanManejo(int AsignacionId, int InmuebleId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_GetAreasFinca_PlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public DataSet GetClaseDesarrolloFinca_PlanManejo(int AsignacionId, int InmuebleId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_GetClaseDesarrolloFinca_PlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public DataSet EspeciesFinca_PlanManejo(int AsignacionId, int InmuebleId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_EspeciesFinca_PlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public DataSet obtener_puntos_poligonos_AreaBosque(int AsignacionId, int InmuebleId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_obtener_puntos_poligonos_AreaBosque", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

       

        public DataSet obtener_puntos_poligonos_AreaIntervencion(int AsignacionId, int InmuebleId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_obtener_puntos_poligonos_AreaIntervencion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public DataSet obtener_puntos_poligonos_AreaProteccion(int AsignacionId, int InmuebleId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_obtener_puntos_poligonos_AreaProteccion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public void Elimina_PropietarioFinca_PlanManejo(int AsignacionId, int InmuebleId, int PersonaId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Elimina_PropietarioFinca_PlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@InmuebleId", SqlDbType.Int).Value = InmuebleId;
                cmd.Parameters.Add("@PersonaId", SqlDbType.Int).Value = PersonaId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Elimina_Representante_PlanManejo(int AsignacionId, int PersonaId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Elimina_Representante_PlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@PersonaId", SqlDbType.Int).Value = PersonaId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Insertar_Datos_Notifica_PlanManejo(int AsignacionId, string Direccion, int MunicipioId, int TelDomicilio, int Telefono, int Celular, string Correo)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insertar_Datos_Notifica_PlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@Direccion", SqlDbType.VarChar,300).Value = Direccion;
                cmd.Parameters.Add("@MunicipioId", SqlDbType.Int).Value = MunicipioId;
                cmd.Parameters.Add("@TelDomicilio", SqlDbType.Int).Value = TelDomicilio;
                cmd.Parameters.Add("@Telefono", SqlDbType.Int).Value = Telefono;
                cmd.Parameters.Add("@Celular", SqlDbType.Int).Value = Celular;
                cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 200).Value = Correo;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Delete_Datos_Notifica_PlanManejo(int AsignacionId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Delete_Datos_Notifica_PlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public DataSet Get_Datos_Notifica_PlanManejo(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Datos_Notifica_PlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public void Insert_InfoGeneral_PlanManejo(int AsignacionId, int TiempoEjecucion, int TiempoExtraccion, double VolExtraer, string SistemaCorta, double IncrementoAnual, double CortaAnual, XmlDocument Especies, int Tipo_GarantiaId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insert_InfoGeneral_PlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@TiempoEjecucion", SqlDbType.Int).Value = TiempoEjecucion;
                cmd.Parameters.Add("@TiempoExtraccion", SqlDbType.Int).Value = TiempoExtraccion;
                cmd.Parameters.Add("@VolExtraer", SqlDbType.Float).Value = VolExtraer;
                cmd.Parameters.Add("@SistemaCorta", SqlDbType.VarChar, 300).Value = SistemaCorta;
                cmd.Parameters.Add("@IncrementoAnual", SqlDbType.Float).Value = IncrementoAnual;
                cmd.Parameters.Add("@CortaAnual", SqlDbType.Float).Value = CortaAnual;
                cmd.Parameters.Add("@Especies", SqlDbType.Xml).Value =  Especies.OuterXml.ToString();
                cmd.Parameters.Add("@Tipo_GarantiaId", SqlDbType.Int).Value = Tipo_GarantiaId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Insert_Accion_Repoblacion_PlanManejo(int AsignacionId, XmlDocument Especies, int Densidad)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insert_Accion_Repoblacion_PlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@Especies", SqlDbType.Xml).Value = Especies.OuterXml.ToString();
                cmd.Parameters.Add("@Densidad", SqlDbType.Int).Value = Densidad;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Insert_SistemaRepoblacionPlanManejo(int AsignacionId, int SistemaRepoblacionId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insert_SistemaRepoblacionPlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@SistemaRepoblacionId", SqlDbType.Int).Value = SistemaRepoblacionId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Insert_RedCaminos_PlanManejo(int AsignacionId, double Primario_Existente, double Primario_Construccion, double Secundario_Existente, double Secundario_Construccion, string Otro_Especifique, double Otro_Existente, double Otro_Construccion)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insert_RedCaminos_PlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@Primario_Existente", SqlDbType.Float).Value = Primario_Existente;
                cmd.Parameters.Add("@Primario_Construir", SqlDbType.Float).Value = Primario_Construccion;
                cmd.Parameters.Add("@Secundario_Existente", SqlDbType.Float).Value = Secundario_Existente;
                cmd.Parameters.Add("@Secundario_Construir", SqlDbType.Float).Value = Secundario_Construccion;
                cmd.Parameters.Add("@Otro_Especifique", SqlDbType.VarChar,200).Value = Otro_Especifique;
                cmd.Parameters.Add("@Otro_Existente", SqlDbType.Float).Value = Otro_Existente;
                cmd.Parameters.Add("@Otro_Construir", SqlDbType.Float).Value = Otro_Construccion;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public DataSet Get_InfoGeneral_PlanManejo(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_InfoGeneral_PlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public DataSet Get_Repoblacion_PlanManejo(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Repoblacion_PlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public DataSet EspeciesInfoGeneral_PlanManejo(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Especies_InfoGeneral_PlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public DataSet Especies_Accion_Repoblacion_PlanManejo(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Especies_Accion_Repoblacion_PlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public DataSet Get_SistemaRepoblacionPlanManejo(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_SistemaRepoblacionPlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public DataSet SP_Get_Censo_PlanManejo(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Censo_PlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public DataSet obtener_puntos_poligonos_Area_Repoblacion(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_obtener_puntos_poligonos_Area_Repoblacion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public void Eliminar_InfoGeneral_PlanManejo(int AsignacionId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("sp_Eliminar_InfoGeneral_PlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Eliminar_InfoGeneral_SistemaRepoblacion_PlanManejo(int AsignacionId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("sp_Eliminar_InfoGeneral_SistemaRepoblacion_PlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Eliminar_Accion_Repoblacion_PlanManejo(int AsignacionId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("sp_Eliminar_Accion_Repoblacion_PlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Eliminar_Poligono_Repoblacion_PlanManejo(int AsignacionId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("sp_Eliminar_Poligono_Repoblacion_PlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Insertar_CaracteristicasBiofisicas_PlanManejo(int AsignacionId, string Topografia, string Suelos, string Hidrografia, int Zona_VidaId, string Altitud)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("sp_Insertar_CaracteristicasBiofisicas_PlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@Topografia", SqlDbType.Text).Value = Topografia;
                cmd.Parameters.Add("@Suelos", SqlDbType.Text).Value = Suelos;
                cmd.Parameters.Add("@Hidrografia", SqlDbType.Text).Value = Hidrografia;
                cmd.Parameters.Add("@Zona_VidaId", SqlDbType.Int).Value = Zona_VidaId;
                cmd.Parameters.Add("@Altitud", SqlDbType.Text).Value = Altitud;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Eliminar_CaracteristicasBiofisicas_PlanManejo(int AsignacionId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("sp_Eliminar_CaracteristicasBiofisicas_PlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public DataSet Get_CaracteristicasBiofisicas_PlanManejo(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_Get_CaracteristicasBiofisicas_PlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public DataSet Get_RedCaminos_PlanManejo(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_RedCaminos_PlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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


        public void Insert_PlanInvestigacion_PlanMenjo(int AsignacionId, string Indice, string Introduccion, string Delimitacion, string Antecedentes, string Objetivos, string Alcance, string Planteamiento, string Justificacion, string Metodologia, string Recursos, string Resultados, string Cronograma, string Bibliografia)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("sp_Insert_Temp_PlanInvestigacion_PlanMenjo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@Indice", SqlDbType.Text).Value = Indice;
                cmd.Parameters.Add("@Introduccion", SqlDbType.Text).Value = Introduccion;
                cmd.Parameters.Add("@Delimitacion_Tema", SqlDbType.Text).Value = Delimitacion;
                cmd.Parameters.Add("@Antecedentes", SqlDbType.Text).Value = Antecedentes;
                cmd.Parameters.Add("@Objetivos", SqlDbType.Text).Value = Objetivos;
                cmd.Parameters.Add("@Alcances", SqlDbType.Text).Value = Alcance;
                cmd.Parameters.Add("@Planteamiento", SqlDbType.Text).Value = Planteamiento;
                cmd.Parameters.Add("@Justificacion", SqlDbType.Text).Value = Justificacion;
                cmd.Parameters.Add("@Metodologia", SqlDbType.Text).Value = Metodologia;
                cmd.Parameters.Add("@Recursos", SqlDbType.Text).Value = Recursos;
                cmd.Parameters.Add("@Resultados", SqlDbType.Text).Value = Resultados;
                cmd.Parameters.Add("@Cronograma", SqlDbType.Text).Value = Cronograma;
                cmd.Parameters.Add("@Bibliografia", SqlDbType.Text).Value = Bibliografia;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Elimina_Temp_PlanInvestigacion_PlanMenjo(int AsignacionId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("sp_Elimina_Temp_PlanInvestigacion_PlanMenjo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Eliminar_RedCaminos_PlanManejo(int AsignacionId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Eliminar_RedCaminos_PlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public DataSet Get_PlanInvestigacion_PlanMenjo(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_Get_Temp_PlanInvestigacion_PlanMenjo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public void Insertar_Plagas_PlanManejo(int AsignacionId, string Agente_Causal, string Sintomologia, string Descripcion_Dano, string FenomenoNatural, string Estimacion_Madera)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("SP_Insert_Plaga_PlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@Agente_Causal", SqlDbType.Text).Value = Agente_Causal;
                cmd.Parameters.Add("@Sintomologia", SqlDbType.Text).Value = Sintomologia;
                cmd.Parameters.Add("@Descripcion_Dano", SqlDbType.Text).Value = Descripcion_Dano;
                cmd.Parameters.Add("@Fenomeno_Natural", SqlDbType.Text).Value = FenomenoNatural;
                cmd.Parameters.Add("@Estimacion_Madera", SqlDbType.Text).Value = Estimacion_Madera;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Eliminar_Plagas_PlanManejo(int AsignacionId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("SP_Eliminar_Plaga_PlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public DataSet Get_Plaga_PlanManejo(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Plaga_PlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public void Insertar_MedidaControl_PlanManejo(int AsignacionId, string DescripionMedidas, string Tratamiento, string Manejo_Residuos)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("SP_Insert_MedidaControl_PlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@DescripcionMedidas", SqlDbType.Text).Value = DescripionMedidas;
                cmd.Parameters.Add("@Tratamiento", SqlDbType.Text).Value = Tratamiento;
                cmd.Parameters.Add("@Manejo_Residuos", SqlDbType.Text).Value = Manejo_Residuos;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Eliminar_MedidaControl_PlanManejo(int AsignacionId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("SP_Eliminar_MedidaControl_PlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public DataSet Get_MedidaControl_PlanManejo(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_MedidaControl_PlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public void Insert_ProteccionForestal(int AsignacionId, string Medida_Prevencion, string Prevencion_ControlPlagas, string Jutificacion_PrevencionIF, string LineaControlRonda, string VigilanciaPuestoControl, string ManejoCombustibles, string IdentificacionAreaCritica, string RespuestaCasoIf, string JustificacionPrevencionPF, string MonitoreoPlagaForestal, string ControlPlagaForestal, string Ampliacion_Ronda, string Ronda_Cortafuego_Intermedia, string Brigada_Incencio, string Identificacion_RutaEscape, string Proteccion_FuenteAgua, string Proteccion_Otros_Factores)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insert_ProteccionForestal", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@Medida_Prevencion", SqlDbType.Text).Value = Medida_Prevencion;
                cmd.Parameters.Add("@Prevencion_ControlPlagas", SqlDbType.Text).Value = Prevencion_ControlPlagas;
                cmd.Parameters.Add("@Justificacion_PrevencionIF", SqlDbType.Text).Value = Jutificacion_PrevencionIF;
                cmd.Parameters.Add("@Linea_Control_Ronda", SqlDbType.Text).Value = LineaControlRonda;
                cmd.Parameters.Add("@Vigilancia_Puesto_Control", SqlDbType.Text).Value = VigilanciaPuestoControl;
                cmd.Parameters.Add("@Manejo_Combustibles", SqlDbType.Text).Value = ManejoCombustibles;
                cmd.Parameters.Add("@Identificacion_Area_Critica", SqlDbType.Text).Value = IdentificacionAreaCritica;
                cmd.Parameters.Add("@Respuesta_CasoIF", SqlDbType.Text).Value = RespuestaCasoIf;
                cmd.Parameters.Add("@Justificacion_PrevencionPF", SqlDbType.Text).Value = JustificacionPrevencionPF;
                cmd.Parameters.Add("@Monitoreo_Plaga_Forestal", SqlDbType.Text).Value = MonitoreoPlagaForestal;
                cmd.Parameters.Add("@Control_Plaga_Forestal", SqlDbType.Text).Value = ControlPlagaForestal;
                cmd.Parameters.Add("@Ampliacion_Ronda", SqlDbType.Text).Value = Ampliacion_Ronda;
                cmd.Parameters.Add("@Ronda_Cortafuego_Intermedia", SqlDbType.Text).Value = Ronda_Cortafuego_Intermedia;
                cmd.Parameters.Add("@Brigada_Incencio", SqlDbType.Text).Value = Brigada_Incencio;
                cmd.Parameters.Add("@Identificacion_RutaEscape", SqlDbType.Text).Value = Identificacion_RutaEscape;
                cmd.Parameters.Add("@Proteccion_FuenteAgua", SqlDbType.Text).Value = Proteccion_FuenteAgua;
                cmd.Parameters.Add("@Proteccion_Otros_Factores", SqlDbType.Text).Value = Proteccion_Otros_Factores;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Eliminar_ProteccionForestal(int AsignacionId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Eliminar_ProteccionForestal", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public DataSet Get_ProteccionForestal(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_ProteccionForestal", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public void Insert_ActividadCronograma(int AsignacionId,string Actividad, DateTime Fec_Ini, DateTime Fec_Fin )
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insert_Actividad_PlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@Actividad", SqlDbType.VarChar,400).Value = Actividad;
                cmd.Parameters.Add("@Fec_Ini", SqlDbType.DateTime).Value = Fec_Ini;
                cmd.Parameters.Add("@Fec_Fin", SqlDbType.DateTime).Value = Fec_Fin;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public DataSet Get_Actividades_Cronograma(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Actividades_Cronograma", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public void Eliminar_Actividad_Cronograma(int AsignacionId, int ActividadId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Eliminar_Actividad_Cronograma", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@ActividadId", SqlDbType.Int).Value = ActividadId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Eliminar_Actividad_Aprovechamiento(int AsignacionId, int ActividadId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Eliminar_Actividad_Aprovechamiento", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@ActividadId", SqlDbType.Int).Value = ActividadId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public decimal Get_SumAreaIntervenir_AreaProteccion(int AsignacionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_SumAreaIntervenir_AreaProteccion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SubCategoriaId", OleDbType.Integer).Value = AsignacionId;
                cmd.Parameters.Add("@Resul", OleDbType.Decimal).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToDecimal(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public void Insert_Actividad_Aprovechamiento(int AsignacionId, int Descripcion_ActividadId, int Tipo_AprovechamientoId, string Actividad)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insert_Actividad_Aprovechamiento", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@Descripcion_ActividadId", SqlDbType.Int).Value = Descripcion_ActividadId;
                cmd.Parameters.Add("@Tipo_AprovechamientoId", SqlDbType.Int).Value = Tipo_AprovechamientoId;
                cmd.Parameters.Add("@Actividad", SqlDbType.VarChar, 200).Value = Actividad;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public DataSet Get_Actividad_Aprovechamiento(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Actividad_Aprovechamiento", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public bool Existe_Especie(string Nombre_Cientifico)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Existe_Especie", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Nombre_Cientifico", OleDbType.VarChar, 200).Value = Nombre_Cientifico ;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                if (Convert.ToInt32(cmd.Parameters["@Resul"].Value) == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                cn.Close();
                return false;
            }
        }

        public void Sp_Insert_Boleta(int AsignacionId, int Turno, int Rodal, double No, double Dap, double Altura, string Nombre_Cientifico, double Troza, int X, int Y)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insert_Boleta", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@Turno", SqlDbType.Int).Value = Turno;
                cmd.Parameters.Add("@Rodal", SqlDbType.Int).Value = Rodal;
                cmd.Parameters.Add("@No", SqlDbType.Float).Value = No;
                cmd.Parameters.Add("@Dap", SqlDbType.Float).Value = Dap;
                cmd.Parameters.Add("@Altura", SqlDbType.Float).Value = Altura;
                cmd.Parameters.Add("@Nombre_Cientifico", SqlDbType.VarChar, 200).Value = Nombre_Cientifico;
                cmd.Parameters.Add("@Troza", SqlDbType.Float).Value = Troza;
                cmd.Parameters.Add("@X", SqlDbType.Int).Value = X;
                cmd.Parameters.Add("@Y", SqlDbType.Int).Value = Y;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Elimina_Boleta(int AsignacionId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Elimina_Boleta", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Elimina_Boleta_Dos(int AsignacionId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Elimina_Boleta_Dos", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public DataSet Get_Datos_Boleta(int AsignacionId, int Tipo)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Datos_Boleta", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public DataSet Get_Resumen_Censo(int Tipo, int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Resumen_Censo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public DataSet Get_ResumenDetalle_PlanManejo(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_ResumenDetalle_PlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public DataSet Get_Silvicultura_PlanManejo(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Silvicultura_PlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public void Insert_Aprovechamiento_Forestal(int AsignacionId, int Tipo_Ingreso_Datos, int Tipo_InventarioId, string Datos_Regresion, int DiamtroMin, int TotRodal, string OtraEcuacion)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insert_Aprovechamiento_Forestal", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@Tipo_Ingreso_DatosId", SqlDbType.Int).Value = Tipo_Ingreso_Datos;
                cmd.Parameters.Add("@Tipo_InventarioId", SqlDbType.Int).Value = Tipo_InventarioId;
                cmd.Parameters.Add("@Datos_Regresion", SqlDbType.Text).Value = Datos_Regresion;
                cmd.Parameters.Add("@DiametroMin", SqlDbType.Int).Value = DiamtroMin;
                cmd.Parameters.Add("@TotRodal", SqlDbType.Int).Value = TotRodal;
                cmd.Parameters.Add("@Ecuacion", SqlDbType.VarChar,1000).Value = OtraEcuacion;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Insert_Ecuacion_PlanManejo(int AsignacionId, int EcuacionId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insert_Ecuacion_PlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@EcuacionId", SqlDbType.Int).Value = EcuacionId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Insertar_Resumen_PlanManejo(int AsignacionId, int Especieid, decimal AreaRodal, int ClaseDesarrolloId, string Edad, int TratamientoId, decimal Dap, decimal Altura, decimal Densidad, decimal AreaBasal, decimal VolTroza, decimal VolLena, decimal VolOtro, decimal VolTotal, int Rodal, int Pendiente, decimal INC, decimal VolHa, decimal VolRodal, int Extrae, decimal VolTrozaExtrae, decimal VolLenaExtrae, decimal VolOtroExtrae, decimal VolTotalExtrae, decimal AreaBasalRodal)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insertar_Resumen_PlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@EspecieId", SqlDbType.Int).Value = Especieid;
                cmd.Parameters.Add("@AreaRodal", SqlDbType.Decimal).Value = AreaRodal;
                cmd.Parameters.Add("@ClaseDesarrolloId", SqlDbType.Int).Value = ClaseDesarrolloId;
                cmd.Parameters.Add("@Edad", SqlDbType.VarChar,100).Value = Edad;
                cmd.Parameters.Add("@TratamientoId", SqlDbType.Int).Value = TratamientoId;
                cmd.Parameters.Add("@Dap", SqlDbType.Decimal).Value = Dap;
                cmd.Parameters.Add("@Altura", SqlDbType.Decimal).Value = Altura;
                cmd.Parameters.Add("@Densidad", SqlDbType.Decimal).Value = Densidad;
                cmd.Parameters.Add("@AreaBasal", SqlDbType.Decimal).Value = AreaBasal;
                cmd.Parameters.Add("@VolTroza", SqlDbType.Decimal).Value = VolTroza;
                cmd.Parameters.Add("@VolLena", SqlDbType.Decimal).Value = VolLena;
                cmd.Parameters.Add("@VolOtro", SqlDbType.Decimal).Value = VolOtro;
                cmd.Parameters.Add("@VolTotal", SqlDbType.Decimal).Value = VolTotal;
                cmd.Parameters.Add("@Rodal", SqlDbType.Int).Value = Rodal;
                cmd.Parameters.Add("@Pendiente", SqlDbType.Int).Value = Pendiente;
                cmd.Parameters.Add("@Inc", SqlDbType.Decimal).Value = INC;
                cmd.Parameters.Add("@VolHa", SqlDbType.Decimal).Value = VolHa;
                cmd.Parameters.Add("@VolRodal", SqlDbType.Decimal).Value = VolRodal;
                cmd.Parameters.Add("@Extrae", SqlDbType.Int).Value = Extrae;
                cmd.Parameters.Add("@VolTrozaExtrae", SqlDbType.Decimal).Value = VolTrozaExtrae;
                cmd.Parameters.Add("@VolLenaExtrae", SqlDbType.Decimal).Value = VolLenaExtrae;
                cmd.Parameters.Add("@VolOtroExtrae", SqlDbType.Decimal).Value = VolOtroExtrae;
                cmd.Parameters.Add("@VolTotalExtrae", SqlDbType.Decimal).Value = VolTotalExtrae;
                cmd.Parameters.Add("@AreaBasalRodal", SqlDbType.Decimal).Value = AreaBasalRodal;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Elimniar_Resumen(int AsignacionId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Elimniar_Resumen", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Eliminiar_Aprovechamiento_Forestal(int AsignacionId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Eliminiar_Aprovechamiento_Forestal", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Eliminiar_Ecuaciones_Manejo(int AsignacionId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Eliminiar_Ecuaciones_Manejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public string Get_ClaseDesarrollo(int Clase_Desarrollo)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_ClaseDesarrollo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Clase_DesarrolloId", OleDbType.Integer).Value = Clase_Desarrollo;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 500).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "";
            }
        }

        public string Get_TratamientoSilvicultural(int TratamientoId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_TratamientoSilvicultural", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@TratamientoId", OleDbType.Integer).Value = TratamientoId;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 500).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "";
            }
        }

        public DataSet Get_Aprovechamiento_Forestal(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Aprovechamiento_Forestal", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public DataSet Get_Ecuacion_Aprovechamiento_Forestal(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Ecuacion_Aprovechamiento_Forestal", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public int Finca_EnGestion_Manejo_Temporal(int InmuebleId, int AsignacionId = 0)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Finca_EnGestion_Manejo_Temporal", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 500).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public DataSet Get_Turno_PlanManejo(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Turno", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                adp.Fill(ds, "DATOS");
                cn.Close();
                return ds;

            }
            catch (Exception ex)
            {
                cn.Close();
                DataSet ds = new DataSet();
                return ds;
            }
        }

        public DataSet Get_Rodal_PlanManejo(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Rodal", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                adp.Fill(ds, "DATOS");
                cn.Close();
                return ds;

            }
            catch (Exception ex)
            {
                cn.Close();
                DataSet ds = new DataSet();
                return ds;
            }
        }

        public void Sp_Insert_Prod_NoMaderable_PlanManejo(int AsignacionId, XmlDocument Productos)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insert_Prod_NoMaderable_PlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@Productos", SqlDbType.Xml).Value = Productos.OuterXml.ToString();
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Insert_Especies_Repoblacion(int AsignacionId, XmlDocument Especies)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insert_Especies_Repoblacion", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@Especies", SqlDbType.Xml).Value = Especies.OuterXml.ToString();
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public DataSet LeerXml_Prod_NoMaderables(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("LeerXml_Prod_NoMaderables", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public DataSet Sp_Get_Arboles_Extraer(int AsignacionId, int Rodal, int EspecieId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Arboles_Extraer", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
                cmd.Parameters.Add("@Rodal", OleDbType.Integer).Value = Rodal;
                cmd.Parameters.Add("@EspecieId", OleDbType.Integer).Value = EspecieId;
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

        public void Update_Extrae_Arbol(int AsignacionId, int Rodal, int EspecieId, int NoArbol, int Extrae)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Update_Extrae_Arbol", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@Rodal", SqlDbType.Int).Value = Rodal;
                cmd.Parameters.Add("@EspecieId", SqlDbType.Int).Value = EspecieId;
                cmd.Parameters.Add("@ArbolNo", SqlDbType.Int).Value = NoArbol;
                cmd.Parameters.Add("@Extrae", SqlDbType.Int).Value = Extrae;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Sp_Insert_Prod_NoMaderable_Extrae_PlanManejo(int AsignacionId, XmlDocument Productos)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insert_Prod_NoMaderable_Extrae_PlanManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@Productos", SqlDbType.Xml).Value = Productos.OuterXml.ToString();
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public DataSet LeerXml_Prod_NoMaderables_Extrae(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("LeerXml_Prod_NoMaderables_Extrae", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public void Insert_Silvicultura_MaderableExtrae(int AsignacionId, int Correlativo, int Turno, decimal VolTroza, decimal VolLena, decimal VolTotal, int TratamientoId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insert_Silvicultura_MaderableExtrae", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@Correlativo", SqlDbType.Int).Value = Correlativo;
                cmd.Parameters.Add("@Turno", SqlDbType.Int).Value = Turno;
                cmd.Parameters.Add("@TratamientoId", SqlDbType.Int).Value = TratamientoId;
                cmd.Parameters.Add("@VolTroza", SqlDbType.Decimal).Value = VolTroza;
                cmd.Parameters.Add("@VolLena", SqlDbType.Decimal).Value = VolLena;
                cmd.Parameters.Add("@VolTotal", SqlDbType.Decimal).Value = VolTotal;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Delete_Silvicultura_MaderableExtrae(int AsignacionId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Delete_Silvicultura_MaderableExtrae", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public DataSet Get_Dato_Silvicultura_Extrae(int AsignacionId, int Correlativo)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Dato_Silvicultura_Extrae", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
                cmd.Parameters.Add("@Correlativo", OleDbType.Integer).Value = Correlativo;
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

        public void Insert_OtrosDatos_Aprovechamiento(int AsignacionId, int FormulaId, decimal Cap, String Justificacion, string ActividadApro, string ObjetivoRecupera, string JustificacionEspecie, string SistemaRepoblacion)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insert_OtrosDatos_Aprovechamiento", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@FormulaId", SqlDbType.Int).Value = FormulaId;
                cmd.Parameters.Add("@Cap", SqlDbType.Decimal).Value = Cap;
                cmd.Parameters.Add("@Justificacion", SqlDbType.VarChar, 8000).Value = Justificacion;
                cmd.Parameters.Add("@ActividadApro", SqlDbType.VarChar, 8000).Value = ActividadApro;
                cmd.Parameters.Add("@ObjetivoRecuperacion", SqlDbType.VarChar, 8000).Value = ObjetivoRecupera;
                cmd.Parameters.Add("@JustificacionEspecie", SqlDbType.VarChar, 8000).Value = JustificacionEspecie;
                cmd.Parameters.Add("@SistemaRepo", SqlDbType.VarChar, 8000).Value = SistemaRepoblacion;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public DataSet Get_Datos_PlanificacionManejo(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Datos_PlanificacionManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public DataSet LeerXml_Especie_Repoblacion(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("LeerXml_Especies_Repoblacion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public void Actualizo_DatosCambioUso(int AsignacionId, int CambioUsoId, string Especifique)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Actualizo_DatosCambioUso", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@CambioUsoId", SqlDbType.Int).Value = CambioUsoId;
                cmd.Parameters.Add("@Especifique", SqlDbType.VarChar,200).Value = Especifique;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public DataSet Get_DatosCambioUso(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_DatosCambioUso", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public double Get_Area_PlanManejo(int AsignacionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Area_PlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
                cmd.Parameters.Add("@Resul", OleDbType.Double).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToDouble(cmd.Parameters["@Resul"].Value);
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        #region ValidacionesPlanManejo

        public int TienePropietarioNomEmp_FincaManejo(int AsignacionId, int InmuebleId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_TienePropietarioNomEmp_FincaManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public int TieneAras_FincaManejo(int AsignacionId, int InmuebleId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_TieneAras_FincaManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public int Existe_DatosNotifica(int AsignacionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Existe_DatosNotifica", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public int Existe_InfoGeneral_PlanManejo(int AsignacionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Existe_InfoGeneral_PlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }


        public int Existe_InfoGeneral_SistemaRepoblacion(int AsignacionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Existe_InfoGeneral_SistemaRepoblacion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }


        public int Existe_InfoGeneral_Poligono_Repoblacion(int AsignacionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Existe_InfoGeneral_Poligono_Repoblacion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }


        public int Existe_Caracbiofisicas_PlanManejo(int AsignacionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Existe_Caracbiofisicas_PlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public int Existe_Aprovechamiento_PlanMenejo(int AsignacionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Existe_Aprovechamiento_PlanMenejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public int Existe_Ecuacion_PlanManejo(int AsignacionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Existe_Ecuacion_PlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public int Existe_Resumen_PlanManejo(int AsignacionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Existe_Resumen_PlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public int Existe_Productos_NoMaderables(int AsignacionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Existe_Productos_NoMaderables", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public int Existe_Silvicultura_PlanManejo(int AsignacionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Existe_Silvicultura_PlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public int Existe_Productos_NoMaderablesExtraer(int AsignacionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Existe_Productos_NoMaderablesExtraer", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public int Existe_PlanificacionManejo_PlanManejo(int AsignacionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_PlanificacionManejo_PlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public int Existe_Sistema_Repoblacion_Especie(int AsignacionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Existe_Sistema_Repoblacion_Especie", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public int Existe_ProteccionForestal_PlanManejo(int AsignacionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Existe_ProteccionForestal_PlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public int Existe_Cronograma_PlanManejo(int AsignacionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Existe_Cronograma_PlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }


        public int Get_Tipo_Propietario_Finca_Manejo(int AsignacionId, int InmuebleId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Tipo_Propietario_Finca_Manejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                cmd.Parameters.Add("@TipoPersonaId", OleDbType.VarChar, 500).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@TipoPersonaId"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public DataSet Get_propietarios_Temp_Finca_Manejo(int AsignacionId, int InmuebleId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_propietarios_Temp_Finca_Manejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public DataSet Get_Otras_Temp_Finca_Manejo(int AsignacionId, int InmuebleId)
        {
            try
            {
                if (ds.Tables["DATOS2"] != null)
                    ds.Tables.Remove("DATOS2");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Otras_Temp_Finca_Manejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                adp.Fill(ds, "DATOS2");
                cn.Close();
                return ds;

            }
            catch (Exception ex)
            {
                cn.Close();
                return ds;
            }
        }

        public int Existe_Propietarios_OtroInmueble_Manejo(int AsignacionId, int InmuebleId, int PersonaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Existe_Propietarios_OtroInmueble_Manejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = PersonaId;
                cmd.Parameters.Add("@Existe", OleDbType.VarChar, 500).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Existe"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public DataSet Get_Fincas_Completas_Manejo(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS2"] != null)
                    ds.Tables.Remove("DATOS2");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Fincas_Completas_Manejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                adp.Fill(ds, "DATOS2");
                cn.Close();
                return ds;

            }
            catch (Exception ex)
            {
                cn.Close();
                return ds;
            }
        }

        public int Existe_Representatnes_PlanManejo(int AsignacionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Existe_Representatnes_PlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        #endregion

        public int Get_PersonaId_AsignacionId(int AsignacionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_PersonaId_AsignacionId", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public void Traslada_TempManejo(int GestionId, int AsignacionId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Traslada_TempManejo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

       public void Traslada_PlanManejo_ConvierteXml(int GestionId, int AsignacionId)
        {
            try
            {
                ClManejoImpresion = new Cl_Manejo_Impresion();
                ClXml = new Cl_Xml();
                
                DataSet DsInmuebles = ClManejoImpresion.Get_Inmuebles_PlanManejo(1, AsignacionId);
                for (int i = 0; i < DsInmuebles.Tables["Datos"].Rows.Count; i++)
                {
                    //Propietarios de Inmuebles
                    DataSet DsPropietarios = GetPropietarios_Inmuebles_Manejo(AsignacionId, Convert.ToInt32(DsInmuebles.Tables["Datos"].Rows[i]["InmuebleId"]));
                    XmlDocument iInformacion = ClXml.CrearDocumentoXML("Propietarios");
                    XmlNode iElementos = iInformacion.CreateElement("Propietarios");
                    for (int j = 0; j < DsPropietarios.Tables["Datos"].Rows.Count; j++)
                    {
                        XmlNode iElementoDetalle = iInformacion.CreateElement("Item");
                        ClXml.AgregarAtributo("PersonaId", DsPropietarios.Tables["Datos"].Rows[j]["PersonaId"], iElementoDetalle);
                        iElementos.AppendChild(iElementoDetalle);
                    }
                    iInformacion.ChildNodes[1].AppendChild(iElementos);
                    Insert_Propietarios_PlanManejo_Real(GestionId, Convert.ToInt32(DsInmuebles.Tables["Datos"].Rows[i]["InmuebleId"]), iInformacion);
                    DsPropietarios.Clear();

                    //Clase de Desarrollo de Fincas
                    XmlDocument iInformacionCDesarrollo = ClXml.CrearDocumentoXML("ClaseDesarrollo");
                    XmlNode iElementosCDesarrollo = iInformacionCDesarrollo.CreateElement("ClaseDesarrollo");
                    DataSet DsClaseDerrollo = GetClaseDesarrolloFinca_PlanManejo(AsignacionId, Convert.ToInt32(DsInmuebles.Tables["Datos"].Rows[i]["InmuebleId"]));
                    for (int k = 0; k < DsClaseDerrollo.Tables["Datos"].Rows.Count; k++)
                    {
                        XmlNode iElementosCDesarrolloDetalle = iInformacionCDesarrollo.CreateElement("Item");
                        ClXml.AgregarAtributo("Clase_DesarrolloId", DsClaseDerrollo.Tables["Datos"].Rows[k]["Clase_DesarrolloId"].ToString().Trim(), iElementosCDesarrolloDetalle);
                        iElementosCDesarrollo.AppendChild(iElementosCDesarrolloDetalle);
                    }
                    iInformacionCDesarrollo.ChildNodes[1].AppendChild(iElementosCDesarrollo);
                    Insert_Clase_Desarrollo_Finca_PlanManejo_Real(GestionId, Convert.ToInt32(DsClaseDerrollo.Tables["Datos"].Rows[i]["InmuebleId"]), iInformacionCDesarrollo);
                    DsClaseDerrollo.Clear();
                }
                DsInmuebles.Clear();

                //Representantes
                XmlDocument iInformacionRepresentantes = ClXml.CrearDocumentoXML("Representantes");
                XmlNode iElementosRepresentantes = iInformacionRepresentantes.CreateElement("Representantes");
                DataSet DsRepresentantes = GetRepresentantes_Manejo(AsignacionId);
                for (int k = 0; k < DsRepresentantes.Tables["Datos"].Rows.Count; k++)
                {
                    XmlNode iElementosRepresentantesDetalle = iInformacionRepresentantes.CreateElement("Item");
                    ClXml.AgregarAtributo("PersonaId", DsRepresentantes.Tables["Datos"].Rows[k]["PersonaId"].ToString().Trim(), iElementosRepresentantesDetalle);
                    iElementosRepresentantes.AppendChild(iElementosRepresentantesDetalle);
                }
                iInformacionRepresentantes.ChildNodes[1].AppendChild(iElementosRepresentantes);
                Insert_Representantes_PlanManejo_Real(GestionId, iInformacionRepresentantes);
                DsRepresentantes.Clear();


                //Ecuaciones
                XmlDocument iInformacionEcuaciones = ClXml.CrearDocumentoXML("Ecuaciones");
                XmlNode iElementosEcuaciones = iInformacionEcuaciones.CreateElement("Ecuaciones");
                DataSet DsEcuaciones = Get_Ecuacion_Aprovechamiento_Forestal(AsignacionId);
                for (int k = 0; k < DsEcuaciones.Tables["Datos"].Rows.Count; k++)
                {
                    XmlNode iElementosEcuacionesDetalle = iInformacionEcuaciones.CreateElement("Item");
                    ClXml.AgregarAtributo("EcuacionId", DsEcuaciones.Tables["Datos"].Rows[k]["EcuacionId"].ToString().Trim(), iElementosEcuacionesDetalle);
                    iElementosEcuaciones.AppendChild(iElementosEcuacionesDetalle);
                }
                iInformacionEcuaciones.ChildNodes[1].AppendChild(iElementosEcuaciones);
                Insert_Ecuaciones_PlanManejo_Real(GestionId, iInformacionEcuaciones);
                DsEcuaciones.Clear();

                //Sistema_Repoblacion
                XmlDocument iInformacionSistemaRepoblacion = ClXml.CrearDocumentoXML("SistemaRepoblacion");
                XmlNode iElementosSistemaRepoblacion = iInformacionSistemaRepoblacion.CreateElement("SistemaRepoblacion");
                DataSet DsSistemaRepoblacion = Get_SistemaRepoblacionPlanManejo(AsignacionId);
                for (int k = 0; k < DsSistemaRepoblacion.Tables["Datos"].Rows.Count; k++)
                {
                    XmlNode iElementosSistemaRepoblacionDetalle = iInformacionSistemaRepoblacion.CreateElement("Item");
                    ClXml.AgregarAtributo("EcuacionId", DsSistemaRepoblacion.Tables["Datos"].Rows[k]["SistemaRepoblacioId"].ToString().Trim(), iElementosSistemaRepoblacionDetalle);
                    iElementosSistemaRepoblacion.AppendChild(iElementosSistemaRepoblacionDetalle);
                }
                iInformacionSistemaRepoblacion.ChildNodes[1].AppendChild(iElementosSistemaRepoblacion);
                Insert_SistemaRepoblacion_PlanManejo_Real(GestionId, iInformacionSistemaRepoblacion);
                DsSistemaRepoblacion.Clear();

                //Censo
                XmlDocument iInformacionCenso = ClXml.CrearDocumentoXML("Censo");
                XmlNode iElementosCenso = iInformacionCenso.CreateElement("Censo");
                DataSet DsCenso = SP_Get_Censo_PlanManejo(AsignacionId);
                for (int k = 0; k < DsCenso.Tables["Datos"].Rows.Count; k++)
                {
                    XmlNode iElementosCensoDetalle = iInformacionCenso.CreateElement("Item");
                    ClXml.AgregarAtributo("Turno", DsCenso.Tables["Datos"].Rows[k]["Turno"].ToString().Trim(), iElementosCensoDetalle);
                    iElementosCenso.AppendChild(iElementosCensoDetalle);

                    ClXml.AgregarAtributo("Rodal", DsCenso.Tables["Datos"].Rows[k]["Rodal"].ToString().Trim(), iElementosCensoDetalle);
                    iElementosCenso.AppendChild(iElementosCensoDetalle);

                    ClXml.AgregarAtributo("No", DsCenso.Tables["Datos"].Rows[k]["No"].ToString().Trim(), iElementosCensoDetalle);
                    iElementosCenso.AppendChild(iElementosCensoDetalle);

                    ClXml.AgregarAtributo("Dap", DsCenso.Tables["Datos"].Rows[k]["Dap"].ToString().Trim(), iElementosCensoDetalle);
                    iElementosCenso.AppendChild(iElementosCensoDetalle);

                    ClXml.AgregarAtributo("Altura", DsCenso.Tables["Datos"].Rows[k]["Altura"].ToString().Trim(), iElementosCensoDetalle);
                    iElementosCenso.AppendChild(iElementosCensoDetalle);

                    ClXml.AgregarAtributo("EspecieId", DsCenso.Tables["Datos"].Rows[k]["EspecieId"].ToString().Trim(), iElementosCensoDetalle);
                    iElementosCenso.AppendChild(iElementosCensoDetalle);

                    ClXml.AgregarAtributo("Troza", DsCenso.Tables["Datos"].Rows[k]["Troza"].ToString().Trim(), iElementosCensoDetalle);
                    iElementosCenso.AppendChild(iElementosCensoDetalle);

                    ClXml.AgregarAtributo("X", DsCenso.Tables["Datos"].Rows[k]["X"].ToString().Trim(), iElementosCensoDetalle);
                    iElementosCenso.AppendChild(iElementosCensoDetalle);

                    ClXml.AgregarAtributo("Y", DsCenso.Tables["Datos"].Rows[k]["Y"].ToString().Trim(), iElementosCensoDetalle);
                    iElementosCenso.AppendChild(iElementosCensoDetalle);

                    ClXml.AgregarAtributo("Volumen", DsCenso.Tables["Datos"].Rows[k]["Volumen"].ToString().Trim(), iElementosCensoDetalle);
                    iElementosCenso.AppendChild(iElementosCensoDetalle);

                    ClXml.AgregarAtributo("Extrae", DsCenso.Tables["Datos"].Rows[k]["Extrae"].ToString().Trim(), iElementosCensoDetalle);
                    iElementosCenso.AppendChild(iElementosCensoDetalle);
                }
                iInformacionCenso.ChildNodes[1].AppendChild(iElementosCenso);
                Insert_Censo_PlanManejo_Real(GestionId, iInformacionCenso);
                DsCenso.Clear();


                //Resumen Plan Manejo
                XmlDocument iInformacionResumenPlanManejo = ClXml.CrearDocumentoXML("Resumen");
                XmlNode iElementosResumenPlanManejo = iInformacionResumenPlanManejo.CreateElement("Resumen");
                DataSet DsResumenPlanManejo = Get_ResumenDetalle_PlanManejo(AsignacionId);
                for (int k = 0; k < DsResumenPlanManejo.Tables["Datos"].Rows.Count; k++)
                {
                    XmlNode iElementosResumenPlanManejoDetalle = iInformacionResumenPlanManejo.CreateElement("Item");
                    ClXml.AgregarAtributo("Correlativo", DsResumenPlanManejo.Tables["Datos"].Rows[k]["Correlativo"].ToString().Trim(), iElementosResumenPlanManejoDetalle);
                    iElementosResumenPlanManejo.AppendChild(iElementosResumenPlanManejoDetalle);

                    ClXml.AgregarAtributo("EspecieId", DsResumenPlanManejo.Tables["Datos"].Rows[k]["EspecieId"].ToString().Trim(), iElementosResumenPlanManejoDetalle);
                    iElementosResumenPlanManejo.AppendChild(iElementosResumenPlanManejoDetalle);

                    ClXml.AgregarAtributo("AreaRodal", DsResumenPlanManejo.Tables["Datos"].Rows[k]["AreaRodal"].ToString().Trim(), iElementosResumenPlanManejoDetalle);
                    iElementosResumenPlanManejo.AppendChild(iElementosResumenPlanManejoDetalle);

                    ClXml.AgregarAtributo("Clase_DesarrolloId", DsResumenPlanManejo.Tables["Datos"].Rows[k]["Clase_DesarrolloId"].ToString().Trim(), iElementosResumenPlanManejoDetalle);
                    iElementosResumenPlanManejo.AppendChild(iElementosResumenPlanManejoDetalle);

                    ClXml.AgregarAtributo("Edad", DsResumenPlanManejo.Tables["Datos"].Rows[k]["Edad"].ToString().Trim(), iElementosResumenPlanManejoDetalle);
                    iElementosResumenPlanManejo.AppendChild(iElementosResumenPlanManejoDetalle);

                    ClXml.AgregarAtributo("Tratamiento_Id", DsResumenPlanManejo.Tables["Datos"].Rows[k]["Tratamiento_Id"].ToString().Trim(), iElementosResumenPlanManejoDetalle);
                    iElementosResumenPlanManejo.AppendChild(iElementosResumenPlanManejoDetalle);

                    ClXml.AgregarAtributo("Dap", DsResumenPlanManejo.Tables["Datos"].Rows[k]["DAp"].ToString().Trim(), iElementosResumenPlanManejoDetalle);
                    iElementosResumenPlanManejo.AppendChild(iElementosResumenPlanManejoDetalle);

                    ClXml.AgregarAtributo("Altura", DsResumenPlanManejo.Tables["Datos"].Rows[k]["Altura"].ToString().Trim(), iElementosResumenPlanManejoDetalle);
                    iElementosResumenPlanManejo.AppendChild(iElementosResumenPlanManejoDetalle);

                    ClXml.AgregarAtributo("Densidad", DsResumenPlanManejo.Tables["Datos"].Rows[k]["Densidad"].ToString().Trim(), iElementosResumenPlanManejoDetalle);
                    iElementosResumenPlanManejo.AppendChild(iElementosResumenPlanManejoDetalle);

                    ClXml.AgregarAtributo("AreaBasal", DsResumenPlanManejo.Tables["Datos"].Rows[k]["AreaBasal"].ToString().Trim(), iElementosResumenPlanManejoDetalle);
                    iElementosResumenPlanManejo.AppendChild(iElementosResumenPlanManejoDetalle);

                    ClXml.AgregarAtributo("VolTroza", DsResumenPlanManejo.Tables["Datos"].Rows[k]["VolTroza"].ToString().Trim(), iElementosResumenPlanManejoDetalle);
                    iElementosResumenPlanManejo.AppendChild(iElementosResumenPlanManejoDetalle);

                    ClXml.AgregarAtributo("VolLena", DsResumenPlanManejo.Tables["Datos"].Rows[k]["VolLena"].ToString().Trim(), iElementosResumenPlanManejoDetalle);
                    iElementosResumenPlanManejo.AppendChild(iElementosResumenPlanManejoDetalle);

                    ClXml.AgregarAtributo("VolOtro", DsResumenPlanManejo.Tables["Datos"].Rows[k]["VolOtro"].ToString().Trim(), iElementosResumenPlanManejoDetalle);
                    iElementosResumenPlanManejo.AppendChild(iElementosResumenPlanManejoDetalle);

                    ClXml.AgregarAtributo("Rodal", DsResumenPlanManejo.Tables["Datos"].Rows[k]["Rodal"].ToString().Trim(), iElementosResumenPlanManejoDetalle);
                    iElementosResumenPlanManejo.AppendChild(iElementosResumenPlanManejoDetalle);

                    ClXml.AgregarAtributo("Pendiente", DsResumenPlanManejo.Tables["Datos"].Rows[k]["Pendiente"].ToString().Trim(), iElementosResumenPlanManejoDetalle);
                    iElementosResumenPlanManejo.AppendChild(iElementosResumenPlanManejoDetalle);

                    ClXml.AgregarAtributo("INC", DsResumenPlanManejo.Tables["Datos"].Rows[k]["INC"].ToString().Trim(), iElementosResumenPlanManejoDetalle);
                    iElementosResumenPlanManejo.AppendChild(iElementosResumenPlanManejoDetalle);

                    ClXml.AgregarAtributo("VolHa", DsResumenPlanManejo.Tables["Datos"].Rows[k]["VolHa"].ToString().Trim(), iElementosResumenPlanManejoDetalle);
                    iElementosResumenPlanManejo.AppendChild(iElementosResumenPlanManejoDetalle);

                    ClXml.AgregarAtributo("VolRodal", DsResumenPlanManejo.Tables["Datos"].Rows[k]["VolRodal"].ToString().Trim(), iElementosResumenPlanManejoDetalle);
                    iElementosResumenPlanManejo.AppendChild(iElementosResumenPlanManejoDetalle);

                    ClXml.AgregarAtributo("Extrae", DsResumenPlanManejo.Tables["Datos"].Rows[k]["Extrae"].ToString().Trim(), iElementosResumenPlanManejoDetalle);
                    iElementosResumenPlanManejo.AppendChild(iElementosResumenPlanManejoDetalle);

                    ClXml.AgregarAtributo("VolTrozaExtrae", DsResumenPlanManejo.Tables["Datos"].Rows[k]["VolTrozaExtrae"].ToString().Trim(), iElementosResumenPlanManejoDetalle);
                    iElementosResumenPlanManejo.AppendChild(iElementosResumenPlanManejoDetalle);

                    ClXml.AgregarAtributo("VolLenaExtrae", DsResumenPlanManejo.Tables["Datos"].Rows[k]["VolLenaExtrae"].ToString().Trim(), iElementosResumenPlanManejoDetalle);
                    iElementosResumenPlanManejo.AppendChild(iElementosResumenPlanManejoDetalle);

                    ClXml.AgregarAtributo("VolOtroExtrae", DsResumenPlanManejo.Tables["Datos"].Rows[k]["VolOtroExtrae"].ToString().Trim(), iElementosResumenPlanManejoDetalle);
                    iElementosResumenPlanManejo.AppendChild(iElementosResumenPlanManejoDetalle);

                    ClXml.AgregarAtributo("VolTotalExtrae", DsResumenPlanManejo.Tables["Datos"].Rows[k]["VolTotalExtrae"].ToString().Trim(), iElementosResumenPlanManejoDetalle);
                    iElementosResumenPlanManejo.AppendChild(iElementosResumenPlanManejoDetalle);

                    ClXml.AgregarAtributo("AreaBasalRodal", DsResumenPlanManejo.Tables["Datos"].Rows[k]["AreaBasalRodal"].ToString().Trim(), iElementosResumenPlanManejoDetalle);
                    iElementosResumenPlanManejo.AppendChild(iElementosResumenPlanManejoDetalle);
                }
                iInformacionResumenPlanManejo.ChildNodes[1].AppendChild(iElementosResumenPlanManejo);
                Insert_Resumen_PlanManejo_Real(GestionId, iInformacionResumenPlanManejo);
                DsResumenPlanManejo.Clear();



                //Silvicultura
                XmlDocument iInformacionSilvicultura = ClXml.CrearDocumentoXML("Silvicultura");
                XmlNode iElementosSilvicultura = iInformacionSilvicultura.CreateElement("Silvicultura");
                DataSet DsSilvicultura = Get_Silvicultura_PlanManejo(AsignacionId);
                for (int k = 0; k < DsResumenPlanManejo.Tables["Datos"].Rows.Count; k++)
                {
                    XmlNode iElementosSilviculturaDetalle = iInformacionSilvicultura.CreateElement("Item");
                    ClXml.AgregarAtributo("Correlativo", DsSilvicultura.Tables["Datos"].Rows[k]["Correlativo"].ToString().Trim(), iElementosSilviculturaDetalle);
                    iElementosSilvicultura.AppendChild(iElementosSilviculturaDetalle);

                    ClXml.AgregarAtributo("Turno", DsSilvicultura.Tables["Datos"].Rows[k]["Turno"].ToString().Trim(), iElementosSilviculturaDetalle);
                    iElementosSilvicultura.AppendChild(iElementosSilviculturaDetalle);

                    ClXml.AgregarAtributo("VolTroza", DsSilvicultura.Tables["Datos"].Rows[k]["VolTroza"].ToString().Trim(), iElementosSilviculturaDetalle);
                    iElementosSilvicultura.AppendChild(iElementosSilviculturaDetalle);

                    ClXml.AgregarAtributo("VolLena", DsSilvicultura.Tables["Datos"].Rows[k]["VolLena"].ToString().Trim(), iElementosSilviculturaDetalle);
                    iElementosSilvicultura.AppendChild(iElementosSilviculturaDetalle);

                    ClXml.AgregarAtributo("VolTotal", DsSilvicultura.Tables["Datos"].Rows[k]["VolTotal"].ToString().Trim(), iElementosSilviculturaDetalle);
                    iElementosSilvicultura.AppendChild(iElementosSilviculturaDetalle);

                    ClXml.AgregarAtributo("Tratamiento_Id", DsSilvicultura.Tables["Datos"].Rows[k]["Tratamiento_Id"].ToString().Trim(), iElementosSilviculturaDetalle);
                    iElementosSilvicultura.AppendChild(iElementosSilviculturaDetalle);
                }
                iInformacionSilvicultura.ChildNodes[1].AppendChild(iElementosSilvicultura);
                Insert_Silvicultura_PlanManejo_Real(GestionId, iInformacionSilvicultura);
                DsSilvicultura.Clear();

                //Cronograma
                XmlDocument iInformacionCronograma = ClXml.CrearDocumentoXML("Cronograma");
                XmlNode iElementosCronograma = iInformacionCronograma.CreateElement("Cronograma");
                DataSet DsCronograma = Get_Actividades_Cronograma(AsignacionId);
                for (int k = 0; k < DsResumenPlanManejo.Tables["Datos"].Rows.Count; k++)
                {
                    XmlNode iElementosCronogramaDetalle = iInformacionCronograma.CreateElement("Item");
                    ClXml.AgregarAtributo("ActividadId", DsCronograma.Tables["Datos"].Rows[k]["ActividadId"].ToString().Trim(), iElementosCronogramaDetalle);
                    iElementosCronograma.AppendChild(iElementosCronogramaDetalle);

                    ClXml.AgregarAtributo("Actividad", DsCronograma.Tables["Datos"].Rows[k]["Actividad"].ToString().Trim(), iElementosCronogramaDetalle);
                    iElementosCronograma.AppendChild(iElementosCronogramaDetalle);

                    ClXml.AgregarAtributo("Fec_Ini", DsCronograma.Tables["Datos"].Rows[k]["Fec_Ini"].ToString().Trim(), iElementosCronogramaDetalle);
                    iElementosCronograma.AppendChild(iElementosCronogramaDetalle);

                    ClXml.AgregarAtributo("Fec_Fin", DsCronograma.Tables["Datos"].Rows[k]["Fec_Fin"].ToString().Trim(), iElementosCronogramaDetalle);
                    iElementosCronograma.AppendChild(iElementosCronogramaDetalle);

                }
                iInformacionCronograma.ChildNodes[1].AppendChild(iElementosCronograma);
                Insert_Cronograma_PlanManejo_Real(GestionId, iInformacionCronograma);
                DsCronograma.Clear();
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }

       public void Elimina_TempPlanManejo(int AsignacionId)
       {
           try
           {
               cnSql.Open();
               SqlCommand cmd = new SqlCommand("SP_Elimina_TempPlanManejo", cnSql);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
               cmd.ExecuteNonQuery();
               cnSql.Close();
           }
           catch (Exception ex)
           {
               cnSql.Close();
           }
       }

       public void Insert_Cronograma_PlanManejo_Real(int GestionId, XmlDocument Actividades)
       {
           try
           {
               cnSql.Open();
               SqlCommand cmd = new SqlCommand("SP_Insert_Cronograma_PlanManejo_Real", cnSql);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
               cmd.Parameters.Add("@Actividades", SqlDbType.Xml).Value = Actividades.OuterXml.ToString();
               cmd.ExecuteNonQuery();
               cnSql.Close();
           }
           catch (Exception ex)
           {
               cnSql.Close();
           }
       }

       public void Insert_Silvicultura_PlanManejo_Real(int GestionId, XmlDocument Silvicultura)
       {
           try
           {
               cnSql.Open();
               SqlCommand cmd = new SqlCommand("SP_Insert_Silvicultura_PlanManejo_Real", cnSql);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
               cmd.Parameters.Add("@Silvicultura", SqlDbType.Xml).Value = Silvicultura.OuterXml.ToString();
               cmd.ExecuteNonQuery();
               cnSql.Close();
           }
           catch (Exception ex)
           {
               cnSql.Close();
           }
       }

       public void Insert_Resumen_PlanManejo_Real(int GestionId, XmlDocument Especies)
       {
           try
           {
               cnSql.Open();
               SqlCommand cmd = new SqlCommand("SP_Insert_Resumen_PlanManejo_Real", cnSql);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
               cmd.Parameters.Add("@Especies", SqlDbType.Xml).Value = Especies.OuterXml.ToString();
               cmd.ExecuteNonQuery();
               cnSql.Close();
           }
           catch (Exception ex)
           {
               cnSql.Close();
           }
       }

       public void Insert_Censo_PlanManejo_Real(int GestionId, XmlDocument Especies)
       {
           try
           {
               cnSql.Open();
               SqlCommand cmd = new SqlCommand("SP_Insert_Censo_PlanManejo_Real", cnSql);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
               cmd.Parameters.Add("@Especies", SqlDbType.Xml).Value = Especies.OuterXml.ToString();
               cmd.ExecuteNonQuery();
               cnSql.Close();
           }
           catch (Exception ex)
           {
               cnSql.Close();
           }
       }

       public void Insert_SistemaRepoblacion_PlanManejo_Real(int GestionId, XmlDocument SistemaRepoblacion)
       {
           try
           {
               cnSql.Open();
               SqlCommand cmd = new SqlCommand("SP_Insert_SistemaRepoblacion_PlanManejo_Real", cnSql);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
               cmd.Parameters.Add("@SistemaRepoblacion", SqlDbType.Xml).Value = SistemaRepoblacion.OuterXml.ToString();
               cmd.ExecuteNonQuery();
               cnSql.Close();
           }
           catch (Exception ex)
           {
               cnSql.Close();
           }
       }

       public void Insert_Ecuaciones_PlanManejo_Real(int GestionId, XmlDocument Ecuaciones)
       {
           try
           {
               cnSql.Open();
               SqlCommand cmd = new SqlCommand("SP_Insert_Ecuaciones_PlanManejo_Real", cnSql);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
               cmd.Parameters.Add("@Ecuaciones", SqlDbType.Xml).Value = Ecuaciones.OuterXml.ToString();
               cmd.ExecuteNonQuery();
               cnSql.Close();
           }
           catch (Exception ex)
           {
               cnSql.Close();
           }
       }

       public void Insert_Representantes_PlanManejo_Real(int GestionId,  XmlDocument Representantes)
       {
           try
           {
               cnSql.Open();
               SqlCommand cmd = new SqlCommand("SP_Insert_Representantes_PlanManejo_Real", cnSql);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
               cmd.Parameters.Add("@Representantes", SqlDbType.Xml).Value = Representantes.OuterXml.ToString();
               cmd.ExecuteNonQuery();
               cnSql.Close();
           }
           catch (Exception ex)
           {
               cnSql.Close();
           }
       }

       public void Insert_Clase_Desarrollo_Finca_PlanManejo_Real(int GestionId, int InmuebleId, XmlDocument ClaseDesarrollo)
       {
           try
           {
               cnSql.Open();
               SqlCommand cmd = new SqlCommand("SP_Insert_Clase_Desarrollo_Finca_PlanManejo_Real", cnSql);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
               cmd.Parameters.Add("@InmuebleId", SqlDbType.Int).Value = InmuebleId;
               cmd.Parameters.Add("@ClaseDesarrollo", SqlDbType.Xml).Value = ClaseDesarrollo.OuterXml.ToString();
               cmd.ExecuteNonQuery();
               cnSql.Close();
           }
           catch (Exception ex)
           {
               cnSql.Close();
           }
       }

       public void Insert_Propietarios_PlanManejo_Real(int GestionId, int InmuebleId, XmlDocument Propietarios)
       {
           try
           {
               cnSql.Open();
               SqlCommand cmd = new SqlCommand("SP_Insert_Propietarios_PlanManejo_Real", cnSql);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
               cmd.Parameters.Add("@InmuebleId", SqlDbType.Int).Value = InmuebleId;
               cmd.Parameters.Add("@Propietarios", SqlDbType.Xml).Value = Propietarios.OuterXml.ToString();
               cmd.ExecuteNonQuery();
               cnSql.Close();
           }
           catch (Exception ex)
           {
               cnSql.Close();
           }
       }

        public string Get_Actividad_Manejo(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Actividad_Manejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar,500).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "";
            }
        }

        public int Get_Actividad_ManejoId(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Actividad_ManejoId", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 500).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public string Get_Identificacion_Gestion_Manejo(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Identificacion_Gestion_Manejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "";
            }
        }

        public string Get_NomCategoria_Manejo(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_NomCategoria_Manejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "";
            }
        }


        public string Get_SubCategoria_Manejo(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_SubCategoria_Manejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "";
            }
        }

        public void Insert_Temp_Compromiso_Calculo(int AsignacionId, string AreaBasal_Extrae, string AreaBasalExistente, string AreaTotalIntervenir, string AreaCompromiso)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insert_Temp_Compromiso_Calculo", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", SqlDbType.Int).Value = AsignacionId;
                if (AreaBasal_Extrae == "")
                    cmd.Parameters.Add("@AreaBasal_Extrae", SqlDbType.Int).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@AreaBasal_Extrae", SqlDbType.Decimal).Value = AreaBasal_Extrae;
                if (AreaBasalExistente == "")
                    cmd.Parameters.Add("@AreaBasalExistente", SqlDbType.Int).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@AreaBasalExistente", SqlDbType.Decimal).Value = AreaBasalExistente;
                if (AreaTotalIntervenir == "")
                    cmd.Parameters.Add("@AreaTotalIntervenir", SqlDbType.Int).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@AreaTotalIntervenir", SqlDbType.Decimal).Value = AreaTotalIntervenir;
                if (AreaCompromiso == "")
                    cmd.Parameters.Add("@AreaCompromiso", SqlDbType.Int).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@AreaCompromiso", SqlDbType.Decimal).Value = AreaCompromiso;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public DataSet Get_CalculosCompromisoForestal(int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_CalculosCompromisoForestal", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        public double Sum_VolTotalSilvicultura(int AsignacionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Get_Sum_VolTotalSilvicultura", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = @AsignacionId;
                cmd.Parameters.Add("@Resul", OleDbType.Double).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToDouble(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public string Get_TratamientoSilvicultura(int AsignacionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Get_TratamientoSilvicultura", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = @AsignacionId;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar,200).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "";
            }
        }

        
    }
}