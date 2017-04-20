using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace SEGEFOR.Clases
{
    

    public class Cl_Profesion
    {
        OleDbConnection cn = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["Conexion"]);
        DataSet ds = new DataSet();

        public bool Existe_Profesion(string Profesion, int CategoriaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Existe_Profesion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Profesion", OleDbType.VarChar, 200).Value = Profesion;
                cmd.Parameters.Add("@CategoriaId", OleDbType.Integer).Value = CategoriaId;
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

        public void Inserta_Profesion(string Profesion, int CategoriaId, int EstatusId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Profesion_Insert", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Profesion", OleDbType.VarChar, 200).Value = Profesion;
                cmd.Parameters.Add("@CategoriaId", OleDbType.Integer).Value = CategoriaId;
                cmd.Parameters.Add("@EstatusId", OleDbType.Integer).Value = EstatusId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public DataSet Profesion_Get(int ProfesionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Profesion_Get", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ProfesionId", OleDbType.Integer).Value = ProfesionId;
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

        public void Actualiza_Profesion(int ProfesionId, string Profesion, int CategoriaId, int EstatusId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Profesion_Update", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ProfesionId", OleDbType.Integer).Value = ProfesionId;
                cmd.Parameters.Add("@Profesion", OleDbType.VarChar, 200).Value = Profesion;
                cmd.Parameters.Add("@CategoriaId", OleDbType.Integer).Value = CategoriaId;
                cmd.Parameters.Add("@EstatusId", OleDbType.Integer).Value = EstatusId;
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