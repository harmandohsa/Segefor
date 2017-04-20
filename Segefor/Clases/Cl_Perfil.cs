using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace SEGEFOR.Clases
{

    public class Cl_Perfil
    {
        OleDbConnection cn = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["Conexion"]);
        DataSet ds = new DataSet();

        public DataSet Perfiles_Ambito_GetAll()
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Perfiles_Ambito", cn);
                cmd.CommandType = CommandType.StoredProcedure;
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

        public void Actualiza_Perfil(int Tipo_UsuarioId, int AmbitoId, string Nombre)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Actuliza_Perfil", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tipo_UsuarioId", OleDbType.Integer).Value = Tipo_UsuarioId;
                cmd.Parameters.Add("@AmbitoId", OleDbType.Integer).Value = AmbitoId;
                cmd.Parameters.Add("@Nombre", OleDbType.VarChar,200).Value = Nombre;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public DataSet Roles_Perfil(int PerfilId, int Padre, int Inicio)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Roles_Perfil", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tipo_UsuarioId", OleDbType.Integer).Value = PerfilId;
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

        public void Actualiza_Rol(int Tipo_UsuarioId, int FormaId, int Permiso, int Actividad)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Actuliza_Rol", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tipo_UsuarioId", OleDbType.Integer).Value = Tipo_UsuarioId;
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

        public bool Existe_Perfil(string Perfil)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Existe_Perfil", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tipo_Usuario", OleDbType.VarChar).Value = Perfil;
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

        public void Insert_Perfil(string TipoUsuario, int AmbitoId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Insert_Perfil", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@TipoUsuario", OleDbType.VarChar,200).Value = TipoUsuario;
                cmd.Parameters.Add("@Ambito_Id", OleDbType.Integer).Value = AmbitoId;
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