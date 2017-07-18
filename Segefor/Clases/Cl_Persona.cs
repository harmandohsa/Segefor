using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace SEGEFOR.Clases
{
    public class Cl_Persona
    {
        OleDbConnection cn = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["Conexion"]);
        DataSet ds = new DataSet();

        public bool Existe_Dpi(string Dpi, int Tipo, int PaisId = 0)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Existe_Dpi", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Dpi", OleDbType.VarChar,13).Value = Dpi;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@PaisId", OleDbType.Integer).Value = PaisId;
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

        public string Nombre_Usuario(int PersonaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_NombreUsuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = PersonaId;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar,400).Direction = ParameterDirection.Output;
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

        public void Actualiza_Foto(int PersonaId, byte[] Archivo, string ContentType, string Nombre_Archivo)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Inserta_Foto_Perfil", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = PersonaId;
                cmd.Parameters.Add("@ContentType", OleDbType.VarChar,200).Value = ContentType;
                cmd.Parameters.Add("@Nombre_Archivo", OleDbType.VarChar,200).Value = Nombre_Archivo;
                cmd.Parameters.AddWithValue("@File", OleDbType.VarBinary).Value = Archivo;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }


        

        public bool Existe_FotoPerfil(int PersonaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Existe_Foto_Perfil", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = PersonaId;
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

        public DataSet Foto_Perfil(int PersonaId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Foto_Perfil_Get", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = PersonaId;
                cmd.Parameters.Add("@MensajeError", OleDbType.VarChar,250).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@Error", OleDbType.Integer).Direction = ParameterDirection.Output;
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

        

        public void Insertar_Persona(int PersonaId, string Nombres, string Apellidos, DateTime Fec_Nac, int GeneroId, string DPI, string Telefono, string Direccion, int MunicipioId, int Tipo_Identificacion, DateTime Fec_VenId, int PaisId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Persona_Insert", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = PersonaId;
                cmd.Parameters.Add("@Nombres", OleDbType.VarChar,200).Value = Nombres;
                cmd.Parameters.Add("@Apellidos", OleDbType.VarChar,200).Value = Apellidos;
                if (Fec_Nac == Convert.ToDateTime(string.Format("{0:dd/MM/yyyy}", "01/01/2000")))
                    cmd.Parameters.Add("@Fec_Nac", OleDbType.Date).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Fec_Nac", OleDbType.Date).Value = Fec_Nac;
                if (GeneroId == 0)
                    cmd.Parameters.Add("@GeneroId", OleDbType.Integer).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@GeneroId", OleDbType.Integer).Value = GeneroId;
                cmd.Parameters.Add("@DPI", OleDbType.VarChar,50).Value = DPI;
                if (Telefono == "")
                    cmd.Parameters.Add("@Telefono", OleDbType.Integer).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Telefono", OleDbType.Integer).Value = Telefono;
                if (Direccion == "")
                    cmd.Parameters.Add("@Direccion", OleDbType.VarChar,200).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Direccion", OleDbType.VarChar, 200).Value = Direccion;
                if (MunicipioId == 0)
                    cmd.Parameters.Add("@MunicipioId", OleDbType.Integer).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@MunicipioId", OleDbType.Integer).Value = MunicipioId;
                cmd.Parameters.Add("@Tipo_Identificacion", OleDbType.Integer).Value = Tipo_Identificacion;
                if (Fec_VenId == Convert.ToDateTime(string.Format("{0:dd/MM/yyyy}", "01/01/2000")))
                    cmd.Parameters.Add("@Fec_Ven", OleDbType.Date).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Fec_Ven", OleDbType.Date).Value = Fec_VenId;
                cmd.Parameters.Add("@PaisId", OleDbType.Integer).Value = PaisId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public void Insertar_Persona_Propietario(int PersonaId, string Nombres, string Apellidos, string DPI, DateTime FecVencimiento, int Origen_PersonaId, int PaisId, int GeneroId = 0, DateTime Fec_Nac = default(DateTime), int EstadoCivilId = 0, int OcupacionId = 0)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Persona_Insert_Propietario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = PersonaId;
                cmd.Parameters.Add("@Nombres", OleDbType.VarChar,200).Value = Nombres;
                cmd.Parameters.Add("@Apellidos", OleDbType.VarChar,200).Value = Apellidos;
                cmd.Parameters.Add("@DPI", OleDbType.VarChar,13).Value = DPI;
                cmd.Parameters.Add("@FechaVencimiento", OleDbType.Date).Value = FecVencimiento;
                cmd.Parameters.Add("@Origen_PersonaId", OleDbType.Integer).Value = Origen_PersonaId;
                if (PaisId == 0)
                    cmd.Parameters.Add("@PaisId", OleDbType.Integer).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@PaisId", OleDbType.Integer).Value = PaisId;
                if (GeneroId == 0)
                    cmd.Parameters.Add("@GeneroId", OleDbType.Integer).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@GeneroId", OleDbType.Integer).Value = GeneroId;
                if (Fec_Nac == default(DateTime))
                    cmd.Parameters.Add("@Fec_Nac", OleDbType.Date).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Fec_Nac", OleDbType.Date).Value = Fec_Nac;
                if (EstadoCivilId == 0)
                    cmd.Parameters.Add("@EstadoCivilId", OleDbType.Integer).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@EstadoCivilId", OleDbType.Integer).Value = EstadoCivilId;
                if (OcupacionId == 0)
                    cmd.Parameters.Add("@OcupacionId", OleDbType.Integer).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@OcupacionId", OleDbType.Integer).Value = OcupacionId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }


        public int MaxPersonaId()
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Max_Persona", cn);
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

        public int Genero_Usuario(int PersonaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Genero_Usuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = PersonaId;
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

        public DataSet Datos_Persona(int PersonaId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Persona_GET", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = PersonaId;
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

        public DataSet Datos_Persona_Dpi(string Dpi, int Tipo, int PaisId = 0)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Persona_GET_Dpi", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Dpi", OleDbType.VarChar, 13).Value = Dpi;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@PaisId", OleDbType.Integer).Value = PaisId;
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

        public bool Existe_Nit(string Nit)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Existe_Nit", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Nit", OleDbType.VarChar,15).Value = Nit;
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

        public void Actualiza_DatosEspecificos(int PersonaId, int GradoId, string Nit, int EtniaId, int GrupoLinId, int OcupacionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_DatosEspecificos_Update", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = PersonaId;
                if (GradoId == 0)
                    cmd.Parameters.Add("@GradoId", OleDbType.Integer).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@GradoId", OleDbType.Integer).Value = GradoId;
                if (Nit == "")
                    cmd.Parameters.Add("@NIT", OleDbType.VarChar,15).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@NIT", OleDbType.VarChar,15).Value = Nit;
                if (EtniaId == 0)
                    cmd.Parameters.Add("@EtniaId", OleDbType.Integer).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@EtniaId", OleDbType.Integer).Value = EtniaId;
                if (GrupoLinId == 0)
                    cmd.Parameters.Add("@GrupoLinId", OleDbType.Integer).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@GrupoLinId", OleDbType.Integer).Value = GrupoLinId;
                if (OcupacionId == 0)
                    cmd.Parameters.Add("@OcupacionId", OleDbType.Integer).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@OcupacionId", OleDbType.Integer).Value = OcupacionId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public void Insertar_DatosPersona_INAB(int PersonaId, int PuestoId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Insertar_DatosPersona_INAB", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = PersonaId;
                cmd.Parameters.Add("@PuestoId", OleDbType.Integer).Value = PuestoId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public int GetPersonaId(int UsuarioId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_GetPersonaId", cn);
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

        public void ActualizaNit_Persona(string Nit, int PersonaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_ActualizaNit_Persona", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Nit", OleDbType.VarChar, 20).Value = Nit;
                cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = PersonaId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public void ActualizaCel_Persona(int Celular, int PersonaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_ActualizaCel_Persona", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Celular", OleDbType.Integer).Value = Celular;
                cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = PersonaId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

    }
}