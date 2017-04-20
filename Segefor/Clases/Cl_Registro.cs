using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace SEGEFOR.Clases
{
    public class Cl_Registro
    {
        OleDbConnection cn = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["Conexion"]);
        DataSet ds = new DataSet();

        public int Existe_Codigo_Registro (string Codigo_Registro)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Existe_Registro", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Codigo_registro", OleDbType.VarChar).Value = Codigo_Registro;
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

        public int Get_RegistroId_Rnf(string Codigo_Registro)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_RegistroId_Rnf", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Codigo_registro", OleDbType.VarChar).Value = Codigo_Registro;
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

        public string Get_Codigo_RNF(int RegistroId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Codigo_RNF", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@RegistroId", OleDbType.Integer).Value = RegistroId;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar,10).Direction = ParameterDirection.Output;
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

       
        public DataSet Get_Categoria_Registro_Usuario(int PersonaId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Categoria_Registro_Usuario", cn);
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

        public void Insertar_Relacion_Registro(int PersonaId, int RegistroId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Insertar_Relacion_Registro", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = PersonaId;
                cmd.Parameters.Add("@RegistroId", OleDbType.Integer).Value = RegistroId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public string Get_Correo_Regente(int RegistroId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Correo_Regente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@RegistroId", OleDbType.Integer).Value = RegistroId;
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

        public int EsElaboradorPM_Activo(int PersonaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_EsElaboradorPM_Activo", cn);
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

    }
}