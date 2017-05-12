using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace SEGEFOR.Clases
{
    public class Cl_Usuario
    {
        OleDbConnection cn = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["Conexion"]);
        DataSet ds = new DataSet();

        public bool Existe_Correo(string Correo)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Existe_Correo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Correo", OleDbType.VarChar).Value = Correo;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                if (Convert.ToInt32(cmd.Parameters["@Resul"].Value) >= 1)
                    return true;
                else
                    return false;
                
            }
            catch (Exception ex)
            {
                cn.Close();
                return false;
            }
        }

        public bool Existe_Usuario(string Usuario)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Existe_Usuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Usuario", OleDbType.VarChar).Value = Usuario;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                if (Convert.ToInt32(cmd.Parameters["@Resul"].Value) >= 1)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                cn.Close();
                return false;
            }
        }

        

        public int UsurioId()
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Max_Usuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
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



        public int CorrId_IngSistema(int UsuarioId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Max_Ingreso_Sistema", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
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

        public int Get_UsuarioId_Persona(int PersonaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_UsuarioId_Persona", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = PersonaId;
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

        public void Insertar_Ingreso_Sistema(int UsuarioId, int CorrId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_IngresosSis_Insert", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.Parameters.Add("@CorrId", OleDbType.Integer).Value = CorrId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public void Actualiza_SalidaSis(int UsuarioId, int CorrId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Actualiza_Salida", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.Parameters.Add("@CorrId", OleDbType.Integer).Value = CorrId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public int CorrId_IngresoPagina()
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Max_Ingreso_Pagina", cn);
                cmd.CommandType = CommandType.StoredProcedure;
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

        public int CorrId_ActividadPagina()
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Max_Actividad_Pagina", cn);
                cmd.CommandType = CommandType.StoredProcedure;
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

        public void Insertar_Ingreso_Pagina(int FormaId, int UsuarioId, int BitacoraPantallaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Ingresos_Pagina", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@FormaId", OleDbType.Integer).Value = FormaId;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.Parameters.Add("@BitacoraPantallaId", OleDbType.Integer).Value = BitacoraPantallaId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public void Insertar_Actividad_Pagina(int FormaId, int UsuarioId, int CorrId, int ActividadId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Actividad_Pagina_Insert", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@FormaId", OleDbType.Integer).Value = FormaId;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.Parameters.Add("@CorrId", OleDbType.Integer).Value = CorrId;
                cmd.Parameters.Add("@ActividadId", OleDbType.Integer).Value = ActividadId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public void Insertar_Usuario(int UsuarioId, string Usuario, int Tipo_Usuario_id, string Clave, int PersonaId, int Tipo_Contratacion,  int UsuarioIdCre, string Correo)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Usuario_Insert", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.Parameters.Add("@Usuario", OleDbType.VarChar,200).Value = Usuario;
                cmd.Parameters.Add("@Tipo_Usuario_Id", OleDbType.Integer).Value = Tipo_Usuario_id;
                cmd.Parameters.Add("@Clave", OleDbType.VarChar,500).Value = Clave;
                cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = PersonaId;
                if (Tipo_Contratacion == 0)
                    cmd.Parameters.Add("@Tipo_Contratacion", OleDbType.Integer).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Tipo_Contratacion", OleDbType.Integer).Value = Tipo_Contratacion;
                if (UsuarioIdCre == 0)
                    cmd.Parameters.Add("@UsuarioIdCre", OleDbType.Integer).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@UsuarioIdCre", OleDbType.Integer).Value = UsuarioIdCre;
                cmd.Parameters.Add("@Correo", OleDbType.VarChar, 200).Value = Correo;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch(Exception ex)
            {
                cn.Close();
            }
        }

        public void Insertar_Permisos(int UsuarioId, int Tipo_Usuario_Id)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Permisos", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.Parameters.Add("@Tipo_Usuario_Id", OleDbType.Integer).Value = Tipo_Usuario_Id;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        

        public string Usuario_Get_Login(int UsuarioId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Usuario_Get_Login", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
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


        public DataSet Datos_Usuario(string Usuario)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Usuario_GET", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Usuario", OleDbType.VarChar,200).Value = Usuario;
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

        public DataSet Datos_UsuarioId(int UsuarioId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Usuario_GET_Id", cn);
                cmd.CommandType = CommandType.StoredProcedure;
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

        public void Actualiza_Clave(int UsuarioId, string Clave, int Cambio_Pwd)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_ActualizaClave", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.Parameters.Add("@Clave", OleDbType.VarChar,500).Value = Clave;
                cmd.Parameters.Add("@Cambio_Pwd", OleDbType.Integer).Value = Cambio_Pwd;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public void Actualiza_DatosUsuario(int UsuarioId, string Usuario, string Correo, int Tipo_UsuarioId, DateTime Fec_VenId, int PersonaId, int Tipo)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Actualiza_DatosUsuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.Parameters.Add("@Usuario", OleDbType.VarChar,200).Value = Usuario;
                cmd.Parameters.Add("@Correo", OleDbType.VarChar,200).Value = Correo;
                cmd.Parameters.Add("@Tipo_UsuarioId", OleDbType.Integer).Value = Tipo_UsuarioId;
                cmd.Parameters.Add("@FecVenId", OleDbType.Date).Value = Fec_VenId;
                cmd.Parameters.Add("@Personaid", OleDbType.Integer).Value = PersonaId;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public int Get_UsuarioId(string Usuario)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_UsuarioId", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Usuario", OleDbType.VarChar,200).Value = Usuario;
                cmd.Parameters.Add("@Resul", OleDbType.BigInt).Direction = ParameterDirection.Output;
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

        public string Get_Clave(int UsuarioId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Clave", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
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

        public DataSet Permisos_Usuario(int UsuarioId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Permisos_GET", cn);
                cmd.CommandType = CommandType.StoredProcedure;
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

        public string Get_Tipo_Usuario(int UsuarioId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Tipo_Usuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
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

        public DataSet Get_Roles(int UsuarioId, int FormaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Roles_Forma_Usuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.BigInt, 13).Value = UsuarioId;
                cmd.Parameters.Add("@FormaId", OleDbType.Integer, 5).Value = FormaId;
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

        public Int64 Get_DpiEmplINAB(int EmplCod)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_DpiEmplINAB", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EmplCod", OleDbType.BigInt, 13).Value = EmplCod;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 500).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt64(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public Int64 Get_DpiPersona(int PersonaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_DpiPersona", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PersonaId", OleDbType.BigInt, 13).Value = PersonaId;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 500).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt64(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public DataSet Get_NombreEmplINAB(Int64 Dpi)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_NombreEmplINAB", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Dpi", OleDbType.BigInt).Value = Dpi;
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

        public DataSet Get_NombrePersona(Int64 Dpi)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_NombrePersona", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Dpi", OleDbType.BigInt).Value = Dpi;
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

        public DataSet Get_DatosEmplINAB(int CodEmpl)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_DatosEmplINAB", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CodEmpl", OleDbType.Integer).Value = CodEmpl;
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

        public int Traduce_region(int CodSubRegion)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Traduce_region", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CodSubRegion", OleDbType.Integer, 13).Value = CodSubRegion;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 500).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                if (cmd.Parameters["@Resul"].Value.ToString() == null)
                    return 0;
                else
                    return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public DataSet Get_Datos_Traduce_region(int CodSubRegion)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Datos_Traduce_region", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CodSubRegion", OleDbType.Integer).Value = CodSubRegion;
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

        public DataSet Get_Datos_Traduce_Puesto_Perfil(int PuestoId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Datos_Traduce_Puesto_Perfil", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CodSubRegion", OleDbType.Integer).Value = PuestoId;
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

        public int Get_Ambito_Perfil(int Tipo_UsuarioId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Ambito_Perfil", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tipo_UsuarioId", OleDbType.Integer).Value = Tipo_UsuarioId;
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

        public void Insert_Usuario_Subregion(int UsuarioId, int SubRegionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Insert_Usuario_Subregion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.Parameters.Add("@SubRegion_Id", OleDbType.Integer).Value = SubRegionId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public void Insert_Usuario_Modulo(int UsuarioId, int ModuloId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Insert_Usuario_Modulo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.Parameters.Add("@ModuloId", OleDbType.Integer).Value = ModuloId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public string Existe_Usuario_Region_SubRegion(int SubRegionId, int Tipo_UsuarioId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Existe_Usuario_Region_SubRegion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SubRegionId", OleDbType.Integer).Value = SubRegionId;
                cmd.Parameters.Add("@Tipo_UsuarioId", OleDbType.Integer).Value = Tipo_UsuarioId;
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

        public DataSet Get_Usuarios(int ModuloId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Usuarios", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ModuloId", OleDbType.Integer).Value = ModuloId;
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

        public void Cambio_Estatus_Usuario(int UsuarioId, int Estatus_UsrId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Cambio_Estatus_Usuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.Parameters.Add("@Estatus_UsrId", OleDbType.Integer).Value = Estatus_UsrId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }


        public DataSet Get_SubRegion_Usuario(int UsuarioId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_SubRegion_Usuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
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

        public DataSet Get_Modulo_Usuario(int UsuarioId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Modulo_Usuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
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

        public void Elimina_SubRegion_Usuario(int UsuarioId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Elimina_SubRegion_Usuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public void Elimina_Modulo_Usuario(int UsuarioId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Elimina_Modulo_Usuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public DataSet Roles_Usuario(int UsuarioId, int Padre, int Inicio)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Roles_Usuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tipo_UsuarioId", OleDbType.Integer).Value = UsuarioId;
                if (Padre == 0)
                    cmd.Parameters.Add("@Padre", OleDbType.Integer).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Padre", OleDbType.Integer).Value = Padre;
                cmd.Parameters.Add("@Inicio", OleDbType.Integer).Value = Inicio;
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

        public void Actualiza_Rol_Usuario(int UsuarioId, int FormaId, int Permiso, int Actividad)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Actuliza_Rol_Usuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tipo_UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.Parameters.Add("@FormaId", OleDbType.Integer).Value = FormaId;
                cmd.Parameters.Add("@Permiso", OleDbType.Integer).Value = Permiso;
                cmd.Parameters.Add("@Actividad", OleDbType.Integer).Value = Actividad;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }



        public string GetCorreoSolicitante(int AsignacionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_GetCorreoSolicitante", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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

        
    }
}